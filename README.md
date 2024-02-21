<!-- 
SPDX-FileCopyrightText: (c) 2019-2023 T. Graf
SPDX-License-Identifier: Apache-2.0
-->

# Tethys.SPDX

![License](https://img.shields.io/badge/license-Apache--2.0-blue.svg)
[![Build status](https://ci.appveyor.com/api/projects/status/beu5qpnvi2urf0i0?svg=true)](https://ci.appveyor.com/project/tngraf/tethys-spdx)
[![Nuget](https://img.shields.io/badge/nuget-2.0.0-brightgreen.svg)](https://www.nuget.org/packages/Tethys.SPDX.KnownLicenses/2.0.0)
[![REUSE status](https://api.reuse.software/badge/git.fsfe.org/reuse/api)](https://api.reuse.software/info/git.fsfe.org/reuse/api)
[![SBOM](https://img.shields.io/badge/SBOM-CycloneDX-brightgreen)](https://github.com/tngraf/Tethys.Dgml/blob/master/SBOM/sbom.cyclonedx.xml)

The [Software Package Data Exchange (SPDX)](https://spdx.dev/) is an open standard for describing a software bill of material.
SPDX focuses especially on licensing and copyright imformation.

License and copyright scanners like [FOSSology](https://www.fossology.org/) for example use this standard
to provide their scan results.

Having a format to describe all the different scenarios of license findings, license relations,
and copyright findings is not trivial, so SPDX can get quite complex.

The libraries in this project support two tasks:

* load the SPDX license list and provide it to .Net applications
* parse SPDX files and provide the contained data in a .Net way

Please note that due to the complexity of the SPDX standard not all
possible SPDX files can be parsed. But the libraries are open source, so feel
free to enhance them.

## Get Package

The following packages are available on NuGet:

* [Tethys.SPDX.Interfaces](https://www.nuget.org/packages/Tethys.SPDX.Interfaces)
* [Tethys.SPDX.KnownLicenses](https://www.nuget.org/packages/Tethys.SPDX.KnownLicenses)
* [Tethys.SPDX.ExpressionParser](https://www.nuget.org/packages/Tethys.SPDX.ExpressionParser)
* [Tethys.SPDX.Model](https://www.nuget.org/packages/Tethys.SPDX.Model)
* [Tethys.SPDX.SimpleSpdxParser](https://www.nuget.org/packages/Tethys.SPDX.SimpleSpdxParser)
* [Tethys.SPDX.Support](https://www.nuget.org/packages/Tethys.SPDX.Support)
* [Tethys.SPDX.Writer](https://www.nuget.org/packages/Tethys.SPDX.Writer)

This library has been influenced by the following GitHub projects:

* https://github.com/jslicense/spdx-expression-parse.js
* https://github.com/microsoft/spdx-simplify
* https://github.com/spdx/Spdx-Java-Library

## SPDX

See https://spdx.org/licenses/ for more details on SPDX, the software
package data exchange format, the SPDX license identifiers
and matching guidelines.

The Software Package Data Exchange (SPDX) Specification
https://spdx.github.io/spdx-spec/

XML data of all SPDX licenses:  
https://github.com/spdx/license-list-XML

## How to use Tethys.SPDX

The SPDX license information is not part of the libraries, you have to
download it from [SPDX License List](https://github.com/spdx/license-list-XML) 
and place it in a local folder.

### Very simple demo

A minimal code snippet looks like this

```code
var knownLicenseManager = new KnownLicenseManager();
knownLicenseManager.LoadSpdxSourceFiles(...SPDX license files...);
knownLicenseManager.LoadSpdxExceptionFiles(...SPDX license exception files...);

var reader = new RdfParser(knownLicenseManager);
var spdxDoc = reader.ReadFromFile(...SPDX file...);
...
```

Just run the demo application

```code
dotnet run --project .\SpdxParserDemo\SpdxParserDemo.csproj .\TestData\yaml-0.1.0.zip.spdx2.rdf.xml

or

dotnet run --project .\SpdxParserDemo\SpdxParserDemo.csproj .\TestData\SPDXJSONExample-v2.3.spdx.json
```

## Build

### Requisites

* Visual Studio 2022
* NuGet access

### Required NuGet Packages

* Tethys.Logging, version 1.6.1
* Newtonsoft.Json, version 13.0.3

### Build Solution

Just use the basic `dotnet` command:

```shell
dotnet build
```

## License

Tethys.SPDX is licensed under the Apache License, Version 2.0.
