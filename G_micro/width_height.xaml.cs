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
using System.IO;
using Microsoft.Win32;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for outcome.xaml
    /// </summary>
    public partial class width_height : Window
    {

        object Payment_Id,Customer;


        public width_height(object customer, object payment_id = null)
        {
            InitializeComponent();

            Payment_Id = payment_id;
            Customer = customer;


          
            if(Payment_Id != null)
            {
                Get_Height_Weight();
            }
        }

        private void Get_Height_Weight()
        {
            try
            {
                DB db2 = new DB("height_weight");

                db2.SelectedColumns.Add("*");

                db2.AddCondition("hw_id", Payment_Id);

                DataRow DR = db2.SelectRow();

                Date_DTP.Value = DateTime.Parse(DR["hw_date"].ToString());

                Height_TB.Text = DR["hw_height"].ToString();
                Weight_TB.Text = DR["hw_weight"].ToString();

                if (DR["hw_img"].ToString() != "")
                {
                    /*
                    Byte[] data = (Byte[])DR["hw_img"];
                    MemoryStream strm = new MemoryStream();
                    strm.Write(data, 0, data.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();
                    Img.Source = bi;
                     * 
                     * */

                    Img.SetValue(System.Windows.Controls.Image.SourceProperty, new BitmapImage(new Uri(DR["hw_img"].ToString())));

                }

            }
            catch
            {


            }
        }

        public bool Add_Update()
        {
            try
            {

                //FileStream fs;

                DB DataBase = new DB("height_weight");
                DataBase.AddColumn("hw_cus_id", Customer);
                DataBase.AddColumn("hw_date", Date_DTP.Value.Value.Date);
                DataBase.AddColumn("hw_height", Height_TB.Text);
                DataBase.AddColumn("hw_weight", Weight_TB.Text);
                
               



                if(this.Payment_Id == null)
                {
                    if (DataBase.IsNotExist("hw_id", "hw_cus_id", "hw_date", "hw_height", "hw_weight"))
                    {
                        if (DataBase.Insert())
                        {
                            if(add_image(DataBase.Last_Inserted))
                            {
                              return Confirm.Check(Update_weight_Height_customer());
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

// hena ye3ny hwa mawgod ba3mel edit
                else
                {
                    DataBase.AddCondition("hw_id", this.Payment_Id);
                    if (DataBase.Update())
                    {

                        if (add_image(this.Payment_Id))
                        {
                            return Confirm.Check(Update_weight_Height_customer());
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
            }
            catch
            {
                return Confirm.Check(false);
            }
        }

        private void add_update_Payment_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if(Add_Update())
                {
                    if((bool)New.IsChecked)
                    {
                        Height_TB.Text = "";
                        Weight_TB.Text = "";
                        Img.Source = new BitmapImage(new Uri("/Images/1147438_question_mark_icon.jpg", UriKind.Relative));

                    }
                    else
                    {
                        this.Close();
                    }
                }


            }
            catch
            {

            }
        }


        private bool Update_weight_Height_customer()
        {

            try
            {
                DB db2 = new DB("height_weight");


                db2.AddCondition("hw_cus_id", Customer);

                DataRow DR = db2.SelectRow(@"select * from height_weight hw
                                             order by hw.hw_date DESC LIMIT 1 ");




                /////////////////////////////////////
                DB DataBase = new DB("customers");
                DataBase.AddColumn("cus_height", DR["hw_height"].ToString());
                DataBase.AddColumn("cus_weight", DR["hw_weight"].ToString());

                DataBase.AddCondition("cus_id", Customer);
                DataBase.Update();
                return true;

            }
            catch
            {
                return false;
                
            }
        }


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
        
        //close class
    }
}
