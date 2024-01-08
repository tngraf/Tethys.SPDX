// ---------------------------------------------------------------------------
// <copyright file="SpdxDocumentForParsing.cs" company="Tethys">
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
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// This is a special variant of <see cref="SpdxDocument"/> without
    /// read-only properties. This simplifies reading the information
    /// from JSON files.
    /// </summary>
    internal class SpdxDocumentForParsing : SpdxElement
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the creation information.
        /// </summary>
        [JsonProperty("creationInfo")]
        public SpdxCreatorInformation CreationInfo { get; set; }

        /// <summary>
        /// Gets or sets the data license.
        /// </summary>
        [JsonProperty("dataLicense")]
        [JsonConverter(typeof(JsonLicenseConverter))]
        public AnyLicenseInfo DataLicense { get; set; }

        /// <summary>
        /// Gets or sets the external document refs.
        /// </summary>
        [JsonProperty("externalDocumentRefs")]
        public List<ExternalDocumentRef> ExternalDocumentRefs { get; set; }

        /// <summary>
        /// Gets or sets the extracted license information.
        /// </summary>
        [JsonProperty("hasExtractedLicensingInfos")]
        public List<ExtractedLicenseInfo> ExtractedLicenseInfos { get; set; }

        /// <summary>
        /// Gets or sets the spec version.
        /// </summary>
        [JsonProperty("spdxVersion")]
        public string SpecVersion { get; set; }

        /// <summary>
        /// Gets or sets the SPDX document namespace.
        /// </summary>
        [JsonProperty("documentNamespace")]
        public string SpdxDocumentNamespace { get; set; }

        /// <summary>
        /// Gets or sets the ids of the SPDX elements that are described by this document.
        /// </summary>
        [JsonProperty("documentDescribes")]
        public List<string> DocumentDescribes { get; set; }

        /// <summary>
        /// Gets or sets the packages of this SPDX document.
        /// </summary>
        [JsonProperty("packages")]
        public List<SpdxPackageForParsing> Packages { get; set; }

        /// <summary>
        /// Gets or sets the files of this SPDX document.
        /// </summary>
        [JsonProperty("files")]
        public List<SpdxFileForParsing> Files { get; set; }

        /// <summary>
        /// Gets or sets the snippets of this SPDX document.
        /// </summary>
        [JsonProperty("snippets")]
        public List<SpdxSnippetForParsing> Snippets { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Creates the final SPDX document.
        /// </summary>
        /// <returns>A <see cref="SpdxDocument"/> object.</returns>
        public SpdxDocument ToSpdxDocument()
        {
            var result = new SpdxDocument();

            // SpdxElement properties
            result.SpdxIdentifier = this.SpdxIdentifier;
            result.Comment = this.Comment;
            result.Name = this.Name;
            if (this.Annotations != null)
            {
                result.SetAnnotations(this.Annotations);
            } // if

            // SpdxDocument properties
            result.SpdxDocumentNamespace = this.SpdxDocumentNamespace;
            result.CreationInfo = this.CreationInfo;
            result.DataLicense = this.DataLicense;
            result.SpecVersion = this.SpecVersion;

            if (this.ExtractedLicenseInfos != null)
            {
                result.SetExtractedLicenseInfos(this.ExtractedLicenseInfos);
            } // if

            if (this.DocumentDescribes != null)
            {
                result.SetDocumentDescribes(this.DocumentDescribes);
            } // if

            if (this.ExternalDocumentRefs != null)
            {
                result.SetExternalDocumentRefs(this.ExternalDocumentRefs);
            } // if

            if (this.Files != null)
            {
                // RelationShips, Files
                foreach (var file in this.Files)
                {
                    result.AddFile(file.ToSpdxFile());
                } // foreach
            } // if

            if (this.Packages != null)
            {
                // RelationShips, Packages
                foreach (var package in this.Packages)
                {
                    result.AddPackage(package.ToSpdxPackage());
                } // foreach
            } // if

            if (this.Snippets != null)
            {
                // RelationShips, Snippets
                foreach (var snippet in this.Snippets)
                {
                    result.AddSnippet(snippet.ToSpdxSnippet());
                } // foreach
            } // if

            if (this.RelationShips != null)
            {
                result.SetRelationShips(this.RelationShips);
            } // if

            if (this.Annotations != null)
            {
                result.SetAnnotations(this.Annotations);
            } // if

            return result;
        } // ToSpdxDocument()
        #endregion // PUBLIC METHODS
    } // SpdxDocumentForParsing
}
