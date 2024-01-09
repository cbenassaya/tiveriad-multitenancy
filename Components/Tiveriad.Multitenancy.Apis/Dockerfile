FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Components/Tiveriad.Multitenancy.Apis/Tiveriad.Multitenancy.Apis.csproj", "Tiveriad.Multitenancy.Apis/"]
COPY ["Components/Tiveriad.Multitenancy.Core/Tiveriad.Multitenancy.Core.csproj", "Tiveriad.Multitenancy.Core/"]
COPY ["Components/Tiveriad.Multitenancy.Applications/Tiveriad.Multitenancy.Applications.csproj", "Tiveriad.Multitenancy.Applications/"]
COPY ["Components/Tiveriad.Multitenancy.Infrastructure/Tiveriad.Multitenancy.Infrastructure.csproj", "Tiveriad.Multitenancy.Infrastructure/"]
COPY ["Components/Tiveriad.Multitenancy.Persistence/Tiveriad.Multitenancy.Persistence.csproj", "Tiveriad.Multitenancy.Persistence/"]
RUN dotnet restore "Components/Tiveriad.Multitenancy.Apis/Tiveriad.Multitenancy.Apis.csproj"
COPY . .
WORKDIR "/src/Tiveriad.Multitenancy.Apis"
RUN dotnet build "Tiveriad.Multitenancy.Apis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tiveriad.Multitenancy.Apis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tiveriad.Multitenancy.Apis.dll"]
