using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Library;
using System.IO;

namespace _1
{
    /// <summary>
    /// Логика взаимодействия для TopUpDWindow.xaml
    /// </summary>
    public partial class TopUpDWindow : Window
    {
        string path = "accounts.txt";
        IListOfAccounts account = new Account<long, int>();
        string logtxt = "log.txt";
        string NameM = "Менеджер";

        public TopUpDWindow()
        {
            InitializeComponent();

            List<Account<long, int>> accounts = new List<Account<long, int>>(account.ListOfAccounts(path));

            cb_TopUpDNumber.ItemsSource = accounts;

        }

        public void Log(string Event, string Name, string path)
        {
            string events = $"{DateTime.Now.ToShortTimeString()} {Name} {Event}\n";

            File.AppendAllText(path, events);
        }

        private void btn_TopUpD_Click(object sender, RoutedEventArgs e)
        {
            List<Account<long, int>> accounts = new List<Account<long, int>>(account.ListOfAccounts(path));

            string acc = "";

            for (int i = 0; i < accounts.Count; i++)
            {
                if (cb_TopUpDNumber.SelectedItem.ToString().Contains(Convert.ToString(accounts[i].Number)))
                {
                    accounts[i].Amount += Convert.ToInt32(tb_TopUpDAmount.Text);

                    acc = Convert.ToString(accounts[i].Number);
                }
            }

            string[] accountsList = new string[accounts.Count];

            for (int i = 0; i < accounts.Count; i++)
            {
                accountsList[i] = accounts[i].ToString();
            }

            File.WriteAllLines(path, accountsList);

            string Event = $"пополнил счёт {acc} на {tb_TopUpDAmount.Text}";
            SaveLog saveLog = Log;
            saveLog(Event, NameM, logtxt);
        }
    }
}
