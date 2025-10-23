using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure.Configurations;

public class InventoryStockModelConfiguration : IEntityTypeConfiguration<InventoryStockModel>
{
    public void Configure(EntityTypeBuilder<InventoryStockModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("integer"); 
        
        builder.HasOne(x => x.InventoryItemModel)
            .WithOne(x => x.InventoryStock) 
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.InventoryItemId);
    }
}