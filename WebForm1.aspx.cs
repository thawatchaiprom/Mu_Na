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

namespace StockMUNA
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            {

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
                    MySqlDataAdapter adapt = new MySqlDataAdapter("select stock_code,inv_no from stock_item", cn);

                    adapt.Fill(dsMember, "DataTableQrcode"); // DataTableQrcode DataSetQrcode

                    cn.Close();


                    //Providing DataSource for the Report   
                    ReportDataSource rds = new ReportDataSource("DataSetQrcode", dsMember.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    //Add ReportDataSource   
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
               }

            }
    }
}