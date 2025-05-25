# RESTful API Integration with Mock API

## Overview
This project is a simple RESTful Web API built with **C# and .NET 8.0**, integrating with the mock API at [https://restful-api.dev](https://restful-api.dev). It enhances the mock API with:
- **Filtering & Pagination** when retrieving data
- **Adding new products**
- **Deleting products**
- **Proper validation and error handling**

## Repository URL
GitHub Repository: [https://github.com/vinil2001/restful-api-integration](https://github.com/vinil2001/restful-api-integration)

## Setup Instructions
### Prerequisites
Ensure you have:
- **.NET 8 SDK** ([Download here](https://dotnet.microsoft.com/en-us/download))
- **Visual Studio 2022** or **VS Code** (Optional)
- **Postman** (Optional for API testing)
- **Git** for version control

### Installation
1. Clone the repository:
git clone https://github.com/vinil2001/restful-api-integration.git

2. Navigate to the project folder:
cd restful-api-integration

3. Restore dependencies:
dotnet restore

4. Run the API:
dotnet run


## API Documentation
### Base URL
http://localhost:{PORT}/api/products

Replace `{PORT}` with the actual port assigned when running the API.

### Endpoints
#### ✅ Retrieve Products
GET /api/products?name={substring}&page={page}&size={size}

- **name** (optional): Filters products by substring match.
- **page** (default: 1): Retrieves paginated data.
- **size** (default: 10): Number of items per page.

#### ✅ Add a Product
POST /api/products

Request Body:
```json
{
    "name": "Smartwatch Pro",
    "description": "Advanced health-tracking smartwatch"
}
✅ Delete a Product
DELETE /api/products/{id}
Error Handling
The API returns proper responses for failures:

400 Bad Request → Validation errors

404 Not Found → When the requested product doesn't exist

500 Internal Server Error → Unexpected failures

Contributing
Feel free to fork this repo and submit pull requests!