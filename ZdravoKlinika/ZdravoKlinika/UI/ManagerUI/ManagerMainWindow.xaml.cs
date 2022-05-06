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
using ZdravoKlinika.Controller;

namespace ZdravoKlinika.UI.ManagerUI
{
    /// <summary>
    /// Interaction logic for SecretaryMainWindow.xaml
    /// </summary>
    public partial class ManagerMainWindow : Window
    {
        AuthController authController = GLOBALS.authController;
        public ManagerMainWindow()
        {
            InitializeComponent();
            SwitchLanguage("srb");
            //ContentFrame.NavigationService.Navigate(new View.Rooms("srb"));
            //ContentFrame.Navigate(new View.Rooms());

        }

        private void Serbian_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = true;
            EnglishMenu.IsChecked = false;
            RussianMenu.IsChecked = false;


            SwitchLanguage("srb");
        }
        private void English_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = true;
            RussianMenu.IsChecked = false;


            SwitchLanguage("en");
        }
        private void Russian_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = false;
            RussianMenu.IsChecked = true;


            SwitchLanguage("rus");
        }
        private void SwitchLanguage(string languageCode)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (languageCode)
            {
                case "en":
                    Console.WriteLine(ContentFrame.Content.GetType().ToString);

                    ContentFrame.NavigationService.Navigate(new View.Rooms("en"));
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.en.xaml", UriKind.Relative);
                    break;
                case "rus":
                    ContentFrame.NavigationService.Navigate(new View.Rooms("rus"));
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.rus.xaml", UriKind.Relative);
                    break;
                case "srb":
                    ContentFrame.NavigationService.Navigate(new View.Rooms("srb"));
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
                default:
                    ContentFrame.NavigationService.Navigate(new View.Rooms("srb"));
                    dictionary.Source = new Uri("..\\..\\Resource\\Dictionary\\StringResources.xaml", UriKind.Relative);
                    break;
            }
            this.Resources.MergedDictionaries.Add(dictionary);
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
        private void Equip_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Equipments());
        }
        private void Renovation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Renovations("srb"));
        }
        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            authController.Logout();
            var window = new MainWindow();
            this.Hide();
            Application.Current.MainWindow = window;
            window.Show();
            this.Close();

        }
    }
}
