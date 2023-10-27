namespace Treasure.Analyzers.MemberOrder.Tests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Test.Verifiers.CSharpAnalyzerVerifier<MemberOrderAnalyzer>;

public sealed class MemberOrderUnitTests
{
    [TestMethod]
    public async Task Analyzer_EmptyContent_NoDiagnostics() =>

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(string.Empty);

    [TestMethod]
    public void GetAccessibilityModifierOrder_NullMember_ThrowsArgumentNullException() =>

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetAccessibilityModifierOrder(null!));

    [TestMethod]
    public void GetMemberCategoryOrder_NullMember_ThrowsArgumentNullException() =>

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetMemberCategoryOrder(null!));

    [TestMethod]
    public void GetMemberCategoryOrder_UnexpectedMember_Returns99()
    {
        // Arrange
        MemberDeclarationSyntax unexpectedMemberSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("MyNamespace"));

        // Act
        int result = MemberOrderAnalyzer.GetMemberCategoryOrder(unexpectedMemberSyntax);

        // Assert
        Assert.AreEqual(99, result);
    }

    [TestMethod]
    public void GetMemberName_NullMember_ThrowsArgumentNullException() =>

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetMemberName(null!));

    [TestMethod]
    public void GetMemberName_UnexpectedMember_ThrowsInvalidOperationException()
    {
        // Arrange
        MemberDeclarationSyntax unexpectedMemberSyntax = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("MyNamespace"));

        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => MemberOrderAnalyzer.GetMemberName(unexpectedMemberSyntax));
    }

    [TestMethod]
    public void GetSpecialKeywordOrder_NullMember_ThrowsArgumentNullException() =>

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetSpecialKeywordOrder(null!));

    [TestMethod]
    public void Initialize_NullContext_ThrowsArgumentNullException()
    {
        // Arrange
        MemberOrderAnalyzer analyzer = new();

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => analyzer.Initialize(null!));
    }
}
