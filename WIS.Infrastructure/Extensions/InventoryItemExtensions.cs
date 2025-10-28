using WIS.Application.DTO;
using WIS.Domain.Entities;
using WIS.Domain.Events;
using WIS.Infrastructure.Entities;

namespace WIS.Infrastructure.Extensions;

public static class InventoryItemExtensions
{
    public static InventoryItemModel ToDbModel(this InventoryItem inventoryItem)
    {
        ArgumentNullException.ThrowIfNull(inventoryItem);

        return new InventoryItemModel
        {
            Brand = inventoryItem.Brand,
            Model = inventoryItem.Model,
            Code = inventoryItem.SkuNumber,
            Color = inventoryItem.Color,
            ProductType = inventoryItem.ProductType,
            Size = inventoryItem.Size,
            InventoryStock = inventoryItem.InventoryStock.ToDbModel()
        };
    }

    public static InventoryItem ToDomain(this InventoryItemModel inventoryItemModel)
    {
        ArgumentNullException.ThrowIfNull(inventoryItemModel);

        return InventoryItem.Create(
            inventoryItemModel.ProductType,
            inventoryItemModel.Brand,
            inventoryItemModel.Model,
            inventoryItemModel.Color,
            inventoryItemModel.Size,
            inventoryItemModel.InventoryStock!.ToDomain());
    }

    public static InventoryStockModel ToDbModel(this InventoryStock inventoryStock)
    {
        ArgumentNullException.ThrowIfNull(inventoryStock);

        return new InventoryStockModel
        {
            Quantity = inventoryStock.Quantity
        };
    }
    
    public static InventoryStock ToDomain(this InventoryStockModel inventoryStock)
    {
        ArgumentNullException.ThrowIfNull(inventoryStock);
        return InventoryStock.Create(inventoryStock.Quantity);
    }
    
    public static InventoryStockModel ToUpdateDbModel(this InventoryStock inventoryStock, InventoryStockModel inventoryStockModel)
    {
        ArgumentNullException.ThrowIfNull(inventoryStock);
        
        inventoryStockModel.Quantity = inventoryStock.Quantity;
        return inventoryStockModel;
    }
    
    public static InventoryItemInfoDto ToDto(this InventoryItemModel inventoryItem)
    {
        return new InventoryItemInfoDto
        {
            Brand = inventoryItem.Brand,
            Model = inventoryItem.Model,
            Code = inventoryItem.Code,
            Color = inventoryItem.Color,
            ProductType = inventoryItem.ProductType,
            Size = inventoryItem.Size,
            Count = inventoryItem.InventoryStock!.Quantity
        };
    }
    
    public static InventoryAuditLogDataModel ToAggregatedData(this StockUpdatedEvent stockUpdatedEvent)
    {
        return new InventoryAuditLogDataModel
        {
            Brand = stockUpdatedEvent.Brand,
            Model = stockUpdatedEvent.Model,
            Code = stockUpdatedEvent.Code,
            Color = stockUpdatedEvent.Color,
            ProductType = stockUpdatedEvent.ProductType,
            Size = stockUpdatedEvent.Size,
            Quantity = stockUpdatedEvent.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
    
    public static StockUpdatedEvent ToAuditData(this InventoryAuditLogDataModel model)
    {
        return new InventoryAuditLogDataModel
        {
            Brand = model.Brand,
            Model = model.Model,
            Code = model.Code,
            Color = model.Color,
            ProductType = model.ProductType,
            Size = model.Size,
            Quantity = model.Quantity,
            UpdatedAt = DateTimeOffset.UtcNow
        };
    }
}