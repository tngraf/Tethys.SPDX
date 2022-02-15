// ---------------------------------------------------------------------------
// <copyright file="KnownLicenseManagerTest.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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
    public class KnownLicenseManagerTest
    {
        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestLoadSpdxSourceFilesSuccess()
        {
            var mgr = new KnownLicenseManager();
            Assert.AreEqual(0, mgr.Licenses.Count);
            mgr.LoadSpdxSourceFiles(@"..\..\..\..\license-list-data\details");
            Assert.IsTrue(mgr.Licenses.Count > 10);
        }

        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestLoadSpdxLicenseListSuccess()
        {
            var mgr = new KnownLicenseManager();
            var list = mgr.LoadSpdxLicenseList(
                @"..\..\..\..\license-list-data\licenses.json");
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Licenses.Count > 400);
        }

        /// <summary>
        /// Unit tests.
        /// </summary>
        [TestMethod]
        public void TestLoadSpdxLicenseExceptionListSuccess()
        {
            var mgr = new KnownLicenseManager();
            var list = mgr.LoadSpdxExceptionList(
                @"..\..\..\..\license-list-data\exceptions.json");
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Exceptions.Count > 30);
        }
    }
}
