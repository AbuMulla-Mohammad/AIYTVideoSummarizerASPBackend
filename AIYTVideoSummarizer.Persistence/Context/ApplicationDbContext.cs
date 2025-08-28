
using AIYTVideoSummarizer.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIYTVideoSummarizer.Persistence.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserExternalLogin> UserExternalLogins { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Summary> Summaries { get; set; }
        public DbSet<SummarySection> SummarySections { get; set; }
        public DbSet<SummarizationRequest> SummarizationRequests { get; set; }
        public DbSet<Prompt> Prompts { get; set; }
        public DbSet<FormattedTranscript> FormattedTranscripts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
