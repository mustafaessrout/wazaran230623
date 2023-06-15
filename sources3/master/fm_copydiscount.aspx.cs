using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_copydiscount : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txproposal.CssClass = "form-control";
            hddisc.Value = ""; hdprop.Value = "";
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> ldisc = new List<string>();
        string sDisc = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@disc_cd", prefixText));
        bll.vSearchDiscountByRemark(arr, ref rs);
        while (rs.Read())
        {
            sDisc = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["disc_cd"].ToString() + "-" + rs["remark"].ToString(), rs["disc_cd"].ToString());
            ldisc.Add(sDisc);
        }
        rs.Close();
        return (ldisc.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        //System.Data.SqlClient.SqlDataReader rs = null;
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@disc_cd", hddisc.Value.ToString()));
        //bll.vGetMstDiscount(arr, ref rs);
        //while (rs.Read())
        //{
        //    //lbstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("dd/MM/yyyy");
        //    //lbdelivery.Text= Convert.ToDateTime(rs["delivery_dt"]).ToString("dd/MM/yyyy");
        //    //lbend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("dd/MM/yyyy");
        //    //lbproposal.Text =  rs["proposal_no"].ToString();
        //    lbfreeitem.Text = ((rs["rditem"].ToString() == "P")?"Item": "Product Group");
        //    lbcust.Text = rs["rdcustomer"].ToString();
        //    lbmethod.Text =  rs["discount_mec"].ToString();
        //    DateTime end_dt = DateTime.ParseExact(lbend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime current_dt = DateTime.Today;
        //    if ((end_dt - current_dt).TotalDays >= 0)
        //    {
        //        dtdelivery_CalendarExtender.StartDate = System.DateTime.ParseExact(lbend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        dtstart_CalendarExtender.StartDate = System.DateTime.ParseExact(lbend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        dtend_CalendarExtender.StartDate = System.DateTime.ParseExact(lbend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    }
        //    else
        //    {
        //        dtdelivery_CalendarExtender.StartDate = System.DateTime.Today;
        //        dtstart_CalendarExtender.StartDate = System.DateTime.Today;
        //        dtend_CalendarExtender.StartDate = System.DateTime.Today;
        //    }
            
        //    dtdelivery.CssClass = cd.csstext;
        //    dtstart.CssClass = cd.csstext;
        //    dtend.CssClass = cd.csstext;
        //}
        //rs.Close();

    }
    protected void btexecute_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (hddisc.Value == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select discount to be duplicated!','select discount','warning');", true);
            return;
        }
        DateTime dt;
        if (dtstart.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select start date!','Start Date','warning');", true);
            return;
        }

        if (dtdelivery.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select delivery date!','Delivery Date','warning');", true);
            return;
        }
        if (dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select end date!','End Date','warning');", true);
            return;
        }

        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please write remark for new discount!','Remark','warning');", true);
            return;
        }
        arr.Add(new cArrayList("@disc_cd", hddisc.Value.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@delivery_dt", System.DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@disc_dt", System.DateTime.Today));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        string sSysno = string.Empty;
        bll.vBatchCopyDicount(ref sSysno, arr);
        lbnewdiscount.Text = sSysno;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New discount created!','"+sSysno+"','success');", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lprop = new List<string>();
        string sProp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vSearchProposalByRemark(arr, ref rs);
        while (rs.Read())
        {
            sProp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() + "-" + rs["bgremark"].ToString(), rs["prop_no"].ToString());
            lprop.Add(sProp);
        }
        rs.Close();
        return (lprop.ToArray());
    }
    protected void btsearchdisc_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@proposal_no", hdprop.Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_discount_getbyproposal2", arr);
        txproposal.CssClass = "form-control ro";
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        Label lbdisccode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
        Label lbstartdate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbstartdate");
        Label lbdeliverydate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdeliverydate");
        Label lbenddate = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbenddate");
        DateTime dtstartdate = DateTime.ParseExact(lbstartdate.Text, "d/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtdeliverydate = DateTime.ParseExact(lbdeliverydate.Text, "d/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtenddate = DateTime.ParseExact(lbenddate.Text, "d/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtcurrent = System.DateTime.Today;
        if (dtenddate > dtcurrent)
        {
            dtstart_CalendarExtender.StartDate = dtenddate.AddDays(1);
            dtdelivery_CalendarExtender.StartDate = dtenddate.AddDays(1);
            dtend_CalendarExtender.StartDate = dtenddate.AddDays(2);
        }
        else
        {
            dtstart_CalendarExtender.StartDate = dtcurrent;
            dtdelivery_CalendarExtender.StartDate = dtcurrent;
            dtend_CalendarExtender.StartDate = dtcurrent.AddDays(1);
        }
        hddisc.Value = lbdisccode.Text;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", hddisc.Value.ToString()));
        bll.vGetMstDiscount(arr, ref rs);
        while (rs.Read())
        {
            lbselecteddiscount.Text = hddisc.Value.ToString();
            lbmethod.Text = rs["discount_mec"].ToString();
            lbcust.Text = rs["rdcustomer"].ToString();
            lbfreeitem.Text = rs["rditem"].ToString();
        }
        rs.Close();
       
        bll.vBindingGridToSp(ref grdsp, "sp_tdiscount_salespoint_get2",arr);
        bll.vBindingGridToSp(ref grdcusgrcd, "sp_tdiscount_cusgrcd_get2", arr);
        bll.vBindingGridToSp(ref grdchannel, "sp_tdiscount_custtype_get2", arr);
        bll.vBindingGridToSp(ref grdcust, "sp_tdiscount_customer_get2", arr);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_copydiscount.aspx");
    }
}