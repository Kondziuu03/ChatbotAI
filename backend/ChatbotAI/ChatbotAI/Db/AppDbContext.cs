using ChatbotAI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatbotAI.Db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<MessageRating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Rating)
                .WithOne(r => r.ChatMessage)
                .HasForeignKey<MessageRating>(r => r.ChatMessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}