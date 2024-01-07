// ---------------------------------------------------------------------------
// <copyright file="SpdxPackageVerificationCode.cs" company="Tethys">
//   Copyright (C) 2018-2024 T. Graf
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

namespace Tethys.SPDX.Model
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Contains an SPDX Package Verification Code, currently consisting
    /// of a value and list of excluded files.
    /// </summary>
    public class SpdxPackageVerificationCode
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The excluded file names.
        /// </summary>
        private List<string> excludedFileNames;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the excluded file names.
        /// </summary>
        [JsonProperty("packageVerificationCodeExcludedFiles")]
        public IReadOnlyList<string> ExcludedFileNames => this.excludedFileNames;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("packageVerificationCodeValue")]
        public string Value { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxPackageVerificationCode"/> class.
        /// </summary>
        public SpdxPackageVerificationCode()
        {
            this.excludedFileNames = new List<string>();
        } // SpdxPackageVerificationCode()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds the name of an excluded file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void AddExcludedFileName(string fileName)
        {
            this.excludedFileNames.Add(fileName);
        } // AddExcludedFileName()

        /// <summary>
        /// Sets the excluded file names.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        public void SetExcludedFileNames(IEnumerable<string> fileNames)
        {
            this.excludedFileNames = new List<string>(fileNames);
        } // SetExcludedFileNames()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxPackageVerificationCode
}
