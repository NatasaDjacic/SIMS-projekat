using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKlinika.Model;
using ZdravoKlinika.Controller;
using ZdravoKlinika.UI.SecretaryUI;
using System.Windows.Navigation;

namespace ZdravoKlinika.UI.PatientUI.ViewModel
{
    public class NotificationsVM:BaseVM
    {

        public RelayCommand NavigateCancel { get; set; }

        public NavigationService navigationService { get; set; }

        NotificationController notificationController = GLOBALS.notificationController;

        public List<Notification> Notifications { get; set; }

        public NotificationsVM(NavigationService navigationService) : base()
        {
            this.navigationService = navigationService;
            this.NavigateCancel = new RelayCommand(Execute_Cancel);
            this.Notifications = notificationController.GetMyNotifications();
        }

        private void Execute_Cancel(object? obj)
        {
            this.navigationService.Navigate(new View.Home());
        }

    }
}
