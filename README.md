# Movie-App

A Netflix-style **Movie & Watchlist API** built with ASP.NET Core 8, Entity Framework Core, MSSQL, and JWT authentication.

## Features

- 🔐 **JWT Authentication** – Register / Login with hashed passwords (BCrypt)
- 🎬 **Movies** – CRUD with search and pagination; add movies is Admin-only
- ⭐ **Watchlist** – Authenticated users can add/remove movies and view their watchlist
- 🔍 **Search History** – Save and retrieve user search queries filtered by type
- 📖 **Swagger UI** – Fully documented and explorable API at `/`
- 🛡️ **Global Error Handling** – Middleware that returns consistent JSON error responses
- 📄 **Pagination** – All list endpoints support `page` and `pageSize` query parameters
- 👤 **Role-based Auth** – `Admin` / `User` roles

## Project Structure

```
MovieApp.API/
├── Controllers/        AuthController, MoviesController, WatchlistController, SearchController
├── Models/             User, Movie, Watchlist, SearchHistory
├── DTOs/               RegisterDto, LoginDto, MovieDto, WatchlistDto, CreateMovieDto
├── Data/               AppDbContext (EF Core Code-First)
├── Repositories/       Interfaces + Implementations (Repository Pattern)
├── Services/           AuthService, MovieService, WatchlistService
├── Helpers/            JwtHelper, PasswordHasher (BCrypt)
├── Middleware/         GlobalExceptionMiddleware
├── Program.cs
└── appsettings.json
```

## API Endpoints

| Method | Endpoint | Auth |
|--------|----------|------|
| POST | `/api/auth/register` | Public |
| POST | `/api/auth/login` | Public |
| GET | `/api/movies?page=1&pageSize=10` | Public |
| GET | `/api/movies/{id}` | Public |
| GET | `/api/movies/search?query=batman` | Public |
| POST | `/api/movies` | Admin |
| GET | `/api/watchlist` | User |
| POST | `/api/watchlist/{movieId}` | User |
| DELETE | `/api/watchlist/{movieId}` | User |
| GET | `/api/search/history?type=movie` | User |
| POST | `/api/search/history` | User |

## Setup

1. Update the connection string in `appsettings.json` to point at your MSSQL instance.
2. Run EF Core migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
3. Run the API:
   ```bash
   dotnet run --project MovieApp.API
   ```
4. Open Swagger UI at `http://localhost:<port>/`
