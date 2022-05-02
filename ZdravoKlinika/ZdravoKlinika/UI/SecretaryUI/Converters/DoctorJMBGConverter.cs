using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.Converters {
    public class DoctorJMBGConverter : IValueConverter {
        List<Doctor> doctors;
        public DoctorJMBGConverter() {
            doctors = GLOBALS.doctorController.GetAll();
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            
            Doctor? f = doctors.Find(d => d.JMBG == value.ToString());
            if (f == null) return "unknown";
            return f.firstName + " " + f.lastName + " - " + f.specialization;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return "ttt";
        }
    }
}
