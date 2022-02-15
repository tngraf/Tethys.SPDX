// ---------------------------------------------------------------------------
// <copyright file="ByteOffsetPointer.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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
    /// <summary>
    /// The byte offset pointer.
    /// </summary>
    /// <seealso cref="SinglePointer" />
    public class ByteOffsetPointer : SinglePointer
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int Offset { get; set; }
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
            return "Byte offset " + this.Offset;
        } // ToString()
        #endregion // PUBLIC METHODS
    } // ByteOffsetPointer
}
