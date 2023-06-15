using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_custLocationUpdate : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            bll.vBindingComboToSp(ref ddlSalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            ddlSalespoint.Items.Insert(0, new ListItem("Select", "0"));
            ddlSalespoint_SelectedIndexChanged(sender, e);
            ddlSalesman_SelectedIndexChanged(sender, e);
            ddlSalesman.Enabled = false;
            ddlSalesman.SelectedValue.Equals(0);
            ddlSalespoint.SelectedValue.Equals(0);
            txCustomer.Text = "";
        }
    }


    protected void ddlSalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", ddlSalespoint.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref ddlSalesman, "sp_salesman_get", "salesman_cd", "emp", arr);
        ddlSalesman.Items.Insert(0, new ListItem("Select", "0"));
        if (ddlSalespoint.SelectedValue.ToString() == "0")
        {
            ddlSalesman.Enabled = false;
            txCustomer.Enabled = false;
            chkCust.Enabled = false;
        }
        else
        { ddlSalesman.Enabled = true; }
    }
    protected void ddlSalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        txCustomer_AutoCompleteExtender.ContextKey = ddlSalesman.SelectedValue.ToString();
        Response.Cookies["salespointcd"].Value = ddlSalespoint.SelectedValue.ToString();
        txCustomer.Text = "";
        if (ddlSalesman.SelectedValue.ToString() == "0")
        {
            txCustomer.Enabled = false;
            chkCust.Enabled = false;
        }
        else
        {
            txCustomer.Enabled = true;
            chkCust.Enabled = true;    
        }
        if (ddlSalesman.SelectedValue.ToString() != "0")
        { hdsalesman.Value = ddlSalesman.SelectedValue.ToString(); }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        //HttpCookie cok;
        //cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["salespointcd"].Value.ToString()));
        bll.vGetCustomersBySalesman(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_desc"].ToString(), rs["cust_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }

   
    protected void chkCust_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCust.Checked)
        {
            txCustomer.Enabled = false;
        }
        else
        {
            txCustomer.Enabled = true;
        }
        txCustomer.Text = "";
    }

    protected void txCustomer_TextChanged(object sender, EventArgs e)
    {
        if (txCustomer.Text != "" && txCustomer.Text.Length > 4)
        {
            hdcust.Value = txCustomer.Text.Substring(0, 4);
            hdcust.Value = hdcust.Value.TrimEnd('-');
            hdcust.Value = hdcust.Value.TrimEnd(' ');
        }

    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {
        if (ddlSalespoint.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Branch is not Selected!','Please Select a Branch!!','warning');", true);
            return;
        }

        if (ddlSalesman.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman is not Selected!','Please Select a Salesman!!','warning');", true);
            return;
        }

        if (chkCust.Checked == false && (txCustomer.Text == ""|| txCustomer.Text.Length < 4))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer is not Selected!','Please Select a Customer!!','warning');", true);
            return;
        }

         if (chkCust.Checked)
         {
             List<cArrayList> arr = new List<cArrayList>();
             arr.Add(new cArrayList("@salespointcd", ddlSalespoint.SelectedValue.ToString()));
             arr.Add(new cArrayList("@salesman_cd", ddlSalesman.SelectedValue.ToString()));
             bll.vBindingGridToSp(ref grd, "sp_salesman_customers_get", arr);
         }
         if (chkCust.Checked == false)
         {
             List<cArrayList> arr = new List<cArrayList>();
             arr.Add(new cArrayList("@salespointcd", ddlSalespoint.SelectedValue.ToString()));
             arr.Add(new cArrayList("@salesman_cd", ddlSalesman.SelectedValue.ToString()));
             arr.Add(new cArrayList("@cust_cd", hdcust.Value));
             bll.vBindingGridToSp(ref grd, "sp_salesman_customer_get", arr);
         }

    }



    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label hdlblcust = (Label)grd.Rows[e.NewSelectedIndex].FindControl("cust_cd");
        Label hdlblong = (Label)grd.Rows[e.NewSelectedIndex].FindControl("custlongitude");
        hdcust.Value = Convert.ToString(hdlblcust.Text);
        if (hdlblong.Text != "0")
        {
            btnUpdate.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Location can not be updated!!','','warning');", true);
            return;
        }

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", ddlSalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salesman_cd", ddlSalesman.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        bll.vInsertCustLoc(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Location has been updated successfully','Update Applied','success');", true);
        bll.vBindingGridToSp(ref grd, "sp_salesman_customer_get", arr);
        btnUpdate.Visible = false;
    }
}