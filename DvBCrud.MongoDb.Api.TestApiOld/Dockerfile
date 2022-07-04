FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DvBCrud.MongoDb.Api.TestApi/DvBCrud.MongoDb.Api.TestApi.csproj", "DvBCrud.MongoDb.Api.TestApi/"]
RUN dotnet restore "DvBCrud.MongoDb.Api.TestApi/DvBCrud.MongoDb.Api.TestApi.csproj"
COPY . .
WORKDIR "/src/DvBCrud.MongoDb.Api.TestApi"
RUN dotnet build "DvBCrud.MongoDb.Api.TestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DvBCrud.MongoDb.Api.TestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DvBCrud.MongoDb.Api.TestApi.dll"]
