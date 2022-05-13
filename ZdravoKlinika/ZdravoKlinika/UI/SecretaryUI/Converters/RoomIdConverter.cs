using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.Converters {
    public class DoctorJMBGConverter : IValueConverter {
        public Dictionary<string,Doctor> Doctors { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try { 
                Doctor f = Doctors[value.ToString()];
                return f.firstName + " " + f.lastName + " - " + f.specialization;
            }catch(Exception ex) {
                return "unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return "ttt";
        }
    }
}
