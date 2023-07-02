using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static INamedTypeSymbol? GetEnumUnderlyingTypeOrDefault(this ITypeSymbol typeSymbol)
        =>
        typeSymbol is INamedTypeSymbol namedTypeSymbol ? namedTypeSymbol.EnumUnderlyingType : null;
}