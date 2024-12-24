# Skinet

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

# Client

This project was generated using [Angular CLI](https://github.com/angular/angular-cli) version 19.0.6.

## Create New Project

```sh
ng new client --skip-git --skip-tests --standalone --style scss --ssr false
```

This command initializes a new Angular project named "client" with the following options:

- `--skip-git`: Skips initializing a git repository.
- `--skip-tests`: Skips creating test files.
- `--standalone`: Uses standalone components instead of NgModules.
- `--style scss`: Sets the default stylesheet format to SCSS.
- `--ssr false`: Disables server-side rendering.

## Disable Angular CLI Analytics

This command disables the Angular CLI analytics feature globally for all projects on your machine. By running this command, you ensure that no usage data is sent to the Angular team, enhancing your privacy.

### Command

```sh
ng config -g cli.analytics false
```

### Explanation

- `ng config`: This is the Angular CLI command used to set or retrieve configuration values.
- `-g`: This flag indicates that the configuration change should be applied globally, affecting all Angular projects on your machine.
- `cli.analytics`: This is the specific configuration key that controls the analytics feature.
- `false`: This value disables the analytics feature.

## Development server

To start a local development server, run:

```bash
ng serve
```

Once the server is running, open your browser and navigate to `http://localhost:4200/`. The application will automatically reload whenever you modify any of the source files.

## Code scaffolding

Angular CLI includes powerful code scaffolding tools. To generate a new component, run:

```bash
ng generate component component-name
```

For a complete list of available schematics (such as `components`, `directives`, or `pipes`), run:

```bash
ng generate --help
```

## Generating a Header Component

To generate a new header component in your Angular project, run the following command:

```sh
ng g c layout/header --skip-tests
```

This command will create a new header component in the `layout` directory with the following options:

- `ng g c`: This is the Angular CLI command to generate a new component.
- `layout/header`: Specifies the path and name of the new component. The component will be created in the `layout` directory and named `header`.
- `--skip-tests`: Skips creating test files for the new component.

### Use of the Header Component

The header component is typically used to display the top navigation bar or header section of your application. It can include elements such as the logo, navigation links, user profile, and other important information that should be accessible from any page within the application.

### Example

After generating the header component, you can include it in your application's main layout. For example, you can add it to the `app.component.html` file:

```html
<app-header></app-header> <router-outlet></router-outlet>
```

This will ensure that the header is displayed on every page of your application, providing a consistent navigation experience for users.

## Building

To build the project run:

```bash
ng build
```

This will compile your project and store the build artifacts in the `dist/` directory. By default, the production build optimizes your application for performance and speed.

## Running unit tests

To execute unit tests with the [Karma](https://karma-runner.github.io) test runner, use the following command:

```bash
ng test
```

## Running end-to-end tests

For end-to-end (e2e) testing, run:

```bash
ng e2e
```

Angular CLI does not come with an end-to-end testing framework by default. You can choose one that suits your needs.

## Additional Resources

For more information on using the Angular CLI, including detailed command references, visit the [Angular CLI Overview and Command Reference](https://angular.dev/tools/cli) page.

# UI Packages

This section provides the necessary commands to set up Angular Material and Tailwind CSS in your Angular project.

1. Add Angular Material to your project:

```sh
ng add @angular/material
```

This command will add Angular Material to your Angular project. Angular Material is a UI component library for Angular developers. It helps to build consistent, modern web applications with a set of reusable UI components based on Google's Material Design.

2. Install Tailwind CSS along with PostCSS and Autoprefixer as development dependencies:

```sh
npm install -D tailwindcss postcss autoprefixer
```

- `tailwindcss`: A utility-first CSS framework for rapidly building custom user interfaces.
- `postcss`: A tool for transforming CSS with JavaScript plugins. It is used by Tailwind CSS to process your CSS files.
- `autoprefixer`: A PostCSS plugin which parses your CSS and adds vendor prefixes to CSS rules using values from "Can I Use".

3. Initialize Tailwind CSS configuration:

```sh
npx tailwindcss init
```

This command will create a `tailwind.config.js` file in your project. This file is used to customize your Tailwind CSS setup, such as defining custom colors, spacing, and other design tokens.

# mkcert

[Github URL](https://github.com/FiloSottile/mkcert)

mkcert is a simple tool for making locally-trusted development certificates. It requires no configuration.

```
$ mkcert -install
Created a new local CA 💥
The local CA is now installed in the system trust store! ⚡️
The local CA is now installed in the Firefox trust store (requires browser restart)! 🦊

$ mkcert example.com "*.example.com" example.test localhost 127.0.0.1 ::1

Created a new certificate valid for the following names 📜
 - "example.com"
 - "*.example.com"
 - "example.test"
 - "localhost"
 - "127.0.0.1"
 - "::1"

The certificate is at "./example.com+5.pem" and the key at "./example.com+5-key.pem" ✅
```

<p align="center"><img width="498" alt="Chrome and Firefox screenshot" src="https://user-images.githubusercontent.com/1225294/51066373-96d4aa80-15be-11e9-91e2-f4e44a3a4458.png"></p>

Using certificates from real certificate authorities (CAs) for development can be dangerous or impossible (for hosts like `example.test`, `localhost` or `127.0.0.1`), but self-signed certificates cause trust errors. Managing your own CA is the best solution, but usually involves arcane commands, specialized knowledge and manual steps.

mkcert automatically creates and installs a local CA in the system root store, and generates locally-trusted certificates. mkcert does not automatically configure servers to use the certificates, though, that's up to you.

## Configuring SSL in an Angular Application

Follow these steps to configure SSL in your Angular application:

1. **Generate SSL Certificates:**

   - Use a tool like `mkcert` to generate a self-signed certificate.
   - Example command:
     ```sh
     mkcert -install
     mkcert localhost
     ```
   - This will generate `localhost.pem` and `localhost-key.pem` files.

2. **Update Angular Configuration:**

   - Open `angular.json` and locate the `serve` options for your project.
   - Add the following SSL configuration:
     ```json
     "options": {
       "ssl": true,
       "sslKey": "path/to/localhost-key.pem",
       "sslCert": "path/to/localhost.pem"
     }
     ```

3. **Serve the Application with SSL:**

   - Use the Angular CLI to serve your application with SSL enabled.
   - Example command:
     ```sh
     ng serve --ssl --ssl-key path/to/localhost-key.pem --ssl-cert path/to/localhost.pem
     ```

4. **Access the Application:**
   - Open your browser and navigate to `https://localhost:4200`.
   - You should see your Angular application served over HTTPS.

## Notes

- For production environments, use certificates issued by a trusted Certificate Authority (CA).
- Ensure that your server configuration (e.g., Nginx, Apache) is also set up to handle SSL if deploying to a web server.

## Generating a Favicon

To generate a favicon for your website, you can use the [favicon.io favicon converter](https://favicon.io/favicon-converter/). Follow these steps:

1. Go to [favicon.io/favicon-converter](https://favicon.io/favicon-converter/).
2. Upload your image or provide a URL to the image you want to use.
3. Customize the favicon settings as needed.
4. Download the generated favicon files.
5. Include the downloaded favicon files in your website's root directory.
6. Add the appropriate HTML link tags to your website's `<head>` section to reference the favicon files.

Example:

```html
<link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png" />
<link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png" />
```

## Using PWABuilder.pwa-studio Extension

The `PWABuilder.pwa-studio` extension is a tool designed to help developers create Progressive Web Apps (PWAs) within Visual Studio Code. PWAs offer a native app-like experience to users, including offline capabilities, push notifications, and more.

### Key Features

1. **Manifest Generation**:

   - Helps create and edit the `manifest.json` file, which defines your PWA's metadata, such as its name, icons, start URL, and display mode.

2. **Service Worker Support**:

   - Assists in generating and managing service workers, enabling features like offline support, background sync, and push notifications.

3. **PWA Validation**:

   - Validates your PWA against best practices and standards, ensuring it meets the criteria for being a high-quality PWA.

4. **Integration with PWABuilder**:
   - Integrates with PWABuilder, making it easier to build and deploy your PWA for different platforms, including Windows, Android, and iOS.

### Example Workflow

1. **Install the Extension**:

   - Install `PWABuilder.pwa-studio` from the Visual Studio Code marketplace.

2. **Generate a Manifest**:

   - Use the extension to create a `manifest.json` file with the necessary metadata for your PWA.

3. **Create a Service Worker**:

   - Generate a service worker script to handle caching and offline functionality.

4. **Validate Your PWA**:

   - Run the validation tool to check for any issues or improvements.

5. **Package and Deploy**:
   - Use PWABuilder to package your PWA for different platforms and deploy it.

### Example `manifest.json`

```json
{
  "name": "My PWA",
  "short_name": "PWA",
  "start_url": "/",
  "display": "standalone",
  "background_color": "#ffffff",
  "theme_color": "#000000",
  "icons": [
    {
      "src": "/icons/icon-192x192.png",
      "sizes": "192x192",
      "type": "image/png"
    },
    {
      "src": "/icons/icon-512x512.png",
      "sizes": "512x512",
      "type": "image/png"
    }
  ]
}
```

This extension simplifies the process of creating and managing PWAs, making it accessible even for developers who are new to PWA development.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contact

For any questions or feedback, please contact [somasundar.journey@gmail.com](mailto:somasundar.journey@gmail.com).
