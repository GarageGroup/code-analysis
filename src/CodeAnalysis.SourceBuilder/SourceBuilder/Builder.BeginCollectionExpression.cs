namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder BeginCollectionExpression()
    {
        _ = codeBuilder.AppendLine().AppendTab(tabNumber).Append('[');

        tabNumber++;
        return this;
    }
}