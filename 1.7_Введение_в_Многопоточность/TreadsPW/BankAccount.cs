using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TreadsPW
{

    public class BankAccount
    {
        public int balance = 1000;
        private readonly object _locker = new object();
        public void Withdraw(int amount)
        {
            lock (_locker)
            {
                if (amount <= balance)
                {
                    Thread.Sleep(10);
                    balance -= amount;
                    Console.WriteLine($"Списание прошло успешно. Осталось {balance}.");
                }
                else
                {
                    Console.WriteLine("ОШИБКА ОПЕРАЦИИ");
                }
            }
        }
    }
}
