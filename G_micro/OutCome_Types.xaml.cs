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

namespace G_micro
{
    /// <summary>
    /// Interaction logic for places.xaml
    /// </summary>
    public partial class OutCome_Types : Window
    {

        object TypeId = null;

        public OutCome_Types()
        {
            InitializeComponent();
            Fill_OutCome_Types_LB();
        }

        private void Fill_OutCome_Types_LB()
        {

            try
            {
                //outcome_types
                DB db2 = new DB("outcome_types");

                // search by name
                db2.AddCondition("ott_name", "%" + Name_TB.Text + "%", false, " like ");

                db2.Fill(LB, "ott_id", "ott_name", "select * from outcome_types");


            }
            catch
            {

            }
        }

        public static void Get_All_OutCome_Types(ComboBox CB, string All = "")
        {
            try
            {
                DB db2 = new DB("outcome_types");

                db2.Fill(CB, "ott_id", "ott_name", "select * from outcome_types", All);

            }
            catch
            {

            }
        }

        public bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("outcome_types");

                DataBase.AddColumn("ott_name", Name_TB.Text);

                if (this.TypeId == null)
                {
                    if (DataBase.IsNotExist("ott_id", "ott_name"))
                    {
                        return Confirm.Check(DataBase.Insert());
                    }
                    else
                    {
                        Message.Show("لقد تم تسجيل نوع المصروف من قبل", MessageBoxButton.OK, 5);
                        return false;
                    }


                }
                else
                {
                    DataBase.AddCondition("ott_id", this.TypeId);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                //MessageBox.Show("kiki_method");
                return false;
            }
        }

        private void add_update_outcome_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Add_Update())
                {
                    TypeId = null;
                    Fill_OutCome_Types_LB();
                    if (!(bool)New.IsChecked)
                    {
                        Main_GD.RowDefinitions[1].Height = new GridLength(0);
                    }
                    // yesafar
                    Name_TB.Text = "";
                }
            }
            catch
            {
                return;
            }
        }

        private void EP_Add(object sender, EventArgs e)
        {
            try
            {
                TypeId = null;
                Main_GD.RowDefinitions[1].Height = new GridLength(35);
            }
            catch
            {

            }
        }

        private void EP_Edit(object sender, EventArgs e)
        {
            try
            {


                TypeId = LB.SelectedValue;
                Name_TB.Text = ((DataRowView)LB.SelectedItem)[1].ToString();
                Main_GD.RowDefinitions[1].Height = new GridLength(35);
            }
            catch
            {

            }
        }

        private void EP_Delete(object sender, EventArgs e)
        {
            try
            {
                if(LB.SelectedIndex != -1)
                {
                if (Message.Show("هل تريد حذف هذا المصروف", MessageBoxButton.YesNoCancel, 10) == MessageBoxResult.Yes)
                {
                    TypeId = LB.SelectedValue;
                    DB db = new DB("outcome_types");
                    db.AddCondition("ott_id", TypeId);
                    db.Delete();
                    Fill_OutCome_Types_LB();
                }
                }
            }
            catch
            {

            }
        }


        private void Model_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Fill_OutCome_Types_LB();
            }
            catch
            {

            }

        }
    }
}
