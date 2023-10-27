namespace Treasure.Analyzers.MemberOrder.Tests;

using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Test.Verifiers.CSharpAnalyzerVerifier<MemberOrderAnalyzer>;

[TestClass]
public class MemberOrderUnitTest_Struct
{
    [TestMethod]
    public async Task Analyzer_AllCategoriesModifiersAndNamesInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
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
            // protected internal const int myProtectedInternalConstantFieldA = 0;
            // protected internal const int myProtectedInternalConstantFieldB = 0;
            // protected internal static int myProtectedInternalStaticFieldA;
            // protected internal static int myProtectedInternalStaticFieldB;
            // protected internal int myProtectedInternalFieldA;
            // protected internal int myProtectedInternalFieldB;
            // private protected const int myPrivateProtectedConstantFieldA = 0;
            // private protected const int myPrivateProtectedConstantFieldB = 0;
            // private protected static int myPrivateProtectedStaticFieldA;
            // private protected static int myPrivateProtectedStaticFieldB;
            // private protected int myPrivateProtectedFieldA;
            // private protected int myPrivateProtectedFieldB;
            // protected const int myProtectedConstantFieldA = 0;
            // protected const int myProtectedConstantFieldB = 0;
            // protected static int myProtectedStaticFieldA;
            // protected static int myProtectedStaticFieldB;
            // protected int myProtectedFieldA;
            // protected int myProtectedFieldB;
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
            // protected internal static int MyProtectedInternalStaticPropertyA { get; set; }
            // protected internal static int MyProtectedInternalStaticPropertyB { get; set; }
            // protected internal int MyProtectedInternalPropertyA { get; set; }
            // protected internal int MyProtectedInternalPropertyB { get; set; }
            // private protected static int MyPrivateProtectedStaticPropertyA { get; set; }
            // private protected static int MyPrivateProtectedStaticPropertyB { get; set; }
            // private protected int MyPrivateProtectedPropertyA { get; set; }
            // private protected int MyPrivateProtectedPropertyB { get; set; }
            // protected static int MyProtectedStaticPropertyA { get; set; }
            // protected static int MyProtectedStaticPropertyB { get; set; }
            // protected int MyProtectedPropertyA { get; set; }
            // protected int MyProtectedPropertyB { get; set; }
            private static int MyPrivateStaticPropertyA { get; set; }
            private static int MyPrivateStaticPropertyB { get; set; }
            private int MyPrivatePropertyA { get; set; }
            private int MyPrivatePropertyB { get; set; }

            // Delegates
            public delegate void MyPublicDelegateA();
            public delegate void MyPublicDelegateB();
            internal delegate void MyInternalDelegateA();
            internal delegate void MyInternalDelegateB();
            // protected internal delegate void MyProtectedInternalDelegateA();
            // protected internal delegate void MyProtectedInternalDelegateB();
            // private protected delegate void MyPrivateProtectedDelegateA();
            // private protected delegate void MyPrivateProtectedDelegateB();
            // protected delegate void MyProtectedDelegateA();
            // protected delegate void MyProtectedDelegateB();
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
            // protected internal event MyProtectedInternalDelegateA MyProtectedInternalEventA { add { } remove { } }
            // protected internal event MyProtectedInternalDelegateB MyProtectedInternalEventB { add { } remove { } }
            // protected internal event MyProtectedInternalDelegateA MyProtectedInternalEventFieldA;
            // protected internal event MyProtectedInternalDelegateB MyProtectedInternalEventFieldB;
            // private protected event MyPrivateProtectedDelegateA MyPrivateProtectedEventA { add { } remove { } }
            // private protected event MyPrivateProtectedDelegateB MyPrivateProtectedEventB { add { } remove { } }
            // private protected event MyPrivateProtectedDelegateA MyPrivateProtectedEventFieldA;
            // private protected event MyPrivateProtectedDelegateB MyPrivateProtectedEventFieldB;
            // protected event MyProtectedDelegateA MyProtectedEventA { add { } remove { } }
            // protected event MyProtectedDelegateB MyProtectedEventB { add { } remove { } }
            // protected event MyProtectedDelegateA MyProtectedEventFieldA;
            // protected event MyProtectedDelegateB MyProtectedEventFieldB;
            private event MyPrivateDelegateA MyPrivateEventA { add { } remove { } }
            private event MyPrivateDelegateB MyPrivateEventB { add { } remove { } }
            private event MyPrivateDelegateA MyPrivateEventFieldA;
            private event MyPrivateDelegateB MyPrivateEventFieldB;

