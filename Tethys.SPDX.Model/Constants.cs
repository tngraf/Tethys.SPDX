// ---------------------------------------------------------------------------
// <copyright file="Constants.cs" company="Tethys">
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
    /// Global SPDX constant values.
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The no assertion statement.
        /// For licenses if
        /// * the SPDX document creator has attempted to but cannot reach
        ///   a reasonable objective determination.
        /// * the SPDX document creator has made no attempt to determine this field; or
        /// * the SPDX document creator has intentionally provided no information
        ///   (no meaning should be implied by doing so).
        /// </summary>
        public const string NoAssertion = "NOASSERTION";

        /// <summary>
        /// The none value.
        /// For licenses
        /// *  if the SPDX document creator concludes there is no license available for this package.
        /// </summary>
        public const string None = "NONE";

        /// <summary>
        /// The SPDX reference prefix.
        /// </summary>
        public const string SpdxRefPrefix = "SPDXRef-";
    }
}
