using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_custtypepriceentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@fld_nm", "otlcd"));
            bll.vBindingGridToSp(ref grdprice, "sp_tfield_value_get", arr);
           // dtstart.Text = System.DateTime.Today.ToShortDateString();
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grdprice.Rows)
        {
            Label lbcusttyp = (Label)row.FindControl("lbcusttyp");
            TextBox txunitprice= (TextBox)row.FindControl("txunitprice");
            TextBox dtStart = (TextBox)row.FindControl("dteffective");
            arr.Clear();
            arr.Add(new cArrayList("@cust_typ", lbcusttyp.Text));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@unitprice", txunitprice.Text));
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact( dtStart.Text,"d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vInsertCustomerTypePrice(arr);
            
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Data saved successfully ...');", true);
     }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        string sItem = string.Empty;
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem(arr, ref rs);
        while (rs.Read())
        { 
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " - " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return(lItem.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdprice.Rows)
        {
            Label lbcusttyp = (Label)row.FindControl("lbcusttyp");
            TextBox txunitprice = (TextBox)row.FindControl("txunitprice");
            TextBox dteffective = (TextBox)row.FindControl("dteffective");
            TextBox dtend = (TextBox)row.FindControl("dtend");
            txunitprice.Text = bll.vLookUp("select unitprice from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + lbcusttyp.Text + "'");
            dteffective.Text = bll.vLookUp("SELECT FORMAT( start_dt, 'd/M/yyyy', 'en-US')  from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + lbcusttyp.Text + "'");
            dtend.Text = bll.vLookUp("SELECT FORMAT( end_dt, 'd/M/yyyy', 'en-US')  from tcustomertype_price where item_cd='" + hditem.Value.ToString() + "' and cust_typ='" + lbcusttyp.Text + "'");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //Response.Redirect("fm_report.aspx?src=itemprice2");        
        //bll.vReportcustomertypeprice_trancate();
        bll.vReportcustomertypeprice();        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=pr');", true);
        
    }
}