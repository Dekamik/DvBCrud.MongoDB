#!/bin/bash
set -euo pipefail
IFS=$'\n\t'

tag=$1
key=$2
src=$3

dotnet pack -c Release -p:PackageVersion=${tag} DvBCrud.MongoDb/DvBCrud.MongoDb.csproj
dotnet pack -c Release -p:PackageVersion=${tag} DvBCrud.MongoDb.Api/DvBCrud.MongoDb.Api.csproj

dotnet nuget push DvBCrud.MongoDb/bin/Release/DvBCrud.MongoDb.${tag}.nupkg --api-key ${key} --source ${src}
dotnet nuget push DvBCrud.MongoDb.Api/bin/Release/DvBCrud.MongoDb.Api.${tag}.nupkg --api-key ${key} --source ${src}
