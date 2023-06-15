using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acc_employeeCashAdvance : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    string txPeriod = null;
    string txjournal = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            //if (Request.Cookies["sp"].Value.ToString() == "0")
            //{
            //    cbsalespoint.Enabled = true;
            //}
            //else
            //{
            //    cbsalespoint.Enabled = false;
            //}
            cbsalespoint.Enabled = false;
            //bll.vBindingComboToSp(ref ddlEmp, "sp_tpch_pettycash_getEmpPettycashProfile2", "emp_cd", "emp_desc");
            bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get5", "emp_cd", "emp_desc");
            ddlEmp.Items.Insert(0, new ListItem("All", "All"));
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }


    protected void btprint_Click(object sender, EventArgs e)
    {
        arr.Clear();
        txPeriod = txpostperiodbyperiodbyuow.Text.ToString();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@period", txPeriod));
        arr.Add(new cArrayList("p_user", Request.Cookies["fullname"].Value.ToString()));

        if (ddlRptTyp.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Report Type is not Selected !','Please Select Report Type first !!','warning');", true);
            return;
        }

            if (ddlRptTyp.SelectedValue == "tran")
        {
            Session["lParamEmployeeCashAdvance"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_employeeCashAdvance');", true);
        }
        else if (ddlRptTyp.SelectedValue == "summ")
        {
            Session["lParamEmployeeCashAdvanceSummary"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=acc_employeeCashAdvanceSummary');", true);
        }
    }
}