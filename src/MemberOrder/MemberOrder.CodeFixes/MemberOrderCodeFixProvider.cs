namespace Treasure.Analyzers.MemberOrder.CodeFixes;

using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;

using MemberOrder;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

/// <summary>
/// Represents a code fixer for the member order analyzer.
/// Implements the <see cref="CodeFixProvider" />
/// </summary>
/// <seealso cref="CodeFixProvider" />
[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(MemberOrderCodeFixProvider)), Shared]
public class MemberOrderCodeFixProvider : CodeFixProvider
{
    /// <summary>
    /// A list of diagnostic IDs that this provider can provide fixes for.
    /// </summary>
    public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(MemberOrderAnalyzer.DiagnosticId);

    /// <summary>
    /// Gets an optional <see cref="FixAllProvider" /> that can fix all/multiple occurrences of diagnostics fixed by this code fix provider.
    /// Return null if the provider doesn't support fix all/multiple occurrences.
    /// Otherwise, you can return any of the well known fix all providers from <see cref="WellKnownFixAllProviders" /> or implement your own fix all provider.
    /// </summary>
    /// <returns><see cref="FixAllProvider"/>.</returns>
    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    /// <summary>
    /// Register code fixes asynchronously.
    /// </summary>
    /// <param name="context">A <see cref="CodeFixContext" /> containing context information about the diagnostics to fix.
    /// The context must only contain diagnostics with a <see cref="Diagnostic.Id" /> included in the <see cref="CodeFixProvider.FixableDiagnosticIds" /> for the current provider.</param>
    /// <returns><see cref="Task" />.</returns>
    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        SyntaxNode? root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false)
            ?? throw new InvalidOperationException("The root value was null.");

        // TODO: Replace the following code with your own analysis, generating a CodeAction for each fix to suggest
        Diagnostic diagnostic = context.Diagnostics.First();
        TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;

        // Find the type declaration identified by the diagnostic.
        TypeDeclarationSyntax declaration = root.FindToken(diagnosticSpan.Start).Parent!.AncestorsAndSelf().OfType<TypeDeclarationSyntax>().First();

        // Register a code action that will invoke the fix.
        context.RegisterCodeFix(
            CodeAction.Create(
                title: CodeFixResources.CodeFixTitle,
                createChangedSolution: c => MakeUppercaseAsync(context.Document, declaration, c),
                equivalenceKey: nameof(CodeFixResources.CodeFixTitle)),
            diagnostic);
    }

    private static async Task<Solution> MakeUppercaseAsync(Document document, TypeDeclarationSyntax typeDecl, CancellationToken cancellationToken)
    {
        // Compute new uppercase name.
        SyntaxToken identifierToken = typeDecl.Identifier;
        string newName = identifierToken.Text.ToUpperInvariant();

        // Get the symbol representing the type to be renamed.
        SemanticModel? semanticModel = await document.GetSemanticModelAsync(cancellationToken);
        INamedTypeSymbol? typeSymbol = semanticModel?.GetDeclaredSymbol(typeDecl, cancellationToken)
            ?? throw new InvalidOperationException("Unable to get type symbol from semantic model.");

        // Produce a new solution that has all references to that type renamed, including the declaration.
        Solution originalSolution = document.Project.Solution;
        Microsoft.CodeAnalysis.Options.OptionSet optionSet = originalSolution.Workspace.Options;
#pragma warning disable CS0618 // Type or member is obsolete
        SymbolRenameOptions options = new(
            RenameOverloads: optionSet.GetOption(RenameOptions.RenameOverloads),
            RenameInStrings: optionSet.GetOption(RenameOptions.RenameInStrings),
            RenameInComments: optionSet.GetOption(RenameOptions.RenameInComments));
#pragma warning restore CS0618 // Type or member is obsolete
        Solution newSolution = await Renamer.RenameSymbolAsync(document.Project.Solution, typeSymbol, options, newName, cancellationToken).ConfigureAwait(false);

        // Return the new solution with the now-uppercase type name.
        return newSolution;
    }
}
