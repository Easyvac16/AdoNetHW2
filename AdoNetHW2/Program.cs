using System.Data.SqlClient;
using System.Text;

namespace AdoNetHW2
{
    internal class Program
    {
        public static string ConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopHW;Integrated Security=True;Connect Timeout=30;";
        static void Main()
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;

            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Відобразити всю інформацію про товар.");
                Console.WriteLine("2. Відобразити всі типи товарів.");
                Console.WriteLine("3. Відобразити всіх постачальників.");
                Console.WriteLine("4. Показати товар з максимальною кількістю.");
                Console.WriteLine("5. Показати товар з мінімальною кількістю.");
                Console.WriteLine("6. Показати товар з мінімальною собівартістю.");
                Console.WriteLine("7. Показати товар з максимальною собівартістю.");
                Console.WriteLine("8. Показати товар заданої категорії");
                Console.WriteLine("9. Показати товар заданого постачальника.");
                Console.WriteLine("10. Показати товар який знаходиться на складі найдовше з усіх.");
                Console.WriteLine("11. Показати середню кількість товарів за кожним типом товару.");
                Console.WriteLine("12. Додати товар.");
                Console.WriteLine("13. Додати тип товару.");
                Console.WriteLine("14. Додати постачальника.");
                Console.WriteLine("15. Оновити дані про товар.");
                Console.WriteLine("16. Оновити тип товару.");
                Console.WriteLine("17. Оновити дані постачальника.");
                Console.WriteLine("18. Видалити дані про товар.");
                Console.WriteLine("19. Видалити тип товару.");
                Console.WriteLine("20. Видалити дані постачальника.");
                Console.WriteLine("21. Показати інформацію про постачальника, в якого кількість товарів на складі найбільша");
                Console.WriteLine("22. Показати інформацію про постачальника, в якого кількість товарів на складі найменша.");
                Console.WriteLine("23. Показати інформацію про тип товару з найбільшою кількістю одиниць на складі.");
                Console.WriteLine("24. Показати інформацію про тип товарів з найменшою кількістю товарів на складі.");
                Console.WriteLine("25. Показати товари, з постачання яких минула задана кількість днів.");

                Console.WriteLine("0. Вийти з програми");

                Console.Write("Виберіть опцію: ");
                choice = int.Parse(Console.ReadLine());


                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();


