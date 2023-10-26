using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static DisplayedTypeData GetDisplayedData(this ITypeSymbol typeSymbol, bool withNullableSymbol = false)
        =>
        InnerGetDisplayedData(
            typeSymbol: typeSymbol ?? throw new ArgumentNullException(nameof(typeSymbol)),
            withNullableSymbol: withNullableSymbol);

    private static DisplayedTypeData InnerGetDisplayedData(ITypeSymbol typeSymbol, bool withNullableSymbol)
    {
        var symbol = typeSymbol;
        var nullableSymbol = string.Empty;

        if (typeSymbol.GetNullableBaseType() is ITypeSymbol baseTypeSymbol)
        {
            symbol = baseTypeSymbol;
            if (withNullableSymbol)
            {
                nullableSymbol = "?";
            }
        }

        if (symbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            var elementTypeData = InnerGetChildrenDisplayedData(arrayTypeSymbol.ElementType);

            var elementTypeNameParts = new List<string>
            {
                elementTypeData.DisplayedTypeName
            };

            elementTypeNameParts.AddRange(Enumerable.Repeat("[]", arrayTypeSymbol.Rank));

            return new DisplayedTypeData(
                allNamespaces: elementTypeData.AllNamespaces,
                displayedTypeName: string.Concat(elementTypeNameParts) + nullableSymbol);
        }

        if (symbol is not INamedTypeSymbol namedTypeSymbol || namedTypeSymbol.TypeArguments.Length is not > 0)
        {
            var typeNamespace = symbol.ContainingNamespace?.ToString();
            var typeNamespaces = new List<string>(1);

            if (string.IsNullOrEmpty(typeNamespace) is false)
            {
                typeNamespaces.Add(typeNamespace ?? string.Empty);
            }

            return new(
                allNamespaces: typeNamespaces,
                displayedTypeName: symbol.Name + nullableSymbol);
        }

        var argumentTypes = namedTypeSymbol.TypeArguments.Select(InnerGetChildrenDisplayedData);

        return new(
            allNamespaces: new List<string>(argumentTypes.SelectMany(GetNamespaces))
            {
                symbol.ContainingNamespace.ToString()
            },
            displayedTypeName: $"{symbol.Name}<{string.Join(", ", argumentTypes.Select(GetName))}>{nullableSymbol}");

        static DisplayedTypeData InnerGetChildrenDisplayedData(ITypeSymbol typeSymbol)
            =>
            InnerGetDisplayedData(typeSymbol, true);

        static IEnumerable<string> GetNamespaces(DisplayedTypeData typeData)
            =>
            typeData.AllNamespaces;

        static string GetName(DisplayedTypeData typeData)
            =>
            typeData.DisplayedTypeName;
    }

    private static ITypeSymbol? GetNullableBaseType(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return null;
        }

        if (namedTypeSymbol.IsValueType is false)
        {
            return namedTypeSymbol.NullableAnnotation is NullableAnnotation.Annotated ? typeSymbol : null;
        }

        if (namedTypeSymbol.TypeArguments.Length is 1 && namedTypeSymbol.IsSystemType("Nullable"))
        {
            return namedTypeSymbol.TypeArguments[0];
        }

        return null;
    }
}