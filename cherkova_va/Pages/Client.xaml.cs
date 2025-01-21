using System.Windows.Controls;
using System.Windows;
using System;
using cherkova_va.Model;
using System.Linq;

namespace cherkova_va.Pages
{
    /// <summary>
    /// Interaction logic for Client.xaml
    /// </summary>
    public partial class Client : Page
    {
        private readonly TimeSpan morningStart = new TimeSpan(10, 0, 0);
        private readonly TimeSpan morningEnd = new TimeSpan(12, 0, 0);

        private readonly TimeSpan dayStart = new TimeSpan(12, 0, 1);
        private readonly TimeSpan dayEnd = new TimeSpan(17, 0, 0);

        private readonly TimeSpan eveningStart = new TimeSpan(17, 0, 1);
        private readonly TimeSpan eveningEnd = new TimeSpan(19, 0, 0);

        public Client(User user, int role)
        {
            InitializeComponent();

            if (user == null)
                tblDayTime.Visibility = tblGreeting.Visibility = Visibility.Hidden;
            else
            {
                tblDayTime.Visibility = tblGreeting.Visibility = Visibility.Visible;
                tblGreeting.Text = $"Mr  {user.login}!";

                TimeSpan now = DateTime.Now.TimeOfDay;
                if ((now >= morningStart) && (now <= morningEnd))
                    tblDayTime.Text = $"Доброе утро,";
                else if ((now >= dayStart) && (now <= dayEnd))
                    tblDayTime.Text = $"Добрый день,";
                else if ((now >= eveningStart) && (now <= eveningEnd))
                    tblDayTime.Text = $"Добрый вечер,";
                
            }
        }
    }
}