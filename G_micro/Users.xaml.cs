using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Source;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();
            Groups.Get_All_Groups(Groups_CB, "الكل");
            Fill_Users_LB();

        }

        private void Fill_Users_LB()
        {

            try
            {
                DB db2 = new DB("Users");

                // search by text
                db2.AddCondition("user_name", Name_TB.Text, false, " like ");

                // search by value
                db2.AddCondition("user_grp_id", Groups_CB.SelectedValue, Groups_CB.SelectedIndex < 1);


                db2.Fill(Users_LB, "user_id", "user_name", @"select * from users");
            }
            catch
            {

            }

        }

        private void EditPanel_Add(object sender, EventArgs e)
        {
            try
            {
                User User = new User();
                User.ShowDialog();
                Fill_Users_LB();
            }
            catch
            {

            }
        }

        private void EditPanel_Edit(object sender, EventArgs e)
        {
            try
            {
                if(Users_LB.SelectedIndex != -1)
                {
                    User User = new User(Edit_Mode.Edit, Users_LB.SelectedValue);
                    User.ShowDialog();
                    Fill_Users_LB();
                }
            }
            catch
            {

            }
        }

        private void EditPanel_Delete(object sender, EventArgs e)
        {
            try
            {
                if(Users_LB.SelectedIndex != -1)
                {

                    if(Message.Show("هل تريد حذف هذا المستخدم", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("users");
                        db.AddCondition("user_id", Users_LB.SelectedValue);
                        db.Delete();
                        Fill_Users_LB();

                    }
                }
            }
            catch
            {

            }
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Fill_Users_LB();
            }
            catch
            {

            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Fill_Users_LB();
            }
            catch
            {

            }
        }

    }
}
