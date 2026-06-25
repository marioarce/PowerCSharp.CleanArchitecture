# Stage 1: Restore
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src

COPY NuGet.Config global.json ./
COPY PowerCSharp.CleanArchitecture.sln ./

COPY src/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj                              src/CleanArchitecture.Domain/
COPY src/CleanArchitecture.Shared/CleanArchitecture.Shared.csproj                              src/CleanArchitecture.Shared/
COPY src/CleanArchitecture.Application/CleanArchitecture.Application.csproj                    src/CleanArchitecture.Application/
COPY src/CleanArchitecture.Operational/CleanArchitecture.Operational.csproj                    src/CleanArchitecture.Operational/
COPY src/CleanArchitecture.Infrastructure/CleanArchitecture.Infrastructure.csproj              src/CleanArchitecture.Infrastructure/
COPY src/CleanArchitecture.Presentation/CleanArchitecture.Presentation.csproj                  src/CleanArchitecture.Presentation/
COPY src/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj                              src/CleanArchitecture.WebApi/
COPY tests/CleanArchitecture.Tests.Shared/CleanArchitecture.Tests.Shared.csproj               tests/CleanArchitecture.Tests.Shared/
COPY tests/CleanArchitecture.WebApi.UnitTests/CleanArchitecture.WebApi.UnitTests.csproj        tests/CleanArchitecture.WebApi.UnitTests/
COPY tests/CleanArchitecture.WebApi.IntegrationTests/CleanArchitecture.WebApi.IntegrationTests.csproj tests/CleanArchitecture.WebApi.IntegrationTests/
COPY Directory.Build.props ./

RUN dotnet restore src/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj

# Stage 2: Publish
FROM restore AS publish
COPY src/ src/

RUN dotnet publish src/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "CleanArchitecture.WebApi.dll"]
