# Nutrition Backend API

REST API backend for the **Nutrition Department of Ubay** — a Barangay Nutrition Management System. It handles authentication, child nutrition records, report generation, and data entry for multiple nutrition-related programs (animal raising, potable water, iodized salt, CR, backyard gardening, pregnant women, vegetable seeds, and animal dispersal).

Built with **ASP.NET Core 9** and **Entity Framework Core** against a **MySQL** database.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Running the API](#running-the-api)
- [Project Structure](#project-structure)
- [Authentication](#authentication)
- [Authorization](#authorization)
- [Database](#database)
- [API Endpoints](#api-endpoints)
  - [Auth](#auth)
  - [Admin (Staff Management)](#admin-staff-management)
  - [Child Records](#child-records)
  - [Reports](#reports)
  - [Report Data Entry](#report-data-entry)
- [Middleware](#middleware)
- [Seeded Data](#seeded-data)
- [CORS](#cors)
- [Swagger](#swagger)
- [License](#license)

## Tech Stack

| Layer    | Technology |
| -------- | ---------- |
| Runtime  | .NET 9.0 (ASP.NET Core Web API) |
| Database | MySQL (via Pomelo.EntityFrameworkCore.MySql) |
| ORM      | Entity Framework Core 9.0 |
| Auth     | JWT Bearer (Microsoft.AspNetCore.Authentication.JwtBearer) |
| Password | BCrypt.Net-Next |
| Docs     | Swashbuckle / Swagger UI |
| Other    | AspNetCoreRateLimit, System.IdentityModel.Tokens.Jwt |

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [MySQL Server](https://dev.mysql.com/downloads/) (8.0+ recommended)
- Optional: MySQL Workbench or a DB client to inspect the database

### Installation

```bash
cd Nutrition_backend
dotnet restore
dotnet build
```

### Configuration

All settings live in `appsettings.json`. Update at minimum:

1. **Connection string** — point to your MySQL instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=NutritionDB;User=root;Password=YOUR_PASSWORD;"
}
```

2. **JWT settings** — change the signing key to a strong secret (at least 32 characters), and adjust the issuer/audience if needed:

```json
"Jwt": {
  "Key": "CHANGE_ME_AT_LEAST_32_CHARACTERS_LONG",
  "Issuer": "NutritionAPI",
  "Audience": "NutritionClient",
  "ExpiryMinutes": 1440
}
```

> **Note:** Tokens are currently issued with a fixed 24-hour expiry in `AuthService`.

### Running the API

```bash
dotnet run
```

The API listens on:

- `http://localhost:5210`
- `http://0.0.0.0:5210`

Swagger UI is served at the root (`/`) in development mode.

On startup, the application automatically applies any pending EF Core migrations.

## Project Structure

```
Nutrition_backend/
├── Controllers/          # API controllers (routes)
│   ├── AdminController.cs
│   ├── AuthController.cs
│   ├── ChildRecordsController.cs
│   ├── ReportDataEntryController.cs
│   └── ReportsController.cs
├── Data/
│   └── ApplicationDbContext.cs   # EF Core DbContext + model configuration + seed data
├── DTOs/                 # Request/response data transfer objects
├── Helpers/              # (reserved for helper utilities)
├── Middleware/
│   ├── ErrorHandlingMiddleware.cs
│   └── RateLimitMiddleware.cs
├── Migrations/           # EF Core migration files
├── Models/               # Entity models (tables)
├── Properties/
│   └── launchSettings.json
├── Services/
│   ├── AuthService.cs
│   ├── ChildRecordService.cs
│   ├── PasswordService.cs
│   ├── AnimalRaisingService.cs
│   ├── AnimalDispersalService.cs
│   ├── PotableWaterService.cs
│   ├── IodizedSaltService.cs
│   ├── CRService.cs
│   ├── BackyardGardeningService.cs
│   ├── PregnantWomenService.cs
│   ├── VegetableSeedService.cs
│   └── ReportService.cs
├── appsettings.json
├── appsettings.Development.json
└── Program.cs            # App entry point + DI wiring + pipeline setup
```

## Authentication

Login is done via `POST /api/auth/login` using either the username or email plus password. Passwords are hashed with BCrypt and verified server-side.

On success the API returns a JWT token plus user details:

```json
{
  "id": 1,
  "username": "admin",
  "email": "dextertenchavez@gmail.com",
  "role": "admin",
  "barangay": null,
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "expiresAt": "2026-08-18T..."
}
```

Send the token on protected endpoints via the `Authorization` header:

```
Authorization: Bearer <token>
```

**Login rate limiting:** The `/api/auth/login` endpoint is limited to **5 attempts per 5 minutes per IP address** (see [Middleware](#middleware)).

## Authorization

The API uses role-based authorization policies defined in `Program.cs`:

| Policy      | Role  | Applies to |
| ----------- | ----- | ---------- |
| `AdminOnly` | admin | Admin endpoints, overall reports |
| `StaffOnly` | staff | (policy defined for staff) |
| Default `[Authorize]` | any authenticated user | Child records & reports |

Controllers:

- `AdminController` — requires the `admin` role.
- `ReportsController` — requires authentication; `/overall` additionally requires `admin`.
- `ChildRecordsController` — requires authentication (any role).
- `ReportDataEntryController` — requires `admin` or `staff`.

## Database

MySQL database (default name `NutritionDB`) managed via EF Core Code-First migrations. Migrations are applied automatically on startup.

Key tables (`DbSet`s in `ApplicationDbContext`):

| Entity | Table / Description |
| ------ | ------------------- |
| `Users` | System users (admin & staff) |
| `Barangays` | Barangay list |
| `ChildRecords` | Individual child nutrition records |
| `VitaminAReports` | Vitamin A supplementation reports |
| `AnimalRaisingReports` | Household animal-raising inventory |
| `PotableWaterReports` | Potable water level assessment |
| `IodizedSaltReports` | Iodized salt / cooking oil inspection |
| `CRReports` | CR (child-related) household data |
| `BackyardGardeningReports` | Backyard gardening households |
| `PregnantWomenReports` | Pregnant women health data |
| `VegetableSeedReports` | Vegetable seed distribution |
| `AnimalDispersalReports` | Animal dispersal records |

To create migrations after model changes:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## API Endpoints

> All endpoints except `POST /api/auth/login` require a valid JWT token. Endpoints marked **[admin]** also require the `admin` role.

### Auth

| Method | Endpoint           | Description                  | Auth   |
| ------ | ------------------ | ---------------------------- | ------ |
| POST   | `/api/auth/login`  | Authenticate & get JWT token | Public (rate-limited) |

**Request body:**

```json
{
  "username": "admin",
  "password": "yourpassword"
}
```

### Admin (Staff Management)

| Method | Endpoint                     | Description                          | Auth    |
| ------ | ---------------------------- | ------------------------------------ | ------- |
| GET    | `/api/admin/staff`           | List all staff users                 | [admin] |
| POST   | `/api/admin/staff`           | Create a new staff account           | [admin] |
| PUT    | `/api/admin/staff/{id}/toggle` | Activate / deactivate a staff user | [admin] |
| DELETE | `/api/admin/staff/{id}`      | Delete a staff user                  | [admin] |

**Create staff body:**

```json
{
  "username": "juan",
  "email": "juan@ubay.gov.ph",
  "password": "secret123",
  "barangay": "Poblacion"
}
```

> `barangay` must be one of the valid barangays listed in `BarangayData`.

### Child Records

| Method | Endpoint                        | Description                     | Auth          |
| ------ | ------------------------------- | ------------------------------- | ------------- |
| POST   | `/api/childrecords/check-duplicate` | Check if a record already exists | Authenticated |
| POST   | `/api/childrecords`             | Create a child record           | Authenticated |
| GET    | `/api/childrecords`             | List all child records          | Authenticated |
| GET    | `/api/childrecords/{id}`        | Get a single child record       | Authenticated |
| PUT    | `/api/childrecords/{id}`        | Update a child record           | Authenticated |
| DELETE | `/api/childrecords/{id}`        | Delete a child record           | Authenticated |

**Child record body:**

```json
{
  "barangay": "Tintinan",
  "purok": 2,
  "targetCategory": "0-59 months",
  "fullName": "Juan Dela Cruz",
  "birthdate": "2022-01-15T00:00:00Z",
  "ageMonths": 36,
  "weight": 12.5,
  "height": 90.0,
  "nutritionalStatus": "Normal",
  "recordedDate": "2026-08-17T00:00:00Z"
}
```

**Duplicate check body:**

```json
{
  "fullName": "Juan Dela Cruz",
  "barangay": "Tintinan",
  "purok": 2,
  "excludeId": 0
}
```

### Reports

| Method | Endpoint                        | Description                              | Auth          |
| ------ | ------------------------------- | ---------------------------------------- | ------------- |
| GET    | `/api/reports/barangay/{barangay}` | Per-barangay (per-purok) nutrition report | Authenticated |
| GET    | `/api/reports/overall?year=2026`  | Overall municipal report (all barangays) | [admin]       |
| GET    | `/api/reports/child-records?barangay=` | Child records (optionally filtered)  | Authenticated |

Reports aggregate child records by age group (`6–11` and `12–59` months) and nutritional status (`Underweight` / `Severely Underweight`).

### Report Data Entry

Full CRUD for the eight program data-entry types. All endpoints require the `admin` or `staff` role. For each type, the pattern is:

| Method | Endpoint |
| ------ | -------- |
| POST   | `/api/reportdataentry/{type}` |
| GET    | `/api/reportdataentry/{type}/all` |
| GET    | `/api/reportdataentry/{type}/{barangay}/{year}` |
| PUT    | `/api/reportdataentry/{type}/{id}` |
| DELETE | `/api/reportdataentry/{type}/{id}` |

Available `{type}` values:

| Type | Description | Key fields |
| ---- | ----------- | ---------- |
| `animal-raising` | Household animal inventory | householdName, chicken/pig/goat/cow/carabao counts (male/female) |
| `potable-water` | Water level assessment | householdName, level1, level2, level3 |
| `iodized-salt` | Salt & oil inspection | storeName, fineSalt/rockSalt/oil brand booleans |
| `cr` | CR household data | householdName, withCR, withoutCR |
| `backyard-gardening` | Gardening presence | householdName, hasGarden |
| `pregnant-women` | Pregnant women health | womanName, weight, height, bmi, bmiCategory |
| `vegetable-seeds` | Seed distribution | householdName, seedTypes |
| `animal-dispersal` | Dispersal records | householdName, animal counts (male/female) |

## Middleware

### `RateLimitMiddleware`

Protects the login endpoint from brute-force attacks. Tracks requests per IP in memory:

- **5 login attempts** allowed per **5 minutes** per IP.
- On exceeding the limit it returns `429 Too Many Requests` with a `retryAfter` value.

### `ErrorHandlingMiddleware`

Global exception handler that converts unhandled exceptions into consistent JSON responses:

| Exception | HTTP Status |
| --------- | ----------- |
| `UnauthorizedAccessException` | 401 |
| `KeyNotFoundException` | 404 |
| `InvalidOperationException` | 400 |
| anything else | 500 |

## Seeded Data

On first run (`OnModelCreating` seed), the database is populated with:

**Admin user:**

```
Username: admin
Email:    dextertenchavez@gmail.com
Password: (BCrypt hash of the default password — see `ApplicationDbContext`)
Role:     admin
```

**Barangays:**

- `Tintinan`
- `Sample Barangay 1`
- `Sample Barangay 2`

> The full list of valid barangays for staff assignments and reporting lives in the static `BarangayData.AllBarangays` list in `Models/Barangay.cs`.

## CORS

A permissive CORS policy named `AllowAll` is enabled for all origins, methods, and headers. The `Cors:AllowedOrigins` section in `appsettings.json` is defined but not enforced by the active policy — tighten it as needed for production.

## Swagger

Swagger UI is available in development at the **root URL** (`http://localhost:5210/`). It includes JWT bearer authentication support — click **Authorize** and paste your token to test protected endpoints.

To get a token quickly, call `POST /api/auth/login` from the Swagger UI first.

## License

Internal project for the Nutrition Department of Ubay. No public license is applied.