using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_adjustmentprice_booking : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
                bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
                cbsalespoint.CssClass = "divhid";
                txcust.CssClass = "divhid";
                cbgroup.CssClass = "divhid";
                dtbooking.Text = System.DateTime.Today.ToString("d/M/yyyy");
                dtbooking.CssClass = "form-control input-sm ro";
                vSalespoint.Attributes.Add("style", "display:none");

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_adjustmentprice_booking");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetListProposal(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vSearchMstProposalAll(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString(), rs["prop_no"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        List<string> lItem = new List<string>();
        string sitem = string.Empty;
        HttpCookie cook = HttpContext.Current.Request.Cookies["sp"];
        List<cArrayList> arr = new List<cArrayList>();
        cbll bll = new cbll();
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        System.Data.SqlClient.SqlDataReader rs = null;
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sitem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(),rs["item_cd"].ToString());
            lItem.Add(sitem);
        } rs.Close();
        return lItem.ToArray();
    }
    protected void rdcust_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            hditem.Value = "";
            txitem.Text = "";
            txvalue.Text = "";
            txcust.Text = "";
            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtend.Text = System.DateTime.Today.ToString("d/M/yyyy");
            if (rdcust.SelectedValue.ToString() == "C")
            {
                txcust.CssClass = "divnormal";
                cbgroup.CssClass = "divhid";
                cbtype.CssClass = "divhid";
                cbsalespoint.CssClass = "divnormal";
                grd.Visible = false;
                grdcust.Visible = true;
                vSalespoint.Attributes.Remove("style");
            }
            else if (rdcust.SelectedValue.ToString() == "G")
            {
                txcust.CssClass = "divhid";
                cbgroup.CssClass = "divnormal";
                cbsalespoint.CssClass = "divhid";
                cbtype.CssClass = "divhid";
                bll.vBindingFieldValueToCombo(ref cbgroup, "cusgrcd");
                cbgroup_SelectedIndexChanged(sender, e);
                grd.Visible = true;
                grdcust.Visible = false;
                vSalespoint.Attributes.Add("style", "display:none");
            }
            else
            {
                txcust.CssClass = "divhid";
                cbgroup.CssClass = "divhid";
                cbsalespoint.CssClass = "divnormal";
                cbtype.CssClass = "divnormal";
                bll.vBindingFieldValueToCombo(ref cbtype, "otlcd");
                cbtype_SelectedIndexChanged(sender, e);
                grd.Visible = false;
                grdcust.Visible = true;
                vSalespoint.Attributes.Remove("style");
            }
            Button1_Click(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_adjustmentprice_booking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdcust.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cust / Group Cust must be select','Choose Customer / Customer Group','warning');", true);
                return;
            }
            if (dtstart.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Start Active Date Price must be choosen','Actvated Date','warning');", true);
                return;
            }
            if (dtend.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('End Active Date Price must be choosen','End Actvated Date','warning');", true);
                return;
            }

            DateTime dt; DateTime dt2;
            //if (!DateTime.TryParse(dtstart.Text, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out dt))
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Wrong date for activated','Activated date','warning');", true);
            //    return;
            //}
            dt = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dt2 = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            if (dt <= System.DateTime.Today)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Activated date can not less or same than today ','Activated Date','warning');", true);
                return;
            }

            if (dt >= dt2)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('End date can not less or same than Start Date ','Start to End date','warning');", true);
                return;
            }


            double dValue;
            if (!double.TryParse(txvalue.Text, out dValue))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Adjustment Value must numeric ','Adjustment Value','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();
            if (rdcust.SelectedValue.ToString() == "G")
            {
                arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue.ToString()));
            }
            else if (rdcust.SelectedValue.ToString() == "C")
            {
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@otlcd", cbtype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            }
            arr.Add(new cArrayList("@item_cd", hditem.Value));
            arr.Add(new cArrayList("@adjust_value", dValue));
            arr.Add(new cArrayList("@booking_dt", System.DateTime.ParseExact(dtbooking.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@prop_no", null));//hdprop.Value));
            if (rdcust.SelectedValue.ToString() == "G")
            {
                bll.vInsertAdjustmentBookingCusGrCd(arr);
            }
            else if (rdcust.SelectedValue.ToString() == "C")
            {
                bll.vInsertAdjustmentBookingCustomer(arr);
            }
            else
            {
                bll.vInsertAdjustmentBookingCustomerType(arr);
            }

            Button1_Click(sender, e);
            hditem.Value = "";
            txitem.Text = "";
            txcust.Text = "";
            txvalue.Text = "";
            dtstart.Text = System.DateTime.Today.ToString("d/M/yyyy");
            dtend.Text = System.DateTime.Today.ToString("d/M/yyyy");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New item adjustment price booking has been successfull add !','New Adjustment Price Booking','info');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_adjustmentprice_booking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdcust.SelectedValue.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please select by Cust/Group Cust','Customer / Group Customer selection','warning');", true);
                hditem.Value = "";
                txitem.Text = "";
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            if (rdcust.SelectedValue.ToString() == "G")
            {
                arr.Add(new cArrayList("@cusgrcd", cbgroup.SelectedValue));
                if (hditem.Value.ToString() != "")
                {
                    arr.Add(new cArrayList("@item_cd", hditem.Value));
                }

                bll.vBindingGridToSp(ref grd, "sp_tadjustmentprice_booking_cusgrcd_get", arr);
            }
            else if (rdcust.SelectedValue.ToString() == "C")
            {
                if (hditem.Value.ToString() != "")
                {
                    arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
                }
                if (hdcust.Value.ToString() != "")
                {
                    arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                    //bll.vBindingGridToSp(ref grd, "sp_tadjustment_customer_get", arr);
                }
                bll.vBindingGridToSp(ref grdcust, "sp_tadjustmentprice_booking_customer_get", arr);
            }
            else
            {
                if (hditem.Value.ToString() != "")
                {
                    arr.Add(new cArrayList("@item_cd", hditem.Value.ToString()));
                }
                arr.Add(new cArrayList("@otlcd", cbtype.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grdcust, "sp_tadjustmentprice_booking_customertype_get", arr);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_adjustmentprice_booking");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button1_Click(sender, e);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
        Label lbcustcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcustcode");
        Label lbitemname = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemname");
        Label lbadjust = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbadjust");
        Label lbstartdate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbstartdate");
        hditem.Value = lbitemcode.Text;
        txitem.Text = lbitemname.Text;
        txvalue.Text = lbadjust.Text;
        cbgroup.SelectedValue = lbcustcode.Text;
        dtstart.Text = lbstartdate.Text;
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", contextKey.ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcust_AutoCompleteExtender.ContextKey = cbsalespoint.SelectedValue.ToString();
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Button1_Click(sender, e);
    }
}