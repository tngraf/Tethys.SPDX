// ---------------------------------------------------------------------------
// <copyright file="LicenseInfoFactory.cs" company="Tethys">
//   Copyright (C) 2019-2022 T. Graf
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

namespace Tethys.SimpleSpdxParser
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Xml.Linq;

    using Tethys.Logging;
    using Tethys.SPDX.Interfaces;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model.License;
    using Tethys.Xml;

    /// <summary>
    /// Create the appropriate SPDXLicenseInfo from the model and node provided.
    /// The appropriate SPDXLicenseInfo subclass object will be chosen based on
    /// the class (rdf type) of the node. If there is no rdf type, then the
    /// license ID is parsed to determine the type.
    /// </summary>
    public class LicenseInfoFactory // for testing
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The listed licenses prefix.
        /// </summary>
        private const string ListedLicensesPrefix = "http://spdx.org/licenses/";

        /// <summary>
        /// The non standard license identifier prefix.
        /// </summary>
        private const string NonStdLicenseIdPrenum = "LicenseRef-";

        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(LicenseInfoFactory));

        /// <summary>
        /// The member name.
        /// </summary>
        private static readonly XName MemberName;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the known licenses.
        /// </summary>
        /// <value>
        /// The known licenses.
        /// </value>
        public static KnownLicenseManager KnownLicenseManager { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="LicenseInfoFactory"/> class.
        /// </summary>
        static LicenseInfoFactory()
        {
            MemberName = RdfParser.SpdxNameSpace + "member";
        } // LicenseInfoFactory()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Creates from XML input.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="AnyLicenseInfo" /> object.
        /// </returns>
        public static AnyLicenseInfo CreateFromXml(XElement parent, RdfParser parser)
        {
            if (parent == null)
            {
                return new SpdxNoneLicense();
            } // if

            var license = (GetLicenseFromUri(parent)
                ?? GetLicenseFromId(parent, parser))
                ?? GetLicenseFromType(parent, parser)
                ?? GetLicenseFromListedLicense(parent, parser);
            if (license == null)
            {
                Log.Error("Error reading license: No ID associated with a license!");
                ////throw new InvalidSpdxAnalysisException("No ID associated with a license");
            } // if

            return license;
        } // CreateFromXml()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Gets the license information from a listed license.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>AnyLicenseInfo.</returns>
        private static AnyLicenseInfo GetLicenseFromListedLicense(XContainer parent, RdfParser parser)
        {
            var parent2 = XmlSupport.GetFirstSubNode(parent, "ListedLicense", false);
            if (parent2 != null)
            {
                var about = XmlSupport.GetAttributeValue(parent2, "about", false);
                if (about != null)
                {
                    var existing = parser.FindListedLicense(about);
                    if (existing != null)
                    {
                        return existing;
                    } // if
                } // if

                var info = new ListedLicenseInfo();
                info.Name = XmlSupport.GetFirstSubNodeValue(parent2, "name", false);
                info.Id = XmlSupport.GetFirstSubNodeValue(parent2, "licenseId", false);
                info.Text = XmlSupport.GetFirstSubNodeValue(parent2, "licenseText");
                parser.AddListedLicense(about, info);
                return info;
            } // if

            return null;
        } // GetLicenseFromListedLicense()

        /// <summary>
        /// Gets the type of the license from.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="AnyLicenseInfo" /> object or null.
        /// </returns>
        private static AnyLicenseInfo GetLicenseFromType(XContainer parent, RdfParser parser)
        {
            var parent2 = XmlSupport.GetFirstSubNode(parent, "ConjunctiveLicenseSet", false);
            if (parent2 != null)
            {
                return ReadConjunctiveLicenseSet(parent2, parser);
            } // if

            parent2 = XmlSupport.GetFirstSubNode(parent, "DisjunctiveLicenseSet", false);
            if (parent2 != null)
            {
                return ReadDisjunctiveLicenseSet(parent2, parser);
            } // if

            parent2 = XmlSupport.GetFirstSubNode(parent, "ExtractedLicensingInfo", false);
            if (parent2 != null)
            {
                return new SpdxNoneLicense();
            } // if

            parent2 = XmlSupport.GetFirstSubNode(parent, "License", false);
            if (parent2 != null)
            {
                return new SpdxNoneLicense();
            } // if

            parent2 = XmlSupport.GetFirstSubNode(parent, "OrLaterOperator", false);
            if (parent2 != null)
            {
                return new SpdxNoneLicense();
            } // if

            parent2 = XmlSupport.GetFirstSubNode(parent, "WithExceptionOperator", false);
            if (parent2 != null)
            {
                return new SpdxNoneLicense();
            } // if

            return null;
        } // GetLicenseFromType()

        /// <summary>
        /// Reads the conjunctive license set.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="AnyLicenseInfo" /> object or null.
        /// </returns>
        private static AnyLicenseInfo ReadConjunctiveLicenseSet(XElement parent, RdfParser parser)
        {
            var members = ReadLicenseSetMembers(parent, parser);
            var licenseSet = new ConjunctiveLicenseSet(members);
            return licenseSet;
        } // ReadConjunctiveLicenseSet()

        /// <summary>
        /// Reads the disjunctive license set.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="AnyLicenseInfo" /> object or null.
        /// </returns>
        private static AnyLicenseInfo ReadDisjunctiveLicenseSet(XElement parent, RdfParser parser)
        {
            var members = ReadLicenseSetMembers(parent, parser);
            var licenseSet = new DisjunctiveLicenseSet(members);
            return licenseSet;
        } // ReadDisjunctiveLicenseSet()

        /// <summary>
        /// Unescapes the given string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The converted string.</returns>
        private static string UnEscapeString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            } // if

            text = WebUtility.HtmlDecode(text);

            // force some replacements
            text = text.Replace("%2B", "+");

            return text;
        } // UnEscapeString()

        /// <summary>
        /// Reads the license members of a license set.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A list of licenses.
        /// </returns>
        private static IReadOnlyList<AnyLicenseInfo> ReadLicenseSetMembers(XElement parent, RdfParser parser)
        {
            var members = new List<AnyLicenseInfo>();

            var xMemberList = from xMember in parent.Elements(MemberName)
                              select xMember;
            foreach (var xMember in xMemberList)
            {
                var resource = XmlSupport.GetAttributeValue(xMember, "resource", false);
                if (!string.IsNullOrEmpty(resource))
                {
                    // this is a license already defined somewhere else
                    // it could by a standard SPDX license
                    resource = UnEscapeString(resource);
                    var member = (AnyLicenseInfo)GetLicenseFromKnownLicenseList(resource);
                    if (member != null)
                    {
                        members.Add(member);
                        continue;
                    } // if

                    // or a license reference inside this document
                    member = parser.FindKnownDocumentLicense(resource);
                    if (member != null)
                    {
                        members.Add(member);
                    } // if
                }
                else
                {
                    var member = parser.ReadLicense(xMember);
                    members.Add(member);
                }
            } // foreach

            return members;
        } // ReadLicenseSetMembers()

        /// <summary>
        /// Gets the license from the identifier.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="AnyLicenseInfo" /> object or null.
        /// </returns>
        private static AnyLicenseInfo GetLicenseFromId(XElement parent, RdfParser parser)
        {
            var subjectUri = XmlSupport.GetAttributeValue(parent, "about", false);
            if (string.IsNullOrEmpty(subjectUri))
            {
                // try a fallback
                var resource = XmlSupport.GetAttributeValue(parent, "resource", false);
                if (!string.IsNullOrEmpty(resource))
                {
                    // this is a license already defined somewhere else
                    // it could by a standard SPDX license
                    var license = (AnyLicenseInfo)GetLicenseFromKnownLicenseList(resource);
                    if (license != null)
                    {
                        return license;
                    } // if

                    // or a license reference inside this document
                    license = parser.FindKnownDocumentLicense(resource);
                    if (license != null)
                    {
                        return license;
                    } // if
                } // if

                return null;
            } // if

            if (KnownLicenseManager?.Licenses != null)
            {
                var license = FindLicenseById(subjectUri);
                if (license != null)
                {
                    return license;
                } // if

                license = GetLicenseFromKnownLicenseList(subjectUri);
                if (license != null)
                {
                    return license;
                } // if
            } // if

            if (subjectUri.StartsWith(NonStdLicenseIdPrenum))
            {
                var license = RdfParser.ReadExtractedLicenseInfo(parent, parser);
                if (license != null)
                {
                    return license;
                } // if
            } // if

            return null;
        } // GetLicenseFromId()

        /// <summary>
        /// Gets the license from the URI.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="AnyLicenseInfo"/> object or null.</returns>
        private static AnyLicenseInfo GetLicenseFromUri(XElement parent)
        {
            var uri = XmlSupport.GetAttributeValue(parent, "resource", false);
            if (string.IsNullOrEmpty(uri))
            {
                return null;
            } // if

            if ((uri == RdfParser.NoAssertionUri)
                || (uri == RdfParser.NoAssertionUri2))
            {
                return new SpdxNoAssertionLicense();
            } // if

            if (uri == RdfParser.NoneUri)
            {
                return new SpdxNoneLicense();
            } // if

            if (uri.StartsWith(ListedLicensesPrefix))
            {
                return GetLicenseFromKnownLicenseList(uri);
            } // if

            return null;
        } // GetLicenseFromUri()

        /// <summary>
        /// Gets the license from known license list.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>A <see cref="License"/> object or null.</returns>
        private static License GetLicenseFromKnownLicenseList(string uri)
        {
            var id = uri.Substring(ListedLicensesPrefix.Length);
            id = UnEscapeString(id);
            if (string.IsNullOrEmpty(id))
            {
                return null;
            } // if

            if (KnownLicenseManager?.Licenses == null)
            {
                // very simplified:
                var license = new License();
                license.Id = id;
                license.Name = $"Generated from ID {id}";
                return license;
            } // if

            return FindLicenseById(id);
        } // GetLicenseFromKnownLicenseList()

        /// <summary>
        /// Finds a license by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="License"/> object.</returns>
        private static License FindLicenseById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            } // if

            id = UnEscapeString(id);
            foreach (var licenseInfo in KnownLicenseManager.Licenses)
            {
                if (licenseInfo.LicenseId == id)
                {
                    return LicenseFromLicenseInfo(licenseInfo);
                } // if
            } // foreach

            return null;
        } // FindLicenseById()

        /// <summary>
        /// Get a license from the given license information.
        /// </summary>
        /// <param name="licenseInfo">The license information.</param>
        /// <returns>A <see cref="License"/> object.</returns>
        private static License LicenseFromLicenseInfo(ISpdxLicenseInfo licenseInfo)
        {
            var license = new License();
            license.Id = licenseInfo.LicenseId;
            license.Name = licenseInfo.Name;
            license.Comment = licenseInfo.LicenseComments;
            license.IsFsfLibre = licenseInfo.IsFsfLibre;
            license.IsOsiApproved = licenseInfo.IsOsiApproved;
            license.IsDeprecated = licenseInfo.IsDeprecatedLicenseId;
            license.LicenseText = licenseInfo.LicenseText;
            license.StandardLicenseHeader = licenseInfo.StandardLicenseHeader;
            license.StandardLicenseHeaderTemplate = licenseInfo.StandardLicenseHeaderTemplate;
            license.StandardLicenseTemplate = licenseInfo.StandardLicenseTemplate;
            return license;
        } // LicenseFromLicenseInfo()
        #endregion // PRIVATE METHODS
    } // LicenseInfoFactory
}
