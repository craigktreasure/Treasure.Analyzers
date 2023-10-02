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

        foreach (Diagnostic diagnostic in context.Diagnostics)
        {
            TextSpan diagnosticSpan = diagnostic.Location.SourceSpan;
            ClassDeclarationSyntax classDeclaration = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First()
                ?? throw new InvalidOperationException("The classDeclaration was null.");

            context.RegisterCodeFix(
                CodeAction.Create(
                    title: CodeFixResources.Treasure0001CodeFixTitle,
                    createChangedDocument: c => ReorderMembersAsync(context.Document, classDeclaration, c),
                    equivalenceKey: nameof(CodeFixResources.Treasure0001CodeFixTitle)),
                diagnostic);
        }
    }

    private static async Task<Document> ReorderMembersAsync(Document document, ClassDeclarationSyntax classDeclaration, CancellationToken cancellationToken)
    {
        SyntaxList<MemberDeclarationSyntax> members = classDeclaration.Members;
        List<MemberDeclarationSyntax> sortedMembers = members
            .OrderBy(MemberOrderAnalyzer.GetMemberCategoryOrder)
            .ThenBy(MemberOrderAnalyzer.GetAccessibilityModifierOrder)
            .ThenBy(MemberOrderAnalyzer.GetSpecialKeywordOrder)
            .ThenBy(MemberOrderAnalyzer.GetMemberName)
            .ToList();
        TryKeepWhiteSpace(ref members, sortedMembers);
        ClassDeclarationSyntax newClassDeclaration = classDeclaration.WithMembers(SyntaxFactory.List(sortedMembers));

        SyntaxNode? root = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false)
            ?? throw new InvalidOperationException("The root value was null.");
        SyntaxNode newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);

        return document.WithSyntaxRoot(newRoot);
    }

    /// <summary>
    /// Tries to keep the white space. This isn't expected to be perfect.
    /// </summary>
    /// <param name="members">The members.</param>
    /// <param name="sortedMembers">The sorted members.</param>
    private static void TryKeepWhiteSpace(ref SyntaxList<MemberDeclarationSyntax> members, List<MemberDeclarationSyntax> sortedMembers)
    {
        MemberDeclarationSyntax firstMember = members[0];
        MemberDeclarationSyntax firstSortedMember = sortedMembers[0];
        if (firstMember != firstSortedMember)
        {
            sortedMembers[0] = firstSortedMember
                .WithLeadingTrivia(firstMember.GetLeadingTrivia())
                .WithTrailingTrivia(firstMember.GetTrailingTrivia());
        }

        int lastIndex = sortedMembers.Count - 1;
        MemberDeclarationSyntax lastMember = members[lastIndex];
        MemberDeclarationSyntax lastSortedMember = sortedMembers[lastIndex];

        if (lastMember != lastSortedMember)
        {
            sortedMembers[lastIndex] = lastSortedMember
                .WithTrailingTrivia(lastMember.GetTrailingTrivia())
                .WithLeadingTrivia(lastMember.GetLeadingTrivia());
        }
    }
}
