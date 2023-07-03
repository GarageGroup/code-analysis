namespace GarageGroup;

public static partial class CodeAnalysisExtensions
{
    private const string SystemNamespace = "System";

    private static string WithCamelCase(this string source)
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        if (source.Length is 1)
        {
            return source.ToLowerInvariant();
        }

        return source[0].ToString().ToLowerInvariant() + source.Substring(1);
    }
}