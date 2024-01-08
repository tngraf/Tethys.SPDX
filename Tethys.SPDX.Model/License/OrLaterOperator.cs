// ---------------------------------------------------------------------------
// <copyright file="OrLaterOperator.cs" company="Tethys">
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

namespace Tethys.SPDX.Model.License
{
    /// <summary>
    /// A License that has an or later operator (e.g. GPL-2.0+).
    /// </summary>
    public class OrLaterOperator
    {
        #region PRIVATE PROPERTIES
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the base License.
        /// </summary>
        public SimpleLicensingInfo License { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="OrLaterOperator"/> class.
        /// </summary>
        /// <param name="license">The License.</param>
        public OrLaterOperator(SimpleLicensingInfo license)
        {
            this.License = license;
        } // OrLaterOperator()
        #endregion // CONSTRUCTION

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
            if (this.License == null)
            {
                return "UNDEFINED OR EXCEPTION";
            } // if

            return $"{this.License.Id}+";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // OrLaterOperator
}
