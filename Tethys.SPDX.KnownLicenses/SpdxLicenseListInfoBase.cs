// ---------------------------------------------------------------------------
// <copyright file="SpdxLicenseListInfoBase.cs" company="Tethys">
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

namespace Tethys.SPDX.KnownLicenses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Base class for SPDX license/exception lists.
    /// </summary>
    [JsonDerivedType(typeof(SpdxLicenseListInfoBase), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(SpdxLicenseListInfo), typeDiscriminator: "ListInfo")]
    [JsonDerivedType(typeof(SpdxExceptionList), typeDiscriminator: "ExceptionInfo")]
    public class SpdxLicenseListInfoBase
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the license list version.
        /// </summary>
        [JsonPropertyName("licenseListVersion")]
        public string LicenseListVersion { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        [JsonPropertyName("releaseDate")]
        public string ReleaseDate { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // SpdxLicenseListInfoBase
}
