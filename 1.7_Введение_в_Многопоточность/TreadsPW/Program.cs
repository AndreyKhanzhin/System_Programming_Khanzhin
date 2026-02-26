using TreadsPW;

class Prigram
{
    static void Main()
    {
        BankAccount account = new BankAccount();

        List<Task> operations = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            operations.Add(Task.Run(() => account.Withdraw(150)));
        }

        Task.WaitAll(operations);
    }
}

