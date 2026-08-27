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