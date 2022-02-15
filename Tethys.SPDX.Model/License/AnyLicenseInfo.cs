// ---------------------------------------------------------------------------
// <copyright file="AnyLicenseInfo.cs" company="Tethys">
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

namespace Tethys.SPDX.Model.License
{
    /// <summary>
    /// This abstract class represents several ways of describing licensing information.
    /// License info can be described as a set of conjunctive licenses (where all licenses
    /// terms must apply), a set of disjunctive licenses (where there is a choice of one
    /// License among the set described) or a specific License. The specific licenses
    /// are of a SimpleLicensingInfoType.
    /// </summary>
    public abstract class AnyLicenseInfo
    {
        // no properties, no methods
    } // AnyLicenseInfo
}
