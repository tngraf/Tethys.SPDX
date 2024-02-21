<!-- 
SPDX-FileCopyrightText: (c) 2019-2024 T. Graf
SPDX-License-Identifier: Apache-2.0
-->

# Tethys.SPDX

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
  This is now a propery of `SpdxItem`.
* have the updated SPDX license list.
* Improve parsing of FOSSology 4.3 SPDX files.

## 1.0.1 (2023-03-31)

* be more compatible to SPDX 2.3 files created by FOSSology.

## 1.0.0 (2022-02-15)

* first version released.
