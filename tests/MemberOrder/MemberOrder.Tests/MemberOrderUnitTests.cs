namespace Treasure.Analyzers.MemberOrder.Tests;

using System.Threading.Tasks;

using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Test.Verifiers.CSharpAnalyzerVerifier<MemberOrderAnalyzer>;

[TestClass]
public class MemberOrderUnitTest
{
    [TestMethod]
    public async Task Analyzer_Class_AllCategoriesModifiersAndNamesInOrder_NoDiagnostics()
    {
        string test = @"
        public class MyClass
        {
            // Fields
            public int myPublicFieldA;
            public int myPublicFieldB;
            internal int myInternalFieldA;
            internal int myInternalFieldB;
            protected internal int myProtectedInternalFieldA;
            protected internal int myProtectedInternalFieldB;
            private protected int myPrivateProtectedFieldA;
            private protected int myPrivateProtectedFieldB;
            protected int myProtectedFieldA;
            protected int myProtectedFieldB;
            private int myPrivateFieldA;
            private int myPrivateFieldB;

            // Properties
            public int MyPublicPropertyA { get; set; }
            public int MyPublicPropertyB { get; set; }
            internal int MyInternalPropertyA { get; set; }
            internal int MyInternalPropertyB { get; set; }
            protected internal int MyProtectedInternalPropertyA { get; set; }
            protected internal int MyProtectedInternalPropertyB { get; set; }
            private protected int MyPrivateProtectedPropertyA { get; set; }
            private protected int MyPrivateProtectedPropertyB { get; set; }
            protected int MyProtectedPropertyA { get; set; }
            protected int MyProtectedPropertyB { get; set; }
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
            public MyClass() { }
            internal MyClass(int a, int b) { }
            protected internal MyClass(int a) { }
            private protected MyClass(int a, int b, int c, int d) { }
            protected MyClass(int a, int b, int c) { }
            private MyClass(int a, int b, int c, int d, int e) { }

            // Destructors
            ~MyClass() { }

            // Methods
            public void MyPublicMethodA() { }
            public void MyPublicMethodB() { }
            internal void MyInternalMethodA() { }
            internal void MyInternalMethodB() { }
            protected internal void MyProtectedInternalMethodA() { }
            protected internal void MyProtectedInternalMethodB() { }
            private protected void MyPrivateProtectedMethodA() { }
            private protected void MyPrivateProtectedMethodB() { }
            protected void MyProtectedMethodA() { }
            protected void MyProtectedMethodB() { }
            private void MyPrivateMethodA() { }
            private void MyPrivateMethodB() { }
        }";

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    [TestMethod]
    public async Task Analyzer_Class_ConstructorBeforeIndexer_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public MyClass() { }
            public int this[int a] { get => 0; set { } }
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_DeconstructorBeforeConstructor_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            ~MyClass() { }
            public MyClass() { }
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_DelegateBeforeProperty_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public delegate void MyDelegate();
            public string MyProperty { get; set; }
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_EventBeforeDelegate_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public event MyDelegate MyEvent { add { } remove { } }
            public delegate void MyDelegate();
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_EventFieldBeforeDelegate_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public event MyDelegate MyEventField;
            public delegate void MyDelegate();
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_IndexerBeforeEvent_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEvent { add { } remove { } }
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_IndexerBeforeEventField_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public delegate void MyDelegate();
            public int this[int a] { get => 0; set { } }
            public event MyDelegate MyEventField;
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_MethodBeforeDeconstructor_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public void MyMethod() { }
            ~MyClass() { }
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_Class_PropertyBeforeField_SingleDiagnostic()
    {
        string test = @"
        public class MyClass
        {
            public string MyProperty { get; set; }
            public int myField;
        }";

        DiagnosticResult[] expected = new[]
        {
            VerifyCS.Diagnostic(MemberOrderAnalyzer.DiagnosticId)
                .WithLocation(string.Empty, 2, 9)
                .WithArguments("MyClass"),
        };

        await VerifyCS.VerifyAnalyzerAsync(test, expected);
    }

    [TestMethod]
    public async Task Analyzer_EmptyContent_NoDiagnostics()
    {
        string test = @"";

        await VerifyCS.VerifyAnalyzerAsync(test);
    }
}
