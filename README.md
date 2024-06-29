# Inventory Management System

## Table of Contents
- [Description](#description)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [License](#license)
- [Contributing](#contributing)
- [Contact](#contact)

## Description
The Inventory Management System is a .NET application designed to help businesses manage their inventory efficiently. It allows users to track products, manage stock levels, and generate reports on inventory data.

## Features
- **Product Management**: Add, update, and delete products.
- **Stock Tracking**: Monitor stock levels and receive notifications for low stock.
- **Reports**: Generate and export inventory reports.
- **User Authentication**: Secure login and user management.

## Installation
To set up the project locally, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/Aravindha-samy/InventoryManagementSystem-BackEnd.git
   cd InventoryManagementSystem-BackEnd
Open the project in Visual Studio:
Open Visual Studio and select Open a project or solution. Navigate to the cloned directory and open the solution file (.sln).

Restore dependencies:
In Visual Studio, go to Tools > NuGet Package Manager > Manage NuGet Packages for Solution... and restore the required packages.

Set up the database:
Configure your database connection in the appsettings.json file. Make sure to update the connection string with your database details.

Update the database:
Open the Package Manager Console in Visual Studio and run the following command to apply migrations:

powershell
Copy code
Update-Database
Run the application:
Press F5 or click on the Start button to run the application. The application will be available at http://localhost:5000.

Usage
Register/Login: Create an account or log in if you already have one.
Add Products: Navigate to the product management section to add new products.
Generate Reports: Go to the reports section to create and download inventory reports.
License
This project is licensed under the MIT License. See the LICENSE file for details.

Contributing
Contributions are welcome! Please fork the repository and create a pull request with your changes. Make sure to update tests as appropriate.

Fork the Project
Create your Feature Branch (git checkout -b feature/AmazingFeature)
Commit your Changes (git commit -m 'Add some AmazingFeature')
Push to the Branch (git push origin feature/AmazingFeature)
Open a Pull Request
Contact
For any questions or inquiries, please contact:
Aravindha Samy
GitHub: Aravindha-samy
