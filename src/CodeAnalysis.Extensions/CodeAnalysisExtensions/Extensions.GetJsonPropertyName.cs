using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static string GetJsonPropertyName(this IPropertySymbol propertySymbol)
    {
        _ = propertySymbol ?? throw new ArgumentNullException(nameof(propertySymbol));

        var jsonPropertyNameAttribute = propertySymbol.GetAttributes().FirstOrDefault(IsJsonPropertyNameAttribute);
        if (jsonPropertyNameAttribute is not null)
        {
            var name = InnerGetAttributeValue(jsonPropertyNameAttribute, 0, null)?.ToString();
            if (string.IsNullOrEmpty(name) is false)
            {
                return name!;
            }
        }

        return propertySymbol.Name.WithCamelCase();

        static bool IsJsonPropertyNameAttribute(AttributeData attributeData)
            =>
            InnerIsType(attributeData.AttributeClass, "System.Text.Json.Serialization", "JsonPropertyNameAttribute") is true;
    }
}