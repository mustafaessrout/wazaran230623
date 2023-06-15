using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_pricereport : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vCustomer.Attributes.Add("style", "display:none");
            vSalespoint.Attributes.Add("style", "display:none");
            vGroupCust.Attributes.Add("style", "display:none");
            bll.vBindingFieldValueToCombo(ref ddCusgrcd, "cusgrcd");
            bll.vBindingSalespointToCombo(ref ddSalespoint);
            ddCusgrcd.Items.Insert(0, new ListItem("All Group", "ALL"));
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                ddSalespoint.Items.Insert(0, "ALL | SBTC KINGDOM");
                ddSalespoint.Enabled = true;
            }
            else
            {
                ddSalespoint.Enabled = false;
                ddSalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            }
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (CBTYPE.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=pr');", true);
        }
        else if (CBTYPE.SelectedValue.ToString() == "2")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=histpr');", true);
        }
        else
        {
            if (ddCustomer.SelectedValue.ToString() == "C")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=adjpr&cust=" + ddCustomer.SelectedValue.ToString() + "&note="+ddSalespoint.SelectedValue.ToString()+"');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=adjpr&cust=" + ddCustomer.SelectedValue.ToString() + "&note=" + ddCusgrcd.SelectedValue.ToString() + "');", true);
            }            
        }
        
    }
    protected void CBTYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CBTYPE.SelectedValue.ToString() == "1")
        {
            vCustomer.Attributes.Remove("style");
            ddCustomer_SelectedIndexChanged(sender, e);
        }
        else if (CBTYPE.SelectedValue.ToString() == "2")
        {
            vCustomer.Attributes.Remove("style");
            ddCustomer_SelectedIndexChanged(sender, e);
        }
        else
        {
            vCustomer.Attributes.Add("style","display:none");
            vSalespoint.Attributes.Add("style", "display:none");
            vGroupCust.Attributes.Add("style", "display:none");
        }
    }
    protected void ddCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddCustomer.SelectedValue.ToString() == "C")
        {
            vCustomer.Attributes.Remove("style");
            vSalespoint.Attributes.Remove("style");
            vGroupCust.Attributes.Add("style", "display:none");
        }
        else
        {
            vCustomer.Attributes.Remove("style");
            vGroupCust.Attributes.Remove("style");
            vSalespoint.Attributes.Add("style", "display:none");
        }
    }
}