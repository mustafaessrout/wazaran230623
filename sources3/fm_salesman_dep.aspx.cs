using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesman_dep : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            cd.v_disablecontrol(lbdepositcode);
            cd.v_disablecontrol(lbtabdevicecode);
            cd.v_disablecontrol(dtdeposit);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            bll.vBindingComboToSpWithEmptyChoosen(ref cbbankaccount, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyjobtitle","emp_cd","emp_nm",arr);
            dtdeposit.Text = Request.Cookies["waz_dt"].Value;
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salesman_dep.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        //string _sql = "update tsalesman_balance_deposit set salesdep_sta_id='C' where deposit_cd='"+hddeposit.Value+"'";
        //bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=salesdep&d="+lbdepositcode.Text+"');", true);
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@deposit_cd", hddeposit.Value));
        List<tsalesman_balance_deposit> _tsalesman_balance_deposit = bll.lSalesmanBalanceDeposit(arr);
        foreach(tsalesman_balance_deposit t in _tsalesman_balance_deposit)
        {
            dtdeposit.Text = t.deposit_dt.ToString("d/M/yyyy");
            txamt.Text = t.amt.ToString();
            //txemployee.Text = t.salesman_nm;
            cbsalesman.SelectedValue = t.salesman_cd;
            cbsalesman_SelectedIndexChanged(sender, e);
            lbdepositcode.Text = t.deposit_cd;
            cbbankaccount.SelectedValue = t.acc_no;
            
           
        };
        cd.v_showcontrol(btprint);
        cd.v_hiddencontrol(btsave);
        cd.v_disablecontrol(cbsalesman);
        cd.v_disablecontrol(txamt);
        cd.v_disablecontrol(cbbankaccount);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (cbbankaccount.SelectedValue == String.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                    "sweetAlert('Please select account bank!','Deposit destination','warning');", true);
            return;
        }
        decimal _amount = 0;
        if (!decimal.TryParse(txamt.Text, out _amount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please enter correct amount!','Amount deposit','warning');", true);
            return;
        }
        if (txdepositno.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please enter deposit number!','Deposit No','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        string _depositcode = string.Empty;
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@amt", _amount));
        arr.Add(new cArrayList("@salesdep_sta_id","N"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@acc_no", cbbankaccount.SelectedValue));
        arr.Add(new cArrayList("@deposit_no", txdepositno.Text));
        arr.Add(new cArrayList("@deposit_dt", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertSalesmanDeposit(arr, ref _depositcode);
        lbdepositcode.Text = _depositcode;
        arr.Clear();

        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(cbsalesman);
        cd.v_disablecontrol(cbbankaccount);
        cd.v_disablecontrol(txamt);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                  "sweetAlert('Deposit to bank has been created!','"+_depositcode+"','success');", true);
    }

    protected void cbsalesman_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal _balance = Convert.ToDecimal( bll.vLookUp("select dbo.fn_getsalesmanbalance('"+cbsalesman.SelectedValue+"')"));
        lbbalance.Text = _balance.ToString("N2");
        cd.v_showcontrol(btsave);
    }
}