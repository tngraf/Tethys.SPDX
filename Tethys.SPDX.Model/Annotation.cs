// ---------------------------------------------------------------------------
// <copyright file="Annotation.cs" company="Tethys">
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
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

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
        [JsonProperty("annotator")]
        public string Annotator { get; set; }

        /// <summary>
        /// Gets or sets the type of the annotation.
        /// </summary>
        [JsonProperty("annotationType")]
        [JsonConverter(typeof(StringUppercaseEnumConverter))]
        public AnnotationType AnnotationType { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [JsonProperty("annotationDate")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonProperty("comment")]
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
