namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AppendDirective(string preprocessorDirective)
    {
        if (string.IsNullOrEmpty(preprocessorDirective))
        {
            return this;
        }

        if (codeBuilder.Length > 0)
        {
            _ = codeBuilder.AppendLine();
        }

        _ = codeBuilder.Append(preprocessorDirective);
        return this;
    }
}