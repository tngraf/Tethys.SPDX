// ---------------------------------------------------------------------------
// <copyright file="ExternalDocumentRef.cs" company="Tethys">
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
    /// <summary>
    /// Information about an external SPDX document reference including the checksum.
    /// This allows for verification of the external references.
    /// <p/>
    /// Since an SPDX document must be in its own container, there are a few special
    /// considerations for this class:
    /// - model, node, and resource are associated with the document making an external reference,
    ///   it does not include the actual document being referenced
    /// - This class can be used with only the URI for the external document being provided.It
    ///   does not require the entire document to be passed in.  The spdxDocument itself is optional.
    /// </summary>
    public class ExternalDocumentRef
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        public Checksum Checksum { get; set; }

        /// <summary>
        /// Gets or sets the SPDX document namespace.
        /// </summary>
        public string SpdxDocumentNamespace { get; set; }

        /// <summary>
        /// Gets or sets the external document identifier.
        /// </summary>
        public string ExternalDocumentId { get; set; }

        /// <summary>
        /// Gets or sets the SPDX document.
        /// </summary>
        public SpdxDocument SpdxDocument { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // ExternalDocumentRef
}
