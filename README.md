# File Processing Web API

## Overview
This project implements a secure RESTful web service for processing CSV and JSON files. The service allows users to upload files, perform data operations, and retrieve processing reports. The API is built using ASP.NET Core and follows best practices for security and code quality.

## Quick Start

### 1. Clone the Repository
`git clone https://github.com/vincentescarcha/ESCARCHA_10152024.git`

### 2. Build the Application
Make sure you have the .NET 8 SDK installed. Run the following commands:

`dotnet restore`

`dotnet build`

`dotnet run`

or open the solution file in Visual Studio and start

### 3. Access the API with Swagger UI
This API uses Swagger for interactive documentation and testing. Swagger UI is automatically enabled, allowing you to explore the API and make sample requests.

To access Swagger:

- Open your browser and go to `http://localhost:<port>/swagger`.
- You will find interactive documentation for the API endpoints.


#### **Authorize Button for API Key**: 
I implemented an **Authorize** button in the Swagger UI, allowing users to enter their API key once and automatically apply it to all API requests. This streamlines the process of authenticating requests while testing endpoints directly through the interactive UI.


#### How to Use the Swagger **Authorize** Button:
1. Open `http://localhost:<port>/swagger`.
2. Generate your API key by sending a request to the `POST /api/auth/generate-api-key` endpoint.
3. Click on the **Authorize** button at the top-right corner of the Swagger UI.
4. Enter your API key in the pop-up modal under `x-api-key`.
5. Click **Authorize**. Your API key will be applied to all API requests made through the Swagger UI.
 



## Web API Endpoints

### 1. Generate API Key
- **Endpoint**: `/api/auth/generate-api-key`
- **Method**: `POST`
- **Description**: Generates a new API key for accessing the service.
- **Responses**:
  - **200 OK**: Returns the generated API key in the response body.
  - **500 Internal Server Error**: For unexpected errors.

### 2. Process File
- **Endpoint**: `/api/files/process`
- **Method**: `POST`
- **Description**: Uploads a CSV or JSON file for processing.
- **Parameters**:
  - `file` : The file to be processed. Supported formats are CSV and JSON.
  - `query` (optional): A query string for operations.
    - For CSV: `aggregate=<rowcol_number>` to calculate the average of a specific column.
    - For JSON: Allows dynamic filtering based on key-value pairs.

- **Responses**:
  - **200 OK**: Returns a JSON object containing:
    - `result`: The processed data.
    - `errors`: A list of any errors encountered during processing.
  - **400 Bad Request**: If the file is missing or invalid.
  - **500 Internal Server Error**: For unexpected errors.

### 3. Report Processed Files
- **Endpoint**: `/api/files/report`
- **Method**: `GET`
- **Description**: Retrieves information about previously processed files.
- **Responses**:
  - **200 OK**: Returns a list of processed files, each represented by a JSON object containing:
    - `name`: The filename.
    - `size`: The size of the file (formatted in KB or MB).
    - `processingTime`: The duration taken to process the file.
  - **500 Internal Server Error**: For unexpected errors.

