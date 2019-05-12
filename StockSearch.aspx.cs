using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMUNA
{
    public partial class StockSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtStockCode.Text.Trim())
                //|| !String.IsNullOrEmpty(txtinv.Text.Trim())
                || !String.IsNullOrEmpty(ddlGroup.SelectedValue)
                || !String.IsNullOrEmpty(ddlSubCode.SelectedValue))
            {
                Stockcs cs = new Stockcs();
                DataTable dt = cs.getStockSearch(txtStockCode.Text.Trim(),ddlGroup.SelectedValue,ddlSubCode.SelectedValue,"");
                grvData.DataSource = dt;
                grvData.DataBind();
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Stockcs stockCs = new Stockcs();
            ddlSubCode.Enabled = true;
            if (ddlGroup.SelectedValue == "MED")
            {
                //bind stock_med
                if (stockCs != null)
                {
                    ddlSubCode.DataSource = stockCs.getMedStock();

                    ddlSubCode.DataValueField = "med_stock_code";
                    ddlSubCode.DataTextField = "name";
                    ddlSubCode.DataBind();

                   
                }
            }
            else if (ddlGroup.SelectedValue == "ICT")
            {
                //bind stock_ict
                if (stockCs != null)
                {
                    ddlSubCode.DataSource = stockCs.getComStock();
                    ddlSubCode.DataValueField = "com_stock_code";
                    ddlSubCode.DataTextField = "name";
                    ddlSubCode.DataBind();

              
                }

            }
            else if (ddlGroup.SelectedValue == "OFF")
            {
                //bind stock_off
                if (stockCs != null)
                {
                    ddlSubCode.DataSource = stockCs.getOfficeStock(); 
                    ddlSubCode.DataValueField = "office_stock_code";
                    ddlSubCode.DataTextField = "name";
                    ddlSubCode.DataBind();

                }

            }
            else
            {
                ddlSubCode.Items.Clear();
                ddlSubCode.Enabled = false;
                
            }
        }

        protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //*** link edit***//
                HyperLink objLink = (HyperLink)(e.Row.FindControl("hplLink"));
                if (objLink != null)
                {
                    string url = "~/StockEdit.aspx?code=" + DataBinder.Eval(e.Row.DataItem, "stock_code").ToString();
                    objLink.NavigateUrl = url;
                }

                //*** link view ***//
                HyperLink objLinkView = (HyperLink)(e.Row.FindControl("hplLinkView"));
                if (objLink != null)
                {
                    string url = "~/StockItemView.aspx?code=" + DataBinder.Eval(e.Row.DataItem, "stock_code").ToString();
                    objLinkView.NavigateUrl = url;
                }
               

            }
        }
    }
}