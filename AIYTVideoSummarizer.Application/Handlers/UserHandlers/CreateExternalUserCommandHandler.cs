using AIYTVideoSummarizer.Application.Commands.UserCommands;
using AIYTVideoSummarizer.Domain.Common.Interfaces.Repositories;
using AIYTVideoSummarizer.Domain.Entities;
using AIYTVideoSummarizer.Domain.Enums;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIYTVideoSummarizer.Application.Handlers.UserHandlers
{
    public class CreateExternalUserCommandHandler : IRequestHandler<CreateExternalUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateExternalUserCommandHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateExternalUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            user.Role = UserRole.User;

            user.ExternalLogins.Add(new UserExternalLogin
            {
                Id = Guid.NewGuid(),
                LoginProvider = request.LoginProvider,
                ProviderKey = request.ProviderKey,
                UserId = user.Id,
                User = user,
            });

            await _userRepository.AddAsync(user);
        }
    }
}
