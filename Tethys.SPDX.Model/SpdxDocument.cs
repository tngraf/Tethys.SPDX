// ---------------------------------------------------------------------------
// <copyright file="SpdxDocument.cs" company="Tethys">
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
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// An SpdxDocument is a summary of the contents, provenance, ownership and licensing
    /// analysis of a specific software package.
    /// This is, effectively, the top level of SPDX information.
    /// <p/>
    /// Documents always have a model.
    /// </summary>
    public class SpdxDocument : SpdxElement
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The external document references.
        /// </summary>
        private List<ExternalDocumentRef> externalDocumentRefs;

        /// <summary>
        /// The extracted license information.
        /// </summary>
        private List<ExtractedLicenseInfo> extractedLicenseInfos;

        /// <summary>
        /// The newReviewers.
        /// </summary>
        private List<Review> reviewers;

        /// <summary>
        /// The "document describes" list.
        /// </summary>
        private List<string> documentDescribes;

        /// <summary>
        /// The packages.
        /// </summary>
        private List<SpdxPackage> packages;

        /// <summary>
        /// The files.
        /// </summary>
        private List<SpdxFile> files;

        /// <summary>
        /// The snippets.
        /// </summary>
        private List<SpdxSnippet> snippets;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

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
        /// Gets the external document refs.
        /// </summary>
        [JsonProperty("externalDocumentRefs")]
        public IReadOnlyList<ExternalDocumentRef> ExternalDocumentRefs => this.externalDocumentRefs;

        /// <summary>
        /// Gets the extracted license information.
        /// </summary>
        [JsonProperty("hasExtractedLicenseInfos")]
        public IReadOnlyList<ExtractedLicenseInfo> ExtractedLicenseInfos => this.extractedLicenseInfos;

        /// <summary>
        /// Gets or sets the spec version.
        /// </summary>
        [JsonProperty("spdxVersion")]
        public string SpecVersion { get; set; }

        /// <summary>
        /// Gets the newReviewers.
        /// </summary>
        [JsonProperty("reviewers")]
        public IReadOnlyList<Review> Reviewers => this.reviewers;

        /// <summary>
        /// Gets or sets the SPDX document namespace.
        /// </summary>
        [JsonProperty("documentNamespace")]
        public string SpdxDocumentNamespace { get; set; }

        /// <summary>
        /// Gets the ids of the SPDX elements that are described by this document.
        /// </summary>
        [JsonProperty("documentDescribes")]
        public IReadOnlyList<string> DocumentDescribes => this.documentDescribes;

        /// <summary>
        /// Gets the packages of this SPDX document.
        /// </summary>
        [JsonProperty("packages")]
        public IReadOnlyList<SpdxPackage> Packages => this.packages;

        /// <summary>
        /// Gets the files of this SPDX document.
        /// </summary>
        [JsonProperty("files")]
        public IReadOnlyList<SpdxFile> Files => this.files;

        /// <summary>
        /// Gets the snippets of this SPDX document.
        /// </summary>
        [JsonProperty("snippets")]
        public IReadOnlyList<SpdxSnippet> Snippets => this.snippets;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxDocument" /> class.
        /// </summary>
        public SpdxDocument()
        {
            this.extractedLicenseInfos = new List<ExtractedLicenseInfo>();
            this.externalDocumentRefs = new List<ExternalDocumentRef>();
            this.reviewers = new List<Review>();
            this.documentDescribes = new List<string>();
            this.packages = new List<SpdxPackage>();
            this.files = new List<SpdxFile>();
            this.snippets = new List<SpdxSnippet>();
        } // SpdxDocument()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Adds a reviewer.
        /// </summary>
        /// <param name="reviewer">The reviewer.</param>
        public void AddReviewer(Review reviewer)
        {
            this.reviewers.Add(reviewer);
        } // AddReviewer()

        /// <summary>
        /// Sets the newReviewers.
        /// </summary>
        /// <param name="newReviewers">The new reviewers.</param>
        public void SetReviewers(IEnumerable<Review> newReviewers)
        {
            this.reviewers = new List<Review>(newReviewers);
        } // SetExternalDocumentRefs()

        /// <summary>
        /// Clears the reviewers.
        /// </summary>
        public void ClearReviewers()
        {
            this.reviewers.Clear();
        } // ClearReviewers()

        /// <summary>
        /// Adds a external document reference.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public void AddExternalDocumentRef(ExternalDocumentRef reference)
        {
            this.externalDocumentRefs.Add(reference);
        } // AddExternalDocumentRef()

        /// <summary>
        /// Sets the external document references.
        /// </summary>
        /// <param name="refs">The references.</param>
        public void SetExternalDocumentRefs(IEnumerable<ExternalDocumentRef> refs)
        {
            this.externalDocumentRefs = new List<ExternalDocumentRef>(refs);
        } // SetExternalDocumentRefs()

        /// <summary>
        /// Adds an extracted license information.
        /// </summary>
        /// <param name="info">The information.</param>
        public void AddExtractedLicenseInfo(ExtractedLicenseInfo info)
        {
            this.extractedLicenseInfos.Add(info);
        } // AddExtractedLicenseInfo()

        /// <summary>
        /// Sets the extracted license information.
        /// </summary>
        /// <param name="info">The information.</param>
        public void SetExtractedLicenseInfos(IEnumerable<ExtractedLicenseInfo> info)
        {
            this.extractedLicenseInfos = new List<ExtractedLicenseInfo>(info);
        } // SetExtractedLicenseInfos()

        /// <summary>
        /// Adds an item that is described by this document.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddDocumentDescribes(string item)
        {
            this.documentDescribes.Add(item);
        } // AddDocumentDescribes()

        /// <summary>
        /// Sets the new items.
        /// </summary>
        /// <param name="newItems">The new reviewers.</param>
        public void SetDocumentDescribes(IEnumerable<string> newItems)
        {
            this.documentDescribes = new List<string>(newItems);
        } // SetDocumentDescribes()

        /// <summary>
        /// Clears the described items.
        /// </summary>
        public void ClearDocumentDescribes()
        {
            this.documentDescribes.Clear();
        } // ClearDocumentDescribes()

        /// <summary>
        /// Adds a package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void AddPackage(SpdxPackage package)
        {
            this.packages.Add(package);
        } // AddPackage()

        /// <summary>
        /// Sets the new packages.
        /// </summary>
        /// <param name="newPackages">The new packages.</param>
        public void SetPackages(IEnumerable<SpdxPackage> newPackages)
        {
            this.packages = new List<SpdxPackage>(newPackages);
        } // SetPackages()

        /// <summary>
        /// Clears the packages.
        /// </summary>
        public void ClearPackages()
        {
            this.packages.Clear();
        } // ClearPackages()

        /// <summary>
        /// Adds a file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void AddFile(SpdxFile file)
        {
            this.files.Add(file);
        } // AddFiles()

        /// <summary>
        /// Sets the new files.
        /// </summary>
        /// <param name="newFiles">The new files.</param>
        public void SetFiles(IEnumerable<SpdxFile> newFiles)
        {
            this.files = new List<SpdxFile>(newFiles);
        } // SetFiles()

        /// <summary>
        /// Clears the files.
        /// </summary>
        public void ClearFiles()
        {
            this.files.Clear();
        } // ClearFiles()

        /// <summary>
        /// Adds a snippet.
        /// </summary>
        /// <param name="snippet">The snippet.</param>
        public void AddSnippet(SpdxSnippet snippet)
        {
            this.snippets.Add(snippet);
        } // AddSnippet()

        /// <summary>
        /// Sets the new snippets.
        /// </summary>
        /// <param name="newSnippets">The new reviewers.</param>
        public void SetSnippets(IEnumerable<SpdxSnippet> newSnippets)
        {
            this.snippets = new List<SpdxSnippet>(newSnippets);
        } // SetSnippets()

        /// <summary>
        /// Clears the snippets.
        /// </summary>
        public void ClearSnippets()
        {
            this.snippets.Clear();
        } // ClearSnippets()

        /// <summary>
        /// Gets the packages from items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>A list of <see cref="SpdxItem"/> objects.</returns>
        public IReadOnlyList<SpdxItem> GetPackagesFromItems(IEnumerable<SpdxItem> items)
        {
            return null;
        } // GetPackagesFromItems()

        /// <summary>
        /// Gets the files from items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>A list of <see cref="SpdxItem"/> objects.</returns>
        public IReadOnlyList<SpdxItem> GetFilesFromItems(IEnumerable<SpdxItem> items)
        {
            return null;
        } // GetFileFromItems()

        /// <summary>
        /// Gets the document describes.
        /// </summary>
        /// <returns>A list of <see cref="SpdxItem"/> objects.</returns>
        public IReadOnlyList<SpdxItem> GetDocumentDescribes()
        {
            return this.GetItems(RelationshipType.Describes);
        } // GetDocumentDescribes()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        /// <summary>
        /// Gets the items of a specific type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// A list of <see cref="SpdxItem" /> objects.
        /// </returns>
        public IReadOnlyList<SpdxItem> GetItems(RelationshipType type)
        {
            var result = new List<SpdxItem>();

            foreach (var relationShip in this.RelationShips)
            {
                if ((relationShip.Type == type)
                    && (relationShip.RelatedElement is SpdxItem item))
                {
                    result.Add(item);
                } // if
            } // foreach

            return result;
        } // GetItems()

        /// <summary>
        /// Gets the items of a specific type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// A list of <see cref="SpdxItem" /> objects.
        /// </returns>
        public IReadOnlyList<SpdxItem> GetItems(Type type)
        {
            var result = new List<SpdxItem>();

            foreach (var relationShip in this.RelationShips)
            {
                if (relationShip.RelatedElement is SpdxItem item && item.GetType() == type)
                {
                    result.Add(item);
                } // foreach
            } // foreach

            return result;
        } // GetItems()
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxDocument
}
