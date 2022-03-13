using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library;

namespace _1
{
    public delegate void SaveLog(string Event, string Name, string path);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IListOfClients client = new Client();
        IListOfAccounts account = new Account<long, int>();
        string clientstxt = "clients.txt";
        string accountstxt = "accounts.txt";
        string logtxt = "log.txt";
        string NameM = "Менеджер";


        public MainWindow()
        {
            InitializeComponent();
            List<Client> clients = new List<Client>(client.ListOfClients(clientstxt));
            List<Account<long, int>> accounts = new List<Account<long, int>>(account.ListOfAccounts(accountstxt));

            ClientsList.ItemsSource = clients;
            AccountsList.ItemsSource = accounts;
            cb_From.ItemsSource = accounts;
            cb_To.ItemsSource = accounts;


        }

        public void Log(string Event, string Name, string path)
        {
            string events = $"{DateTime.Now.ToShortTimeString()} {Name} {Event}\n";

            File.AppendAllText(path, events);
        }

        private void btn_AddAccount_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.Show();


        }

        private void btn_Trasfer_Click(object sender, RoutedEventArgs e)
        {
            ITransfer transfer = new Account<long, int>();

            transfer.Transfer(cb_From.Text, cb_To.Text, tx_Amount.Text, accountstxt);

            string Event = $"перевёл с {cb_From.Text} на {cb_To.Text} {tx_Amount.Text}";

            SaveLog saveLog = Log;

            saveLog(Event, NameM, logtxt);
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            List<Account<long, int>> accounts = new List<Account<long, int>>(account.ListOfAccounts(accountstxt));

            string accountstr = AccountsList.SelectedItem.ToString();
            string[] accountarr = accountstr.Split(' ');
            string acc = "";

            for (int i = 0; i < accounts.Count; i++)
            {
                if (accountstr.Contains(Convert.ToString(accounts[i].Number)))
                {
                    accounts[i].Status = "закрыт";

                    acc = Convert.ToString(accounts[i].Number);
                }
            }
            string[] accountsList = new string[accounts.Count];

            for (int i = 0; i < accounts.Count; i++)
            {
                accountsList[i] = accounts[i].ToString();
            }

            File.WriteAllLines(accountstxt, accountsList);

            string Event = $"закрыл счёт {acc}";
            SaveLog saveLog = Log;
            saveLog(Event, NameM, logtxt);
        }

        private void btn_TopUpD_Click(object sender, RoutedEventArgs e)
        {
            TopUpDWindow topUpDWindow = new TopUpDWindow();
            topUpDWindow.Show();
        }

        private void btn_Log_Click(object sender, RoutedEventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            logWindow.Show();
        }
    }
}
