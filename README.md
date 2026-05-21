# Registration Management System

A full-stack web application built with ASP.NET Core Web API and Angular.

## Features

- User Registration with strong password policy
- Secure Login with JWT Authentication
- Cascading State and City dropdowns
- Multiple file upload with size and type validation
- Paginated registration list with sorting and filtering
- View and download uploaded documents
- Role-based access for edit and delete

## Tech Stack

**Backend**
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core (Code First)
- SQL Server
- JWT Authentication
- BCrypt password hashing

**Frontend**
- Angular
- TypeScript

## Getting Started

### Backend Setup
1. Clone the repository
2. Update connection string in appsettings.json
3. Run migrations: `Update-Database`
4. Run the API project

### Frontend Setup
1. Navigate to RegistrationManagementSystem.UI
2. Run `npm install`
3. Run `ng serve`
4. Open `http://localhost:4200`

## Project Structure
RegistrationManagementSystem.API
├── Controllers
├── Models
├── DTOs
├── Data
├── Repositories
├── Services
└── Helpers

## Author
Bhavyang Dixit