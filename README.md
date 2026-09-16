# To-Do List Application

A simple console-based task management application developed with C# and SQL Server.

## About

This project is a personal practice project for working with C# and SQL Server.

The application allows users to manage daily tasks, set priorities, view task status, mark tasks as completed, and delete tasks.

## Features

- Add new tasks
- View tasks
- Mark tasks as completed
- Delete tasks
- Set task priority
- Store and retrieve data from SQL Server
- Basic input validation

## Technologies

- C#
- SQL Server
- Microsoft.Data.SqlClient

## Database

The application uses SQL Server to store task information.

Each task includes:

- Task ID
- Title
- Completion status
- Priority

## How to Run

1. Clone or download the repository.
2. Create a SQL Server database named ToDoListDB.
3. Create the required Tasks table.
4. Update the SQL Server connection string in Program.cs.
5. Open the project in Visual Studio or VS Code.
6. Run the application.
