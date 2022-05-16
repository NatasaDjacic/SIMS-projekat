using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ZdravoKlinika.UI.PatientUI.View
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand cancelCommand = new CancelCommand(); }
        
    
    
    public class CancelCommand: RoutedUICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public static readonly RoutedUICommand Cancel = new RoutedUICommand(
                   "Cancel",
                   "Cancel",
                   typeof(RoutedCommands),
                   new InputGestureCollection()
                   {
                        new KeyGesture(Key.Enter),
                   }
                   );
    }


    


    
}


