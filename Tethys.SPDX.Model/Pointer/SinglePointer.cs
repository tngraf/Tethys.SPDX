// ---------------------------------------------------------------------------
// <copyright file="SinglePointer.cs" company="Tethys">
//   Copyright (C) 2019-2024 T. Graf
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

namespace Tethys.SPDX.Model.Pointer
{
    using Newtonsoft.Json;

    /// <summary>
    /// A pointing method made up of a unique pointer. This is an abstract single pointer that
    /// provides the necessary framework, but it does not provide any kind of pointer, so more
    /// specific subclasses must be used.
    /// See <see href="http://www.w3.org/2009/pointers"/>
    /// and <see href="https://www.w3.org/WAI/ER/Pointers/WD-Pointers-in-RDF10-20110427"/>.
    /// </summary>
    public abstract class SinglePointer
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the document within which the pointer is applicable or meaningful.
        /// </summary>
        [JsonProperty("reference")]
        [JsonConverter(typeof(SpdxElementRefConverter))]
        public SpdxElement Reference { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // SinglePointer
}
