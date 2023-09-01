using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace AccountAppDemo.Model
{
    internal class AccountController
    {
        private static string filePath = @"C:\Users\albin.kurian\Desktop\DotNet\Section15\AccountAppDemo\myFile.txt";
        Account account = AccountController.LoadAccountDetails(filePath);

       // private static string filePath = @"C:\Users\albin.kurian\Desktop\DotNet\Section15\AccountAppDemo\myFile.txt";
        public static void SaveAccountDetails(Account account, string filePath)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(fs, account);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while saving account details: " + ex.Message);
            }
        }

        public static Account LoadAccountDetails(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    IFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return (Account)formatter.Deserialize(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(" " + ex.Message);
            }

            return null;
        }


        public static Account CreateAccount(string filePath)//for creating acccount
        {
            Console.Write("Enter Account Name: ");
            string accountName = Console.ReadLine();
            Console.Write("Enter Bank Name: ");
            string bankName = Console.ReadLine();
            Console.Write("Enter Opening Balance: ");
            double openingBalance = Convert.ToDouble(Console.ReadLine());

            Account account = new Account
            {
                AccountName = accountName,
                BankName = bankName,
                Balance = openingBalance
            };

            AccountController.SaveAccountDetails(account, filePath);// to save the account deteils
            

            return account;
        }

        public static void DepositAmount(Account account, string filePath)
        {
            Console.Write("Enter amount to deposit: ");
            double depositAmount = Convert.ToDouble(Console.ReadLine());

            account = AccountManager.DepositAmount(account, depositAmount, filePath);

            Console.WriteLine("Amount Deposited Successfully");
        }

        public static void WithdrawAmount(Account account, string filePath)
        {
            Console.Write("Enter amount to withdraw: ");
            double withdrawAmount = Convert.ToDouble(Console.ReadLine());
            if (withdrawAmount > account.Balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else
            {
                account = AccountManager.WithdrawAmount(account, withdrawAmount, filePath);
            }
        }



        public static void Start()
        {
            Account account = LoadAccountDetails(filePath); // Load the account details

            Console.WriteLine("Account loaded successfully.");

            int choice;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1) Deposit");
                Console.WriteLine("2) Withdraw");
                Console.WriteLine("3) Display Balance");
                Console.WriteLine("4) Exit");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        DepositAmount(account, filePath);
                        break;
                    case 2:
                        WithdrawAmount(account, filePath); 
                        break;
                    case 3:
                        Console.WriteLine($"Current balance: {account.Balance}");
                        break;
                    case 4:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (choice != 4);
        }

    }
}
    
