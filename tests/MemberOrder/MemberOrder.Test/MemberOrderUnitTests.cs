﻿namespace Treasure.Analyzers.MemberOrder.Test;

using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyCS = Verifiers.CSharpCodeFixVerifier<
    MemberOrderAnalyzer,
    CodeFixes.MemberOrderCodeFixProvider>;

[TestClass]
public class MemberOrderUnitTest
{
    //No diagnostics expected to show up
    [TestMethod]
    public async Task TestMethod1()
    {
        string test = @"";

        await VerifyCS.VerifyAnalyzerAsync(test);
    }

    //Diagnostic and CodeFix both triggered and checked for
    [TestMethod]
    public async Task TestMethod2()
    {
        string test = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class {|#0:TypeName|}
        {   
        }
    }";

        string fixtest = @"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;

    namespace ConsoleApplication1
    {
        class TYPENAME
        {   
        }
    }";

        Microsoft.CodeAnalysis.Testing.DiagnosticResult expected = VerifyCS.Diagnostic("MemberOrder").WithLocation(0).WithArguments("TypeName");
        await VerifyCS.VerifyCodeFixAsync(test, expected, fixtest);
    }
}
