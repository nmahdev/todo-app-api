using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Data.Configurations;

public class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.HasKey(t => t.Id);
            
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(100);
                
        builder.Property(t => t.Description)
            .HasMaxLength(500);
                
        builder.Property(t => t.IsCompleted)
            .HasDefaultValue(false);
                
        builder.Property(t => t.CreatedAt)
            .IsRequired();
                
        // Relationship with User
        builder.HasOne<User>()
            .WithMany(u => u.Todos)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}