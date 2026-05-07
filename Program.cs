using Microsoft.Data.SqlClient;

namespace ADODOTNETCoreDemo;

public class Program
{
    static void Main(string[] args)
    {
        try
        {
            string masterConn = "Server=.;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
            string createDb = @"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'StudentDB')
            BEGIN
                CREATE DATABASE StudentDB
            END";

            // If There is no database named StudentDB, it will create one.
            // ADO.NET won't automatically create database if it isn't exist. 
            using(SqlConnection connection = new SqlConnection(masterConn))
            {
                connection.Open();
                using var cmd = new SqlCommand(createDb, connection);
                cmd.ExecuteNonQuery();
            }


            string connectionString = "Server=.;Database=StudentDB;Trusted_Connection=True;TrustServerCertificate=True;";

            // Wrapped connection and command objects in using statements to ensure resources are properly disposed.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Successfully connected to the database.");
            

                // Table Creation
                // string createTableCommand = @"
                //     CREATE TABLE Students (
                //         Id INT PRIMARY KEY IDENTITY(1, 1),
                //         FirstName NVARCHAR(50),
                //         LastName NVARCHAR(50),
                //         Email NVARCHAR(50)
                //     );";
                
                // using (SqlCommand command = new SqlCommand(createTableCommand, connection))
                // {
                //     command.ExecuteNonQuery();
                //     Console.WriteLine("Table 'Students' created successfully");
                // }


                // Data insertion
                // string studentFirstName = "Minhajul";
                // string studentLastName = "Islam";
                // string studentEmail = "minhaj@mail.com";

                // string insertSql = @"
                // INSERT INTO Students (FirstName, LastName, Email)
                // VALUES (@FirstName, @LastName, @Email);";

                // using(SqlCommand command = new SqlCommand(insertSql, connection))
                // {
                //     command.Parameters.AddWithValue("@FirstName", studentFirstName);
                //     command.Parameters.AddWithValue("@LastName", studentLastName);
                //     command.Parameters.AddWithValue("@Email", studentEmail);

                //     int rowsAffected = command.ExecuteNonQuery();

                //     if(rowsAffected > 0) Console.WriteLine("Data inserted successfully!");
                //     else Console.WriteLine("No rows were inserted into the database");
                // }



                // Data Fetching
                // Retrive All Rows
                // string selectAllQuery = "SELECT * FROM Students;";

                // using (SqlCommand command = new SqlCommand(selectAllQuery, connection))
                // {
                //     // ExecuteReader is used for queries that return rows.
                //     using (SqlDataReader reader = command.ExecuteReader())
                //     {
                //         while (reader.Read())
                //         {
                //             // Access columns by name or index.
                //             Console.WriteLine(
                //                 $"Id; {reader["Id"]}, " + 
                //                 $"First Name: {reader["FirstName"]}, " +
                //                 $"Last Name: {reader["LastName"]}" +
                //                 $"Email: {reader["Email"]}"
                //             );
                //         }
                //     }
                // }

                // Retrive Specific Row
                // string selectOneQuery = "SELECT * FROM Students WHERE Id = @Id;";

                // using (SqlCommand command = new SqlCommand(selectOneQuery, connection))
                // {
                //     command.Parameters.AddWithValue("@Id", 1);

                //     using (SqlDataReader reader = command.ExecuteReader())
                //     {
                //         while (reader.Read())
                //         {
                //             Console.WriteLine(
                //                 $"Id: {reader["Id"]}, " +
                //                 $"First Name: {reader["FirstName"]}, " +
                //                 $"Last Name: {reader["LastName"]}, " +
                //                 $"Email: {reader["Email"]}"
                //             );
                //         }
                //     }
                // }



                // Update Query
                // string updateSql = @"
                //     UPDATE Students
                //     SET LastName = @LastName,
                //         Email = @Email
                //     WHERE Id = @Id;
                // ";

                // int studentIdToUpdate = 1;
                // string newLastName = "UpdatedLastName";
                // string newEmail = "UpdatedEmail@Example.com";

                // using (SqlCommand command = new SqlCommand(updateSql, connection))
                // {
                //     command.Parameters.AddWithValue("@LastName", newLastName);
                //     command.Parameters.AddWithValue("@Email", newEmail);
                //     command.Parameters.AddWithValue("@Id", studentIdToUpdate);

                //     int rowsAffected = command.ExecuteNonQuery();

                //     if(rowsAffected > 0) Console.WriteLine("Record updated successfully.");
                //     else Console.WriteLine("No record found with the specific Id or no changes made.");
                // }




                // Delete Query
                // string deleteSql = "DELETE FROM Students WHERE Id = @Id";

                // int studentIdToDelete = 2;

                // using (SqlCommand command = new SqlCommand(deleteSql, connection))
                // {
                //     command.Parameters.AddWithValue("@Id", studentIdToDelete);

                //     int rowsAffected = command.ExecuteNonQuery();

                //     if(rowsAffected > 0) Console.WriteLine("Record Deleted Successfully.");
                //     else Console.WriteLine("No record found with this Id.");
                // }



                // Always good practice but not strictly necessary inside a using block:
                connection.Close();
            }

        }
        catch (SqlException sqlEx)
        {
            // Handle SQL-specific exceptions.
            Console.WriteLine($"SQL Exception: {sqlEx.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Something is wrong: {ex.Message}");
        }
    }
}