// ---------------------------------------------------------------------------
// <copyright file="IKnownLicenseManager.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
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

namespace Tethys.SPDX.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for license managers, i.e. implementations that provide lists
    /// of SPDX license information.
    /// </summary>
    public interface IKnownLicenseManager
    {
        /// <summary>
        /// Gets the licenses.
        /// </summary>
        IReadOnlyList<ISpdxLicenseInfo> Licenses { get; }

        /// <summary>
        /// Loads a SPDX license source files.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        void LoadSpdxSourceFiles(string folderName);

        /// <summary>
        /// Loads the SPDX license list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The <see cref="ISpdxLicenseListInfo"/> license list.</returns>
        ISpdxLicenseListInfo LoadSpdxLicenseList(string fileName);

        /// <summary>
        /// Loads the SPDX license list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The <see cref="ISpdxExceptionList"/> license list.</returns>
        ISpdxExceptionList LoadSpdxExceptionList(string fileName);
    }
}