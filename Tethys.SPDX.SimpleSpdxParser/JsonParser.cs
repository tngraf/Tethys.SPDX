// ---------------------------------------------------------------------------
// <copyright file="JsonParser.cs" company="Tethys">
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Newtonsoft.Json;
    using Tethys.Logging;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Support;

    /// <summary>
    /// Read SPDX documents in JSON format.
    /// </summary>
    public class JsonParser
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonParser));

        /// <summary>
        /// The known license manager.
        /// </summary>
        private readonly KnownLicenseManager knownLicenseManager;

        /// <summary>
        /// The data manager.
        /// </summary>
        private IDataManager dataManager;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the SPDX parsing options.
        /// </summary>
        public SpdxParsingOptions SpdxParsingOptions { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser" /> class.
        /// </summary>
        /// <param name="licenseManager">The license manager.</param>
        /// <param name="options">The options.</param>
        public JsonParser(KnownLicenseManager licenseManager, SpdxParsingOptions options = null)
        {
            this.knownLicenseManager = licenseManager;
            this.SpdxParsingOptions = options ?? new SpdxParsingOptions();
            this.dataManager = new DataManager(this.knownLicenseManager, options);
            JsonLicenseConverter.DataManager = this.dataManager;
            SpdxDocumentRefConverter.DataManager = this.dataManager;
        } // JsonParser()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Reads an SPDX document from the given file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A <see cref="SpdxDocument"/> object.</returns>
        public SpdxDocument ReadFromFile(string filename)
        {
            try
            {
                LicenseInfoFactory.KnownLicenseManager = this.knownLicenseManager;

                var encoding = IoSupport.GetEncoding(filename);
                if (encoding.Equals(Encoding.ASCII))
                {
                    // use default encoding for JSON files: UTF-8
                    encoding = Encoding.UTF8;
                } // if

                using var stream = File.OpenRead(filename);
                return this.ReadFromFile(stream, encoding);
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX JSON file", ex);
                throw;
            } // catch
        } // ReadFromFile()

        /// <summary>
        /// Reads SPDX data from the given file stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        /// <returns>A <see cref="SpdxDocument"/> object.</returns>
        /// </returns>
        public SpdxDocument ReadFromFile(Stream stream, Encoding encoding)
        {
            try
            {
                using var sr = new StreamReader(stream, encoding);
                return this.ReadFromString(sr.ReadToEnd());
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX JSON file", ex);
                throw;
            } // catch
        } // ReadFromFile()

        /// <summary>
        /// Reads SPDX data from the given file stream.
        /// </summary>
        /// <param name="fileContents">The file contents.</param>
        /// <returns>
        /// <returns>A <see cref="SpdxDocument"/> object.</returns>
        /// </returns>
        public SpdxDocument ReadFromString(string fileContents)
        {
            Log.Debug("Reading SPDXDocument from string...");

            var parsed = JsonConvert.DeserializeObject<SpdxDocumentForParsing>(fileContents);
            if (parsed == null)
            {
                throw new InvalidSpdxAnalysisException("Invalid SPDX JSON document");
            } // if

            var final = parsed.ToSpdxDocument();
            return final;
        } // ReadFromString()

        // public void ResolveRelationShips()

        /// <summary>
        /// Checks the consistency of the specific SPDX document.
        /// </summary>
        /// <param name="spdxDoc">The SPDX document.</param>
        /// <returns>A list with issues. If there are no issues, then the document passed the check.</returns>
        public IReadOnlyList<string> CheckConsistency(SpdxDocument spdxDoc)
        {
            var result = new List<string>();
            return result;
        } // CheckConsistency()

        /// <summary>
        /// Determines whether this is valid SPDX identifier.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if this is valid SPDX identifier; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidSpdxIdentifier(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            } // if

            if (!text.StartsWith(Constants.SpdxRefPrefix))
            {
                return false;
            } // if

            foreach (var ch in text)
            {
                if (char.IsLetterOrDigit(ch))
                {
                    continue;
                } // if

                if ((ch == '.') || (ch == '-'))
                {
                    continue;
                } // if

                return false;
            } // foreach

            return true;
        } // IsValidSpdxIdentifier()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        #endregion // PRIVATE METHODS
    } // JsonParser
}
