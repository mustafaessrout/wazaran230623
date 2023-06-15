using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_activedeactive : System.Web.UI.Page
{
    cbll bll =new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@qry_cd", "SalesJob"));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_nm", arr);
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        BindingGrid();
    }

    void BindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue));
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@cust_cd", txcustomer.Text));
        bll.vBindingGridToSp(ref grd, "sp_tmst_customer_getbysalesman", arr);
    }
    protected void btactivated_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        Label lbcustomercode = (Label)_row.FindControl("lbcustomercode");
        string _sql = "update tmst_customer set cust_sta_id='A' where cust_cd='" + lbcustomercode.Text + "'";
        bll.vExecuteSQL(_sql);
        BindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
              "sweetAlert('Customer has been Activated !','" + lbcustomercode.Text + "','success');", true);
    }

    protected void btdeactivated_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        Label lbcustomercode = (Label)_row.FindControl("lbcustomercode");
        Label lbbalance = (Label)_row.FindControl("lbbalance");
        double _balance = Convert.ToDouble(lbbalance.Text);
        if (_balance > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('You can not deactivated this customer !','Because have balance must be zero','warning');", true);
            return;
        }
        string _sql = "update tmst_customer set cust_sta_id='I' where cust_cd='" + lbcustomercode.Text + "'";
        bll.vExecuteSQL(_sql);
        BindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Customer has been deactivated !','"+lbcustomercode.Text+"','success');", true);
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbstatus = (Label)e.Row.FindControl("lbstatus");
            Label lbcustomercode = (Label)e.Row.FindControl("lbcustomercode");
            string _citycode = bll.vLookUp("select city_cd from tmst_customer where cust_cd='"+lbcustomercode.Text+"'");
            string _districtcode = bll.vLookUp("select district_cd from tmst_customer where cust_cd='" + lbcustomercode.Text + "'");
            DropDownList cbcity = (DropDownList)e.Row.FindControl("cbcity");
            DropDownList cbdistrict = (DropDownList)e.Row.FindControl("cbdistrict");
            arr.Add(new cArrayList("@loc_typ", "CIT"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcity, "sp_tmst_location_getbytype", "loc_cd", "loc_nm", arr);
            cbcity.SelectedValue = _citycode;
            arr.Clear();
            arr.Add(new cArrayList("@loc_cd", cbcity.SelectedValue));
            arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbdistrict, "sp_tmst_location_getbyparent", "loc_cd", "loc_nm", arr);
            cbdistrict.SelectedValue = _districtcode;
            if (lbstatus.Text == "INACTIVE")
            {
                e.Row.BackColor = System.Drawing.Color.Silver;
            }
        }
    }

    protected void btupdateaddress_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        TextBox txaddress = (TextBox)_row.FindControl("txaddress");
        Label lbcustomercode = (Label)_row.FindControl("lbcustomercode");
        if (txaddress.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Address can not empty !','Edit Address','warning');", true);
            return;
        }
        string _sql = "update tmst_customer set addr='"+txaddress.Text+"' where cust_cd='"+lbcustomercode.Text+"'";
        bll.vExecuteSQL(_sql);
        BindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Address has been changed successfully !','New Address : "+txaddress.Text+"','success');", true);
    }

    protected void btupdatecity_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        DropDownList cbcity = (DropDownList)_row.FindControl("cbcity");
        Label lbcustomercode = (Label)_row.FindControl("lbcustomercode");
        if (cbcity.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('City must be selected !','Edit City','warning');", true);
            return;
        }

        string _sql = "update tmst_customer set city_cd='" + cbcity.SelectedValue+ "' where cust_cd='" + lbcustomercode.Text + "'";
        bll.vExecuteSQL(_sql);
        BindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('City has been changed successfully !','New City : " +cbcity.SelectedItem.Text + "','success');", true);
    }

    protected void btupdatedistrict_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        DropDownList cbdistrict = (DropDownList)_row.FindControl("cbdistrict");
        
        Label lbcustomercode = (Label)_row.FindControl("lbcustomercode");
        if (cbdistrict.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('District must be selected !','Edit District','warning');", true);
            return;
        }

        string _sql = "update tmst_customer set district_cd='" + cbdistrict.SelectedValue + "' where cust_cd='" + lbcustomercode.Text + "'";
        bll.vExecuteSQL(_sql);
        BindingGrid();
    }

    protected void cbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList btn = (DropDownList)sender;
        GridViewRow _row = (GridViewRow)btn.NamingContainer;
        DropDownList cbdistrict = (DropDownList)_row.FindControl("cbdistrict");
        DropDownList cbcity = (DropDownList)_row.FindControl("cbcity");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", cbcity.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", cbsalesman.SelectedValue)); 
        bll.vBindingComboToSpWithEmptyChoosen(ref cbdistrict, "sp_tmst_location_getbyparent", "loc_cd", "loc_nm", arr);
    }
}