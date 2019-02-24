﻿// Copyright (c) Tunnel Vision Laboratories, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace StyleCop.Analyzers.Test.CSharp9.SpacingRules
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis.Testing;
    using StyleCop.Analyzers.Test.CSharp8.SpacingRules;
    using Xunit;
    using static StyleCop.Analyzers.SpacingRules.SA1023DereferenceAndAccessOfSymbolsMustBeSpacedCorrectly;
    using static StyleCop.Analyzers.Test.Verifiers.StyleCopCodeFixVerifier<
        StyleCop.Analyzers.SpacingRules.SA1023DereferenceAndAccessOfSymbolsMustBeSpacedCorrectly,
        StyleCop.Analyzers.SpacingRules.TokenSpacingCodeFixProvider>;

    public class SA1023CSharp9UnitTests : SA1023CSharp8UnitTests
    {
        [Fact]
        public async Task TestFunctionPointerParameterValidSpacingAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerParameterInvalidPrecedingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate*<int {|#0:*|}> FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorNotPreceded).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerParameterInvalidTrailingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate*<int{|#0:*|} > FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorNotFollowed).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeValidSpacingAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeInvalidPrecedingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate {|#0:*|}<int*> FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorNotPreceded).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeInvalidTrailingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate{|#0:*|} <int*> FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate*<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorNotFollowed).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeWithCallingConventionValidSpacingAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate* managed<int*> FuncPtr;
}
";

            await VerifyCSharpDiagnosticAsync(testCode, DiagnosticResult.EmptyDiagnosticResults, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeWithCallingConventionInvalidPrecedingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate {|#0:*|} managed<int*> FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate* managed<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorNotPreceded).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }

        [Fact]
        public async Task TestFunctionPointerTypeWithCallingConventionMissingTrailingSpaceAsync()
        {
            var testCode = @"public class TestClass
{
    unsafe delegate{|#0:*|}managed<int*> FuncPtr;
}
";

            var fixedCode = @"public class TestClass
{
    unsafe delegate* managed<int*> FuncPtr;
}
";

            var expected = Diagnostic(DescriptorFollowed).WithLocation(0);
            await VerifyCSharpFixAsync(testCode, expected, fixedCode, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
