// ---------------------------------------------------------------------------
// <copyright file="CompoundPointer.cs" company="Tethys">
//   Copyright (C) 2019 T. Graf
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

namespace Tethys.SPDX.Model.Pointer
{
    /// <summary>
    /// A pointing method made up of a pair of pointers that identify a well defined section
    /// within a document delimited by a begin and an end.
    /// See <see href="http://www.w3.org/2009/pointers"/> and
    /// <see href="https://www.w3.org/WAI/ER/Pointers/WD-Pointers-in-RDF10-20110427"/>.
    /// This is an abstract class of pointers which must be sub-classed.
    /// </summary>
    public abstract class CompoundPointer
    {
        #region PUBLIC PROPERTIES
        /// <summary>
        /// Gets or sets the start pointer.
        /// </summary>
        public SinglePointer StartPointer { get; set; }
        #endregion // PUBLIC PROPERTIES
    } // CompoundPointer
}
