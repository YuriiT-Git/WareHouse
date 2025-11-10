using Microsoft.EntityFrameworkCore;
using WIS.Application.Common.Common.Abstractions;
using WIS.Domain.Abstractions;
using WIS.Domain.Entities;
using WIS.Infrastructure.Persistence.Entities;
using WIS.Infrastructure.Persistence.Extensions;

namespace WIS.Infrastructure.Persistence.Repositories;

public class EventStorageRepository(WareHouseDbContext dbContext) : IEventStorageRepository
{
    public async Task AddAsync(IDomainEvent @event, CancellationToken cancellation)
    {
        var eventModel = @event.ToDbModel();
        await dbContext.Set<EventsStorageModel>().AddAsync(eventModel, cancellation);
        await dbContext.SaveChangesAsync(cancellation);
    }

    public async Task<InventoryItemAggregate> GetAsync(string skuNumber, CancellationToken cancellation)
    {
        var dbEventsPerItem = await dbContext
            .Set<EventsStorageModel>()
            .Where(x => x.Code == skuNumber)
            .Select(x => x.ToDomain())
            .ToListAsync(cancellation);

        return InventoryItemAggregate.FromHistory(dbEventsPerItem);
    }

    public async Task<InventoryItemAggregate> GetByDateTimeAsync(string skuNumber, DateTimeOffset dateTime, CancellationToken cancellation)
    {
        var items = await dbContext
            .Set<EventsStorageModel>()
            .Where(e => e.CreatedAt <= dateTime && e.Code == skuNumber)
            .OrderBy(e => e.Id)
            .Select(x => x.ToDomain())
            .ToListAsync(cancellation);
            

        return InventoryItemAggregate.FromHistory(items);
    }
}