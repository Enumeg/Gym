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
    public partial class jobs : Window
    {

        object TypeId = null;

        public jobs()
        {
            InitializeComponent();
            Fill_Jobs_LB();
        }

        private void Fill_Jobs_LB()
        {

            try
            {
                DB db2 = new DB("jobs");

                // search by name
                db2.AddCondition("job_name", "%" + Name_TB.Text + "%", false, " like ");

                db2.Fill(LB, "job_id", "job_name", "select * from jobs");


            }
            catch
            {

            }
        }

        public static void Get_All_Jobs(ComboBox CB, string All = "")
        {
            try
            {
                DB db2 = new DB("jobs");

                db2.Fill(CB, "job_id", "job_name", "select * from jobs", All);

            }
            catch
            {

            }
        }

        public bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("jobs");

                DataBase.AddColumn("job_name", Name_TB.Text);

                if (this.TypeId == null)
                {
                    if (DataBase.IsNotExist("job_id", "job_name"))
                    {
                        return Confirm.Check(DataBase.Insert());
                    }
                    else
                    {
                        Message.Show("لقد تم تسجيل هذه الوظيفه من قبل", MessageBoxButton.OK, 5);
                        return false;
                    }


                }
                else
                {
                    DataBase.AddCondition("job_id", this.TypeId);
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
                    Fill_Jobs_LB();
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
                if (Message.Show("هل تريد حذف هذه الوظيفه", MessageBoxButton.YesNoCancel, 10) == MessageBoxResult.Yes)
                {
                    TypeId = LB.SelectedValue;
                    DB db = new DB("jobs");
                    db.AddCondition("job_id", TypeId);
                    db.Delete();
                    Fill_Jobs_LB();
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
                Fill_Jobs_LB();
            }
            catch
            {

            }

        }
    }
}
