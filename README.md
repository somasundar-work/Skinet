# Skinet E-Commerce

## Overview

Skinet E-Commerce is an online shopping platform that allows users to browse and purchase products. This project is built using modern web technologies and follows best practices for scalability and maintainability.

## Features

- User authentication and authorization
- Product browsing and searching
- Shopping cart and checkout process
- Order management
- Responsive design

## Technologies Used

- Backend: ASP.NET Core
- Database: PostgreSQL
- Authentication: JWT

## Project Structure

The project is organized into the following main directories:

- **API**: Contains the ASP.NET Core Web API project.
- **Core**: Contains the core domain entities and interfaces.
- **Infrastructure**: Contains the implementation of the core interfaces, including data access and external services.

### API

The API project is the entry point of the application and contains the following key components:

- **Controllers**: Handle HTTP requests and return responses.
- **Models**: Define the data structures used in the application.
- **Services**: Contain the business logic of the application.
- **Repositories**: Handle data access and database operations.

### Core

The Core project contains the domain entities and interfaces:

- **Entities**: Define the main business objects.
- **Interfaces**: Define the contracts for the services and repositories.

### Infrastructure

The Infrastructure project contains the implementations of the core interfaces:

- **Data**: Contains the Entity Framework Core DbContext and migrations.
- **Repositories**: Implement the data access logic using Entity Framework Core.
- **Services**: Implement external service integrations.

## Architecture

The project follows a clean architecture approach, separating concerns into different layers:

- **API Layer**: Handles HTTP requests and responses.
- **Application Layer**: Contains business logic and service implementations.
- **Domain Layer**: Contains core entities and interfaces.
- **Infrastructure Layer**: Contains data access and external service implementations.

### Design Patterns

The project uses several design patterns to ensure maintainability and scalability:

- **Repository Pattern**: Used to abstract data access logic.
- **Specification Pattern**: Used to encapsulate query logic.
- **Dependency Injection**: Used to manage dependencies and promote loose coupling.

## Getting Started

### Prerequisites

- .NET Core SDK
- PostgreSQL

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/skinet-ecommerce.git
   ```
2. Navigate to the project directory:
   ```bash
   cd "Skinet E-Commerce"
   ```
3. Install backend dependencies:
   ```bash
   cd API
   dotnet restore
   ```

### Configuration

1. Update the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Host=your_host;Database=your_database;Username=your_username;Password=your_password"
   }
   ```

### Running the Application

1. Apply database migrations:
   ```bash
   cd API
   dotnet ef database update
   ```
2. Start the backend server:
   ```bash
   dotnet run
   ```
3. Open your browser and navigate to `http://localhost:5000`.

## Database Migrations

To add a new migration, use the following command:

```bash
dotnet ef migrations add MigrationName
```

To remove the last migration, use the following command:

```bash
dotnet ef migrations remove
```

To update the database with the latest migrations, use the following command:

```bash
dotnet ef database update
```

## Testing

The project includes unit tests to ensure the correctness of the application. The tests are organized into the following categories:

- **Unit Tests**: Test individual components in isolation.
- **Integration Tests**: Test the interaction between multiple components.

To run the tests, use the following command:

```bash
dotnet test
```

## Code Formatting with CSharpier

This project utilizes **CSharpier**, an opinionated code formatter for C#. CSharpier ensures that the codebase maintains a consistent coding style by automatically formatting the code according to its predefined rules. This helps improve code readability and maintainability, making it easier for developers to collaborate on the project.

### [CSharpier Official Docs](https://csharpier.com/docs/About)

### How to Use CSharpier

To use CSharpier, follow these steps:

1. Install CSharpier as a global tool using the following command:
   ```sh
   dotnet tool install -g csharpier
   ```
2. Run CSharpier on your code files by executing:
   ```sh
   csharpier .
   ```
   This command will format all the C# files in the current directory and its subdirectories.

By integrating CSharpier into your workflow, you can ensure that your code remains clean and consistent throughout the development process.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

For any questions or feedback, please contact [somasundar.journey@gmail.com](mailto:somasundar.journey@gmail.com).
