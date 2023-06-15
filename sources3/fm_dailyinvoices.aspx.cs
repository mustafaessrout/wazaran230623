using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_dailyinvoices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbreport_SelectedIndexChanged(sender, e);
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "0")
        {
            if (dt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date','','warning');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rinv&tg=" + dt.Text + "');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rcusrc');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            if (dt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date','','warning');", true);
                return;
            }
            if (dt0.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date','','warning');", true);
                return;
            }
            if (cbrepby.SelectedValue.ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=oer14&tg=" + dt.Text + "&&tg1=" + dt0.Text + "&p=0');", true);
            }
            else if (cbrepby.SelectedValue.ToString() == "1")
            {              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=oer14&tg=" + dt.Text + "&tg1=" + dt0.Text + "&sls=" + hdsalesman_cd.Value.ToString() + "&p=1');", true);
            }
            
        }
        else if (cbreport.SelectedValue.ToString() == "3")
        {
            if (dt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date','','warning');", true);
                return;
            }
            if (dt0.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please Insert Date','','warning');", true);
                return;
            }
            if (cbrepby.SelectedValue.ToString() == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=int05r&tg=" + dt.Text + "&&tg1=" + dt0.Text + "&p=0');", true);
            }
            else if (cbrepby.SelectedValue.ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=int05r&tg=" + dt.Text + "&tg1=" + dt0.Text + "&sls=" + hdsalesman_cd.Value.ToString() + "&p=1');", true);
            }

        }
        else if (cbreport.SelectedValue.ToString() == "4")
        {            
            if (ch5days.Checked)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rcusrc1');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rcusrc15');", true);
            }

        }
        else if (cbreport.SelectedValue.ToString() == "5")
        {
            if (ch5days.Checked)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rcusrcd1');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=rcusrcd15');", true);
            }

        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salesman_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchSalesman(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void cbreport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "0")
        {
            dt.Visible = true;
            lbdate.Text = "Date";
            lbdate.Visible = true;
            lbDatePnl.Visible = true;
            lbdt0.Visible = false;
            lbdt0Pnl.Visible = false;
            dt0.Visible = false;
            lblrepby.Visible = false;
            cbrepby.Visible = false;
            ch5days.Visible = false;
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            dt.Visible = false;
            lbdate.Visible = false;
            lbDatePnl.Visible = false;
            lbdt0.Visible = false;
            lbdt0Pnl.Visible = false;
            dt0.Visible = false;
            txsalesman.Visible = false;
            lblrepby.Visible = false;
            cbrepby.Visible = false;
            ch5days.Visible = false;
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            dt.Visible = true;
            lbdate.Text = "Start Date";
            lbdate.Visible = true;
            lbDatePnl.Visible = true;
            lbdt0.Visible = true;
            lbdt0Pnl.Visible = true;            
            dt0.Visible = true;
            lblrepby.Visible = true;
            cbrepby.Visible = true;
            ch5days.Visible = false;
        }
        else if (cbreport.SelectedValue.ToString() == "3")
        {
            dt.Visible = true;
            lbdate.Text = "Start Date";
            lbdate.Visible = true;
            lbDatePnl.Visible = true;
            lbdt0.Visible = true;
            lbdt0Pnl.Visible = true;
            dt0.Visible = true;
            lblrepby.Visible = true;
            cbrepby.Visible = true;
            ch5days.Visible = false;
        }
        else if (cbreport.SelectedValue.ToString() == "4")
        {
            dt.Visible = false;
            lbdate.Visible = false;
            lbDatePnl.Visible = false;
            lbdt0.Visible = false;
            lbdt0Pnl.Visible = false;
            dt0.Visible = false;
            txsalesman.Visible = false;
            lblrepby.Visible = false;
            cbrepby.Visible = false;
            ch5days.Visible = true;
        }
        else if (cbreport.SelectedValue.ToString() == "5")
        {
            dt.Visible = false;
            lbdate.Text = "Start Date";
            lbdate.Visible = false;
            lbDatePnl.Visible = false;
            lbdt0.Visible = false;
            lbdt0Pnl.Visible = false;
            dt0.Visible = false;
            lblrepby.Visible = false;
            cbrepby.Visible = false;
            ch5days.Visible = true;
        }
    }

    protected void cbrepby_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbrepby.SelectedValue.ToString() == "0")
        {
            txsalesman.Visible = false;
            lblsls.Visible = false;
            txsalesman.CssClass = "ro";
        }
        else if (cbrepby.SelectedValue.ToString() == "1")
        {
            txsalesman.Visible = true;
            lblsls.Visible = true;
            txsalesman.CssClass = "";
        }

    }
}