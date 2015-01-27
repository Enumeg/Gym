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
    public partial class Outcome : Window
    {
        
        object Outcome_Id,Point_ID=1;
        int USER_ID;

        public Outcome(int user,object outcome_id = null)
        {
            InitializeComponent();
            
            Outcome_Id = outcome_id;

            USER_ID = user;

            if (USER_ID == 1)
            {
                OutCome_Types.Get_All_OutCome_Types(Type_CB, "الأنواع");
            }
            else 
            {
                OutCome_Types.Get_All_OutCome_Types(Type_CB);
            }
            if (Outcome_Id != null)
            {
                Get_Outcome();
            }

            

        }

        private void Get_Outcome()
        {
            try
            {
                    DB db2 = new DB("outcome");

                    db2.SelectedColumns.Add("*");

                    db2.AddCondition("out_id", Outcome_Id);

                    DataRow DR = db2.SelectRow();

                    Date_TB.Value = DateTime.Parse(DR["out_date"].ToString());
                    Type_CB.SelectedValue = DR["out_ott_id"];
                    Value_TB.Text = DR["out_value"].ToString();
                    Description_TB.Text = DR["out_description"].ToString();

                    Point_ID = DR["out_pon_id"];



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
                        Description_TB.Text = "";
                        
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

                DB DataBase = new DB("outcome");

                DataBase.AddColumn("out_date", Date_TB.Text);
                DataBase.AddColumn("out_value", Value_TB.Text);
                DataBase.AddColumn("out_description", Description_TB.Text);
                DataBase.AddColumn("out_ott_id", Type_CB.SelectedValue);
                //DataBase.AddColumn("out_pon_id", Point_ID);



                if (this.Outcome_Id == null)
                {
                    if (DataBase.IsNotExist("out_id", "out_date", "out_value", "out_description", "out_ott_id", "out_pon_id"))
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
                    DataBase.AddCondition("out_id", this.Outcome_Id);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                return Confirm.Check(false);
            }
        }

        private void Type_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(Type_CB.SelectedIndex == 0) { OutCome_Types outt = new OutCome_Types(); outt.ShowDialog(); OutCome_Types.Get_All_OutCome_Types(Type_CB, "الأنواع"); }
            }
            catch
            {

            }
        }


//close class
    }
}
