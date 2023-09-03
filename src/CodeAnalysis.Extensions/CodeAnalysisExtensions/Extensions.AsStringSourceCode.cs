namespace GarageGroup;

partial class CodeAnalysisExtensions
{
    public static string AsStringSourceCodeOr(this string? source, string defaultSourceCode = "\"\"")
        =>
        string.IsNullOrEmpty(source) ? defaultSourceCode : InnerWrapStringSourceCode(source!);

    public static string AsStringSourceCodeOrStringEmpty(this string? source)
        =>
        string.IsNullOrEmpty(source) ? "string.Empty" : InnerWrapStringSourceCode(source!);

    private static string InnerWrapStringSourceCode(string source)
    {
        var encodedString = source.Replace("\"", "\\\"");
        return $"\"{encodedString}\"";
    }
}