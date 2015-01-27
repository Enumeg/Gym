using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Source;
using System.Data;
using System.IO;
using Microsoft.Win32;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for outcome.xaml
    /// </summary>
    public partial class customer_services : Window
    {

        object Payment_Id,Customer;


        public customer_services(object customer, object payment_id = null)
        {
            InitializeComponent();

            Payment_Id = payment_id;
            Customer = customer;
            Fill_Services_ComboBox();

          
            if(Payment_Id != null)
            {
                Get_customer_services();
            }
        }


        public void Fill_Services_ComboBox()
        {
            try
            {
                DB db2 = new DB("service");
                db2.Fill(Service_CB, "ser_id", "ser_name", "select * from service");

               

            }
            catch
            {

            }
        }
        
        private void Get_customer_services()
        {
            try
            {
                DB db2 = new DB("customer_services");

                db2.SelectedColumns.Add("*");

                db2.AddCondition("cs_id", Payment_Id);

                DataRow DR = db2.SelectRow();

                Date_DTP.Value = DateTime.Parse(DR["cs_date"].ToString());
                Service_CB.SelectedValue = DR["cs_ser_id"];

                Value_TB.Text = DR["cs_value"].ToString();
                Paid_TB.Text = DR["cs_paid"].ToString();
                Rest_TB.Text = (decimal.Parse(Value_TB.Text) - decimal.Parse(Paid_TB.Text)).ToString("0.00");

              
            }
            catch
            {


            }
        }

        public bool Add_Update()
        {
            try
            {



                DB DataBase = new DB("customer_services");

                DataBase.AddColumn("cs_cus_id", Customer);
                DataBase.AddColumn("cs_date", Date_DTP.Value.Value.Date);
                DataBase.AddColumn("cs_value", Value_TB.Text);
                DataBase.AddColumn("cs_paid", Paid_TB.Text);
                DataBase.AddColumn("cs_ser_id", Service_CB.SelectedValue);
              

                if(this.Payment_Id == null)
                {
                    if (DataBase.IsNotExist("cs_id", "cs_cus_id", "cs_date", "cs_value", "cs_paid", "cs_ser_id"))
                    {

                        return Confirm.Check(DataBase.Insert());
                       
                    }
                    else
                    {
                        // ye3ny hwa mawgood asln mesh ha3ml 7aga 
                        Message.Show("هذا المستند موجود من قبل", MessageBoxButton.OK, 5);
                        return false;
                        //DataBase.AddCondition("pl_id", this.placeId);
                        //DataBase.Update();

                    }


                }

// hena ye3ny hwa mawgod ba3mel edit
                else
                {
                    DataBase.AddCondition("cs_id", this.Payment_Id);
                  
                        return Confirm.Check(DataBase.Update());
                   
                
                }
            }
            catch
            {
                return Confirm.Check(false);
            }
        }

        private void add_update_Payment_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(Add_Update())
                {
                    if((bool)New.IsChecked)
                    {
                        Value_TB.Text = "";
                        Paid_TB.Text = "";
                        Rest_TB.Text = "";
                        Service_CB.SelectedIndex = -1;

                    }
                    else
                    {
                        this.Close();
                    }
                }


            }
            catch
            {

            }
        }


       

        private void Paid_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Rest_TB.Text = (decimal.Parse(Value_TB.Text) - decimal.Parse(Paid_TB.Text)).ToString("0.00");
                
            }
            catch
            {
                
                
            }
        }

        private void Service_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DB db2 = new DB("service");

                db2.SelectedColumns.Add("*");

                db2.AddCondition("ser_id", Service_CB.SelectedValue);

                DataRow DR = db2.SelectRow();

                Value_TB.Text = DR["ser_value"].ToString();

            }
            catch
            {


            }
        }


      
        
        
        //close class
    }
}
