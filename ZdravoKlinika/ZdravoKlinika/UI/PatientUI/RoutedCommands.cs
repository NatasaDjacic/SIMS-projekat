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
        public static readonly RoutedUICommand Profile = new RoutedUICommand(
           "Profile",
           "Profile",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.F8),
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
            new KeyGesture(Key.F7),
           }
           );

        public static readonly RoutedUICommand Prescriptions = new RoutedUICommand(
           "Prescriptions",
           "Prescriptions",
           typeof(RoutedCommands),
           new InputGestureCollection()
            {
            new KeyGesture(Key.F6),
           }
           );

        public static readonly RoutedUICommand Reports = new RoutedUICommand(
           "Reports",
           "Reports",
           typeof(RoutedCommands),
           new InputGestureCollection()
            {
            new KeyGesture(Key.F5),
           }
           );

        public static readonly RoutedUICommand Theraphy = new RoutedUICommand(
           "Theraphy",
           "Theraphy",
           typeof(RoutedCommands),
           new InputGestureCollection()
           {
                new KeyGesture(Key.F4),
           }
           );

        public static readonly RoutedUICommand Log_Out = new RoutedUICommand(
          "Log_Out",
          "Log_Out",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.F9),
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

        public static readonly RoutedUICommand NewReminder = new RoutedUICommand(
          "NewReminder",
          "NewReminder",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.N, ModifierKeys.Control),
          }
          );


        public static readonly RoutedUICommand Save = new RoutedUICommand(
          "Save",
          "Save",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.S, ModifierKeys.Control),
          }
          );

        public static readonly RoutedUICommand Edit = new RoutedUICommand(
          "Edit",
          "Edit",
          typeof(RoutedCommands),
          new InputGestureCollection()
           {
            new KeyGesture(Key.E, ModifierKeys.Control),
          }
          );

        public static readonly RoutedUICommand Delete = new RoutedUICommand(
         "Delete",
         "Delete",
         typeof(RoutedCommands),
         new InputGestureCollection()
          {
            new KeyGesture(Key.D, ModifierKeys.Control),
         }
         );

        public static readonly RoutedUICommand Print = new RoutedUICommand(
         "Print",
         "Print",
         typeof(RoutedCommands),
         new InputGestureCollection()
          {
            new KeyGesture(Key.P, ModifierKeys.Control),
         }
         );


    }
}
