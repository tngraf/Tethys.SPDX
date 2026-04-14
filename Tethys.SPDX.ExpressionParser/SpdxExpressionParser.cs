// ---------------------------------------------------------------------------
// <copyright file="SpdxExpressionParser.cs" company="Tethys">
//   Copyright (C) 2023-2026 T. Graf
//   Copyright(C) 2026 Simon Ensslen
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

namespace Tethys.SPDX.ExpressionParser
{
    using System;

    /*************************************************************************
    * SPDX Expressions
    * ----------------
    * idstring = 1*(ALPHA / DIGIT / "-" / "." )
    *
    * license-id = <short form license identifier in Annex A.1>
    *
    * license-exception-id = <short form license exception identifier in Annex A.2>
    *
    * license-ref = ["DocumentRef-"(idstring)":"]"LicenseRef-"(idstring)
    *
    * simple-expression = license-id / license-id"+" / license-ref
    *
    * compound-expression = (simple-expression /
    *    simple-expression "WITH" license-exception-id /
    *    compound-expression "AND" compound-expression /
    *    compound-expression "OR" compound-expression /
    *    "(" compound-expression ")" )
    *
    * license-expression = (simple-expression / compound-expression)
    *
    ************************************************************************/

    /// <summary>
    /// Represents an SPDX expression.
    /// </summary>
    public static class SpdxExpressionParser
    {
        #region PRIVATE TYPES
        /// <summary>
        /// Parser context - to have a re-entrant SPDX expression parser.
        /// </summary>
        private sealed class ParserContext
        {
            #region PUBLIC PROPERTIES
            /// <summary>
            /// Gets the tokens.
            /// </summary>
            public string[] Tokens { get; }

            /// <summary>
            /// Gets or sets the position.
            /// </summary>
            public int Position { get; set; } = -1;

            /// <summary>
            /// Gets the method to tell whether this is a SPDX identifier.
            /// </summary>
            public Func<string, bool> IsSpdxIdentifier { get; }

            /// <summary>
            /// Gets the method to tell whether this is a SPDX exception.
            /// </summary>
            public Func<string, bool> IsSpdxException { get; }

            /// <summary>
            /// Gets the options.
            /// </summary>
            public SpdxParsingOptions Options { get; }
            #endregion // PUBLIC PROPERTIES

            /// <summary>
            /// Initializes a new instance of the <see cref="ParserContext"/> class.
            /// </summary>
            /// <param name="tokens">The tokens.</param>
            /// <param name="isIdentifier">The is identifier.</param>
            /// <param name="isException">The is exception.</param>
            /// <param name="parsingOptions">The parsing options.</param>
            public ParserContext(
                string[] tokens,
                Func<string, bool> isIdentifier,
                Func<string, bool> isException,
                SpdxParsingOptions parsingOptions = SpdxParsingOptions.Default)
            {
                this.Tokens = tokens;
                this.IsSpdxIdentifier = isIdentifier;
                this.IsSpdxException = isException;
                this.Options = parsingOptions;
            } // ParserContext
        } // ParserContext
        #endregion // PRIVATE TYPES

        //// ---------------------------------------------------------------------

        /// <summary>
        /// Parses a SPDX expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="isIdentifier">The is identifier.</param>
        /// <param name="isException">The is exception.</param>
        /// <param name="parsingOptions">The options.</param>
        /// <returns>
        /// A <see cref="SpdxExpression" />.
        /// </returns>
        /// <exception cref="SpdxExpressionException">
        /// Exception for parsing problems.
        /// </exception>
        public static SpdxExpression Parse(
            string expression,
            Func<string, bool> isIdentifier,
            Func<string, bool> isException,
            SpdxParsingOptions parsingOptions = SpdxParsingOptions.Default)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            } // if

            if (isIdentifier is null)
            {
                throw new ArgumentNullException(nameof(isIdentifier));
            } // if

