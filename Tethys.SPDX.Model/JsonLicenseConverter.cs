// ---------------------------------------------------------------------------
// <copyright file="JsonLicenseConverter.cs" company="Tethys">
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

namespace Tethys.SPDX.Model
{
    using System;

    using Newtonsoft.Json;
    using Tethys.SPDX.ExpressionParser;
    using Tethys.SPDX.Model.License;

    /// <summary>
    /// Converts a license to the SPDX JSON specific information.
    /// </summary>
    public class JsonLicenseConverter : JsonConverter
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the license manager.
        /// </summary>
        public static IDataManager DataManager { get; set; }
        #endregion // PUBLIC PROPERTIES

        //// ---------------------------------------------------------------------

        #region PUBLIC METHODS
        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is SpdxNoneLicense)
            {
                writer.WriteValue(Constants.None);
                return;
            } // if

            if (value is SpdxNoAssertionLicense)
            {
                writer.WriteValue(Constants.NoAssertion);
                return;
            } // if

            if (value is ConjunctiveLicenseSet cl)
            {
                writer.WriteValue(cl.ToString());
                return;
            } // if

            if (value is DisjunctiveLicenseSet dl)
            {
                writer.WriteValue(dl.ToString());
                return;
            } // if

            if (value is ListedLicenseInfo ll)
            {
                writer.WriteValue(ll.Id);
                return;
            } // if

            throw new NotSupportedException("Unknown SPDX license type!");
        } // WriteJson()

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>
        /// The object value.
        /// </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = (string)reader.Value;
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentOutOfRangeException("SPDX license statement missing (must not be empty!)");
            } // if

            var license = LicenseExpressionToLicenseObject(token);
            if (license != null)
            {
                return license;
            } // if

            throw new ArgumentOutOfRangeException($"Unknown SPDX license statement: {token}");
        } // ReadJson()

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.
        /// </value>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(AnyLicenseInfo);
        } // CanConvert()

        /// <summary>
        /// Licenses the expression to license object.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="AnyLicenseInfo"/> object or null.</returns>
        public static AnyLicenseInfo LicenseExpressionToLicenseObject(string expression)
        {
            if (expression == Constants.NoAssertion)
            {
                return new SpdxNoAssertionLicense();
            } // if

            if (expression == Constants.None)
            {
                return new SpdxNoneLicense();
            } // if

            if (DataManager != null)
            {
                var license = DataManager.FindLicenseById(expression);
                if (license != null)
                {
                    return license;
                } // if

                // try to parse the expression
                var parsed = DataManager.ParseSpdxExpression(expression);
                var result = BuildLicenseFromParserResult(parsed);
                return result;
            } // if

            return null;
        } // LicenseExpressionToLicenseObject()
        #endregion // PUBLIC METHODS

        //// ---------------------------------------------------------------------

        #region PRIVATE METHODS
        /// <summary>
        /// Builds the license from parser result.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="AnyLicenseInfo"/> object.</returns>
        private static AnyLicenseInfo BuildLicenseFromParserResult(SpdxExpression expression)
        {
            if (expression is SpdxAndExpression andex)
            {
                return GetConjunctiveLicense(andex);
            } // if

            if (expression is SpdxOrExpression orex)
            {
                return GetDisjunctiveLicense(orex);
            } // if

            if (expression is SpdxWithExpression withex)
            {
                var result = new SimpleLicensingInfo();
                result.Id = withex.ToString();
                return result;
            } // if

            if (expression is SpdxLicenseExpression licex)
            {
                var result = new SimpleLicensingInfo();
                result.Id = licex.ToString();
                return result;
            } // if

            if (expression is SpdxLicenseReference refex)
            {
                var result = new SimpleLicensingInfo();
                result.Id = refex.ToString();
                return result;
            } // if

            if (expression is SpdxScopedExpression scopex)
            {
                var result = BuildLicenseFromParserResult(scopex.Expression);
                return result;
            } // if

            throw new ArgumentOutOfRangeException("Invalid SPDX license expression");
        } // BuildLicenseFromParserResult()

        /// <summary>
        /// Gets the conjunctive license.
        /// </summary>
        /// <param name="expressionTree">The expression tree.</param>
        /// <returns>A <see cref="ConjunctiveLicenseSet"/>.</returns>
        private static ConjunctiveLicenseSet GetConjunctiveLicense(SpdxAndExpression expressionTree)
        {
            var left = BuildLicenseFromParserResult(expressionTree.Left);
            var right = BuildLicenseFromParserResult(expressionTree.Right);
            var result = new ConjunctiveLicenseSet(new[] { left, right });
            return result;
        } // GetConjunctiveLicense()

        /// <summary>
        /// Gets the disjunctive license.
        /// </summary>
        /// <param name="expressionTree">The expression tree.</param>
        /// <returns>A <see cref="DisjunctiveLicenseSet"/>.</returns>
        private static DisjunctiveLicenseSet GetDisjunctiveLicense(SpdxOrExpression expressionTree)
        {
            var left = BuildLicenseFromParserResult(expressionTree.Left);
            var right = BuildLicenseFromParserResult(expressionTree.Right);
            var result = new DisjunctiveLicenseSet(new[] { left, right });
            return result;
        } // GetDisjunctiveLicense()
        #endregion // PRIVATE METHODS
    } // JsonDateConverter
}
