using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>(entity =>
            {
                entity.Property(p => p.Title).IsRequired().HasMaxLength(NumberValues.PostTitleLength);
                entity.Property(p => p.Message).IsRequired();
                entity.Property(p => p.Preview).IsRequired().HasMaxLength(NumberValues.PostPreviewLength);
            });

            builder.Entity<Comment>(entity =>
            {
                entity.Property(p => p.Message).IsRequired().HasMaxLength(NumberValues.CommentMessageLength);
            });
        }
    }
}
