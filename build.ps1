# -------------
# Build project
# -------------

dotnet restore

dotnet build --configuration Release --no-restore
