# Security Policy

## Supported Versions

| Version | Supported |
|---------|-----------|
| Latest (`main`) | Yes |
| Older tags | No — please upgrade to the latest version |

This is a template repository. Security fixes are applied to the `main` branch and released as new versions. Projects scaffolded from this template should be updated manually as needed.

## Reporting a Vulnerability

**Do not open a public GitHub issue for security vulnerabilities.**

Please report security vulnerabilities via **GitHub's private security advisory** feature:

1. Go to the [Security tab](https://github.com/marioarce/PowerCSharp.CleanArchitecture/security) of this repository.
2. Click **"Report a vulnerability"**.
3. Fill in the details — include steps to reproduce, potential impact, and any suggested mitigations.

You will receive an acknowledgement within **72 hours**. We aim to provide a fix or mitigation plan within **14 days** for critical issues.

## Security Considerations for Projects Built from This Template

This template ships sample/demo endpoints (`/v1/samples/*`) that expose or mutate server-side state. These endpoints are:

- **Flag-gated** (`PowerFeatures:Samples:Enabled`): disabled by default, enabled in `Development` only.
- **Not protected by authentication or authorization** — they are demos, not production code.

Before going to production with a project scaffolded from this template:

- Disable or remove the `Samples` feature module and its controller.
- Add proper authentication (e.g. JWT Bearer) and authorization policies to any sensitive endpoint.
- Review and scope the CORS policy (`PowerFeatures:Cors:AllowedOrigins`) to your actual front-end origins.
- Do not expose internal cache state (`/status`) or destructive operations (`DELETE /cache`) without authorization.

## Dependency Security

Dependencies are monitored via [Dependabot](.github/dependabot.yml). Pull requests for security updates are created automatically and prioritized for review.
