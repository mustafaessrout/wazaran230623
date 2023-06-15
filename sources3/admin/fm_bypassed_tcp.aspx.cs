using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_bypassed_tcp : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // bll.vBindingComboToSpWithEmptyChoosen(ref cbparameter, "sp_tcontrol_parameter_get", "parm_valu", "parm_display");
        }
    }

    protected void btsalesmandeposit_Click(object sender, EventArgs e)
    {
        string _sql = "update tcontrol_parameter set parm_valu='Y' where parm_nm='bypassed_deposit'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Bypassed done','Salesman Deposit','success');", true);
    }

    protected void btpaymetncash_Click(object sender, EventArgs e)
    {
        string _sql = "update tcontrol_parameter set parm_valu='Y' where parm_nm='bypassed_paymentcash'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Bypassed done','Customer cash must paid','success');", true);
    }

    protected void btbypassinvoicereceived_Click(object sender, EventArgs e)
    {
        string _sql = "update tcontrol_parameter set parm_valu='Y' where parm_nm='bypassed_invoicereceived'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Bypassed done','Invoice received bypassed','success');", true);
    }
}