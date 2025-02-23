# 📚 Book Management API

A **RESTful API** built using **ASP.NET Core Web API** for managing books. This API supports **CRUD** operations, soft deletion, pagination, bulk operations, and features **JWT-based authentication** for secure access.

---

## 🚀 Features

- 📖 **CRUD Operations** for books (Create, Read, Update, Delete)
- 🗑️ **Soft Delete** functionality
- ➕ **Bulk Add/Delete** of books
- 📃 **Pagination & Sorting** (List books from most to least popular)
- ⭐ **Popularity Score** calculation (based on views & age)
- 📊 **Swagger/OpenAPI** integration for API documentation

---

## 🛠️ Technologies Used

- **ASP.NET Core Web API (.NET 9)**
- **Entity Framework Core**
- **SQL Server**
- **Swagger (Swashbuckle)**

---

## 📂 Project Structure

```
BookManagementAPI/
├── Controllers/
│   └── BooksController.cs
├── Data/
│   └── ApplicationDbContext.cs
├── Models/
│   └── Book.cs
├── Repositories/
│   ├── IBookRepository.cs
│   └── BookRepository.cs
├── Services/
│   └── BookService.cs
├── Migrations/
├── appsettings.json
├── Program.cs
└── README.md
```

---

## ⚡ Getting Started

### 1️⃣ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/downloads/) or any API testing tool

---

### 2️⃣ Installation

1. **Clone the repository:**
    
    ```bash
    git clone https://github.com/yourusername/BookManagementAPI.git
    cd BookManagementAPI
    ```
    
2. **Configure the Database:**
    
    Update your **`appsettings.json`** with your SQL Server connection string:
    
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=BookManagementDB;Trusted_Connection=True;"
    }
    ```
    
3. **Run Migrations:**
    
    Apply Entity Framework Core migrations to create the database schema:
    
    ```bash
    dotnet ef database update
    ```
    
4. **Run the Application:**
    
    Start the API:
    
    ```bash
    dotnet run
    ```
    
5. **Access Swagger UI:**
    
    Navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) to explore and test the API using **Swagger UI**.
    

---

## 📋 API Endpoints

### 📚 Books

|Method|Endpoint|Description|
|---|---|---|
|GET|`/api/books`|Get all books (with pagination & sorting)|
|GET|`/api/books/{id}`|Get details of a specific book|
|POST|`/api/books`|Add one or multiple books|
|PUT|`/api/books/{id}`|Update a specific book|
|DELETE|`/api/books`|Delete one or multiple books (soft delete)|

---

## 🧪 Testing the API

You can test the API using:

1. **Swagger UI:** Navigate to [http://localhost:5000/swagger](http://localhost:5000/swagger) for an interactive API tester.
    
2. **Postman:** Import the API endpoints and test CRUD operations.
    
3. **cURL:** Example to get all books:
    
    ```bash
    curl -X GET "https://localhost:5000/api/books" -H "accept: application/json"
    ```
    

---

## ⚙️ Popularity Score Calculation

- **Formula:**
    
    ```plaintext
    Popularity Score = (BookViews * 0.5) + (YearsSincePublished * 2)
    ```
    
- **BookViews:** Number of times the book was accessed.
    
- **YearsSincePublished:** Age of the book (older books get a smaller bonus).
    

---

## 💡 Notes

- This project was created as a **test assignment** for the **Exadel .NET Bootcamp**.
- Contributions and feedback are welcome! 😊🚀
