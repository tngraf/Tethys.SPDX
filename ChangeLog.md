<!-- 
SPDX-FileCopyrightText: (c) 2019-2024 T. Graf
SPDX-License-Identifier: Apache-2.0
-->

# Tethys.SPDX

## 2.1.2 (2024-05-08)

* Fixing SPDX validation issues
  * It must be `OPERATING_SYSTEM` even though the SPDX documentation (HTML) says `OPERATING-SYSTEM`.
  * It must be `hasExtractedLicensingInfos` and not `hasExtractedLicenseInfos`.
  * File information of an `SpdxPackage` is nor correctly exported as a `hasFiles` array.

## 2.1.1 (2024-04-08)

* Multitarget in order to avoid System.Text.Json dependency when it is not needed.

## 2.1.0 (2024-03-24)

* `Tethys.SPDX.KnownLicenses`: use `System.Text.Json` instead of `Newtonsoft.Json`.
* All packages use `netstandard2.0`.
* Have basic GitHub actions.

## 2.0.0 (2024-02-21)

* Incompatible changes:
  * Namespace change: `Tethys.SimpleSpdxParser` moved to `Tethys.SPDX.SimpleSpdxParser`
  * RelationshipType has now enum values according to C# naming rules.
  * In the past all packages, files and snippets where related. This has now been changed.
    There are separate lists of packages, files and snippets, relations and documentDecribes.
* `KnownLicenseManager` can read license exceptions.
* Data model fully support SPDX 2.3.
* Support of JSON file format for writing and reading.
* SPDX license list updated to version 3.22 or 2023-10-05.
* SPDX expression parser added.

## 1.1.0

* handle `spdx:ListedLicense` correctly.
* support `spdx:attributionText`.  
  This is now a property of `SpdxItem`.
* have the updated SPDX license list.
* Improve parsing of FOSSology 4.3 SPDX files.

## 1.0.1 (2023-03-31)

* be more compatible to SPDX 2.3 files created by FOSSology.

## 1.0.0 (2022-02-15)

* first version released.
