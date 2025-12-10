---
name: GitHub CI and StyleCop Setup
overview: Add GitHub Actions CI workflow to build and test the project, and configure StyleCop analyzer with widely used defaults to lint C# code during CI runs.
todos:
  - id: create-github-workflow
    content: Create .github/workflows/ci.yml with build, test, and StyleCop analysis steps
    status: completed
  - id: add-stylecop-packages
    content: Add StyleCop.Analyzers NuGet package (1.1.118) to both .csproj files
    status: completed
  - id: create-stylecop-config
    content: Create stylecop.json configuration file with widely used defaults aligned with .editorconfig
    status: completed
  - id: configure-projects-stylecop
    content: Configure both .csproj files to reference stylecop.json, enable code analysis, and treat warnings as errors
    status: completed
---

# GitHub CI and StyleCop Setup

## Overview

This plan adds a GitHub Actions CI workflow that builds the project, runs tests, and performs StyleCop code analysis. StyleCop will be configured with widely used defaults and integrated into both the main project and test project.

## Implementation Steps

### 1. Create GitHub Actions Workflow

Create [`.github/workflows/ci.yml`](.github/workflows/ci.yml) that:

- Runs on push and pull requests to main/master branches
- Uses the latest Ubuntu runner
- Sets up .NET SDK 10.0 (matching the project's target framework)
- Restores dependencies
- Builds the solution using `dotnet build`
- Runs all tests using `dotnet test`
- StyleCop analysis runs automatically as part of the build (configured in project files)
- Fails if any step fails

### 2. Add StyleCop Analyzers Package

Add `StyleCop.Analyzers` NuGet package (version 1.1.118) to both:

- [`AdventOfCode2025/AdventOfCode2025.csproj`](AdventOfCode2025/AdventOfCode2025.csproj)
- [`AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj`](AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj)

### 3. Create StyleCop Configuration

Create [`stylecop.json`](stylecop.json) in the solution root with widely used defaults:

- Disable file header rules (SA1633, SA1634, SA1635, SA1636) as configured in .editorconfig
- Use standard indentation (4 spaces) and spacing rules
- Configure using directives placement (outside namespace for file-scoped namespaces)
- Set documentation rules to allow missing documentation for private/internal elements
- Use common rule severity settings

### 4. Configure Projects for StyleCop

Update both `.csproj` files to:

- Reference the `stylecop.json` file via AdditionalFiles
- Enable code analysis during build (`EnforceCodeStyleInBuild`)
- Treat StyleCop warnings as errors (`TreatWarningsAsErrors`) to fail CI on violations

### 5. Verify Integration

Ensure the CI workflow will:

- Automatically run StyleCop analyzers during `dotnet build`
- Fail the build if StyleCop violations are found (since warnings are treated as errors)
- Run tests after successful build

## Files to Create/Modify

- **Create**: [`.github/workflows/ci.yml`](.github/workflows/ci.yml) - GitHub Actions workflow
- **Create**: [`stylecop.json`](stylecop.json) - StyleCop configuration file
- **Modify**: [`AdventOfCode2025/AdventOfCode2025.csproj`](AdventOfCode2025/AdventOfCode2025.csproj) - Add StyleCop package and configuration
- **Modify**: [`AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj`](AdventOfCode2025.Tests/AdventOfCode2025.Tests.csproj) - Add StyleCop package and configuration

## Notes

- The workflow will use `dotnet build` and `dotnet test` commands which will automatically run StyleCop analyzers
- StyleCop configuration will align with existing `.editorconfig` settings (file headers disabled, etc.)
- The build will fail if StyleCop violations are found, ensuring code quality in CI
- StyleCop rules disabled in `.editorconfig` (SA1200, SA1600, etc.) will remain disabled
