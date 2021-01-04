# BloodDonationApp

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

My Asp.Net Core app for the course of C# Web - ASP.NET Core - October 2020 [SoftUni](https://softuni.bg/).

https://blooddon.azurewebsites.net

## :point_right: Project Introduction

**BloodDonationApp** is a web application developed through ASP.NET Core 3.1 (further updated to ASP.NET 5) and it is using SQL Server for storing the data. 
 - **The goal** is to be provided easy and fast way for connection between hospitals (regarding their demand for blood and blood plasma) and the potential donors.The application consists of three types of users - donors, hospitals and administrators. 
 - **Each user** can register (and choose role between donor or hospital), login and logout and contact administration via sending message.
 - **Hospitals** can register recipients and to send request for blood donation. 
 - **Donors** can choose the respective request and to donate blood. Donors can donate directly (without request published by hospital) to selected by them hospital. By clicking donate button, for the perpouse of the presentation the reserve of the blood bank (for the respective blood group) of the respective hospital is increasing. 
 - **Admins** can add adminis and delete donors and hospitals users.

## ⚙️ Test accounts:

  - Admin => Username: Admin1@admin1.bg / password: Admin1@admin1.bg

  - Hospital => Username: Hospital1@hospital.bg / password: Hospital1@hospital.bg

  - Donor => Username: Donor1@donor.bg / password: Donor1@donor.bg	


## :hammer_and_pick: Built With:
- ASP.NET Core
    - ASP.NET Identity System
    - MVC Areas with Multiple Layouts
    - Razor View Engine, Sections, Partial Views
    - View Components
    - Repository Pattern
    - Auto Мapping
    - Dependency Injection
    - Exception Handling
    - Data Validation, both Client-side and Server-side
    - Data Validation in the Models and Input View Models
    - Custom Validation Attributes
    - Pagination
- Entity Framework Core
- MSSQL Server
- JavaScript
- Bootstrap
- HTML 5
- CSS
- Moq
- NUnit

## Screenshots:

### Home Page
![HomePage](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/HomePage.bmp)

### Admin Panel
![AdminPanel](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/AdminPanel.bmp)

### Donation Requests
![DonationRequests](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/DonationRequests.bmp)

### BloodBank Reservs
![BloodBankReservs](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/BloodBankReservs.bmp)

### Error Handling
![ErrorHandling](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/ErrorHandling.bmp)

## :floppy_disk: Database Diagram
![](https://github.com/nixford/BloodDonationApp/blob/master/src/Web/BloodDonationApp.Web/wwwroot/ImagesReadme/BDA-Diagram.png)

