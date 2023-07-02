using System.Collections.Generic;

namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AddUsings(IReadOnlyCollection<string?>? usings)
    {
        if (usings?.Count is not > 0)
        {
            return this;
        }

        foreach (var @using in usings)
        {
            _ = InnerAddUsing(@using);
        }

        return this;
    }
}