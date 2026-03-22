using ICEtask5;
using System;
using System.Collections.Generic;
using System.Speech.Synthesis;

namespace BankSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Sample accounts
            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(new BankAccount("1001", 1234, 500));
            accounts.Add(new BankAccount("1002", 5678, 1000));

            synth.Speak("Welcome to Simple Bank System.");
            Console.WriteLine("=== Welcome to Simple Bank System ===");

            BankAccount currentAccount = null;

            while (currentAccount == null)
            {
                synth.Speak("Please enter your account number.");
                Console.Write("Enter Account Number: ");
                string accNum = Console.ReadLine();

                synth.Speak("Please enter your pin.");
                Console.Write("Enter PIN: ");
                bool pinValid = int.TryParse(Console.ReadLine(), out int pinInput);

                if (!pinValid)
                {
                    Console.WriteLine("Invalid PIN format.");
                    synth.Speak("Invalid PIN format.");
                    continue;
                }

                currentAccount = accounts.Find(a => a.AccountNumber == accNum && a.PIN == pinInput);

                if (currentAccount == null)
                {
                    Console.WriteLine("Invalid account number or PIN.");
                    synth.Speak("Invalid account number or PIN. Try again.");
                }
            }

            bool exit = false;

            while (!exit)
            {
                synth.Speak("Please choose an option from the menu.");
                Console.WriteLine("\n--- Menu ---");
                synth.Speak(" Menu options");
                Console.WriteLine("1. Deposit");
                synth.Speak("press 1 for deposit");
                Console.WriteLine("2. Withdraw");
                synth.Speak("press 2 for withdrawal");
                Console.WriteLine("3. Check Balance");
                synth.Speak("press 3 to check balance");
                Console.WriteLine("4. Transfer");
                synth.Speak("press 4 to transfer");

                Console.WriteLine("5. Transaction History");
                synth.Speak("press 5 for transaction history");
                Console.WriteLine("6. Exit");
                synth.Speak("press 6 to exit");
                Console.Write("Choose an option: ");
                synth.Speak("enter number now");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            synth.Speak("Enter deposit amount.");
                            Console.Write("Enter deposit amount: ");
                            double depositAmount = double.Parse(Console.ReadLine());
                            currentAccount.Deposit(depositAmount);
                            break;

                        case "2":
                            synth.Speak("Enter withdrawal amount.");
                            Console.Write("Enter withdrawal amount: ");
                            double withdrawAmount = double.Parse(Console.ReadLine());
                            currentAccount.Withdraw(withdrawAmount);
                            break;

                        case "3":
                            currentAccount.CheckBalance();
                            break;

                        case "4":
                            synth.Speak("Enter recipient account number.");
                            Console.Write("Enter recipient account number: ");
                            string recipientAcc = Console.ReadLine();
                            BankAccount toAccount = accounts.Find(a => a.AccountNumber == recipientAcc);

                            if (toAccount == null)
                            {
                                Console.WriteLine("Recipient account not found.");
                                synth.Speak("Recipient account not found.");
                                break;
                            }

                            synth.Speak("Enter transfer amount.");
                            Console.Write("Enter transfer amount: ");
                            double transferAmount = double.Parse(Console.ReadLine());
                            currentAccount.Transfer(toAccount, transferAmount);
                            break;

                        case "5":
                            currentAccount.ShowTransactionHistory();
                            break;

                        case "6":
                            exit = true;
                            Console.WriteLine("Thank you for using Simple Bank System!");
                            synth.Speak("Thank you for using Simple Bank System. Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            synth.Speak("Invalid option. Try again.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter numeric values.");
                    synth.Speak("Invalid input. Please enter numeric values.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    synth.Speak(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    synth.Speak(ex.Message);
                }
            }
        }
    }
}
