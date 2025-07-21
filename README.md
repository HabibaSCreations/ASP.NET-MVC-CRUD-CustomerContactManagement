
# ASP.NET-MVC-CRUD-CustomerContactManagement

A clean ASP.NET MVC 5 CRUD web application to manage customer information with create, read, update, and delete operations.

---


## 📖 **Project Overview**
This project is a **Customer Management System** built using **ASP.NET MVC 5** and **Entity Framework 6**.  
The application allows managing **customer details, customer types, and multiple addresses** with image upload functionality.

---

## ✨ **Key Features**
- **Customer Management:** Add, view, edit, and delete customers.
- **Multiple Addresses:** Manage multiple addresses for each customer.
- **Image Upload:** Upload and update profile pictures.
- **Customer Types:** Assign and manage customer types.
- **Validation:** Strong client-side and server-side validation using Data Annotations.
- **AJAX Support:** Create and edit customer records using AJAX for better UX.
- **Responsive UI:** Bootstrap-based layout for mobile-friendly views.

---

## 🛠 **Technology Stack**
- **ASP.NET MVC 5**
- **Entity Framework 6**
- **SQL Server**
- **C#**
- **Razor Views**
- **Bootstrap**
- **jQuery & AJAX**

---

## 🚀 **How to Download & Run the Project in Visual Studio**

### **Step 1: Clone or Download**
- **Clone using Git:**
  ```bash
  git clone https://github.com/HabibaSCreations/ASP.NET-MVC-CRUD-CustomerContactManagement.git
  ```
- **OR Download as ZIP:**  
  Click **Code > Download ZIP** and extract it.

### **Step 2: Open in Visual Studio**
- Open **Visual Studio (2019/2022)**.
- Go to **File > Open > Project/Solution**.
- Select **ASP.NET-MVC-CRUD-CustomerContactManagement.sln** file.

### **Step 3: Restore NuGet Packages**
- Right-click the **Solution** in Solution Explorer.
- Select **"Restore NuGet Packages"**.

### **Step 4: Setup Database**
- Open SQL Server (or SQL Server Express).
- Create a new database (e.g., `CustomerDB`).
- Update the `Web.config` file connection string:
  ```xml
  <connectionStrings>
    <add name="con" connectionString="Data Source=.;Initial Catalog=CustomerDB;Integrated Security=True" providerName="System.Data.SqlClient" />
  </connectionStrings>
  ```
- When you run the project for the first time, Entity Framework will create tables automatically (if Code-First Migration is enabled).

### **Step 5: Build & Run**
- **Build** the project: `Ctrl + Shift + B`.
- **Run** the project: `F5` or **Start Debugging**.
- The project will open in your default browser (e.g., `http://localhost:xxxx/Customers/Index`).

---

## 📂 **Project Structure**
```
ASP.NET-MVC-CRUD-CustomerContactManagement/
│
├── Controllers/
│   └── CustomersController.cs
│
├── Models/
│   ├── Customer.cs
│   ├── Address.cs
│   ├── CustomerType.cs
│   └── con.cs
│
├── ViewModels/
│   └── CustomerViewModel.cs
│
├── Views/
│   ├── Customers/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   └── Edit.cshtml
│
├── Scripts/ (jQuery, Bootstrap)
├── Content/ (CSS, Images)
└── Web.config
```

---

## 📌 **Future Improvements**
- User Authentication & Role-based Access Control.
- Export customer data to Excel/PDF.
- Advanced search and filtering.
- REST API for mobile or third-party apps.

---

## 💡 **How to Contribute**
1. Fork this repository.
2. Create a new branch: `git checkout -b feature-branch`
3. Commit your changes: `git commit -m 'Add new feature'`
4. Push to the branch: `git push origin feature-branch`
5. Create a Pull Request.

---


## 👩‍💻 **Project Owner**
**GitHub:** [HabibaSCreations](https://github.com/HabibaSCreations)

---

## 🏷 **License**
This project is open-source and available under the **MIT License**.
