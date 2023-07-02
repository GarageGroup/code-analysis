namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder BeginLambda()
    {
        tabNumber++;
        _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append("=>");

        return this;
    }
}