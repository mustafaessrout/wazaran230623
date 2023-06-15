using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_discountlist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@disc_sta_id", "N"));
            bll.vBindingGridToSp(ref grddiscount, "sp_tmst_discount_get",arr);
        }
    }
    protected void grddiscount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddiscount.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_sta_id", "N"));
        bll.vBindingGridToSp(ref grddiscount, "sp_tmst_discount_get", arr);
        Label lbdisccode = (Label)grddiscount.Rows[e.NewEditIndex].FindControl("lbdisccode");  
    }

    protected void grddiscount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddiscount.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_sta_id", "N"));
        bll.vBindingGridToSp(ref grddiscount, "sp_tmst_discount_get", arr);
    }
    protected void grddiscount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbdisccode = (Label)grddiscount.Rows[e.RowIndex].FindControl("lbdisccode");
        TextBox txpropno = (TextBox)grddiscount.Rows[e.RowIndex].FindControl("txpropno");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", lbdisccode.Text));
        arr.Add(new cArrayList("@proposal_no", txpropno.Text));
        arr.Add(new cArrayList("@disc_sta_id", "W"));
        bll.vUpdateMstDiscount(arr);
        Random rnd = new Random();
        string sRnd = rnd.Next(1000,9999).ToString();
        List<string> lInfo = bll.lGetApproval("discount", 2);
       // string sMsg = "New discount no." + lbdisccode.Text + ",proposal : " + txpropno.Text + " has been created. It need your approval , please repply SMS with (Y/N)" + sRnd;
        string sMobileNo = lInfo[0];
       // cd.vSendSms("New Discount No. " + lbdisccode.Text + ", Proposal Number : "  + txpropno.Text +    " : , Please check your email for detail content. Do you approve this ? Please reply (N/Y) " + sRnd , sMobileNo);
        arr.Clear();
        arr.Add(new cArrayList("@doc_typ", "discount"));
        arr.Add(new cArrayList("@token",sRnd));
        arr.Add(new cArrayList("@receiver", sMobileNo));
        arr.Add(new cArrayList("@doc_no", lbdisccode.Text));
        bll.vInsertSMSSent(arr);
        grddiscount.EditIndex = -1;
        arr.Clear();
        arr.Add(new cArrayList("@disc_sta_id", "N"));
        bll.vBindingGridToSp(ref grddiscount, "sp_tmst_discount_get", arr);
    }
    protected void grddiscount_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdisccode = (Label)grddiscount.Rows[e.NewSelectedIndex].FindControl("lbdisccode");
        //Response.Redirect("fm_reqdiscount.aspx?dc=" + lbdisccode.Text);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opx", "openwindow('fm_discountinfo.aspx?dc=" + lbdisccode.Text + "');", true);
    }
}