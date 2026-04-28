# ⚡ SportsReserves

A web application for managing sports facility reservations, built with **ASP.NET Core MVC**, **Entity Framework Core**, and **MySQL**. The system allows administrators to manage users, sport types, and time-based reservations through a clean, dark-themed interface.

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core 9 |
| Database | MySQL (via Pomelo EF Provider) |
| Frontend | Bootstrap 5, Bootstrap Icons |
| UI Design | [Claude.ai](https://claude.ai) — dark admin panel design, views redesign, CSS system |
| Bug Fixing | ChatGPT — debugging and improvements in `ReserveController` logic |

---

## 📁 Project Structure

```
SportsReserves/
├── Controllers/
│   ├── HomeController.cs
│   ├── ReserveController.cs
│   ├── SportController.cs
│   └── UserController.cs
├── DTOs/
│   ├── ReserveDto.cs
│   ├── SportDto.cs
│   └── UserDto.cs
├── Data/
│   └── AppDbContext.cs
├── Interfaces/
│   ├── IReserveService.cs
│   ├── ISportService.cs
│   └── IUserService.cs
├── Models/
│   ├── Reserve.cs
│   ├── Sport.cs
│   └── User.cs
├── Services/
│   ├── ReserveService.cs
│   ├── SportService.cs
│   └── UserService.cs
├── Views/
│   ├── Home/
│   ├── Reserve/
│   ├── Sport/
│   ├── User/
│   └── Shared/
├── Migrations/
├── wwwroot/
└── appsettings.json
```

---

## ⚙️ Prerequisites

Before running the project, make sure you have installed:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server 8+](https://dev.mysql.com/downloads/mysql/)
- [dotnet-ef CLI tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

Install the EF CLI tool if you don't have it:

```bash
dotnet tool install --global dotnet-ef
```

---

## 🗄️ Database Setup

### 1. Create the MySQL database

Log into your MySQL server and run:

```sql
CREATE DATABASE SportsReserves;
```

### 2. Configure the connection string

Open `appsettings.json` and update the connection string with your MySQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=SportsReserves;user=root;password=YOUR_PASSWORD;"
  }
}
```

### 3. Apply migrations

From the project root (where the `.csproj` file is), run:

```bash
dotnet ef database update
```

This will create all the required tables (`Users`, `Sports`, `Reserves`) automatically.

> **Note:** If you need to reset the database, you can drop it in MySQL and re-run `dotnet ef database update`.

---

## 🚀 Running the Project

```bash
# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

The application will start on `https://localhost:5001` (or the port shown in the terminal). The default home page redirects to the **Sports** list.

---

## 🗃️ Data Models

### User
| Field | Type | Description |
|---|---|---|
| Id | int | Primary key |
| Name | string | Full name |
| Document | string | ID document number |
| Phone | string | Phone number |
| Email | string | Email address |

### Sport
| Field | Type | Description |
|---|---|---|
| Id | int | Primary key |
| TypeSport | string | Sport name (e.g. Football, Basketball) |
| Capacity | int | Max number of participants |

### Reserve
| Field | Type | Description |
|---|---|---|
| Id | int | Primary key |
| UserId | int | FK → User |
| SportId | int | FK → Sport |
| Date | DateTime | Reservation date |
| HourStart | TimeSpan | Start time |
| HourEnd | TimeSpan | End time |
| State | string | Status: `Programada` / `Confirmada` / `Cancelada` |

---

## 🔗 Endpoints

All routes follow the ASP.NET MVC convention: `/{Controller}/{Action}/{id?}`

---

### 🏠 Home

| Method | Route | Description |
|---|---|---|
| GET | `/` | Dashboard (redirects to Sport/Index by default) |
| GET | `/Home/Index` | Main dashboard view |

---

### 📅 Reserve

| Method | Route | Description |
|---|---|---|
| GET | `/Reserve/Index` | List all reservations |
| GET | `/Reserve/Create` | Show create reservation form |
| POST | `/Reserve/Create` | Submit new reservation |
| POST | `/Reserve/ChangeState/{id}` | Update reservation status |
| POST | `/Reserve/Delete/{id}` | Delete a reservation |

**Reserve — Form Fields (POST /Reserve/Create)**

```
UserId    → int    (required) - Selected user ID
SportId   → int    (required) - Selected sport ID
Date      → date   (required) - Reservation date
HourStart → time   (required) - Start hour
HourEnd   → time   (required) - End hour
```

---

### 🏆 Sport

| Method | Route | Description |
|---|---|---|
| GET | `/Sport/Index` | List all sports |
| GET | `/Sport/Index?typeSport={name}` | Filter sports by name |
| GET | `/Sport/Create` | Show create sport form |
| POST | `/Sport/Create` | Submit new sport |
| GET | `/Sport/Edit/{id}` | Show edit form for a sport |
| POST | `/Sport/Edit/{id}` | Submit sport update |
| GET | `/Sport/Delete/{id}` | Show delete confirmation page |
| POST | `/Sport/Delete/{id}` | Confirm and delete a sport |

**Sport — Form Fields (POST /Sport/Create or /Sport/Edit/{id})**

```
TypeSport → string  (required) - Sport type name
Capacity  → int     (required) - Maximum capacity
```

---

### 👥 User

| Method | Route | Description |
|---|---|---|
| GET | `/User/Index` | List all users |
| GET | `/User/Create` | Show create user form |
| POST | `/User/Create` | Submit new user |
| GET | `/User/Edit/{id}` | Show edit form for a user |
| POST | `/User/Edit/{id}` | Submit user update |
| GET | `/User/Delete/{id}` | Show delete confirmation page |
| POST | `/User/Delete/{id}` | Confirm and delete a user |

**User — Form Fields (POST /User/Create or /User/Edit/{id})**

```
Name     → string  (required) - Full name
Document → string  (required) - ID document
Phone    → string  (required) - Phone number
Email    → string  (required) - Email address
```

---

## 🧩 Architecture

The project follows a layered architecture pattern:

```
Controller → Interface → Service → DbContext (EF Core) → MySQL
```

- **Controllers** handle HTTP requests and pass data to Views via `ViewBag` or strongly-typed models.
- **Services** contain business logic and are injected via interfaces (Dependency Injection).
- **DTOs** are used for form input to keep Models decoupled from form submissions.
- **EF Core** handles all database operations with async/await patterns throughout.

---

## 📦 NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
```

---

## 🤖 AI Tools Used

| Tool | Usage |
|---|---|
| **[Claude.ai](https://claude.ai)** | Full UI redesign — dark admin panel layout, Bootstrap 5 views, sidebar navigation, CSS design system, badge states, responsive layout |
| **[ChatGPT](https://chat.openai.com)** | Debugging and error fixing in `ReserveController`, specifically around async flow, `SelectList` loading, and state management |

---

## 📝 License

This project was developed for academic purposes.

---

*© 2026 SportsReserves*
