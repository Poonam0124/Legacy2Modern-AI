# AI Modernization Architecture

## 1. Overview

Legacy2Modern-AI uses a layered architecture to support the analysis and planning of legacy application modernization opportunities.

The system is designed to separate modernization analysis logic from AI provider integration. This separation makes the workflow easier to maintain, validate, test, and extend.

The architecture separates:

- Modernization findings and analysis context
- Prompt construction
- AI request construction
- AI provider integration
- Response parsing and validation
- Provider fallback
- Modernization planning
- Dependency and impact analysis
- Presentation of analysis results

The project explores how AI-assisted analysis and structured planning can help developers understand legacy application concerns and organize modernization work.

## 2. High-Level Architecture

The following diagram illustrates the core AI analysis pipeline.

```text
Legacy2Modern.Web
        |
        v
ModernizationAnalysisService
        |
        +----------------------+
        |                      |
        v                      v
Prompt Builder          Request Builder
        |                      |
        +----------+-----------+
                   |
                   v
        ModernizationAIService
                   |
                   v
        AIProviderOrchestrator
              /           \
             /             \
            v               v
     IAIProvider     IAIProviderFallback
          |                  |
          v                  v
       Ollama              Mock
       Primary            Fallback
```

The core pipeline separates analysis orchestration, prompt construction, request construction, provider execution, and result handling.

Additional workflows extend the analysis into modernization planning and dependency-and-impact assessment.

## 3. Core AI Analysis Pipeline

### 3.1 Modernization Analysis Service

`ModernizationAnalysisService` coordinates the initial modernization analysis workflow.

Its role is to organize the analysis process and work with the supporting components responsible for preparing requests and processing AI responses.

### 3.2 Prompt and Request Construction

Prompt construction and AI request construction are separate responsibilities.

This separation helps keep modernization-specific instructions distinct from the structure required to communicate with an AI provider.

It also makes the workflow easier to maintain as the analysis requirements evolve.

### 3.3 AI Provider Orchestration

`AIProviderOrchestrator` coordinates the configured AI provider strategy.

The existing architecture includes:

- **Ollama:** the primary provider in the documented configuration.
- **Mock provider:** the fallback option in the documented configuration.

The provider abstraction reduces direct coupling between modernization analysis and a specific AI implementation.

### 3.4 Response Parsing and Validation

AI responses must be processed before their results are used by the application.

Parsing converts provider output into application models, while validation checks whether the resulting data satisfies the required structure and reference constraints.

This separation helps prevent malformed or inconsistent analysis results from flowing directly into the presentation layer.

## 4. Modernization Planning

Modernization planning extends the workflow beyond identifying concerns and producing recommendations.

The planning model represents a modernization strategy through phases and individual plan items.

A modernization plan can contain:

- Plan title and overall strategy
- Target architecture
- Ordered modernization phases
- Plan item identifiers
- Associated finding identifiers
- Recommended actions and reasoning
- Priority, risk, complexity, and effort
- Dependencies between plan items
- Affected components
- Implementation steps

The planning models provide a structured representation of the work required to address modernization findings.

### 4.1 Planning Provider Abstraction

The planning workflow uses a provider abstraction through `IAIPlanningProvider`.

The repository includes `MockModernizationPlanningAIProvider`, which supplies a predictable modernization plan for development and testing.

The mock provider helps exercise the planning workflow without requiring every test to depend on live AI output.

### 4.2 Plan Item Relationships

Each `ModernizationPlanItem` has a `PlanItemId` and can contain a collection of dependencies.

These relationships provide the basis for identifying prerequisites between modernization tasks.

For example, establishing a centralized application service may need to precede migrating individual Web Forms pages to consume that service.

Actual dependency relationships must be supported by the generated plan data; they should not be assumed merely because two items belong to different phases.

## 5. Dependency and Impact Analysis

The dependency-and-impact analysis stage builds on modernization planning and existing modernization findings.

It produces structured results that describe relationships between planned work and the potential consequences of addressing identified findings.

The main response model is `ModernizationDependencyImpactAnalysis`.

It contains an analysis title, an overall assessment, a dependency collection, and an impact collection.

### 5.1 Dependency Analysis

Each `ModernizationDependency` contains:

- `DependencyId`
- `SourceId`
- `TargetId`
- `DependencyType`
- `Description`
- `Impact`
- `AffectedComponents`
- `Reasons`

