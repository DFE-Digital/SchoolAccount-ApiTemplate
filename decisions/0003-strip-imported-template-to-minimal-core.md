---
status: "accepted"
date: "2026-07-16"
decision-makers: Paul Custance and Mark Harrop
---

# Strip the imported template to a minimal core

## Context and Problem Statement

This template began as an import of Milan Jovanovic's clean architecture
template, which ships with two worked example domains (todos, users) and a
complete JWT authentication and permission-based authorisation stack. Should
projects stamped from our template start from that full example or from a
minimal core?

## Decision Drivers

* Example code gets copied: whatever ships in the template tends to survive
  into real projects, appropriate or not.
* It isn't clear at the moment what auth mechanisms, if any, will be needed
* Deleting unused example code is the first chore of every new project.
* Newcomers still need at least one worked example of the endpoint ->
  handler -> response flow.

## Considered Options

* Strip to a minimal core with one small worked example
* Keep the full imported template (todos, users, auth) as-is
* Keep the examples but mark them clearly as deletable

## Decision Outcome

Chosen option: "Strip to a minimal core with one small worked example",
because a template should contain only what every stamped project needs,
anything else is deletion work pushed onto every consumer.

Removed from the import (84 files):

* The todos and users example domains, including their domain events,
  handlers, validators, and endpoints.
* The authentication and authorisation stack: JWT token provider, password
  hasher, user context, and the permission-based authorisation system.
* Abstractions that only served the removed code (`IPasswordHasher`,
  `ITokenProvider`, `IUserContext`, `IApplicationDbContext`).

Retained: the architectural skeleton recorded in
[ADR-0002](0002-use-clean-architecture.md), the infrastructure stack
(Serilog/Seq), and the cross-cutting decorators.
A single domain-relevant example, `GetOrganisationByLaestab`, replaces the
removed domains as the worked example of the endpoint -> handler -> response
flow. Swagger was swaped for Scalar.

### Consequences

* Good, because stamped projects start clean instead of starting with a
  deletion chore.
* Good, because is isn't clear at the moment that authentication will be required
  and if it is what that mechanism will be.
* Bad, because one example demonstrates fewer patterns than the originals
  (e.g. no command with domain events remains in the template).

### Confirmation

Reviewed through the pull-request process; the `Build` workflow confirms the
solution builds and all tests pass without the removed code.

## More Information

* Removal happened in commits `37f2de5` ("Strip back template to only what is
  required", 2026-07-15) and `f09bf94` ("Remove todo and user examples",
  2026-07-16); the deleted auth stack can be recovered from them if needed.
* [ADR-0002](0002-use-clean-architecture.md) records the architecture that was
  retained.
