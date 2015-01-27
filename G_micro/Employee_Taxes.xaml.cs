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
    public partial class Employee_Taxes : Window
    {
        object Employee_Id,Tax_Bonus_id;

        // 1 Taxes   2 Bonus
        int Operation_Type = 1; 



        public Employee_Taxes(object employee_id,int type=1,object tax_bonus_id = null)
        {
            InitializeComponent();

            Employee_Id = employee_id;
            Tax_Bonus_id = tax_bonus_id;
            Operation_Type = type;

            Get_Outcome();
            Fill_Tax_bonus_ComboBox();

        }

        private void Get_Outcome()
        {
            try
            {
                DB db2 = new DB("employee_tax_bonus");

                    db2.SelectedColumns.Add("*");

                    db2.AddCondition("emh_id", Tax_Bonus_id);

                    DataRow DR = db2.SelectRow();

                    Date_TB.Value = DateTime.Parse(DR["emh_date"].ToString());
                    Value_TB.Text = DR["emh_value"].ToString();
                    Reason_TB.Text = DR["emh_reason"].ToString();
                    Employee_Tax_bonus_CB.SelectedValue = DR["emh_nm_id"];


            }
            catch
            {
                
               
            }
        }

        private void add_update_outcome_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Add_Update())
                {
                    if ((bool)New.IsChecked)
                    {
                        Value_TB.Text = "";
                        Reason_TB.Text = "";
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

        public bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("employee_tax_bonus");

                DataBase.AddColumn("emh_emp_id", Employee_Id);
                DataBase.AddColumn("emh_date", Date_TB.Text);
                DataBase.AddColumn("emh_reason", Reason_TB.Text);
                DataBase.AddColumn("emh_value", Value_TB.Text);
                DataBase.AddColumn("emh_nm_id", Employee_Tax_bonus_CB.SelectedValue);

                if (this.Tax_Bonus_id == null)
                {
                    if (DataBase.IsNotExist("emh_id", "emh_emp_id", "emh_date", "emh_reason", "emh_value", "emh_nm_id"))
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
                    DataBase.AddCondition("emh_id", this.Tax_Bonus_id);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                return false;
            }
        }


        public void Fill_Tax_bonus_ComboBox()
        {
            try
            {
                DB db2 = new DB("names");


                // Taxes
                if (Operation_Type == 1)
                {


                    db2.Fill(Employee_Tax_bonus_CB, "nm_id", "nm_name", "select * from names n where n.nm_id=1 or n.nm_id=2");

                }
                else  // Boonus 
                {


                    db2.Fill(Employee_Tax_bonus_CB, "nm_id", "nm_name", "select * from names n where n.nm_id=3 or n.nm_id=4");                
                
                }
                
            }
            catch
            {

            }
        }

//close class
    }
}
