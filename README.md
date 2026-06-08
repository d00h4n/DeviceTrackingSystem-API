# Company Device Tracking System - API & Dashboard

**🟢 Live Demo:** [https://devicetrackingsystem-api.onrender.com/](https://devicetrackingsystem-api.onrender.com/)

A Simple light weight web application built for tracking and managing company software/tools and hardware devices. Developed using C# .NET Core Minimal APIs and a vanilla HTML/JS frontend with Bootstrap 5.

## 🚀 Features
* **Device Management (CRUD):** Add, Edit, Update Status, and Delete devices.
* **Real-Time Dashboard Metrics:** Automatically calculates Total Devices, Assigned, Unassigned, and In Repair devices.
* **History/Logs Tracking:** Automatically tracks and logs device status changes to a relational database when updated.
* **Search & Filter:** Instantly filter devices by name, assigned employee, or status.
* **Export to CSV:** Download the current device table directly to local storage.
* **Responsive UI:** Clean and mobile-friendly interface powered by Bootstrap 5.

## 🛠️ Tech Stack
* **Backend:** C# .NET 10.0, ASP.NET Core Minimal APIs
* **Database:** SQLite & Entity Framework Core (Code-First Approach)
* **Frontend:** HTML5, CSS3, Vanilla JavaScript, Bootstrap 5
* **Deployment:** Docker, Render (Hosting)

## 📂 Database Architecture (SQLite)
The system utilizes a relational database design with two primary tables:
1. **Devices:** Stores core device information (`Id`, `DeviceName`, `DeviceType`, `AssignedEmployee`, `Status`, `LastCheckedDate`, `Notes`).
2. **DeviceLogs:** Stores historical records of status/assignment changes (One-to-Many relationship with `Devices`).

## 💻 How to Run Locally

1. Clone this repository:
   ```bash
   git clone [https://github.com/d00h4n/DeviceTrackingSystem-API.git](https://github.com/d00h4n/DeviceTrackingSystem-API.git)
   
2. Navigate project directory

   ```bash
   cd DeviceTrackingSystem-API

   
3. Apply Entity Framework migrations to create the SQLite database:

   ```bash
   dotnet ef database update

4. Run app

   ```bash
   dotnet run

   

