using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static object? GetAttributeValue(this AttributeData attributeData, int constructorOrder, string? propertyName = null)
    {
        if (attributeData is null)
        {
            return null;
        }

        return InnerGetAttributeValue(attributeData, constructorOrder, propertyName);
    }

    private static object? InnerGetAttributeValue(AttributeData attributeData, int constructorOrder, string? propertyName)
    {
        if (attributeData.ConstructorArguments.Length > constructorOrder)
        {
            return attributeData.ConstructorArguments[constructorOrder].Value;
        }

        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        return InnerGetAttributePropertyValue(attributeData, propertyName!);
    }
}