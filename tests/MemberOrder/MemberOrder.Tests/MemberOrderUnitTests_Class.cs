﻿namespace Treasure.Analyzers.MemberOrder.Tests;

using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Treasure.Analyzers.MemberOrder.Tests.TestUtils;

using VerifyCS = Test.Verifiers.CSharpAnalyzerVerifier<MemberOrderAnalyzer>;

[TestClass]
public class MemberOrderUnitTests_Class
{
    [TestMethod]
    public async Task Category_AllCategoriesModifiersAndNamesInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            // Fields
            public const int myPublicConstantFieldA = 0;
            public const int myPublicConstantFieldB = 0;
            public static int myPublicStaticFieldA;
            public static int myPublicStaticFieldB;
            public int myPublicFieldA;
            public int myPublicFieldB;
            internal const int myInternalConstantFieldA = 0;
            internal const int myInternalConstantFieldB = 0;
            internal static int myInternalStaticFieldA;
            internal static int myInternalStaticFieldB;
            internal int myInternalFieldA;
            internal int myInternalFieldB;
            protected internal const int myProtectedInternalConstantFieldA = 0;
            protected internal const int myProtectedInternalConstantFieldB = 0;
            protected internal static int myProtectedInternalStaticFieldA;
            protected internal static int myProtectedInternalStaticFieldB;
            protected internal int myProtectedInternalFieldA;
            protected internal int myProtectedInternalFieldB;
            private protected const int myPrivateProtectedConstantFieldA = 0;
            private protected const int myPrivateProtectedConstantFieldB = 0;
            private protected static int myPrivateProtectedStaticFieldA;
            private protected static int myPrivateProtectedStaticFieldB;
            private protected int myPrivateProtectedFieldA;
            private protected int myPrivateProtectedFieldB;
            protected const int myProtectedConstantFieldA = 0;
            protected const int myProtectedConstantFieldB = 0;
            protected static int myProtectedStaticFieldA;
            protected static int myProtectedStaticFieldB;
            protected int myProtectedFieldA;
            protected int myProtectedFieldB;
            private const int myPrivateConstantFieldA = 0;
            private const int myPrivateConstantFieldB = 0;
            private static int myPrivateStaticFieldA;
            private static int myPrivateStaticFieldB;
            private int myPrivateFieldA;
            private int myPrivateFieldB;

            // Properties
            public static int MyPublicStaticPropertyA { get; set; }
            public static int MyPublicStaticPropertyB { get; set; }
            public int MyPublicPropertyA { get; set; }
            public int MyPublicPropertyB { get; set; }
            internal static int MyInternalStaticPropertyA { get; set; }
            internal static int MyInternalStaticPropertyB { get; set; }
            internal int MyInternalPropertyA { get; set; }
            internal int MyInternalPropertyB { get; set; }
            protected internal static int MyProtectedInternalStaticPropertyA { get; set; }
            protected internal static int MyProtectedInternalStaticPropertyB { get; set; }
            protected internal int MyProtectedInternalPropertyA { get; set; }
            protected internal int MyProtectedInternalPropertyB { get; set; }
            private protected static int MyPrivateProtectedStaticPropertyA { get; set; }
            private protected static int MyPrivateProtectedStaticPropertyB { get; set; }
            private protected int MyPrivateProtectedPropertyA { get; set; }
            private protected int MyPrivateProtectedPropertyB { get; set; }
            protected static int MyProtectedStaticPropertyA { get; set; }
            protected static int MyProtectedStaticPropertyB { get; set; }
            protected int MyProtectedPropertyA { get; set; }
            protected int MyProtectedPropertyB { get; set; }
            private static int MyPrivateStaticPropertyA { get; set; }
            private static int MyPrivateStaticPropertyB { get; set; }
            private int MyPrivatePropertyA { get; set; }
            private int MyPrivatePropertyB { get; set; }

            // Delegates
            public delegate void MyPublicDelegateA();
            public delegate void MyPublicDelegateB();
            internal delegate void MyInternalDelegateA();
            internal delegate void MyInternalDelegateB();
            protected internal delegate void MyProtectedInternalDelegateA();
            protected internal delegate void MyProtectedInternalDelegateB();
            private protected delegate void MyPrivateProtectedDelegateA();
            private protected delegate void MyPrivateProtectedDelegateB();
            protected delegate void MyProtectedDelegateA();
            protected delegate void MyProtectedDelegateB();
            private delegate void MyPrivateDelegateA();
            private delegate void MyPrivateDelegateB();

