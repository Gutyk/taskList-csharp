# TaskList C#  

Console CRUD application for task management in **C# (.NET 8)**.  
Tasks are stored in a local **JSON file** with support for create, read, update, delete and completion tracking.

---

## 🚀 Features
- Add new tasks (title + description)
- List all tasks with status (⏳ pending / ✔ completed)
- Mark tasks as completed (with timestamp)
- Update task title and description
- Delete tasks (IDs are reorganized sequentially)
- Data persistence in `tasks.json`

---

## 📂 Project Structure
taskList-csharp/
├─ Program.cs # main application logic
├─ taskList-csharp.csproj
└─ tasks.json # data persistence (created at runtime)


---

## ▶️ Running the Project
Make sure you have **.NET 8 SDK** installed.  

```bash
# Run inside the project folder
dotnet run
