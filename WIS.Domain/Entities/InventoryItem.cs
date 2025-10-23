using WIS.Domain.Exceptions;
using WIS.Domain.Extensions;
using WIS.Domain.ValueObjects.Enums;

namespace WIS.Domain.Entities;

public class InventoryItem
{
    private string? _skuNumber;
    public string SkuNumber => _skuNumber ??= GetSkUNumber();
    public required ProductType ProductType { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required ItemSize Size { get; set; }
    public required string Color { get; set; }

    public InventoryStock InventoryStock { get; set; } = InventoryStock.Create(0);

    private InventoryItem()
    {
    }

    public static InventoryItem Create(
        ProductType productType,
        string brand,
        string model,
        string color,
        ItemSize size,
        InventoryStock? stock = null)
    {
        if (string.IsNullOrEmpty(brand))
        {
            throw new InvalidBrandException("Brand cannot be empty");
        }

        if (string.IsNullOrEmpty(model))
        {
            throw new InvalidBrandException("Model cannot be empty");
        }

        if (string.IsNullOrEmpty(color))
        {
            throw new InvalidBrandException("Color cannot be empty");
        }

        return new InventoryItem
        {
            ProductType = productType,
            Brand = brand,
            Model = model,
            Color = color,
            Size = size,
            InventoryStock = stock??InventoryStock.Create(0)
        };
    }

    public static InventoryItem Create(string productTypeStr, string brand, string model, string color, string sizeStr)
    {
        if (!Enum.TryParse<ProductType>(productTypeStr, out var productType))
        {
            //TODO: Rework to domain exceptions
            throw new InvalidOperationException(nameof(productTypeStr));
        }

        if (!Enum.TryParse<ItemSize>(sizeStr, out var size))
        {
            //TODO: Rework to domain exceptions
            throw new InvalidOperationException(nameof(sizeStr));
        }

        return Create(productType, brand, model, color, size);
    }

    private string GetSkUNumber()
    {
        return
            $"{ProductType.GetDescriptionAttributeValue()}-{Brand.ToUpper()}-{Model.ToUpper()}-{Size.GetDescriptionAttributeValue()}-{Color.ToUpper()}";
    }
}