# 🚀 Mottrist Project Documentation

Welcome to the Mottrist REST API!
Mottrist is a powerful .NET 9 and MSSQL-based application leveraging Entity Framework Core (EF Core) for seamless data management. It features efficient database initialization and comes preloaded with seed data, including languages, car fields, cities, and countries, ensuring a structured and well-organized system.
The Mottrist REST API is your gateway to managing a comprehensive travel platform that connects travelers with drivers, facilitating seamless travel experiences. This Postman collection provides a complete set of tools to interact with the Mottrist API, enabling you to manage Travelers, Drivers, Cars, Destinations, Messages, Countries, Cities, Languages, and access Admin and Employee dashboards.

---

## 📌 Overview

Mottrist is a robust platform designed to streamline travel logistics by connecting travelers with drivers and providing rich metadata for destinations, vehicles, and communication. The API, built with ASP.NET Core, offers a RESTful interface to perform operations such as creating and updating driver profiles, managing traveler accounts, retrieving location data, sending messages, and authenticating users. It leverages dependency injection for service interactions, standardized ApiResponse objects for consistent responses, and role-based authorization (Admin, Employee, Driver, Traveler) to secure sensitive operations.

This Postman collection organizes all API endpoints into logical folders, each corresponding to a key component of the Mottrist application. Each folder includes an endpoint table listing HTTP methods, paths, and descriptions, along with an overview of the controller's purpose and functionality. The collection supports both Development and Production environments, with clear instructions for setting up authentication and environment variables.

### Key Features

* **Comprehensive Management:** Supports end-to-end operations for drivers, travelers, destinations, messages, and metadata (car brands, car fields, countries, cities, languages).
* **Role-Based Authorization:** Secures endpoints with roles (Admin, Employee, Driver, Traveler) where applicable, while allowing public access (\[AllowAnonymous]) for read-heavy or onboarding endpoints.
* **Pagination Support:** Implements pagination for large datasets (e.g., drivers, travelers, destinations, messages, car brands) to optimize performance.
* **Standardized Responses:** Uses ApiResponse objects for consistent response formats, with HTTP status codes (e.g., 200 OK, 201 Created, 400 Bad Request, 404 Not Found, 500 Internal Server Error).
* **Public and Secure Access:** Balances public access for data retrieval (e.g., countries, cities, languages, car fields) with authenticated access for sensitive operations (e.g., user management, driver updates).

---

## 🏗️ Technologies Used
- **Backend**: `ASP.NET Core WebAPI (.NET 9)`
- **Database**: `Microsoft SQL Server`
- **ORM**: `Entity Framework Core`
- **Database Initialization**: `Preconfigured Seed Data for Languages, Cities, Countries, and Car Fields`
- **Authentication**: `Default User System with Secure Access Controls`

---

## 🔧 Setup & Installation
### 1- **Clone Repository**
```bash
git clone  https://github.com/arammtech/Mottrist.git
cd mottrist
```

### 2- Run Migrations & Seed Data
```bash
dotnet ef database update
dotnet run
```
---

### 📜 License
This project is Apache License 2.0 licensed 


### **🔹 Key Features**
✅ **Organized Technology Section** → Highlights `.NET 9`, **EF Core**, and **MSSQL**.  
✅ **Step-by-step Setup Guide** → Makes installation easy.  
✅ **Seed Data Information** → **Languages, Cities, and Countries** included.  
✅ **License Mention** → **Apache License 2.0** structured properly.  


## Detailed Documentation for Mottrist collection or APIs !

## Getting Started

To begin using the Mottrist API in Postman:

1. **Import the Collection:** Import this collection into Postman to access all Mottrist API endpoints.
2. **Select Environment:** Choose the appropriate environment (Development or Production) from the Postman Environments sidebar.
3. **Authenticate:**

   * Navigate to the Getting Started folder and use the `POST {{baseUrl}}/api/users/auth/login` request.
   * Provide credentials `{ "email": "<your-email>", "password": "<your-password>" }`.
   * Copy the returned JWT token and set it as the `{{token}}` environment variable.
4. **Set Authorization:** All requests requiring authentication use Bearer authentication. Ensure the Authorization header is set to `Bearer {{token}}` in Postman.

### Environment Setup

Create two environments in Postman (Development and Production) with the following variables:

