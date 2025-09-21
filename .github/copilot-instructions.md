# AI Coding Agent Guidance

This document provides guidance for AI coding agents working on the ProductCatalog ASP.NET project.

## Project Overview

- **Architecture:** ASP.NET Core MVC project with Entity Framework Core for data access and Identity for authentication. Key files include `Program.cs`, controllers in the `Controllers` folder, and views in the `Views` folder.
- **Data Management:** Two DbContexts are defined in the `Data` folder (`AppDbContext` and `AppIdentityDbContext`). Database migrations are managed under the `Migrations` directory.
- **Services & DI:** Services (e.g., `GreetingService` in the `Services` folder) are injected via ASP.NET Core's built-in dependency injection.

## Architectural Patterns & Conventions

- **MVC Structure:** Controllers (e.g., `HomeController.cs`, `ProductController.cs`) handle HTTP requests, while views rely on the standard Razor setup in `Views` (see `Views/Shared/_Layout.cshtml` for overall layout).
- **Entity Models:** Models (like `Product.cs` in `Models`) are simple POCOs reflecting database tables. Seeding is handled in `Data/SeedData.cs`.
- **Project Organization:** Follow folder conventions strictly; key directories include `Controllers`, `Models`, `Views`, `Data`, and `Services`.

## Developer Workflows

- **Building & Running:** Use `dotnet build` and `dotnet run` from the terminal, or leverage the VS Code launch configurations provided in `Properties/launchSettings.json`.
- **Database Migrations:** Use commands such as `dotnet ef migrations add <MigrationName>` and `dotnet ef database update` to manage schema changes.
- **Debugging:** Utilize VS Code's integrated debugger. Key breakpoints are often set in controllers and `Program.cs` for startup issues.

## Integration and Dependencies

- **External Libraries:** Refer to `ProductCatalog.csproj` for dependencies like Entity Framework Core, ASP.NET Core Identity, and Humanizer.
- **Cross-Component Communication:** Controllers interact with services and DbContexts to process data and pass it to views.

## Guidelines for AI Agents

- **Feature Implementation:** When adding features, ensure adherence to the MVC pattern, and update corresponding tests and database migrations.
- **Modification Conventions:** Reflect changes in dependency injection configurations in `Program.cs` and document updates to seed data in `Data/SeedData.cs`.
- **Example Reference:** For product management patterns, refer to `Controllers/ProductController.cs` and associated views in `Views/Product`.

_This document is intended to help AI coding agents quickly understand project-specific patterns and developer workflows. Please provide feedback on unclear or missing sections._
