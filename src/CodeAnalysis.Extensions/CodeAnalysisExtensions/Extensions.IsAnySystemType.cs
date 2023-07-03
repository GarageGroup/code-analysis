using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static bool IsAnySystemType(this ITypeSymbol typeSymbol, params string[] types)
        =>
        InnerIsAnyType(typeSymbol, SystemNamespace, types);
}