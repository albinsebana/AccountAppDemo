using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountAppDemo.Model
{
    internal class AccountManager
    {
        public static Account DepositAmount(Account account, double depositAmount, string filePath)
        {
            account.Balance += depositAmount;
            AccountController.SaveAccountDetails(account, filePath);
            return account;
        }

        public static Account WithdrawAmount(Account account, double withdrawAmount, string filePath)
        {
            
            account.Balance -= withdrawAmount;
            AccountController.SaveAccountDetails(account, filePath);
            Console.WriteLine("Amount withdrawn successfully.");
            return account;
        }
    }
}
