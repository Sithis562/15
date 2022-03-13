using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public interface IListOfAccounts
    {
        public List<Account<long, int>> ListOfAccounts(string path)
        {
            List<Account<long, int>> accounts = new List<Account<long, int>>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] arg = sr.ReadLine().Split(' ');

                    accounts.Add(new Account<long, int>(Convert.ToInt32(arg[0]), Convert.ToInt64(arg[1]), Convert.ToInt32(arg[2]), arg[3], arg[4]));
                }
            }

            return accounts;
        }
    }

    public interface IAddAccount : IListOfAccounts
    {
        public void AddAccount(Account<long, int> account, string path)
        {
            List<Account<long, int>> accounts = new List<Account<long, int>>();

            accounts.Add(account);

            string[] accountstxt = new string[accounts.Count];

            for (int i = 0; i < accounts.Count; i++)
            {
                accountstxt[i] = accounts[i].ToString();
            }

            File.AppendAllLines(path, accountstxt);

        }
    }

    public interface ITransfer : IListOfAccounts
    {
        public void Transfer(string from, string to, string Amount, string path)
        {
            string accountstxt = "accounts.txt";

            List<Account<long, int>> accounts = new List<Account<long, int>>(ListOfAccounts(accountstxt));

            for (int i = 0; i < accounts.Count; i++)
            {
                if (from.Contains(Convert.ToString(accounts[i].Number)))
                {
                    for (int j = 0; j < accounts.Count; j++)
                    {
                        if (to.Contains(Convert.ToString(accounts[j].Number)))
                        {
                            int amount = Convert.ToInt32(Amount);

                            accounts[j].Amount = accounts[j].Amount + amount;
                            accounts[i].Amount = accounts[i].Amount - amount;
                        }
                    }
                }
            }
            string[] accountsList = new string[accounts.Count];

            for (int i = 0; i < accounts.Count; i++)
            {
                accountsList[i] = accounts[i].ToString();
            }

            File.WriteAllLines(accountstxt, accountsList);
        }
    }

    public class Account<T1, T2> : IAddAccount, ITransfer
    {
        protected int id;
        protected T1 number;
        protected T2 amount;
        protected string status;
        protected string type;

        public int ID { get { return this.id; } }

        /// <summary>
        /// Номер счёта
        /// </summary>
        public T1 Number { get { return this.number; } }

        /// <summary>
        /// Средства
        /// </summary>
        public T2 Amount { get { return this.amount; } set { this.amount = value; } }

        /// <summary>
        /// Статус, открыт/закрыт
        /// </summary>
        public string Status { get { return this.status; } set { this.status = value; } }

        /// <summary>
        /// Тип, депозитный/недепозитный
        /// </summary>
        public string Type { get { return this.type; } }

        public Account(int ID, T1 Number, T2 Amount, string Status, string Type)
        {
            id = ID;
            number = Number;
            amount = Amount;
            status = Status;
            type = Type;
        }

        public Account() : this(0, default, default, "", "")
        {
        }

        public override string ToString()
        {
            return $"{ID} {Number} {Amount} {Status} {Type}";
        }
    }
}
