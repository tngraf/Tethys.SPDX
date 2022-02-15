// ---------------------------------------------------------------------------
// <copyright file="RdfBaseItem.cs" company="Tethys">
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

namespace Tethys.SPDX.Model
{
    using System.Text;

    /// <summary>
    /// Implements base properties of a RDF item.
    /// </summary>
    /// <remarks>
    /// <see href="https://www.w3.org/TR/rdf-syntax-grammar/#coreSyntaxTerms"/>
    /// for details.
    /// </remarks>
    public class RdfBaseItem
    {
        #region PRIVATE PROPERTIES
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the description attribute.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the about attribute.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the identifier attribute.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the resource attribute.
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// Gets or sets the node identifier attribute.
        /// </summary>
        public string NodeId { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type of the data.
        /// </summary>
        public string DataType { get; set; }
        #endregion PUBLIC PROPERTIES

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
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(this.Id))
            {
                sb.Append($"ID={this.Id}");
            } // if

            if (!string.IsNullOrEmpty(this.NodeId))
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                } // if

                sb.Append($"NodeId={this.NodeId}");
            } // if

            if (!string.IsNullOrEmpty(this.Language))
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                } // if

                sb.Append($"lang={this.Language}");
            } // if

            if (!string.IsNullOrEmpty(this.DataType))
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                } // if

                sb.Append($"Type={this.DataType}");
            } // if

            if (!string.IsNullOrEmpty(this.About))
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                } // if

                sb.Append($"About={this.About}");
            } // if

            if (!string.IsNullOrEmpty(this.Resource))
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                } // if

                sb.Append($"Resource={this.Resource}");
            } // if

            return sb.ToString();
        } // ToString()
        #endregion // PUBLIC METHODS
    } // RdfBaseItem
}