            // Indexers
            public int this[int a] { get => 0; set { } }
            internal int this[int a, int b] { get => 0; set { } }
            // protected internal int this[int a, int b, int c] { get => 0; set { } }
            // private protected int this[int a, int b, int c, int d] { get => 0; set { } }
            // protected int this[int a, int b, int c, int d, int e] { get => 0; set { } }
            private int this[int a, int b, int c, int d, int e, int f] { get => 0; set { } }

            // Constructors
            static MyStruct() { }
            public MyStruct() { }
            internal MyStruct(int a, int b) { }
            //protected internal MyStruct(int a) { }
            //private protected MyStruct(int a, int b, int c, int d) { }
            //protected MyStruct(int a, int b, int c) { }
            private MyStruct(int a, int b, int c, int d, int e) { }

            // Methods
            public static void MyPublicStaticMethodA() { }
            public static void MyPublicStaticMethodB() { }
            public void MyPublicMethodA() { }
            public void MyPublicMethodB() { }
            internal static void MyInternalStaticMethodA() { }
            internal static void MyInternalStaticMethodB() { }
            internal void MyInternalMethodA() { }
            internal void MyInternalMethodB() { }
            // protected internal static void MyProtectedInternalStaticMethodA() { }
            // protected internal static void MyProtectedInternalStaticMethodB() { }
            // protected internal void MyProtectedInternalMethodA() { }
            // protected internal void MyProtectedInternalMethodB() { }
            // private protected static void MyPrivateProtectedStaticMethodA() { }
            // private protected static void MyPrivateProtectedStaticMethodB() { }
            // private protected void MyPrivateProtectedMethodA() { }
            // private protected void MyPrivateProtectedMethodB() { }
            // protected static void MyProtectedStaticMethodA() { }
            // protected static void MyProtectedStaticMethodB() { }
            // protected void MyProtectedMethodA() { }
            // protected void MyProtectedMethodB() { }
            private static void MyPrivateStaticMethodA() { }
            private static void MyPrivateStaticMethodB() { }
            private void MyPrivateMethodA() { }
            private void MyPrivateMethodB() { }
        }";

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Analyzer_ConstructorBeforeIndexer_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public MyStruct() { }
            public int this[int a] { get => 0; set { } }
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_DelegateBeforeProperty_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public delegate void MyDelegate();
            public string MyProperty { get; set; }
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_EventBeforeDelegate_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public event MyDelegate MyEvent { add { } remove { } }
            public delegate void MyDelegate();
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_EventFieldBeforeDelegate_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public event MyDelegate MyEventField;
            public delegate void MyDelegate();
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_IndexerBeforeEvent_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEvent { add { } remove { } }
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_IndexerBeforeEventField_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEventField;
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_KeywordsInOrder_NoDiagnostics()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
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

            // Constructors
            public MyStruct() { }

            // Methods
            public static void MyPublicStaticMethod() { }
            public void MyPublicMethod() { }
        }";

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }

    [TestMethod]
    public async Task Analyzer_PropertyBeforeField_SingleDiagnostic()
    {
        // Arrange
        const string sourceText = @"
        public struct MyStruct
        {
            public string MyProperty { get; set; }
            public int myField;
        }";

        DiagnosticResult[] expectedDiagnosticResults = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyStruct"),
        };

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText, expectedDiagnosticResults);
    }

    [TestMethod]
    public async Task Analyzer_SubTypes_NoDiagnostics()
    {
        // Arrange
        const string sourceText = @"
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
        }";

        // Act and assert
        await VerifyCS.VerifyAnalyzerAsync(sourceText);
    }
}
