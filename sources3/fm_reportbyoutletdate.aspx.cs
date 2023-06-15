using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reportbyoutletdate : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbpaymentterm, "payment_term");
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));            
            // bll.vBindingComboToSp(ref cbcustnm, "sp_tmst_customer_get", "Cust_CD", "cust_desc", arr);
        }
    }
    protected void btreport_Click(object sender, EventArgs e)
    {

       
        if (cbreport.SelectedValue == "0" || cbreport.SelectedValue == "1")
        {
            if (!chcustomernm.Checked)
            {
                if (txcust.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Salesman name must be filled','error');", true);
                    return;
                }
            }
            List<cArrayList> arr = new List<cArrayList>();
            if (cbreport.SelectedValue == "1")
            { arr.Add(new cArrayList("@bef_3days", cbday.SelectedValue)); }
            else
            { arr.Add(new cArrayList("@bef_3days", null)); }
            if (!chcustomernm.Checked)
            {
                arr.Add(new cArrayList("@emp_cd", hdemp.Value));
            }
            else
            {
                arr.Add(new cArrayList("@emp_cd", null));
            }
            if (!chpayment.Checked)
            {
                arr.Add(new cArrayList("@payment_term", cbpaymentterm.SelectedValue));
            }
            else
            {
                arr.Add(new cArrayList("@payment_term", null));
            }

            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
            Session["lParamrpout"] = arr;
            if (!chcustomeralone.Checked)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=rpout');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=rpout1');", true);
            }
        }
        else if (cbreport.SelectedValue == "2" || cbreport.SelectedValue == "3")
        {
            List<cArrayList> arr = new List<cArrayList>();
            if (cbreport.SelectedValue == "3")
            { arr.Add(new cArrayList("@bef_3days", cbday.SelectedValue)); }
            else
            { arr.Add(new cArrayList("@bef_3days", null)); }
            if (!chpayment.Checked)
            {
                arr.Add(new cArrayList("@payment_term", cbpaymentterm.SelectedValue));
            }
            else
            {
                arr.Add(new cArrayList("@payment_term", null));
            }
            arr.Add(new cArrayList("@cusgrcd",cbcusgrcd.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@emp_cd", hdemp.Value));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@usr_nm", Request.Cookies["usr_id"].Value.ToString()));
            Session["lParamrpout2"] = arr;
            if (!chcustomeralone.Checked)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=rpout2');", true);
            }
            else
            {                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=rpout3');", true);
            }
        }


    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SALESCD"));
        //arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }
    protected void chpayment_CheckedChanged(object sender, EventArgs e)
    {
        if (chpayment.Checked)
        {
            cbpaymentterm.Enabled = false;
        }
        else
        {
            cbpaymentterm.Enabled = true;
        }
    }
    protected void chcustomernm1_CheckedChanged(object sender, EventArgs e)
    {
        //if (bll.nCheckAccess("SOA3", Request.Cookies["usr_id"].Value.ToString()) == 0)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To print this report !!','warning');", true);
        //    chcustomeralone.Checked = false;
        //    return;
        //}
        if (chcustomernm.Checked)
        {
            txcust.Enabled = false;
        }
        else
        {
            txcust.Enabled = true;

        }

    }
    protected void chcustomernm_CheckedChanged(object sender, EventArgs e)
    {
      
        if (chcustomernm.Checked)
        {
            txcust.Enabled = false;
        }
        else
        {
            txcust.Enabled = true;

        }
    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue == "0" || cbreport.SelectedValue == "2")
        {
            lblday.Visible = false;
            cbday.Visible = false;
            panelcbday.Visible = false;
            lbcusgrcd.Visible = false;
            cbcusgrcd.Visible = false;
            panelcbcusgrcd.Visible = false;
            chcustomernm.Visible = true;
            txcust.Visible = true;
            lblcust.Visible = true;
            if (cbreport.SelectedValue == "2") { chcustomernm.Visible = false;
            lbcusgrcd.Visible = true;
            cbcusgrcd.Visible = true;
            panelcbcusgrcd.Visible = true;
            txcust.Visible = false;
            lblcust.Visible = false;
            chcustomernm.Visible = false;
            }
        }
        else if (cbreport.SelectedValue == "1" || cbreport.SelectedValue == "3")
        {
            lblday.Visible = true;
            cbday.Visible = true;
            panelcbday.Visible = true;
            txcust.Visible = true;
            lblcust.Visible = true;
            chcustomernm.Visible = true;
            lbcusgrcd.Visible = false;
            cbcusgrcd.Visible = false;
            panelcbcusgrcd.Visible = false;
            if (cbreport.SelectedValue == "3") { chcustomernm.Visible = false;
            lbcusgrcd.Visible = true;
            cbcusgrcd.Visible = true;
            panelcbcusgrcd.Visible = true;
            txcust.Visible = false;
            lblcust.Visible = false;
            chcustomernm.Visible = false;
            }
        }
    }
}