// ---------------------------------------------------------------------------
// <copyright file="SimpleLicensingInfo.cs" company="Tethys">
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

namespace Tethys.SPDX.Model.License
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The SimpleLicenseInfo class includes all resources that represent
    /// simple, atomic, licensing information.
    /// </summary>
    public class SimpleLicensingInfo : AnyLicenseInfo
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The see also elements.
        /// </summary>
        private List<string> seeAlso;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("licenseId")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string Comment { get; set; }

        /// <summary>
        /// Gets the 'see also' elements.
        /// </summary>
        [JsonProperty("seeAlsos")]
        public IReadOnlyList<string> SeeAlso => this.seeAlso;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleLicensingInfo"/> class.
        /// </summary>
        public SimpleLicensingInfo()
        {
            // initialize all lists with null so that they are not JSON serialized
            this.seeAlso = null;
        } // SimpleLicensingInfo()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds the see also.
        /// </summary>
        /// <param name="element">The element.</param>
        public void AddSeeAlso(string element)
        {
            this.seeAlso ??= new List<string>();
            this.seeAlso.Add(element);
        } // AddSeeAlso()

        /// <summary>
        /// Sets the 'see also' elements.
        /// </summary>
        /// <param name="elements">The 'see also' elements.</param>
        public void SetSeeAlso(IEnumerable<string> elements)
        {
            this.seeAlso = new List<string>(elements);
        } // SetSeeAlso()

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Name} ({this.Id}), {this.Comment}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SimpleLicensingInfo
}
