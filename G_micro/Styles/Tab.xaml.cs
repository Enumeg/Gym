using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
namespace G_micro.Styles
{
    partial class Tab : ResourceDictionary
    {
        public Tab()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var f = (TabItem)((FrameworkElement)sender).TemplatedParent;
                ((TabControl)f.Parent).Items.Remove(f);

            }
            catch
            {

            }
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var f = (Button)((FrameworkElement)sender).TemplatedParent;

                WrapPanel wp = (WrapPanel)f.Parent;
                wp.Tag = wp.Children.IndexOf(f);
                wp.Children.Remove(f);
                //f.Visibility = Visibility.Collapsed;
                //f.Tag = "Closed";
            }
            catch
            {

            }
        }
    }
}
