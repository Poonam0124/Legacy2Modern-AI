# AI Modernization Architecture

## Overview

Legacy2Modern-AI uses a layered AI architecture to analyze legacy
application modernization opportunities.

The design separates:

- modernization findings
- analysis context
- prompt construction
- AI request construction
- AI provider integration
- response parsing
- response validation
- provider fallback
- presentation

This allows the AI provider to be changed without redesigning the
modernization analysis workflow.

## Architecture

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