            // Events and event fields
            public event MyPublicDelegateA MyPublicEventA { add { } remove { } }
            public event MyPublicDelegateB MyPublicEventB { add { } remove { } }
            public event MyPublicDelegateA MyPublicEventFieldA;
            public event MyPublicDelegateB MyPublicEventFieldB;
            internal event MyInternalDelegateA MyInternalEventA { add { } remove { } }
            internal event MyInternalDelegateB MyInternalEventB { add { } remove { } }
            internal event MyInternalDelegateA MyInternalEventFieldA;
            internal event MyInternalDelegateB MyInternalEventFieldB;
            protected internal event MyProtectedInternalDelegateA MyProtectedInternalEventA { add { } remove { } }
            protected internal event MyProtectedInternalDelegateB MyProtectedInternalEventB { add { } remove { } }
            protected internal event MyProtectedInternalDelegateA MyProtectedInternalEventFieldA;
            protected internal event MyProtectedInternalDelegateB MyProtectedInternalEventFieldB;
            private protected event MyPrivateProtectedDelegateA MyPrivateProtectedEventA { add { } remove { } }
            private protected event MyPrivateProtectedDelegateB MyPrivateProtectedEventB { add { } remove { } }
            private protected event MyPrivateProtectedDelegateA MyPrivateProtectedEventFieldA;
            private protected event MyPrivateProtectedDelegateB MyPrivateProtectedEventFieldB;
            protected event MyProtectedDelegateA MyProtectedEventA { add { } remove { } }
            protected event MyProtectedDelegateB MyProtectedEventB { add { } remove { } }
            protected event MyProtectedDelegateA MyProtectedEventFieldA;
            protected event MyProtectedDelegateB MyProtectedEventFieldB;
            private event MyPrivateDelegateA MyPrivateEventA { add { } remove { } }
            private event MyPrivateDelegateB MyPrivateEventB { add { } remove { } }
            private event MyPrivateDelegateA MyPrivateEventFieldA;
            private event MyPrivateDelegateB MyPrivateEventFieldB;

            // Indexers
            public int this[int a] { get => 0; set { } }
            internal int this[int a, int b] { get => 0; set { } }
            protected internal int this[int a, int b, int c] { get => 0; set { } }
            private protected int this[int a, int b, int c, int d] { get => 0; set { } }
            protected int this[int a, int b, int c, int d, int e] { get => 0; set { } }
            private int this[int a, int b, int c, int d, int e, int f] { get => 0; set { } }

            // Constructors
            static MyClass() { }
            public MyClass() { }
            internal MyClass(int a, int b) { }
            protected internal MyClass(int a) { }
            private protected MyClass(int a, int b, int c, int d) { }
            protected MyClass(int a, int b, int c) { }
            private MyClass(int a, int b, int c, int d, int e) { }

            // Destructors
            ~MyClass() { }

