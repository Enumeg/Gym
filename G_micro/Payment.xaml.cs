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

namespace G_micro
{
    /// <summary>
    /// Interaction logic for outcome.xaml
    /// </summary>
    public partial class Payment : Window
    {

        object Payment_Id,Customer;
       

        public Payment(object customer, object payment_id = null)
        {
            InitializeComponent();

            Payment_Id = payment_id;
            Customer = customer;


          
            if(Payment_Id != null)
            {
                Get_Payment();
            }
        }

        private void Get_Payment()
        {
            try
            {
                DB db2 = new DB("payments");

                db2.SelectedColumns.Add("*");

                db2.AddCondition("pay_id", Payment_Id);

                DataRow DR = db2.SelectRow();

                Date_DTP.Value = DateTime.Parse(DR["pay_date"].ToString());

                Value_TB.Text = DR["pay_value"].ToString();
              
            }
            catch
            {


            }
        }

        public bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("payments");
                DataBase.AddColumn("pay_cus_id", Customer);
                DataBase.AddColumn("pay_date", Date_DTP.Value.Value.Date);
                DataBase.AddColumn("pay_value", Value_TB.Text.Trim());

                if(this.Payment_Id == null)
                {
                    if (DataBase.IsNotExist("pay_id", "pay_cus_id", "pay_date", "pay_value"))
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
                    DataBase.AddCondition("pay_id", this.Payment_Id);
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

       

        //close class
    }
}
