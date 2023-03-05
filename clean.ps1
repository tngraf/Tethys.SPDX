# ---------------------------------------------
# Clean project

# SPDX-FileCopyrightText: (c) 2019-2023 T. Graf
# SPDX-License-Identifier: Apache-2.0
# ---------------------------------------------

dotnet clean
Remove-Item ".tests" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser.Test\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser.Test\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser.Test\TestResults" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.SimpleSpdxParser.Test\coverage.cobertura.xml" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Interfaces\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Interfaces\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses.Test\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses.Test\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses.Test\TestResults" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.KnownLicenses.Test\coverage.cobertura.xml" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Model\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Model\obj" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Support\bin" -Recurse -erroraction silentlycontinue
Remove-Item "Tethys.SPDX.Support\obj" -Recurse -erroraction silentlycontinue
Remove-Item "TestResults" -Recurse -erroraction silentlycontinue
