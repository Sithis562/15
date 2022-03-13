using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library
{
    public interface IListOfClients
    {
        public List<Client> ListOfClients(string path)
        {
            var clients = new List<Client>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] arg = sr.ReadLine().Split(' ');

                    clients.Add(new Client(int.Parse(arg[0]), arg[1], arg[2], arg[3]));
                }
            }

            return clients;
        }
    }

    public class Client : Account<long, int>, IListOfClients
    {
        protected int idc;
        protected string firstName;
        protected string lastName;
        protected string patronymic;

        public int IDc { get { return this.idc; } }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get { return this.firstName; } }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get { return this.lastName; } }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get { return this.patronymic; } }

        /// <summary>
        /// Конструктор клиента
        /// </summary>
        public Client(int IDc, string LastName, string FirstName, string Patronymic)
        {
            idc = IDc;
            lastName = LastName;
            firstName = FirstName;
            patronymic = Patronymic;
        }

        public Client() : this(0, "", "", "")
        {
        }

        public override string ToString()
        {
            return $"{IDc} {LastName} {FirstName} {Patronymic}";
        }
    }
}