                        switch (choice)
                        {
                            case 1:
                                DisplayAllProductData(connection);
                                break;
                            case 2:
                                DisplayAllProductTypes(connection);
                                break;
                            case 3:
                                DisplayAllSuppliers(connection);
                                break;
                            case 4:
                                DisplayProductWithMaxQuantity(connection);
                                break;
                            case 5:
                                DisplayProductWithMinQuantity(connection);
                                break;
                            case 6:
                                DisplayProductWithMinCost(connection);
                                break;
                            case 7:
                                DisplayProductWithMaxCost(connection);
                                break;
                            case 8:
                                DisplayProductsByType(connection);
                                break;
                            case 9:
                                DisplayProductsBySupplier(connection);
                                break;
                            case 10:
                                ShowProductWithLongestSupplyDate(connection);
                                break;
                            case 11:
                                ShowAverageQuantityByProductType(connection);
                                break;
                            case 12:
                                InsertProduct(connection);
                                break;
                            case 13:
                                InsertProductType(connection);
                                break;
                            case 14:
                                InsertSupplier(connection);
                                break;
                            case 15:
                                UpdateProduct(connection);
                                break;
                            case 16:
                                UpdateProductType(connection);
                                break;
                            case 17:
                                UpdateSupplier(connection);  
                                break;
                            case 18:
                                DeleteProduct(connection);
                                break;
                            case 19:
                                DeleteProductType(connection);
                                break;
                            case 20:
                                DeleteSupplier(connection);
                                break;
                            case 21:
                                ShowSupplierWithMostProducts(connection);
                                break;
                            case 22:
                                ShowSupplierWithLeastProducts(connection);
                                break;
                            case 23:
                                ShowProductTypeWithMostUnits(connection);
                                break;
                            case 24:
                                ShowProductTypeWithLeastUnits(connection);
                                break;
                            case 25:
                                ShowProductsBySupplyDate(connection);
                                break;
                            case 0:
                                Console.WriteLine("Poka!");
                                break;
                            default:
                                Console.WriteLine("Неправильний вибір.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                }
                Thread.Sleep(5000);
            } while (choice != 0);

        }
        static void DisplayAllProductData(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Name: {reader["ProductName"]}, Product Type: {reader["ProductType"]}, Supplier ID: {reader["SupplierID"]}, Quantity: {reader["Quantity"]}, Cost: {reader["Cost"]}, Supply Date: {reader["SupplyDate"]}");
                }
            }
        }

        static void DisplayAllProductTypes(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT DISTINCT ProductType FROM Products", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Type: {reader["ProductType"]}");
                }
            }
        }

        static void DisplayAllSuppliers(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Suppliers", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Supplier ID: {reader["SupplierID"]}, Supplier Name: {reader["SupplierName"]}, Supplier Location: {reader["SupplierLocation"]}");
                }
            }
        }

        static void DisplayProductWithMaxQuantity(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Quantity DESC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Quantity: {reader["Quantity"]}");
                }
            }
        }

        static void DisplayProductWithMinQuantity(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Quantity ASC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Quantity: {reader["Quantity"]}");
                }
            }
        }

        static void DisplayProductWithMinCost(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Cost ASC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Cost: {reader["Cost"]}");
                }
            }
        }

        static void DisplayProductWithMaxCost(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 * FROM Products ORDER BY Cost DESC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product ID: {reader["ProductID"]}, Product Name: {reader["ProductName"]}, Cost: {reader["Cost"]}");
                }
            }
        }

        static void DisplayProductsBySupplier(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT p.ProductName FROM Products p JOIN Suppliers s ON p.SupplierID = s.SupplierID WHERE s.SupplierID = 3;", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductName"]}");
                }
            }

        }

        static void DisplayProductsByType(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT p.ProductName FROM Products p WHERE p.ProductType = 'Type 1';", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductName"]}");
                }
            }

        }

        static void ShowProductWithLongestSupplyDate(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 ProductName FROM Products ORDER BY SupplyDate DESC;", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Товар, який знаходиться на складі найдовше: {reader["ProductName"]}"); ;
                }

            }
        }

        static void ShowAverageQuantityByProductType(SqlConnection connection)
        {

            using (SqlCommand command = new SqlCommand("SELECT ProductType, AVG(Quantity) AS AverageQuantity FROM Products GROUP BY ProductType", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Тип: {reader["ProductType"]}, Середня кількість: {reader["AverageQuantity"]}");
                }

            }

        }






        static void InsertProduct(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Products (ProductName, ProductType, SupplierID, Quantity, Cost, SupplyDate) VALUES ('Product 4', 'Type 2', 3, 300, 20.00, '2024-02-11')", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Товар успішно додано.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати товар.");
                }
            }
        }

        static void InsertProductType(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Products (ProductType) VALUES ('Type 4')", connection))
            {

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Тип товару успішно додано.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати тип товару.");
                }
            }
        }

        static void InsertSupplier(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO Suppliers (SupplierName, SupplierLocation) VALUES ('Supplier D', 'Location D')", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Постачальника успішно додано.");
                }
                else
                {
                    Console.WriteLine("Не вдалося додати постачальника.");
                }
            }
        }


        static void UpdateProduct(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Products SET ProductName = 'Product 0', ProductType = 'Type 0', Quantity = '100500', Cost = 100.50, SupplyDate = '2023-03-23' WHERE ProductID = 4;", connection))
            {

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Інформацію про товар успішно оновлено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося оновити інформацію про товар.");
                }
            }
        }

        static void UpdateSupplier(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Suppliers SET SupplierName = 'Supplier O', SupplierLocation = 'Location O' WHERE SupplierID = 4", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Інформацію про постачальника успішно оновлено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося оновити інформацію про постачальника.");
                }
            }
        }

        static void UpdateProductType(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("UPDATE Products SET ProductType = 'Type A' WHERE ProductID = 4;", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Інформацію про тип товару успішно оновлено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося оновити інформацію про тип товару.");
                }
            }
        }

        static void DeleteProduct(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductID = 4", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Товар успішно видалено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося видалити товар.");
                }
            }
        }

        static void DeleteSupplier(SqlConnection connection)
        {

            using (SqlCommand command = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID = 4", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Постачальника успішно видалено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося видалити постачальника.");
                }
            }
        }

        static void DeleteProductType(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductType = 'Type 4'", connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Тип товару успішно видалено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося видалити тип товару.");
                }
            }
        }


        static void ShowSupplierWithMostProducts(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 s.SupplierID, s.SupplierName, SUM(p.Quantity) AS TotalProducts FROM Products p INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID GROUP BY s.SupplierID, s.SupplierName ORDER BY TotalProducts DESC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Постачальник з найбільшою кількістю товарів:");
                    Console.WriteLine($"ID: {reader["SupplierID"]}, Назва: {reader["SupplierName"]}, Кількість товарів: {reader["TotalProducts"]}");
                }
            }
        }


        static void ShowSupplierWithLeastProducts(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 s.SupplierID, s.SupplierName, SUM(p.Quantity) AS TotalProducts FROM Products p INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID GROUP BY s.SupplierID, s.SupplierName ORDER BY TotalProducts ASC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Постачальник з найменшою кількістю товарів:");
                    Console.WriteLine($"ID: {reader["SupplierID"]}, Назва: {reader["SupplierName"]}, Кількість товарів: {reader["TotalProducts"]}");
                }
            }
        }

        static void ShowProductTypeWithMostUnits(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 p.ProductType, SUM(p.Quantity) AS TotalUnits FROM Products p GROUP BY p.ProductType ORDER BY TotalUnits DESC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Тип товару з найбільшою кількістю одиниць на складі:");
                    Console.WriteLine($"Тип: {reader["ProductType"]}, Кількість: {reader["TotalUnits"]}");
                }
            }
        }


        static void ShowProductTypeWithLeastUnits(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT TOP 1 p.ProductType, SUM(p.Quantity) AS TotalUnits FROM Products p GROUP BY p.ProductType ORDER BY TotalUnits ASC", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Тип товару з найменшою кількістю товарів на складі:");
                    Console.WriteLine($"Тип: {reader["ProductType"]}, Кількість: {reader["TotalUnits"]}");
                }
            }
        }

        static void ShowProductsBySupplyDate(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE SupplyDate <= DATEADD(day, -5, GETDATE())", connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Назва: {reader["ProductName"]}, Кількість: {reader["Quantity"]}, Дата постачання: {reader["SupplyDate"]}");
                }
            }
        }

    }


}