using System;


namespace Bank
{
    class Program
    {

        public interface IAccount
        {
            // nazwa klienta, bez spacji przed i po
            // readonly - modyfikowalna wyłącznie w konstruktorze
            string Name { get; }

            // bilans, aktualny stan środków, podawany w zaokrągleniu do 2 miejsc po przecinku
            decimal Balance { get; }

            // czy konto jest zablokowane
            bool IsBlocked { get; }
            void Block();            // zablokowanie konta
            void Unblock();          // odblokowanie konta

            // wpłata, dla kwoty ujemnej - zignorowana (false)
            // jeśli konto zablokowane - zignorowana (false)
            // true jeśli wykonano i nastąpiła zmiana salda
            bool Deposit(decimal amount);

            // wypłata, dla kwoty ujemnej - zignorowana (false)
            // jeśli konto zablokowane - zignorowana (false)
            // jeśli jest niewystarczająca ilość środków - zignorowana (false)
            // true jeśli wykonano i nastąpiła zmiana salda   
            bool Withdrawal(decimal amount);
        }

        public class Account : IAccount
        {
            public string Name { get; }
            

            public decimal Balance { get; set; }

            public bool IsBlocked { get; set; }

            string IAccount.Name { get; }

            public void Block()
            {
                IsBlocked = true;
            }

            public bool Deposit(decimal amount)
            {
                if (amount > 0 && IsBlocked == false)
                {
                    Balance = Balance + amount;
                    Math.Round(Balance, 4);
                    return true;
                }
                else
                    return false;
            }

            public void Unblock()
            {
                IsBlocked = false;
            }

            public bool Withdrawal(decimal amount)
            {
                if (Balance - amount > 0 && IsBlocked == false && amount > 0)
                {
                    Balance = Balance - amount;
                    Math.Round(Balance, 4);
                    return true;
                }
                else
                    return false;
            }

            public decimal CheckBalance(decimal Balance)
            {
                if (Balance < 0)
                {

                    throw new ArgumentOutOfRangeException();
                }
                else
                    return Math.Round(Balance, 4);

            }

            public string CheckName(string Name)
            {
                if (Name == null)
                {
                    throw new ArgumentOutOfRangeException();
                    
                }else if(Name.Trim(' ').Length < 3)
                {

                    throw new ArgumentException();
                }
                else
                    return Name.Trim(' ');

            }

            public Account(string Name, decimal Balance = 0)
            {
                this.Name = CheckName(Name);
                this.Balance = CheckBalance(Balance);
                this.IsBlocked = false;
            }

            public override string ToString()
            {
                if (IsBlocked == false)
                    return $"Account name: {Name}, " + $"balance: {String.Format("{0:0.00}", Math.Round(Balance, 2, MidpointRounding.ToEven))}";
                else
                    return $"Account name: {Name.Trim(' ')}, " + $"balance: {String.Format("{0:0.00}", Math.Round(Balance, 2, MidpointRounding.ToEven))}, " + "blocked";
            }




        }

        static void Main()
        {

     
            Console.ReadLine();
        }
    }
}
