using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static DisplayedTypeData GetDisplayedData(this ITypeSymbol typeSymbol)
        =>
        InnerGetDisplayedData(
            typeSymbol ?? throw new ArgumentNullException(nameof(typeSymbol)));

    private static DisplayedTypeData InnerGetDisplayedData(ITypeSymbol typeSymbol)
    {
        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            var elementTypeData = InnerGetDisplayedData(arrayTypeSymbol.ElementType);

            var elementTypeNameParts = new List<string>
            {
                elementTypeData.DisplayedTypeName
            };

            elementTypeNameParts.AddRange(Enumerable.Repeat("[]", arrayTypeSymbol.Rank));

            return new DisplayedTypeData(
                allNamespaces: elementTypeData.AllNamespaces,
                displayedTypeName: string.Concat(elementTypeNameParts));
        }

        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol || namedTypeSymbol.TypeArguments.Length is not > 0)
        {
            var typeNamespace = typeSymbol.ContainingNamespace?.ToString();
            var typeNamespaces = new List<string>(1);

            if (string.IsNullOrEmpty(typeNamespace) is false)
            {
                typeNamespaces.Add(typeNamespace ?? string.Empty);
            }

            return new(
                allNamespaces: typeNamespaces,
                displayedTypeName: typeSymbol.Name);
        }

        var argumentTypes = namedTypeSymbol.TypeArguments.Select(InnerGetDisplayedData);

        return new(
            allNamespaces: new List<string>(argumentTypes.SelectMany(GetNamespaces))
            {
                typeSymbol.ContainingNamespace.ToString()
            },
            displayedTypeName: $"{typeSymbol.Name}<{string.Join(",", argumentTypes.Select(GetName))}>");

        static IEnumerable<string> GetNamespaces(DisplayedTypeData typeData)
            =>
            typeData.AllNamespaces;

        static string GetName(DisplayedTypeData typeData)
            =>
            typeData.DisplayedTypeName;
    }
}