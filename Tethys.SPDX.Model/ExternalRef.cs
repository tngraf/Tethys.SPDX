// ---------------------------------------------------------------------------
// <copyright file="ExternalRef.cs" company="Tethys">
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
    using Newtonsoft.Json;

    /// <summary>
    /// An External Reference allows a Package to reference an external source of
    /// additional information, metadata, enumerations, asset identifiers, or
    /// downloadable content believed to be relevant to the Package.
    /// </summary>
    public class ExternalRef
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the reference category.
        /// </summary>
        [JsonProperty("referenceCategory")]
        [JsonConverter(typeof(ReferenceCategoryConverter))]
        public ReferenceCategory ReferenceCategory { get; set; }

        /// <summary>
        /// Gets or sets the type of the reference.
        /// </summary>
        [JsonProperty("referenceType")]
        ////[JsonConverter(typeof(ReferenceTypeConverter))]
        public string ReferenceType { get; set; }

        /// <summary>
        /// Gets or sets the locator of the reference.
        /// </summary>
        [JsonProperty("referenceLocator")]
        public string ReferenceLocator { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // ExternalRef
}
