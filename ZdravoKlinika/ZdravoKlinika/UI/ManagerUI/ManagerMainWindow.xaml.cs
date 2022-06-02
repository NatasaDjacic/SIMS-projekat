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
        string global_language; 
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
            global_language = "srb";
        }
        private void English_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = true;
            RussianMenu.IsChecked = false;


            SwitchLanguage("en");
            global_language = "en";

        }
        private void Russian_Click(object sender, RoutedEventArgs e)
        {

            SerbianMenu.IsChecked = false;
            EnglishMenu.IsChecked = false;
            RussianMenu.IsChecked = true;


            SwitchLanguage("rus");
            global_language = "rus";

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
            BlueTheme.IsChecked = false;
            PinkTheme.IsChecked = false;
            Properties.Settings.Default.ColorMode = "Light";
            Properties.Settings.Default.Save();
        }
        private void DarkTheme_Click(object sender, RoutedEventArgs e)
        {

            DarkTheme.IsChecked = true;
            LightTheme.IsChecked = false;
            BlueTheme.IsChecked = false;
            PinkTheme.IsChecked = false;
            Properties.Settings.Default.ColorMode = "Dark";
            Properties.Settings.Default.Save();
        }

        private void BlueTheme_Click(object sender, RoutedEventArgs e)
        {

            DarkTheme.IsChecked = false;
            LightTheme.IsChecked = false;
            BlueTheme.IsChecked = true;
            PinkTheme.IsChecked = false;
            Properties.Settings.Default.ColorMode = "Blue";
            Properties.Settings.Default.Save();
        }

        private void PinkTheme_Click(object sender, RoutedEventArgs e)
        {

            DarkTheme.IsChecked = false;
            LightTheme.IsChecked = false;
            BlueTheme.IsChecked = false;
            PinkTheme.IsChecked = true;
            Properties.Settings.Default.ColorMode = "Pink";
            Properties.Settings.Default.Save();
        }

        private void Equip_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Equipments(global_language));
        }
        private void Renovation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Renovations(global_language));
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
        private void Rooms_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Rooms(global_language));
        }
        private void Drug_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Drugs(global_language));
        }
        private void RoomsMerge_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.RoomsMerge(global_language));
        }
        private void RoomsSeparation_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.RoomsSeparate(global_language));
        }
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Profile(global_language));
        }
        private void Polls_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.NavigationService.Navigate(new View.Polls(global_language));
        }
    }
}
