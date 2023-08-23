# Logistic Booking System

The Logistic Booking System is a server-side C# application built using Domain-Driven Design (DDD) and Command Query Responsibility Segregation (CQRS). It provides functionalities to manage load/unload bookings for trucks at a warehouse.

## Features

- Booking Management: Create, update, and delete bookings for specific locations.
- Location Management: Manage the details of warehouse locations.
- Business Rules: Enforce various business rules such as location capacity, booking time window, booking state transition, booking conflict, booking cancellation, booking completion, and location booking capacity.
- Server-Side Validation: Validate incoming requests using FluentValidation or a similar library.
- Error Handling: Implement appropriate error handling and exception handling mechanisms.
- Database Integration: Utilize Microsoft SQL Server or MySQL as the database using Entity Framework Core for database access.
- Unit Tests: Write comprehensive unit tests to cover business rules and key functionality.

## Technologies Used

- C#/.NET
- Domain-Driven Design (DDD)
- Command Query Responsibility Segregation (CQRS)
- Microsoft SQL Server 
- Entity Framework Core
- MediatR (for command and query handling)
- FluentValidation (for server-side validation)

## Setup and Configuration

1. Install the required dependencies: .NET SDK, Entity Framework Core, etc.
2. Clone the repository: `git clone https://github.com/thebhavikthakkar/LogisticBookingSystemTest.git`
3. Navigate to the project directory: `cd LogisticBookingSystem`
4. Configure the database connection in the `appsettings.json` file.
5. Run database migrations to create the necessary tables: `dotnet ef database update`, In case you would like to setup database directly via script then Navigate to folder - DbScript amd execute that script. 
6. Build the project: `dotnet build`
7. Run the application: `dotnet run`

## API Endpoints

The following endpoints are available in the Logistic Booking System:

- `GET /api/locations`: Get all locations.
- `GET /api/locations/{id}`: Get a specific location by ID.
- `POST /api/locations`: Create a new location.
- `PUT /api/locations/{id}`: Update an existing location.
- `DELETE /api/locations/{id}`: Delete a location.

- `GET /api/bookings`: Get all bookings.
- `GET /api/bookings/{id}`: Get a specific booking by ID.
- `POST /api/bookings`: Create a new booking.
- `PUT /api/bookings/{id}`: Update an existing booking.
- `DELETE /api/bookings/{id}`: Delete a booking.
- `POST /api/bookings/{id}/process`: Process a booking and update its state.

Refer to the API documentation or Swagger UI for more details on request/response formats and data validation.

## Demo - https://www.loom.com/share/63f89435a9cf473783d6c5f35eda5e2a

## Testing

The project includes unit tests to validate the business rules and key functionality. To run the tests, use the following command:

```bash
dotnet test
