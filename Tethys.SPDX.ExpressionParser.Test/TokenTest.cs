// ---------------------------------------------------------------------------
// <copyright file="TokenTest.cs" company="Tethys">
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
    public class TokenTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTokenInvalidArgs()
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Token(TokenType.Exception, null);
        }

        [TestMethod]
        public void TestToString()
        {
            var token = new Token(TokenType.And, "AND");
            var actual = token.ToString();
            Assert.AreEqual("And: AND", actual);
        }
    }
}
