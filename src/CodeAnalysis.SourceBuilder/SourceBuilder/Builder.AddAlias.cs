using System;
using System.Linq;

namespace GarageGroup;

partial class SourceBuilder
{
    public SourceBuilder AddAlias(string alias)
    {
        if (string.IsNullOrWhiteSpace(alias))
        {
            return this;
        }

        if (aliases.Any(AliasEquals))
        {
            return this;
        }

        aliases.Add(alias);
        return this;

        bool AliasEquals(string aliasValue)
            =>
            string.Equals(aliasValue, alias, StringComparison.InvariantCulture);
    }
}