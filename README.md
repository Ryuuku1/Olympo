# Olympo - Gym Training API

A production-ready, containerized backend API for gym training management built with Clean Architecture and vertical slices.

## Features

### Core Functionality
- **Authentication & Authorization**: JWT-based authentication with User/Admin roles
- **User Management**: Registration, login, and profile management
- **Exercise Library**: Admin-managed exercise database with caching
- **Training Plan Management**: Admin-created training plans with weekly structure
- **Workout Session Tracking**: Real-time workout execution with set tracking
- **Personal Records**: Automatic PR detection and tracking

### Architecture
- **Clean Architecture** with vertical slice organization
- **Domain-Driven Design** principles
- **Repository Pattern** with Entity Framework Core
- **Minimal APIs** (no MVC controllers)
- **Request/Response** pattern (no DTOs)
- **Memory Caching** for exercise library

## Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: PostgreSQL with Entity Framework Core
- **Authentication**: JWT Bearer tokens
- **Caching**: IMemoryCache
- **Containerization**: Docker & Docker Compose

## API Endpoints

### Authentication
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login (returns JWT token)

### User Features
- `GET /api/myplan` - Get current user's training plan
- `GET /api/exercises` - Get exercise library (cached)

### Workout Sessions
- `POST /api/sessions/{workoutId}/start` - Start a workout session
- `PUT /api/sessions/{sessionId}` - Update set data (reps/load/notes)
- `POST /api/sessions/{sessionId}/complete` - Complete session with PR detection

## Quick Start

### Using Docker Compose (Recommended)

1. Clone the repository
2. Navigate to the project root
3. Run the application:

```bash
docker-compose up
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: http://localhost:5001
- OpenAPI: http://localhost:5000/swagger (in development)

### Manual Setup

1. Ensure PostgreSQL is running
2. Update connection string in `appsettings.json`
3. Run the API:

```bash
cd src/Olympo.Api
dotnet run
```

## Database

The application uses PostgreSQL with automatic database creation. The connection string in `docker-compose.yml` is pre-configured for the containerized setup.

### Key Entities
- **User**: User accounts with authentication
- **Exercise**: Exercise library with descriptions
- **TrainingPlan**: Admin-created workout programs
- **WorkoutSession**: Real-time workout tracking
- **PersonalRecord**: Automatic PR tracking

## Project Structure

```
Olympo/
├── src/
│   ├── Olympo.Api/           # API layer with minimal APIs
│   ├── Olympo.Application/   # Application layer with features
│   ├── Olympo.Domain/        # Domain entities and interfaces
│   └── Olympo.Infrastructure/ # Data access and external services
├── docker-compose.yml        # Container orchestration
├── Dockerfile               # API container definition
└── README.md               # This file
```

## Example Usage

### 1. Register a User
```json
POST /api/auth/register
{
  "email": "john@example.com",
  "password": "SecurePassword123",
  "firstName": "John",
  "lastName": "Doe",
  "weight": 75.5,
  "height": 180.0,
  "fitnessLevel": "Intermediate"
}
```

### 2. Login
```json
POST /api/auth/login
{
  "email": "john@example.com",
  "password": "SecurePassword123"
}
```

### 3. Start Workout Session
```json
POST /api/sessions/{workoutId}/start
{
  "workoutId": "guid-here"
}
```

### 4. Update Set Data
```json
PUT /api/sessions/{sessionId}
{
  "setNumber": 1,
  "repetitions": 10,
  "load": 100.0,
  "notes": "Felt strong today"
}
```

### 5. Complete Session
```json
POST /api/sessions/{sessionId}/complete
{
  "notes": "Great workout overall"
}
```

## Security

- JWT tokens with configurable expiration
- Password hashing with BCrypt
- Role-based authorization (User/Admin)
- HTTPS support in production
