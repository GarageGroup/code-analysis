using System.IO;
using Microsoft.CodeAnalysis;

namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static bool IsStreamType(this ITypeSymbol typeSymbol)
        =>
        InnerIsType(typeSymbol, "System.IO", nameof(Stream));
}