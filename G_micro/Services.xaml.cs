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
    public partial class Services : Window
    {
        object TypeId = null;

        public Services()
        {
            InitializeComponent();
            Fill_Raw_Matrials_LB();

        }
        
        private void Fill_Raw_Matrials_LB()
        {
            try
            {
                DB db = new DB("service");
                db.AddCondition("ser_name", "%" + Name_TB.Text + "%", false, " like ");
                db.AddCondition("ser_value", "%" + Price_TB.Text + "%", false, " like ");
                db.Fill(LB, "ser_id", "ser_name", "select * from service ");
            }
            catch
            {

            }
        }      
        
        private void EP_Add(object sender, EventArgs e)
        {
            try
            {
                TypeId = null;
                Main_GD.RowDefinitions[2].Height = new GridLength(35);
                Name_TB.Text = "";
                Price_TB.Text = "";
            }
            catch
            {

            }
        }
        private void EP_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {

                    TypeId = LB.SelectedValue;

                    DataRowView DR = ((DataRowView)LB.SelectedItem);

                    Name_TB.Text = DR["ser_name"].ToString();
                    Price_TB.Text = DR["ser_value"].ToString();

                    Main_GD.RowDefinitions[2].Height = new GridLength(35);
                }
            }
            catch
            {

            }
        }
        private void EP_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    if (Message.Show("هل تريد حذف هذه الخدمه", MessageBoxButton.YesNoCancel, 10) == MessageBoxResult.Yes)
                    {

                        DB db = new DB("service");
                        db.AddCondition("ser_id", LB.SelectedValue);
                        db.Delete();


                        Name_TB.Text = "";
                        Price_TB.Text = "";
                        Fill_Raw_Matrials_LB();
                    }

                   
                }
            }
            catch
            {

            }
        }
        
        private void add_update_outcome_Click(object sender, RoutedEventArgs e)
        {
            try
            {


               

                if (Notify.validate("من فضلك ادخل اسم الخدمه", Name_TB.Text, this))
                {
                    return;
                }

                if (Notify.validate("من فضلك ادخل السعر", Price_TB.Text, this))
                {
                    return;
                }

                if (Add_Update())
                {
                    
                    

                    Main_GD.RowDefinitions[2].Height = new GridLength(0);

                    // yesafar
                   
                    Name_TB.Text = "";
                    Price_TB.Text = "";


                    Fill_Raw_Matrials_LB();
                }
            }
            catch
            {
                return;
            }
        }
        private bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("service");


                DataBase.AddColumn("ser_name", Name_TB.Text);
                DataBase.AddColumn("ser_value", Price_TB.Text);

                if (TypeId == null)
                {
                    if (DataBase.IsNotExist("ser_id", "ser_name", "ser_value"))
                    {
                        return Confirm.Check(DataBase.Insert());
                    }
                    else
                    {
                        Message.Show("لقد تم تسجيل هذه الخدمه من قبل", MessageBoxButton.OK, 5);
                        return false;
                    }


                }
                else
                {
                    DataBase.AddCondition("ser_id", this.TypeId);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                //MessageBox.Show("kiki_method");
                return false;
            }
        }
        
        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
           /*
                DB db2 = new DB("service");


                db2.AddCondition("ser_id", LB.SelectedValue);

                DataRow DR = db2.SelectRow("select * from service");



                Name_TB.Text = DR["ser_name"].ToString();
                Price_TB.Text = DR["ser_value"].ToString();
            */
            }
            catch
            {

            }

        }

        private void Name_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Fill_Raw_Matrials_LB();
            }
            catch
            {

            }
        }
       

    }
}
