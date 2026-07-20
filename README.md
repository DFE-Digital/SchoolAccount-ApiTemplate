# Introduction

SchoolAccount-ApiTemplate is a template for building ASP.NET Core Web APIs for the DfE School Account service. It
provides a minimal clean architecture solution, with CQRS abstractions, structured logging, error handling, and
architecture tests already wired up, so new backend services can start from a consistent, proven baseline rather than
from scratch.

## Documentation

- [Clean Architecture](docs/clean-architecture.md) - layers, dependency rules, and code organisation

Architecture decisions are recorded as ADRs in the [decisions](decisions) folder:

- [Use Markdown Architectural Decision Records](decisions/0001-record-architecture-decisions.md) - why and how we record decisions
- [Structure the solution using clean architecture](decisions/0002-use-clean-architecture.md) - layers, dependency rules, and code organisation
- [Strip the imported template to a minimal core](decisions/0003-strip-imported-template-to-minimal-core.md) - what was removed from the original template and why

New decisions should follow the [ADR template](decisions/0000-adr-template.md).

# Getting Started

Follow these steps to start the API locally.

1. Install prerequisites:
    - [.NET 10 SDK](https://dotnet.microsoft.com/download)
    - [Docker Desktop](https://www.docker.com/products/docker-desktop/)
    - Rider, Visual Studio, or Visual Studio Code

2. Run the API using one of the following:

   | Method         | Command                              | Outcome                                                              |
   |----------------|--------------------------------------|----------------------------------------------------------------------|
   | Docker Compose | `docker compose up --build`          | Starts the API and its dependencies (Seq) in containers              |
   | .NET CLI       | `dotnet run --project src/Web.Api`   | Runs the API directly using the `http` launch profile, no containers |

   In Rider or Visual Studio you can use the equivalent `docker-compose` or `http` run configurations from the toolbar.

3. Once running, the API is available at `http://localhost:5100`:
    - Interactive API reference (Scalar) at `http://localhost:5100/scalar/v1`
    - Health checks at `http://localhost:5100/health`
    - Logs (if started with compose) at `http://localhost:8081`

   > The Scalar API reference is only mapped in the `Development` environment.

4. Debugging guidance:
    - Set breakpoints in your C# files under `src/` and start either run configuration with debugging enabled.
    - `.http` files alongside the endpoints in `src/Web.Api/Endpoints` can be used to exercise the API from your IDE.

# Build and Test

Use the .NET CLI to build or test the solution.

- To build locally:

  ```bash
  dotnet build SchoolAccount.ApiTemplate.slnx
  ```

- To run all tests:

  ```bash
  dotnet test **/*Tests/*.csproj
  ```

Architecture tests under `tests/ArchitectureTests` enforce the clean architecture dependency rules between layers.

## Architecture

The solution follows a clean architecture pattern with vertical slice features:

| Project          | Purpose                                                      |
|------------------|--------------------------------------------------------------|
| `Web.Api`        | ASP.NET Core Web API - endpoints, middleware, error handling |
| `Application`    | CQRS handlers and feature logic, organised by feature folder |
| `Domain`         | Domain entities and business rules                           |
| `Infrastructure` | External concerns - time, data access, integrations          |
| `SharedKernel`   | Shared primitives - `Result<T>`, `Error`, `ValidationError`  |

Each endpoint implements `IEndpoint` and is discovered and mapped automatically at startup. See
[Structure the solution using clean architecture](decisions/0002-use-clean-architecture.md) for the dependency rules.

## Logging

Structured logs are written via Serilog to [Seq](https://datalust.co/seq). When running via Docker Compose, the Seq UI
is available at http://localhost:8081.

## Contributing

1. Branch from `main` using the convention `task/<short-description>` or `feature/<short-description>`.
2. Open a [pull request](https://github.com/DFE-Digital/SchoolAccount-ApiTemplate/pulls) against `main`.
3. The [build workflow](.github/workflows/build.yml) must pass before merging.
