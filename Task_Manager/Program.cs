// See https://aka.ms/new-console-template for more information

using Task_Manager;

class Program
{
    static void Main()
    {
        int opcion;
        var task = new Task_User();
        do
        {
            Console.WriteLine("Bienvenido al gestor de tareas. Indique la opcion que desee: ");
            Console.Write("1. Anadir tarea.\n" +
                          "2. Listar tareas.\n" +
                          "3. Actualizar tarea.\n" +
                          "4. Eliminar tarea.\n" +
                          "5. Salir.\n");

            try
            {
                opcion = int.Parse(Console.ReadLine());
                Console.WriteLine(opcion);
                switch (opcion)
                {
                    case 1:
                        task.CreateTask();
                        break;
                    case 2:
                        Console.WriteLine("opcion 2.");
                        break;
                    case 3:
                        Console.WriteLine("opcion 3.");
                        break;
                    case 4:
                        Console.WriteLine("opcion 4.");
                        break;
                    case 5:
                        Console.WriteLine("opcion 5.");
                        break;
                    default:
                        Console.WriteLine("Seleccione una opcion valida.");
                        break;
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Ingrese una opcion valida (Numeros del 1 al 5)");
                opcion = 0;
            }
            
        }while (opcion != 5);
    }
}