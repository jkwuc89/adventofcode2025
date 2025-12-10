---
name: GitHub CI and StyleCop Setup
overview: Add GitHub Actions CI workflow to build and test the project, and configure StyleCop analyzer with widely used defaults to lint C# code during CI runs.
todos:
  - id: create-github-workflow
    content: Create .github/workflows/ci.yml with build, test, and StyleCop analysis steps
    status: pending
  - id: add-stylecop-packages
    content: Add StyleCop.Analyzers NuGet package to both .csproj files
    status: pending
  - id: create-stylecop-config
    content: Create stylecop.json configuration file with widely used defaults
    status: pending
  - id: configure-projects-stylecop
    content: Configure both .csproj files to reference stylecop.json and enable code analysis
    status: pending
---

# GitHub CI and StyleCop Setup

## Overview

This plan adds a GitHub Actions CI workflow that builds the project, runs tests, and performs StyleCop code analysis. StyleCop will be configured with widely used defaults and integrated into both the main project and test project.

## Implementation Steps

### 1. Create GitHub Actions Workflow

Create `.github/workflows/ci.yml` that:

- Runs on push and pull requests
- Uses the latest Ubuntu runner
- Sets up .NET SDK (matching the project's target framework)
- Restores dependencies
- Builds the solution
- Runs all tests using `dotnet test`
- Runs StyleCop analysis as part of the build (configured in project files)
- Fails if any step fails

### 2. Add StyleCop Analyzers Package

Add `StyleCop.Analyzers` NuGet package to both:

- `AdventOfCode2025/AdventOfCode2025.csproj`
- `AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj`

Use the latest stable version (typically 1.1.118 or newer).

### 3. Create StyleCop Configuration

Create `stylecop.json` in the solution root with widely used defaults:

- Enable documentation rules (SA1600, SA1601, SA1602) but allow missing documentation for now
- Use standard indentation and spacing rules
- Configure file naming conventions
- Set copyright header rules (can be disabled if not needed)
- Use common rule severity settings

### 4. Configure Projects for StyleCop

Update both `.csproj` files to:

- Reference the `stylecop.json` file
- Enable code analysis during build
- Treat StyleCop warnings as errors (optional, but common in CI)

### 5. Update CI Workflow for StyleCop

Ensure the CI workflow treats analyzer warnings/errors appropriately. The build will automatically fail if StyleCop violations are configured as errors.

## Files to Create/Modify

- **Create**: `.github/workflows/ci.yml` - GitHub Actions workflow
- **Create**: `stylecop.json` - StyleCop configuration file
- **Modify**: `AdventOfCode2025/AdventOfCode2025.csproj` - Add StyleCop package and configuration
- **Modify**: `AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj` - Add StyleCop package and configuration

## Notes

- The workflow will use `dotnet build` and `dotnet test` commands which will automatically run StyleCop analyzers
- StyleCop configuration uses widely accepted defaults that balance code quality with practical development
- Documentation rules may need adjustment based on project preferences, but will be set to warnings initially to avoid breaking existing code

