// ---------------------------------------------------------------------------
// <copyright file="RdfParser.cs" company="Tethys">
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

namespace Tethys.SPDX.SimpleSpdxParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Tethys.Logging;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.Model.License;
    using Tethys.SPDX.Model.Pointer;
    using Tethys.SPDX.Support;
    using Tethys.Xml;

    /// <summary>
    /// Reads and write SPDX documents in RDF format.
    /// </summary>
    public class RdfParser
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// Gets the RDF name space.
        /// </summary>
        private static readonly XNamespace RdfNameSpace;

        /// <summary>
        /// The snippet name.
        /// </summary>
        private static readonly XName SnippetName;

        /// <summary>
        /// The RDF root name.
        /// </summary>
        private static readonly XName RdfRootName;

        /// <summary>
        /// The relationship name.
        /// </summary>
        private static readonly XName RelationshipName;

        /// <summary>
        /// The annotation name.
        /// </summary>
        private static readonly XName AnnotationName;

        /// <summary>
        /// The data license name.
        /// </summary>
        private static readonly XName DataLicenseName;

        /// <summary>
        /// The has extracted licensing information name.
        /// </summary>
        private static readonly XName HasExtractedLicensingInfoName;

        /// <summary>
        /// The external document reference name.
        /// </summary>
        private static readonly XName ExternalDocumentRefName;

        /// <summary>
        /// The creation information name.
        /// </summary>
        private static readonly XName CreationInfoName;

        /// <summary>
        /// The creator name.
        /// </summary>
        private static readonly XName CreatorName;

        /// <summary>
        /// The package verification code excluded file name.
        /// </summary>
        private static readonly XName PackageVerificationCodeExcludedFileName;

        /// <summary>
        /// The checksum name.
        /// </summary>
        private static readonly XName ChecksumName;

        /// <summary>
        /// The has file name.
        /// </summary>
        private static readonly XName HasFileName;

        /// <summary>
        /// The license information in snippet name.
        /// </summary>
        private static readonly XName LicenseInfoInSnippetName;

        /// <summary>
        /// The contributor name.
        /// </summary>
        private static readonly XName ContributorName;

        /// <summary>
        /// The contributor name.
        /// </summary>
        private static readonly XName LicenseInfoInFileName;

        /// <summary>
        /// The name for a name object.
        /// </summary>
        private static readonly XName NameName;

        /// <summary>
        /// The comment name.
        /// </summary>
        private static readonly XName CommentName;

        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(RdfParser));

        /// <summary>
        /// The known SPDX elements.
        /// </summary>
        private static readonly Dictionary<string, SpdxElement> KnownSpdxElements;

        /// <summary>
        /// The known RDF nodes (identified via ID).
        /// </summary>
        private static readonly Dictionary<string, object> KnownRdfNodes;

        /// <summary>
        /// The relationship types dictionary.
        /// </summary>
        private static Dictionary<string, RelationshipType> mapRelationshipTypes;

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
        /// The no assertion URI.
        /// </summary>
        public const string NoAssertionUri = "http://spdx.org/rdf/terms#noassertion";

        /// <summary>
        /// Another no assertion URI.
        /// </summary>
        public const string NoAssertionUri2 = "http://spdx.org/licenses/NOASSERTION";

        /// <summary>
        /// The none URI.
        /// </summary>
        public const string NoneUri = "http://spdx.org/rdf/terms#none";

        /// <summary>
        /// Gets the SPDX name space.
        /// </summary>
        public static readonly XNamespace SpdxNameSpace;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes static members of the <see cref="RdfParser"/> class.
        /// </summary>
        static RdfParser()
        {
            // namespaces
            SpdxNameSpace = "http://spdx.org/rdf/terms#";
            RdfNameSpace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";

            // element names
            SnippetName = SpdxNameSpace + "Snippet";
            RdfRootName = SpdxNameSpace + "SpdxDocument";
            RelationshipName = SpdxNameSpace + "relationship";
            AnnotationName = SpdxNameSpace + "annotation";
            DataLicenseName = SpdxNameSpace + "dataLicense";
            HasExtractedLicensingInfoName = SpdxNameSpace + "hasExtractedLicensingInfo";
            ExternalDocumentRefName = SpdxNameSpace + "externalDocumentRef";
            CreationInfoName = SpdxNameSpace + "creationInfo";
            CreatorName = SpdxNameSpace + "creator";
            PackageVerificationCodeExcludedFileName = SpdxNameSpace + "packageVerificationCodeExcludedFile";
            ChecksumName = SpdxNameSpace + "checksum";
            HasFileName = SpdxNameSpace + "hasFile";
            LicenseInfoInSnippetName = SpdxNameSpace + "licenseInfoInSnippet";
            ContributorName = SpdxNameSpace + "fileContributor";
            LicenseInfoInFileName = SpdxNameSpace + "licenseInfoInFile";
            NameName = "name";
            CommentName = "comment";

            InitializeMapRelationshipTypes();

            KnownSpdxElements = new Dictionary<string, SpdxElement>();
            KnownRdfNodes = new Dictionary<string, object>();
        } // RdfParser()

        /// <summary>
        /// Initializes a new instance of the <see cref="RdfParser" /> class.
        /// </summary>
        /// <param name="licenseManager">The license manager.</param>
        public RdfParser(KnownLicenseManager licenseManager)
        {
            this.knownLicenseManager = licenseManager;
            this.knownDocumentLicenses = new Dictionary<string, AnyLicenseInfo>();
            this.listedLicenses = new Dictionary<string, AnyLicenseInfo>();
        } // RdfParser()
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
                    // use default encoding for RDF/XML files: UTF-8
                    encoding = Encoding.UTF8;
                } // if

                using (var stream = File.OpenRead(filename))
                {
                    return this.ReadFromFile(stream, encoding);
                } // using
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX file", ex);
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
                using (var sr = new StreamReader(stream, encoding))
                {
                    return this.ReadFromString(sr.ReadToEnd());
                } // using
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX file", ex);
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

            var rdfRootName = RdfNameSpace + "RDF";

            var document = XDocument.Parse(fileContents);
            var xspdx = document.Element(rdfRootName);
            if (xspdx == null)
            {
                throw new XmlException($"XML element '{rdfRootName}' missing");
            } // if

            var spdxDoc = this.ReadFromXElement(xspdx);
            return spdxDoc;
        } // ReadFromString()

        /// <summary>
        /// Reads SPDX data from an XML node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxDocument" /> object.</returns>
        public SpdxDocument ReadFromXElement(XContainer parent)
        {
            Log.Debug("Reading SPDXDocument from XElement...");

            this.knownDocumentLicenses.Clear();
            this.listedLicenses.Clear();
            KnownSpdxElements.Clear();
            SpdxDocument spdxDoc = null;
            SpdxSnippet snippet = null;
            foreach (var xElement in parent.Elements())
            {
                if (xElement.Name == SnippetName)
                {
                    snippet = this.ReadSnippet(xElement);
                    continue;
                } // if

                if (xElement.Name == RdfRootName)
                {
                    spdxDoc = this.ReadSpdxDocument(xElement);
                } // if
            } // foreach

            if ((spdxDoc != null) && (snippet != null))
            {
                spdxDoc.AddSnippet(snippet);
            } // if

            return spdxDoc;
        } // ReadFromXElement()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region INTERNAL METHODS
        /// <summary>
        /// Adds a known document licenses.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="license">The license.</param>
        internal void AddKnownDocumentLicense(string identifier, AnyLicenseInfo license)
        {
            this.knownDocumentLicenses.Add(identifier, license);
        } // AddKnownDocumentLicense()

        /// <summary>
        /// Finds a known document license.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>An <see cref="AnyLicenseInfo"/> object or null.</returns>
        internal AnyLicenseInfo FindKnownDocumentLicense(string identifier)
        {
            if (this.knownDocumentLicenses.ContainsKey(identifier))
            {
                return this.knownDocumentLicenses[identifier];
            } // if

            Log.Warn($"Known license reference not found: {identifier}");

            return null;
        } // FindKnownDocumentLicense()

        /// <summary>
        /// Adds a listed license.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="license">The license.</param>
        internal void AddListedLicense(string identifier, AnyLicenseInfo license)
        {
            this.listedLicenses.Add(identifier, license);
        } // AddListedLicense()

        /// <summary>
        /// Finds a listed license.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>An <see cref="AnyLicenseInfo"/> object or null.</returns>
        internal AnyLicenseInfo FindListedLicense(string identifier)
        {
            if (this.listedLicenses.ContainsKey(identifier))
            {
                return this.listedLicenses[identifier];
            } // if

#if false
            foreach (var license in this.knownLicenseManager.Licenses)
            {
                if (license.LicenseId.Equals(identifier))
                {
                    return license
                }
            }
#endif

            Log.Warn($"Listed license reference not found: {identifier}");

            return null;
        } // FindListedLicense()

        /// <summary>
        /// Finds a known RDF node.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>An object or null.</returns>
        internal static object FindKnownRdfNode(string identifier)
        {
            if (KnownRdfNodes.ContainsKey(identifier))
            {
                return KnownRdfNodes[identifier];
            } // if

            Log.Warn($"RDF node not found: {identifier}");

            return null;
        } // FindKnownRdfNode()
        #endregion INTERNAL METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Reads the snippet.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxSnippet"/> object.</returns>
        private SpdxSnippet ReadSnippet(XElement parent)
        {
            Log.Debug("Reading Snippet...");

            var snippet = new SpdxSnippet();

            var text = XmlSupport.GetAttributeValue(parent, "about");
            snippet.SpdxIdentifier = GetSpdxIdentifierFromUri(text);
            snippet.Name = XmlSupport.GetFirstSubNodeValue(parent, "name");
            snippet.LicenseComments = XmlSupport.GetFirstSubNodeValue(parent, "licenseComments");
            snippet.AttributionText = XmlSupport.GetFirstSubNodeValue(parent, "attributionText", false);

            var xRange = XmlSupport.GetFirstSubNode(parent, "range");
            var xStartEndPointer = XmlSupport.GetFirstSubNode(xRange, "StartEndPointer");
            var xEndPointer = XmlSupport.GetFirstSubNode(xStartEndPointer, "endPointer");
            var lcpEnd = this.ReadLineCharPointer(xEndPointer);
            var bopEnd = this.ReadByteOffsetPointer(xEndPointer);
            var xStartPointer = XmlSupport.GetFirstSubNode(xStartEndPointer, "startPointer");
            var lcpStart = this.ReadLineCharPointer(xStartPointer);
            var bopStart = this.ReadByteOffsetPointer(xStartPointer);

            if ((lcpEnd != null) && (lcpStart != null))
            {
                var range = new StartEndPointer();
                range.StartPointer = lcpStart;
                range.EndPointer = lcpEnd;
                snippet.AddRange(range);
            } // if

            if ((bopEnd != null) && (bopStart != null))
            {
                var range = new StartEndPointer();
                range.StartPointer = bopStart;
                range.EndPointer = bopEnd;
                snippet.AddRange(range);
            } // if

            var xSnippetFromFile = XmlSupport.GetFirstSubNode(parent, "snippetFromFile");
            var resource = XmlSupport.GetAttributeValue(xSnippetFromFile, "resource", false);
            var id = GetSpdxIdentifierFromUri(resource);
            snippet.SnippetFromFile = FindSpdxElementByName(id) as SpdxFile;

            var xLicenseInfoInSnippetList = from xLicenseInfoInSnippet in parent.Elements(LicenseInfoInSnippetName)
                                            select xLicenseInfoInSnippet;
            foreach (var xLicenseInfoInSnippet in xLicenseInfoInSnippetList)
            {
                var license = this.ReadLicense(xLicenseInfoInSnippet);
                snippet.AddLicenseInfoInSnippet(license);
            } // foreach

            var xLicenseConcluded = XmlSupport.GetFirstSubNode(parent, "licenseConcluded");
            if (xLicenseConcluded != null)
            {
                snippet.LicenseConcluded = this.ReadLicense(xLicenseConcluded);
            } // if

            snippet.Comment = XmlSupport.GetFirstSubNodeValue(parent, "comment", false);
            snippet.CopyrightText = XmlSupport.GetFirstSubNodeValue(parent, "copyrightText");

            KnownSpdxElements.Add(snippet.SpdxIdentifier, snippet);

            return snippet;
        } // ReadSnippet()

        /// <summary>
        /// Reads the byte offset pointer.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="ByteOffsetPointer"/> object or null.</returns>
        private ByteOffsetPointer ReadByteOffsetPointer(XElement parent)
        {
            parent = XmlSupport.GetFirstSubNode(parent, "ByteOffsetPointer", false);
            if (parent == null)
            {
                return null;
            } // if

            var bop = new ByteOffsetPointer();
            bop.Reference = this.ReadReference(parent);
            bop.Offset = int.Parse(XmlSupport.GetFirstSubNodeValue(parent, "offset"));

            return bop;
        } // ReadByteOffsetPointer()

        /// <summary>
        /// Reads a line character pointer.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="LineCharPointer"/> object or null.</returns>
        private LineCharPointer ReadLineCharPointer(XElement parent)
        {
            parent = XmlSupport.GetFirstSubNode(parent, "LineCharPointer", false);
            if (parent == null)
            {
                return null;
            } // if

            var lcp = new LineCharPointer();
            lcp.Reference = this.ReadReference(parent);
            lcp.LineNumber = int.Parse(XmlSupport.GetFirstSubNodeValue(parent, "lineNumber"));

            return lcp;
        } // ReadLineCharPointer()

        /// <summary>
        /// Reads a reference.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxElement"/> or null.</returns>
        private SpdxElement ReadReference(XContainer parent)
        {
            SpdxElement reference = null;
            var xReference = XmlSupport.GetFirstSubNode(parent, "reference");
            var resource = XmlSupport.GetAttributeValue(xReference, "resource", false);
            if (resource == null)
            {
                var xFile = XmlSupport.GetFirstSubNode(xReference, "File");
                var about = XmlSupport.GetAttributeValue(xFile, "about", false);
                if (about != null)
                {
#if false
                    var file = new SpdxFile();
                    file.SpdxIdentifier = GetSpdxIdentifierFromUri(about);
                    KnownSpdxElements.Add(file.SpdxIdentifier, file);
#else
                    var file = this.ReadSpdxFile(xFile);
#endif
                    reference = file;
                } // if
            } // if

            if (reference == null)
            {
                var id = GetSpdxIdentifierFromUri(resource);
                reference = FindSpdxElementByName(id);
            } // if

            return reference;
        } // ReadReference()

        /// <summary>
        /// Gets the SPDX identifier from URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>A string.</returns>
        private static string GetSpdxIdentifierFromUri(string uri)
        {
            var pos = uri.IndexOf("#", StringComparison.OrdinalIgnoreCase);
            if (pos < 0)
            {
                return string.Empty;
            } // if

            return uri.Substring(pos + 1);
        } // GetSpdxIdentifierFromUri()

        /// <summary>
        /// Reads a SPDX document.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxDocument"/> object.</returns>
        private SpdxDocument ReadSpdxDocument(XElement parent)
        {
            Log.Debug("Reading SpdxDocument...");

            var spdxDoc = new SpdxDocument();

            var help = XmlSupport.GetAttributeValue(parent, "about");
            var pos = help.IndexOf("#", StringComparison.OrdinalIgnoreCase);
            if (pos < 0)
            {
                throw new InvalidSpdxAnalysisException("No document namespace specified!");
            } // if

            spdxDoc.SpdxIdentifier = help.Substring(pos + 1);
            spdxDoc.SpdxDocumentNamespace = help.Substring(0, pos);

            spdxDoc.SpecVersion = XmlSupport.GetFirstSubNodeValue(parent, "specVersion", false);
            spdxDoc.Comment = XmlSupport.GetFirstSubNodeValue(parent, "comment", false);
            spdxDoc.Name = XmlSupport.GetFirstSubNodeValue(parent, "name", false);

            foreach (var xElement in parent.Elements())
            {
                if (xElement.Name == AnnotationName)
                {
                    var a = ReadAnnotation(xElement);
                    spdxDoc.AddAnnotation(a);
                    continue;
                } // if

                if (xElement.Name == RelationshipName)
                {
                    var rel = this.ReadRelationShip(xElement);
                    spdxDoc.AddRelationShip(rel);
                    continue;
                } // if

                if (xElement.Name == DataLicenseName)
                {
                    var license = this.ReadLicense(xElement);
                    spdxDoc.DataLicense = license;
                    continue;
                } // if

                if (xElement.Name == HasExtractedLicensingInfoName)
                {
                    var info = ReadExtractedLicenseInfo(xElement, this);
                    spdxDoc.AddExtractedLicenseInfo(info);
                    continue;
                } // if

                if (xElement.Name == ExternalDocumentRefName)
                {
                    /*var reference = */
                    ReadExternalDocumentRef(xElement);
                    continue;
                } // if

                if (xElement.Name == CreationInfoName)
                {
                    spdxDoc.CreationInfo = ReadCreationInfo(xElement);
                    continue;
                } // if

                if (xElement.Name.LocalName == CommentName)
                {
                    spdxDoc.Comment = ReadComment(xElement);
                    continue;
                } // if

                if (xElement.Name.LocalName == NameName)
                {
                    spdxDoc.Name = ReadName(xElement);
                } // if
            } // foreach

            KnownSpdxElements.Add(spdxDoc.SpdxIdentifier, spdxDoc);

            return spdxDoc;
        } // ReadSpdxDocument()

        /// <summary>
        /// Gets the resource attribute or value.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>The value.</returns>
        private static string GetResourceAttributeOrValue(XElement parent)
        {
            if (parent == null)
            {
                return string.Empty;
            } // if

            var help = XmlSupport.GetAttributeValue(parent, "resource", false);
            if (help != null)
            {
                if (help.Equals(RdfParser.NoAssertionUri, StringComparison.OrdinalIgnoreCase))
                {
                    return Constants.NoAssertion;
                } // if

                if (help.Equals(RdfParser.NoneUri, StringComparison.OrdinalIgnoreCase))
                {
                    return Constants.None;
                } // if
            } // if

            var text = GetCDataValue(parent);
            return text.Trim();
        } // GetResourceAttributeOrValue()

        /// <summary>
        /// Reads a SPDX package.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxPackage"/> object.</returns>
        private SpdxPackage ReadSpdxPackage(XElement parent)
        {
            Log.Debug("Reading SpdxPackage...");

            var package = new SpdxPackage();

            var help = XmlSupport.GetAttributeValue(parent, "about");
            package.SpdxIdentifier = GetSpdxIdentifierFromUri(help);

            package.Name = XmlSupport.GetFirstSubNodeValue(parent, "name");
            package.PackageFileName = XmlSupport.GetFirstSubNodeValue(parent, "packageFileName");
            var xdownloadLocation = XmlSupport.GetFirstSubNode(parent, "downloadLocation");
            if (xdownloadLocation == null)
            {
                // mandatory property
                throw new InvalidSpdxAnalysisException("Download location missing!");
            } // if

            package.DownloadLocation = GetResourceAttributeOrValue(xdownloadLocation);

            var xPackageVerificationCode = XmlSupport.GetFirstSubNode(parent, "packageVerificationCode");
            if (xPackageVerificationCode != null)
            {
                package.PackageVerificationCode = ReadSpdxPackageVerificationCode(xPackageVerificationCode);
            } // if

            var xChecksumList = from xChecksum in parent.Elements(ChecksumName)
                                select xChecksum;
            foreach (var xChecksum in xChecksumList)
            {
                package.AddChecksum(ReadChecksum(xChecksum));
            } // foreach

            // does not need to exist in SPDX 2.3
            var xLicenseConcluded = XmlSupport.GetFirstSubNode(parent, "licenseConcluded", false);
            if (xLicenseConcluded != null)
            {
                package.LicenseConcluded = this.ReadLicense(xLicenseConcluded);
            } // if

            package.LicenseComments = GetCDataValue(XmlSupport.GetFirstSubNode(parent, "licenseComments", false));
            package.AttributionText = GetCDataValue(XmlSupport.GetFirstSubNode(parent, "attributionText", false));
            package.LicenseDeclared = this.ReadLicense(XmlSupport.GetFirstSubNode(parent, "licenseDeclared"));

            var xLicenseInfoFromFiles = XmlSupport.GetFirstSubNode(parent, "licenseInfoFromFiles");
            help = GetResourceAttributeOrValue(xLicenseInfoFromFiles);
            if (!string.IsNullOrEmpty(help))
            {
                if (help == Constants.None)
                {
                    package.AddLicenseInfoFromFile(new SpdxNoneLicense());
                } // if

                if (help == Constants.NoAssertion)
                {
                    package.AddLicenseInfoFromFile(new SpdxNoAssertionLicense());
                } // if
            }
            else
            {
                var xLicenseFormFilesList = from xLicenseFormFiles in parent.Elements(HasFileName)
                                            select xLicenseFormFiles;
                foreach (var xLicenseFormFiles in xLicenseFormFilesList)
                {
                    package.AddLicenseInfoFromFile(this.ReadLicense(xLicenseFormFiles));
                } // foreach
            } // if

            package.CopyrightText = XmlSupport.GetFirstSubNodeValue(parent, "copyrightText");

            var xHasFiles = XmlSupport.GetFirstSubNode(parent, "hasFile");
            if (xHasFiles != null)
            {
                var xHasFileList = from xHasFile in parent.Elements(HasFileName)
                                   select xHasFile;
                foreach (var xHasFile in xHasFileList)
                {
                    package.AddFile(this.ReadSpdxFile(xHasFile));
                } // foreach
            } // if

            KnownSpdxElements.Add(package.SpdxIdentifier, package);

            return package;
        } // ReadSpdxPackage()

        /// <summary>
        /// Reads a SPDX file.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxFile"/> object.</returns>
        private SpdxFile ReadSpdxFile(XElement parent)
        {
            ////Log.Debug("Reading SpdxFile...");

            var file = new SpdxFile();

            if (parent.Name.LocalName == "hasFile")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "File", false);
                if (parent == null)
                {
                    return file;
                }
            } // if

            var text = XmlSupport.GetAttributeValue(parent, "about");
            file.SpdxIdentifier = GetSpdxIdentifierFromUri(text);
            file.Name = XmlSupport.GetFirstSubNodeValue(parent, "fileName");

            var xChecksumList = from xChecksum in parent.Elements(ChecksumName)
                                select xChecksum;
            foreach (var xChecksum in xChecksumList)
            {
                file.AddChecksum(ReadChecksum(xChecksum));
            } // foreach

            var xContributorList = from xContributor in parent.Elements(ContributorName)
                                   select xContributor;
            foreach (var xContributor in xContributorList)
            {
                file.AddFileContributor(xContributor.Value);
            } // foreach

            var xLicenseConcluded = XmlSupport.GetFirstSubNode(parent, "licenseConcluded");
            if (xLicenseConcluded != null)
            {
                file.LicenseConcluded = this.ReadLicense(xLicenseConcluded);
            } // if

