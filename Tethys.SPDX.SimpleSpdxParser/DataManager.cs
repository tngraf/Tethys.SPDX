// ---------------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Tethys">
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
    using Tethys.Logging;
    using Tethys.SPDX.ExpressionParser;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;
    using SpdxParsingOptions = Tethys.SPDX.Model.SpdxParsingOptions;

    /// <summary>
    /// Implement a manager for SPDX data required for reading SPDX documents.
    /// </summary>
    public class DataManager : IDataManager
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataManager));

        /// <summary>
        /// The known SPDX elements.
        /// </summary>
        private readonly Dictionary<string, SpdxElement> knownSpdxElements;

        /// <summary>
        /// The known license manager.
        /// </summary>
        private readonly KnownLicenseManager knownLicenseManager;

        /// <summary>
        /// The list of known licenses of this SPDX document.
        /// </summary>
        private readonly Dictionary<string, AnyLicenseInfo> knownDocumentLicenses;

        /// <summary>
        /// The list of listed licenses of this SPDX document.
        /// </summary>
        private readonly Dictionary<string, AnyLicenseInfo> listedLicenses;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the known document licenses.
        /// </summary>
        public IReadOnlyDictionary<string, AnyLicenseInfo> KnownDocumentLicenses => this.knownDocumentLicenses;

        /// <summary>
        /// Gets the known document licenses.
        /// </summary>
        public IReadOnlyDictionary<string, AnyLicenseInfo> ListedLicenses => this.listedLicenses;

        /// <summary>
        /// Gets the SPDX parsing options.
        /// </summary>
        public SpdxParsingOptions SpdxParsingOptions { get; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="DataManager" /> class.
        /// </summary>
        /// <param name="licenseManager">The license manager.</param>
        /// <param name="options">The options.</param>
        public DataManager(KnownLicenseManager licenseManager, SpdxParsingOptions options = null)
        {
            this.knownSpdxElements = new Dictionary<string, SpdxElement>();
            this.knownLicenseManager = licenseManager;
            this.knownDocumentLicenses = new Dictionary<string, AnyLicenseInfo>();
            this.listedLicenses = new Dictionary<string, AnyLicenseInfo>();
            this.SpdxParsingOptions = options ?? new SpdxParsingOptions();
            // if
        } // DataManager()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Finds the license by identifier.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <returns>A <see cref="AnyLicenseInfo"/> object or null.</returns>
        public License FindLicenseById(string licenseId)
        {
            foreach (var licenseInfo in this.knownLicenseManager.Licenses)
            {
                if (licenseInfo.LicenseId == licenseId)
                {
                    var license = LicenseInfoFactory.LicenseFromLicenseInfo(licenseInfo);
                    return license;
                } // if
            } // foreach

            return null;
        } // FindLicenseById()

        /// <summary>
        /// Parses an SPDX expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="SpdxExpression"/> object.</returns>
        public SpdxExpression ParseSpdxExpression(string expression)
        {
            var parsed = SpdxExpressionParser.Parse(
                expression,
                this.IsSpdxIdentifier,
                this.IsSpdxException);
            return parsed;
        } // ParseSpdxExpression()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PROTECTED METHODS
        #endregion // PROTECTED METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Determines whether this is a SPDX identifier.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if this is a SPDX identifier; otherwise, <c>false</c>.
        /// </returns>
        private bool IsSpdxIdentifier(string expression)
        {
            foreach (var spdxLicense in this.knownLicenseManager.Licenses)
            {
                if (spdxLicense.LicenseId.Equals(expression, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                } // if
            } // foreach

            return false;
        } // IsSpdxIdentifier()

        /// <summary>
        /// Determines whether this is a SPDX exception.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if this is a SPDX exception; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSpdxException(string expression)
        {
            foreach (var spdxException in this.knownLicenseManager.Exceptions)
            {
                if (spdxException.LicenseExceptionId.Equals(expression, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                } // if
            } // foreach

            return false;
        } // IsSpdxException()
        #endregion // PRIVATE METHODS
    } // DataManager
}
