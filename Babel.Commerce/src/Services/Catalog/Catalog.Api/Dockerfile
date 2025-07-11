#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Catalog/Catalog.Api/Catalog.Api.csproj", "src/Services/Catalog/Catalog.Api/"]
COPY ["src/Common/Common.Logging/Common.Logging.csproj", "src/Common/Common.Logging/"]
COPY ["src/Services/Catalog/Catalog.Persistence.Database/Catalog.Persistence.Database.csproj", "src/Services/Catalog/Catalog.Persistence.Database/"]
COPY ["src/Services/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Services/Catalog/Catalog.Domain/"]
COPY ["src/Services/Catalog/Catalog.Service.EventHandlers/Catalog.Service.EventHandlers.csproj", "src/Services/Catalog/Catalog.Service.EventHandlers/"]
COPY ["src/Services/Catalog/Catalog.Common/Catalog.Common.csproj", "src/Services/Catalog/Catalog.Common/"]
COPY ["src/Services/Catalog/Catalog.Service.Queries/Catalog.Service.Queries.csproj", "src/Services/Catalog/Catalog.Service.Queries/"]
COPY ["src/Services/Common/Service.Common.Collection/Service.Common.Collection.csproj", "src/Services/Common/Service.Common.Collection/"]
COPY ["src/Services/Common/Service.Common.Mapping/Service.Common.Mapping.csproj", "src/Services/Common/Service.Common.Mapping/"]
COPY ["src/Services/Common/Services.Common.Paging/Services.Common.Paging.csproj", "src/Services/Common/Services.Common.Paging/"]
RUN dotnet restore "./src/Services/Catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Catalog/Catalog.Api"
RUN dotnet build "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]