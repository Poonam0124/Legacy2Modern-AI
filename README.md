# Legacy2Modern-AI

**AI-Assisted Legacy .NET Application Modernization**

Legacy2Modern-AI is a portfolio project exploring how AI can help developers understand legacy .NET applications, identify modernization opportunities, and plan changes in a structured, incremental way.

The project uses a sample enterprise application built with ASP.NET Web Forms and .NET Framework 4.8 as its legacy baseline. It combines modernization findings, AI-assisted recommendations, phased planning, and dependency-and-impact analysis to support more informed modernization decisions.

Rather than treating modernization as a complete rewrite, the project focuses on understanding existing business behavior, identifying architectural risks, validating analysis results, and organizing modernization work into actionable steps.

## Key Features

### 1. Legacy Application Baseline

A sample customer and service-request management application provides a realistic starting point for modernization analysis.

The legacy application includes:

- ASP.NET Web Forms and C#
- .NET Framework 4.8
- Entity Framework 6 with EDMX / Database First
- Microsoft SQL Server / LocalDB
- Business and data layers using service and repository patterns

The baseline intentionally retains architectural concerns that can be investigated and addressed through future modernization work.

### 2. AI-Assisted Modernization Analysis

The analysis workflow uses existing findings and application context to generate structured modernization recommendations.

Its architecture separates responsibilities such as:

- Analysis orchestration
- Prompt construction
- AI request construction
- Provider integration
- Response parsing and validation
- Results presentation

This separation helps keep the workflow maintainable and testable as requirements evolve.

### 3. Structured Modernization Planning

The planning workflow represents modernization work as phases and individual plan items.

Plan data can include:

- Modernization strategy and target architecture
- Plan phases and actionable tasks
- Priority, risk, complexity, and estimated effort
- Related modernization findings
- Dependencies between plan items
- Affected components
- Reasoning and implementation steps

The structured plan provides a basis for organizing modernization into smaller, reviewable changes.

### 4. Dependency and Impact Analysis

The dependency-and-impact workflow builds on modernization findings and the modernization plan.

Its structured results support:

- Relationships between modernization plan items
- Dependency descriptions and their impacts
- Affected components and supporting reasons
- Impact areas and impact levels
- Risks and mitigation strategies
- Validation of dependency and finding references

The Web Forms application includes a results UI for reviewing the analysis.

### 5. Pluggable AI Provider Architecture

The AI integration separates modernization workflows from provider-specific implementation details.

The current architecture includes:

- AI provider abstractions
- An Ollama-based provider
- Mock providers for predictable development and testing scenarios
- Response parsing and validation
- Configurable provider fallback

The documented configuration uses Ollama as the primary provider and a mock provider as the fallback. Fallback behavior is configurable through application settings.

### 6. Automated Testing

The project includes a dedicated MSTest project targeting .NET Framework 4.8.

**Latest verified test result: 99 passed, 0 failed.**

Tests cover request and prompt construction, response parsing and validation, modernization planning, dependency-and-impact analysis, provider factories, orchestration, and fallback behavior.

The unit tests use test doubles where appropriate, so the tested workflows do not require a running AI provider for every test.

See [TESTING.md](TESTING.md) for prerequisites, instructions, coverage details, and testing limitations.

## Technology Stack

| Area | Technology |
|---|---|
| Programming language | C# |
| Legacy web framework | ASP.NET Web Forms |
| Runtime | .NET Framework 4.8 |
| ORM | Entity Framework 6 |
| Database | Microsoft SQL Server / LocalDB |
| Data modeling | EDMX / Database First |
| Application design | Layered architecture |
| AI integration | Ollama and mock providers |
| AI model documented in the project | Qwen3 4B |
| Automated testing | MSTest |
| Development environment | Visual Studio |
| Version control | Git and GitHub |

## Architecture Overview

The core AI-assisted analysis workflow separates application-level processing from AI provider integration.

```text
Legacy2Modern.Web
        |
        v
ModernizationAnalysisService
        |
        +-----------------------+
        |                       |
        v                       v
   Prompt Builder         Request Builder
        |                       |
        +-----------+-----------+
                    |
                    v
          ModernizationAIService
                    |
                    v
          AIProviderOrchestrator
                    |
             +------+------+
             |             |
             v             v
           Ollama         Mock
           Primary      Fallback

Additional workflows:
- Modernization planning
- Dependency and impact analysis
- Response parsing and validation
- Results presentation
```

