# Legacy2Modern — Legacy Architecture

## 1. Overview

Legacy2Modern is a sample enterprise customer and service-request
management application built using a legacy Microsoft .NET technology stack.

The application is intentionally implemented using technologies and
architectural patterns commonly found in older enterprise applications.

The purpose of this application is to provide a realistic legacy system
that can later be analyzed and modernized using the Legacy2Modern-AI platform.

---

## 2. Current Technology Stack

| Area | Technology |
|---|---|
| Runtime | .NET Framework |
| Web Framework | ASP.NET WebForms |
| Language | C# |
| ORM | Entity Framework 6 |
| Database | Microsoft SQL Server |
| Data Model | EDMX / Database First |
| Architecture | Layered Architecture |
| Data Access | Repository Pattern |
| Business Logic | Service Layer |
| UI | WebForms / Code-behind |
| Development IDE | Visual Studio |

---

## 3. Current Architecture

The current application follows a layered architecture.

```text
Presentation Layer
       |
       v
Business / Service Layer
       |
       v
Repository / Data Access Layer
       |
       v
Entity Framework 6
       |
       v
SQL Server
```   

       
## 4. Hard-coded Workflow Rules

Service Request status transitions are implemented using
hard-coded string comparisons and procedural conditional logic.

Current supported transitions:

- Open → Assigned
- Assigned → Open
- Assigned → In Progress
- In Progress → Assigned
- In Progress → Resolved
- Resolved → In Progress
- Resolved → Closed

Closed is a terminal status and does not allow further transitions.

The workflow is embedded in application code rather than being
represented as an explicit workflow model.

Potential modernization:

- Explicit state/transition model
- Centralized workflow definition
- Strongly typed status representation
- Configurable transition rules where appropriate
- Automated workflow tests

### Scattered Business Status Rules

Service Request status values are represented in multiple ways
within the application.

For example, the initial Service Request status is assigned using
a hard-coded string:

`Status = "Open"`

while the Service Request service also maintains status constants
such as:

`StatusOpen = "Open"`

The workflow helper additionally contains status values used for
transition validation.

This creates multiple representations of the same business concept.

Potential risks:

- Inconsistent status values
- Changes requiring updates in multiple locations
- Increased maintenance effort
- Potential behavior differences between components
- Difficulty identifying all usages of a business status

Potential modernization:

- Centralized status representation
- Strongly typed status values
- Explicit state model
- Centralized workflow definition