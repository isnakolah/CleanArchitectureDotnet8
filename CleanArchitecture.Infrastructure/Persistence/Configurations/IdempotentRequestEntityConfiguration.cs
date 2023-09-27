using CleanArchitecture.Infrastructure.Idempotency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

internal sealed class IdempotentRequestEntityConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
    public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
    {
        builder.HasKey(ir => ir.Id);
        builder.Property(ir => ir.Name).IsRequired();
    }
}