The architecture is designed to keep analysis, request construction, provider execution, validation, and presentation as distinct responsibilities.

The project also documents the legacy application's current architecture and the technical debt that forms the basis for modernization analysis.

## Repository Structure

```text
Legacy2Modern-AI/
├── docs/
│   ├── adr/
│   │   └── ADR-001-incremental-modernization.md
│   └── architecture/
│       ├── AI_MODERNIZATION_ARCHITECTURE.md
│       ├── LEGACY_ARCHITECTURE.md
│       ├── LEGACY_ASSESSMENT.md
│       └── system-overview.md
├── legacy/
│   └── Legacy2Modern.Legacy/
│       ├── Legacy2Modern.Business/
│       ├── Legacy2Modern.Data/
│       ├── Legacy2Modern.Web/
│       └── Legacy2Modern.Legacy.sln
├── tests/
│   └── Legacy2Modern.Business.Tests/
│       ├── AIProviderFactoryTests.cs
│       ├── AIProviderOrchestratorTests.cs
│       ├── ModernizationDependencyImpactAnalysisServiceTests.cs
│       ├── ModernizationDependencyImpactPromptBuilderTests.cs
│       ├── ModernizationDependencyImpactRequestBuilderTests.cs
│       ├── ModernizationDependencyImpactResponseParserTests.cs
│       ├── ModernizationDependencyImpactResponseValidatorTests.cs
│       ├── ModernizationPlanningPromptBuilderTests.cs
│       ├── ModernizationPlanningRequestBuilderTests.cs
│       ├── ModernizationPlanningResponseParserTests.cs
│       └── ModernizationPlanningServiceTests.cs
├── .gitignore
├── README.md
└── TESTING.md
```

This is a simplified view of the repository; it omits individual application files and generated build output.

## Documentation

- [System Overview](docs/architecture/system-overview.md) — high-level architecture and guiding principles.
- [Legacy Architecture](docs/architecture/LEGACY_ARCHITECTURE.md) — legacy technology stack, design, workflow rules, and architectural characteristics.
- [Legacy Assessment](docs/architecture/LEGACY_ASSESSMENT.md) — identified technical debt, risks, and modernization priorities.
- [AI Modernization Architecture](docs/architecture/AI_MODERNIZATION_ARCHITECTURE.md) — AI workflow, planning models, dependency-impact analysis, validation, and implementation boundaries.
- [Incremental Modernization ADR](docs/adr/ADR-001-incremental-modernization.md) — architecture decision record.
- [Testing Guide](TESTING.md) — how to run the automated tests and understand their scope.

## Current Project Status

The project includes an implemented legacy application baseline, AI-assisted modernization analysis, structured modernization planning, dependency-and-impact analysis, response validation, provider orchestration, and results presentation.

The automated unit-test suite has a latest verified result of 99 passed and 0 failed.

The project is evolving incrementally. Passing unit tests do not establish that all live AI integrations, end-to-end workflows, deployment scenarios, or performance characteristics have been verified.

## Future Direction

Potential areas for further development include:

- Improving dependency graph analysis, including additional cycle detection and validation.
- Expanding automated test coverage.
- Evaluating additional AI providers and models.
- Improving modernization recommendations and implementation planning.
- Enhancing analysis-result presentation.
- Documenting a complete local setup and demonstration workflow.
- Exploring modernization toward a more modular, modern application architecture.

These are potential extensions, not claims that the corresponding features are already implemented.

## Why This Project?

Legacy modernization requires more than replacing an old framework with a new one. Developers must understand existing business rules, uncover dependencies, evaluate technical risks, and plan changes without disrupting existing functionality.

Legacy2Modern-AI explores how structured analysis and AI assistance can support that process while keeping developers responsible for reviewing and approving modernization decisions.

The project demonstrates practical work with C#, legacy .NET applications, layered architecture, AI provider integration, response validation, automated testing, and incremental modernization planning.