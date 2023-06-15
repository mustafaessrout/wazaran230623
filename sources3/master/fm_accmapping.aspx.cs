using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_accmapping : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbtrn, "trn_typ");
            vBindingGrid();
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcoa = new List<string>();
        string sCoa = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@coa_cd", prefixText));
        bll.vSearchAccMstCOA(arr, ref rs);
        while (rs.Read())
        {
            sCoa = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["coa_cd"].ToString() + "-" + rs["coa_nm"].ToString(), rs["coa_cd"].ToString());
            lcoa.Add(sCoa);
        }
        rs.Close();
        return (lcoa.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcoa = new List<string>();
        string sCoa = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@coa_cd", prefixText));
        bll.vSearchAccMstCOA(arr, ref rs);
        while (rs.Read())
        {
            sCoa = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["coa_cd"].ToString() + "-" + rs["coa_nm"].ToString(), rs["coa_cd"].ToString());
            lcoa.Add(sCoa);
        }
        rs.Close();
        return (lcoa.ToArray());
    }
    protected void btsave_Click(object sender, EventArgs e)
    {

        if ((txcredit.Text == "") || (txdebet.Text == ""))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select both of debet/credit!','Credit / Debet','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@debet", hddebet.Value.ToString()));
        arr.Add(new cArrayList("@credit", hdcredit.Value.ToString()));
        arr.Add(new cArrayList("@trn_typ", cbtrn.SelectedValue.ToString()));
        arr.Add(new cArrayList("@refno", cbrefno.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deleted", 0));
        bll.vInsertAccTransactionMap(arr);
        vBindingGrid();
        txcredit.Text = ""; hdcredit.Value = "";
        txdebet.Text = ""; hddebet.Value = "";
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trn_typ", cbtrn.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tacc_transactionmap_get", arr);
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbrefno = (Label)grd.Rows[e.RowIndex].FindControl("lbrefno");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trn_typ", cbtrn.SelectedValue.ToString()));
        arr.Add(new cArrayList("@refno", lbrefno.Text));
        bll.vDelAccTransactionMap(arr);
        vBindingGrid();
    }
    protected void cbtrn_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@trn_typ", cbtrn.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tacc_transactionmap_get", arr);
        switch (cbtrn.SelectedValue.ToString())
        {
            case "CASHOUT":
                arr.Clear();
                arr.Add(new cArrayList("@inout", "O"));
                bll.vBindingComboToSp(ref cbrefno, "sp_tmst_itemcashout_getbyinout", "itemco_cd", "itemco_nm", arr);
                break;
            case "CASHIN":
                arr.Clear();
                arr.Add(new cArrayList("@inout", "I"));
                bll.vBindingComboToSp(ref cbrefno, "sp_tmst_itemcashout_getbyinout", "itemco_cd", "itemco_nm", arr);
                break;
        }
    }
}