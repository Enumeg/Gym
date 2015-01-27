using System.Data;
using System.Windows;
using Source;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for newUser.xaml
    /// </summary>
    public partial class User : Window
    {
        
        object User_Id;
        Edit_Mode Edit_Mode;

        public User(Edit_Mode edit_Mode = Edit_Mode.Add, object id = null)
        {
            InitializeComponent();
            User_Id = id;
            Edit_Mode = edit_Mode;
            Groups.Get_All_Groups(Groups_CB,"المجموعات");
            try
            {
                switch(Edit_Mode)
                {
                    case Edit_Mode.Edit:
                        Main_GD.RowDefinitions[1].Height = Main_GD.RowDefinitions[2].Height = new GridLength(0);
                        New.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case Edit_Mode.Change_Password:
                        New.Visibility = System.Windows.Visibility.Collapsed;
                        Main_GD.RowDefinitions[0].Height = Main_GD.RowDefinitions[3].Height = new GridLength(0);
                        break;
                }
                Get_User();
            }
            catch
            {

            }
        }

        private void Get_User()
        {
            try
            {
                DB db2 = new DB("users");

                db2.SelectedColumns.Add("*");

                db2.AddCondition("user_id", User_Id);

                DataRow DR = db2.SelectRow();

                Name_TB.Text = DR["user_name"].ToString();

                Groups_CB.SelectedValue = DR["user_grp_id"];

            }
            catch
            {

            }
        }

        private void add_update_user_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(Add_Update())
                {
                    if(!(bool)New.IsChecked)
                    {
                        this.Close();
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public bool Add_Update()
        {
            try
            {
                DB DataBase = new DB("users");
                if(Password_TB.Text.Equals(RePassword_TB.Text))
                {
                    if(this.User_Id == null)
                    {
                        DataBase.AddColumn("user_name", Name_TB.Text);
                        DataBase.AddColumn("user_pass", Password_TB.Text.Trim().GetHashCode());
                        DataBase.AddColumn("user_grp_id", Groups_CB.SelectedValue);

                        if(DataBase.IsNotExist("user_id", "user_name"))
                        {
                            return Confirm.Check(DataBase.Insert());
                        }
                        else
                        {
                            Message.Show("هذا الاسم مستخدم من قبل", MessageBoxButton.OK, 5);
                            return false;
                        }
                    }
                    else
                    {
                        switch(Edit_Mode)
                        {
                            case Edit_Mode.Edit:
                                DataBase.AddColumn("user_name", Name_TB.Text);
                                DataBase.AddColumn("user_grp_id", Groups_CB.SelectedValue);
                                break;
                            case Edit_Mode.Change_Password:
                                DataBase.AddColumn("user_pass", Password_TB.Text.Trim().GetHashCode());
                                break;
                        }
                        DataBase.AddCondition("user_id", this.User_Id);
                        return DataBase.Update();
                    }
                }
                else
                {
                    Message.Show("كلمة المرور غير متطابقة", MessageBoxButton.OK, 10);
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        private void Groups_CB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                if(Groups_CB.SelectedIndex == 0) { Groups g = new Groups(); g.ShowDialog(); Groups.Get_All_Groups(Groups_CB, "المجموعات"); }
            }
            catch
            {

            }
        }

    }
}
