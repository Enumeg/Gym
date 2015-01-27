using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace Expected_Delivery.Styles
{
    partial class DateTimePicker : ResourceDictionary
    {
        public DateTimePicker()
        {
            InitializeComponent();
        }
        private void Part_Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var f = (Xceed.Wpf.Toolkit.DateTimePicker)((FrameworkElement)sender).TemplatedParent;
                if (e.AddedItems.Count > 0)
                {
                    var newDate = (DateTime?)e.AddedItems[0];

                    if ((f.Value != null) && (newDate != null) && newDate.HasValue)
                        newDate = new DateTime(newDate.Value.Year, newDate.Value.Month, newDate.Value.Day, 0, 0, 0);

                    if (!object.Equals(newDate, f.Value))
                        f.Value = newDate;
                }
                f.IsOpen = false;
            }
            catch
            {

            }
        }

    }
}
