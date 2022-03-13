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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        string path = "accounts.txt";
        string logtxt = "log.txt";
        string NameM = "Менеджер";

        public AddClientWindow()
        {
            InitializeComponent();
        }

        public void Log(string Event, string Name, string path)
        {
            string events = $"{DateTime.Now.ToShortTimeString()} {Name} {Event}\n";

            File.AppendAllText(path, events);
        }

        private void btn_Add_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int ID = Convert.ToInt32(add_ID.Text);
                long Number = Convert.ToInt64(add_Number.Text);
                int Amount = 0;
                string Status = "открыт";
                string Type = add_Type.Text;

                if (Number < 423706209800000) throw new IndexOutOfRangeException();

                IAddAccount add = new Account<long, int>();
                Account<long, int> newAccount = new Account<long, int>(ID, Number, Amount, Status, Type);

                add.AddAccount(newAccount, path);

                string Event = $"открыл новый счёт {add_Number.Text}";
                SaveLog saveLog = Log;
                saveLog(Event, NameM, logtxt);
            }
            catch (Exception s)
            {
                string message = $"Ошибка >> {s.Message}";
                MessageBox.Show(message);
            }
        }
    }
}
