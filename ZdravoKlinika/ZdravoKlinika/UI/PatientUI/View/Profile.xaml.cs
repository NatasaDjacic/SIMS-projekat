using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.Model;
using ZdravoKlinika.Repository;
using ZdravoKlinika.UI.PatientUI.ViewModel;


namespace ZdravoKlinika.UI.PatientUI.View
{
    public partial class Profile : Page
    {
        ProfileVM profileVM;

        public Profile()
        {
            profileVM = new ProfileVM();

            this.DataContext = profileVM;
            
            InitializeComponent();
        }

      

    }
}



