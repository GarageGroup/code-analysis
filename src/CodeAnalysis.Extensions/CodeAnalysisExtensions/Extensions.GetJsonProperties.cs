using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static IReadOnlyCollection<IPropertySymbol> GetJsonProperties(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            return Array.Empty<IPropertySymbol>();
        }

        return typeSymbol.GetMembers().OfType<IPropertySymbol>().Where(IsPublic).Where(IsNotIgnored).ToArray();

        static bool IsPublic(IPropertySymbol propertySymbol)
            =>
            propertySymbol.DeclaredAccessibility is Accessibility.Public;

        static bool IsNotIgnored(IPropertySymbol propertySymbol)
            =>
            propertySymbol.GetAttributes().Any(IsJsonIgnoreAttribute) is false;

        static bool IsJsonIgnoreAttribute(AttributeData attributeData)
        {
            if (InnerIsType(attributeData?.AttributeClass, "System.Text.Json.Serialization", "JsonIgnoreAttribute") is not true)
            {
                return false;
            }

            return attributeData?.InnerGetAttributePropertyValue("Condition") is null or 1;
        }
    }
}