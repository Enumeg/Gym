using System;
using System.Windows;
using System.Windows.Controls;
using Source;
using System.Data;


namespace G_micro
{
    /// <summary>
    /// Interaction logic for Groups.xaml
    /// </summary>
    public partial class Groups : Window
    {
        object Group_Id;

        public Groups()
        {
            InitializeComponent();
            Fill_Groups_LB();
        }

        private void Fill_Groups_LB()
        {
            try
            {
                DB db = new DB("groups");
                db.AddCondition("grp_name", Type_TB.Text.Trim(), false, " like ");
                db.SelectedColumns.Add("*");
                db.Fill(LB, "grp_id", "grp_name");
            }
            catch
            {

            }
        }

        public static void Get_All_Groups(ComboBox CB, string All = "")
        {
            try
            {
                DB db = new DB("groups");
                db.SelectedColumns.Add("*");
                db.Fill(CB, "grp_id", "grp_name", "", All);
            }
            catch
            {

            }
        }

        private bool Add_Update()
        {
            try
            {

                DB DataBase = new DB("groups");

                DataBase.AddColumn("grp_name", Type_TB.Text);

                if(Group_Id == null)
                {
                    if(DataBase.IsNotExist("grp_id", "grp_name"))
                    {
                        return Confirm.Check(DataBase.Insert());
                    }
                    else
                    {
                        Message.Show("لقد تم تسجيل هذا النوع من قبل", MessageBoxButton.OK, 5);
                        return false;
                    }


                }
                else
                {
                    DataBase.AddCondition("grp_id", this.Group_Id);
                    return Confirm.Check(DataBase.Update());
                }
            }
            catch
            {
                //MessageBox.Show("kiki_method");
                return false;
            }
        }

        private void Save_Save(object sender, EventArgs e)
        {
            try
            {

                if(Add_Update())
                {
                    if(!(bool)New.IsChecked)
                    {
                        Main_GD.RowDefinitions[1].Height = new GridLength(0);
                    }
                    Fill_Groups_LB();
                    Group_Id = null;
                    Type_TB.Text = "";
                }
            }
            catch
            {
                return;
            }
        }

        private void Save_Cancel(object sender, EventArgs e)
        {
            try
            {
                Main_GD.RowDefinitions[1].Height = new GridLength(0);
                Fill_Groups_LB();
                Group_Id = null;
                Type_TB.Text = "";
            }
            catch
            {

            }
        }

        private void EditPanel_Add(object sender, EventArgs e)
        {
            try
            {
                Group_Id = null;
                Main_GD.RowDefinitions[1].Height = new GridLength(35);
            }
            catch
            {

            }
        }

        private void EditPanel_Edit(object sender, EventArgs e)
        {
            try
            {
                Group_Id = LB.SelectedValue;
                Type_TB.Text = ((DataRowView)LB.SelectedItem)[1].ToString();
                Main_GD.RowDefinitions[1].Height = new GridLength(35);
            }
            catch
            {

            }
        }

        private void EditPanel_Delete(object sender, EventArgs e)
        {

            try
            {
                if(Message.Show("هل تريد حذف هذه المجموعة", MessageBoxButton.YesNoCancel, 10) == MessageBoxResult.Yes)
                {
                    Group_Id = LB.SelectedValue;
                    DB db = new DB("groups");
                    db.AddCondition("grp_id", Group_Id);
                    db.Delete();
                    Fill_Groups_LB();
                }
            }
            catch
            {

            }
        }

        private void Type_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Fill_Groups_LB();
            }
            catch
            {

            }
        }


    }
}
