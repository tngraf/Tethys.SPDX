// ---------------------------------------------------------------------------
// <copyright file="ReferenceType.cs" company="Tethys">
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
    /// Type of external reference.
    /// </summary>
    public enum ReferenceType
    {
        /// <summary>
        /// A CPE 2.2 value.
        /// </summary>
        Cpe22Type,

        /// <summary>
        /// A CPE 2.3 value.
        /// </summary>
        Cpe23Type,

        /// <summary>
        /// A reference to the published security advisory
        /// </summary>
        Advisory,

        /// <summary>
        /// A reference to the source code with a fix for the vulnerability
        /// </summary>
        Fix,

        /// <summary>
        /// URL as defined by https://www.ietf.org/rfc/rfc1738.txt.
        /// </summary>
        Url,

        /// <summary>
        /// A SW-ID.
        /// </summary>
        SwId,

        /// <summary>
        /// Maven coordinates.
        /// </summary>
        MavenCentral,

        /// <summary>
        /// A NPM package.
        /// </summary>
        Npm,

        /// <summary>
        /// A NuGet package.
        /// </summary>
        NuGet,

        /// <summary>
        /// A Bower package.
        /// </summary>
        Bower,

        /// <summary>
        /// A package URL.
        /// </summary>
        Purl,

        /// <summary>
        /// A SoftWare Heritage persistent Identifier.
        /// </summary>
        Swh,

        /// <summary>
        /// A git object id.
        /// </summary>
        GitOid,

        /// <summary>
        /// Any kind of ID string.
        /// </summary>
        IdString,
    } // ReferenceType
}
