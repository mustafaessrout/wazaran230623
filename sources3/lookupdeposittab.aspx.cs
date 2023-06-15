using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookupdeposittab : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            vBindingGrid();
        }
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        bll.vBindingGridToSp(ref grd, "sp_ttab_salesman_balance_deposit_getbysalespoint", arr);
    }

 //   @deposit_cd varchar(50) OUT,    
 //@salesman_cd varchar(50),    
 //@amt numeric(18,5),     
 //@salesdep_sta_id varchar(50),    
 //@deposit_dt date,
 //@salespointcd varchar(50) ,  
 //@tab_deposit_cd varchar(50)
    protected void btimport_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbdepositcode = (Label)row.FindControl("lbdepositcode");
        HiddenField hdacc = (HiddenField)row.FindControl("hdacc");
        HiddenField hddepositNo = (HiddenField)row.FindControl("hddepositNo");
        HiddenField hdsalesman = (HiddenField)row.FindControl("hdsalesman");
        Label lbamount = (Label)row.FindControl("lbamount");
        Label lbdepositdate = (Label)row.FindControl("lbdepositdate");
        DateTime _depositdate = System.DateTime.ParseExact(lbdepositdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string _deposit = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@tab_deposit_cd", lbdepositcode.Text));
        arr.Add(new cArrayList("@salesman_cd", hdsalesman.Value));
        arr.Add(new cArrayList("@amt", Convert.ToDecimal(lbamount.Text)));
        arr.Add(new cArrayList("@salesdep_sta_id", "N"));
        arr.Add(new cArrayList("@deposit_dt", _depositdate));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@acc_no", hdacc.Value));
        arr.Add(new cArrayList("@deposit_no", hddepositNo.Value));
        bll.vInsertSalesmanDeposit(arr, ref _deposit);
        string _sql = "update ttab_salesman_balance_deposit set trf_dt=dbo.fn_getsystemdate('" + Request.Cookies["sp"].Value +"') where tab_deposit_cd='" + lbdepositcode.Text + "'";
        bll.vExecuteSQL(_sql);

        string _salesmanname = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='"+hdsalesman.Value+"'");
        string _message = "#New salesman deposit has been created with code :" + _deposit + 
            " with salesman name "+_salesmanname+" deposit amount : "+lbamount.Text+", Please do confirmation " +
            " as soon as possible, to settle salesman balance";
        arr.Clear();
        arr.Add(new cArrayList("@doc_typ", "salesmandeposit"));
        arr.Add(new cArrayList("@level_no", 1));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        List<tapprovalpattern> _app = bll2.lApprovalPattern(arr);
        foreach(tapprovalpattern _t in _app)
        {
            bll.vSendWhatsapp(_t.whatsapp_no, _message);
            bll.vSendWhatsapp("966503743024", _message);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "window.opener.SelectData('"+_deposit+"');window.close();", true);
    }

    protected void btreject_Click(object sender, EventArgs e)
    {
        LinkButton btn  = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbdepositcode = (Label)row.FindControl("lbdepositcode");
        string _sql = "update ttab_salesman_balance_deposit set trf_dt=getdate(), deleted=1 where tab_deposit_cd='" + lbdepositcode.Text+"'";
        bll.vExecuteSQL(_sql);
        vBindingGrid();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Reject deposit has been successfully !','"+lbdepositcode.Text+"','success');", true);
    }
}