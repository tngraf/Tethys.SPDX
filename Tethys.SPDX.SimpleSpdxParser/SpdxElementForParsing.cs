// ---------------------------------------------------------------------------
// <copyright file="SpdxElementForParsing.cs" company="Tethys">
//   Copyright (C) 2024 T. Graf
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

namespace Tethys.SPDX.SimpleSpdxParser
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Tethys.SPDX.Model;

    /// <summary>
    /// An SpdxElement is any thing described in SPDX, either a document or an SpdxItem.
    /// SpdxElements can be related to other SpdxElements.
    /// <p/>
    /// All subclasses should override getType, equals and hashCode.
    /// <p/>
    /// If a sub property is used for the name property name, getNamePropertyName should be overridden.
    /// <p/>
    /// If absolute URIs are required, getUri should be overridden.
    /// </summary>
    public class SpdxElementForParsing
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the OPTIONAL annotations.
        /// </summary>
        [JsonProperty("annotations")]
        public List<Annotation> Annotations { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the relation ships.
        /// </summary>
        [JsonProperty("relationships")]
        public List<RelationShip> RelationShips { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("SPDXID")]
        public string SpdxIdentifier { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxElementForParsing"/> class.
        /// </summary>
        public SpdxElementForParsing()
        {
        } // SpdxElementForParsing()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name}, {this.SpdxIdentifier}, #{this.RelationShips.Count}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxElementForParsing
}
