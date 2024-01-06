// ---------------------------------------------------------------------------
// <copyright file="SpdxExpressionTest.cs" company="Tethys">
//   Copyright (C) 2023-2024 T. Graf
// </copyright>
//
// Licensed under the Apache License, Version 2.0.
// SPDX-License-Identifier: Apache-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied.
// ---------------------------------------------------------------------------


namespace Tethys.SPDX.ExpressionParser.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tethys.SPDX.ExpressionParser;

    [TestClass]
    public class SpdxExpressionTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLicenseExpressionInvalidArgs()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxLicenseExpression(null, false);
        }

        [TestMethod]
        public void TestSimpleLicenseExpression()
        {
            var expr = new SpdxLicenseExpression("GPL-2.0", false);
            var actual = expr.ToString();
            Assert.AreEqual("GPL-2.0", actual);
        }

        [TestMethod]
        public void TestLicenseOrLater1()
        {
            var expr = new SpdxLicenseExpression("GPL-2.0", true);
            var actual = expr.ToString();
            Assert.AreEqual("GPL-2.0+", actual);
        }

        [TestMethod]
        public void TestLicenseOrLater2()
        {
            // expression does not make sense, but is a VALID expression
            var expr = new SpdxLicenseExpression("MIT", true);
            var actual = expr.ToString();
            Assert.AreEqual("MIT+", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestLicenseReferenceInvalidArgs()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxLicenseReference(null);
        }

        [TestMethod]
        public void TestLicenseReference()
        {
            const string Reference = "LicenseRef-org-name";
            var expr = new SpdxLicenseReference(Reference);
            var actual = expr.ToString();
            Assert.AreEqual(Reference, actual);

            expr = new SpdxLicenseReference(string.Empty);
            actual = expr.ToString();
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithExpressionNoExpression()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxWithExpression(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestWithExpressionNoException()
        {
            var license = new SpdxLicenseExpression("Apache-2.0", false);

            // ReSharper disable once ObjectCreationAsStatement
            new SpdxWithExpression(license, null);
        }

        [TestMethod]
        public void TestWithExpression()
        {
            const string Exception = "LLVM-exception";
            var license = new SpdxLicenseExpression("Apache-2.0", false);
            var expr = new SpdxWithExpression(license, Exception);
            var actual = expr.ToString();
            Assert.AreEqual("Apache-2.0 WITH LLVM-exception", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAndExpressionNoLeftExpression()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxAndExpression(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAndExpressionNoRightExpression()
        {
            var left = new SpdxLicenseExpression("Apache-2.0", false);

            // ReSharper disable once ObjectCreationAsStatement
            new SpdxAndExpression(left, null);
        }

        [TestMethod]
        public void TestAndExpression()
        {
            var left = new SpdxLicenseExpression("Apache-2.0", false);
            var right = new SpdxLicenseExpression("MIT", false);
            var expr = new SpdxAndExpression(left, right);
            var actual = expr.ToString();
            Assert.AreEqual("Apache-2.0 AND MIT", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOrExpressionNoLeftExpression()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxOrExpression(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestOrExpressionNoRightExpression()
        {
            var left = new SpdxLicenseExpression("Apache-2.0", false);

            // ReSharper disable once ObjectCreationAsStatement
            new SpdxOrExpression(left, null);
        }

        [TestMethod]
        public void TestOrExpression()
        {
            var left = new SpdxLicenseExpression("GPL-2.0", true);
            var right = new SpdxLicenseExpression("EPL-1.0", false);
            var expr = new SpdxOrExpression(left, right);
            var actual = expr.ToString();
            Assert.AreEqual("GPL-2.0+ OR EPL-1.0", actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestScopedExpressionNoExpression()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new SpdxScopedExpression(null);
        }


        [TestMethod]
        public void TestScopedExpression()
        {
            var left = new SpdxLicenseExpression("GPL-2.0", true);
            var expr = new SpdxScopedExpression(left);
            var actual = expr.ToString();
            Assert.AreEqual("(GPL-2.0+)", actual);
        }
    }
}
