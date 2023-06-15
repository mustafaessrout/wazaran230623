using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqcustomerlocation : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_get ", "salespointcd", "salespoint_nm");
            cbbranch.Items.Insert(0, new ListItem("All Branch", "ALL"));
            btnSearch_Click(sender, e);
        }
    }

    protected void cbstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
    }

    protected void cbbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearch_Click(sender, e);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdlocation, "sp_tmstcustomer_location_get", arr);
    }

    protected void grdlocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdlocation.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@status", cbstatus.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", cbbranch.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdlocation, "sp_tmstcustomer_location_get", arr);
    }

    protected void grdlocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
        Label lbstatus = (Label)row.FindControl("lbstatus");
        Label lbcustomer = (Label)row.FindControl("lbcustnm");
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName.Equals("View"))
        {
            string[] code = grdlocation.DataKeys[index].Value.ToString().Split('_');
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openwindow('fm_reqcustomerlocation_lookup.aspx?dc=" + code[1] + "&sp="+ code[0] +"');", true);
        }
        else if (e.CommandName.Equals("Approve"))
        {
            if (lbstatus.Text != "Approved")
            {
                List<cArrayList> arr = new List<cArrayList>();
                string[] code = grdlocation.DataKeys[index].Value.ToString().Split('_');
                arr.Add(new cArrayList("@cust_cd", code[1]));
                arr.Add(new cArrayList("@salespointcd", code[0]));
                arr.Add(new cArrayList("@status", "approve"));
                bll.vApprovalCustomerLocation(arr);
                btnSearch_Click(sender, e);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Multiple Location for this customer : "+code[1]+" - "+ lbcustomer.Text+ ",'Approved !!','success');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Already Approved','Multiple Location Customer !!','warning');", true);
                return;
            }
        }
        else if (e.CommandName.Equals("Reject"))
        {
            if (lbstatus.Text != "Approved")
            {
                List<cArrayList> arr = new List<cArrayList>();
                string[] code = grdlocation.DataKeys[index].Value.ToString().Split('_');
                arr.Add(new cArrayList("@cust_cd", code[1]));
                arr.Add(new cArrayList("@salespointcd", code[0]));
                arr.Add(new cArrayList("@status", "reject"));
                bll.vApprovalCustomerLocation(arr);
                btnSearch_Click(sender, e);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Multiple Location for this customer : " + code[1] + " - " + lbcustomer.Text + ",'Rejected !!','success');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Already Approved','Multiple Location Customer !!','warning');", true);
                return;
            }
        }

    }
}