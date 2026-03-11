# HR Leave Management System

## Project Description

The HR Leave Management System is a web application developed using ASP.NET Core.
This system allows employees to apply for leave and managers to review, approve, or reject leave requests.
It helps organizations manage employee leave records efficiently.

## Technologies Used

* ASP.NET Core Web API
* ASP.NET Core MVC
* Dapper ORM
* SQL Server
* Stored Procedures
* JWT Authentication
* GitHub

## Features

* Employee Management
* Leave Request Submission
* Leave Approval / Rejection by Manager
* Leave Reports
* Secure API using JWT Authentication

## Database Setup

1. Open SQL Server.
2. Create a new database.
3. Run the SQL script available in `/db/schema.sql` to create the required tables and stored procedures.

## Configuration

Update the connection string in **appsettings.json**.

Example:
"ConnectionStrings": {
"DefaultConnection": "Your_Database_Connection_String"
}

If any API Gateway or external key is required, add it in the configuration file.

## How to Run the Project

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Restore NuGet packages.
4. Update the database connection string.
5. Run the application.

## Repository

The complete source code is available in this repository.
# HRLeaveManagement
