using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Source;
using System.Windows.Media;


namespace G_micro
{
    /// <summary>
    /// Interaction logic for Doctor.xaml
    /// </summary>
    public partial class Reception : Window
    {
        DoubleAnimation ani;

        Customer Customer_page;

        Outcome_Page_Reception Outcome_page;
       
        SolidColorBrush scb1, scb2;
        public Reception()
        {
            InitializeComponent();
            ani = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 700));
            scb1 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF323B3B"));
            scb2 = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF15B4B4"));
            
            Customer_page = new Customer(2);
            Outcome_page = new Outcome_Page_Reception();
            
            Frame.Navigate(Customer_page);
            Customer_BTN.Background = scb2;
        }

      

       

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                Page page = Frame.Content as Page;
                page.BeginAnimation(Page.OpacityProperty, ani);
            }
            catch
            {

            }
        }

        private void Customer_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer_BTN.Background = scb2;
                Ourcome_BTN.Background = scb1;
                Frame.Navigate(Customer_page);
            }
            catch
            {

            }
        }

        private void Ourcome_BTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer_BTN.Background = scb1;
                Ourcome_BTN.Background = scb2;
                Frame.Navigate(Outcome_page);
            }
            catch
            {

            }
        }

    }
}
