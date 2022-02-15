// ---------------------------------------------------------------------------
// <copyright file="KnownLicenseManager.cs" company="Tethys">
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

namespace Tethys.SPDX.KnownLicenses
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using Tethys.Logging;
    using Tethys.SPDX.Interfaces;

    /// <summary>
    /// Reads data about known SPDX licenses from JSON files
    /// available at <see href="https://github.com/spdx/license-list-data"/>.
    /// </summary>
    public class KnownLicenseManager : IKnownLicenseManager
    {
        #region PRIVATE PROPERTIES
        /// <summary>
        /// The logger for this class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(KnownLicenseManager));

        /// <summary>
        /// The licenses.
        /// </summary>
        private readonly List<ISpdxLicenseInfo> licenses;
        #endregion // PRIVATE PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets the licenses.
        /// </summary>
        public IReadOnlyList<ISpdxLicenseInfo> Licenses => this.licenses;
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="KnownLicenseManager"/> class.
        /// </summary>
        public KnownLicenseManager()
        {
            this.licenses = new List<ISpdxLicenseInfo>();
        } // KnownLicenseManager()
        #endregion // CONSTRUCTION

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Reads SPDX data from the given file stream.
        /// </summary>
        /// <param name="fileContents">The file contents.</param>
        /// <returns>
        /// A <see cref="SpdxLicenseInfo" /> object.
        /// </returns>
        public static ISpdxLicenseInfo ReadFromString(string fileContents)
        {
            var spdxinfo = JsonConvert.DeserializeObject<SpdxLicenseInfo>(fileContents);
            return spdxinfo;
        } // ReadFromString()

        /// <summary>
        /// Loads a SPDX license source files.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        public void LoadSpdxSourceFiles(string folderName)
        {
            if (!Directory.Exists(folderName))
            {
                var di = new DirectoryInfo(folderName);
                Log.Warn($"SPDX license info folder does not exist: '{di.FullName}'");
                return;
            } // if

            foreach (var file in Directory.EnumerateFiles(folderName))
            {
                var spdxInfo = LoadSpdxSourceFile(file);
                this.licenses.Add(spdxInfo);
            } // foreach

            Log.Info($"{this.licenses.Count} SPDX licenses read.");
        } // LoadSpdxSourceFiles()

        /// <summary>
        /// Loads the SPDX license list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The <see cref="SpdxLicenseListInfo"/> license list.</returns>
        public ISpdxLicenseListInfo LoadSpdxLicenseList(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                using (var sr = new StreamReader(stream))
                {
                    stream = null;
                    var spdxinfo = JsonConvert.DeserializeObject<SpdxLicenseListInfo>(sr.ReadToEnd());
                    return spdxinfo;
                } // using
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX license list", ex);
            }
            finally
            {
                stream?.Dispose();
            } // finally

            return null;
        } // LoadSpdxLicenseList()

        /// <summary>
        /// Loads the SPDX license list.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The <see cref="SpdxExceptionList"/> license list.</returns>
        public ISpdxExceptionList LoadSpdxExceptionList(string fileName)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(fileName);
                using (var sr = new StreamReader(stream))
                {
                    stream = null;
                    var spdxinfo = JsonConvert.DeserializeObject<SpdxExceptionList>(sr.ReadToEnd());
                    return spdxinfo;
                } // using
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX exception list", ex);
            }
            finally
            {
                stream?.Dispose();
            } // finally

            return null;
        } // LoadSpdxExceptionList()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Loads a SPDX license source file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>A <see cref="SpdxLicenseInfo"/> object.</returns>
        private static ISpdxLicenseInfo LoadSpdxSourceFile(string filename)
        {
            FileStream stream = null;
            try
            {
                stream = File.OpenRead(filename);
                using (var sr = new StreamReader(stream))
                {
                    stream = null;
                    return ReadFromString(sr.ReadToEnd());
                } // using
            }
            catch (Exception ex)
            {
                Log.Error("Error reading SPDX license file", ex);
                throw;
            }
            finally
            {
                stream?.Dispose();
            } // finally
        } // LoadSpdxSourceFile()
        #endregion // PRIVATE METHODS
    } // KnownLicenseManager
}
