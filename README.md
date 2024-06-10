# E-commerce Project Documentation

## Table of Contents

1. [Introduction](#introduction)
2. [Technologies Used](#technologies-used)
3. [Project Structure](#project-structure)
4. [API Documentation](#api-documentation)
   - [Account Controller](#account-controller)
   - [Cart Controller](#cart-controller)
   - [Category Controller](#category-controller)
   - [Upload Controller](#upload-controller)
   - [Order Controller](#order-controller)
   - [Product Controller](#product-controller)
   - [Review Controller](#review-controller)
   - [SubCategory Controller](#subcategory-controller)
   - [Wishlist Controller](#wishlist-controller)
5. [Authentication & Authorization](#authentication--authorization)
6. [Database Schema](#database-schema)
7. [Deployment](#deployment)
8. [Contributing](#contributing)
9. [License](#license)

## Introduction

This project is a comprehensive e-commerce platform built with ASP.NET Core Web APIs, Angular for the frontend, MySQL as the database, and integrates various third-party services like Stripe for payments, Azure for deployment, and Google Cloud Storage for file uploads.

## Technologies Used

- **Backend:** ASP.NET Core, Dapper
- **Frontend:** Angular
- **Database:** MySQL
- **Payment Processing:** Stripe
- **Cloud Storage:** Google Cloud Storage
- **Deployment:** Azure
- **API Documentation:** Swagger

## Project Structure

```plaintext
EcommerceBackend/
├── Controllers/
│   ├── AccountController.cs
│   ├── CartController.cs
│   ├── CategoryController.cs
│   ├── UploadController.cs
│   ├── OrderController.cs
│   ├── ProductController.cs
│   ├── ReviewController.cs
│   ├── SubCategoryController.cs
│   └── WishlistController.cs
├── Models/
│   ├── DBModels/
│   ├── Request/
├── Services/
│   ├── Interfaces/
│   └──Business Logic Implementations/
├── Repository/
│   ├── Interfaces/
│   └── Databae Query Implementations/
├── Helper/
├── Enums/
├── CustomMiddleware/
         ├── ExceptionHandlingMiddleware
├── Program.cs
├── DbContext.cs
└── appsettings.json
```

## API Documentation

### Account Controller

Handles user registration, login, user details retrieval and update, password management, and purchase processing.

- **Route:** `/Account`
  
#### Endpoints:

- `POST /register` - Register a new user
- `POST /login` - User login
- `GET /` - Get user details (requires authorization)
- `PUT /update` - Update user details (requires authorization)
- `POST /forgot-password` - Forgot password
- `GET /purchase/{uuid}` - Purchase items in the cart (requires authorization)
- `POST /webhook` - Stripe webhook to handle payment events
- `PUT /update-password` - Update password

### Cart Controller

Handles operations related to the shopping cart.

- **Route:** `/Cart`
  
#### Endpoints:

- `POST /add` - Add item to cart (requires authorization)
- `PUT /` - Update cart (requires authorization)
- `DELETE /{productId:int}` - Delete cart item (requires authorization)
- `DELETE /deletecart` - Delete entire cart (requires authorization)

### Category Controller

Manages product categories.

- **Route:** `/Category`
  
#### Endpoints:

- `POST /add` - Create a new category
- `GET /` - Get all categories
- `GET /data` - Get categories with subcategories
- `DELETE /{id:int}` - Delete a category

### Upload Controller

Handles image uploads to Google Cloud Storage.

- **Route:** `/Upload`
  
#### Endpoints:

- `POST /` - Upload an image

### Order Controller

Handles user orders.

- **Route:** `/Order`
  
#### Endpoints:

- `GET /all` - Get all orders for the authenticated user
- `GET /transactionId/{id}` - Get order details by transaction ID

### Product Controller

Manages products.

- **Route:** `/Product`
  
#### Endpoints:

- `POST /add` - Create a new product
- `GET /` - Get all products
- `GET /subcategory/{id}` - Get products by subcategory ID
- `GET /{id}` - Get product by ID
- `DELETE /{id}` - Delete a product

### Review Controller

Handles product reviews.

- **Route:** `/Review`
  
#### Endpoints:

- `POST /add` - Add a review (requires authorization)
- `GET /product/{id}` - Get reviews for a product
- `GET /User/product/{id}` - Get user's review for a product (requires authorization)
- `PUT /update` - Update a review (requires authorization)
- `DELETE /product/{productId:int}` - Delete a review (requires authorization)

### SubCategory Controller

Manages product subcategories.

- **Route:** `/SubCategory`
  
#### Endpoints:

- `POST /add` - Create a new subcategory
- `GET /` - Get all subcategories
- `GET /parentCategory/{id}` - Get subcategories by parent category ID
- `DELETE /{id:int}` - Delete a subcategory

### Wishlist Controller

Handles wishlist operations.

- **Route:** `/Wishlist`
  
#### Endpoints:

- `POST /add` - Add item to wishlist (requires authorization)
- `DELETE /{productId:int}` - Remove item from wishlist (requires authorization)

## Authentication & Authorization

Authentication and authorization are implemented using JWT tokens. Protected routes require a valid JWT token in the `Authorization` header. The token includes user claims that are used to identify and authorize users for specific actions.

## Database Schema

The database schema includes tables for users, products, categories, subcategories, orders, cart items, and reviews. Here is a high-level overview:

- **Users:** Stores user information
- **Products:** Stores product details
- **Categories:** Stores product categories
- **SubCategories:** Stores subcategories linked to categories
- **Orders:** Stores order information
- **CartItems:** Stores items added to the cart by users
- **Reviews:** Stores user reviews for products

## Deployment

The project is deployed on Azure. Below are the deployment steps:

1. **Build the project:** 
    ```sh
    dotnet build
    ```

2. **Publish the project:**
    ```sh
    dotnet publish --configuration Release
    ```

3. **Deploy to Azure:**
    - Use Azure Portal or Azure CLI to create an App Service.
    - Deploy the published files to the App Service.

4. **Configure environment variables:**
    - Set the required environment variables like connection strings, API keys, etc., in the Azure App Service configuration settings.