            // Methods
            public static void MyPublicStaticMethodA() { }
            public static void MyPublicStaticMethodB() { }
            public void MyPublicMethodA() { }
            public void MyPublicMethodB() { }
            internal static void MyInternalStaticMethodA() { }
            internal static void MyInternalStaticMethodB() { }
            internal void MyInternalMethodA() { }
            internal void MyInternalMethodB() { }
            protected internal static void MyProtectedInternalStaticMethodA() { }
            protected internal static void MyProtectedInternalStaticMethodB() { }
            protected internal void MyProtectedInternalMethodA() { }
            protected internal void MyProtectedInternalMethodB() { }
            private protected static void MyPrivateProtectedStaticMethodA() { }
            private protected static void MyPrivateProtectedStaticMethodB() { }
            private protected void MyPrivateProtectedMethodA() { }
            private protected void MyPrivateProtectedMethodB() { }
            protected static void MyProtectedStaticMethodA() { }
            protected static void MyProtectedStaticMethodB() { }
            protected void MyProtectedMethodA() { }
            protected void MyProtectedMethodB() { }
            private static void MyPrivateStaticMethodA() { }
            private static void MyPrivateStaticMethodB() { }
            private void MyPrivateMethodA() { }
            private void MyPrivateMethodB() { }
        }
        """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Category_ConstructorBeforeIndexer_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public MyClass() { }
            public int this[int a] { get => 0; set { } }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_DeconstructorBeforeConstructor_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            ~MyClass() { }
            public MyClass() { }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_DelegateBeforeProperty_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public delegate void MyDelegate();
            public string MyProperty { get; set; }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_EventBeforeDelegate_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public event MyDelegate MyEvent { add { } remove { } }
            public delegate void MyDelegate();
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_EventFieldBeforeDelegate_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public event MyDelegate MyEventField;
            public delegate void MyDelegate();
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_IndexerBeforeEvent_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEvent { add { } remove { } }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_IndexerBeforeEventField_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEventField;
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_KeywordsInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            // Fields
            public const int myPublicConstantField = 0;
            public static readonly int myPublicStaticReadonlyField;
            public static int myPublicStaticField;
            public readonly int myPublicReadonlyField = 1;
            public int myPublicField;

            // Properties
            public static int MyPublicStaticProperty { get; set; }
            public int MyPublicProperty { get; set; }

            // Delegates
            public delegate void MyPublicDelegate();

            // Events and event fields
            public static event MyPublicDelegate MyPublicStaticEvent { add { } remove { } }
            public static event MyPublicDelegate MyPublicStaticEventField;
            public event MyPublicDelegate MyPublicEvent { add { } remove { } }
            public event MyPublicDelegate MyPublicEventField;

            // Methods
            public static void MyPublicStaticMethod() { }
            public void MyPublicMethod() { }
        }
        """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Category_MethodBeforeDeconstructor_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public void MyMethod() { }
            ~MyClass() { }
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_PropertyBeforeField_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = """
        public class MyClass
        {
            public string MyProperty { get; set; }
            public int myField;
        }
        """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Category_SubTypes_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
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
        }
        """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Constructors_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} MyClass(string a) { }
                {{secondAccessModifier}} MyClass(int a) { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Constructors_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} MyClass(string a) { }
                {{secondAccessModifier}} MyClass(int a) { }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Constructors_StaticConstructorFirst_NoDiagnostics()
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                static MyClass() { }
                public MyClass() { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Constructors_StaticConstructorNotFirst_SingleDiagnostic()
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public MyClass() { }
                static MyClass() { }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Delegates_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} delegate void Member2();
                {{secondAccessModifier}} delegate void Member1();
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Delegates_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} delegate void Member1();
                {{secondAccessModifier}} delegate void Member2();
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Delegates_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void Member1();
                public delegate void Member2();
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Delegates_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void Member2();
                public delegate void Member1();
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task EventFields_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                {{firstAccessModifier}} event MemberDelegate Member2;
                {{secondAccessModifier}} event MemberDelegate Member1;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task EventFields_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                {{firstAccessModifier}} event MemberDelegate Member1;
                {{secondAccessModifier}} event MemberDelegate Member2;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task EventFields_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member1;
                public event MemberDelegate Member2;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task EventFields_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member2;
                public event MemberDelegate Member1;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task EventFields_StaticFirst_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public static event MemberDelegate StaticMember1;
                public event MemberDelegate Member1;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task EventFields_StaticNotFirst_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member1;
                public static event MemberDelegate StaticMember1;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Events_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                {{firstAccessModifier}} event MemberDelegate Member2 { add { } remove { } }
                {{secondAccessModifier}} event MemberDelegate Member1 { add { } remove { } }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Events_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                {{firstAccessModifier}} event MemberDelegate Member1 { add { } remove { } }
                {{secondAccessModifier}} event MemberDelegate Member2 { add { } remove { } }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Events_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member1 { add { } remove { } }
                public event MemberDelegate Member2 { add { } remove { } }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Events_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member2 { add { } remove { } }
                public event MemberDelegate Member1 { add { } remove { } }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Events_StaticFirst_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public static event MemberDelegate StaticMember1 { add { } remove { } }
                public event MemberDelegate Member1 { add { } remove { } }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Events_StaticNotFirst_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public delegate void MemberDelegate();
                public event MemberDelegate Member1 { add { } remove { } }
                public static event MemberDelegate StaticMember1 { add { } remove { } }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Fields_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int Member2;
                {{secondAccessModifier}} int Member1;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Fields_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int Member1;
                {{secondAccessModifier}} int Member2;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Fields_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public int Member1;
                public int Member2;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Fields_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public int Member2;
                public int Member1;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Fields_ConstGroupedByAccessibility_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
            public class MyClass
            {
                public const int ConstMember1 = 0;
                public int Member1 = 0;
                internal const int ConstMember2 = 0;
                internal int Member2 = 0;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DataRow("const", "static")]
    [DataRow("const", "")]
    [DataRow("static", "")]
    public async Task Fields_KeywordsInOrder_NoDiagnostics(string firstKeyword, string secondKeyword)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public {{firstKeyword}} int Member2 = 0;
                public {{secondKeyword}} int Member1 = 0;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DataRow("static", "const")]
    [DataRow("", "const")]
    [DataRow("", "static")]
    public async Task Fields_KeywordsNotInOrder_SingleDiagnostic(string firstKeyword, string secondKeyword)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                public {{firstKeyword}} int Member1 = 0;
                public {{secondKeyword}} int Member2 = 0;
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Fields_StaticGroupedByAccessibility_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
            public class MyClass
            {
                public static int StaticMember1 = 0;
                public int Member1 = 0;
                internal static int StaticMember2 = 0;
                internal int Member2 = 0;
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Indexers_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int this[int a] { get => 0; set { } }
                {{secondAccessModifier}} int this[string a] { get => 0; set { } }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Indexers_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int this[int a] { get => 0; set { } }
                {{secondAccessModifier}} int this[string a] { get => 0; set { } }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Methods_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} void Method2() { }
                {{secondAccessModifier}} void Method1() { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Methods_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} void Method1() { }
                {{secondAccessModifier}} void Method2() { }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Methods_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public void Method1() { }
                public void Method2() { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Methods_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public void Method2() { }
                public void Method1() { }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Methods_StaticFirst_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public static void Method2() { }
                public void Method1() { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Methods_StaticGroupedByAccessibility_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
            public class MyClass
            {
                public static void StaticMethod1() { }
                public void Method1() { }
                internal static void StaticMethod2() { }
                internal void Method2() { }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Methods_StaticNotFirst_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public void Method1() { }
                public static void Method2() { }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersInOrder), typeof(TestData))]
    public async Task Properties_AccessModifiersInOrder_NoDiagnostics(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int Member2 { get; set; }
                {{secondAccessModifier}} int Member1 { get; set; }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    [DynamicData(nameof(TestData.ClassAccessModifiersNotInOrder), typeof(TestData))]
    public async Task Properties_AccessModifiersNotInOrder_SingleDiagnostic(string firstAccessModifier, string secondAccessModifier)
    {
        // Arrange
        string sourceText = $$"""
            public class MyClass
            {
                {{firstAccessModifier}} int Member1 { get; set; }
                {{secondAccessModifier}} int Member2 { get; set; }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Properties_AlphabeticalInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public int Member1 { get; set; }
                public int Member2 { get; set; }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Properties_AlphabeticalNotInOrder_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public int Member2 { get; set; }
                public int Member1 { get; set; }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Properties_StaticFirst_NoDiagnostics()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public static int Member2 { get; set; }
                public int Member1 { get; set; }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Properties_StaticGroupedByAccessibility_NoDiagnostics()
    {
        // Arrange
        const string sourceText = """
            public class MyClass
            {
                public static int StaticMember1 { get; set; }
                public int Member1 { get; set; }
                internal static int StaticMember2 { get; set; }
                internal int Member2 { get; set; }
            }
            """;

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Properties_StaticNotFirst_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = $$"""
            public class MyClass
            {
                public int Member1 { get; set; }
                public static int Member2 { get; set; }
            }
            """;

        DiagnosticResult[] expectedDiagnosticResults =
        [
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 1, 1)
                .WithArguments("MyClass"),
        ];

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }
}
