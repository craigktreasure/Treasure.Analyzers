namespace Treasure.Analyzers.MemberOrder.CodeFixes.Tests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = TestVerifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderCodeFixProviderTests_Struct
{
    [TestMethod]
    public async Task Category_MembersTypeNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        struct MyStruct
        {
            private void MyPrivateMethodB() { }
            private MyStruct(int a) { }
            private int this[int a] { get => 0; set { } }
            private event MyPrivateDelegate MyPrivateEventField;
            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }
            private delegate void MyPrivateDelegate();
            private int MyPrivateProperty { get; set; }
            private int myPrivateField;
        }
        """;

        const string expectedFixedSourceText = """
        struct MyStruct
        {
            private int myPrivateField;
            private int MyPrivateProperty { get; set; }
            private delegate void MyPrivateDelegate();
            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }
            private event MyPrivateDelegate MyPrivateEventField;
            private int this[int a] { get => 0; set { } }
            private MyStruct(int a) { }
            private void MyPrivateMethodB() { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Category_MembersTypeNotInOrderWithWhiteSpace_ReorderedWithWhiteSpaceMaintained()
    {
        // Arrange
        const string sourceText = """
        struct MyStruct
        {
            private void MyPrivateMethodB() { }

            private MyStruct(int a) { }

            private int this[int a] { get => 0; set { } }

            private event MyPrivateDelegate MyPrivateEventField;

            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }

            private delegate void MyPrivateDelegate();

            private int MyPrivateProperty { get; set; }

            private int myPrivateField;
        }
        """;

        const string expectedFixedSourceText = """
        struct MyStruct
        {
            private int myPrivateField;

            private int MyPrivateProperty { get; set; }

            private delegate void MyPrivateDelegate();

            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }

            private event MyPrivateDelegate MyPrivateEventField;

            private int this[int a] { get => 0; set { } }

            private MyStruct(int a) { }

            private void MyPrivateMethodB() { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Category_SubTypesNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        public struct MyStruct
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
        public struct MyStruct
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
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Fields_AccesibilityNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        struct MyStruct
        {
            private int myPrivateField;
            public int myPublicField;
        }
        """;

        const string expectedFixedSourceText = """
        struct MyStruct
        {
            public int myPublicField;
            private int myPrivateField;
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Fields_AlphabeticalNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        struct MyStruct
        {
            public int myPublicFieldB;
            public int myPublicFieldA;
        }
        """;

        const string expectedFixedSourceText = """
        struct MyStruct
        {
            public int myPublicFieldA;
            public int myPublicFieldB;
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task Fields_SpecialKeywordsNotInOrder_Reordered()
    {
        // Arrange
        const string sourceText = """
        struct MyStruct
        {
            public int myPublicField;
            public readonly int myPublicReadonlyField = 1;
            public static int myPublicStaticField;
            public static readonly int myPublicStaticReadonlyField;
            public const int myPublicConstantField = 0;

            public MyStruct() { }
        }
        """;

        const string expectedFixedSourceText = """
        struct MyStruct
        {
            public const int myPublicConstantField = 0;
            public static readonly int myPublicStaticReadonlyField;
            public static int myPublicStaticField;
            public readonly int myPublicReadonlyField = 1;
            public int myPublicField;

            public MyStruct() { }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("MyStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }
}
