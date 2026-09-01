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

### Cross-Layer Coupling: Web Layer Directly Depends on Data Layer

The WebForms presentation layer has a direct project dependency on
Legacy2Modern.Data in addition to Legacy2Modern.Business.

Current dependency structure:

Web
 ├── Business
 └── Data

Several WebForms code-behind files directly use Entity Framework
entity types from the Data layer.

Examples include:

- Customers/CustomerEdit.aspx.cs
- Customers/CustomerContactEdit.aspx.cs
- Customers/CustomerProductEdit.aspx.cs
- ServiceRequests/ServiceRequestDetails.aspx.cs

Examples of direct Data-layer entity usage include:

    new Customer()
    new CustomerContact()
    new CustomerProduct()
    as Employee
    ServiceRequest request

ServiceRequestDetails.aspx.cs also directly navigates through
persistence entities and their relationships, including:

    request.Customer
    request.CustomerProduct
    request.CustomerProduct.Product
    request.Employee

This means the presentation layer understands details of the
EF6 persistence model.

Potential risks:

- Changes to EF entities can directly affect WebForms pages.
- Presentation code becomes coupled to persistence implementation.
- Testing the Web layer becomes more difficult.
- Migration from EF6 to another persistence technology becomes harder.
- Database model changes can propagate directly into UI code.
- DTO/ViewModel boundaries are not consistently enforced.

Current state is intentionally retained as part of the legacy baseline.

Potential modernization:

- Remove the direct Web → Data project dependency.
- Keep Web dependent on Business.
- Move EF entity usage behind business/service boundaries.
- Introduce DTOs/ViewModels where appropriate.
- Keep EF entities inside the Data layer.
- Return application-specific models from the Business layer.

### Configuration Debt

The application configuration contains environment-specific and
development-oriented settings directly in Web.config.

#### Debug configuration

The application currently has:

    <compilation debug="true" targetFramework="4.8" />

Debug mode is configured directly in the main Web.config rather
than being clearly separated by environment.

Potential risks:

- Development settings can accidentally be used outside development.
- Deployment configuration requires manual verification.
- Environment-specific behavior is not explicitly modeled.

#### Database environment coupling

The EF6 connection string directly references:

    (localdb)\MSSQLLocalDB

and the database:

    Legacy2ModernDB

The current configuration therefore assumes a specific local
development database environment.

Potential risks:

- Application deployment requires configuration changes.
- Development and production configuration can become coupled.
- Environment differences are not explicitly represented.
- Configuration management becomes increasingly difficult as
  environments are added.

Potential modernization:

- Separate environment-specific configuration.
- Externalize environment-specific settings.
- Use deployment-time configuration transformation.
- Avoid embedding development infrastructure assumptions in the
  application baseline.
- Introduce secure configuration management for credentials and
  secrets if external services are added.

Current state is intentionally retained as part of the legacy
baseline.

### UI Layer Contains Business Validation Logic

Several WebForms code-behind files contain validation and decision
logic that overlaps with business-layer responsibilities.

For example, ServiceRequestCreate.aspx.cs validates required
Service Request fields before calling the Business layer:

    Customer
    Request Type
    Priority

The page also determines whether optional Customer Product and
Assigned Employee values should be converted into nullable IDs.

The Business layer independently validates some of the same
business requirements, for example:

    customerId <= 0
    Subject is required
    Priority is required

This creates a responsibility boundary that is not consistently
defined.

Potential risks:

- Validation rules can become duplicated.
- Different entry points may enforce different rules.
- Business rules can become dependent on WebForms controls.
- Automated testing of business rules becomes harder.
- Future API or UI clients may need to duplicate validation logic.

Potential modernization:

- Keep presentation-specific validation in the Web layer.
- Move domain/business invariants into the Business layer.
- Avoid coupling business rules to WebForms controls.
- Centralize reusable business validation.
- Use DTOs/ViewModels to define application boundaries.

Current state is intentionally retained as part of the legacy
baseline.