using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
            
        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(50);
                
        builder.HasIndex(u => u.Username)
            .IsUnique();
                
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
                
        builder.HasIndex(u => u.Email)
            .IsUnique();
                
        builder.Property(u => u.PasswordHash)
            .IsRequired();
                
        builder.Property(u => u.CreatedAt)
            .IsRequired();
    }
}