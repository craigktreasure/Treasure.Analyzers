namespace Treasure.Analyzers.MemberOrder.CodeFixes.Tests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Test.Verifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderCodeFixProviderTests_Interface
{
    [TestMethod]
    public async Task Category_MembersTypeNotInOrderWithWhiteSpace_ReorderedWithWhiteSpaceMaintained()
    {
        // Arrange
        const string sourceText = """
        interface MyInterface
        {
            void MyMethodB() { }

            int this[int a] { get => 0; set { } }

            event MyDelegate MyEventField;

            event MyDelegate MyEvent { add { } remove { } }

            delegate void MyDelegate();

            int MyProperty { get; set; }
        }
        """;

        const string expectedFixedSourceText = """
        interface MyInterface
        {
            int MyProperty { get; set; }

            delegate void MyDelegate();

            event MyDelegate MyEvent { add { } remove { } }

            event MyDelegate MyEventField;

            int this[int a] { get => 0; set { } }

            void MyMethodB() { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Category_MemberTypeNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        interface MyInterface
        {
            void MyMethodB() { }
            int this[int a] { get => 0; set { } }
            event MyDelegate MyEventField;
            event MyDelegate MyEvent { add { } remove { } }
            delegate void MyDelegate();
            int MyProperty { get; set; }
        }
        """;

        const string expectedFixedSourceText = """
        interface MyInterface
        {
            int MyProperty { get; set; }
            delegate void MyDelegate();
            event MyDelegate MyEvent { add { } remove { } }
            event MyDelegate MyEventField;
            int this[int a] { get => 0; set { } }
            void MyMethodB() { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Category_SubTypesNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        public interface MyInterface
        {
            // Classes
            public class MySubClass { }

            // Records
            public record MySubRecord { }

            // Record structs
            public record struct MySubRecordStruct { }

            // Structs
            public struct MySubStruct { }

            // Interfaces
            public interface IMySubInterface { }

            // Enums
            public enum MySubEnum { EnumValue, }

            // Methods
            public void MyPublicMethod() { }
        }
        """;

        const string expectedFixedSourceText = """
        public interface MyInterface
        {
            // Methods
            public void MyPublicMethod() { }

            // Enums
            public enum MySubEnum { EnumValue, }

            // Interfaces
            public interface IMySubInterface { }

            // Structs
            public struct MySubStruct { }

            // Record structs
            public record struct MySubRecordStruct { }

            // Records
            public record MySubRecord { }

            // Classes
            public class MySubClass { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Methods_AccesibilityNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        interface MyInterface
        {
            private int MyPrivateMethod() => 1;
            public int MyPublicMethod() => 1;
        }
        """;

        const string expectedFixedSourceText = """
        interface MyInterface
        {
            public int MyPublicMethod() => 1;
            private int MyPrivateMethod() => 1;
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Properties_AlphabeticalNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        interface MyInterface
        {
            int MyPropertyB { get; set; }
            int MyPropertyA { get; set; }
        }
        """;

        const string expectedFixedSourceText = """
        interface MyInterface
        {
            int MyPropertyA { get; set; }
            int MyPropertyB { get; set; }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }
}
