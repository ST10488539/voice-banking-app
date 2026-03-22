using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICEtask5
{
    internal class BankSystemApp
    {
        interface IBankOperations
        {
            void Deposit(double amount);
            void Withdraw(double amount);
            void CheckBalance();
        }
    }
}
