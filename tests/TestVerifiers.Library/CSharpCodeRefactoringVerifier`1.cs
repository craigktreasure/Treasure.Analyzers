namespace MemberOrder.Test;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CodeRefactorings;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

using Treasure.Analyzers.TestVerifiers;

[SuppressMessage("Design", "CA1000:Do not declare static members on generic types")]
public static class CSharpCodeRefactoringVerifier<TCodeRefactoring>
    where TCodeRefactoring : CodeRefactoringProvider, new()
{
    /// <inheritdoc cref="CodeRefactoringVerifier{TCodeRefactoring, TTest, TVerifier}.VerifyRefactoringAsync(string, string)"/>
    public static async Task VerifyRefactoringAsync(string source, string fixedSource)
    {
        await VerifyRefactoringAsync(source, DiagnosticResult.EmptyDiagnosticResults, fixedSource);
    }

    /// <inheritdoc cref="CodeRefactoringVerifier{TCodeRefactoring, TTest, TVerifier}.VerifyRefactoringAsync(string, DiagnosticResult, string)"/>
    public static async Task VerifyRefactoringAsync(string source, DiagnosticResult expected, string fixedSource)
    {
        await VerifyRefactoringAsync(source, [expected], fixedSource);
    }

    /// <inheritdoc cref="CodeRefactoringVerifier{TCodeRefactoring, TTest, TVerifier}.VerifyRefactoringAsync(string, DiagnosticResult[], string)"/>
    public static async Task VerifyRefactoringAsync(string source, DiagnosticResult[] expected, string fixedSource)
    {
        Test test = new()
        {
            TestCode = source,
            FixedCode = fixedSource,
        };

        test.ExpectedDiagnostics.AddRange(expected);
        await test.RunAsync(CancellationToken.None);
    }

    internal sealed class Test : CSharpCodeRefactoringTest<TCodeRefactoring, DefaultVerifier>
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
