using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System;
namespace Task_Manager;

public class Task_User
{
    public Task_User(){}
    public string Tittle; 
    public string Description;
    public enum Priority
    {
        LOW = 3,
        MEDIUM = 2,
        HIGH = 1,
    }
    public DateTime StartTime;
    public DateTime EndTime;

    public void CreateTask()
    {
        Console.Write("Ingrese el titulo: ");
        Tittle = ValidateString(Console.ReadLine(), "Titulo");
        Console.Write("Ingrese la descripcion: ");
        Description = ValidateString(Console.ReadLine(), "Descripcion");
        bool check = false;
        do
        {
            Console.Write("Seleccione la prioridad (Indique el numero o el nombre): \n");
            foreach (var value in Enum.GetValues(typeof(Priority)))
            {
                Console.WriteLine($"{(int)value} - {value}");
            }
            Console.Write("Prioridad: ");
            string input = Console.ReadLine().ToUpper();
            int x;
            Priority priority;
            
            if (Int32.TryParse(input, out x))
            {
                check = Enum.IsDefined(typeof(Priority), x);
            }
            else
            {
                check = Enum.TryParse<Priority>(input, out priority);
            }
            
            if (!check)
            {
                Console.WriteLine("Ingrese una prioridad valida.");
            }
        }while(!check);
        
        Console.Write("Ingrese la fecha de inicio (format DD/MM/YYYY): ");
        StartTime = ValidateDate(Console.ReadLine());
        
        Console.Write("Ingrese la fecha de fin (format DD/MM/YYYY): ");
        EndTime = ValidateDate(Console.ReadLine());
        
        writeTask(Tittle,Description,StartTime,EndTime);
                
    }

    private DateTime ValidateDate(string date)
    {
        DateTime dateTime;
        string datePattern = "dd/MM/yyyy";
        while (true)
        {
            if (string.IsNullOrEmpty(date))
            {
                Console.WriteLine("La fecha no puede estar vacio.");
            }else if (!DateTime.TryParseExact(date, datePattern, null, DateTimeStyles.None, out dateTime))
            {
                Console.WriteLine($"Formato de fecha invalido, el formato debe de ser: {datePattern} ");
            }
            else
            {
                break;
            }
            Console.Write($"Ingrese la fecha nuevamente (formato {datePattern}): ");
            date = Console.ReadLine();
        }
        return dateTime;
    }

    private string ValidateString(string input, string fieldName)
    {
        while (true)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine($"{fieldName} no puede estar vacio.");
                Console.Write($"Ingrese el {fieldName} nuevamente: ");
                input = Console.ReadLine();
            }
            else
            {
                break;
            }
            
        }
        return input;
    }

    private void writeTask(string title, string description, DateTime startTime, DateTime endTime)
    {
        List<Task_User> tasks = new List<Task_User>();
        
        var pathFile = @"/home/d3s/RiderProjects/Task_Manager/Task_Manager/tasks.json";

        try
        {
            if (File.Exists(pathFile))
            {
                tasks = JsonConvert.DeserializeObject<List<Task_User>>(pathFile) ?? new List<Task_User>();
            }
            else
            {
                File.WriteAllText(pathFile, "");
            }

            tasks.Add(new Task_User()
            {
                Tittle = title,
                Description = description,
                StartTime = startTime,
                EndTime = endTime
            });
        
            string json = JsonConvert.SerializeObject(tasks);
            File.WriteAllText(pathFile, json);
        }
        catch (Exception ex)
        {
            var pathLogs = @"/home/d3s/RiderProjects/Task_Manager/Task_Manager/tasks.json";
            Console.WriteLine(ex.Message);
        }
    }

    public List<Task_User>  readTask()
    {
        List<Task_User>? tasks = new List<Task_User>();
        var pathFile = @"/home/d3s/RiderProjects/Task_Manager/Task_Manager/tasks.json";   
        FileInfo fileInfo = new FileInfo(pathFile);
        if (File.Exists(pathFile) && fileInfo.Length > 0)
        {
            string jsonContent = File.ReadAllText(pathFile);
            tasks = JsonConvert.DeserializeObject<List<Task_User>>(jsonContent);
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Tarea {i+1}. {tasks[i].Tittle}, {tasks[i].Description}, {tasks[i].StartTime}, {tasks[i].EndTime}");
            }
        }
        else
        {
            Console.WriteLine("No hay tareas.");
        }
        return tasks;
    }

    public void deleteTask()
    {
        List<Task_User> tasks = readTask();
        Console.WriteLine("Ingrese el titulo: ");
        

    }
}