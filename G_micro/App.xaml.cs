using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Source;
using System.Globalization;

namespace G_micro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      public static int User_Id;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CultureInfo c = new CultureInfo("ar-EG");
            NumberFormatInfo n = new NumberFormatInfo();
            n.DigitSubstitution = DigitShapes.NativeNational;
            c.NumberFormat = n;
            System.Threading.Thread.CurrentThread.CurrentCulture = c;
            DB.ConnectionString = ConfigurationManager.ConnectionStrings["G_micro.Properties.Settings.Con"];
            try
            {
                DB.OpenConnection();

                
                //Window w = new Reception();  // Reception


               // Window w = new Management();  //Admin


                Window w = new Login();  //Login
                
                w.ShowDialog();
             
            }
            catch
            {

            }

        }
    

    }
}
