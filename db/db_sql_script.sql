
CREATE DATABASE EmployeeManagementDB;

USE EmployeeManagementDB;

CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);

CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(10) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    MiddleName NVARCHAR(100),
    LastName NVARCHAR(100) NOT NULL,
    NICNumber NVARCHAR(15) NOT NULL,
    EPFNumber NVARCHAR(10),
    ETFNumber NVARCHAR(10),
    DateOfBirth DATE,
    Gender NVARCHAR(10) NOT NULL,
    ActiveStatus BIT NOT NULL DEFAULT (1),
    DepartmentId INT NOT NULL,

    CONSTRAINT FK_Department FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

CREATE TABLE Salaries (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EmployeeId INT UNIQUE NOT NULL,
    BasicSalary DECIMAL(18,2) DEFAULT(0.00) NOT NULL,

    CONSTRAINT FK_Employee FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE
);

