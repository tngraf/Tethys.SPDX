// ---------------------------------------------------------------------------
// <copyright file="ChecksumAlgorithm.cs" company="Tethys">
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
    /// <summary>
    /// Supported checksum algorithms.
    /// </summary>
    public enum ChecksumAlgorithm
    {
        /// <summary>
        /// The SHA1 algorithm.
        /// </summary>
        SHA1,

        /// <summary>
        /// The SHA224 algorithm.
        /// </summary>
        SHA224,

        /// <summary>
        /// The SHA256 algorithm.
        /// </summary>
        SHA256,

        /// <summary>
        /// The SHA384 algorithm.
        /// </summary>
        SHA384,

        /// <summary>
        /// The SHA512 algorithm.
        /// </summary>
        SHA512,

        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The BLAKE3 algorithm.
        /// </summary>
        BLAKE3,

        /// <summary>
        /// The ADLER32 algorithm.
        /// </summary>
        ADLER32,

        /// <summary>
        /// The SHA3-256 algorithm.
        /// </summary>
        SHA3_256,

        /// <summary>
        /// The SHA3-384 algorithm.
        /// </summary>
        SHA3_384,

        /// <summary>
        /// The SHA3-512 algorithm.
        /// </summary>
        SHA3_512,

        /// <summary>
        /// The BLAKE2b-256 algorithm.
        /// </summary>
        BLAKE2b_256,

        /// <summary>
        /// The BLAKE2b-384 algorithm.
        /// </summary>
        BLAKE2b_384,

        /// <summary>
        /// The BLAKE2b-512 algorithm.
        /// </summary>
        BLAKE2b_512,

        /// <summary>
        /// The MD2 algorithm.
        /// </summary>
        MD2,

        /// <summary>
        /// The MD4 algorithm.
        /// </summary>
        MD4,

        /// <summary>
        /// The MD5 algorithm.
        /// </summary>
        MD5,

        /// <summary>
        /// The MD6 algorithm.
        /// </summary>
        MD6,
        // ReSharper restore InconsistentNaming
    } // ChecksumAlgorithm
}
