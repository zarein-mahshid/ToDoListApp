using System;
using Microsoft.Data.SqlClient;

namespace ToDoApp
{
    class Program
    {
        // Connection string for SQL Server (update Server name if needed)
        static string _connectionString = "Server=DESKTOP-O8MUUM8;Database=ToDoListDB;Integrated Security=True;TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== To-Do List Menu ===");
                Console.WriteLine("1. Show Tasks");
                Console.WriteLine("2. Add New Task");
                Console.WriteLine("3. Mark Task as Done");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write(" Choose an option (1-5): ");

                string choice = Console.ReadLine()??"";

                if (string.IsNullOrWhiteSpace(choice))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                Console.Clear();

                switch (choice)
                {
                    case "1":
                     ShowTasks();
                     break;
                    case "2":
                     AddTask(); 
                     break;
                    case "3": 
                    CompleteTask(); 
                    break;
                    case "4": 
                    DeleteTask(); 
                    break;
                    case "5": 
                    Console.WriteLine("Goodbye!"); 
                    return;
                    default: 
                    Console.WriteLine("Invalid input. Please try again."); 
                    break;
                }
            }
        }

        static void ShowTasks()
        {
            // Create a connection to the database
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT Id, Title, IsCompleted, Priority FROM Tasks";
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                    Console.WriteLine("No tasks found!");
                else
                {
                    Console.WriteLine("Your Tasks:");
                    while (reader.Read())
                    {
                        string status = reader.GetBoolean(2) ? "[Done]" : "[ ] Pending";
                        int priority = reader.GetInt32(3);
                        string priorityText = priority == 1 ? "Hight" : (priority == 2 ? "Medium" : "Low");
                        string priorityEmoji = priority == 1 ? "🔴" : (priority == 2 ? "🟡" : "🟢");
                        Console.WriteLine($" {status}  ID: {reader.GetInt32(0)}  Title: {reader.GetString(1)}  Priority: {priorityEmoji} {priorityText}");
                    }
                }
                reader.Close();
            }
        }

        static void AddTask()
        {
            Console.Write("Enter new task title:");
            string title = Console.ReadLine()??"";

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty!");
                return;
            }

            Console.WriteLine("Select priority");
            Console.WriteLine("  1. High (🔴)");
            Console.WriteLine("  2. Medium (🟡)");
            Console.WriteLine("  3. Low (🟢)");
            Console.Write("Enter 1, 2, or 3: ");

            string priorityInput = Console.ReadLine()??"";

            int priority = 3;
            if (priorityInput == "1") priority = 1;
            else if (priorityInput == "2") priority = 2;
            else priority = 3;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Tasks (Title, Priority) VALUES (@Title, @Priority)";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@Priority", priority);

                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine($"Task '{title}' with priority {priority} added successfully.");
                else
                    Console.WriteLine("Failed to add task.");
            }
        }

        static void CompleteTask()
        {
            ShowTasks();
            Console.Write("Enter the ID of the task you completed:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Please enter a valid number.");
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = @Id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine($"Task with ID {id} marked as done! Well done!");
                else
                    Console.WriteLine($"Task with ID {id} not found.");
            }
        }

        static void DeleteTask()
        {
            ShowTasks();
            Console.Write("Enter the ID of the task to delete:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Please enter a valid number.");
                return;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Tasks WHERE Id = @Id";
                var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine($"Task with ID {id} deleted successfully.");
                else
                    Console.WriteLine($"Task with ID {id} not found.");
            }
        }
    }
}


