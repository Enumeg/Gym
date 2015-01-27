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
    public partial class Employee_Absence : Window
    {
        object Absence_Id,Employee_id;


        public Employee_Absence(object employee_id,object absence_id = null)
        {
            InitializeComponent();

            Employee_id = employee_id;
            Absence_Id = absence_id;


            Get_Absence();

        }

        private void Get_Absence()
        {
            try
            {
                DB db2 = new DB("employee_absence");

                    db2.SelectedColumns.Add("*");

                    db2.AddCondition("emc_id", Absence_Id);

                    DataRow DR = db2.SelectRow();

                    Date_TB.Value = DateTime.Parse(DR["emc_date"].ToString());
                    Reason_TB.Text = DR["emc_reason"].ToString();
                   
                

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

                DB DataBase = new DB("employee_absence");

                DataBase.AddColumn("emc_emp_id", Employee_id);
                DataBase.AddColumn("emc_date", Date_TB.Text);
                DataBase.AddColumn("emc_reason", Reason_TB.Text);
               

                if (this.Absence_Id == null)
                {
                    if (DataBase.IsNotExist("emc_id", "emc_emp_id", "emc_date", "emc_reason"))
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
                    DataBase.AddCondition("emc_id", this.Absence_Id);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                return false;
            }
        }


//close class
    }
}
