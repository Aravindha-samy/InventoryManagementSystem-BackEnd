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


### Clone the repository
1. Open the **Package Manager Console** in Visual Studio.
    ```powershell
    git clone https://github.com/Aravindha-samy/InventoryManagementSystem-BackEnd.git
    cd InventoryManagementSystem-BackEnd
    ```


### Open the Project in Visual Studio
1. Open Visual Studio and select **Open a project or solution**.
2. Navigate to the cloned directory and open the solution file (`.sln`).

### Restore Dependencies
1. In Visual Studio, go to **Tools > NuGet Package Manager > Manage NuGet Packages for Solution...**.
2. Restore the required packages.

### Set Up the Database
1. Configure your database connection in the `appsettings.json` file.
2. Update the connection string with your database details.

### Update the Database
1. Open the **Package Manager Console** in Visual Studio.
2. Run the following command to apply migrations:
    ```powershell
    Update-Database
    ```

### Run the Application
1. Press `F5` or click on the **Start** button to run the application.
2. The application will be available at `http://localhost:44354`.

## Usage

- **Register/Login:** Create an account or log in if you already have one.
- **Add Products:** Navigate to the product management section to add new products.
- **Track Inventory:** Monitor the stock levels from the dashboard.
- **Generate Reports:** Go to the reports section to create and download inventory reports.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. Make sure to update tests as appropriate.

1. Fork the Project.
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`).
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the Branch (`git push origin feature/AmazingFeature`).
5. Open a Pull Request.

## Contact

For any questions or inquiries, please contact:

**Aravindha Samy**

- GitHub: [Aravindha-samy](https://github.com/Aravindha-samy)
