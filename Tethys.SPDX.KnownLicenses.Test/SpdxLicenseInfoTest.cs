// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseInfoTest.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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

namespace Tethys.SPDX.KnownLicenses.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Unit tests.
    /// </summary>
    [TestClass]
    public class SpdxLicenseInfoTest
    {
        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestToString()
        {
            // this is just to have a better code coverage
            var obj = new SpdxLicenseInfo();
            var actual = obj.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(actual));
        }
    }
}
