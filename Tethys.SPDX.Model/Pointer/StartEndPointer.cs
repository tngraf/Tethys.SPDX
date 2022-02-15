// ---------------------------------------------------------------------------
// <copyright file="StartEndPointer.cs" company="Tethys">
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
    using System.Text;

    /// <summary>
    /// A compound pointer pointing out parts of a document by means of a range delimited by a pair of
    /// single pointers that define the start point and the end point.
    /// See <see href="http://www.w3.org/2009/pointers"/> and
    /// <see href="https://www.w3.org/WAI/ER/Pointers/WD-Pointers-in-RDF10-20110427"/>.
    /// </summary>
    /// <seealso cref="CompoundPointer" />
    public class StartEndPointer : CompoundPointer
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the end pointer.
        /// </summary>
        public SinglePointer EndPointer { get; set; }
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
            var sb = new StringBuilder("From: ");
            if (this.StartPointer != null)
            {
                sb.Append(this.StartPointer);
            }
            else
            {
                sb.Append("[UNKNOWN]");
            } // if

            sb.Append(" To: ");
            if (this.EndPointer != null)
            {
                sb.Append(this.EndPointer);
            }
            else
            {
                sb.Append("[UNKNOWN]");
            } // if

            return sb.ToString();
        } // ToString()
        #endregion // PUBLIC METHODS
    } // StartEndPointer
}
