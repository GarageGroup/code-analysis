namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder BeginCodeBlock()
    {
        _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append('{');

        tabNumber++;
        return this;
    }
}