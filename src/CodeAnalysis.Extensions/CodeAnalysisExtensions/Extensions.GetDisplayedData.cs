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
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol || namedTypeSymbol.TypeArguments.Length is not > 0)
        {
            return new(
                allNamespaces: new[]
                {
                    typeSymbol.ContainingNamespace.ToString()
                },
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