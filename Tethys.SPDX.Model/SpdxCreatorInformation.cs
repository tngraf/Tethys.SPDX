// ---------------------------------------------------------------------------
// <copyright file="SpdxCreatorInformation.cs" company="Tethys">
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
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Creator class for SPDX documents.
    /// </summary>
    public class SpdxCreatorInformation
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The creators.
        /// </summary>
        private List<string> creators;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the creators.
        /// </summary>
        [JsonProperty("creators")]
        public IReadOnlyList<string> Creators => this.creators;

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the license list version.
        /// </summary>
        [JsonProperty("licenseListVersion")]
        public string LicenseListVersion { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxCreatorInformation"/> class.
        /// </summary>
        public SpdxCreatorInformation()
        {
            this.creators = new List<string>();
        } // SpdxCreatorInformation()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds a creator.
        /// </summary>
        /// <param name="creator">The creator.</param>
        public void AddCreator(string creator)
        {
            this.creators.Add(creator);
        } // AddCreator()

        /// <summary>
        /// Sets the creators.
        /// </summary>
        /// <param name="creatorList">The creators.</param>
        public void SetCreators(IEnumerable<string> creatorList)
        {
            this.creators = new List<string>(creatorList);
        } // SetCreators()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxCreatorInformation
}
