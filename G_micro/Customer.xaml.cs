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
using Microsoft.Win32;
using System.IO;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class Customer : Page
    {

        int User;

        public Customer(int user)
        {
            InitializeComponent();

            User = user;
            Fill_Customer_LB();
            Save_Panel.Visibility = System.Windows.Visibility.Collapsed;
            checked_user();
        }

        private void checked_user()
        {

            if (User == 1)
            {
                
            }

            else 
            {
                Search_Grid.RowDefinitions[3].Height = new GridLength(0);
                Search_Grid.RowDefinitions[4].Height = new GridLength(35);


                Customer_services_GRID.RowDefinitions[1].Height = new GridLength(0);
                Customer_services_GRID.RowDefinitions[2].Height = new GridLength(35);


                Customer_Payments_GRID.RowDefinitions[1].Height = new GridLength(0);
                Customer_Payments_GRID.RowDefinitions[2].Height = new GridLength(35);



                Customer_Height_Weight_GRID.RowDefinitions[1].Height = new GridLength(0);
                Customer_Height_Weight_GRID.RowDefinitions[2].Height = new GridLength(35);




            }
        }

        private void Fill_Customer_LB()
        {

            try
            {
                DB db2 = new DB("customers");

                // search by name
                db2.AddCondition("p.per_name", "%" + Name_Search_TB.Text + "%", false, " like ", "per_name");

                // search by mobile
                db2.AddCondition("p.per_mobile", "%" + Phone_Search_TB.Text + "%", false, " like ", "per_mobile");


                db2.Fill(LB, "cus_id", "per_name", @"select * from customers c join persons p 
                                                       on c.cus_per_id=p.per_id ");
            }
            catch
            {

            }

        }

        public static void Get_All_Customers(ComboBox CB, string All = "")
        {

            try
            {
                DB db2 = new DB("customers");

                db2.Fill(CB, "cus_id", "per_name", "select c.cus_id,p.per_name from customers c join persons p on c.cus_per_id=p.per_id ", All);
            }

            catch
            {

            }


        }

       //
        
        public bool Add_Update()
        {

            object person_id_customer = null;

            try
            {

              

                DB DataBase = new DB("persons");

                DataBase.AddColumn("per_name", Name_TB.Text);
                DataBase.AddColumn("per_address", Address_TB.Text, string.IsNullOrWhiteSpace(Address_TB.Text));
                DataBase.AddColumn("per_mobile", Mobile_TB.Text);
                DataBase.AddColumn("per_mail", Email_TB.Text);
                DataBase.AddColumn("per_age", Age_TB.Text);

                // ba3ml insert
                if (LB.SelectedIndex == -1)
                {
                    if (DataBase.IsNotExist("per_id", "per_name", "per_address", "per_mobile", "per_mail", "per_age"))
                    {

                        // Insert Person
                        if (DataBase.Insert())
                        {

                            
                            person_id_customer = DataBase.Last_Inserted;
                            return Confirm.Check(add_update_Customer(person_id_customer, true));
                            


                           
                        }
                        else
                        {
                            return false;
                        }

                    }

                    else
                    {
                        
                        Message.Show("هذا العميل مسجل من قبل", MessageBoxButton.OK, 5);
                        return false;

                    }

                  




                }



// hena ye3ny hwa mawgod ba3mel edit
                else
                {

                    DataBase.AddCondition("per_id", ((DataRowView)LB.SelectedItem)["per_id"]);
                    if (DataBase.Update())
                    {
                        return Confirm.Check(add_update_Customer(((DataRowView)LB.SelectedItem)["per_id"], false));

                    }
                    else
                    {
                        return false;
                    }
                   


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
                Save_Panel.Visibility = System.Windows.Visibility.Visible;
                LB.IsEnabled = false;
                LB.SelectedIndex = -1;
                Form.Set_Style(Main_Grid, Operations.Add);
            }
            catch
            {

            }
        }

        private void EditPanel_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Get_Img();
                    Save_Panel.Visibility = System.Windows.Visibility.Visible;
                    LB.IsEnabled = false;
                    Form.Set_Style(Main_Grid, Operations.Edit);
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
                if (LB.SelectedIndex != -1)
                {

                    if (Message.Show("هل تريد حذف هذا العميل", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("persons");

                        db.AddCondition("per_id", ((DataRowView)LB.SelectedItem)["per_id"]);
                        db.Delete();



                        Fill_Customer_LB();

                    }
                }
            }
            catch
            {

            }
        }



        private void Name_Search_TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Fill_Customer_LB();
        }

        private void Save_Panel_Save(object sender, EventArgs e)
        {
            try
            {

                Form.Set_Style(Main_Grid, Operations.View);

                Save_Panel.Visibility = System.Windows.Visibility.Collapsed;

                LB.IsEnabled = true;
                
                Add_Update();

                int i = LB.SelectedIndex;

                Fill_Customer_LB();
                
                LB.SelectedIndex = i;

                Get_Img();
            }
            catch
            {

            }
        }

        private void Save_Panel_Cancel(object sender, EventArgs e)
        {
            try
            {
                Form.Set_Style(Main_Grid, Operations.View);
                Save_Panel.Visibility = System.Windows.Visibility.Collapsed;
                LB.IsEnabled = true;
            }
            catch
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // EP.Add_IsEnable = App.Is_Granted("collector_Add");
                // EP.Edit_IsEnable = App.Is_Granted("collector_Edit");
                // EP.Delete_IsEnable = App.Is_Granted("collector_Del");


            }
            catch
            {

            }
        }

       

        private void LB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Get_Img();

                Fill_DG_Services();
                Fill_DG_Payments();
                Fill_DG_Height_Weight();

            }
            catch
            {

            }
        }

        #region Services
        private void Fill_DG_Services()
        {
            try
            {

                decimal total = 0;
                DB db = new DB();

                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("date");
                dt.Columns.Add("service");
                dt.Columns.Add("value");
                dt.Columns.Add("paid");
                dt.Columns.Add("REST");


                db.AddCondition("cs_cus_id", LB.SelectedValue);

                DataTable ds = db.SelectTable(@"select *,(cs.cs_value - cs.cs_paid)AS REST from customer_services cs join service s on s.ser_id=cs.cs_ser_id");

                foreach (DataRow row in ds.Rows)
                {
                    total += decimal.Parse(row["cs_paid"].ToString());

                    dt.Rows.Add(row["cs_id"], row["cs_date"], row["ser_name"], row["cs_value"], row["cs_paid"], row["REST"]);
                }

                dt.Rows.Add("","","اجمالى المدفوع","",total,"");


                DB db2 = new DB();

                db2.AddCondition("cs_cus_id", LB.SelectedValue);

                DataRow DR = db2.SelectRow(@"select
                                             COALESCE((sum(cs.cs_value)-(sum(cs.cs_paid))),0)as raseed 
                                            from customer_services cs
                                            join customers c on c.cus_id=cs.cs_cus_id");


                DB db3 = new DB();

                db3.AddCondition("pay_cus_id", LB.SelectedValue);
                DataRow DR2 = db3.SelectRow(@"select COALESCE(sum(p.pay_value),0)as payment 
                                              from payments p ");


                dt.Rows.Add("", "", "الرصيد", "", decimal.Parse(DR["raseed"].ToString()) - decimal.Parse(DR2["payment"].ToString()), "");


                Services_DG.ItemsSource = dt.DefaultView;
                //Services_DG.ItemsSource = db.SelectTableView(@"select *,(cs.cs_value - cs.cs_paid)AS REST from customer_services cs join service s on s.ser_id=cs.cs_ser_id");

            }
            catch
            {

            }
        }

        private void EP_Services_Add(object sender, EventArgs e)
        {
            try
            {
                customer_services c = new customer_services(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Services();
            }
            catch
            {

            }
        }

        private void EP_Services_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    customer_services c = new customer_services(LB.SelectedValue, ((DataRowView)Services_DG.SelectedItem)["cs_id"]);
                    c.ShowDialog();
                    Fill_DG_Services();
                }
            }
            catch
            {

            }
        }

        private void EP_Services_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {

                    if (Message.Show("هل تريد حذف هذه الخدمه", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("customer_services");

                        db.AddCondition("cs_id", ((DataRowView)Services_DG.SelectedItem)["cs_id"]);
                        db.Delete();



                        Fill_DG_Services();

                    }
                }
            }
            catch
            {

            }
        }


        #endregion

        #region Payments
        private void Fill_DG_Payments()
        {
            DB db = new DB();
            try
            {

                db.AddCondition("pay_cus_id", LB.SelectedValue);

                Payment_DG.ItemsSource = db.SelectTableView(@"select * from payments p");

            }
            catch
            {

            }
        }

        private void EP_Payment_Add(object sender, EventArgs e)
        {
            try
            {
                Payment c = new Payment(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Payments();
                Fill_DG_Services();
            }
            catch
            {

            }
        }

        private void EP_Payment_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    Payment c = new Payment(LB.SelectedValue, ((DataRowView)Payment_DG.SelectedItem)["pay_id"]);
                    c.ShowDialog();
                    Fill_DG_Payments();
                    Fill_DG_Services();
                }
            }
            catch
            {

            }
        }

        private void EP_Payment_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {

                    if (Message.Show("هل تريد حذف هذا القسط", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("payments");

                        db.AddCondition("pay_id", ((DataRowView)Payment_DG.SelectedItem)["pay_id"]);
                        db.Delete();



                        Fill_DG_Payments();
                        Fill_DG_Services();

                    }
                }
            }
            catch
            {

            }
        }

        
        #endregion

        #region Height_Weight
        private void Fill_DG_Height_Weight()
        {
            DB db = new DB();
            try
            {

                db.AddCondition("hw_cus_id", LB.SelectedValue);

                Height_Weight_DG.ItemsSource = db.SelectTableView(@"select * from height_weight hw ");

            }
            catch
            {

            }
        }

        private void EP_Height_Add(object sender, EventArgs e)
        {
            try
            {
                width_height c = new width_height(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Height_Weight();
            }
            catch
            {

            }
        }

        private void EP_Height_Edit(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {
                    width_height c = new width_height(LB.SelectedValue, ((DataRowView)Height_Weight_DG.SelectedItem)["hw_id"]);
                    c.ShowDialog();
                    Fill_DG_Height_Weight();
                }
            }
            catch
            {

            }
        }

        private void EP_Height_Delete(object sender, EventArgs e)
        {
            try
            {
                if (LB.SelectedIndex != -1)
                {

                    if (Message.Show("هل تريد الحذف", MessageBoxButton.YesNoCancel, 5) == MessageBoxResult.Yes)
                    {
                        DB db = new DB("height_weight");

                        db.AddCondition("hw_id", ((DataRowView)Height_Weight_DG.SelectedItem)["hw_id"]);
                        db.Delete();



                        Fill_DG_Height_Weight();

                    }
                }
            }
            catch
            {

            }
        }

        #endregion



        #region Image
        private void Img_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                if (!BTNImg.IsMouseOver)
                {
                    BTNImg.Visibility = System.Windows.Visibility.Hidden;
                }
            }
            catch
            {

            }

        }
        private void BTNImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                OpenFileDialog dlg = new OpenFileDialog();
                if ((bool)dlg.ShowDialog())
                {
                    BTNImg.Visibility = System.Windows.Visibility.Hidden;
                    if (!string.IsNullOrEmpty(dlg.FileName))
                    {
                        Img.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(dlg.FileName)));
                    }
                }
            }
            catch
            {

            }


        }
        private void Img_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (!BTNImg.IsMouseOver)
                {
                    BTNImg.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch
            {

            }

        }
        #endregion


        private void Get_Img()
        {
            try
            {
                DB db = new DB("height_weight");

                db.SelectedColumns.Add("hw_img");

                db.AddCondition("hw_cus_id", LB.SelectedValue);

                DataRow DR = db.SelectRow(@"select hw_img from height_weight hw
                                             order by hw.hw_id DESC LIMIT 1");
                if (DR["hw_img"].ToString() != "")
                {
                    //Byte[] data = (Byte[])DR["hw_img"];
                    //MemoryStream strm = new MemoryStream();
                    //strm.Write(data, 0, data.Length);
                    //strm.Position = 0;
                    //System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                    //BitmapImage bi = new BitmapImage();
                    //bi.BeginInit();
                    //MemoryStream ms = new MemoryStream();
                    //img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    //ms.Seek(0, SeekOrigin.Begin);
                    //bi.StreamSource = ms;
                    //bi.EndInit();
                    //Img.Source = bi;


                    Img.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(DR["hw_img"].ToString())));

                }
                else 
                {
                    Img.Source = new BitmapImage(new Uri("/Images/1147438_question_mark_icon.jpg", UriKind.Relative));
                    //Source="/G_micro;component/Images/1147438_question_mark_icon.jpg"
                    //Img.Source = "/G_micro;component/Images/1147438_question_mark_icon.jpg";

                    //Img.Source = null;
                
                
                }
            }
            catch
            {

                Img.Source = new BitmapImage(new Uri("/Images/1147438_question_mark_icon.jpg", UriKind.Relative));
            }
        }


        private bool add_update_Customer(object person,bool add_update)
        {

            object customer_id = null;
            try
            {

                DB DataBase = new DB("customers");


                DataBase.AddColumn("cus_per_id", person );
                DataBase.AddColumn("cus_height", Height_TB.Text);
                DataBase.AddColumn("cus_weight", Weight_TB.Text);

                if (add_update == true)
                {
                    // ADD
                    if (DataBase.Insert())
                    {
                        customer_id = DataBase.Last_Inserted;
                        return add_height_Weight_Customer(customer_id);
                    }
                    else
                    {
                        return false;
                    }
                }
                else 
                {
                  // Update
                    DataBase.AddCondition("cus_id", LB.SelectedValue);

                    if (DataBase.Update())
                    {
                        return update_last_image_in_height_weight(LB.SelectedValue);
                    }
                    else 
                    {
                        return false;
                    }
                    
                }

               

               
            }
            catch
            {
                return false;

            }
        }

        private bool update_last_image_in_height_weight(object customer_id)
        {
            try
            {

                DB db2 = new DB("height_weight");

                db2.SelectedColumns.Add("hw_id");

                db2.AddCondition("hw_cus_id", customer_id);

                DataRow DR = db2.SelectRow(@"select hw_id from height_weight hw
                                             order by hw.hw_id DESC LIMIT 1");


                //DR["hw_img"];

                add_image(DR["hw_id"]);
                
                //db.AddCondition("hw_id", DR["hw_id"]);


              
                return true;
            }
            catch 
            {

                return false;
            }
        }

        private bool add_height_Weight_Customer(object customer)
        {
            try
            {

               // FileStream fs;

                DB DataBase = new DB("height_weight");
                DataBase.AddColumn("hw_cus_id", customer);
               
                DataBase.AddColumn("hw_height", Height_TB.Text);
                DataBase.AddColumn("hw_weight", Weight_TB.Text);

                DateTime thisDay = DateTime.Today;

                DataBase.AddColumn("hw_date", thisDay.Year + "-" + thisDay.Month + "-" + thisDay.Day);

              
                    if (DataBase.IsNotExist("hw_id", "hw_cus_id", "hw_height", "hw_weight"))
                    {

                        if (DataBase.Insert())
                        {
                            if (add_image(DataBase.Last_Inserted))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                      
                    }
                    else
                    {
                        // ye3ny hwa mawgood asln mesh ha3ml 7aga 
                        Message.Show("هذا المستند موجود من قبل", MessageBoxButton.OK, 5);
                        return false;
                        //DataBase.AddCondition("pl_id", this.placeId);
                        //DataBase.Update();

                    }


                
            }
            catch
            {
                return false;
            }
        }





        // ADD Customer From Reception
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Save_Panel.Visibility = System.Windows.Visibility.Visible;
                LB.IsEnabled = false;
                LB.SelectedIndex = -1;
                Form.Set_Style(Main_Grid, Operations.Add);
            }
            catch
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                customer_services c = new customer_services(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Services();
            }
            catch
            {

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                Payment c = new Payment(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Payments();
            }
            catch
            {

            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                width_height c = new width_height(LB.SelectedValue);
                c.ShowDialog();
                Fill_DG_Height_Weight();
            }
            catch
            {

            }
        }


        private bool add_image(object id)
        {
            try
            {
                DB DataBase = new DB("height_weight");
                DataBase.AddCondition("hw_id", id);

                if (!Img.Source.ToString().StartsWith("pack") && !Img.Source.ToString().StartsWith("System"))
                {

                    string path = "D:\\GYM";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string newImagePage = path + "\\" + id + ".jpg";

                    File.Copy(Img.Source.ToString().Remove(0, 8), newImagePage, true);

                    DataBase.AddColumn("hw_img", newImagePage);

                    if (DataBase.Update())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return true;
                }

            }
            catch
            {

                return false;
            }
        }
      


    }
}
