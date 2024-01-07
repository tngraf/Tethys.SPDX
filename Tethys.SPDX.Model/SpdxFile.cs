// ---------------------------------------------------------------------------
// <copyright file="SpdxFile.cs" company="Tethys">
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
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// A File represents a named sequence of information
    /// that is contained in a software package.
    /// </summary>
    public class SpdxFile : SpdxItem
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The file types.
        /// </summary>
        private List<FileType> filetypes;

        /// <summary>
        /// The checksums.
        /// </summary>
        private List<Checksum> checksums;

        /// <summary>
        /// The file contributors.
        /// </summary>
        private List<string> fileContributors;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets the file types.
        /// </summary>
        [JsonProperty("fileTypes")]
        [JsonConverter(typeof(FileTypeConverter))]
        public IReadOnlyList<FileType> FileTypes => this.filetypes;

        /// <summary>
        /// Gets the checksums.
        /// </summary>
        [JsonProperty("checksums")]
        public IReadOnlyList<Checksum> Checksums => this.checksums;

        /// <summary>
        /// Gets the file contributors.
        /// </summary>
        [JsonProperty("fileContributors")]
        public IReadOnlyList<string> FileContributors => this.fileContributors;

        /// <summary>
        /// Gets or sets the notice text.
        /// </summary>
        [JsonProperty("noticeText")]
        public string NoticeText { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="SpdxFile"/> class.
        /// </summary>
        public SpdxFile()
        {
            this.filetypes = new List<FileType>();
            this.checksums = new List<Checksum>();
            this.fileContributors = new List<string>();
        } // SpdxFile()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Sets the file types.
        /// </summary>
        /// <param name="ftypes">The file types.</param>
        public void SetFileTypes(IEnumerable<FileType> ftypes)
        {
            this.filetypes = new List<FileType>(ftypes);
        } // SetFileTypes()

        /// <summary>
        /// Adds the type of the file.
        /// </summary>
        /// <param name="ftype">The file type.</param>
        public void AddFileType(FileType ftype)
        {
            this.filetypes.Add(ftype);
        } // AddFileType()

        /// <summary>
        /// Sets the checksums.
        /// </summary>
        /// <param name="chksums">The checksums.</param>
        public void SetChecksums(IEnumerable<Checksum> chksums)
        {
            this.checksums = new List<Checksum>(chksums);
        } // SetChecksums()

        /// <summary>
        /// Adds the checksum.
        /// </summary>
        /// <param name="chksum">The checksum.</param>
        public void AddChecksum(Checksum chksum)
        {
            this.checksums.Add(chksum);
        } // AddChecksum()

        /// <summary>
        /// Adds a file contributor.
        /// </summary>
        /// <param name="contributor">The contributor.</param>
        public void AddFileContributor(string contributor)
        {
            this.fileContributors.Add(contributor);
        } // AddFileContributor()

        /// <summary>
        /// Sets the file contributors.
        /// </summary>
        /// <param name="contributors">The contributors.</param>
        public void SetFileContributors(IEnumerable<string> contributors)
        {
            this.fileContributors = new List<string>(contributors);
        } // SetFileContributors()

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var license = this.LicenseConcluded;
            if ((license == null) && (this.LicenseInfoFromFiles != null))
            {
                license = this.LicenseInfoFromFiles.First();
            } // if

            var licenseHint = "(Unknown)";
            if (license != null)
            {
                licenseHint = license.ToString();
            } // if

            return $"{this.Name}, {this.SpdxIdentifier}, {licenseHint}";
        } // ToString()
        #endregion // PUBLIC METHODS
    } // SpdxFile
}
