using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        string[] sites = {
            "https://github.com/AndreyKhanzhin/System_Programming_Khanzhin",
            "https://warthunder.ru/ru",
            "https://www.dota2.com/home?l=russian"
        };

        var canselToken = new CancellationTokenSource();
        var progressBar = new Progress<int>(p =>
            Console.WriteLine($"Прогресс: {p}% (Поток: {Thread.CurrentThread.ManagedThreadId})"));

        _ = Task.Run(() =>
        {
            Console.WriteLine("Нажми любую клавишу, чтобы отменить загрузку...");
            Console.ReadKey();
            canselToken.Cancel();
        });

        int totalsymBols = 0;

        try
        {
            for (int i = 0; i < sites.Length; i++)
            {
                int символовНаСтранице = await LoadWebPage(
                    sites[i],
                    canselToken.Token,
                    progressBar);

                totalsymBols += символовНаСтранице;
            }

            Console.WriteLine($"\nГОТОВО! Всего символов: {totalsymBols}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("\nЗагрузка отменена пользователем!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static async Task<int> LoadWebPage(string url, CancellationToken token, IProgress<int> progress)
    {
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Начинаю загрузку: {url}");

        using var client = new HttpClient();
        string data = await client.GetStringAsync(url, token);
        progress?.Report(50);
        int amount = data.Length;
        await Task.Delay(800, token);
        progress?.Report(100);
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Страница готова, символов: {amount}");
        return amount;
    }
}