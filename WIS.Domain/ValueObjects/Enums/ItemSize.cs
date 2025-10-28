using System.ComponentModel;

namespace WIS.Domain.ValueObjects.Enums;

public enum ItemSize
{
    [Description("S")]
    Small,
    [Description("M")]
    Medium,
    [Description("L")]
    Large,
    [Description("XL")]
    ExtraLarge
}