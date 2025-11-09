using WIS.Domain.Abstractions;
using WIS.Domain.Events;
using WIS.Domain.Exceptions;
using WIS.Domain.Extensions;
using WIS.Domain.ValueObjects.Enums;

namespace WIS.Domain.Entities;

public sealed class InventoryItemAggregate
{
    private string? _skuNumber;
    public string SkuNumber => _skuNumber ??= GetSkUNumber();
    public ProductType ProductType { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public ItemSize Size { get; private set; }
    public string Color { get; private set; }
    
    public InventoryStock InventoryStock { get; private set; } = InventoryStock.Create(0);
    private bool IsCreated { get; set; }
    
    private readonly List<IDomainEvent> _pendingDomainEvents = new();
    
    private InventoryItemAggregate()
    {
    }
    
    public void RegisterIncomingStock(int quantity)
    {
        var @event = new StockAddedEvent
        {
            CreatedAt = DateTimeOffset.UtcNow,
            SkuNumber = SkuNumber,
            Quantity = quantity
        };
        
        Apply(@event, false);
        _pendingDomainEvents.Add(@event);
    }

    public void RegisterOutgoingStock(int quantity)
    {
        var @event = new StockRemovedEvent
        {
            SkuNumber = SkuNumber,
            CreatedAt = DateTimeOffset.UtcNow,
            Quantity = quantity
        };
        
        Apply(@event, false);
        _pendingDomainEvents.Add(@event);
    }

    public async Task RaiseEventsAsync(Func<IDomainEvent, Task> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
       
        var eventsToRaise = _pendingDomainEvents.ToList();
        _pendingDomainEvents.Clear();

        foreach (var domainEvent in eventsToRaise)
        {
            await handler(domainEvent);
        }
    }
    
    public static InventoryItemAggregate Create(InventoryItemCreatedEvent @event)
    {
        var aggregate = new InventoryItemAggregate();
        aggregate.Apply(@event, false);
        return aggregate;
    }
    
    public static InventoryItemAggregate FromHistory(IEnumerable<IDomainEvent> events)
    {
        var aggregate = new InventoryItemAggregate();

        foreach (var e in events)
        {
            aggregate.Apply(e, true);
        }

        if (!aggregate.IsCreated)
        {
            throw new InventoryNotFoundException("");
        }

        return aggregate;
    }

    private void Apply(IDomainEvent @event, bool hasReadFromDb)
    {
        switch (@event.GetTypeName())
        {
            case nameof(InventoryItemCreatedEvent):
                InitAggregate((InventoryItemCreatedEvent)@event, hasReadFromDb);
                break;
            case nameof(StockAddedEvent):
                ThrowIfAggregateIsNotCreated();
                InventoryStock.RegisterIncoming(((StockAddedEvent)@event).Quantity);
                break;

            case nameof(StockRemovedEvent):
                ThrowIfAggregateIsNotCreated();
                InventoryStock.RegisterOutgoing(((StockRemovedEvent)@event).Quantity);
                break;
        }
    }

    private void ThrowIfAggregateIsNotCreated()
    {
        if (!IsCreated)
        {
            throw new InventoryNotCreatedException(SkuNumber);
        }
    }

    private void InitAggregate(InventoryItemCreatedEvent @event, bool hasReadFromDb)
    {
        if (string.IsNullOrEmpty(@event.Brand))
        {
            throw new InvalidBrandException("Brand cannot be empty");
        }

        if (string.IsNullOrEmpty(@event.Model))
        {
            throw new InvalidBrandException("Model cannot be empty");
        }

        if (string.IsNullOrEmpty(@event.Color))
        {
            throw new InvalidBrandException("Color cannot be empty");
        }

        ProductType = @event.ProductType;
        Brand = @event.Brand;
        Model = @event.Model;
        Color = @event.Color;
        Size = @event.Size;
        InventoryStock = InventoryStock.Create(0);

        if (hasReadFromDb)
        {
            IsCreated = true;
        }

        if (!IsCreated)
        {
            @event.SkuNumber = SkuNumber;
            _pendingDomainEvents.Add(@event);
            IsCreated = true;
        }
    }

    private string GetSkUNumber()
    {
        if (string.IsNullOrEmpty(Brand))
        {
            throw new InvalidBrandException("Brand cannot be empty");
        }
        
        if (string.IsNullOrEmpty(Model))
        {
            throw new InvalidModelException("Model cannot be empty");
        }
        
        if (string.IsNullOrEmpty(Color))
        {
            throw new InvalidColorException("Color cannot be empty");
        }
        
        return
            $"{ProductType.GetDescriptionAttributeValue()}-{Brand.ToUpper()}-{Model.ToUpper()}-{Size.GetDescriptionAttributeValue()}-{Color.ToUpper()}";
    }
}