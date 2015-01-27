using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Source;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Employees : Page
    {


        public Employees()
        {
            InitializeComponent();

            jobs.Get_All_Jobs(Job_CB);

            jobs.Get_All_Jobs(Job_Search_CB, "All");

            Fill_Employees_LB();


        }

        private void Fill_Employees_LB()
        {

            try
            {
                DB db2 = new DB("employees");

                // search by name
                db2.AddCondition("per_name", "%" + Name_Search_TB.Text + "%", false, " like ");

                // search by mobile
                db2.AddCondition("per_mobile", "%" + Mobile_Search_TB.Text + "%", false, " like ");

                // search by Job
                db2.AddCondition("emp_job_id", Job_Search_CB.SelectedValue, Job_Search_CB.SelectedIndex < 1, "=", "emp_job_id");



                db2.Fill(LB, "emp_id", "per_name", @"select * from employees e join persons p
                                                     on e.emp_per_id=p.per_id
                                                     join jobs j on e.emp_job_id=j.job_id");

            }
            catch
            {

            }

        }

        public static void Get_All_employees(ComboBox CB, int job = 0, string All = "")
        {

            try
            {
                DB db2 = new DB("employees");

                db2.AddCondition("emp_job_id", job, job == 0);

                db2.Fill(CB, "emp_id", "per_name", "select emp_id,per_name from employees e join persons p on p.per_id=e.emp_per_id join jobs j on j.job_id=e.emp_job_id", All);
            }

            catch
            {

            }


        }

        public bool Add_Update()
        {

            try
            {

                DB db1 = new DB("persons");

                db1.AddColumn("per_name", Name_TB.Text.Trim());
                db1.AddColumn("per_address", Address_TB.Text.Trim());
                db1.AddColumn("per_mobile", Mobile_TB.Text.Trim());



                DB db2 = new DB("employees");

                db2.AddColumn("emp_job_id", Job_CB.SelectedValue);
                db2.AddColumn("emp_salary", Salary_TB.Text.Trim());
                db2.AddColumn("emp_join_date", Date_TB.Text);


                if(LB.SelectedIndex == -1)
                {
                    db2.AddColumn("emp_per_id", db1);
                    return db2.Execute_Queries(db1, db2);
                }
                else
                {
                    db1.AddCondition("per_id", ((DataRowView)LB.SelectedItem)["per_id"]);
                    db2.AddCondition("emp_id", LB.SelectedValue);


                    return db2.Execute_Queries(db1, db2);
                }


            }
            catch
            {
                return false;
            }
        }

        private void EditPanel_Add(object sender, EventArgs e)
        {
            try
            {
                Form.Set_Style(Main_GD, Operations.Add);
                Main_GD.RowDefinitions[3].Height = new GridLength(35);
                LB.IsEnabled = false;
                LB.SelectedIndex = -1;
            }
            catch
            {

            }
        }

        private void EditPanel_Edit(object sender, EventArgs e)
        {
            try
            {
                Form.Set_Style(Main_GD, Operations.Edit);
                Main_GD.RowDefinitions[3].Height = new GridLength(35);
                LB.IsEnabled = false;
            }
            catch
            {

            }
        }

        private void EditPanel_Delete(object sender, EventArgs e)
        {
            try
            {
                if(LB.SelectedIndex != -1)
                {
                    if(Message.Show("هل تريد حذف هذا الموظف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("persons");

                        db.AddCondition("per_id", ((DataRowView)LB.SelectedItem)["per_id"]);

                        db.Delete();
                        Fill_Employees_LB();

                    }
                }
            }
            catch
            {

            }
        }

        private void Save_Panel_Save(object sender, EventArgs e)
        {
            try
            {

                if(Confirm.Check(Add_Update()))
                {

                    Form.Set_Style(Main_GD, Operations.View);

                    Main_GD.RowDefinitions[3].Height = new GridLength(0);

                    LB.IsEnabled = true;

                    int i = LB.SelectedIndex;


                    Fill_Employees_LB();

                    LB.SelectedIndex = i;
                }
            }

            catch
            {

            }
        }

        private void Save_Panel_Cancel(object sender, EventArgs e)
        {
            try
            {
                Form.Set_Style(Main_GD, Operations.View);
                Main_GD.RowDefinitions[3].Height = new GridLength(0);
                LB.IsEnabled = true;
            }
            catch
            {

            }
        }

        private void Job_Search_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Fill_Employees_LB();
        }
       
        private void Name_Search_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fill_Employees_LB();
        }

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Fill_DG_Absence();
                Fill_DG_Tax();
                Fill_DG_Bonus();
            }
            catch
            {

            }
        }


        private void From_DTP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (this.IsLoaded)
                {
                    Fill_DG_Absence();
                    Fill_DG_Tax();
                    Fill_DG_Bonus();
                }
            }
            catch
            {

            }
        }




        private void Fill_DG_Absence()
        {
            DB db = new DB();
            try
            {

                db.AddCondition("emc_emp_id", LB.SelectedValue);

                db.AddCondition("emc_date", From_DTP.Value.Value.Date, false, ">=", "SD");
                db.AddCondition("emc_date", To_DTP.Value.Value.Date, false, "<=", "ED");

                Absence_DG.ItemsSource = db.SelectTableView(@"select * from employee_absence");

            }
            catch
            {

            }
        }

        private void EP1_Add(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Absence em = new Employee_Absence(LB.SelectedValue);
                    em.ShowDialog();
                    Fill_DG_Absence();
                }
            }
            catch
            {

            }
        }

        private void EP1_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Absence em = new Employee_Absence(LB.SelectedValue, ((DataRowView)Absence_DG.SelectedItem)["emc_id"]);
                    em.ShowDialog();
                    Fill_DG_Absence();
                }
            }
            catch
            {

            }
        }

        private void EP1_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    if (Message.Show("هل تريد الحذف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("employee_absence");
                        db.AddCondition("emc_id", ((DataRowView)Absence_DG.SelectedItem)["emc_id"]);
                        db.Delete();
                        Fill_DG_Absence();

                    }
                }
            }
            catch
            {

            }
        }


        private void Fill_DG_Tax()
        {
            DB db = new DB();
            try
            {

                db.AddCondition("emh_emp_id", LB.SelectedValue);
                db.AddCondition("emh_date", From2_DTP.Value.Value.Date, false, ">=", "SD");
                db.AddCondition("emh_date", To2_DTP.Value.Value.Date, false, "<=", "ED");

                Tax_DG.ItemsSource = db.SelectTableView(@"select * from employee_tax_bonus etb 
                                                            join names n on etb.emh_nm_id=n.nm_id
                                                            where (etb.emh_nm_id=1 or etb.emh_nm_id=2) ");

            }
            catch
            {

            }
        }

        private void EP2_Add(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Taxes em = new Employee_Taxes(LB.SelectedValue,1);
                    em.ShowDialog();
                    Fill_DG_Tax();
                }
            }
            catch
            {

            }
        }

        private void EP2_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Taxes em = new Employee_Taxes(LB.SelectedValue, 1, ((DataRowView)Tax_DG.SelectedItem)["emh_id"]);
                    em.ShowDialog();
                    Fill_DG_Tax();
                }
            }
            catch
            {

            }
        }

        private void EP2_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    if (Message.Show("هل تريد الحذف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("employee_tax_bonus");
                        db.AddCondition("emh_id", ((DataRowView)Tax_DG.SelectedItem)["emh_id"]);
                        db.Delete();
                        Fill_DG_Tax();


                    }
                }
            }
            catch
            {

            }
        }


        private void Fill_DG_Bonus()
        {
            DB db = new DB();
            try
            {

                db.AddCondition("emh_emp_id", LB.SelectedValue);
                db.AddCondition("emh_date", From3_DTP.Value.Value.Date, false, ">=", "SD");
                db.AddCondition("emh_date", To3_DTP.Value.Value.Date, false, "<=", "ED");

                Bonus_DG.ItemsSource = db.SelectTableView(@"select * from employee_tax_bonus etb 
                                                            join names n on etb.emh_nm_id=n.nm_id
                                                            where (etb.emh_nm_id=3 or etb.emh_nm_id=4)");

            }
            catch
            {

            }
        }

        private void EP3_Add(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Taxes em = new Employee_Taxes(LB.SelectedValue, 2);
                    em.ShowDialog();
                    Fill_DG_Bonus();
                }
            }
            catch
            {

            }
        }

        private void EP3_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Employee_Taxes em = new Employee_Taxes(LB.SelectedValue, 2, ((DataRowView)Bonus_DG.SelectedItem)["emh_id"]);
                    em.ShowDialog();
                    Fill_DG_Bonus();
                }
            }
            catch
            {

            }
        }

        private void EP3_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    if (Message.Show("هل تريد الحذف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {

                        DB db = new DB("employee_tax_bonus");
                        db.AddCondition("emh_id", ((DataRowView)Bonus_DG.SelectedItem)["emh_id"]);
                        db.Delete();
                        Fill_DG_Bonus();
                    }
                }
            }
            catch
            {

            }
        }





    }
}
