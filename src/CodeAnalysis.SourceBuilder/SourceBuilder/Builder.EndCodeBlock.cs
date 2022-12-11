namespace GGroupp;

partial class SourceBuilder
{
    public SourceBuilder EndCodeBlock(string? afterEndSymbol = default)
    {
        tabNumber--;
        _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append('}');

        if (string.IsNullOrEmpty(afterEndSymbol) is false)
        {
            _ = codeBuilder.Append(afterEndSymbol);
        }

        return this;
    }

    public SourceBuilder EndCodeBlock(char afterEndSymbol)
    {
        tabNumber--;
        _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append('}').Append(afterEndSymbol);

        return this;
    }
}