# analitict-trend

## 📌 Overview
This project is a **C# .NET application** built using **Clean Architecture** principles to ensure:
- Clear separation of concerns
- High maintainability
- Minimal merge conflicts when working in a team
- Easy testing and future scalability

The application uses **SQLite** as the database and **Entity Framework Core (EF Core)** for data access.

---

## How to run BackEnd

- **First of all you need to clone the project**
- **Open file .sln**
- **Build Solutionm**
- **Press F5** then it will open browser then url https://localhost:7283/
- **You can use user email: admin@example.com password: admin123**

## 🧱 Architecture

This project follows **Clean Architecture**, where dependencies always point **inward**.

## 🗄 Database

- **Database**: SQLite  
- **ORM**: Entity Framework Core

### Why SQLite?
- Lightweight and fast
- No external DB server required

### SQLite File
By default, the SQLite database file is stored locally
