// ---------------------------------------------------------------------------
// <copyright file="Annotation.cs" company="Tethys">
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
    /// An Annotation is a comment on an SpdxItem by an agent.
    /// </summary>
    public class Annotation
    {
        #region PRIVATE PROPERTIES
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the annotator.
        /// </summary>
        /// <remarks>
        /// This field identifies the person, organization or tool that has commented on a file, package, or entire document.
        /// </remarks>
        public string Annotator { get; set; }

        /// <summary>
        /// Gets or sets the type of the annotation.
        /// </summary>
        public AnnotationType AnnotationType { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public string Comment { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="Annotation"/> class.
        /// </summary>
        public Annotation()
        {
            this.AnnotationType = AnnotationType.Other;
        } // Annotation
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        #endregion // PUBLIC METHODS
    } // Annotation
}
