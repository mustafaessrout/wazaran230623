using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bankcleareanceho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            string sID = Request.QueryString["id"];
            bll.vBindingComboToSp(ref cbbankto, "sp_tmst_bankaccount_get", "acc_no", "bank_desc");
            dtclear.Text = Request.Cookies["waz_dt"].Value.ToString();
            lbcreated.Text = Request.Cookies["usr_id"].Value.ToString(); //''bll.sGetEmployeeName(Request.Cookies["usr_id"].Value.ToString());
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@deposit_id", sID));
            bll.vGetBankDeposit(arr, ref rs);
            while (rs.Read())
            {
                lbamount.Text = rs["amt"].ToString();
                lbdepositno.Text = rs["deposit_no"].ToString();
                lbbankaccountno.Text = rs["acc_no"].ToString();
                lbbankname.Text = bll.vLookUp("select bank_nm from tmst_bank where bank_cd='" + rs["bank_cd"] + "'");
            } rs.Close();
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "sweetAlert('Remark for reject reason can not empty','Remark','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        string sDepositID = Request.QueryString["id"];
        arr.Add(new cArrayList("@deposit_id", sDepositID));
        arr.Add(new cArrayList("@dep_sta_id", "R"));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@clear_dt", DateTime.ParseExact(dtclear.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vUpdateBankDeposit(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ow", "window.opener.RefreshData();window.close();", true);
    }
    protected void btyes_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        string sDepositID = Request.QueryString["id"];
        arr.Add(new cArrayList("@deposit_id", sDepositID));
        arr.Add(new cArrayList("@dep_sta_id", "C"));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@clear_dt", DateTime.ParseExact(dtclear.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vUpdateBankDeposit(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ow", "window.opener.RefreshData();window.close();", true);
    }
    protected void cbbankto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@bank_cd", cbbankto.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingComboToSp(ref cbaccto, "sp_tmst_bankaccount_getbybankcd","acc_no","acc_no", arr);
    }
}