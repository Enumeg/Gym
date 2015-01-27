using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System;
using System.Linq;
using Source;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for Managent.xaml
    /// </summary>
    public partial class Management : Window
    {
        //Data
        Services Services_Window;
        Employees Employees_Page;
        Users Users_Page;
        
        //Accounts
        Accounting Accounting_Page;
        
        //Customers
        Customer Customer_Page;

        string Selected_Button = "";      
        DoubleAnimation ani;
        System.Collections.Generic.List<object> pages;

        public Management()
        {
            InitializeComponent();
            ani = new DoubleAnimation(0, 1, new TimeSpan(0, 0, 0, 0, 700));
            Tab_Panel.Children.Clear();
            pages = new System.Collections.Generic.List<object>();
           
        }
                                 
        private void Set_Selected(Button button)
        {
            try
            {

                if(Selected_Button != "")
                {
                    ((Button)FindName(Selected_Button)).Style = FindResource("Side") as Style;
                    ((Button)FindName("Tab_" + Selected_Button)).Style = FindResource("Closed_Tab") as Style;
                }
                if(button.Name.StartsWith("Tab_"))
                {
                    button.Style = FindResource("Selected_Closed_Tab") as Style;                    
                    Selected_Button = button.Name.Replace("Tab_", "");
                    Button btn1 = ((Button)FindName(button.Name.Replace("Tab_", "")));
                    btn1.Style = FindResource("Selected_Side") as Style;
                }
                else
                {
                    button.Style = FindResource("Selected_Side") as Style;                    
                    Selected_Button = button.Name;
                    Button btn1 = ((Button)FindName("Tab_" + button.Name));
                    if(!Tab_Panel.Children.Contains(btn1)) { Tab_Panel.Children.Add(btn1); }
                    btn1.Style = FindResource("Selected_Closed_Tab") as Style;
                }
            }
            catch
            {

            }
        }

        private void Close_Page(Button btn)
        {
            try
            {

                if(btn.Name.Replace("Tab_", "") == Selected_Button)
                {
                    pages.Remove(Frame.Content);
                    if(Tab_Panel.Children.Count > 0)
                    {
                        Frame.Navigate(pages[int.Parse(Tab_Panel.Tag.ToString()) - 1]);
                        Set_Selected((Button)Tab_Panel.Children[int.Parse(Tab_Panel.Tag.ToString()) - 1]);
                    }
                    else
                    {
                        Frame.Navigate(null);
                        Set_Selected(new Button());
                    }

                }
                else
                {
                    ((Button)FindName(btn.Name.Replace("Tab_", ""))).Style = FindResource("Side") as Style;
                    pages.Remove(pages[int.Parse(Tab_Panel.Tag.ToString())]);
                }
            }
            catch
            {

            }
        }

        private void Frame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                Page page = Frame.Content as Page;
                page.BeginAnimation(Page.OpacityProperty, ani);
                if(!pages.Contains(Frame.Content)) { pages.Add(Frame.Content); }

            }
            catch
            {

            }
        }



        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                  
            }
            catch
            { 
            
            }
        }

        private void Tab_BTN_Services_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               // Button btn = sender as Button;
                //if (btn.Name.StartsWith("Tab_") && !Tab_Panel.Children.Contains(btn))
                //{
                 //   Close_Page(btn);
                //}
                //else if (btn.Name != Selected_Button && btn.Name.Replace("Tab_", "") != Selected_Button)
                //{
                  //  if (Services_Window == null) { Services_Window = new Services(); }
                   // Frame.Navigate(Services_Window);
                   // Set_Selected(btn);
               // }


                Services_Window = new Services();
                Services_Window.ShowDialog();
                
                //Fill_DG_Services();


            }
            catch
            {

            }
        }

        private void Tab_BTN_Employees_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name.StartsWith("Tab_") && !Tab_Panel.Children.Contains(btn))
                {
                    Close_Page(btn);
                }
                else if (btn.Name != Selected_Button && btn.Name.Replace("Tab_", "") != Selected_Button)
                {
                    if (Employees_Page == null) { Employees_Page = new Employees(); }
                    Frame.Navigate(Employees_Page);
                    Set_Selected(btn);
                }
            }
            catch
            {

            }
        }

        private void Tab_BTN_Users_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name.StartsWith("Tab_") && !Tab_Panel.Children.Contains(btn))
                {
                    Close_Page(btn);
                }
                else if (btn.Name != Selected_Button && btn.Name.Replace("Tab_", "") != Selected_Button)
                {
                    if (Users_Page == null) { Users_Page = new Users(); }
                    Frame.Navigate(Users_Page);
                    Set_Selected(btn);
                }
            }
            catch
            {

            }
        }

        private void Tab_BTN_Accounting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name.StartsWith("Tab_") && !Tab_Panel.Children.Contains(btn))
                {
                    Close_Page(btn);
                }
                else if (btn.Name != Selected_Button && btn.Name.Replace("Tab_", "") != Selected_Button)
                {
                    if (Accounting_Page == null) { Accounting_Page = new Accounting(); }
                    Frame.Navigate(Accounting_Page);
                    Set_Selected(btn);
                }
            }
            catch
            {

            }
        }

        private void Tab_BTN_Customers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name.StartsWith("Tab_") && !Tab_Panel.Children.Contains(btn))
                {
                    Close_Page(btn);
                }
                else if (btn.Name != Selected_Button && btn.Name.Replace("Tab_", "") != Selected_Button)
                {
                    if (Customer_Page == null) { Customer_Page = new Customer(1); }
                    Frame.Navigate(Customer_Page);
                    Set_Selected(btn);
                }
            }
            catch
            {

            }
        }

       






    }
}
