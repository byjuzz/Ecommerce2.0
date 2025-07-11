#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Identity/Identity.Api/Identity.Api.csproj", "src/Services/Identity/Identity.Api/"]
COPY ["src/Services/Common/Service.Common.Collection/Service.Common.Collection.csproj", "src/Services/Common/Service.Common.Collection/"]
COPY ["src/Services/Identity/Identity.Service.EventHandlers/Identity.Service.EventHandlers.csproj", "src/Services/Identity/Identity.Service.EventHandlers/"]
COPY ["src/Services/Identity/Identity.Persistence.Database/Identity.Persistence.Database.csproj", "src/Services/Identity/Identity.Persistence.Database/"]
COPY ["src/Services/Identity/Identity.Domain/Identity.Domain.csproj", "src/Services/Identity/Identity.Domain/"]
COPY ["src/Services/Identity/Identity.Service.Queries/Identity.Service.Queries.csproj", "src/Services/Identity/Identity.Service.Queries/"]
COPY ["src/Services/Common/Service.Common.Mapping/Service.Common.Mapping.csproj", "src/Services/Common/Service.Common.Mapping/"]
COPY ["src/Services/Common/Services.Common.Paging/Services.Common.Paging.csproj", "src/Services/Common/Services.Common.Paging/"]
RUN dotnet restore "./src/Services/Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Identity.Api"
RUN dotnet build "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Identity.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]