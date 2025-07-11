#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateways/Api.Gateway.WebClient/Api.Gateway.WebClient.csproj", "src/Gateways/Api.Gateway.WebClient/"]
COPY ["src/Gateways/Api.Gateway.Proxies/Api.Gateway.Proxies.csproj", "src/Gateways/Api.Gateway.Proxies/"]
COPY ["src/Gateways/Api.Gateway.Models/Api.Gateway.Models.csproj", "src/Gateways/Api.Gateway.Models/"]
RUN dotnet restore "./src/Gateways/Api.Gateway.WebClient/Api.Gateway.WebClient.csproj"
COPY . .
WORKDIR "/src/src/Gateways/Api.Gateway.WebClient"
RUN dotnet build "./Api.Gateway.WebClient.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.Gateway.WebClient.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Gateway.WebClient.dll"]