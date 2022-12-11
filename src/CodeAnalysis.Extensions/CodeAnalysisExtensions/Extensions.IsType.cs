using System;
using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static bool IsType(this ITypeSymbol typeSymbol, string @namespace, string typeName)
        =>
        InnerIsType(typeSymbol, @namespace, typeName);

    private static bool InnerIsType(ITypeSymbol? typeSymbol, string @namespace, string typeName)
        =>
        typeSymbol is not null &&
        string.Equals(typeSymbol.ContainingNamespace?.ToString(), @namespace, StringComparison.InvariantCulture) &&
        string.Equals(typeSymbol.Name, typeName, StringComparison.InvariantCulture);
}