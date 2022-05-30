using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.Converters {
    public class EmployeJMBGConverter : IValueConverter {
        public Dictionary<string,User> Employes { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try { 
                List<string> employesJMBG = (List<string>)value;
                string output = "";
                foreach (string employe in employesJMBG) {
                    User u = Employes[employe];
                    output += ((output==""? "":"\r\n")+ u.firstName + " " + u.lastName);
                }

                return output;
            } catch(Exception ex) {
                return "unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return "ttt";
        }
    }
}
