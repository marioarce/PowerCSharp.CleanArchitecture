# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

---

## [1.0.0] - 2026-06-24

### Added

- Clean Architecture solution structure: `Domain`, `Shared`, `Application`, `Operational`, `Infrastructure`, `Presentation`, `WebApi`
- CQRS pattern via MediatR 12.3 with `LoggingBehavior<TRequest, TResponse>` pipeline behavior
- FluentValidation 11.9 with automatic assembly scanning and MediatR pipeline integration
- `Ardalis.Result` for consistent result semantics across handlers
- `ApiResponse<T>` / `ApiResponse` HTTP response envelope with automatic `Result` → HTTP status mapping
- `BaseApiController` with `SendAsync<T>()` and `SendAsync()` dispatcher methods
- PowerCSharp Features framework integration (feature module registration, flag resolution, middleware ordering)
- `PowerCSharp.BuiltInFeatures` CORS feature module, configurable via `appsettings.json`
- `PowerCSharp.Feature.Cache.BitFaster` in-memory cache via `ICacheService`
- `PowerCSharp.Feature.Cache.Disk` persistent disk cache via `IDiskCacheService`
- `SamplesFeatureModule` host-local feature with living-documentation endpoints (`/v1/samples`)
- Polly-based resilience: `IRetryPolicyProvider` / `RetryPolicyProvider` in the `Operational` layer
- `IDateTimeProvider` port in Application, `DateTimeProvider` adapter in Infrastructure
- Health check endpoint at `/health`
- Swagger/OpenAPI via Swashbuckle 6.6.2
- `public partial class Program` for `WebApplicationFactory<Program>` test compatibility
- Integration test suite with `SamplesApiFactory`, `DisabledSamplesApiFactory`, and `SamplesCacheEndpointTests`
- `Directory.Build.props` for shared build settings (TargetFramework, Nullable, ImplicitUsings, LangVersion)
- Comprehensive `.editorconfig` enforcing PowerCSharp + Clean Architecture conventions
- MIT License
- `NuGet.Config` pointing exclusively to nuget.org (all PowerCSharp packages are published)
- Multi-stage `Dockerfile` (restore → publish → aspnet:8.0 runtime)
- `docker-compose.yml` for local development (`docker compose up --build`)
- `.template.config/template.json` for `dotnet new powercsharp-cleanarchitecture` support
- `PowerCSharp.CleanArchitecture.Template.csproj` for NuGet template packaging
- GitHub Actions CI workflow (build + test on push/PR to `main`)
- GitHub Actions release workflow (tag-triggered GitHub Release + NuGet publish)
- `global.json` with pinned .NET 8 SDK version
- `CONTRIBUTING.md`, `CODE_OF_CONDUCT.md`, `SECURITY.md`
- GitHub issue templates (bug report, feature request)
- GitHub pull request template
- `.github/dependabot.yml` for automated dependency updates

[Unreleased]: https://github.com/marioarce/PowerCSharp.CleanArchitecture/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/marioarce/PowerCSharp.CleanArchitecture/releases/tag/v1.0.0
