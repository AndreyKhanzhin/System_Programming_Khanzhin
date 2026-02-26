using System.Diagnostics;


while (true)
{
    Process[] processes = Process.GetProcesses();
    string com;
    int id;
    Process process;

    foreach (var pro in processes)
    {
        try
        {
            Console.WriteLine($"ID процесса: {pro.Id} | Имя процесса: {pro.ProcessName} | Время запуска: {pro.StartTime} | Приоритет: {pro.PriorityClass} ;");
        }
        catch (System.ComponentModel.Win32Exception) { }
        catch (System.InvalidOperationException) { }
    }

    Console.WriteLine("Доступные команды:\n/kill\n/start\n/info");

    com = Console.ReadLine();

    switch (com)
    {
        case "/kill":
            Console.WriteLine("Введите идентификатор процесса.");
            int.TryParse(Console.ReadLine(), out id);
            process = processes.FirstOrDefault(p => p.Id == id);
            if (process.CloseMainWindow() == false)
            {
                process.Kill();
            }
            Console.WriteLine($"Процесс {id} завершён.");
            break;

        case "/start":
            Console.WriteLine("Введите полный путь исполняемого файла.");
            com = Console.ReadLine();
            Process.Start($"\"{com}\"");
            break;

        case "/info":
            Console.WriteLine("Введите ID или имя процесса.");
            com = Console.ReadLine();
            if (int.TryParse(com, out id) == false)
            {
                process = processes.FirstOrDefault(p => p.ProcessName == com);
            }
            else
            {
                process = processes.FirstOrDefault(p => p.Id == id);
            }
            Console.WriteLine($"Время работы — {process.TotalProcessorTime} | Занимаемая память — {process.WorkingSet64 / 8} МБ | Количество потоков — {process.Threads.Count}");
            break;
    }
}