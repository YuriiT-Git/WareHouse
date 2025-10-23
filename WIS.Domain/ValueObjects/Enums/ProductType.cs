using System.ComponentModel;

namespace WIS.Domain.ValueObjects.Enums;

public enum ProductType
{
    [Description("TSH")]
    TShirt,
    [Description("JNS")]
    Jeans,
    [Description("JCKT")]
    Jacket
}