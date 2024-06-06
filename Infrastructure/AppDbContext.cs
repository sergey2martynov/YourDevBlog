using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

            builder.Entity<Post>(post =>
            {
                post.Property(p => p.Title).IsRequired().HasMaxLength(NumberValues.PostTitleMaxLength);
                post.Property(p => p.Message).IsRequired();
                post.Property(p => p.Preview).IsRequired().HasMaxLength(NumberValues.PostPreviewMaxLength);
                post.HasMany(p => p.Comments).WithOne(c => c.Post).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);
                post.HasMany(p => p.MediaFiles).WithOne(c => c.Post).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Comment>(comment =>
            {
                comment.Property(c => c.Message).IsRequired().HasMaxLength(NumberValues.CommentMessageMaxLength);
            });

            builder.Entity<MediaFile>(file =>
            {
                file.Property(f => f.Url).IsRequired().HasMaxLength(NumberValues.MediaFileUrlMaxLength);
                file.Property(f => f.MediaFileType).HasConversion<int>().IsRequired();
            });

            builder.Entity<Tag>(category =>
            {
                category.Property(c => c.Name).IsRequired().HasMaxLength(NumberValues.TagNameMaxLength);
                category.HasIndex(c => c.Name);
            });
        }
    }
}