The source and target identifiers reference modernization plan items.

This representation supports describing which plan items depend on other items and why those relationships matter.

### 5.2 Impact Analysis

Each `ModernizationImpact` contains:

- `ImpactId`
- `FindingId`
- `ImpactArea`
- `ImpactLevel`
- `Description`
- `AffectedComponents`
- `Risks`
- `Mitigations`

The `FindingId` connects an impact to an existing modernization finding.

This makes it possible to explain the potential consequences of a finding and document risks and mitigation strategies alongside the analysis.

### 5.3 Context Used for Validation

`ModernizationDependencyImpactContext` provides the contextual data used to validate analysis results.

It includes:

- Application name and description
- Technology stack
- Modernization goal
- Existing findings
- Modernization recommendations
- Modernization plan

The context allows validation to check whether generated references correspond to existing plan items and findings.

## 6. Dependency and Impact Response Validation

`ModernizationDependencyImpactResponseValidator` validates the structure and references of dependency-and-impact analysis results.

The validation process includes the following checks.

### Structural validation

- The analysis response is present.
- The analysis title and overall assessment are provided.
- Dependency and impact collections are present.
- Required identifiers and descriptive fields are provided.
- Required supporting collections are initialized.
- Dependency identifiers are unique.
- Impact identifiers are unique.

Empty dependency or impact collections are permitted. An analysis may legitimately identify no dependencies or no impacts.

### Reference validation

When the analysis context is available, the validator checks that:

- Dependency source identifiers reference existing plan items.
- Dependency target identifiers reference existing plan items.
- A dependency does not reference the same plan item as both source and target.
- Impact finding identifiers reference existing findings.

These checks help preserve referential consistency between the analysis response and the data used to produce it.

The current validator does not, by itself, establish that all possible dependency cycles have been detected. Circular-dependency detection would require additional validation logic.

## 7. Results Presentation

The WebForms application presents the dependency-and-impact analysis results through its results UI.

The displayed information includes:

- Overall assessment
- Dependency details
- Impact details
- Affected components
- Supporting reasons
- Risks
- Mitigations
- Analysis title and provider information

This presentation makes the structured analysis available for review instead of leaving the results only in an internal model or raw provider response.

## 8. Legacy Application Baseline

The modernization workflow uses a legacy enterprise application as its baseline.

The legacy application uses:

- C#
- .NET Framework 4.8
- ASP.NET Web Forms
- Entity Framework 6
- SQL Server
- EDMX / Database First
- A layered application structure

The baseline contains architectural concerns that provide realistic modernization scenarios, including hard-coded workflow rules, cross-layer coupling, configuration debt, and validation responsibilities spread across layers.

For further details, see [Legacy Architecture](LEGACY_ARCHITECTURE.md).

The legacy application is intentionally retained as the baseline for modernization analysis.

## 9. Design Principles

The architecture follows these principles.

### Separation of responsibilities

Analysis orchestration, prompt construction, provider integration, planning, validation, and presentation are maintained as distinct responsibilities.

### Provider independence

Provider abstractions help reduce coupling between modernization workflows and a specific AI implementation.

### Structured outputs

Dedicated models represent findings, recommendations, plans, dependencies, and impacts. This provides clearer boundaries than passing unstructured text through the entire application.

### Reference integrity

Generated dependencies and impacts are validated against existing plan items and findings.

### Testability

Mock providers allow predictable results for development and testing without relying exclusively on live AI responses.

### Incremental modernization

The planning workflow supports organizing modernization work into phases and individual tasks rather than assuming that a legacy application must be replaced in one step.

## 10. Current Implementation Boundaries

Legacy2Modern-AI is an evolving portfolio project.

The implemented workflow includes AI-assisted modernization analysis, structured planning models, dependency-and-impact analysis models, response validation, and a results UI.

The existence of a model or architecture extension does not automatically mean every advanced analysis capability is implemented. For example, comprehensive circular-dependency detection would require additional implementation and tests.

The project continues to evolve through incremental development, validation, and documentation.

## 11. Related Documentation

- [Legacy Architecture](LEGACY_ARCHITECTURE.md) — the legacy application's technology stack, current architecture, identified concerns, and modernization opportunities.
- [Project README](../README.md) — the project overview, capabilities, technology stack, and repository entry point.
