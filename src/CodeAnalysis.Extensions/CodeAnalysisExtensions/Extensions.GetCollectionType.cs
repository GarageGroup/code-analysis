using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace GGroupp;

partial class CodeAnalysisExtensions
{
    public static ITypeSymbol? GetCollectionTypeOrDefault(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is IArrayTypeSymbol arrayTypeSymbol)
        {
            return arrayTypeSymbol.ElementType;
        }

        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return null;
        }

        var enumerableInterface = namedTypeSymbol.AllInterfaces.FirstOrDefault(IsGenericEnumerable);
        if (enumerableInterface is not null)
        {
            return enumerableInterface.TypeArguments[0];
        }

        return typeSymbol.GetMembers().OfType<IMethodSymbol>().Where(IsEnumeratorMethod).Select(GetEnumeratorType).FirstOrDefault(NotNull);

        static bool IsGenericEnumerable(INamedTypeSymbol symbol)
            =>
            InnerIsType(symbol, "System.Collections.Generic", "IEnumerable") && symbol.TypeArguments.Length is 1;

        static bool IsEnumeratorMethod(IMethodSymbol methodSymbol)
            =>
            methodSymbol.IsGenericMethod is false &&
            methodSymbol.Parameters.Length is 0 &&
            string.Equals(methodSymbol.Name, "GetEnumerator");

        static ITypeSymbol? GetEnumeratorType(IMethodSymbol methodSymbol)
            =>
            methodSymbol.ReturnType?.GetEnumeratorTypeOrDefault();

        static bool NotNull(ITypeSymbol? typeSymbol)
            =>
            typeSymbol is not null;
    }

    private static ITypeSymbol? GetEnumeratorTypeOrDefault(this ITypeSymbol typeSymbol)
    {
        if (typeSymbol is not INamedTypeSymbol namedTypeSymbol)
        {
            return null;
        }

        var enumeratorInterface = namedTypeSymbol.AllInterfaces.FirstOrDefault(IsGenericEnumerator);
        if (enumeratorInterface is not null)
        {
            return enumeratorInterface.TypeArguments[0];
        }

        if (namedTypeSymbol.GetMembers().OfType<IMethodSymbol>().Any(IsMoveNextMethod) is false)
        {
            return null;
        }

        return namedTypeSymbol.GetMembers().OfType<IPropertySymbol>().FirstOrDefault(IsCurrentProperty)?.Type;

        static bool IsGenericEnumerator(INamedTypeSymbol symbol)
            =>
            InnerIsType(symbol, "System.Collections.Generic", "IEnumerator") && symbol.TypeArguments.Length is 1;

        static bool IsMoveNextMethod(IMethodSymbol methodSymbol)
            =>
            methodSymbol.IsGenericMethod is false &&
            methodSymbol.Parameters.Length is 0 &&
            methodSymbol.ReturnType.IsSystemType("Boolean") &&
            string.Equals(methodSymbol.Name, "MoveNext");

        static bool IsCurrentProperty(IPropertySymbol propertySymbol)
            =>
            string.Equals(propertySymbol.Name, "Current", StringComparison.InvariantCulture);
    }
}