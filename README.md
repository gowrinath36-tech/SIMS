# SIMS - Student Information Management System

## 🚀 Project Overview
SIMS is a robust web-based management solution built with **ASP.NET Core MVC**. It features a custom security architecture, including role-based authorization and secure credential handling, designed to manage student data and generate administrative reports.

## 🛠 Tech Stack
* **Backend:** C# (.NET)
* **Architecture:** Model-View-Controller (MVC)
* **Security:** Custom Role-Based Authorization & Password Hashing
* **Frontend:** Razor Pages (CSHTML), Bootstrap, JavaScript
* **Data Handling:** JSON Configuration & SQL Server

## ✨ Key Features & Implementation
* **Role-Based Security:** Implemented a custom `RoleAuthorizeAttribute` to restrict access based on user permissions.
* **Secure Authentication:** Integrated a `PasswordHelper` module for safe credential management.
* **Reporting System:** A dedicated `ReporterController` to handle data visualization and student archives.
* **Session Management:** Uses secure session state handling for user tracking.

## 📂 Project Structure
* `Controllers/`: Logic for handling requests (Reporter, Password, etc.)
* `Attributes/`: Custom decorators for security and validation.
* `Views/`: UI components including `Archives.cshtml`.
* `appsettings.json`: Configuration management for the environment.

## 👨‍💻 How to Collaborate
1. **Clone:** `git clone https://github.com/gowrinath36-tech/SIMS.git`
2. **Open:** Launch the `.sln` file in Visual Studio.
3. **Branch:** Create a new branch for your feature: `git checkout -b feature-name`
4. **Push:** Commit your changes and push to the new repository.
