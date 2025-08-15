# ProductWarehouse

A modern .NET 9 application built with Clean Architecture principles, featuring a Blazor frontend and robust data management capabilities for warehouse product management.

## 🏗️ Architecture Overview

This project implements Clean Architecture with the following structure:

- **Presentation Layer**: ProductWarehouse.Presentation (Blazor UI)
- **Application Layer**: ProductWarehouse.Application (Business logic and use cases)
- **Domain Layer**: ProductWarehouse.Core (Core entities and business rules)
- **Infrastructure Layer**: ProductWarehouse.Infrastructure (Data access with EF Core + Dapper)
- **Shared Layer**: ProductWarehouse.Shared (Common DTOs and contracts)
- **Service Defaults**: ProductWarehouse.ServiceDefaults (Aspire service configurations)

## 🛠️ Technology Stack

- **.NET 9**: Latest .NET framework
- **ASP.NET Core**: Web framework
- **Blazor**: Frontend UI framework
- **.NET Aspire**: Cloud-native development platform
- **Entity Framework Core**: ORM for data access
- **Dapper**: Micro ORM for performance-critical queries
- **TestContainers**: Integration testing with real databases
- **GitHub Actions**: CI/CD pipeline

## 📋 Features

- Product CRUD operations
- Repository pattern implementation
- Clean Architecture separation of concerns
- Comprehensive test coverage
- Automated CI/CD pipeline

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for TestContainers and Aspire)
- [PostgreSQL](https://www.postgresql.org/download/) (optional, can use Docker)
- IDE: Visual Studio 2022, VS Code, or JetBrains Rider

### Clone the Repository

```bash
git clone https://github.com/yourusername/ProductWarehouse.git
cd ProductWarehouse
```

### Setup and Installation

1. **Restore NuGet packages:**
   ```bash
   dotnet restore
   ```

2. **Update database connection string:**
   - Update `appsettings.json` in the AppHost project with your PostgreSQL connection string
   - Or use the default connection string for local development
   - Example: `"Host=localhost;Database=ProductWarehouse;Username=postgres;Password=yourpassword"`

3. **Apply database migrations:**
   ```bash
   dotnet ef database update --project src/ProductWarehouse.Infrastructure --startup-project src/ProductWarehouse.AppHost
   ```

4. **Install Aspire workload (if not already installed):**
   ```bash
   dotnet workload install aspire
   ```

### Running the Application

#### Using .NET Aspire (Recommended)
```bash
dotnet run --project src/ProductWarehouse.AppHost
```

This will start the Aspire dashboard and orchestrate all services.

#### Running Individual Projects
```bash
# Start the presentation layer
dotnet run --project src/ProductWarehouse.Presentation

# Or with hot reload for development
dotnet watch --project src/ProductWarehouse.Presentation
```

The application will be available at:
- **Aspire Dashboard**: `https://localhost:17011` (HTTPS) or `http://localhost:15046` (HTTP)
- **Web App**: Check Aspire dashboard for dynamically assigned ports

## 🧪 Running Tests

### Unit Tests
```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test tests/ProductWarehouse.Infrastructure.Tests/
```

### Integration Tests
```bash
# Run integration tests (requires Docker for TestContainers)
dotnet test tests/ProductWarehouse.Infrastructure.Tests/

# Ensure Docker is running before executing integration tests
```

### Test Coverage Report
```bash
# Generate and view coverage report (requires reportgenerator tool)
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:coverage.cobertura.xml -targetdir:coverage -reporttypes:Html
```

## 📁 Project Structure

```
ProductWarehouse.sln
├── src/
│   ├── ProductWarehouse.AppHost/           # Aspire orchestration project
│   ├── ProductWarehouse.Application/       # Use cases and business logic
│   ├── ProductWarehouse.Core/             # Core business entities and interfaces  
│   ├── ProductWarehouse.Infrastructure/    # EF Core, Dapper, Repository implementations
│   ├── ProductWarehouse.Presentation/     # Blazor frontend
│   ├── ProductWarehouse.ServiceDefaults/  # Aspire service configurations
│   ├── ProductWarehouse.Shared/           # Common DTOs and contracts
│   └── ProductWarehouse.UI/               # Additional UI components
└── tests/
    └── ProductWarehouse.Infrastructure.Tests/  # Integration tests with TestContainers
```

## 🔧 Development

### Database Migrations
```bash
# Add new migration
dotnet ef migrations add MigrationName --project src/ProductWarehouse.Infrastructure --startup-project src/ProductWarehouse.AppHost

# Update database
dotnet ef database update --project src/ProductWarehouse.Infrastructure --startup-project src/ProductWarehouse.AppHost

# Remove last migration
dotnet ef migrations remove --project src/ProductWarehouse.Infrastructure --startup-project src/ProductWarehouse.AppHost
```

### Code Quality
This project includes:
- Code analysis rules
- EditorConfig for consistent formatting
- GitHub Actions for CI/CD

## 🚀 Deployment

### GitHub Actions CI/CD

The project includes automated workflows that:
- Run unit and integration tests
- Build the application
- Generate test coverage reports
- Deploy to staging/production environments

Workflows are triggered on:
- Pull requests to main branch
- Pushes to main branch

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Create a Pull Request

## 📝 License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

## 🐛 Troubleshooting

### Common Issues

1. **Docker not running**: Ensure Docker Desktop is running for TestContainers and Aspire
2. **PostgreSQL connection issues**: Verify connection strings in `appsettings.json` and ensure PostgreSQL is running
3. **Missing .NET workloads**: Run `dotnet workload restore`
4. **Port conflicts**: Check if ports 17011, 15046, 21012, 22080 are available
5. **Aspire dashboard not accessible**: Verify the ASPIRE_DASHBOARD_OTLP_ENDPOINT_URL environment variable

### Getting Help

- Create an issue in this repository
- Check the [.NET 9 documentation](https://docs.microsoft.com/en-us/dotnet/)
- Review [Aspire documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)

## 📚 Additional Resources

- [Clean Architecture Guide](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [.NET Aspire Documentation](https://learn.microsoft.com/en-us/dotnet/aspire/)
- [Blazor Documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/)
- [TestContainers Documentation](https://dotnet.testcontainers.org/)