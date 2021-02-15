#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

tag=$1
key=$2
src=$3

dotnet pack -c Release -p:PackageVersion=${tag} DvBCrud.MongoDB/DvBCrud.MongoDB.csproj
dotnet pack -c Release -p:PackageVersion=${tag} DvBCrud.MongoDB.API/DvBCrud.MongoDB.API.csproj

dotnet nuget push DvBCrud.MongoDB/bin/Release/DvBCrud.MongoDB.${tag}.nupkg --api-key ${key} --source ${src}
dotnet nuget push DvBCrud.MongoDB.API/bin/Release/DvBCrud.MongoDB.API.${tag}.nupkg --api-key ${key} --source ${src}
