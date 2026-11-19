
# Testing Guide

## Overview

Legacy2Modern-AI includes automated unit tests for the AI-assisted
modernization workflow. The tests verify request construction, prompt
generation, response parsing, validation, service behavior, provider
orchestration, and fallback handling.

**Latest verified test result:** 99 passed, 0 failed.

The test project is:

`tests/Legacy2Modern.Business.Tests`

The tests use MSTest and target .NET Framework 4.8.

## Prerequisites

- Visual Studio with .NET Framework 4.8 development tools.
- The solution and its project dependencies restored successfully.
- The test project builds successfully.

A running AI provider is not required to execute the unit tests described
in this guide.

## Running the Tests

### Using Visual Studio

1. Open `legacy/Legacy2Modern.Legacy/Legacy2Modern.Legacy.sln`.
2. Restore NuGet packages if prompted.
3. Build the solution.
4. Open **Test > Test Explorer**.
5. Select **Run All Tests**.
6. Review the passed, failed, and skipped test counts.

The expected result for the current test suite is:

- Passed: 99
- Failed: 0

If tests are added or removed, update the documented count after running
the complete suite again.

## Test Coverage

### 1. Response validation

Tests cover validation of modernization dependency-impact analysis
responses, including invalid data and validation failures.

### 2. Request builders

Tests verify that planning and dependency-impact request builders:

- Reject invalid or incomplete input.
- Pass the expected application context to prompt builders.
- Populate request properties correctly.
- Handle invalid construction scenarios.

### 3. Prompt builders

Tests verify that planning and dependency-impact prompts:

- Include relevant application context.
- Include supplied findings and recommendations.
- Include modernization plan details where required.
- Describe the expected JSON response structure.
- Include constraints intended to prevent unsupported dependencies
  and references to nonexistent plan items.

### 4. Response parsers

Tests cover response parsing, including:

- Valid JSON responses.
- Supported response structures.
- Markdown code fences where applicable.
- Empty or malformed responses.
- Error handling when a response cannot be parsed.

### 5. Planning service

Tests verify that the planning service:

- Rejects a null context.
- Passes the context to the request builder.
- Passes the built request to the provider.
- Returns the provider's response.
- Rejects a null provider response.
- Propagates request-builder and provider exceptions.

### 6. Dependency-impact analysis service

Tests verify that the analysis service:

- Rejects null constructor dependencies and a null context.
- Builds and forwards the analysis request.
- Rejects a null analysis response.
- Validates the response before returning it.
- Rejects missing or unsuccessful validation results.
- Preserves the analysis response when validation succeeds.
- Stops processing when request construction or provider execution fails.

### 7. AI provider orchestration and fallback

Tests verify that the orchestrator:

- Rejects null constructor dependencies and requests.
- Returns the primary provider's response when successful.
- Propagates the primary exception when fallback is disabled.
- Uses the fallback provider when enabled and the primary provider fails.
- Passes the same request to both providers during fallback.
- Reports failure when both providers fail.
- Preserves both underlying exceptions in the aggregate exception.

### 8. Provider factories

Tests cover provider factory behavior and relevant configuration
orchestration without requiring a live AI provider call.

## Test Isolation

The service and orchestration tests use test doubles to control provider,
request-builder, and validator behavior.

This allows the tests to verify success paths and failure paths
deterministically without depending on:

- Network availability.
- A running Ollama server.
- A particular AI model being installed.
- External AI service availability.

## Current Limitations

Passing unit tests do not establish that every production integration
works correctly.

The following areas require separate verification where applicable:

- End-to-end execution against a configured AI provider.
- AI response quality and consistency across real model outputs.
- Behavior with unusually large modernization contexts.
- Performance and resource usage under load.
- Database and application integration behavior outside the unit tests.
- UI workflows and deployment configuration.

These are separate from the current unit-test suite and should not be
considered covered solely because all 99 tests pass.

## Regression Testing

Run the complete test suite after changes to:

- Request and prompt builders.
- AI response parsers.
- Response validators.
- Planning and dependency-impact services.
- Provider factories and fallback orchestration.
- AI request or response models.

Investigate failing tests before merging or committing changes that
affect the tested behavior.

## Maintenance

When changing test coverage:

1. Add tests for new behavior and relevant failure cases.
2. Avoid duplicating existing test scenarios.
3. Keep unit tests independent of live AI services where possible.
4. Run the complete suite.
5. Update the verified test count and this document when necessary.
