# -------------------------------------------------------
# Create a folder with all binary NuGet packages

# SPDX-FileCopyrightText: (c) 2024 T. Graf
# SPDX-License-Identifier: Apache-2.0
# -------------------------------------------------------

$packagefolder = "./packages"

# 1. Create folder if it does not yet exist
New-Item -ItemType Directory -Force -Path $packagefolder

# 2. Get all NuGet packages from Release folders
Write-Host "Getting all NuGet packages..."
$packages = Get-ChildItem -Path . -Recurse -Filter "*.nupkg"
foreach ($p in $packages) {
    if (-Not $p.FullName.Contains("Release")) {
        continue
    }
    Write-Host $p.FullName
    Copy-Item -Path $p.FullName -Destination $packagefolder
}