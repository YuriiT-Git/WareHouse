using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WIS.Infrastructure.Persistence.Entities;

namespace WIS.Infrastructure.Persistence.Configurations;

public class EventsStorageModelConfiguration : IEntityTypeConfiguration<EventsStorageModel>
{
    public void Configure(EntityTypeBuilder<EventsStorageModel> builder)
    {
        builder.ToTable("EventsStorage"); 
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.Code).IsRequired().HasMaxLength(50);
        builder.Property(x => x.EventType).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Data).IsRequired().HasMaxLength(500);
    }
}