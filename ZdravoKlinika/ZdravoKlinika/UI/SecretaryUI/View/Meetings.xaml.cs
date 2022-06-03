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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKlinika.UI.SecretaryUI.ViewModel;

namespace ZdravoKlinika.UI.SecretaryUI.View {
    /// <summary>
    /// Interaction logic for Meetings.xaml
    /// </summary>
    public partial class Meetings : Page {
        public Meetings() {
            var sw = Application.Current.Windows
            .Cast<Window>()
            .FirstOrDefault(window => window is SecretaryMainWindow) as SecretaryMainWindow;
            
            this.DataContext = new MeetingsVM(sw.ContentFrame.NavigationService);
            InitializeComponent();
        }
    }
}
