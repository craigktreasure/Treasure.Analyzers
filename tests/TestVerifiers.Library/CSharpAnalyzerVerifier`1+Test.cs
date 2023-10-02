namespace Treasure.Analyzers.MemberOrder.Test.Verifiers;

using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing.Verifiers;

using Treasure.Analyzers.TestVerifiers;

public static partial class CSharpAnalyzerVerifier<TAnalyzer>
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    internal sealed class Test : CSharpAnalyzerTest<TAnalyzer, MSTestVerifier>
    {
        public Test()
        {
            this.SolutionTransforms.Add((solution, projectId) =>
            {
                Microsoft.CodeAnalysis.CompilationOptions? compilationOptions = solution.GetProject(projectId)?.CompilationOptions
                ?? throw new InvalidOperationException("Unable to get project compilation options.");
                compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
                    compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
                solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

                return solution;
            });
        }
    }
}
