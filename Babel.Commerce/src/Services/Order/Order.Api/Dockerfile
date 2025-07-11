#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Services/Order/Order.Api/Order.Api.csproj", "src/Services/Order/Order.Api/"]
COPY ["src/Common/Common.Logging/Common.Logging.csproj", "src/Common/Common.Logging/"]
COPY ["src/Services/Order/namespace Order.Service.EventHandlers/Order.Service.EventHandlers.csproj", "src/Services/Order/namespace Order.Service.EventHandlers/"]
COPY ["src/Services/Order/Order.Common/Order.Common.csproj", "src/Services/Order/Order.Common/"]
COPY ["src/Services/Order/Order.Persistence.Database/Order.Persistence.Database.csproj", "src/Services/Order/Order.Persistence.Database/"]
COPY ["src/Services/Order/Order.Domain/Order.Domain.csproj", "src/Services/Order/Order.Domain/"]
COPY ["src/Services/Order/Order.Service.Proxies/Order.Service.Proxies.csproj", "src/Services/Order/Order.Service.Proxies/"]
COPY ["src/Services/Order/Order.Service.Queries/Order.Service.Queries.csproj", "src/Services/Order/Order.Service.Queries/"]
COPY ["src/Services/Common/Service.Common.Collection/Service.Common.Collection.csproj", "src/Services/Common/Service.Common.Collection/"]
COPY ["src/Services/Common/Service.Common.Mapping/Service.Common.Mapping.csproj", "src/Services/Common/Service.Common.Mapping/"]
COPY ["src/Services/Common/Services.Common.Paging/Services.Common.Paging.csproj", "src/Services/Common/Services.Common.Paging/"]
RUN dotnet restore "./src/Services/Order/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Order/Order.Api"
RUN dotnet build "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]