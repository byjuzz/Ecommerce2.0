#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Client/Client.Api/Client.Api.csproj", "src/Services/Client/Client.Api/"]
COPY ["src/Common/Common.Logging/Common.Logging.csproj", "src/Common/Common.Logging/"]
COPY ["src/Services/Client/Client.Persistence.Database/Client.Persistence.Database.csproj", "src/Services/Client/Client.Persistence.Database/"]
COPY ["src/Services/Client/Client.Domain/Client.Domain.csproj", "src/Services/Client/Client.Domain/"]
COPY ["src/Services/Client/Client.Service.EventHandlers/Client.Service.EventHandlers.csproj", "src/Services/Client/Client.Service.EventHandlers/"]
COPY ["src/Services/Client/Client.Service.Queries/Client.Service.Queries.csproj", "src/Services/Client/Client.Service.Queries/"]
COPY ["src/Services/Common/Service.Common.Collection/Service.Common.Collection.csproj", "src/Services/Common/Service.Common.Collection/"]
COPY ["src/Services/Common/Service.Common.Mapping/Service.Common.Mapping.csproj", "src/Services/Common/Service.Common.Mapping/"]
COPY ["src/Services/Common/Services.Common.Paging/Services.Common.Paging.csproj", "src/Services/Common/Services.Common.Paging/"]
RUN dotnet restore "./src/Services/Client/Client.Api/Client.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Client/Client.Api"
RUN dotnet build "./Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Client.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.Api.dll"]