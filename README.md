# Legacy2Modern-AI

**AI-Assisted Legacy .NET Application Modernization**

Legacy2Modern-AI is a portfolio project that explores how AI can assist developers in understanding, analyzing, and planning the modernization of legacy .NET applications.

The project uses a legacy ASP.NET Web Forms application as its modernization baseline. It analyzes existing modernization findings and builds structured recommendations, modernization plans, dependency relationships, and impact assessments to help developers make informed modernization decisions.

The goal is to demonstrate a practical approach to legacy application modernization while preserving existing business functionality and reducing migration risks.

## Project Goals

- Identify architectural and maintainability concerns in legacy applications.
- Generate structured modernization recommendations from existing findings.
- Develop phased modernization plans.
- Analyze dependencies between modernization plan items.
- Assess the potential impacts, risks, and mitigations associated with modernization findings.
- Integrate AI providers through a layered architecture with response validation and fallback handling.
- Explore incremental modernization rather than relying on a complete application rewrite.

## Key Features

### 1. Legacy Application Analysis

The project includes a sample enterprise application built with legacy Microsoft .NET technologies.

The application provides a realistic baseline for identifying modernization opportunities, including:

- Hard-coded workflow rules.
- Scattered business status representations.
- Direct dependencies between the presentation and data layers.
- Environment-specific configuration.
- Business validation responsibilities spread across layers.

### 2. AI-Assisted Modernization Recommendations

The modernization workflow uses existing findings as input for structured analysis and recommendations.

The design separates analysis context, prompt construction, AI request construction, provider integration, response parsing, and validation.

### 3. Modernization Planning

The planning workflow produces a structured modernization plan containing phases and actionable plan items.

Each plan item can include its priority, risk, complexity, estimated effort, dependencies, affected components, reasoning, and implementation steps.

### 4. Dependency and Impact Analysis

The dependency-and-impact analysis stage builds on the modernization plan and findings.

It supports structured results covering:

- Dependencies between modernization plan items.
- Dependency descriptions and their impacts.
- Affected components and supporting reasons.
- Impact areas and impact levels.
- Potential risks and mitigation strategies.
- Validation of dependency references and impact references.

The results can be displayed in the application for review.

### 5. Pluggable AI Provider Architecture

The AI integration is designed to separate the modernization workflow from the underlying AI provider.

The current architecture includes:

- A provider abstraction.
- An Ollama-based AI provider.
- A mock provider for fallback and predictable testing.
- Response parsing and validation.
- Configurable provider fallback.

This approach makes it possible to change or extend the AI integration without redesigning the entire analysis workflow.

## Technology Stack

| Area | Technology |
|---|---|
| Legacy application | C# and ASP.NET Web Forms |
| Runtime | .NET Framework 4.8 |
| ORM | Entity Framework 6 |
| Database | Microsoft SQL Server |
| Data modeling | EDMX / Database First |
| Application architecture | Layered architecture |
| AI integration | Ollama and mock provider |
| AI model used locally | Qwen3 4B |
| Development environment | Visual Studio |
| Version control | Git and GitHub |

The legacy application is intentionally retained as the baseline for modernization analysis.

## Architecture Overview

The AI-assisted modernization workflow separates application-level analysis from provider-specific AI integration.

```text
Legacy2Modern.Web
        |
        v
Modernization Analysis
        |
        v
Prompt Builder / Request Builder
        |
        v
Modernization AI Service
        |
        v
AI Provider Orchestrator
        |
        +-------------------+
        |                   |
        v                   v
      Ollama               Mock
      Primary             Fallback
```

The modernization workflow also includes response parsing, validation, and presentation.

The planning and dependency-impact analysis stages extend the workflow beyond initial findings and recommendations.

## Repository Structure

```text
Legacy2Modern-AI/
|
├── docs/
│   ├── LEGACY_ARCHITECTURE.md
│   └── AI_MODERNIZATION_ARCHITECTURE.md
│
├── legacy/
│   └── Legacy2Modern.Legacy/
│       └── Legacy2Modern.Legacy.sln
│
└── README.md
```

*This is the intended high-level structure; additional files and folders may exist in the repository.*

## Documentation

Detailed architecture documentation is available here:

- [Legacy Architecture](docs/LEGACY_ARCHITECTURE.md) — describes the legacy application, its technology stack, current architectural design, and identified modernization opportunities.
- [AI Modernization Architecture](docs/AI_MODERNIZATION_ARCHITECTURE.md) — describes the layered AI integration architecture and its provider workflow.

## Current Project Status

The project has progressed through the modernization analysis, planning, and dependency-and-impact analysis stages.

Implemented capabilities include structured modernization analysis results, modernization planning, dependency and impact models, response validation, and a results UI.

Development and validation continue incrementally as the project evolves.

## Future Direction

Potential areas for further development include:

- Expanding validation and automated test coverage.
- Improving dependency graph analysis.
- Exploring additional AI providers and models.
- Extending modernization recommendations and implementation planning.
- Improving the presentation of analysis results.
- Documenting a complete local setup and demonstration workflow.

These are potential extensions, not claims that the features are already implemented.

## Why This Project?

Legacy modernization is not simply about replacing an older framework with a newer one. Developers must understand existing business rules, identify architectural risks, evaluate dependencies, and plan changes without disrupting existing functionality.

Legacy2Modern-AI explores how structured analysis and AI assistance can support that process.

The project demonstrates my practical work with C#, .NET application architecture, legacy systems, AI provider integration, validation, and incremental modernization planning.
