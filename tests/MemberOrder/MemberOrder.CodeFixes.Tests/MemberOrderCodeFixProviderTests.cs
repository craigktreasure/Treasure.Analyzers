namespace Treasure.Analyzers.MemberOrder.CodeFixes.Tests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCSCodeFix = Test.Verifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderCodeFixProviderTests
{
    [TestMethod]
    public async Task CodeFix_FieldsOutOfAccesibilityOrder_Reordered()
    {
        string test = @"
        class MyClass
        {
            private int myPrivateField;
            public int myPublicField;
        }";

        string fixtest = @"
        class MyClass
        {
            public int myPublicField;
            private int myPrivateField;
        }";

        DiagnosticResult expected = VerifyCSCodeFix.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCSCodeFix.VerifyCodeFixAsync(test, expected, fixtest);
    }

    [TestMethod]
    public async Task CodeFix_FieldsOutOfNameOrder_Reordered()
    {
        string test = @"
        class MyClass
        {
            public int myPublicFieldB;
            public int myPublicFieldA;
        }";

        string fixtest = @"
        class MyClass
        {
            public int myPublicFieldA;
            public int myPublicFieldB;
        }";

        DiagnosticResult expected = VerifyCSCodeFix.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCSCodeFix.VerifyCodeFixAsync(test, expected, fixtest);
    }

    [TestMethod]
    public async Task CodeFix_FieldsOutOfStaticOrder_Reordered()
    {
        string test = @"
        class MyClass
        {
            public int myPublicFieldA;
            public static int myPublicStaticFieldA;
        }";

        string fixtest = @"
        class MyClass
        {
            public static int myPublicStaticFieldA;
            public int myPublicFieldA;
        }";

        DiagnosticResult expected = VerifyCSCodeFix.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCSCodeFix.VerifyCodeFixAsync(test, expected, fixtest);
    }

    [TestMethod]
    public async Task CodeFix_MembersOutOfTypeOrder_Reordered()
    {
        string test = @"
        class MyClass
        {
            private void MyPrivateMethodB() { }
            ~MyClass() { }
            private MyClass() { }
            private int this[int a] { get => 0; set { } }
            private event MyPrivateDelegate MyPrivateEventField;
            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }
            private delegate void MyPrivateDelegate();
            private int MyPrivateProperty { get; set; }
            private int myPrivateField;
        }";

        string fixtest = @"
        class MyClass
        {
            private int myPrivateField;
            private int MyPrivateProperty { get; set; }
            private delegate void MyPrivateDelegate();
            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }
            private event MyPrivateDelegate MyPrivateEventField;
            private int this[int a] { get => 0; set { } }
            private MyClass() { }
            ~MyClass() { }
            private void MyPrivateMethodB() { }
        }";

        DiagnosticResult expected = VerifyCSCodeFix.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCSCodeFix.VerifyCodeFixAsync(test, expected, fixtest);
    }

    [TestMethod]
    public async Task CodeFix_MembersOutOfTypeOrderWithWhiteSpace_ReorderedWithWhiteSpaceMaintained()
    {
        string test = @"
        class MyClass
        {
            private void MyPrivateMethodB() { }

            ~MyClass() { }

            private MyClass() { }

            private int this[int a] { get => 0; set { } }

            private event MyPrivateDelegate MyPrivateEventField;

            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }

            private delegate void MyPrivateDelegate();

            private int MyPrivateProperty { get; set; }

            private int myPrivateField;
        }";

        string fixtest = @"
        class MyClass
        {
            private int myPrivateField;

            private int MyPrivateProperty { get; set; }

            private delegate void MyPrivateDelegate();

            private event MyPrivateDelegate MyPrivateEvent { add { } remove { } }

            private event MyPrivateDelegate MyPrivateEventField;

            private int this[int a] { get => 0; set { } }

            private MyClass() { }

            ~MyClass() { }

            private void MyPrivateMethodB() { }
        }";

        DiagnosticResult expected = VerifyCSCodeFix.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCSCodeFix.VerifyCodeFixAsync(test, expected, fixtest);
    }
}
