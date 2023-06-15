using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_balance : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
        }

    }

    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbSalesPointCD.SelectedValue.ToString() == "-1")
        {
            cbsalesman.Enabled = false;
        }
        else
        {
            cbsalesman.Enabled = true;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_salesman_getall", "salesman_cd", "salesman_nm", arr);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
        if (cbsalesman.Enabled == false)
        {
            arr.Add(new cArrayList("@salesman_cd", "-1"));
        }
        else
        {
            arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        }
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue.ToString()));
        if (txbalance.Text == "")
        {
            arr.Add(new cArrayList("@balance", 0));
        }
        else { arr.Add(new cArrayList("@balance", txbalance.Text)); }


        Session["sbalance"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('/fm_report2.aspx?src=balance');", true);
    }
}