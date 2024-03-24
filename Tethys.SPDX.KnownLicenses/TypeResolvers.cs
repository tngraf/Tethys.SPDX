// ---------------------------------------------------------------------------
// <copyright file="TypeResolvers.cs" company="Tethys">
//   Copyright (C) 2024 T. Graf
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
    using System;
    using System.Text.Json.Serialization.Metadata;
    using Tethys.SPDX.Interfaces;

    /// <summary>
    /// Type resolvers to allow correct deserialization with System.Text.Json.
    /// </summary>
    public static class TypeResolvers
    {
        /// <summary>
        /// Gets the SPDX license list entry type resolver, i.e. tells System.Text.Json
        /// to deserialize a ISpdxLicenseListEntry as SpdxLicenseListEntry.
        /// </summary>
        /// <returns>A <see cref="JsonTypeInfo"/> action.</returns>
        public static Action<JsonTypeInfo> GetSpdxLicenseListEntryTypeResolvers()
        {
            return typeInfo =>
            {
                if (typeInfo.Type == typeof(ISpdxLicenseListEntry))
                {
                    typeInfo.CreateObject = () => new SpdxLicenseListEntry();
                }
            };
        } // GetSpdxLicenseListEntryTypeResolvers()

        /// <summary>
        /// Gets the SPDX license list entry type resolver, i.e. tells System.Text.Json
        /// to deserialize a ISpdxExceptionListEntry as SpdxExceptionListEntry.
        /// </summary>
        /// <returns>A <see cref="JsonTypeInfo"/> action.</returns>
        public static Action<JsonTypeInfo> GetSpdxExceptionListEntryTypeResolvers()
        {
            return typeInfo =>
            {
                if (typeInfo.Type == typeof(ISpdxExceptionListEntry))
                {
                    typeInfo.CreateObject = () => new SpdxExceptionListEntry();
                }
            };
        } // GetSpdxExceptionListEntryTypeResolvers()
    }
}
