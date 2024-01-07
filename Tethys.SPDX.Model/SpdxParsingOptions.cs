// ---------------------------------------------------------------------------
// <copyright file="SpdxParsingOptions.cs" company="Tethys">
// Copyright (C) 2024 T. Graf
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
    /// <summary>
    /// Parsing options for SPDX documents.
    /// </summary>
    public class SpdxParsingOptions
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets a value indicating whether to load external documents.
        /// </summary>
        public bool LoadExternalDocuments { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxParsingOptions"/> class.
        /// </summary>
        public SpdxParsingOptions()
        {
            this.LoadExternalDocuments = false;
        } // SpdxParsingOptions()
        #endregion // CONSTRUCTION
    } // SpdxParsingOptions
}
