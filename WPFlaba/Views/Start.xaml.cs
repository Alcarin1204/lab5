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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFlaba.Models;

namespace WPFlaba.Views
{
    public partial class Start : Window
    {
        PermyakovForWPF dbContext = new PermyakovForWPF();
        public Start()
        {
            InitializeComponent();
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User accessUser = (User)dbContext.Users.Single(c =>
                c.Email == emailBox.Text && c.Password == passBox.Text);
                Common common = new Common(accessUser);
                this.Hide();
                common.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка входа!" + ex.Message);
            }
        }

        private void registrButton_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.ShowDialog();
            this.Show();
        }
    }
}
