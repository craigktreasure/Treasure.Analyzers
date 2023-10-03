﻿namespace Treasure.Analyzers.MemberOrder.CodeFixes.Tests;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Test.Verifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderCodeFixProviderTests
{
    [TestMethod]
    public async Task CodeFix_Class_SubTypesOutOfOrder_Reordered()
    {
        string test = @"
        public class MyClass
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
        }";

        string fixtest = @"
        public class MyClass
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
        }";

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    }

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

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
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

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    }

    [TestMethod]
    public async Task CodeFix_FieldsOutOfSpecialKeywordOrder_Reordered()
    {
        string test = @"
        class MyClass
        {
            public int myPublicField;
            public readonly int myPublicReadonlyField = 1;
            public static int myPublicStaticField;
            public static readonly int myPublicStaticReadonlyField;
            public const int myPublicConstantField = 0;
        }";

        string fixtest = @"
        class MyClass
        {
            public const int myPublicConstantField = 0;
            public static readonly int myPublicStaticReadonlyField;
            public static int myPublicStaticField;
            public readonly int myPublicReadonlyField = 1;
            public int myPublicField;
        }";

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
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

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
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

        DiagnosticResult expected = VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
            .WithLocation("", 2, 9)
            .WithArguments("MyClass");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    }
}
