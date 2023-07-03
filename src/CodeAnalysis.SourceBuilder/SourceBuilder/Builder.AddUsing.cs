using System;
using System.Linq;

namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AddUsing(string? @using, params string?[] other)
    {
        _ = InnerAddUsing(@using);

        if (other?.Length is not > 0)
        {
            return this;
        }

        foreach (var otherUsing in other)
        {
            _ = InnerAddUsing(otherUsing);
        }

        return this;
    }

    private SourceBuilder InnerAddUsing(string? @using)
    {
        if (string.IsNullOrWhiteSpace(@using))
        {
            return this;
        }

        if (string.Equals(@using, @namespace, StringComparison.InvariantCulture))
        {
            return this;
        }

        if (@namespace.StartsWith(@using + ".", StringComparison.InvariantCulture))
        {
            return this;
        }

        if (usings.Any(UsingEquals))
        {
            return this;
        }

        usings.Add(@using!);
        return this;

        bool UsingEquals(string usingValue)
            =>
            string.Equals(usingValue, @using, StringComparison.InvariantCulture);
    }
}