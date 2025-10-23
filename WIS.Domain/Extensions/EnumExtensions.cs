using System.ComponentModel;
using System.Reflection;

namespace WIS.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDescriptionAttributeValue(this Enum productType)
    {
        var type = productType.GetType();
        var member = type.GetMember(productType.ToString()).FirstOrDefault();

        var productTypeCode = member
            .GetCustomAttribute<DescriptionAttribute>(false)?.Description;
        
        return productTypeCode ?? "";
    }
}