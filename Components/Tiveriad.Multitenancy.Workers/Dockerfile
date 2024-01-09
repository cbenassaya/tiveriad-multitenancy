FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Components/Tiveriad.Multitenancy.Workers/Tiveriad.Multitenancy.Workers.csproj", "Tiveriad.Multitenancy.Workers/"]
COPY ["Components/Tiveriad.Multitenancy.Core/Tiveriad.Multitenancy.Core.csproj", "Tiveriad.Multitenancy.Core/"]
COPY ["Components/Tiveriad.Multitenancy.Infrastructure/Tiveriad.Multitenancy.Infrastructure.csproj", "Tiveriad.Multitenancy.Infrastructure/"]
COPY ["Components/Tiveriad.Multitenancy.Persistence/Tiveriad.Multitenancy.Persistence.csproj", "Tiveriad.Multitenancy.Persistence/"]
RUN dotnet restore "Components/Tiveriad.Multitenancy.Workers/Tiveriad.Multitenancy.Workers.csproj"
COPY . .
WORKDIR "/src/Tiveriad.Multitenancy.Workers"
RUN dotnet build "Tiveriad.Multitenancy.Workers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tiveriad.Multitenancy.Workers.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tiveriad.Multitenancy.Workers.dll"]
