using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Spire.Barcode;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Drawing.Imaging;
using KeepAutomation.Barcode.Bean;
using QRCoder;
using ZXing;
using Spire.Barcode.Implementation.Generator;
using System.Drawing;


namespace StockMUNA
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
               // barcode();
                genqr();


    



            if (!IsPostBack)
                {

                /*  //set Processing Mode of Report as Local   
                  ReportViewer1.ProcessingMode = ProcessingMode.Local;
                  //set path of the Local report 
                  // ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportQrcode.rdlc");
                  ReportViewer1.LocalReport.ReportEmbeddedResource = "StockMUNA.ReportQrcode.rdlc";

                  //creating object of DataSet dsmember and filling the DataSet using SQLDataAdapter 

              DataSetStock dsMember = new DataSetStock();
              MySqlConnection cn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_stock"].ConnectionString);
              cn.Open();
              MySqlDataAdapter adapt = new MySqlDataAdapter("select stock_code,inv_no from stock_item where stock_code = 'TPS-009-002-001' ", cn);
              adapt.Fill(dsMember, "DataTableQrcode"); //DataTableQrcode DataSetQrcode
              cn.Close();

                  //Providing DataSource for the Report   
                  ReportDataSource rds = new ReportDataSource("DataSetQrcode", dsMember.Tables[0]);
                  ReportViewer1.LocalReport.DataSources.Clear();
                  //Add ReportDataSource   
                  ReportViewer1.LocalReport.DataSources.Add(rds);*/


                //set Processing Mode of Report as Local   
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //set path of the Local report 
                // ReportViewer1.LocalReport.ReportPath = Server.MapPath("ReportQrcode.rdlc");
                ReportViewer1.LocalReport.ReportEmbeddedResource = "StockMUNA.ReportQrcode.rdlc";

                DataSetStock dsMember = new DataSetStock();
                MySqlConnection cn = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection_stock"].ConnectionString);
                cn.Open();
                MySqlDataAdapter adapt = new MySqlDataAdapter("select stock_code,inv_no from stock_item  ", cn);
                adapt.Fill(dsMember, "DataTableQrcode"); //DataTableQrcode DataSetQrcode

                // Create and setup an instance of Bytescout Barcode SDK


                // Update DataTable with barcode image
                foreach (DataSetStock.DataTableQrcodeRow row in dsMember.DataTableQrcode.Rows)
                {
               
                    row.test = "ddd";
                }

                //Providing DataSource for the Report   
                ReportDataSource rds = new ReportDataSource("DataSetQrcode", dsMember.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                //Add ReportDataSource   
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.Refresh();

                //////////////////////////////////////////////////////// images path


                //  ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/StockMUNA.ReportQrcode.rdlc");



                ReportViewer1.LocalReport.EnableExternalImages = true;
                string imagePathq = new Uri(Server.MapPath("images/qrcode/Test.png")).AbsoluteUri;
                ReportParameter parameterq = new ReportParameter("ImagePathQ", imagePathq);
                ReportViewer1.LocalReport.SetParameters(parameterq);
                ReportViewer1.LocalReport.Refresh();


               // ReportViewer1.LocalReport.EnableExternalImages = true;
                string imagePath = new Uri(Server.MapPath("images/qrcode/barc22.png")).AbsoluteUri;
                ReportParameter parameter = new ReportParameter("ImagePath3", imagePath);
                ReportViewer1.LocalReport.SetParameters(parameter);
                ReportViewer1.LocalReport.Refresh();


                //////////////////////////////////////////////////////// parameter byte
               
               
                string result;
                var content = "abc-589-cdf-757";
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128
                };
                var bitmap = writer.Write(content);

                System.Drawing.Bitmap b = bitmap;
                byte[] bytes;
                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    b.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    bytes = stream.ToArray();
                    result = Convert.ToBase64String(bytes, 0, bytes.Length); ;
                }


                /////////////////////////////////////////////////////////////////////
                ReportParameter bitmap_bar = new ReportParameter("bitmab_barcode", result);
                ReportViewer1.LocalReport.SetParameters(bitmap_bar);
                ReportViewer1.LocalReport.Refresh();




            } // post back

           /* DataSetStock dsMember2 = new DataSetStock();
            DataTable tableadapt = new DataTable("DataTableQrcode");
            foreach (DataRow dr in tableadapt.Rows) // search whole table
            {
                dr["test"] = "jkb"; //change the name 
                dr["inv_no"] = "5565455"; //change the name 
                
            }
               dsMember2.Tables[0].AcceptChanges();*/






        } //  end page load




         public void genqr()
        {
            string code = "abc-589-cdf-757";
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
                    string result = Convert.ToBase64String(byteImage, 0, byteImage.Length); 


                    //CreateImage(result.ToString());
                }
               // plQRCode.Controls.Add(imgBarCode);
            }

        }





        public void barcode()
        {

            var content = "abc-589-cdf-757";
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128
            };
            var bitmap = writer.Write(content);

            System.Drawing.Bitmap b = bitmap;
            byte[] bytes ;
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes = stream.ToArray();
            }



            // bitmap.Save(Server.MapPath("Images/qrcode/barc22.png"));

        }



    } // end class

}