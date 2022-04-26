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

namespace ZdravoKlinika.UI.ManagerUI
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        public ManagerMainWindow()
        {
            InitializeComponent();
            ContentFrame.Navigate(new View.Rooms());
            
        }

        private void Serbian_Click(object sender, RoutedEventArgs e)
        {

                SerbianMenu.IsChecked = true;
                EnglishMenu.IsChecked = false;
                RussianMenu.IsChecked = false;
        }
        private void English_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = true;
            RussianMenu.IsChecked = false;
        }
        private void Russian_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = false;
            RussianMenu.IsChecked = true;
        }
        private void LightTheme_Click(object sender, RoutedEventArgs e)
        {

            DarkTheme.IsChecked = false;
            LightTheme.IsChecked = true;
        }
        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {

            DarkTheme.IsChecked = true;
            LightTheme.IsChecked = false;
        }
        
    }
}
