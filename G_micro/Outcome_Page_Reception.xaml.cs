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
    /// Interaction logic for Outcome_Page_Reception.xaml
    /// </summary>
    public partial class Outcome_Page_Reception : Page
    {
        public Outcome_Page_Reception()
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
                  
                }
            }
            catch
            {

            }
        }


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
     

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Outcome o = new Outcome(2);
                o.ShowDialog();
                Fill_Outcome();
            }
            catch
            {

            }
        }
    //
    }
}
