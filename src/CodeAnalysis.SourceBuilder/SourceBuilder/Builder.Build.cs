using System;
using System.Linq;
using System.Text;

namespace GGroupp;

partial class SourceBuilder
{
    public string Build()
    {
        var builder = new StringBuilder("// Auto-generated code");
        builder = builder.AppendLine().Append("#nullable enable");

        if (usings.Any())
        {
            builder = builder.AppendLine();
        }

        foreach (var @using in usings.OrderBy(GetNamespaceOrder))
        {
            builder = builder.AppendLine().Append("using").Append(' ').Append(@using).Append(';');
        }

        builder = builder.AppendLine().AppendLine().Append("namespace").Append(' ').Append(@namespace).Append(';');

        if (aliases.Any())
        {
            builder = builder.AppendLine();
        }

        foreach (var alias in aliases)
        {
            builder = builder.AppendLine().Append("using").Append(' ').Append(alias).Append(';');
        }

        if (codeBuilder.Length is not > 0)
        {
            return builder.ToString();
        }

        return builder.AppendLine().AppendLine().Append(codeBuilder).ToString();
    }

    private static string GetNamespaceOrder(string ns)
        =>
        ns.StartsWith("System", StringComparison.InvariantCulture) ? "_" + ns : ns;
}