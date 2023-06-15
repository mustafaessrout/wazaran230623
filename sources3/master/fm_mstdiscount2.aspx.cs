using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstdiscount2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@disc_sta_id", "A"));
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);
            bll.vBindingFieldValueToCombo(ref cbstatus, "disc_sta_id");
            cbstatus_SelectedIndexChanged(sender, e);
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                grddisc.Columns[9].Visible = true;
                grddisc.Columns[8].Visible = true;
            }
            else
            {
                grddisc.Columns[9].Visible = false;
                grddisc.Columns[8].Visible = false;
            }
            
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqdiscount3.aspx");
    }

    protected void grddisc_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdisccode = (Label)grddisc.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
        //Response.Redirect("fm_reqdiscount.aspx?dc=" + lbdisccode.Text);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opx", "openwindow('fm_discountinfo.aspx?dc=" + lbdisccode.Text + "');",true);
    }
    protected void grddisc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grddisc_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grddisc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        
        Label lbdisccode = (Label)grddisc.Rows[e.NewEditIndex].FindControl("lbdisccode");
        Label lbexpired = (Label)grddisc.Rows[e.NewEditIndex].FindControl("lbend_dt");
        DateTime timeNow = DateTime.Now;
        DateTime timeEnd = DateTime.ParseExact(lbexpired.Text.ToString(), "d/M/yyyy", null);
        //if (timeEnd >= timeNow)
        //{
            if (bll.nCheckAccess("discedit", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To Edit this Scheme(Discount) contact Administrator !!','warning');", true);
                return;
            }
            Response.Redirect("fm_editdiscount2.aspx?dc=" + lbdisccode.Text);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Sorry, this discount expired.','Discount " + lbdisccode.Text + "','warning');", true);
        //    return;
        //}        
    }

    protected void grddisc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbdisccode = (Label)grddisc.Rows[e.RowIndex].FindControl("lbdisccode");
        Label lbsta_upd = (Label)grddisc.Rows[e.RowIndex].FindControl("lbsta_upd");
        Label lbend_dt = (Label)grddisc.Rows[e.RowIndex].FindControl("lbend_dt");
        Label lbdelivery_dt = (Label)grddisc.Rows[e.RowIndex].FindControl("lbdelivery_dt");
        string sstaupd="";
        if (lbsta_upd.Text == "Activated")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('discountdate.aspx?disccode=" + lbdisccode.Text + "&end_dt=" + lbend_dt.Text + "&delivery_dt="+lbdelivery_dt.Text+"');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "popupwindow('discountdate.aspx?disccode=" + lbdisccode.Text  + "');", true);
            return;
        }
        if (lbsta_upd.Text=="Activated"){sstaupd="A";}else {sstaupd="R";}
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        arr.Add(new cArrayList("@disc_sta_id",sstaupd));
        bll.vUpdateMstDiscount(arr); arr.Clear();

        arr.Add(new cArrayList("@disc_sta_id", "A"));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount "+lbsta_upd.Text+" successfull','Disc No. " + lbdisccode.Text + "' ,'success');", true);
         
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    string confirmValue = Request.Form["confirm_value"];
    //    if (confirmValue == "Yes")
    //    {
    //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
    //    }
    //    else
    //    {
    //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
    //    }
    //}
    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_sta_id", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@disc_sta_id", "A"));
        bll.vRptMstDiscount(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rd');", true);
         
        //if (!chall.Checked)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rd&sta=" + cbstatus.SelectedValue.ToString() + "');", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rd&sta=all');", true);
        //}
    }
    protected void chall_CheckedChanged(object sender, EventArgs e)
    {
        
        if (chall.Checked)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);

        }
        else { cbstatus_SelectedIndexChanged(sender, e); }

        if (Request.Cookies["sp"].Value.ToString() == "0")
        {
            grddisc.Columns[9].Visible = true;
            grddisc.Columns[8].Visible = true;
        }
    }
    protected void grddisc_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        
    }
    protected void grddisc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grddisc.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        if (!chall.Checked)
        {
           // List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@disc_sta_id", cbstatus.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);
        }
        else
        {
            arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);
        }
    }


    protected void grddisc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbsta_upd = (Label)e.Row.FindControl("lbsta_upd");
            LinkButton lb = (LinkButton)e.Row.Cells[9].Controls[0];

            if (lb != null)
            {
                lb.Text = lbsta_upd.Text;
                lb.Attributes.Add("onclick", "javascript:return " +
                "confirm('Are you sure you want to " + lbsta_upd.Text + " this record ')");
               
            }
        }
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_sta_id", "A"));
        arr.Add(new cArrayList("@salespoint", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get_spt", arr);
        cbstatus_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Discount activated successfull','' ,'success');", true);
      
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", prefixText));
        bll.vSearchMstDiscount(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["disc_cd"].ToString() + " | " + rs["proposal_no"].ToString() +" | " + rs["remark"].ToString() + " | " + rs["disc_sta_nm"].ToString(), rs["disc_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@disc_sta_id", "A"));
        arr.Add(new cArrayList("@disc_cd", hddisc.Value.ToString()));
        bll.vBindingGridToSp(ref grddisc, "sp_tmst_discount_get", arr);
    }
}