namespace MemberOrder.Test;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

using Treasure.Analyzers.TestVerifiers;

public static partial class CSharpCodeRefactoringVerifier<TCodeRefactoring>
    where TCodeRefactoring : CodeRefactoringProvider, new()
{
    internal sealed class Test : CSharpCodeRefactoringTest<TCodeRefactoring, MSTestVerifier>
    {
        public Test()
        {
            this.SolutionTransforms.Add((solution, projectId) =>
            {
                Microsoft.CodeAnalysis.CompilationOptions? compilationOptions = solution.GetProject(projectId)?.CompilationOptions
                    ?? throw new InvalidOperationException("Unable to get project's compilation options.");
                compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
                    compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));
                solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

                return solution;
            });
        }
    }
}
