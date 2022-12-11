using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static bool IsAnySystemType(this ITypeSymbol typeSymbol, params string[] types)
        =>
        InnerIsAnyType(typeSymbol, SystemNamespace, types);
}