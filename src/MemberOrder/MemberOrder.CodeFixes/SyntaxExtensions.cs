namespace Treasure.Analyzers.MemberOrder.CodeFixes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal static class SyntaxExtensions
{
    public static IEnumerable<SyntaxTrivia> GetLeadingWhitespace(this SyntaxTriviaList triviaList)
    {
        foreach (SyntaxTrivia trivia in triviaList)
        {
            if (trivia.Kind() is SyntaxKind.WhitespaceTrivia or SyntaxKind.EndOfLineTrivia)
            {
                yield return trivia;
            }
            else
            {
                yield break;
            }
        }
    }

    public static MemberDeclarationSyntax WithLeadingWhitespaceFrom(this MemberDeclarationSyntax targetMember, MemberDeclarationSyntax whitespaceSourceMember)
    {
        IEnumerable<SyntaxTrivia> newLeadingWhitespace = whitespaceSourceMember.GetLeadingTrivia().GetLeadingWhitespace().ToArray();
        IEnumerable<SyntaxTrivia> leadingNonWhitespace = targetMember.GetLeadingTrivia().WithoutLeadingWhitespace().ToArray();
        return targetMember.WithLeadingTrivia(newLeadingWhitespace.Concat(leadingNonWhitespace));
    }

    public static IEnumerable<SyntaxTrivia> WithoutLeadingWhitespace(this SyntaxTriviaList triviaList)
    {
        bool takeRest = false;
        foreach (SyntaxTrivia trivia in triviaList)
        {
            if (!takeRest && (trivia.Kind() is SyntaxKind.WhitespaceTrivia or SyntaxKind.EndOfLineTrivia))
            {
                continue;
            }
            else
            {
                takeRest = true;
                yield return trivia;
            }
        }
    }

    public static MemberDeclarationSyntax WithTrailingTriviaFrom(this MemberDeclarationSyntax targetMember, MemberDeclarationSyntax sourceMember)
        => targetMember.WithTrailingTrivia(sourceMember.GetTrailingTrivia());
}
