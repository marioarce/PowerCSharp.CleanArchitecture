# Contributing to PowerCSharp.CleanArchitecture

Thank you for your interest in contributing. This document covers how to propose changes, coding standards, and the review process.

---

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [How to Contribute](#how-to-contribute)
- [Development Setup](#development-setup)
- [Coding Standards](#coding-standards)
- [Commit Message Format](#commit-message-format)
- [Pull Request Guidelines](#pull-request-guidelines)
- [Reporting Bugs](#reporting-bugs)
- [Requesting Features](#requesting-features)

---

## Code of Conduct

This project follows the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you agree to uphold it. Please report unacceptable behavior to the project maintainer.

---

## How to Contribute

1. **Search existing issues** before opening a new one — your question or bug may already be tracked.
2. **Open an issue** for non-trivial changes before writing code. This allows design discussion and avoids wasted effort.
3. **Fork** the repository and create a dedicated branch from `main`:
   ```bash
   git checkout -b feat/my-feature
   ```
4. **Implement** your change following the coding standards below.
5. **Test** your change — all existing tests must pass and new behavior should have test coverage.
6. **Push** your branch and open a **Pull Request** against `main`.

---

## Development Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git

### Steps

```bash
git clone https://github.com/marioarce/PowerCSharp.CleanArchitecture.git
cd PowerCSharp.CleanArchitecture

dotnet restore
dotnet build
dotnet test
```

All packages resolve from nuget.org — no additional local feed setup is required.

---

## Coding Standards

This repo follows the conventions defined in `.editorconfig`. Key rules:

- **File-scoped namespaces** — required (`namespace Foo;` not `namespace Foo { ... }`).
- **Explicit accessibility modifiers** — always required (`public`, `private`, etc.).
- **Nullable reference types** — enabled globally; no `!` suppressions without justification.
- **No `this.` prefix** — avoid qualifying members with `this.`.
- **`var`** — use only when the type is evident from the right-hand side.
- **XML doc comments** — required on all `public` members.
- **No trailing whitespace**, LF line endings, UTF-8 encoding.

The build enforces code style (`EnforceCodeStyleInBuild = true`). A build that introduces style violations will fail.

---

## Commit Message Format

```
<scope>: <description>

<changes>
```

- **Scope** is required. Use the area being changed (e.g. `ci`, `docs`, `nuget`, `template`, `docker`, `chore`, `feat`, `fix`).
- **Description** is a short imperative sentence (no period at end).
- **Changes** are bullet points (`- `) summarising what was done.
- No emojis in commit messages.

Example:

```
feat: add IRetryPolicyProvider to Operational layer

- Add IRetryPolicyProvider interface with Execute overloads
- Add RetryPolicyProvider implementation using Polly
- Register in AddOperational DI extension
```

---

## Pull Request Guidelines

- Keep PRs **focused** — one logical change per PR.
- Fill out the [PR template](.github/pull_request_template.md) completely.
- Ensure all CI checks pass before requesting review.
- Update `CHANGELOG.md` under the `[Unreleased]` section.
- Do not force-push to a PR branch after review has started.

---

## Reporting Bugs

Use the [Bug Report issue template](.github/ISSUE_TEMPLATE/bug_report.yml). Include:

- Steps to reproduce
- Expected vs actual behaviour
- .NET SDK version and OS

---

## Requesting Features

Use the [Feature Request issue template](.github/ISSUE_TEMPLATE/feature_request.yml). Explain:

- The problem you are trying to solve
- The proposed solution
- Alternatives you have considered
