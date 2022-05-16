using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ZdravoKlinika.UI.PatientUI
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand Home = new RoutedUICommand(
           "Home",
           "Home",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.F1),
           }
           );


        public static readonly RoutedUICommand Appointments = new RoutedUICommand(
            "Appointments",
            "Appointments",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.F3),
            }
            );

        public static readonly RoutedUICommand New_Appointment = new RoutedUICommand(
           "New Appointment",
           "New Appointment",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.F2),
           }
           );

        public static readonly RoutedUICommand Notifications = new RoutedUICommand(
           "Notifications",
           "Notifications",
           typeof(RoutedCommands),
           new InputGestureCollection()
            {
            new KeyGesture(Key.F8),
           }
           );

        public static readonly RoutedUICommand Log_Out = new RoutedUICommand(
          "Log_Out",
          "Log_Out",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.F10),
          }
          );

        public static readonly RoutedUICommand Cancel = new RoutedUICommand(
          "Cancel",
          "Cancel",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.B, ModifierKeys.Control),
          }
          );

        public static readonly RoutedUICommand Find = new RoutedUICommand(
          "Find",
          "Find",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.F, ModifierKeys.Control),
          }
          );

    }
}
