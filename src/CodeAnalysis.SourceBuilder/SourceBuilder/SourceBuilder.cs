using System.Collections.Generic;
using System.Text;

namespace GGroupp;

public sealed partial class SourceBuilder
{
    private readonly List<string> usings;

    private readonly string @namespace;

    private readonly List<string> aliases;

    private readonly StringBuilder codeBuilder;

    private int tabNumber;

    public SourceBuilder(string? @namespace)
    {
        usings = new();
        this.@namespace = string.IsNullOrEmpty(@namespace) ? "GGroupp" : @namespace!;
        aliases = new();
        codeBuilder = new();
        tabNumber = 0;
    }
}