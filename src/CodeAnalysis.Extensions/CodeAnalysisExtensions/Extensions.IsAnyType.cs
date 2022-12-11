using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static bool IsAnyType(this ITypeSymbol typeSymbol, string @namespace, params string[] types)
        =>
        InnerIsAnyType(typeSymbol, @namespace, types);

    private static bool InnerIsAnyType(ITypeSymbol? typeSymbol, string @namespace, params string[]? types)
    {
        if (typeSymbol is null || types is null)
        {
            return false;
        }

        if (string.Equals(typeSymbol.ContainingNamespace?.ToString(), @namespace, StringComparison.InvariantCulture) is false)
        {
            return false;
        }

        return types.Any(IsEqualToType);

        bool IsEqualToType(string type)
            =>
            string.Equals(typeSymbol.Name, type, StringComparison.InvariantCulture);
    }
}