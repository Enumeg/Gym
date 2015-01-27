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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Source;
using System.Data;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for accounting.xaml
    /// </summary>
    public partial class Accounting : Page
    {
        public Accounting()
        {
            InitializeComponent();
        }
        
        private void From_DTP_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (this.IsLoaded)
                {
                    Fill_Outcome();
                    Fill_Income();
                }
            }
            catch
            {

            }
        }

        #region Income
        private void Fill_Income()
        {


            try
            {

                decimal total = 0;
                DB db = new DB();

                DataTable dt = new DataTable();

            
            dt.Columns.Add("date");
            dt.Columns.Add("customer");
            dt.Columns.Add("value");
            dt.Columns.Add("paid");
            dt.Columns.Add("type");


            db.AddCondition("cs_date", From_DTP.Value.Value.Date, false, ">=", "SD");
            db.AddCondition("cs_date", To_DTP.Value.Value.Date, false, "<=", "ED");

            DataTable ds = db.SelectTable(@"select cs.*,p.per_name,s.ser_name from customer_services cs join customers c on c.cus_id = cs.cs_cus_id
                                                join persons p on p.per_id=c.cus_per_id
                                                join service s on s.ser_id = cs.cs_ser_id");

                foreach (DataRow row in ds.Rows)
                {
                    total += decimal.Parse(row["cs_paid"].ToString());

                    dt.Rows.Add(row["cs_date"], row["per_name"], row["cs_value"], row["cs_paid"], row["ser_name"]);
                }



                DB db2 = new DB();

                db.AddCondition("pay_date", From_DTP.Value.Value.Date, false, ">=", "SD");
                db.AddCondition("pay_date", To_DTP.Value.Value.Date, false, "<=", "ED");

                DataTable ds2 = db2.SelectTable(@"select p.*,pp.per_name from payments p join customers s on s.cus_id=p.pay_cus_id
                                                  join persons pp on pp.per_id = s.cus_per_id");

                foreach (DataRow row in ds2.Rows)
                {
                    total += decimal.Parse(row["pay_value"].ToString());

                    dt.Rows.Add(row["pay_date"], row["per_name"], row["pay_value"], row["pay_value"], "قسط");
                }
            


                
                dt.Rows.Add("","الاجمالى","", total, "");

                Income_DG.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }
     

        #endregion
        #region Outcome
        private void Fill_Outcome()
        {
           
            try
            {

                decimal total2 = 0;
                DB db2 = new DB();

                DataTable dt2 = new DataTable();

                dt2.Columns.Add("id");
                dt2.Columns.Add("date");
                dt2.Columns.Add("value");
                dt2.Columns.Add("type");
                dt2.Columns.Add("description");
               

                db2.AddCondition("out_date", From_DTP.Value.Value.Date, false, ">=", "SD");
                db2.AddCondition("out_date", To_DTP.Value.Value.Date, false, "<=", "ED");

                DataTable ds2 = db2.SelectTable(@"select * from outcome o join outcome_types ot on o.out_ott_id=ot.ott_id where out_date>=@SD and out_date<=@ED");


                foreach (DataRow row2 in ds2.Rows)
                {
                    total2 += decimal.Parse(row2["out_value"].ToString());
                    dt2.Rows.Add(row2["out_id"], row2["out_date"], row2["out_value"], row2["ott_name"], row2["out_description"]);
                }


                dt2.Rows.Add("", "الاجمالى", total2, "");

                Outcome_DG.ItemsSource = dt2.DefaultView;

            }
            catch
            {

            }
        }
        private void Outcome_EP_Add(object sender, EventArgs e)
        {
            try
            {
                Outcome o = new Outcome(1);
                o.ShowDialog();
                Fill_Outcome();
            }
            catch
            {

            }
        }
        private void Outcome_EP_Edit(object sender, EventArgs e)
        {
            try
            {
                Outcome o = new Outcome(1,((DataRowView)Outcome_DG.SelectedItem)["id"]);
                o.ShowDialog();
                Fill_Outcome();
            }
            catch
            {

            }
        }
        private void Outcome_EP_Delete(object sender, EventArgs e)
        {
            try
            {
                if (Message.Show("هل تريد الحذف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                {
                    DB d = new DB("Outcome");
                    d.AddCondition("out_id", ((DataRowView)Outcome_DG.SelectedItem)["id"]);
                    d.Delete();
                    Fill_Outcome();
                }
            }
            catch
            {

            }
        }

        #endregion
    
    
    }
}
