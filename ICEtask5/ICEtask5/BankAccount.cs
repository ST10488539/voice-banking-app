using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace ICEtask5
{
    internal class BankAccount : BankSystemApp
    {
        private double balance;
        private string accountNumber;
        private int pin;
        private List<string> transactionHistory = new List<string>();

        private SpeechSynthesizer synth = new SpeechSynthesizer();

        public string AccountNumber { get { return accountNumber; } }
        public int PIN { get { return pin; } }

        public BankAccount(string accNumber, int pinCode, double initialBalance = 0)
        {
            accountNumber = accNumber;
            pin = pinCode;
            balance = initialBalance;
        }

        public void Deposit(double amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than 0.");
            balance += amount;
            transactionHistory.Add($"Deposited: {amount:C}, New Balance: {balance:C}");
            Console.WriteLine($"Successfully deposited {amount:C}. Current balance: {balance:C}");
            synth.Speak($"Successfully deposited {amount} dollars. Current balance is {balance} dollars.");
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal amount must be greater than 0.");
            if (amount > balance) throw new InvalidOperationException("Insufficient funds.");

            balance -= amount;
            transactionHistory.Add($"Withdrew: {amount:C}, New Balance: {balance:C}");
            Console.WriteLine($"Successfully withdrew {amount:C}. Current balance: {balance:C}");
            synth.Speak($"Successfully withdrew {amount} dollars. Current balance is {balance} dollars.");
        }

        public void CheckBalance()
        {
            Console.WriteLine($"Current balance: {balance:C}");
            synth.Speak($"Your current balance is {balance} dollars.");
        }

        public void Transfer(BankAccount toAccount, double amount)
        {
            if (amount <= 0) throw new ArgumentException("Transfer amount must be greater than 0.");
            if (amount > balance) throw new InvalidOperationException("Insufficient funds for transfer.");

            balance -= amount;
            toAccount.balance += amount;

            transactionHistory.Add($"Transferred {amount:C} to {toAccount.AccountNumber}, New Balance: {balance:C}");
            toAccount.transactionHistory.Add($"Received {amount:C} from {AccountNumber}, New Balance: {toAccount.balance:C}");

            Console.WriteLine($"Successfully transferred {amount:C} to account {toAccount.AccountNumber}");
            synth.Speak($"Successfully transferred {amount} dollars to account {toAccount.AccountNumber}.");
        }

        public void ShowTransactionHistory()
        {
            Console.WriteLine("\n--- Transaction History ---");
            synth.Speak("Here is your transaction history.");
            if (transactionHistory.Count == 0)
            {
                Console.WriteLine("No transactions yet.");
                synth.Speak("No transactions yet.");
            }
            else
            {
                foreach (string record in transactionHistory)
                {
                    Console.WriteLine(record);
                }
            }
        }
    }
}
