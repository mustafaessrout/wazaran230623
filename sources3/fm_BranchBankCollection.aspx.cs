using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_BranchBankCollection : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbfromperiod, "sp_getperiod", "period", "ymtName");
            bll.vBindingComboToSp(ref cbtoperiod, "sp_getperiod", "period", "ymtName");
        }
    }

    protected void btPrint_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(cbfromperiod.Text) > Convert.ToDecimal(cbtoperiod.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select correct condition','Wrong Condition','warning');", true);
            return;
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnRefNo&RefNo=" + refho_no + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=BankCollectionByDate&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);
    }
    protected void btnOther_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(cbfromperiod.Text) > Convert.ToDecimal(cbtoperiod.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select correct condition','Wrong Condition','warning');", true);
            return;
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnRefNo&RefNo=" + refho_no + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=OtherCollectionByDate&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);
    }
    protected void btnAll_Click(object sender, EventArgs e)
    {
        if (Convert.ToDecimal(cbfromperiod.Text) > Convert.ToDecimal(cbtoperiod.Text))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Select correct condition','Wrong Condition','warning');", true);
            return;
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=acccndnRefNo&RefNo=" + refho_no + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=AllCollectionByDate&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);
    }
    //protected void btnPayCashDep_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=PaymentCashDeposit&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);

    //}
    //protected void btnPayBank_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=PaymentBank&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);

    //}
    //protected void btnPayUnpaid_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=PaymentUnpaid&from=" + cbfromperiod.Text + "&to=" + cbtoperiod.Text + "');", true);

    //}
}