using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure.Configurations;

public class InventoryAuditLogDataModelConfiguration : IEntityTypeConfiguration<InventoryAuditLogDataModel>
{
    public void Configure(EntityTypeBuilder<InventoryAuditLogDataModel> builder)
    {
        builder.ToTable("InventoryAuditLog"); 
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Brand).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Model).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Color).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Size).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.HasIndex(x => x.Code);
        builder.Property(x => x.Quantity).IsRequired().HasDefaultValue(0);
    }
}