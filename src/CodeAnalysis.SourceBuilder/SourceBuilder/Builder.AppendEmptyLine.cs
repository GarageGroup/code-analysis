namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AppendEmptyLine()
    {
        _ = codeBuilder.AppendLine();
        return this;
    }
}