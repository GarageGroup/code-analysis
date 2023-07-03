using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static bool IsSystemType(this ITypeSymbol typeSymbol, string typeName)
        =>
        InnerIsType(typeSymbol, SystemNamespace, typeName);
}