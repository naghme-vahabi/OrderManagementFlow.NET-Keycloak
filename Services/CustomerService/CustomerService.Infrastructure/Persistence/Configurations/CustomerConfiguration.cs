using CustomerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(t => t.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Email)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