            if (isException is null)
            {
                throw new ArgumentNullException(nameof(isException));
            } // if

            // ensure that we detect all parenthesis
            expression = expression.Replace("(", " ( ");
            expression = expression.Replace(")", " ) ");

            // very much simplified ...
            string[] tokens = expression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            ParserContext context = new ParserContext(tokens, isIdentifier, isException, parsingOptions);

            var current = GetNextToken(context);
            if (current == null)
            {
                throw new SpdxExpressionException(string.Empty);
            } // if

            var expr = ParseOr(context);

            return expr;
        } // Parse()

        /// <summary>
        /// Parses an "and" expression.
        /// </summary>
        /// <returns>A <see cref="SpdxExpression"/>.</returns>
        private static SpdxExpression ParseAnd(ParserContext context)
        {
            SpdxExpression expression;
            var currentToken = GetCurrentToken(context);
            if (currentToken.Type == TokenType.Left)
            {
                expression = ParseScopedExpression(context);
            }
            else
            {
                expression = ParseLicense(context);
            } // if

            currentToken = GetCurrentToken(context);
            while (currentToken.Type == TokenType.And)
            {
                GetNextToken(context);
                expression = new SpdxAndExpression(expression, ParseAnd(context));
                currentToken = GetCurrentToken(context);
            } // while

            return expression;
        } // ParseAnd()

        /// <summary>
        /// Parses an "or" expression.
        /// </summary>
        /// <returns>A <see cref="SpdxExpression"/>.</returns>
        private static SpdxExpression ParseOr(ParserContext context)
        {
            var expression = ParseAnd(context);
            var currentToken = GetCurrentToken(context);
            while (currentToken?.Type == TokenType.Or)
            {
                GetNextToken(context);
                expression = new SpdxOrExpression(expression, ParseAnd(context));
                currentToken = GetCurrentToken(context);
            } // while

            return expression;
        } // ParseOr()

        /// <summary>
        /// Parses a scoped expression.
        /// </summary>
        /// <returns>A <see cref="SpdxExpression"/>.</returns>
        private static SpdxExpression ParseScopedExpression(ParserContext context)
        {
            GetNextToken(context);
            var expression = ParseOr(context);
            if (GetCurrentToken(context).Type != TokenType.Right)
            {
                throw new SpdxExpressionException("Unexpected end of expression.");
            } // if

            GetNextToken(context);

            return new SpdxScopedExpression(expression);
        } // ParseScopedExpression()

        /// <summary>
        /// Determines whether this expression contains invalid characters.
        /// Allowed are (ALPHA / DIGIT / "-" / "." ).
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        ///   <c>true</c> if this expression contains invalid characters; otherwise, <c>false</c>.
        /// </returns>
        private static bool ContainsInvalidCharacters(string expression)
        {
            foreach (var c in expression)
            {
                if (c != '+' && c != '.' && c != '-' && c != '(' && c != ')' && !char.IsDigit(c) && !char.IsLetter(c))
                {
                    return true;
                } // if
            } // foreach

            return false;
        } // ContainsInvalidCharacters()

        /// <summary>
        /// Parses a license.
        /// </summary>
        /// <returns>A <see cref="SpdxExpression"/>.</returns>
        private static SpdxExpression ParseLicense(ParserContext context)
        {
            var token = GetCurrentToken(context);
            if (token.Type == TokenType.LicenseId)
            {
                if ((context.Options & SpdxParsingOptions.AllowUnknownLicenses) == 0
                    && !context.IsSpdxIdentifier(token.Value.TrimEnd('+')))
                {
                    throw new SpdxExpressionException("Invalid/unknown SPDX license id");
                } // if

                var tokenNext = PeekNextToken(context);
                if (tokenNext?.Type == TokenType.With)
                {
                    var t2 = PeekNextNextToken(context);
                    if (t2?.Type == TokenType.Exception)
                    {
                        GetNextToken(context);
                        GetNextToken(context);
                        GetNextToken(context);

                        return new SpdxWithExpression(
                            GetLicenseExpression(token.Value),
                            t2.Value);
                    } // if
                } // if

                GetNextToken(context);

                return GetLicenseExpression(token.Value);
            } // if

            if (token.Type == TokenType.LicenseRef)
            {
                GetNextToken(context);
                return new SpdxLicenseReference(token.Value);
            } // if

            return null;
        } // ParseLicense()

        /// <summary>
        /// Gets a license expression from the given string.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>A <see cref="SpdxLicenseExpression"/>.</returns>
        private static SpdxLicenseExpression GetLicenseExpression(string expression)
        {
            if (expression.EndsWith("+"))
            {
                return new SpdxLicenseExpression(expression.TrimEnd('+'), true);
            } // if

            return new SpdxLicenseExpression(expression, false);
        } // GetLicenseExpression()

        /// <summary>
        /// Gets a token from the given text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="context">The parser context.</param>
        /// <returns>A <see cref="Token"/>.</returns>
        private static Token GetToken(string text, ParserContext context)
        {
            var textCompare = text.Trim().ToLower();
            if (textCompare == "(")
            {
                return new Token(TokenType.Left, string.Empty);
            } // if

            if (textCompare == ")")
            {
                return new Token(TokenType.Right, string.Empty);
            } // if

            if (textCompare == "and")
            {
                return new Token(TokenType.And, "AND");
            } // if

            if (textCompare == "or")
            {
                return new Token(TokenType.Or, "OR");
            } // if

            if (textCompare.Contains("with"))
            {
                return new Token(TokenType.With, text);
            } // if

            if (textCompare.StartsWith("licenseref"))
            {
                return new Token(TokenType.LicenseRef, text);
            } // if

            if (textCompare.EndsWith("+"))
            {
                return new Token(TokenType.LicenseId, text);
            } // if

            if (context.IsSpdxIdentifier(textCompare))
            {
                return new Token(TokenType.LicenseId, text);
            } // if

            if (context.IsSpdxException(textCompare))
            {
                return new Token(TokenType.Exception, text);
            } // if

            if (ContainsInvalidCharacters(textCompare))
            {
                throw new SpdxExpressionException("Invalid characters found");
            } // if

            if ((context.Options & SpdxParsingOptions.AllowUnknownExceptions) != 0)
            {
                return new Token(TokenType.Exception, text);
            } // if

            throw new SpdxExpressionException($"Unknown token: {text}");
        } // GetToken()

        /// <summary>
        /// Gets the current token.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>A <see cref="Token"/>.</returns>
        private static Token GetCurrentToken(ParserContext context)
        {
            if (context.Position < context.Tokens.Length)
            {
                return GetToken(context.Tokens[context.Position], context);
            } // if

            return null;
        }

        /// <summary>
        /// Gets the next token.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>A <see cref="Token"/> or null.</returns>
        private static Token GetNextToken(ParserContext context)
        {
            if (context.Position < context.Tokens.Length - 1)
            {
                return GetToken(context.Tokens[++context.Position], context);
            } // if

            return null;
        } // GetNextToken()

        /// <summary>
        /// Peeks the next token.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>A <see cref="Token"/> or null.</returns>
        private static Token PeekNextToken(ParserContext context)
        {
            if (context.Position < context.Tokens.Length - 1)
            {
                return GetToken(context.Tokens[context.Position + 1], context);
            } // if

            return null;
        } // PeekNextToken()

        /// <summary>
        /// Peeks the next token.
        /// </summary>
        /// <param name="context">The parser context.</param>
        /// <returns>
        /// A <see cref="Token" /> or null.
        /// </returns>
        private static Token PeekNextNextToken(ParserContext context)
        {
            if (context.Position < context.Tokens.Length - 2)
            {
                return GetToken(context.Tokens[context.Position + 2], context);
            } // if

            return null;
        } // PeekNextNextToken()
    } // SpdxExpression
}
