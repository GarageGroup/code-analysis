namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AppendCodeLine(string codeLine, params string[] codeLines)
    {
        if (codeBuilder.Length > 0)
        {
            _ = codeBuilder.AppendLine();
        }

        _ = codeBuilder.AppendTab(tabNumber).Append(codeLine);

        if (codeLines?.Length is not > 0)
        {
            return this;
        }

        foreach (var line in codeLines)
        {
            _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append(line);
        }

        return this;
    }
}