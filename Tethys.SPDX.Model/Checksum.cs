// ---------------------------------------------------------------------------
// <copyright file="Checksum.cs" company="Tethys">
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
    using Newtonsoft.Json;

    /// <summary>
    /// SPDX Checksum class for packages and files.
    /// </summary>
    public class Checksum
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        [JsonProperty("algorithm")]
        [JsonConverter(typeof(ChecksumAlgorithmConverter))]
        public ChecksumAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("checksumValue")]
        public string Value { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"Alg = {this.Algorithm}: {this.Value}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxChecksum
}
