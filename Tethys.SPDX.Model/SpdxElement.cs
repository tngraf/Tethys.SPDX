// ---------------------------------------------------------------------------
// <copyright file="SpdxElement.cs" company="Tethys">
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
    using System.Collections.Generic;

    using Newtonsoft.Json;

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
    public class SpdxElement
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The annotations.
        /// </summary>
        private List<Annotation> annotations;

        /// <summary>
        /// The relation ships.
        /// </summary>
        private List<RelationShip> relationShips;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the OPTIONAL annotations.
        /// </summary>
        [JsonProperty("annotations")]
        public IReadOnlyList<Annotation> Annotations => this.annotations;

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
        /// Gets the relation ships.
        /// </summary>
        [JsonProperty("relationships")]
        public IReadOnlyList<RelationShip> RelationShips => this.relationShips;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("SPDXID")]
        public string SpdxIdentifier { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxElement"/> class.
        /// </summary>
        public SpdxElement()
        {
            // initialize all lists with null so that they are not JSON serialized
            this.annotations = null;
            this.relationShips = null;
        } // SpdxElement()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds an annotation.
        /// </summary>
        /// <param name="annotation">The annotation.</param>
        public void AddAnnotation(Annotation annotation)
        {
            this.annotations = this.annotations ?? new List<Annotation>();
            this.annotations.Add(annotation);
        } // AddAnnotation()

        /// <summary>
        /// Sets the annotations.
        /// </summary>
        /// <param name="newAnnotations">The new annotations.</param>
        public void SetAnnotations(IEnumerable<Annotation> newAnnotations)
        {
            this.annotations = new List<Annotation>(newAnnotations);
        } // SetAnnotations()

        /// <summary>
        /// Adds a relation ship.
        /// </summary>
        /// <param name="relation">The relation.</param>
        public void AddRelationShip(RelationShip relation)
        {
            this.relationShips = this.relationShips ?? new List<RelationShip>();
            this.relationShips.Add(relation);
        } // AddRelationShip()

        /// <summary>
        /// Sets the relation ships.
        /// </summary>
        /// <param name="relations">The relations.</param>
        public void SetRelationShips(IEnumerable<RelationShip> relations)
        {
            this.relationShips = new List<RelationShip>(relations);
        } // SetRelationShips()

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name}, {this.SpdxIdentifier}, #{this.RelationShips.Count}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxElement
}