| Variable | Initial Value                                                | Current Value           | Description                     |
| -------- | ------------------------------------------------------------ | ----------------------- | ------------------------------- |
| baseUrl  | [https://dev.api.mottrist.com](https://dev.api.mottrist.com) | (leave blank or modify) | Base URL for API calls          |
| token    | (empty)                                                      | (JWT from login)        | Bearer token for authentication |

---

## Folder Structure

* **Getting Started:** Authentication endpoints for login and logout.
* **Users:** Registration and email confirmation.
* **Drivers:** Driver profile management, filters, and interactions.
* **Travelers:** Traveler profile management.
* **Destinations:** Destination data operations.
* **Messages:** Messaging operations.
* **Car Brands:** Car brand CRUD.
* **Car Fields:** Car metadata (body types, colors, fuel types).
* **Countries:** Country data retrieval.
* **Cities:** City data retrieval.
* **Languages:** Language data retrieval.

---

## Usage Guidelines

* **Base URL:** Replace `{{baseUrl}}` with the environment-specific URL.
* **Authentication:** Obtain a JWT token via the login endpoint and include it in the `Authorization` header.
* **Folder Navigation:** Each folder contains detailed documentation with endpoint tables and controller overviews.
* **Error Handling:** Handle HTTP status codes and error messages appropriately.

---

## Changelog

* **2025-05-04:** Initial release of the Mottrist API Postman collection, covering all core functionalities.

---

## Detailed Folder Documentation

### Getting Started

**Overview:** Authentication-related endpoints for login and logout. The login endpoint returns a JWT token; the logout endpoint invalidates the session.

| Method | Endpoint                            | Description                                |
| ------ | ----------------------------------- | ------------------------------------------ |
| POST   | `{{baseUrl}}/api/users/auth/login`  | Logs in a user with email and password.    |
| POST   | `{{baseUrl}}/api/users/auth/logout` | Logs out the currently authenticated user. |

### Users

**Overview:** User registration and email confirmation endpoints.

| Method | Endpoint                                   | Description                                 |
| ------ | ------------------------------------------ | ------------------------------------------- |
| POST   | `{{baseUrl}}/api/users/register`           | Registers a new user.                       |
| POST   | `{{baseUrl}}/api/users/send-confirm-email` | Sends a confirmation email.                 |
| POST   | `{{baseUrl}}/api/users/confirm-email`      | Confirms a user’s email using ID and token. |

### Drivers

**Overview:** Manage drivers with filtering, pagination, and interactions.

| Method | Endpoint                                                                          | Description                                             |
| ------ | --------------------------------------------------------------------------------- | ------------------------------------------------------- |
| GET    | `{{baseUrl}}/api/drivers/{id:int}`                                                | Retrieves a driver by ID.                               |
| GET    | `{{baseUrl}}/api/drivers/all`                                                     | Retrieves all drivers.                                  |
| GET    | `{{baseUrl}}/api/drivers/all/paged`                                               | Retrieves paginated list of drivers.                    |
| GET    | `{{baseUrl}}/api/drivers/top-rated`                                               | Retrieves top-rated drivers.                            |
| GET    | `{{baseUrl}}/api/drivers/by-country/{countryId:int}`                              | Retrieves drivers by country.                           |
| GET    | `{{baseUrl}}/api/drivers/by-country/{countryId:int}/city/{cityId:int}`            | Retrieves drivers by country and city.                  |
| GET    | `{{baseUrl}}/api/drivers/by-country/{countryId:int}/city/{cityId:int}/date`       | Retrieves drivers by country, city, and date.           |
| GET    | `{{baseUrl}}/api/drivers/paged/by-country/{countryId:int}`                        | Retrieves paginated drivers by country.                 |
| GET    | `{{baseUrl}}/api/drivers/paged/by-country/{countryId:int}/city/{cityId:int}`      | Retrieves paginated drivers by country and city.        |
| GET    | `{{baseUrl}}/api/drivers/paged/by-country/{countryId:int}/city/{cityId:int}/date` | Retrieves paginated drivers by country, city, and date. |
| GET    | `{{baseUrl}}/api/drivers/paged/by-status`                                         | Retrieves paginated drivers by status.                  |
| GET    | `{{baseUrl}}/api/drivers/by-status`                                               | Retrieves drivers by status.                            |
| POST   | `{{baseUrl}}/api/drivers`                                                         | Adds a new driver.                                      |
| PUT    | `{{baseUrl}}/api/drivers/{id:int}`                                                | Updates an existing driver.                             |
| PATCH  | `{{baseUrl}}/api/drivers/update-status/{driverId:int}`                            | Updates driver status.                                  |
| PATCH  | `{{baseUrl}}/api/drivers/update-availability/{driverId:int}`                      | Updates driver availability.                            |
| PATCH  | `{{baseUrl}}/api/drivers/update-price/{driverId:int}`                             | Updates driver hourly price.                            |
| POST   | `{{baseUrl}}/api/drivers/like-dislike/{driverId:int}`                             | Records a like/dislike for a driver by a user.          |
| POST   | `{{baseUrl}}/api/drivers/increment-view/{driverId:int}`                           | Records a unique view for a driver.                     |
| DELETE | `{{baseUrl}}/api/drivers/{id:int}`                                                | Deletes a driver by ID.                                 |
| GET    | `{{baseUrl}}/api/drivers/all-driver-form-fields`                                  | Retrieves form fields for driver registration.          |

### Travelers

**Overview:** Manage traveler profiles with CRUD operations and pagination.

| Method | Endpoint                              | Description                    |
| ------ | ------------------------------------- | ------------------------------ |
| GET    | `{{baseUrl}}/api/travelers/{id:int}`  | Retrieves a traveler by ID.    |
| GET    | `{{baseUrl}}/api/travelers/all`       | Retrieves all travelers.       |
| GET    | `{{baseUrl}}/api/travelers/all/paged` | Retrieves paginated travelers. |
| POST   | `{{baseUrl}}/api/travelers`           | Creates a new traveler.        |
| PUT    | `{{baseUrl}}/api/travelers/{id:int}`  | Updates an existing traveler.  |
| DELETE | `{{baseUrl}}/api/travelers/{id:int}`  | Deletes a traveler by ID.      |

### Destinations

**Overview:** CRUD operations for destinations with public and secured endpoints.

| Method | Endpoint                                 | Description                       |
| ------ | ---------------------------------------- | --------------------------------- |
| GET    | `{{baseUrl}}/api/destinations/{id:int}`  | Retrieves a destination by ID.    |
| GET    | `{{baseUrl}}/api/destinations/all`       | Retrieves all destinations.       |
| GET    | `{{baseUrl}}/api/destinations/all/paged` | Retrieves paginated destinations. |
| POST   | `{{baseUrl}}/api/destinations`           | Adds a new destination.           |
| PUT    | `{{baseUrl}}/api/destinations/{id:int}`  | Updates an existing destination.  |
| DELETE | `{{baseUrl}}/api/destinations/{id:int}`  | Deletes a destination by ID.      |

### Messages

**Overview:** CRUD operations for messages within the Mottrist platform.

| Method | Endpoint                             | Description                   |
| ------ | ------------------------------------ | ----------------------------- |
| GET    | `{{baseUrl}}/api/messages/{id:int}`  | Retrieves a message by ID.    |
| GET    | `{{baseUrl}}/api/messages/all`       | Retrieves all messages.       |
| GET    | `{{baseUrl}}/api/messages/all/paged` | Retrieves paginated messages. |
| POST   | `{{baseUrl}}/api/messages`           | Adds a new message.           |
| PUT    | `{{baseUrl}}/api/messages/{id:int}`  | Updates an existing message.  |
| DELETE | `{{baseUrl}}/api/messages/{id:int}`  | Deletes a message by ID.      |

### Car Brands

**Overview:** Manage car brands with CRUD operations and pagination.

| Method | Endpoint                           | Description                     |
| ------ | ---------------------------------- | ------------------------------- |
| GET    | `{{baseUrl}}/api/Brands/{id:int}`  | Retrieves a car brand by ID.    |
| GET    | `{{baseUrl}}/api/Brands/all`       | Retrieves all car brands.       |
| GET    | `{{baseUrl}}/api/Brands/all/paged` | Retrieves paginated car brands. |
| POST   | `{{baseUrl}}/api/Brands`           | Adds a new car brand.           |
| PUT    | `{{baseUrl}}/api/Brands/{id:int}`  | Updates an existing car brand.  |
| DELETE | `{{baseUrl}}/api/Brands/{id:int}`  | Deletes a car brand by ID.      |

### Car Fields

**Overview:** Retrieve car-related metadata (body types, colors, fuel types).

| Method | Endpoint                                  | Description                   |
| ------ | ----------------------------------------- | ----------------------------- |
| GET    | `{{baseUrl}}/api/CarFields/All/BodyTypes` | Retrieves all car body types. |
| GET    | `{{baseUrl}}/api/CarFields/All/Colors`    | Retrieves all car colors.     |
| GET    | `{{baseUrl}}/api/CarFields/All/FuelTypes` | Retrieves all car fuel types. |
| GET    | `{{baseUrl}}/api/CarFields/All/CarFields` | Retrieves all car field data. |

### Countries

**Overview:** Retrieve country data for forms and dropdowns.

| Method | Endpoint                             | Description                |
| ------ | ------------------------------------ | -------------------------- |
| GET    | `{{baseUrl}}/api/countries/{id:int}` | Retrieves a country by ID. |
| GET    | `{{baseUrl}}/api/countries/All`      | Retrieves all countries.   |

### Cities

**Overview:** Retrieve city data, with optional country filtering.

| Method | Endpoint                                  | Description                             |
| ------ | ----------------------------------------- | --------------------------------------- |
| GET    | `{{baseUrl}}/api/Cities/{id:int}`         | Retrieves a city by ID.                 |
| GET    | `{{baseUrl}}/api/Cities/All`              | Retrieves all cities.                   |
| GET    | `{{baseUrl}}/api/Cities/country/{id:int}` | Retrieves cities by country ID.         |
| GET    | `{{baseUrl}}/api/Cities/WithCountry`      | Retrieves all cities with country info. |

### Languages

**Overview:** Retrieve language data for selection interfaces.

| Method | Endpoint                             | Description                 |
| ------ | ------------------------------------ | --------------------------- |
| GET    | `{{baseUrl}}/api/Languages/{id:int}` | Retrieves a language by ID. |
| GET    | `{{baseUrl}}/api/Languages/All`      | Retrieves all languages.    |

---

For additional details, refer to the individual folder documentation within the Postman collection. If you encounter issues or have questions, contact the Mottrist development team for assistance.

Happy exploring!