#if true
            var xLicenseInfoInFileList = from xLicenseInfoInFile in parent.Elements(LicenseInfoInFileName)
                                         select xLicenseInfoInFile;
            foreach (var xLicenseInfoInFile in xLicenseInfoInFileList)
            {
                var help = this.ReadLicense(xLicenseInfoInFile);
                file.AddLicenseInfoFromFile(help);
            } // foreach
#else
            var xLicenseInfoInFile = XmlSupport.GetFirstSubNode(parent, "licenseInfoInFile");
            if (xLicenseInfoInFile != null)
            {
                var help = this.ReadLicense(xLicenseInfoInFile);
                file.AddLicenseInfoFromFile(help);
            } // if
#endif

            var xct = XmlSupport.GetFirstSubNode(parent, "copyrightText", false);
            file.CopyrightText = GetResourceAttributeOrValue(xct);
            file.AttributionText = GetCDataValue(XmlSupport.GetFirstSubNode(parent, "attributionText", false));

            var xFileType = XmlSupport.GetFirstSubNode(parent, "fileType", false);

            // fileDependency is deprecated
            KnownSpdxElements.Add(file.SpdxIdentifier, file);

            return file;
        } // ReadSpdxFile()

        /// <summary>
        /// Gets the <c>CDATA</c> value as string.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A string.</returns>
        private static string GetCDataValue(XElement parent)
        {
            if (parent == null)
            {
                return string.Empty;
            } // if

            foreach (var xnode in parent.Nodes())
            {
                if (xnode.NodeType == XmlNodeType.CDATA)
                {
                    if (!(xnode is XText xtext))
                    {
                        return string.Empty;
                    } // if

                    return xtext.Value;
                } // if
            } // foreach

            var attr = XmlSupport.GetAttributeValue(parent, "resource", false);
            if ((attr != null) && (attr == "http://spdx.org/rdf/terms#noassertion"))
            {
                return attr;
            } // if

            // fallback
            var first = parent.FirstNode;
            if ((first != null) && (first.NodeType == XmlNodeType.Text))
            {
                if (!(first is XText xtext))
                {
                    return string.Empty;
                } // if

                return xtext.Value;
            } // if

            return string.Empty;
        } // GetCDataValue()

        /// <summary>
        /// Reads a checksum.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="Checksum"/> object.</returns>
        private static Checksum ReadChecksum(XElement parent)
        {
            ////Log.Debug("Reading Checksum...");

            var checksum = new Checksum();

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    checksum = (Checksum)FindKnownRdfNode(rdf.NodeId);
                    return checksum;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, checksum);
            } // if

            if (parent.Name.LocalName == "checksum")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "Checksum");
            } // if

            rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    checksum = (Checksum)FindKnownRdfNode(rdf.NodeId);
                    return checksum;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, checksum);
            } // if

            checksum.Value = XmlSupport.GetFirstSubNodeValue(parent, "checksumValue");
            var xAlgorithm = XmlSupport.GetFirstSubNode(parent, "algorithm");
            if (xAlgorithm != null)
            {
                var text = XmlSupport.GetAttributeValue(xAlgorithm, "resource");
                checksum.Algorithm = MapStringToChecksumAlgorithm(text);
            } // if

            return checksum;
        } // ReadChecksum()

        /// <summary>
        /// Maps the string to checksum algorithm.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="ChecksumAlgorithm"/> value.</returns>
        private static ChecksumAlgorithm MapStringToChecksumAlgorithm(string text)
        {
            if (text.Equals("http://spdx.org/rdf/terms#checksumAlgorithm_md5"))
            {
                return ChecksumAlgorithm.MD5;
            } // if

            if (text.Equals("http://spdx.org/rdf/terms#checksumAlgorithm_sha1"))
            {
                return ChecksumAlgorithm.SHA1;
            } // if

            if (text.Equals("http://spdx.org/rdf/terms#checksumAlgorithm_sha256"))
            {
                return ChecksumAlgorithm.SHA256;
            } // if

            throw new ArgumentOutOfRangeException($"'{text}' is no valid ChecksumAlgorithm!");
        } // MapStringToChecksumAlgorithm()

        /// <summary>
        /// Reads a SPDX package verification code.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxPackageVerificationCode"/> object.</returns>
        private static SpdxPackageVerificationCode ReadSpdxPackageVerificationCode(XElement parent)
        {
            Log.Debug("Reading SpdxPackageVerificationCode...");

            var code = new SpdxPackageVerificationCode();

            if (parent.Name.LocalName == "packageVerificationCode")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "PackageVerificationCode");
            } // if

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    code = (SpdxPackageVerificationCode)FindKnownRdfNode(rdf.NodeId);
                    return code;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, code);
            } // if

            code.Value = XmlSupport.GetFirstSubNodeValue(parent, "packageVerificationCodeValue");
            var xExcludedFileList = from xExcludedFile in parent.Elements(PackageVerificationCodeExcludedFileName)
                                    select xExcludedFile;
            foreach (var xExcludedFile in xExcludedFileList)
            {
                code.AddExcludedFileName(xExcludedFile.Value);
            } // foreach

            return code;
        } // ReadSpdxPackageVerificationCode()

        /// <summary>
        /// Reads a comment.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A comment string.</returns>
        private static string ReadComment(XElement parent)
        {
            Log.Debug("Reading SpdxName...");
            return parent.Value;
        } // ReadComment()

        /// <summary>
        /// Reads a name.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A name string.</returns>
        private static string ReadName(XElement parent)
        {
            Log.Debug("Reading SpdxName...");
            return parent.Value;
        } // ReadName()

        /// <summary>
        /// Reads the creation information.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="SpdxCreatorInformation"/> object.</returns>
        private static SpdxCreatorInformation ReadCreationInfo(XElement parent)
        {
            Log.Debug("Reading SpdxCreatorInformation...");

            if (parent.Name.LocalName == "creationInfo")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "CreationInfo");
            } // if

            var info = new SpdxCreatorInformation();

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    info = (SpdxCreatorInformation)FindKnownRdfNode(rdf.NodeId);
                    return info;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, info);
            } // if

            info.LicenseListVersion = XmlSupport.GetFirstSubNodeValue(parent, "licenseListVersion", false);
            var text = XmlSupport.GetFirstSubNodeValue(parent, "created");
            info.CreatedDate = DateTime.Parse(text);
            info.Comment = XmlSupport.GetFirstSubNodeValue(parent, "comment", false);

            var xCreatorList = from xCreator in parent.Elements(CreatorName)
                               select xCreator;
            foreach (var xCreator in xCreatorList)
            {
                info.AddCreator(xCreator.Value);
            } // foreach

            return info;
        } // ReadCreationInfo

        /// <summary>
        /// Reads an external document reference.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>An <see cref="ExternalDocumentRef"/> object.</returns>
        private static ExternalDocumentRef ReadExternalDocumentRef(XElement parent)
        {
            Log.Debug("Reading ExternalDocumentRef...");

            //// TODO

            return null;
        } // ReadExternalDocumentRef()

        /// <summary>
        /// Reads an extracted license information.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="parser">The parser.</param>
        /// <returns>
        /// A <see cref="ExtractedLicenseInfo" /> object or null.
        /// </returns>
        internal static ExtractedLicenseInfo ReadExtractedLicenseInfo(XElement parent, RdfParser parser)
        {
            Log.Debug("Reading ExtractedLicenseInfo...");

            var info = new ExtractedLicenseInfo();

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    info = (ExtractedLicenseInfo)FindKnownRdfNode(rdf.NodeId);
                    return info;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, info);
            } // if

            var resource = XmlSupport.GetAttributeValue(parent, "resource", false);
            if (!string.IsNullOrEmpty(resource))
            {
                var id = GetSpdxIdentifierFromUri(resource);
                var license = parser.FindKnownDocumentLicense(id);
                if (license != null)
                {
                    info.ExtractedText = string.Empty;
                    return info;
                } // if
            } // if

            if (parent.Name.LocalName != "ExtractedLicensingInfo")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "ExtractedLicensingInfo", false);
            } // if

            if (parent == null)
            {
                return null;
            } // if

            info.ExtractedText = XmlSupport.GetFirstSubNodeValue(parent, "extractedText");
            info.Id = XmlSupport.GetFirstSubNodeValue(parent, "licenseId", false);
            info.Name = XmlSupport.GetFirstSubNodeValue(parent, "name", false);

            var about = XmlSupport.GetAttributeValue(parent, "about", false);
            parser.AddKnownDocumentLicense(about, info);

            return info;
        } // ReadExtractedLicenseInfo()

        /// <summary>
        /// Reads a license.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>
        /// A license object.
        /// </returns>
        internal AnyLicenseInfo ReadLicense(XElement parent)
        {
            ////Log.Debug("Reading a License...");

            var license = new License();

            if (parent == null)
            {
                return license;
            } // if

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    license = (License)FindKnownRdfNode(rdf.NodeId);
                    return license;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, license);
            } // if

            if (parent.Name.LocalName != "License")
            {
                var parent2 = XmlSupport.GetFirstSubNode(parent, "License", false);
                if (parent2 == null)
                {
                    return LicenseInfoFactory.CreateFromXml(parent, this);
                } // if

                parent = parent2;
            } // if

            return LicenseInfoFactory.CreateFromXml(parent, this);
        } // ReadLicense(()

        /// <summary>
        /// Reads a relation ship.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="RelationShip"/> object.</returns>
        private RelationShip ReadRelationShip(XElement parent)
        {
            Log.Debug("Reading a RelationShip...");

            if (parent.Name.LocalName == "relationship")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "Relationship");
            } // if

            var relation = new RelationShip();

            var xRelationshipType = XmlSupport.GetFirstSubNode(parent, "relationshipType");
            if (xRelationshipType != null)
            {
                var text = XmlSupport.GetAttributeValue(xRelationshipType, "resource");
                relation.Type = MapStringToRelationShipType(text);
            } // if

            var xRelatedSpdxElement = XmlSupport.GetFirstSubNode(parent, "relatedSpdxElement");
            var resource = XmlSupport.GetAttributeValue(xRelatedSpdxElement, "resource", false);
            if (resource != null)
            {
                // for example: http://spdx.org/spdxdocs/spdx-example-444504E0-4F89-41D3-9A0C-0305E82C3301#SPDXRef-JenaLib
                var id = GetSpdxIdentifierFromUri(resource);
                relation.RelatedElement = FindSpdxElementByName(id);
                if (relation.RelatedElement != null)
                {
                    return relation;
                } // if
            } // if

            // the related element can be one of:
            // - ExternalSpdxElement
            // - SpdxDocument
            // - SpdxItem ... which can be a
            //   - SpdxPackage
            //   - SpdxSnippet
            //   - SpdxFile
            string reference;
            var found = false;
            var xSpdxElement = XmlSupport.GetFirstSubNode(xRelatedSpdxElement, "SpdxElement", false);
            if (xSpdxElement != null)
            {
                reference = XmlSupport.GetAttributeValue(xSpdxElement, "about");
                relation.RelatedElement = FindSpdxElementByName(reference);
                found = true;
            } // if

            var xSpdxFile = XmlSupport.GetFirstSubNode(xRelatedSpdxElement, "File", false);
            if (xSpdxFile != null)
            {
                relation.RelatedElement = this.ReadSpdxFile(xSpdxFile);
                found = true;
            } // if

            var xSpdxPackage = XmlSupport.GetFirstSubNode(xRelatedSpdxElement, "Package", false);
            if (xSpdxPackage != null)
            {
                relation.RelatedElement = this.ReadSpdxPackage(xSpdxPackage);
                found = true;
            } // if

            if (!found)
            {
                reference = XmlSupport.GetAttributeValue(xRelatedSpdxElement, "resource");
                relation.RelatedElement = FindSpdxElementByName(reference);
            } // if

            return relation;
        } // ReadRelationShip()

        /// <summary>
        /// Finds the name of the SPDX element by.
        /// </summary>
        /// <param name="reference">The reference.</param>
        /// <returns>An <see cref="SpdxElement"/> or null.</returns>
        private static SpdxElement FindSpdxElementByName(string reference)
        {
            if (KnownSpdxElements.ContainsKey(reference))
            {
                return KnownSpdxElements[reference];
            } // if

            return null;
        } // FindSpdxElementByName()

        /// <summary>
        /// Maps the given string to a relation ship type.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A <see cref="RelationshipType"/> value.</returns>
        private static RelationshipType MapStringToRelationShipType(string text)
        {
            if (mapRelationshipTypes.ContainsKey(text))
            {
                return mapRelationshipTypes[text];
            } // if

            throw new ArgumentOutOfRangeException($"'{text}' is no valid RelationshipType!");
        } // MapStringToRelationShipType()

        /// <summary>
        /// Reads the annotation.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>An <see cref="Annotation"/> object.</returns>
        private static Annotation ReadAnnotation(XElement parent)
        {
            Log.Debug("Reading an ReadAnnotation...");

            if (parent.Name.LocalName == "annotation")
            {
                parent = XmlSupport.GetFirstSubNode(parent, "Annotation");
            } // if

            var annotation = new Annotation();

            var rdf = ReadRdfBaseProperties(parent);
            if (!string.IsNullOrEmpty(rdf.NodeId))
            {
                if (!parent.HasElements)
                {
                    annotation = (Annotation)FindKnownRdfNode(rdf.NodeId);
                    return annotation;
                } // if

                KnownRdfNodes.Add(rdf.NodeId, annotation);
            } // if

            var text = XmlSupport.GetFirstSubNodeValue(parent, "annotationDate", false);
            annotation.Date = DateTime.Parse(text);
            annotation.Comment = XmlSupport.GetFirstSubNodeValue(parent, "comment", false);
            annotation.Annotator = XmlSupport.GetFirstSubNodeValue(parent, "annotator", false);
            var xannotationType = XmlSupport.GetFirstSubNode(parent, "annotationType", false);
            if (xannotationType != null)
            {
                text = XmlSupport.GetAttributeValue(xannotationType, "resource");
                annotation.AnnotationType = MapStringToAnnotationType(text);
            } // if

            return annotation;
        } // ReadAnnotation()

        /// <summary>
        /// Maps the given text to an annotation type.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>An <see cref="AnnotationType"/> value.</returns>
        private static AnnotationType MapStringToAnnotationType(string text)
        {
            if (text.Equals("http://spdx.org/rdf/terms#annotationType_other"))
            {
                return AnnotationType.Other;
            } // if

            if (text.Equals("http://spdx.org/rdf/terms#annotationType_review"))
            {
                return AnnotationType.Review;
            } // if

            throw new ArgumentOutOfRangeException($"'{text}' is no valid AnnotationType!");
        } // MapStringToAnnotationType()

        /// <summary>
        /// Initializes the map relationship types.
        /// </summary>
        private static void InitializeMapRelationshipTypes()
        {
            mapRelationshipTypes = new Dictionary<string, RelationshipType>();
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_amends", RelationshipType.Amends);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_ancestorOf", RelationshipType.AncestorOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_buildToolOf", RelationshipType.BuildToolOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_containedBy", RelationshipType.ContainedBy);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_contains", RelationshipType.Contains);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_copyOf", RelationshipType.CopyOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_dataFileOf", RelationshipType.DataFileOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_descendantOf", RelationshipType.DescendantOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_distributionArtifact", RelationshipType.DistributionArtifact);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_documentationof", RelationshipType.DocumentationOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_dynamicLink", RelationshipType.DynamicLink);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_expandedFromArchive", RelationshipType.ExpandedFromArchive);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_fileAdded", RelationshipType.FileAdded);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_fileDeleted", RelationshipType.FileDeleted);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_describes", RelationshipType.Describes);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_describedBy", RelationshipType.DescribedBy);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_fileModified", RelationshipType.FileModified);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_generatedFrom", RelationshipType.GeneratedFrom);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_generates", RelationshipType.Generates);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_metafileOf", RelationshipType.MetaFileOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_optionalComponentOf", RelationshipType.OptionalComponentOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_other", RelationshipType.Other);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_packageOf", RelationshipType.PackageOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_patchApplied", RelationshipType.PatchApplied);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_patchFor", RelationshipType.PatchFor);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_staticLink", RelationshipType.StatikLink);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_testCaseOf", RelationshipType.TestCaseOf);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_prerequisite_for", RelationshipType.PrerequisiteFor);
            mapRelationshipTypes.Add("http://spdx.org/rdf/terms#relationshipType_hasPrerequisite", RelationshipType.HasPrerequisite);
        } // InitializeMapRelationshipTypes()

        /// <summary>
        /// Reads the base properties.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>A <see cref="RdfBaseItem"/> object.</returns>
        public static RdfBaseItem ReadRdfBaseProperties(XElement parent)
        {
            var item = new RdfBaseItem();

            if (parent == null)
            {
                return item;
            } // if

            item.About = XmlSupport.GetAttributeValue(parent, "about", false);
            item.Description = XmlSupport.GetAttributeValue(parent, "Description", false);
            item.Id = XmlSupport.GetAttributeValue(parent, "ID", false);
            item.Language = XmlSupport.GetAttributeValue(parent, "lang", false);
            item.NodeId = XmlSupport.GetAttributeValue(parent, "nodeID", false);
            item.Resource = XmlSupport.GetAttributeValue(parent, "resource", false);
            item.DataType = XmlSupport.GetAttributeValue(parent, "datatype", false);

            return item;
        } // ReadBaseProperties()
        #endregion // PRIVATE METHODS
    } // RdfParser
}
