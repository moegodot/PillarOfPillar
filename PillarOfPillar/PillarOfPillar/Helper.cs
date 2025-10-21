using System;
using Microsoft.CodeAnalysis;

namespace PillarOfPillar;

/// <summary>
/// A set of tools that help SourceGenerator works.
/// </summary>
public static class Helper
{
    /// <summary>
    /// Print a debug information to console.
    /// </summary>
    public static void Debug(this SourceProductionContext context, string msg)
    {
        msg = msg.Replace("\n", " ;;; ");
        context.ReportDiagnostic(Diagnostic.Create(
            new DiagnosticDescriptor("DEBUG0", "SOURCE_GENERATOR_DEBUG_OUTPUT", "{0}", "",
                DiagnosticSeverity.Error, true),
            null,
            msg));
    }

    /// <summary>
    /// Get `[GeneratedCode("name","version")]` from generator type.
    /// </summary>
    /// <param name="generator">The generator type.</param>
    /// <returns>String that seems like `[GeneratedCode("name","version")]`</returns>
    public static string GetGeneratedCodeAttribute(Type generator)
    {
        var version  = generator.Assembly.GetName().Version?.ToString() ?? "0.0.1-unknown";
        return $"[System.CodeDom.Compiler.GeneratedCode(\"{generator.FullName}\",\"{version}\")]";
    }
    
    public static string GetHintNameOfType(ISymbol symbol)
    {
        return symbol.ToString().Replace("@", "[at]");
    }

    /// <summary>
    /// Return a generator-unique name that can be hintName of <see cref="SourceProductionContext.AddSource(string,string)"/>
    /// </summary>
    public static string GetHintNameOfGenerator(Type generator)
    {
        return generator.FullName!;
    }
}