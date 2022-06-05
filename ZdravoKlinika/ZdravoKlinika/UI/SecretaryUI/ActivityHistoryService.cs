using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKlinika.UI.SecretaryUI {
    public class ActivityHistoryService: INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        string[] backgroundColors = new string[]{ "#009E0F", "#00FFFF", "#FF9900", "#351C75", "#FFFF00", "#741B47" };
        string[] foregroundColors = new string[] { "#FFF","#000","#000", "#FFF", "#000", "#FFF" };

        static ActivityHistoryService? instance;

        public static ActivityHistoryService Instance {
            get {
                if(instance == null) instance = new ActivityHistoryService();
                return instance;
            }
        }

        ObservableCollection<Activity> activities;

        public ObservableCollection<Activity> Activities {
            get => activities;
            set {
                activities = value;
                OnPropertyChanged("Activities");
            }
        }

        private ActivityHistoryService() {
            activities = new ObservableCollection<Activity>();
        }

        public void NewActivity(ActivityType type, string title, string description) {
            Activities.Insert(0, new Activity(title, description, backgroundColors[(int)type], foregroundColors[(int)type]));
            if(Activities.Count > 24) {
                Activities.RemoveAt(24);
            }
            OnPropertyChanged("Activities");
        }



    }
    public enum ActivityType {
        PATIENT = 0,
        APPOINTMENT = 1,
        MEETING = 2,
        ORDER_DYNAMIC_EQUIPMENT = 3,
        SICK_DAYS = 4,
        ROOM_REPORT = 5,

    }
    public struct Activity {
        public string title { get; set; }
        public string description { get; set; }
        public string background { get; set; }
        public string foreground { get; set; }
        public Activity(string title, string description, string background, string foreground) {
            this.title = title;
            this.description = description;
            this.background = background;
            this.foreground = foreground;
        }
    }
}
