# Samuraizer — ASP.NET Backend

> Part of the **Samuraizer** project — an intelligent YouTube video summarization platform. Users submit a YouTube URL and receive a structured summary and formatted transcript powered by AI.

This repository contains the **main backend service** for Samuraizer. It handles user management, authentication, summarization request orchestration, data persistence, and all business logic. It communicates with the [Samuraizer AI Service](https://github.com/AbuMulla-Mohammad/YTVideoSummarizer-AIService) to perform the actual AI summarization.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Configuration / Environment Variables](#configuration--environment-variables)
- [Running Locally](#running-locally)
- [Database Setup](#database-setup)

---

## Overview

The ASP.NET Backend is a RESTful API built with **.NET 8** following **Clean Architecture** principles. When a user submits a YouTube URL, this service:

1. Validates the request and authenticates the user via **JWT**.
2. Calls the AI Service to validate the YouTube URL and retrieve the video ID.
3. Checks if a summary for the same video and prompt combination already exists in the database — if so, it returns the cached result immediately without re-summarizing.
4. If no cached summary exists, it calls the AI Service to generate a new summary and formatted transcript, persists everything to the database, and returns the result.
5. Logs every summarization request with its status (`Pending`, `Completed`, or `Failed`) for auditability.

The service also supports user registration with **email verification**, password reset flows, and **Google OAuth** external login.

---

## Architecture

The solution is organized into four projects following Clean Architecture:

```
AIYTVideoSummarizer.Domain          # Core entities, enums, repository interfaces, domain exceptions
AIYTVideoSummarizer.Application     # CQRS commands/queries (MediatR), handlers, DTOs, validators, AutoMapper profiles
AIYTVideoSummarizer.Infrastructure  # JWT, Argon2 password hashing, SMTP email, AI service HTTP client
AIYTVideoSummarizer.Persistence     # EF Core DbContext, migrations, repository implementations
AIYTVideoSummarizer.Api             # ASP.NET controllers, middleware, auth extensions, Swagger
```

**Key patterns used:**

- **CQRS** with MediatR — every operation is a named command or query.
- **Repository pattern** — all database access is behind interfaces defined in the Domain layer.
- **FluentValidation** — every command and query has a dedicated validator.
- **AutoMapper** — DTO-to-entity and entity-to-DTO mapping.
- **Global exception middleware** — maps domain exceptions to appropriate HTTP status codes.
- **Smart caching** — if the same video+prompt combination has been summarized before, the stored result is returned without re-calling the AI service.

---

## Tech Stack

| Layer             | Technology                                                                      |
| ----------------- | ------------------------------------------------------------------------------- |
| Framework         | ASP.NET Core 8                                                                  |
| ORM               | Entity Framework Core 8                                                         |
| Database          | PostgreSQL                                                                      |
| Authentication    | JWT Bearer Tokens + Google OAuth                                                |
| Password Hashing  | Argon2                                                                          |
| Mediator          | MediatR                                                                         |
| Mapping           | AutoMapper                                                                      |
| Validation        | FluentValidation                                                                |
| Email             | SMTP (MailKit)                                                                  |
| API Documentation | Swagger / [Postman](https://documenter.getpostman.com/view/37800136/2sB3Wk14ik) |
| AI Integration    | HTTP client calling the Samuraizer AI Service                                   |

---

## Project Structure

```
AIYTVideoSummarizer.Api/
├── Controllers/
│   ├── AccountController.cs            # Registration, login, Google login, password management
│   ├── UsersController.cs              # User profile and admin user management
│   ├── SummarizationRequestsController.cs  # Submit summarization requests; view history
│   ├── SummariesController.cs          # Retrieve summaries
│   ├── VideosController.cs             # Retrieve and manage videos
│   └── PromptsController.cs            # CRUD for AI prompt configurations
├── Middlewares/
│   └── ExceptionHandlerMiddleware.cs   # Global error handling
└── Common/
    ├── Extensions/                     # Auth, authorization, claims helpers
    └── Responses/ApiResponse.cs        # Unified API response wrapper

AIYTVideoSummarizer.Application/
├── Commands/                           # Write operations (CQRS)
├── Queries/                            # Read operations (CQRS)
├── Handlers/                           # MediatR request handlers
├── DTOs/                               # Data Transfer Objects
├── Validators/                         # FluentValidation validators
└── Profiles/                           # AutoMapper profiles

AIYTVideoSummarizer.Domain/
├── Entities/                           # User, Video, Summary, SummarizationRequest, Prompt, etc.
├── Enums/                              # RequestStatus, UserRole
├── Common/Exceptions/                  # Domain-specific exceptions
└── Common/Interfaces/Repositories/    # Repository contracts

AIYTVideoSummarizer.Infrastructure/
├── ExternalServices/AIService/         # HTTP client wrapper for the AI service
├── Security/                           # JWT provider, Argon2 hasher
└── Services/                           # SMTP email sender, username generator

AIYTVideoSummarizer.Persistence/
├── Context/                            # EF Core DbContext
├── Configurations/                     # Entity type configurations
├── Migrations/                         # EF Core migrations
└── Repositories/                       # Concrete repository implementations
```

---

## API Endpoints

> Full interactive documentation is available on the [Postman collection](https://documenter.getpostman.com/view/37800136/2sB3Wk14ik).

### Authentication (`/api/account`)

| Method | Endpoint                             | Auth   | Description                                            |
| ------ | ------------------------------------ | ------ | ------------------------------------------------------ |
| `POST` | `/api/account`                       | Public | Register a new user. Sends an email verification link. |
| `GET`  | `/api/account/verify-email?token=`   | Public | Verify email address using the token sent by email.    |
| `POST` | `/api/account/login`                 | Public | Log in with email and password. Returns a JWT token.   |
| `POST` | `/api/account/google-login`          | Public | Log in or register using a Google OAuth token.         |
| `POST` | `/api/account/forgot-password`       | Public | Request a password reset email.                        |
| `POST` | `/api/account/reset-password?token=` | Public | Reset password using the token from the reset email.   |
| `POST` | `/api/account/change-password`       | 🔒 JWT | Change the current user's password.                    |

---

### Summarization Requests (`/api/summarizationrequests`)

This is the **core endpoint** of the application. Submitting a request triggers the full summarization pipeline.

| Method | Endpoint                              | Auth     | Description                                                                                                                                                                        |
| ------ | ------------------------------------- | -------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `POST` | `/api/summarizationrequests`          | 🔒 JWT   | Submit a YouTube URL for summarization. Returns summary sections and the formatted transcript. If the same video+prompt has been summarized before, the cached result is returned. |
| `GET`  | `/api/summarizationrequests/user`     | 🔒 JWT   | Get the current user's summarization request history (paginated).                                                                                                                  |
| `GET`  | `/api/summarizationrequests/{id}`     | 🔒 JWT   | Get details of a specific summarization request.                                                                                                                                   |
| `GET`  | `/api/summarizationrequests`          | 🔒 Admin | Get all summarization requests (paginated).                                                                                                                                        |
| `GET`  | `/api/summarizationrequests/{status}` | 🔒 Admin | Filter requests by status: `Pending`, `Completed`, or `Failed`.                                                                                                                    |

**Create Request Body:**

```json
{
  "youTubeUrl": "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
  "promptId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

**Response includes:** `summarySections` (each with `title`, `summary`, `start`, `end`) and `formattedTranscripts` (each with `text`, `start`, `end`).

---

### Summaries (`/api/summaries`)

| Method | Endpoint                       | Auth     | Description                                                          |
| ------ | ------------------------------ | -------- | -------------------------------------------------------------------- |
| `GET`  | `/api/summaries/{id}`          | Public   | Get a specific summary by ID, including all sections and transcript. |
| `GET`  | `/api/summaries/user`          | 🔒 JWT   | Get summaries belonging to the current user (paginated, searchable). |
| `GET`  | `/api/summaries`               | 🔒 Admin | Get all summaries (paginated).                                       |
| `GET`  | `/api/summaries/videoId/{id}`  | 🔒 Admin | Get all summaries for a specific video (paginated, searchable).      |
| `GET`  | `/api/summaries/promptId/{id}` | 🔒 Admin | Get all summaries generated with a specific prompt (paginated).      |

---

### Videos (`/api/videos`)

| Method   | Endpoint           | Auth     | Description                                                            |
| -------- | ------------------ | -------- | ---------------------------------------------------------------------- |
| `GET`    | `/api/videos/{id}` | 🔒 JWT   | Get details of a specific video by ID.                                 |
| `GET`    | `/api/videos/user` | 🔒 JWT   | Get all videos summarized by the current user (paginated, searchable). |
| `GET`    | `/api/videos`      | 🔒 Admin | Get all videos in the system (paginated).                              |
| `DELETE` | `/api/videos/{id}` | 🔒 Admin | Delete a video and its associated data.                                |

---

### Prompts (`/api/prompts`)

Prompts configure which AI summarization strategy is used. Admins manage the available prompts; all users can read them.

| Method   | Endpoint            | Auth     | Description                                           |
| -------- | ------------------- | -------- | ----------------------------------------------------- |
| `GET`    | `/api/prompts`      | Public   | List all available prompt configurations (paginated). |
| `GET`    | `/api/prompts/{id}` | Public   | Get details of a specific prompt.                     |
| `POST`   | `/api/prompts`      | 🔒 Admin | Create a new prompt configuration.                    |
| `PUT`    | `/api/prompts/{id}` | 🔒 Admin | Update an existing prompt.                            |
| `DELETE` | `/api/prompts/{id}` | 🔒 Admin | Delete a prompt.                                      |

---

### Users (`/api/users`)

| Method | Endpoint                  | Auth     | Description                                   |
| ------ | ------------------------- | -------- | --------------------------------------------- |
| `GET`  | `/api/users/user/profile` | 🔒 JWT   | Get the current authenticated user's profile. |
| `GET`  | `/api/users/{id}`         | 🔒 Admin | Get details for any user by ID.               |
| `GET`  | `/api/users`              | 🔒 Admin | List all users (paginated).                   |

---

**Pagination** is returned in the `X-Pagination` response header for all paginated endpoints. Query parameters `pageNumber` and `pageSize` are supported.

---

## Configuration / Environment Variables

All configuration lives in `appsettings.json`. For production, override sensitive values using environment variables or a secrets manager.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "<your-PostgreSQL-database-connection-string>"
  },
  "AIService": {
    "BaseUrl": "http://127.0.0.1:8000/"
  },
  "PasswordHasher": {
    "SaltSize": "16",
    "TimeCost": "3",
    "Secret": "<random-secret-string>",
    "HashLength": "32"
  },
  "AppSettings": {
    "ApiUrl": "https://localhost:7063"
  },
  "EmailSettings": {
    "FromAdress": "no-reply@yourdomain.com",
    "FromName": "Samuraizer Team",
    "SmtpServer": "smtp.yourmailprovider.com",
    "SmtpPort": 587,
    "SmtpUserName": "your-smtp-username",
    "SmtpPassword": "your-smtp-password"
  },
  "Authentication": {
    "SecretForKey": "<long-random-jwt-secret>",
    "Issuer": "Samuraizer",
    "Audience": "SamuraizerUsers",
    "Google": {
      "ClientId": "<google-oauth-client-id>",
      "ClientSecret": "<google-oauth-client-secret>"
    }
  }
}
```

| Key                                   | Required             | Description                                                                            |
| ------------------------------------- | -------------------- | -------------------------------------------------------------------------------------- |
| `ConnectionStrings:DefaultConnection` | ✅ Yes               | PostgreSQL connection string for EF Core.                                              |
| `AIService:BaseUrl`                   | ✅ Yes               | Base URL of the running Samuraizer AI Service. Default: `http://127.0.0.1:8000/`.      |
| `PasswordHasher:Secret`               | ✅ Yes               | Secret pepper for Argon2 password hashing. Use a long random string.                   |
| `Authentication:SecretForKey`         | ✅ Yes               | Secret key for signing JWT tokens. Must be at least 32 characters.                     |
| `Authentication:Issuer`               | ✅ Yes               | JWT issuer claim. Default: `Samuraizer`.                                               |
| `Authentication:Audience`             | ✅ Yes               | JWT audience claim. Default: `SamuraizerUsers`.                                        |
| `Authentication:Google:ClientId`      | ⚠️ Google login only | Google OAuth 2.0 client ID from Google Cloud Console.                                  |
| `Authentication:Google:ClientSecret`  | ⚠️ Google login only | Google OAuth 2.0 client secret.                                                        |
| `EmailSettings:SmtpServer`            | ✅ Yes               | SMTP server hostname for sending verification and reset emails.                        |
| `EmailSettings:SmtpPort`              | ✅ Yes               | SMTP port. Default: `587`.                                                             |
| `EmailSettings:SmtpUserName`          | ✅ Yes               | SMTP authentication username.                                                          |
| `EmailSettings:SmtpPassword`          | ✅ Yes               | SMTP authentication password.                                                          |
| `EmailSettings:FromAdress`            | ✅ Yes               | The sender email address shown to users.                                               |
| `AppSettings:ApiUrl`                  | ✅ Yes               | The public base URL of this API, used for building email verification and reset links. |

---

## Running Locally

**Prerequisites:** .NET 8 SDK, PostgreSQL, and the [AI Service](https://github.com/AbuMulla-Mohammad/YTVideoSummarizer-AIService) running on port `8000`.

```bash
# Clone the repository
git clone hhttps://github.com/AbuMulla-Mohammad/AIYTVideoSummarizerASPBackend
cd AIYTVideoSummarizerASPBackend

# Restore dependencies
dotnet restore

# Set up configuration
# Edit AIYTVideoSummarizer.Api/appsettings.json with your values
# (or use dotnet user-secrets for local development)

# Apply database migrations
dotnet ef database update --project AIYTVideoSummarizer.Persistence --startup-project AIYTVideoSummarizer.Api

# Run the API
dotnet run --project AIYTVideoSummarizer.Api
```

## The API will start on `https://localhost:7063` by default.

## Database Setup

The service uses **Entity Framework Core** with PostgreSQL. Two migrations are included:

- `InitialCreate` — creates all core tables (Users, Videos, Summaries, SummarySections, Prompts, SummarizationRequests, FormattedTranscripts, UserExternalLogins).
- `AddUserSecurityFields` — adds security-related fields to the User entity (email verification tokens, password reset tokens, etc.).

---

## Related Repositories

- **[Samuraizer AI Service](https://github.com/AbuMulla-Mohammad/YTVideoSummarizer-AIService)** — The Python/FastAPI microservice that handles YouTube transcript extraction and Cohere-powered summarization.
