# ---------------------------------------------
# Build project

# SPDX-FileCopyrightText: (c) 2019-2023 T. Graf
# SPDX-License-Identifier: Apache-2.0
# ---------------------------------------------

dotnet restore

dotnet build --configuration Release --no-restore
