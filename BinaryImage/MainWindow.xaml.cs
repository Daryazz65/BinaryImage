using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BinaryImage
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Отображаем изображение
                SelectedImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));

                // Конвертируем в бинарный код
                string binaryData = ConvertImageToBinary(openFileDialog.FileName);
                BinaryOutput.Text = binaryData;
            }
        }

        private string ConvertImageToBinary(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            StringBuilder binaryString = new StringBuilder();

            foreach (byte b in imageBytes)
            {
                binaryString.Append(Convert.ToString(b, 2).PadLeft(8, '0')).Append(" ");
            }

            return binaryString.ToString();
        }

        private void CopyBinary_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BinaryOutput.Text))
            {
                Clipboard.SetText(BinaryOutput.Text);
                MessageBox.Show("Бинарные данные скопированы!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
