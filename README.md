<div align="center">
  <h1>🍽️ Restaurant Management API</h1>
  <p>
    <strong>A robust RESTful API for restaurant management built with .NET 9 and Clean Architecture</strong>
  </p>
  
  <img src="https://img.shields.io/badge/.NET%209-512BD4?logo=dotnet" alt=".NET 9">
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoft-sql-server&logoColor=white" alt="SQL Server">
  <img src="https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=black" alt="Swagger">
  <img src="https://img.shields.io/badge/License-MIT-yellow.svg" alt="License: MIT">
</div>

## 📖 Overview

This project is a comprehensive Restaurant Management System API built with the latest .NET 9 technologies. It serves as a robust backend solution for managing restaurants, their Dishes, and user authentication. The architecture follows Clean Architecture principles, ensuring maintainability, testability, and scalability.

### 🎯 Key Features

#### 🔐 Authentication & Authorization
- JWT-based authentication with role-based access control
- Secure password hashing with ASP.NET Core Identity
- Refresh token mechanism for enhanced security
- Role-based authorization (Admin, Owner, User)

#### 🏗️ Architecture
- **Clean Architecture** with clear separation of concerns
- **CQRS Pattern** for better separation of read and write operations
- **Domain-Driven Design** principles
- Repository and Unit of Work patterns
- Mediator pattern using MediatR

#### 🛠️ Technical Features
- **RESTful API** with proper HTTP methods and status codes
- **Entity Framework Core 9 with SQL Server** for data access
- **FluentValidation** for request validation
- **AutoMapper** for object-to-object mapping
- **xUnit** and **Moq** for unit and integration testing
- **OpenAPI/Swagger** documentation
- **Serilog** for structured logging

## 🏗️ Project Structure

```
📦 Restaurant.Api           # API Layer (Presentation)
│   ├── Controllers/       # API endpoints
│   ├── Middlewares/       # Global exception handling, request logging
│   ├── Filters/           # Action filters for request/response handling
│   ├── Extensions/        # Startup configuration extensions
│   └── appsettings.json   # Configuration
│
📦 Restaurant.Application   # Application Layer (Use Cases)
│   ├── Common/            # Shared application logic
│   ├── Dishes/            # Dish-related commands and queries
│   ├── Restaurants/       # Restaurant-related commands and queries
│   ├── Users/             # User management logic
│   └── Extensions/        # Service collection extensions
│
📦 Restaurant.Domain        # Domain Layer (Business Logic)
│   ├── Entities/          # Core domain models
│   ├── Enums/             # Domain-specific enums
│   ├── Exceptions/        # Custom exceptions
│   ├── Interfaces/        # Repository interfaces
│   └── ValueObjects/      # Domain value objects
│
📦 Restaurant.Infrastructure # Infrastructure Layer
│   ├── Data/              # Database context and configurations
│   ├── Identity/          # Identity configuration
│   ├── Services/          # External service implementations
│   └── Repository/        # Repository implementations
```

## 🚀 Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server 2019 or later](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2026 Insider](https://visualstudio.microsoft.com/vs/preview/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/MahmoodElbadri/restaurant-api.git
   cd restaurant-api
   ```

2. **Set up the database**
   - Update the connection string in `appsettings.Development.json` to point to your SQL Server instance. Example:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=YOURdB;Trusted_Connection=True;TrustServerCertificate=True;"
     }

3. **Apply database migrations**
   ```bash
   cd Restaurant.Infrastructure
   dotnet ef database update --startup-project ../Restaurant.Api
   ```
   
   > **Note:** Ensure SQL Server is running and the connection string is correctly configured before running migrations.

4. **Run the application**
   ```bash
   dotnet run --project Restaurant.Api
   ```
   - API: `https://localhost:5001`
   - Swagger UI: `https://localhost:5001/swagger`

## 📚 API Documentation

### Authentication
| Endpoint | Method | Description | Required Role |
|----------|--------|-------------|---------------|
| `/api/identity/register` | POST | Register a new user | Public |
| `/api/identity/login` | POST | Authenticate and get JWT token | Public |
| `/api/identity/refresh-token` | POST | Refresh access token | Authenticated |

### Restaurants
| Endpoint | Method | Description | Required Role |
|----------|--------|-------------|---------------|
| `/api/restaurants` | GET | Get all restaurants (with filtering and pagination) | Any |
| `/api/restaurants/{id}` | GET | Get restaurant by ID | Any |
| `/api/restaurants` | POST | Create a new restaurant | Admin, Owner |
| `/api/restaurants/{id}` | PUT | Update a restaurant | Admin, Owner |
| `/api/restaurants/{id}` | DELETE | Delete a restaurant | Admin |

### Dishes
| Endpoint | Method | Description | Required Role |
|----------|--------|-------------|---------------|
| `/api/dishes` | GET | Get all dishes (with filtering) | Any |
| `/api/dishes/{id}` | GET | Get dish by ID | Any |
| `/api/dishes` | POST | Create a new dish | Admin, Owner |
| `/api/dishes/{id}` | PUT | Update a dish | Admin, Owner |
| `/api/dishes/{id}` | DELETE | Delete a dish | Admin |

## 🛡️ Security

- JWT token-based authentication
- Role-based authorization
- Secure password hashing
- HTTPS enforced in production
- CORS policy configuration
- Input validation and sanitization

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


## 🙏 Acknowledgments

- [.NET 9](https://dotnet.microsoft.com/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [CQRS Pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [MediatR](https://github.com/jbogard/MediatR)