using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ZdravoKlinika.Model;

namespace ZdravoKlinika.UI.SecretaryUI.Converters {
    public class RoomIdConverter : IValueConverter {
        public Dictionary<string,Room> Rooms { get; set; }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try { 
                Room f = Rooms[value.ToString()];
                return f.name;
            }catch(Exception ex) {
                return "unknown";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            return "ttt";
        }
    }
}
