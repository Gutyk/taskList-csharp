using System.Text.Json;
using System.Linq;
class TodoTask
{
    public int Id { get; set; }
    public string Title { get; set; } = " ";
    public string Description { get; set; } = " ";
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; } = null;
}


class Program
{
    static string dataFile = "tasks.json";
    static List<TodoTask> tasks = new();

    static void Main()
    {
        Console.WriteLine(@"
    ▀█▀ ▄▀█ █▀ █▄▀ █░░ █ █▀ ▀█▀
    ░█░ █▀█ ▄█ █░█ █▄▄ █ ▄█ ░█░
    ");

        LoadTasks();
        ListOptions();

    }

    static void ListOptions()
    {
        int option;
        do
        {
            Console.WriteLine("\n1 - List\n2 - Add\n3 - Complete\n4 - Update\n5 - Delete\n6 - Exit");

            Console.Write("\nSelect an option: ");
            option = int.Parse(Console.ReadLine() ?? "0");
            switch (option)
            {
                case 1: ListTasks(); break;
                case 2: AddTask(); break;
                case 3: CompleteTask(); break;
                case 4: UpdateTask(); break;
                case 5: DeleteTask(); break;
                case 6: Console.WriteLine("Exiting..."); break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        } while (option != 6);
    }

    static void ListTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.\n");
            return;
        }
        foreach (var task in tasks)
        {
            string completedInfo = task.IsCompleted
                ? $"✔ (Completed at {task.CompletedAt:yyyy-MM-dd HH:mm})"
                : "⏳ Pending";

            Console.WriteLine($"ID: {task.Id} | Title: {task.Title} | Description: {task.Description} | {completedInfo} | Created At: {task.CreatedAt:yyyy-MM-dd HH:mm}");
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine() ?? "";
        while (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title cannot be empty.");
            Console.Write("Enter task title: ");
            title = Console.ReadLine() ?? "";
        }
        Console.Write("Enter task description: ");
        string description = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(description))
        {
            description = "No description provided.";
        }
        var newTask = new TodoTask
        {
            Id = tasks.Count > 0 ? tasks.Max(tasks => tasks.Id) + 1 : 1,
            Title = title,
            Description = description,
            IsCompleted = false,
            CreatedAt = DateTime.Now
        };

        tasks.Add(newTask);
        Console.WriteLine($"Task '{title}' added with description: {description}");

        SaveTask();
    }

    static void CompleteTask()
    {
        ListTasks();
        Console.Write("Enter the ID of the task to complete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var task = tasks.Find(task => task.Id == id);
        if (task != null)
        {
            task.IsCompleted = true;
            task.CompletedAt = DateTime.Now;
            SaveTask();
            Console.WriteLine($"Task '{task.Title}' marked as completed at {task.CompletedAt:yyyy-MM-dd HH:mm}.");
        }
    }

    static void UpdateTask()
    {
        ListTasks();
        Console.Write("Enter the ID of the task to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var task = tasks.Find(task => task.Id == id);
        if (task != null)
        {
            Console.Write("Enter new title (leave blank to keep current): ");
            string newTitle = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                task.Title = newTitle;
            }

            Console.Write("Enter new description (leave blank to keep current): ");
            string newDescription = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                task.Description = newDescription;
            }

            Console.WriteLine($"Task '{task.Title}' updated.");
            SaveTask();
        }
    }

    static void DeleteTask()
    {
        ListTasks();
        Console.Write("Enter the ID of the task to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var task = tasks.Find(task => task.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].Id = i + 1;
            }
            Console.WriteLine($"Task '{task.Title}' deleted.");
            SaveTask();
        }
    }

    static void LoadTasks()
    {
        if (File.Exists(dataFile))
            tasks = JsonSerializer.Deserialize<List<TodoTask>>(File.ReadAllText(dataFile)) ?? new();
    }

    static void SaveTask()
    {
        File.WriteAllText(dataFile, JsonSerializer.Serialize(tasks));
    }
}