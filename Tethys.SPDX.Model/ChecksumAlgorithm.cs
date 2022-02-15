// ---------------------------------------------------------------------------
// <copyright file="ChecksumAlgorithm.cs" company="Tethys">
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
    /// Supported checksum algorithms.
    /// </summary>
    public enum ChecksumAlgorithm
    {
        /// <summary>
        /// The MD5 algorithm.
        /// </summary>
        MD5,

        /// <summary>
        /// The SHA1 algorithm.
        /// </summary>
        SHA1,

        /// <summary>
        /// The SHA256 algorithm.
        /// </summary>
        SHA256,
    } // ChecksumAlgorithm
}
