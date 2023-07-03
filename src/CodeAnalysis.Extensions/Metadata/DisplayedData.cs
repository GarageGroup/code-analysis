using System;
using System.Collections.Generic;

namespace GarageGroup;

public sealed record class DisplayedTypeData
{
    public DisplayedTypeData(IReadOnlyCollection<string> allNamespaces, string displayedTypeName)
    {
        AllNamespaces = allNamespaces ?? Array.Empty<string>();
        DisplayedTypeName = displayedTypeName ?? string.Empty;
    }

    public IReadOnlyCollection<string> AllNamespaces { get; }

    public string DisplayedTypeName { get; }
}