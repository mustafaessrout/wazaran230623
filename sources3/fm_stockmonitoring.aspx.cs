using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockmonitoring : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            try
            {

                List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
                //bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get3", "ID", "ProdName");
                //bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get3", "ID", "ProdName");
                arr.Add(new cArrayList("@datToDate", "SalesJob"));
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBatchStockConfirm(arr);
                bll.vBindingComboToSp(ref cbitem_cdFr, "sp_tmst_item_get4", "ID", "ItemName");
                bll.vBindingComboToSp(ref cbitem_cdTo, "sp_tmst_item_get4", "ID", "ItemName");
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
                if (Request.Cookies["sp"].Value.ToString() == "0")
                {
                    cbsalespoint.Enabled = true;
                }
                else
                {
                    cbsalespoint.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockmonitoring");
                Response.Redirect("fm_loginol.aspx");
            }

        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@cbitem_cdFr", cbitem_cdFr.SelectedValue));
        arr.Add(new cArrayList("@cbitem_cdTo", cbitem_cdTo.SelectedValue));
        arr.Add(new cArrayList("@cbitem_cdFrtx", cbitem_cdFr.SelectedItem.Text));
        arr.Add(new cArrayList("@cbitem_cdTotx", cbitem_cdTo.SelectedItem.Text));   
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));        
        arr.Add(new cArrayList("@so_dt", null));       
        if (cbtype.SelectedValue.ToString() == "0")
        {
            arr.Add(new cArrayList("Salespoint_nm", bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + cbsalespoint.SelectedValue + "'")));
            Session["lParamstkmon"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stkmon');", true);
        }
        else
        {
            Session["lParamstkmon1"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stkmon1');", true);
        }
        
    }
}