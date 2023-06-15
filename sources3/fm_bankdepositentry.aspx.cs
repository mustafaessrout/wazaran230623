using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_bankdepositentry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbbankaccount, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
            bll.vBindingFieldValueToCombo(ref cbdeposittype, "deposit_typ");
            dtdeposit.Text = Request.Cookies["waz_dt"].Value.ToString();
           // bll.sFormat2ddmmyyyy(ref dtdeposit);
        //    bll.vBindingFieldValueToCombo(ref cbstatus, "dep_sta_id");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double dAmt = 0;
        if (!double.TryParse(txamount.Text, out dAmt))
        { 
            ScriptManager.RegisterStartupScript(Page,Page.GetType(),"al","sweetAlert('Amount must numeric','Amount','success');",true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@deposit_no", txdocno.Text));
        arr.Add(new cArrayList("@remark", txremark.Text));
	    arr.Add(new cArrayList("@deposit_dt" , DateTime.ParseExact( dtdeposit.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@acc_no", cbbankaccount.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@amt", txamount.Text));
        arr.Add(new cArrayList("@dep_sta_id","N"));
        arr.Add(new cArrayList("@deposit_typ", cbdeposittype.SelectedValue.ToString()));
        bll.vInsertBankDeposit(arr);
        ScriptManager.RegisterStartupScript(Page,Page.GetType(),"al","sweetAlert('Bank Deposit saved successfully','','warning');",true);
        txamount.CssClass = "ro";
        txdocno.CssClass = "ro";
        txremark.CssClass = "ro";
        cbbankaccount.CssClass = "ro";
        cbdeposittype.CssClass = "ro";
      }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_bankdepositentry.aspx");
    }
}