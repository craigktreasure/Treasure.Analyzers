namespace Treasure.Analyzers.MemberOrder.CodeFixes.Tests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = TestVerifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderCodeFixProviderTests_NestedTypes
{
    [TestMethod]
    public async Task NestedClass_InnerClassMembersOutOfOrder_InnerClassReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public class InnerClass
            {
                private void InnerMethod() { }
                private int innerField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 5, 5)
            .WithArguments("InnerClass");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task NestedClass_OuterClassMembersOutOfOrder_OuterClassReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }
            public int outerField;

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public int outerField;
            public void OuterMethod() { }

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 1, 1)
            .WithArguments("OuterClass");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task NestedClass_BothOuterAndInnerMembersOutOfOrder_BothReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }
            public int outerField;

            public class InnerClass
            {
                private void InnerMethod() { }
                private int innerField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public int outerField;
            public void OuterMethod() { }

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults = [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("OuterClass"),
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 6, 5)
                .WithArguments("InnerClass")
        ];

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task NestedStruct_InnerStructMembersOutOfOrder_InnerStructReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public struct InnerStruct
            {
                private void InnerMethod() { }
                private int innerField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public struct InnerStruct
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 5, 5)
            .WithArguments("InnerStruct");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task NestedRecord_InnerRecordMembersOutOfOrder_InnerRecordReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public record InnerRecord
            {
                private void InnerMethod() { }
                private int innerField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public record InnerRecord
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 5, 5)
            .WithArguments("InnerRecord");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task NestedInterface_InnerInterfaceMembersOutOfOrder_InnerInterfaceReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public interface IInnerInterface
            {
                void InnerMethodB();
                int InnerProperty { get; set; }
                void InnerMethodA();
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public interface IInnerInterface
            {
                int InnerProperty { get; set; }
                void InnerMethodA();
                void InnerMethodB();
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 5, 5)
            .WithArguments("IInnerInterface");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task MultipleNestedLevels_DeepestLevelOutOfOrder_DeepestLevelReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public class MiddleClass
            {
                public void MiddleMethod() { }

                public class InnerClass
                {
                    private void InnerMethod() { }
                    private int innerField;
                }
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }

            public class MiddleClass
            {
                public void MiddleMethod() { }

                public class InnerClass
                {
                    private int innerField;
                    private void InnerMethod() { }
                }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 9, 9)
            .WithArguments("InnerClass");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task MultipleNestedLevels_AllLevelsOutOfOrder_AllLevelsReordered()
    {
        // Arrange
        const string sourceText = """
        public class OuterClass
        {
            public void OuterMethod() { }
            public int outerField;

            public class MiddleClass
            {
                public void MiddleMethod() { }
                public int middleField;

                public class InnerClass
                {
                    private void InnerMethod() { }
                    private int innerField;
                }
            }
        }
        """;

        const string expectedFixedSourceText = """
        public class OuterClass
        {
            public int outerField;
            public void OuterMethod() { }

            public class MiddleClass
            {
                public int middleField;
                public void MiddleMethod() { }

                public class InnerClass
                {
                    private int innerField;
                    private void InnerMethod() { }
                }
            }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults = [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("OuterClass"),
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 6, 5)
                .WithArguments("MiddleClass"),
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 11, 9)
                .WithArguments("InnerClass")
        ];

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task StructWithNestedClass_InnerClassOutOfOrder_InnerClassReordered()
    {
        // Arrange
        const string sourceText = """
        public struct OuterStruct
        {
            public void OuterMethod() { }

            public class InnerClass
            {
                private void InnerMethod() { }
                private int innerField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public struct OuterStruct
        {
            public void OuterMethod() { }

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult expectedDiagnosticResults = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation(string.Empty, 5, 5)
            .WithArguments("InnerClass");

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText);
    }

    [TestMethod]
    public async Task RecordWithNestedTypes_MixedNestedTypesOutOfOrder_NestedTypesReordered()
    {
        // Arrange
        const string sourceText = """
        public record OuterRecord
        {
            public void OuterMethod() { }

            public class InnerClass
            {
                private void InnerMethod() { }
                private int innerField;
            }

            public struct InnerStruct
            {
                private void StructMethod() { }
                private int structField;
            }
        }
        """;

        const string expectedFixedSourceText = """
        public record OuterRecord
        {
            public void OuterMethod() { }

            public struct InnerStruct
            {
                private int structField;
                private void StructMethod() { }
            }

            public class InnerClass
            {
                private int innerField;
                private void InnerMethod() { }
            }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults = [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("OuterRecord"),
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 5, 5)
                .WithArguments("InnerClass"),
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 11, 5)
                .WithArguments("InnerStruct")
        ];

        // Act and assert
        await VerifyCS.VerifyCodeFixAsync(sourceText, expectedDiagnosticResults, expectedFixedSourceText, 2);
    }
}
