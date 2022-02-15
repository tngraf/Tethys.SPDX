// ---------------------------------------------------------------------------
// <copyright file="ReferenceType.cs" company="Tethys">
//   Copyright (C) 2018 T. Graf
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
    using System;

    /// <summary>
    /// Type of external reference
    /// Note that there are very few required fields for this class in that
    /// the external reference type does not need to be provided in the SPDX
    /// document for the document to be valid.
    /// </summary>
    public class ReferenceType
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the contextual example.
        /// </summary>
        public string ContextualExample { get; set; }

        /// <summary>
        /// Gets or sets the documentation.
        /// </summary>
        public Uri Documentation { get; set; }

        /// <summary>
        /// Gets or sets the external reference site.
        /// </summary>
        public Uri ExternalReferenceSite { get; set; }

        /// <summary>
        /// Gets or sets the reference type URI.
        /// </summary>
        public Uri ReferenceTypeUri { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // ReferenceType
}
