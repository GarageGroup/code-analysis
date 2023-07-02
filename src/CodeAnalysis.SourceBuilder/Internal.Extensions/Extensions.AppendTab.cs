using System.Text;

namespace GarageGroup;

partial class SourceBuilderExtensions
{
    internal static StringBuilder AppendTab(this StringBuilder builder, int tabNumber)
    {
        if (tabNumber is not > 0)
        {
            return builder;
        }

        var tab = new string(' ', TabInterval * tabNumber);
        return builder.Append(tab);
    }
}