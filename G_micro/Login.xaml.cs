using System.Data;
using System.Windows;
using System.Windows.Input;
using Source;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for logIn.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Check_Login()
        {
            try
            {
                //select the user information giving the username 
                DB db = new DB("users");
                db.AddCondition("user_name", User_name_TB.Text.Trim());
                DataRow User = db.SelectRow("select * from users");

                //check if user is exist
                if(User != null)
                {
                    //check if password hashcode match
                    if(Password_TB.Password.GetHashCode().ToString() == User["user_pass"].ToString())
                    {
                        //set global variables
                        App.User_Id = (int)User["user_id"];
                        //start application
                        Window m = new Window();
                        //check privileges
                        switch(int.Parse(User["user_grp_id"].ToString()))
                        {
                            case 1:
                                m = new Management();
                                break;
                            case 2:
                                m = new Reception();
                                break;
                        
                        }
                        //hide Login window and show the selected window
                        this.Hide();
                        m.ShowDialog();
                        //close application on windwos close
                        Application.Current.Shutdown();

                    }
                    else
                    {
                        Message.Show("كلمة المرور غير صحيحة", MessageBoxButton.OK, 10);
                    }
                }
                else
                {
                    Message.Show("إسم المستخدم غير صحيح", MessageBoxButton.OK, 10);
                }
            }
            catch
            {

            }
        }

        private void Log_In_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Chech Login information and open application if ok
                Check_Login();
            }
            catch
            {

            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Change Password
                //select the user information giving the username 
                DB db = new DB("users");
                db.AddCondition("user_name", User_name_TB.Text.Trim());
                DataRow User = db.SelectRow("select * from users");

                //check if user is exist
                if(User != null)
                {
                    //check if password hashcode match
                    if(Password_TB.Password.GetHashCode().ToString() == User["user_pass"].ToString())
                    {
                        //set global variables
                       // App.User_Id = User["User_Id"];
                        //open change password window
                        User u = new User(Edit_Mode.Change_Password, User["User_Id"]);
                        this.Hide();
                        u.ShowDialog();
                        //show Login window
                        this.ShowDialog();
                    }
                    else
                    {
                        Message.Show("كلمة المرور غير صحيحة", MessageBoxButton.OK, 10);
                    }
                }
                else
                {
                    Message.Show("إسم المستخدم غير صحيح", MessageBoxButton.OK, 10);
                }
            }
            catch
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //set input language to arabic (Righ to left)


            }
            catch
            {

            }
        }

    }
}
