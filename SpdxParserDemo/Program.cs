// ---------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tethys">
//   Copyright (C) 2022-2024 T. Graf
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

using System;

namespace SpdxParserDemo
{
    using System.IO;
    using System.Reflection;
    using Tethys.SPDX.KnownLicenses;
    using Tethys.SPDX.Model;
    using Tethys.SPDX.SimpleSpdxParser;

    /// <summary>
    /// Main class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The expected data path for the SPDX license data.
        /// </summary>
        private const string ExpectedDataPath = @"..\..\..\..\license-list-data";

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Simple demo for SPDX file parsing.");
            if (args.Length < 1)
            {
                Console.WriteLine("No SPDX file specified!");
                return;
            } // if

            try
            {
                var knownLicenseManager = GetKnownLicenseManager();

                var fi = new FileInfo(args[0]);
                if (!fi.Exists)
                {
                    Console.WriteLine("Input file not found!");
                    return;
                } // if

                SpdxDocument spdxDoc = null;
                if (fi.Extension.Equals(".xml"))
                {
                    Console.WriteLine($"Parsing SPDX XML file {fi.Name}");
                    var reader = new RdfParser(knownLicenseManager);
                    spdxDoc = reader.ReadFromFile(args[0]);
                } // if

                if (fi.Extension.Equals(".json"))
                {
                    Console.WriteLine($"Parsing SPDX JSON file {fi.Name}");
                    var reader = new JsonParser(knownLicenseManager);
                    spdxDoc = reader.ReadFromFile(args[0]);
                } // if

                if (spdxDoc == null)
                {
                    Console.WriteLine("Unsupported file format!");
                    return;
                } // if

                if (spdxDoc.ExtractedLicenseInfos.Count < 1)
                {
                    Console.WriteLine("No license information found!");
                    return;
                } // if

                foreach (var licenseInfo in spdxDoc.ExtractedLicenseInfos)
                {
                    Console.WriteLine($"  License found: {licenseInfo.Id} ({licenseInfo.Name})");
                } // foreach 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing SPDX file: " + ex.Message);
            } // catch
        } // Main()

        /// <summary>
        /// Gets an initialized instance of a known license manager.
        /// </summary>
        /// <returns>A <see cref="KnownLicenseManager"/>.</returns>
        private static KnownLicenseManager GetKnownLicenseManager()
        {
            // initialize (known) licenses.
            var knownLicenseManager = new KnownLicenseManager();

            var dataPath = FindSpdxLicenseDataPath();
            if (string.IsNullOrEmpty(dataPath))
            {
                throw new ApplicationException("SPDX license list not found!");
            } // if

            var detailsFolder = Path.Combine(dataPath, "details");
            knownLicenseManager.LoadSpdxSourceFiles(detailsFolder);

            var exceptionsFolder = Path.Combine(dataPath, "exceptions");
            knownLicenseManager.LoadSpdxExceptionFiles(exceptionsFolder);

            return knownLicenseManager;
        } // GetKnownLicenseManager()

        /// <summary>
        /// Finds the SPDX license data path.
        /// This is a very basic approach.
        /// </summary>
        /// <returns>A valid path or an empty string.</returns>
        private static string FindSpdxLicenseDataPath()
        {
            var di = new DirectoryInfo(ExpectedDataPath);
            if (di.Exists)
            {
                return ExpectedDataPath;
            } // if

            var fi = new FileInfo(Assembly.GetExecutingAssembly().Location);
            var path = fi.DirectoryName;
            while (true)
            {
                if (string.IsNullOrEmpty(path))
                {
                    return string.Empty;
                } // if

                var testpath = Path.Combine(path, "license-list-data");
                if (Directory.Exists(testpath))
                {
                    return testpath;
                } // if

                var parent = Directory.GetParent(path);
                if (parent == null)
                {
                    return string.Empty;
                } // if

                path = parent.FullName;
            } // while
        } // FindSpdxLicenseDataPath()
    }
}
