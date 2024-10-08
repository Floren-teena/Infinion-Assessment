# Infinion-Assessment - An ASP.NET Core Web Application Project

This project is a web application built using ASP.NET Core with Earth Layer Architecture. The application provides user authentication (registration and login), product CRUD operations with pagination and filtering, and email services using SMTP.

## Project Structure

The project follows Earth Layer Architecture with the following layers:

- **API Layer**: Contains the ASP.NET Core Web API that handles HTTP requests.
- **Application Layer**: Contains business logic, DTOs, utility classes, services and validators.
- **Data Layer**: Manages database access and repositories.
- **Domain Layer**: Defines the core entities(models).

## Features

1. **User Registration**: Users can register with their First and Last name, email, and password. The registration endpoint sends a welcome email after successful registration.
2. **User Login**: Users can log in with their email and password to receive a JWT token for authentication.
3. **Product Management**:
   - **Create**, **Update**, **Delete**, **Get by ID**, and **Get All** products with pagination and filtering.
4. **Email Services**: Sends emails using SMTP.

## Prerequisites

- .NET 6.0 SDK or higher
- SQL Server (or your preferred database)
- Visual Studio 2022 or Visual Studio Code
- Postman or Swagger for testing API endpoints

## Installation

1. **Clone the Repository**
   git clone https://github.com/Floren-teena/Infinion-Assessment.git
2. **Open the Project** Open the solution file Florentina_Infinion_Assessment.API.sln in Visual Studio or Visual Studio Code.
3. **Install Dependencies** Restore NuGet packages for all projects by running the following command in the solution directory:
   dotnet restore
4. **Database Configuration** Open appsettings.json in the API project and configure the database connection string:
   "ConnectionStrings": {
   "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Initial Catalog=ProductsDb;Integrated Security=True;Trust Server Certificate=True";"
   }
   Run the migrations in the NuGet Package Manager Console to create the database:
   Update-Database
5. **Configure Email Service** In appsettings.json, add your email provider’s SMTP settings under the "EmailSettings" section:
   "EmailSettings": {
   "Host": "your-SMTP-server",
   "Port": 587,
   "Username": "your-email@example.com",
   "Password": "your-email-app-password"
   }

## How to run the project

1. **Build the Solution and Run the API** In Visual Studio or using CLI:
   dotnet build
   dotnet run --project API
2. **Test the API Endpoints** Using Swagger or Postman

## Api Endpoints

### Authentication Endpoints

1. **User Registration**

- URL: /api/auth/register
- Method: POST
- Request Body:
  {
  "firstName": "John",
  "lastName": "Doe",
  "email": "johndoe@example.com",
  "password": "Password123!"
  }

2. **User Login**

- URL: /api/auth/login
- Method: POST
- Request Body:
  {
  "email": "johndoe@example.com",
  "password": "Password123!"
  }
- Response: Returns a JWT token that can be used for further authenticated requests.

### Product Endpoints

1. **Get All Products**

- URL: /api/products-get-all-products
- Method: GET
- Query Parameters: filter, page, pageSize

2. **Get Product by ID**

- URL: /api/products/get-all-products-by-id-{id}
- Method: GET

3. **Add a Product**

- URL: /api/products/add-products
- Method: POST
- Request Body:
  {
  "name": "Product Name",
  "description": "Product Description",
  "price": 100
  }

4. **Update a Product**

- URL: /api/products/update-product-{id}
- Method: PUT
- Request Body:
  {
  "name": "Updated Name",
  "description": "Updated Description",
  "price": 120
  }

5. **Delete a Product**

- URL: /api/products/delete-product-{id}
- Method: DELETE

### Technologies Used

- C# / ASP.NET Web API - .NET 8.0
- Entity Framework Core for database access
- JWT for authentication
- FluentValidation for input validation
- MailKit for sending emails via SMTP
- Swagger for API documentation
- SQL Server as the database (can be replaced with any supported DB)

### License

This project is licensed under the MIT License.
This `README.md` file provides clear instructions on how to set up, run, and test the application, along with an overview of its architecture and features. You can customize it as needed based on the specifics of your project.
