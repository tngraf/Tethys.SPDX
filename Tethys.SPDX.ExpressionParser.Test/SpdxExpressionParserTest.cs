// ---------------------------------------------------------------------------
// <copyright file="SpdxExpressionParserTest.cs" company="Tethys">
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

    /// <summary>
    /// Test of the SPDX expression parser.
    /// </summary>
    /// <example>
    /// Valid Expressions:
    ///   MIT
    ///   Apache-2.0
    ///   BSD-3-Clause
    ///   GPL-2.0
    ///   GPL-2.0-or-later
    ///   ISC
    ///   MIT and Apache-2.0
    ///   MIT and ISC and Apache-2.0
    ///   MIT OR Apache-2.0
    ///   MIT OR Apache-2.0 or GPL-2.0
    ///   GPL-2.0 with Autoconf-exception-2.0
    ///   GPL-2.0+
    ///   LicenseRef-siemens-DOM4J
    /// </example>
    [TestClass]
    public class SpdxExpressionParserTest
    {
        /// <summary>
        /// The SPDX licenses.
        /// </summary>
        private List<string> spdxLicenses;

        /// <summary>
        /// The SPDX exceptions.
        /// </summary>
        private List<string> spdxExceptions;

        [TestInitialize]
        public void TestInitialize()
        {
            this.spdxLicenses = new List<string>
            {
                "MIT",
                "Apache-2.0",
                "BSD-3-Clause",
                "GPL-2.0",
                "GPL-2.0-or-later",
                "ISC",
                "MPL-1.0"
            };

            this.spdxExceptions = new List<string>
            {
                "Autoconf-exception-2.0",
                "Bison-exception-2.2",
                "GCC-exception-2.0",
                "Libtool-exception"
            };
        }

        /// <summary>
        /// Determines whether this is a SPDX identifier.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if this is a SPDX identifier; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSpdxIdentifier(string expression)
        {
            foreach (var spdxLicense in this.spdxLicenses)
            {
                if (spdxLicense.Equals(expression, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                } // if
            } // foreach

            return false;
        } // IsSpdxIdentifier()

        /// <summary>
        /// Determines whether this is a SPDX exception.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if this is a SPDX exception; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSpdxException(string expression)
        {
            foreach (var spdxLicense in this.spdxExceptions)
            {
                if (spdxLicense.Equals(expression, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                } // if
            } // foreach

            return false;
        } // IsSpdxException()

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseNoExpression()
        {
            SpdxExpressionParser.Parse(null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseNoExpressionChecker()
        {
            SpdxExpressionParser.Parse("MIT", null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestParseNoExceptionChecker()
        {
            SpdxExpressionParser.Parse("MIT", this.IsSpdxIdentifier, null);
        }

        [TestMethod]
        [ExpectedException(typeof(SpdxExpressionException))]
        public void TestEmptyExpression()
        {
            SpdxExpressionParser.Parse(
                " ", this.IsSpdxIdentifier, this.IsSpdxException);
        }

        [TestMethod]
        public void TestSimple()
        {
            foreach (var spdxLicense in this.spdxLicenses)
            {
                var actual = SpdxExpressionParser.Parse(
                    spdxLicense, this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(SpdxLicenseExpression));
                var license = actual as SpdxLicenseExpression;
                Assert.IsNotNull(license);
                Assert.AreEqual(spdxLicense, license.Id);
                Assert.AreEqual(spdxLicense, actual.ToString());
            } // foreach
        }

        [TestMethod]
        public void TestSimpleAnd()
        {
            var actual = SpdxExpressionParser.Parse(
                "MIT and Apache-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(SpdxAndExpression));
            var expr = actual as SpdxAndExpression;
            Assert.IsNotNull(expr);
            var left = expr.Left as SpdxLicenseExpression;
            Assert.IsNotNull(left);
            var right = expr.Right as SpdxLicenseExpression;
            Assert.IsNotNull(right);
            Assert.AreEqual("MIT", left.Id);
            Assert.AreEqual("Apache-2.0", right.Id);
            Assert.AreEqual("MIT AND Apache-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestMultiAnd()
        {
            var actual = SpdxExpressionParser.Parse(
                "MIT and ISC and Apache-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(SpdxAndExpression));
            var expr = actual as SpdxAndExpression;
            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr.Left, typeof(SpdxAndExpression));
            var right1 = expr.Right as SpdxLicenseExpression;
            Assert.IsNotNull(right1);

            var expr2 = expr.Left as SpdxAndExpression;
            Assert.IsNotNull(expr2);
            var left2 = expr2.Left as SpdxLicenseExpression;
            Assert.IsNotNull(left2);
            var right2 = expr2.Right as SpdxLicenseExpression;
            Assert.IsNotNull(right2);

            Assert.AreEqual("MIT", left2.Id);
            Assert.AreEqual("ISC", right2.Id);

            Assert.AreEqual("Apache-2.0", right1.Id);

            Assert.AreEqual("MIT AND ISC AND Apache-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestSimpleOr()
        {
            var actual = SpdxExpressionParser.Parse(
                "MIT OR Apache-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(SpdxOrExpression));
            var expr = actual as SpdxOrExpression;
            Assert.IsNotNull(expr);
            var left = expr.Left as SpdxLicenseExpression;
            Assert.IsNotNull(left);
            var right = expr.Right as SpdxLicenseExpression;
            Assert.IsNotNull(right);
            Assert.AreEqual("MIT", left.Id);
            Assert.AreEqual("Apache-2.0", right.Id);

            Assert.AreEqual("MIT OR Apache-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestMultiOr()
        {
            var actual = SpdxExpressionParser.Parse(
                "MIT OR Apache-2.0 or GPL-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(SpdxOrExpression));
            var expr = actual as SpdxOrExpression;
            Assert.IsNotNull(expr);
            var left1 = expr.Left as SpdxLicenseExpression;
            Assert.IsNotNull(left1);
            Assert.AreEqual("MIT", left1.Id);
            Assert.IsInstanceOfType(expr.Right, typeof(SpdxOrExpression));

            var expr2 = expr.Right as SpdxOrExpression;
            Assert.IsNotNull(expr2);
            var left2 = expr2.Left as SpdxLicenseExpression;
            Assert.IsNotNull(left2);
            var right2 = expr2.Right as SpdxLicenseExpression;
            Assert.IsNotNull(right2);

            Assert.AreEqual("Apache-2.0", left2.Id);
            Assert.AreEqual("GPL-2.0", right2.Id);

            Assert.AreEqual("MIT OR Apache-2.0 OR GPL-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestWith()
        {
            var actual = SpdxExpressionParser.Parse(
                "GPL-2.0 with Autoconf-exception-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxWithExpression));
            var expr = actual as SpdxWithExpression;
            Assert.IsNotNull(expr);
            Assert.AreEqual("Autoconf-exception-2.0", expr.Exception);

            var expr2 = expr.Expression as SpdxLicenseExpression;
            Assert.IsNotNull(expr2);
            Assert.AreEqual("GPL-2.0", expr2.Id);

            Assert.AreEqual("GPL-2.0 WITH Autoconf-exception-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestOrLaterOld()
        {
            var actual = SpdxExpressionParser.Parse(
                "GPL-2.0+", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxLicenseExpression));
            var expr = actual as SpdxLicenseExpression;
            Assert.IsNotNull(expr);
            Assert.AreEqual("GPL-2.0", expr.Id);
            Assert.IsTrue(expr.OrLater);
            Assert.AreEqual("GPL-2.0+", actual.ToString());
        }

        [TestMethod]
        public void TestOrLaterNew()
        {
            var actual = SpdxExpressionParser.Parse(
                "GPL-2.0-or-later", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxLicenseExpression));
            var expr = actual as SpdxLicenseExpression;
            Assert.IsNotNull(expr);
            Assert.AreEqual("GPL-2.0-or-later", expr.Id);

            // YES, the flag is false. Rationale is that "GPL-2.0-or-later" is the official SPDX identifier
            Assert.IsFalse(expr.OrLater);
            Assert.AreEqual("GPL-2.0-or-later", actual.ToString());
        }

        [TestMethod]
        public void TestSimpleLicenseRef()
        {
            const string ThisRef = "LicenseRef-siemens-DOM4J";
            var actual = SpdxExpressionParser.Parse(
                ThisRef, this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxLicenseReference));
            var expr = actual as SpdxLicenseReference;
            Assert.IsNotNull(expr);
            Assert.AreEqual(ThisRef, expr.LicenseRef);

            Assert.AreEqual("LicenseRef-siemens-DOM4J", actual.ToString());
        }

        [TestMethod]
        public void TestComplexExpressions()
        {
            var actual = SpdxExpressionParser.Parse(
                "GPL-2.0 with Autoconf-exception-2.0 and LicenseRef-siemens-DOM4J or Apache-2.0",
                this.IsSpdxIdentifier,
                this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxAndExpression));
            var expr = actual as SpdxAndExpression;
            Assert.IsNotNull(expr);
            Assert.IsInstanceOfType(expr.Left, typeof(SpdxWithExpression));
            var expr2 = expr.Left as SpdxWithExpression;
            Assert.IsNotNull(expr2);
            Assert.AreEqual("Autoconf-exception-2.0", expr2.Exception);
            Assert.IsInstanceOfType(expr2.Expression, typeof(SpdxLicenseExpression));
            Assert.AreEqual("GPL-2.0", ((SpdxLicenseExpression)expr2.Expression).Id);

            Assert.IsInstanceOfType(expr.Right, typeof(SpdxOrExpression));
            var expr3 = expr.Right as SpdxOrExpression;
            Assert.IsNotNull(expr3);
            Assert.IsInstanceOfType(expr3.Left, typeof(SpdxLicenseReference));
            Assert.IsInstanceOfType(expr3.Right, typeof(SpdxLicenseExpression));
            Assert.AreEqual("LicenseRef-siemens-DOM4J", ((SpdxLicenseReference)expr3.Left).LicenseRef);
            Assert.AreEqual("Apache-2.0", ((SpdxLicenseExpression)expr3.Right).Id);

            Assert.AreEqual("GPL-2.0 WITH Autoconf-exception-2.0 AND LicenseRef-siemens-DOM4J OR Apache-2.0", actual.ToString());
        }

        [TestMethod]
        public void TestSimpleScopedExpression()
        {
            const string Expr = "Apache-2.0";
            var actual = SpdxExpressionParser.Parse(
                $"({Expr})", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxScopedExpression));
            var expr = actual as SpdxScopedExpression;
            Assert.IsNotNull(expr);

            var expr2 = expr.Expression as SpdxLicenseExpression;
            Assert.IsNotNull(expr2);
            Assert.AreEqual(Expr, expr2.Id);

            Assert.AreEqual("(Apache-2.0)", actual.ToString());
        }

        [TestMethod]
        public void TestScopedExpressionNoClosingParenthesis()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "(MIT", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.Fail("We must not arrive here!");
            }
            catch (SpdxExpressionException spex)
            {
                Assert.AreEqual("Unexpected end of expression.", spex.Message);
            }
        }

        [TestMethod]
        public void TestComplexScopedExpression1()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT) or (ISC)", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsInstanceOfType(actual, typeof(SpdxOrExpression));
            var expr = actual as SpdxOrExpression;
            Assert.IsNotNull(expr);

            var left = expr.Left as SpdxScopedExpression;
            Assert.IsNotNull(left);
            var right = expr.Right as SpdxScopedExpression;
            Assert.IsNotNull(right);

            Assert.AreEqual("MIT", ((SpdxLicenseExpression)left.Expression).Id);
            Assert.AreEqual("ISC", ((SpdxLicenseExpression)right.Expression).Id);

            Assert.AreEqual("(MIT) OR (ISC)", actual.ToString());
        }

        [TestMethod]
        public void TestComplexScopedExpression2()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT or GPL-2.0)", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.AreEqual("(MIT OR GPL-2.0)", actual.ToString());
        }

        [TestMethod]
        public void TestComplexScopedExpression3()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT and GPL-2.0)", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.AreEqual("(MIT AND GPL-2.0)", actual.ToString());
        }

        [TestMethod]
        public void TestComplexScopedExpression4()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT or GPL-2.0) and ISC", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.AreEqual("(MIT OR GPL-2.0) AND ISC", actual.ToString());
        }

        [TestMethod]
        public void TestComplexScopedExpression5()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT or GPL-2.0) and (ISC or MPL-1.0)", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.AreEqual("(MIT OR GPL-2.0) AND (ISC OR MPL-1.0)", actual.ToString());
        }

        [TestMethod]
        public void TestComplexScopedExpression6()
        {
            var actual = SpdxExpressionParser.Parse(
                "(MIT or (GPL-2.0 and ISC))", this.IsSpdxIdentifier, this.IsSpdxException);
            Assert.IsNotNull(actual);
            Assert.AreEqual("(MIT OR (GPL-2.0 AND ISC))", actual.ToString());
        }

        [TestMethod]
        public void TestInvalidExpressionsUnknownLicenseId()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "XXX-2.0+", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.IsTrue(false, "Exception was not caught!");
            }
            catch (SpdxExpressionException ex)
            {
                Assert.AreEqual("Invalid/unknown SPDX license id", ex.Message);
            }

            var actual2 = SpdxExpressionParser.Parse(
                "XXX-2.0+", this.IsSpdxIdentifier, this.IsSpdxException,
                SpdxParsingOptions.AllowUnknownLicenses);
            Assert.IsNotNull(actual2);
            Assert.IsInstanceOfType(actual2, typeof(SpdxLicenseExpression));
            Assert.AreEqual("XXX-2.0", ((SpdxLicenseExpression)actual2).Id);
        }

        [TestMethod]
        public void TestInvalidExpressionsUnknownLicenseId2()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "GLP-2.0", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.IsTrue(false, "Exception was not caught!");
            }
            catch (SpdxExpressionException ex)
            {
                Assert.AreEqual("Unknown token: GLP-2.0", ex.Message);
            }
        }

        [TestMethod]
        public void TestInvalidExpressionsUnknownLicenseId3()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "GPL_2.0", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.IsTrue(false, "Exception was not caught!");
            }
            catch (SpdxExpressionException ex)
            {
                Assert.AreEqual("Invalid characters found", ex.Message);
            }
        }

        [TestMethod]
        public void TestInvalidExpressionsUnknownLicenseException()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "GPL-2.0 with linking-exception", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.IsTrue(false, "Exception was not caught!");
            }
            catch (SpdxExpressionException ex)
            {
                Assert.AreEqual("Unknown token: linking-exception", ex.Message);
            }

            var actual2 = SpdxExpressionParser.Parse(
                "GPL-2.0 with linking-exception",
                this.IsSpdxIdentifier, this.IsSpdxException,
                SpdxParsingOptions.AllowUnknownExceptions);
            Assert.IsInstanceOfType(actual2, typeof(SpdxWithExpression));
            var expr = actual2 as SpdxWithExpression;
            Assert.IsNotNull(expr);
            Assert.AreEqual("linking-exception", expr.Exception);
            Assert.AreEqual("GPL-2.0", ((SpdxLicenseExpression)expr.Expression).Id);
        }

        [TestMethod]
        public void TestInvalidCharactersFound()
        {
            try
            {
                SpdxExpressionParser.Parse(
                    "GPL_2.0", this.IsSpdxIdentifier, this.IsSpdxException);
                Assert.Fail("Exception was not caught!");
            }
            catch (SpdxExpressionException spex)
            {
                Assert.AreEqual("Invalid characters found", spex.Message);
            }
        }
    }
}