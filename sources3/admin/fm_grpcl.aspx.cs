using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_grpcl : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            bll.vBindingGridToSp(ref grd, "sp_tmst_groupcreditlimit_get");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        double dCL = 0;
        if (!double.TryParse(txcreditlimit.Text, out dCL))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please credit limit on numeric only','Credit limit not inserted properly','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cusgrcd", cbcusgrcd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@credit_limit", txcreditlimit.Text));
        arr.Add(new cArrayList("@created_dt", System.DateTime.Now));
        arr.Add(new cArrayList("@lastchanged_dt", System.DateTime.Now));
        arr.Add(new cArrayList("@createdby",  Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertMstGroupCreditLimist(arr);
        bll.vBindingGridToSp(ref grd, "sp_tmst_groupcreditlimit_get");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Group CL has been created','Group Credit Limit','success');", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbcusgrcd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbcusgrcd");
        cbcusgrcd.SelectedValue = lbcusgrcd.Text;
        txcreditlimit.Text = bll.vLookUp("select credit_limit from tmst_groupcreditlimit where cusgrcd='"+lbcusgrcd.Text+"'");
    }
}