Create database ShopHW;

use ShopHW;

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100),
    ProductType NVARCHAR(50),
    SupplierID INT FOREIGN KEY REFERENCES Suppliers(SupplierID),
    Quantity INT,
    Cost DECIMAL(10, 2),
    SupplyDate DATE
);

CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(100),
    SupplierLocation NVARCHAR(100)
);


INSERT INTO Suppliers (SupplierName, SupplierLocation) VALUES
('Supplier A', 'Location A'),
('Supplier B', 'Location B'),
('Supplier C', 'Location C');


INSERT INTO Products (ProductName, ProductType, SupplierID, Quantity, Cost, SupplyDate) VALUES
('Product 1', 'Type 1', 1, 100, 10.99, '2024-02-17'),
('Product 2', 'Type 2', 2, 200, 20.49, '2024-02-16'),
('Product 3', 'Type 1', 3, 150, 15.75, '2024-02-15');




