// ---------------------------------------------------------------------------
// <copyright file="AnnotationType.cs" company="Tethys">
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
    /// Type of the annotation.
    /// </summary>
    public enum AnnotationType
    {
        /// <summary>
        /// Type of annotation which does not fit in any of the pre-defined annotation types.
        /// </summary>
        Other,

        /// <summary>
        /// A Review represents an audit and signoff by an individual, organization or tool on the information for an SpdxElement.
        /// </summary>
        Review,
    }
}
