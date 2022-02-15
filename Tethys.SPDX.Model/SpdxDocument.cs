// ---------------------------------------------------------------------------
// <copyright file="SpdxDocument.cs" company="Tethys">
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
    using System.Collections.Generic;

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
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the creation information.
        /// </summary>
        public SpdxCreatorInformation CreationInfo { get; set; }

        /// <summary>
        /// Gets or sets the data license.
        /// </summary>
        public AnyLicenseInfo DataLicense { get; set; }

        /// <summary>
        /// Gets the external document refs.
        /// </summary>
        public IReadOnlyList<ExternalDocumentRef> ExternalDocumentRefs => this.externalDocumentRefs;

        /// <summary>
        /// Gets the extracted license information.
        /// </summary>
        public IReadOnlyList<ExtractedLicenseInfo> ExtractedLicenseInfos => this.extractedLicenseInfos;

        /// <summary>
        /// Gets or sets the spec version.
        /// </summary>
        public string SpecVersion { get; set; }

        /// <summary>
        /// Gets the newReviewers.
        /// </summary>
        public IReadOnlyList<Review> Reviewers => this.reviewers;

        /// <summary>
        /// Gets or sets the SPDX document namespace.
        /// </summary>
        public string SpdxDocumentNamespace { get; set; }

        /// <summary>
        /// Gets or sets the (optional) snippet information.
        /// </summary>
        public SpdxSnippet Snippet { get; set; }
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
        public IReadOnlyList<SpdxItem> GetFileFromItems(IEnumerable<SpdxItem> items)
        {
            return null;
        } // GetFileFromItems()

        /// <summary>
        /// Gets the document describes.
        /// </summary>
        /// <returns>A list of <see cref="SpdxItem"/> objects.</returns>
        public IReadOnlyList<SpdxItem> GetDocumentDescribes()
        {
            var result = new List<SpdxItem>();

            foreach (var relationShip in this.RelationShips)
            {
                if ((relationShip.Type == RelationshipType.DESCRIBES)
                    && (relationShip.ReleatedElement is SpdxItem))
                {
                    result.Add((SpdxItem)relationShip.ReleatedElement);
                } // foreach
            } // foreach

            return result;
        } // GetDocumentDescribes()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // SpdxDocument
}
