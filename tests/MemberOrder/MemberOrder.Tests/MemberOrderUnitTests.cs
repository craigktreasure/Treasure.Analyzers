namespace Treasure.Analyzers.MemberOrder.Tests;

using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

using VerifyCS = TestVerifiers.CSharpAnalyzerVerifier<MemberOrderAnalyzer>;

[TestClass]
public sealed class MemberOrderUnitTests
{
    [TestMethod]
    public async Task EmptyContent_NoDiagnostics() =>
        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(string.Empty);

    [TestMethod]
    public void GetAccessibilityModifierOrder_NullMember_ThrowsArgumentNullException() =>
        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetAccessibilityModifierOrder(null!));

    [TestMethod]
    public void GetAccessibilityModifierOrder_UnexpectedModifiers_Returns99()
    {
        // Arrange
        SyntaxTokenList modifierSyntax = TokenList(Token(SyntaxKind.ConstKeyword));
        MemberDeclarationSyntax unexpectedMemberSyntax = ClassDeclaration("Foo").WithModifiers(modifierSyntax);

        // Act
        int result = MemberOrderAnalyzer.GetAccessibilityModifierOrder(unexpectedMemberSyntax);

        // Assert
        Assert.AreEqual(99, result);
    }

    [TestMethod]
    public void GetMemberCategoryOrder_NullMember_ThrowsArgumentNullException() =>
        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetMemberCategoryOrder(null!));

    [TestMethod]
    public void GetMemberCategoryOrder_UnexpectedMember_Returns99()
    {
        // Arrange
        MemberDeclarationSyntax unexpectedMemberSyntax = NamespaceDeclaration(IdentifierName("MyNamespace"));

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
        MemberDeclarationSyntax unexpectedMemberSyntax = NamespaceDeclaration(IdentifierName("MyNamespace"));

        // Act and assert
        Assert.ThrowsException<InvalidOperationException>(() => MemberOrderAnalyzer.GetMemberName(unexpectedMemberSyntax));
    }

    [TestMethod]
    public void GetSpecialKeywordOrder_NullMember_ThrowsArgumentNullException() =>
        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => MemberOrderAnalyzer.GetSpecialKeywordOrder(null!));

    [TestMethod]
    public void Initialize_NoErrors()
    {
        // Arrange
        MemberOrderAnalyzer analyzer = new();
        MockAnalysisContext context = new();

        // Act and assert
        analyzer.Initialize(context);

        // Assert
        context.RegisteredSyntaxKinds.Contains(SyntaxKind.ClassDeclaration);
        context.RegisteredSyntaxKinds.Contains(SyntaxKind.InterfaceDeclaration);
        context.RegisteredSyntaxKinds.Contains(SyntaxKind.StructDeclaration);
        context.RegisteredSyntaxKinds.Contains(SyntaxKind.RecordDeclaration);
        context.RegisteredSyntaxKinds.Contains(SyntaxKind.RecordStructDeclaration);
    }

    [TestMethod]
    public void Initialize_NullContext_ThrowsArgumentNullException()
    {
        // Arrange
        MemberOrderAnalyzer analyzer = new();

        // Act and assert
        Assert.ThrowsException<ArgumentNullException>(() => analyzer.Initialize(null!));
    }

    private sealed class MockAnalysisContext : AnalysisContext
    {
        public List<SyntaxKind> RegisteredSyntaxKinds { get; } = [];

        public override void ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags analysisMode) { }

        public override void EnableConcurrentExecution() { }

        public override void RegisterCodeBlockAction(Action<CodeBlockAnalysisContext> action) { }

        public override void RegisterCodeBlockStartAction<TLanguageKindEnum>(Action<CodeBlockStartAnalysisContext<TLanguageKindEnum>> action) { }

        public override void RegisterCompilationAction(Action<CompilationAnalysisContext> action) { }

        public override void RegisterCompilationStartAction(Action<CompilationStartAnalysisContext> action) { }

        public override void RegisterSemanticModelAction(Action<SemanticModelAnalysisContext> action) { }

        public override void RegisterSymbolAction(Action<SymbolAnalysisContext> action, ImmutableArray<SymbolKind> symbolKinds) { }

        public override void RegisterSyntaxNodeAction<TLanguageKindEnum>(Action<SyntaxNodeAnalysisContext> action, ImmutableArray<TLanguageKindEnum> syntaxKinds)
        {
            this.RegisteredSyntaxKinds.AddRange(syntaxKinds.Cast<SyntaxKind>());
        }

        public override void RegisterSyntaxTreeAction(Action<SyntaxTreeAnalysisContext> action) { }
    }
}
