# Legacy2Modern AI — System Architecture

## Purpose

Legacy2Modern AI is an AI-assisted platform for analyzing and modernizing
legacy .NET applications.

## High-Level Architecture

Legacy Application
        |
        v
Migration Analyzer
        |
        v
Migration Intelligence
        |
        +------ Risk Analysis
        |
        +------ Migration Recommendations
        |
        +------ Migration Roadmap
        |
        v
Modern Application
        |
        +------ ASP.NET Core API
        +------ React Frontend
        +------ SQL Server
        +------ Background Worker
        |
        v
AI Layer
        |
        +------ LLM
        +------ RAG
        +------ Python AI Service
        +------ Future AI Agent
        
## Core Principles

1. Deterministic analysis before AI.
2. AI provides recommendations, not uncontrolled modifications.
3. Human approval is required for migration changes.
4. Legacy and modern applications remain independently runnable.
5. Every major architecture decision is documented.