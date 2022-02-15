// ---------------------------------------------------------------------------
// <copyright file="Review.cs" company="Tethys">
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
    using System;

    /// <summary>
    /// SPDX review information section.
    /// </summary>
    ////[Obsolete("This field has been deprecated since SPDX 2.0.")]
    public class Review
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// Gets or sets the reviewer.
        /// </summary>
        public string Reviewer { get; set; }

        /// <summary>
        /// Gets or sets the review date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Initializes a new instance of the <see cref="Review"/> class.
        /// </summary>
        public Review()
        {
            this.Date = DateTime.Now;
        } // Review()
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // Review
}
