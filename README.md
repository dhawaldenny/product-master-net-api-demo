# Product Master

## Description
This Web API project is used to Fetch, Add, Update the Products from the Database.  

## Prerequisites
- .NET 6.0 SDK
- Visual Studio
- SQL Server 2019
  
## Installation
1. Clone the repository.
2. Open the solution file (`ProductMaster.sln`) in Visual Studio.
3. Build the solution to restore packages.

## Getting Started
1. Run the application.
2. Access the API endpoints using tools like Postman or Swagger UI.

## Configuration
- Added ProductMaster.bak file in the Database folder.
- Restore the database in SQL Server 2019.
- Set the Connection String in appsettings.json file under "DefaultConnection" property.

## APIs
- Below are the list of APIs
- GET - /api/products/getallproducts
- GET - /api/products/productid/{productId}
- POST - /api/products/addproduct
- PUT - /api/products/updateproduct
- POST - /api/products/updateproductsprice

