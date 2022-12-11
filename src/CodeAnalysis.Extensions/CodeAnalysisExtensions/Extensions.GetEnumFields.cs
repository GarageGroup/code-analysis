using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static IEnumerable<IFieldSymbol> GetEnumFields(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is null)
        {
            return Enumerable.Empty<IFieldSymbol>();
        }

        return typeSymbol.GetMembers().OfType<IFieldSymbol>().Where(IsPublic).Where(NotEmptyName);

        static bool IsPublic(IFieldSymbol fieldSymbol)
            =>
            fieldSymbol.DeclaredAccessibility is Accessibility.Public;

        static bool NotEmptyName(IFieldSymbol fieldSymbol)
            =>
            string.IsNullOrEmpty(fieldSymbol.Name) is false;
    }
}