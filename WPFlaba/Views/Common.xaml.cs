using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFlaba.Models;

namespace WPFlaba.Views
{
    public partial class Common : Window
    {
        PermyakovForWPF dbContext = new PermyakovForWPF();
        User user;
        public Common(User user)
        {
            InitializeComponent();
            this.user = user;
            var images = GetImages();
            if (images.Count > 0)
            {
                ListView.ItemsSource = images;
            }
        }

        private List<Image> GetImages()
        {
            List<Image> list = new List<Image>();
            foreach(Image item in user.Images)           
            {                    
                list.Add(item);
            }
            return list;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();//выбор файлов
                openFileDialog.Filter = "JPG|*.jpg";//фильтр отображения файлов
                BitmapImage image = new BitmapImage();
                if (openFileDialog.ShowDialog() == true)
                {
                    image = new BitmapImage(new Uri(openFileDialog.FileName));//загрузка выбранного файла               
                }
                Image cretaeImage = new Image();
                using (var ms = new MemoryStream())
                {
                    var JpegBitmapEncoder = new JpegBitmapEncoder();
                    JpegBitmapEncoder.Frames.Add(BitmapFrame.Create(image));
                    JpegBitmapEncoder.Save(ms);

                    cretaeImage.Date_added = DateTime.Now;
                    cretaeImage.Image1 = ms.ToArray();
                }
                User bs = dbContext.Users.Single(c => c.Id == user.Id);
                user = null;
                cretaeImage.Users1.Add(user);
                dbContext.Images.Add(cretaeImage);
                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message);
            }
        }
    }
}
