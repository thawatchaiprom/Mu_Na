using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


using MySql.Data.MySqlClient;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;



using QRCoder;

using System.Drawing;

namespace StockMUNA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            genqr();

                if (!IsPostBack)
                {
                    //set Processing Mode of Report as Local   
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    //set path of the Local report 

                    // ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportQrcode.rdlc");
                    ReportViewer1.LocalReport.ReportEmbeddedResource = "StockMUNA.ReportQrcode.rdlc";

                    /* using (StreamReader rdlcSR = new StreamReader(@"StockMUNA.ReportQrcode.rdlc"))
                     {
                         ReportViewer1.LocalReport.LoadReportDefinition(rdlcSR);
                         ReportViewer1.LocalReport.Refresh();
                     }*/



                    //creating object of DataSet dsmember and filling the DataSet using SQLDataAdapter   
                    DataSetStock dsMember = new DataSetStock();
                    // string ConStr = ConfigurationManager.ConnectionStrings["connection_stock"].ConnectionString;
                    // SqlConnection con = new SqlConnection(ConStr);

                    MySqlConnection cn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_stock"].ConnectionString);

                    cn.Open();
                    MySqlDataAdapter adapt = new MySqlDataAdapter("select stock_code,inv_no from stock_item where stock_code = 'TPS-009-002-001' ", cn);

                    adapt.Fill(dsMember, "DataTableQrcode"); // DataTableQrcode DataSetQrcode

                    cn.Close();

                    //Providing DataSource for the Report   
                    ReportDataSource rds = new ReportDataSource("DataSetQrcode", dsMember.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    //Add ReportDataSource   
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }










        } // end page load




         public void genqr()
        {









            string code = "dddd";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 50;
            imgBarCode.Width = 50;
            System.Web.UI.WebControls.Image Imgqrcode = new System.Web.UI.WebControls.Image();



            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {



                using (MemoryStream ms = new MemoryStream())
                {
                  
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();



                 
                  System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                   img.Save(Server.MapPath("Images/qrcode/") + "Test.Png", System.Drawing.Imaging.ImageFormat.Png);


                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    string result = Convert.ToBase64String(byteImage, 0, byteImage.Length); ;


                    //CreateImage(result.ToString());
                }
                plQRCode.Controls.Add(imgBarCode);
            }

        }




        public string CreateImage(string Byt)
        {
            try
            {
                byte[] data = Convert.FromBase64String(Byt);

                var filename = Convert.ToString(System.Guid.NewGuid()).Substring(0, 5) + Convert.ToString(System.Guid.NewGuid()).Substring(0, 5) + System.DateTime.Now.ToString("FFFFFF") + System.DateTime.Now.Minute + ".png";// +System.DateTime.Now.ToString("fffffffffff") + ".png";
                var file = HttpContext.Current.Server.MapPath("~/Images/qrcode/" + filename);
                //var file = HttpContext.Current.Server.MapPath("~/AppImages/" + filename);


            
                

                System.IO.File.WriteAllBytes(file, data);
                string ImgName = ".../profileimages/" + filename;
                
                return filename;
            }
            catch (Exception e)
            {
                return "Error";
            }

        }







    } // end class

}