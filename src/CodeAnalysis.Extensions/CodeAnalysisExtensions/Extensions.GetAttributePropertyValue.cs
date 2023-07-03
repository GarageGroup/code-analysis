using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static object? GetAttributePropertyValue(this AttributeData attributeData, string propertyName)
    {
        if (attributeData is null)
        {
            return null;
        }

        return InnerGetAttributePropertyValue(attributeData, propertyName);
    }

    private static object? InnerGetAttributePropertyValue(this AttributeData attributeData, string propertyName)
    {
        return attributeData.NamedArguments.FirstOrDefault(IsNameMatched).Value.Value;

        bool IsNameMatched(KeyValuePair<string, TypedConstant> pair)
            =>
            string.Equals(pair.Key, propertyName, StringComparison.InvariantCulture);
    }
}