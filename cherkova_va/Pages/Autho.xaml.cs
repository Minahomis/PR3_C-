using cherkova_va.Model;
using cherkova_va.Services;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace cherkova_va.Pages
{
    /// <summary>
    /// Interaction logic for Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        int click;
        private DispatcherTimer signinTimer = new DispatcherTimer();
        private const int initalTime = 10;
        private int secLeft = initalTime;

        public Autho()
        {
            InitializeComponent();
            click = 0;
            signinTimer.Tick += timer_Tick;
            txtBlockCaptha.TextDecorations = TextDecorations.Strikethrough;

            InitializeComponent();

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            tblTimer.Text = $"Вы сможете снова зайти через: {secLeft} сек.";
            secLeft--;
   
            if (secLeft == 0)
            {
                btnEnter.IsEnabled = true;
                tblTimer.Visibility = Visibility.Hidden;
            }
        }
        private readonly TimeSpan workStart = new TimeSpan(10, 0, 0);
        private readonly TimeSpan workEnd = new TimeSpan(19, 0, 0);

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = txtbLogin.Text.Trim();
            string password = paswdPassword.Password.Trim();
            string hashPassword = Hash.HashPassword(password);


            hh2Entities db = hh2Entities.GetContext();
            //ЕСЛИ ЧЕ, ТУТ МОЖЕТ БЫТЬ ПРИКОЛ С login, а нет не должно быть
            var user = db.Users.Where(x => x.login == login && x.password == hashPassword).FirstOrDefault();
            if (click == 1)
            {
                if (user != null)
                {
                    string roleUs;
                    roleUs = (user.role == 1)?("Клиент") : ("Не клиент");
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    if (!(now >= workStart) && (now <= workEnd))
                        UnavailableAccount();
                    else
                    {
                        LoadPage((int)user.role, user);
                        MessageBox.Show("Вы вошли под: " + user.role.ToString());
                    }
                    //MessageBox.Show("Вы вошли под: " + roleUs);
                    //LoadPage((int)user.role, user);
                }
                else
                {
                    MessageBox.Show("Вы ввелиj логин или пароль неверно!");
                    GenerateCapctcha();
                }
            }
            else if (click < 3)
            {
                if (user != null && txtboxCaptha.Text == txtBlockCaptha.Text)
                {
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    if (!(now >= workStart) && (now <= workEnd))
                        UnavailableAccount();
                    else
                    {
                        LoadPage((int)user.role, user);
                        MessageBox.Show("Вы вошли под: " + user.role.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Введите данные заново!");
                    GenerateCapctcha();
                }
            }
            else
            {
                if (user != null && txtboxCaptha.Text == txtBlockCaptha.Text)
                {
                    TimeSpan now = DateTime.Now.TimeOfDay;
                    if (!(now >= workStart) && (now <= workEnd))
                        UnavailableAccount();
                    else
                        LoadPage((int)user.role, user);
                }
                else
                {
                    btnEnter.IsEnabled = false;
                    tblTimer.Visibility = Visibility.Visible;
                    GenerateCapctcha();

                    secLeft = initalTime;
                    signinTimer.Interval = new TimeSpan(0, 0, 1);
                    signinTimer.Start();
                    timer_Tick(null, null);
                    MessageBox.Show("Слишком много попыток входа");
                }
            }
        }

        private void UnavailableAccount()
        {
            MessageBox.Show("Вне рабочего времени, у Вас нет доступа к аккаунту.");
        }


        private void LoadPage(int roleID, User user)
        {
            click = 0;
            switch (roleID)
            {
                case 1:
                    NavigationService.Navigate(new Client(user, roleID));
                    break;
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GenerateCapctcha()
        {
            txtboxCaptha.Visibility = Visibility.Visible;
            txtBlockCaptha.Visibility = Visibility.Visible;

            string capctchaText = CaptchaGenerator.GenerateCaptchaText(6);
            txtBlockCaptha.Text = capctchaText;
            txtBlockCaptha.TextDecorations = TextDecorations.Strikethrough;
        }

        private void btnEnterGuests_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, 0));
        }
    }
}
