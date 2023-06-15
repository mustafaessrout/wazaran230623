using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_customerpricebooking : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                dtbooking.Text = System.DateTime.Today.ToString("d/M/yyyy");
                dtbooking.CssClass = "form-control input-sm ro";
                dteff.Text = System.DateTime.Today.ToString("d/M/yyyy");
                bll.vBindingFieldValueToCombo(ref cbcusttype, "otlcd");
                arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dteff.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll.vBindingGridToSp(ref grd, "sp_tcustomerprice_booking_get", arr);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customerpricebooking");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sItem = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lItem = new List<string>();
        arr.Add(new cArrayList("@item_nm", prefixText));
        bll.vSearchMstItem2(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + " | " + rs["item_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        } rs.Close();
        return (lItem.ToArray());
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (hditem.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Item Not yet selected','Item','warning');", true);
                return;
            }
            double dPrice;
            if (!double.TryParse(txprice.Text, out dPrice))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Unit price must numeric','Unit price','warning');", true);
                return;
            }
            if (dteff.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select effective date','Effective Date','warning');", true);
                return;
            }

            if (dtend.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please date of end price!','End Price date','success');", true);
                return;
            }
            //DateTime dt = System.DateTime.ParseExact(dteff.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //if (dt < System.DateTime.Today)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Effective date can not less than today!','"+dteff.Text+"','warning');", true);
            //    return;
            //}
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dteff.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cust_typ", cbcusttype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
            arr.Add(new cArrayList("@booking_dt", DateTime.ParseExact(dteff.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@unitprice", txprice.Text));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            bll.vInsertCustomerPriceBooking(arr);
            arr.Clear();
            bll.vBindingGridToSp(ref grd, "sp_tcustomerprice_booking_get");
            txsearchitem.Text = string.Empty;
            hditem.Value = string.Empty;
            txprice.Text = string.Empty;
            lbcurrent.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New price has been setup successfully!','Price Changed','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customerpricebooking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ids", hdids.Value.ToString()));
            bll.vDelCustomerPriceBooking(arr);
            bll.vBindingGridToSp(ref grd, "sp_tcustomerprice_booking_get");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customerpricebooking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprice_Click(object sender, EventArgs e)
    {
        try
        {
            lbcurrent.Text = bll.vLookUp("select isnull(unitprice,0) from tcustomertype_price where cust_typ='" + cbcusttype.SelectedValue.ToString() + "' and item_cd='" + hditem.Value.ToString() + "'");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_customerpricebooking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbcusttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        btprice_Click(sender, e);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=MSTPRICE');", true);
    }
}