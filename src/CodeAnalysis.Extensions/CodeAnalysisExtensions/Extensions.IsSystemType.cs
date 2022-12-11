using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static bool IsSystemType(this ITypeSymbol typeSymbol, string typeName)
        =>
        InnerIsType(typeSymbol, SystemNamespace, typeName);
}