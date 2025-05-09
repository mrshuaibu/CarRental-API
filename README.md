# 🚗 Car Rental API

The **Car Rental API** is a RESTful web service built with ASP.NET Core for 
managing car rental operations. It supports operations like managing vehicles, 
customers, and bookings, with structured DTOs and AutoMapper integration to 
ensure clean, efficient data handling.

This project is ideal for practicing API development, clean architecture, and 
testing concepts in C#.

---

## 🎯 Project Aim

The main goal of this API is to:

- Enable creation, updating, and deletion of cars, customers, and bookings
- Practice using **DTOs** and **AutoMapper**
- Ensure separation of concerns with a layered architecture
- Support **unit testing** and clean entity relationships
- Provide a learning resource for REST API development in ASP.NET Core

---

## 🛠️ Technologies Used

- **ASP.NET Core Web API**
- **Entity Framework Core**
- **AutoMapper**
- **SQL Server** (or SQLite for local testing)
- **Swagger / Swashbuckle** (for API documentation)
- **xUnit** for testing
- **Dependency Injection**

---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET SDK 6.0 or higher](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Visual Studio 2022+ or Visual Studio Code
- SQL Server or SQLite

---

### 📦 Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/your-username/car-rental-api.git
   cd car-rental-api

2. **Update the database connection string in appsettings.json:**
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=IRL_DB;Trusted_Connection=True;"
}
3. **Apply migrations**
    If migrations are already included, just update the database:
    update-database

    Otherwise, add a new migration and apply it:
    Add-migration
    update-database
4. **Run the Application**
