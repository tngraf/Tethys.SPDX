// ---------------------------------------------------------------------------
// <copyright file="InvalidSpdxAnalysisException.cs" company="Tethys">
//   Copyright (C) 2018 T. Graf
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

    /// <summary>
    /// Exception for invalid SPDX Documents.
    /// </summary>
    /// <seealso cref="Exception" />
    [SerializableAttribute]
    public class InvalidSpdxAnalysisException : Exception
    {
        #region CONSTRUCTION
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSpdxAnalysisException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidSpdxAnalysisException(string message)
            : base(message)
        {
        } // InvalidSpdxAnalysisException()

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSpdxAnalysisException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference (<see langword="Nothing" /> in Visual Basic) if no inner exception is specified.</param>
        public InvalidSpdxAnalysisException(string message, Exception innerException)
            : base(message, innerException)
        {
        } // InvalidSpdxAnalysisException()
        #endregion // CONSTRUCTION
    } // InvalidSpdxAnalysisException
}
