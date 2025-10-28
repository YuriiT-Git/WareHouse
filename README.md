# 🏭 Warehouse Inventory System

A modular inventory management backend built with **.NET 8**, **Entity Framework Core**, and **Docker**.  
The system provides RESTful API endpoints for managing inventory items, tracking incoming and outgoing stock,  
and performing warehouse operations via Swagger UI.

---

## 🚀 Getting Started

### 1. Prerequisites

Make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

---

### 2. Run the infrastructure (databases, message brokers, etc.)

From the root of the repository:

```bash
docker compose up -d
This will start all required containers (database, monitoring, etc.)
defined in docker-compose.yaml.

3. Run the API
You can run the backend directly from your IDE (e.g. Rider / Visual Studio)
or via command line:

bash
dotnet run --project "Warehouse Inventory System/WIS.WebApi.Host"
The API will be available at:

HTTPS: https://localhost:5001

HTTP: http://localhost:5000

4. Open Swagger UI
Once the application is running, open your browser and navigate to:

👉 https://localhost:5001/swagger

From the Swagger interface, you can:

Explore all available endpoints

Execute API calls directly from the browser

View request and response models

