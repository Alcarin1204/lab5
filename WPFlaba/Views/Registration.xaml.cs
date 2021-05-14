using Microsoft.Win32;
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
using System.Windows.Shapes;
using WPFlaba.Models;

namespace WPFlaba.Views
{
    public partial class Registration : Window
    {
        PermyakovForWPF dbContext = new PermyakovForWPF();
        BitmapImage _image;
        public Registration()
        {
            InitializeComponent();
        }

        private void registrButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User addUser = new User
                {
                    Name = nameBox.Text,
                    Surname = sernameBox.Text,
                    Birthday = datePiker.SelectedDate.Value,
                    Email = mailBox.Text,
                    Password = passBox.Password
                };
                if (_image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        var JpegBitmapEncoder = new JpegBitmapEncoder();
                        JpegBitmapEncoder.Frames.Add(BitmapFrame.Create(_image));
                        JpegBitmapEncoder.Save(ms);
                        addUser.Image = new Models.Image()
                        {
                            Date_added = DateTime.Now,
                            Image1 = ms.ToArray()
                        };
                    }
                    addUser.Images.Add(addUser.Image);
                    dbContext.Users.Add(addUser);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, регстрация не завершена! " + ex.Message);
            }
            finally
            {
                this.Close();
            }
        }

        private void loadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();//выбор файлов
            openFileDialog.Filter = "JPG|*.jpg";//фильтр отображения файлов
            if (openFileDialog.ShowDialog() == true)
            {
                _image = new BitmapImage(new Uri(openFileDialog.FileName));//загрузка выбранного файла
                imageBox.Source = _image;
            }
        }
    }
}
