// ---------------------------------------------------------------------------
// <copyright file="Program.cs" company="Tethys">
//   Copyright (C) 2022 T. Graf
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
    using Tethys.SimpleSpdxParser;
    using Tethys.SPDX.KnownLicenses;

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
                var reader = new RdfParser(knownLicenseManager);
                var spdxDoc = reader.ReadFromFile(args[0]);

                if (spdxDoc.ExtractedLicenseInfos.Count < 1)
                {
                    Console.WriteLine("No license information found!");
                    return;
                } // if

                foreach (var licenseInfo in spdxDoc.ExtractedLicenseInfos)
                {
                    Console.WriteLine($"  License found: {licenseInfo.Name}");
                } // foreach 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing SPDX file: " + ex.Message);
            } // catch
        }

        /// <summary>
        /// Gets an initialized instance of a known license manager.
        /// </summary>
        /// <returns>A <see cref="KnownLicenseManager"/>.</returns>
        private static KnownLicenseManager GetKnownLicenseManager()
        {
            // initialize (known) licenses.
            var knownLicenseManager = new KnownLicenseManager();

            var detailsFolder = Path.Combine(ExpectedDataPath, "details");
            knownLicenseManager.LoadSpdxSourceFiles(detailsFolder);

            var exceptionsFolder = Path.Combine(ExpectedDataPath, "exceptions");
            knownLicenseManager.LoadSpdxSourceFiles(exceptionsFolder);

            return knownLicenseManager;
        } // GetKnownLicenseManager()
    }
}
