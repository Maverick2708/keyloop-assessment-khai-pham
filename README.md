# Keyloop Assessment: Intelligent Inventory Dashboard (Scenario B)

A backend service for an Intelligent Inventory Dashboard built with ASP.NET Core Web API. The system manages vehicle inventory, identifies aging stock (vehicles in inventory for more than 90 days), and logs proposed actions against vehicles.

## Prerequisites

- .NET 10 SDK (or later)
- An IDE (Visual Studio, VS Code, or Rider)
- Git

## Getting Started

### Clone the Repository

```bash
git clone <repository-url>
cd InventoryDashboard.Api
```

### Build the Application

```bash
dotnet build
```

### Run the Application

```bash
dotnet run
```

The application will start on `http://localhost:5000` (or the port configured in `Properties/launchSettings.json`).

### Access Swagger UI

Once the application is running in Development mode, navigate to:

```
http://localhost:5000/swagger
```

The Swagger UI provides interactive documentation for all API endpoints.

### Database

The application uses SQLite with a local file (`inventory.db`). The database is automatically created and seeded with sample data on first run — no manual setup required.

### Run Tests

```bash
dotnet test InventoryDashboard.Tests --verbosity normal
```

This executes the xUnit test suite which validates the core business logic including the `IsAgingStock` computed property and `InventoryService` operations using an EF Core InMemory database.

## Core API Endpoints

### GET /api/inventory

Returns all vehicles in inventory, including their associated actions and computed `IsAgingStock` status.

**Response:** `200 OK` with a JSON array of vehicle objects.

### POST /api/inventory/{vehicleId}/actions

Logs a proposed action against a specific vehicle.

**Request Body:**

```json
{
  "proposedAction": "Reduce price by 15%"
}
```

**Responses:**
- `201 Created` — Action successfully logged.
- `404 Not Found` — Vehicle with the specified ID does not exist.

## Project Structure

```
InventoryDashboard.Api/
├── Controllers/          # API endpoints
├── Data/                 # EF Core DbContext and database seeder
├── Middleware/           # Global exception handling
├── Models/               # Domain entities (Vehicle, VehicleAction)
├── Services/             # Business logic layer
├── InventoryDashboard.Tests/  # xUnit test project
├── Program.cs            # Application entry point and DI configuration
└── SYSTEM_DESIGN.md      # Detailed system design document
```

## AI Collaboration Narrative

In this assessment, I utilized Generative AI as an iterative pair programmer while firmly maintaining the role of Tech Lead and System Architect.

1. **High-Level Guiding Strategy:**

   > Instead of prompting the AI to generate the entire application at once, I guided it through a step-by-step, domain-driven approach. I started with Domain Models and EF Core, moved to Services, and finally exposed the API Controllers and Middleware.

2. **Verification and Refinement Process:**

   > I did not accept the AI's output blindly. For instance, I ensured the 'aging stock' logic (>90 days) was neatly encapsulated as a computed property (IsAgingStock) directly within the Domain Model, rather than cluttered in the service layer. I also explicitly directed the AI to create a robust xUnit test suite using an InMemory database to verify edge cases.

3. **Ensuring Final Code Quality:**

   > I took full ownership of the final architecture. The decisions to use .NET 10, N-Tier architecture, Dependency Injection, and Global Exception Middleware were my intentional architectural choices. The AI was utilized strictly to execute these design decisions efficiently.
