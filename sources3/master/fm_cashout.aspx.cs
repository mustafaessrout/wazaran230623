using AppClassTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_cashout : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    AppClass app = new AppClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cbfor_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ");
            bll.vBindingFieldValueToCombo(ref cbinout, "inout");
            bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
            //bll.vBindingComboToSp(ref cbcredit, "sp_tacc_mst_coa_getbylastlevel", "coa_cd", "coa_desc");
            //bll.vBindingComboToSp(ref cbdebet, "sp_tacc_mst_coa_getbylastlevel", "coa_cd", "coa_desc");
            vBindingGrid();
            hditem.Value = "";
            cbfor.CssClass = cd.csstextro;
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sNo = string.Empty;
        //if (txcashoutcode.Text == "")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cashout Code can not empty!','Check Cashout Code','warning');", true);
        //    return;
        //}
        if (txcashoutname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cashout Name can not empty!','Check Cashout Name','warning');", true);
            return;
        }

        //if (hddebet.Value.ToString() == hdcredit.Value.ToString())
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Account Debet and Credit can not be same!','Check COA','warning');", true);
        //    return;
        //}
        hditem.Value = txcashoutcode.Text;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", txcashoutcode.Text));
        arr.Add(new cArrayList("@itemco_nm", txcashoutname.Text));
        arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        arr.Add(new cArrayList("@coa_cd_credit", hdcredit.Value.ToString()));
        arr.Add(new cArrayList("@coa_cd_debet", hddebet.Value.ToString()));
        arr.Add(new cArrayList("@deleted", 0));
        if (cbfor.SelectedValue.ToString() == "E")
        {
            
            bll.vInsertExhibitMstItemCashout(arr, ref sNo);
            txcashoutcode.CssClass = cd.csstextro;
            txcashoutcode.Text = sNo;
        }
        else if (cbfor.SelectedValue.ToString() == "B")
        {
            bll.vInsertMstItemCashout1(arr, ref sNo);
            txcashoutcode.CssClass = cd.csstextro;
            btadd.CssClass = "btn btn-default ro";

        }
        vBindingGrid();
        if (sNo == "-2") { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Duplicate records','failed','warning');", true); }
        else { ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data has been saved successfully','Successfully','success');", true); }
        
    }

    void vBindingGrid()
    {
        if (cbfor.SelectedValue.ToString() == "B")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
            arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
        }
        else if (cbfor.SelectedValue.ToString() == "E")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_texhibit_mstitemcashout_get", arr);
        }
    }
    protected void cbfor_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        txcashoutcode.Text = "";
        txcashoutname.Text = "";

        vBindingGrid();
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {

    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        vBindingGrid();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        Label lbitemcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemcode");
        Label lbitemname = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbitemname");
        hditem.Value = lbitemcode.Text;
        List<cArrayList> arr = new List<cArrayList>();
        txcashoutcode.Text = lbitemcode.Text;
        txcashoutname.Text = lbitemname.Text;
        cbcategory.SelectedValue = bll.vLookUp("select cashout_typ from tmst_itemcashout where itemco_cd='" + lbitemcode.Text + "'");
        txcashoutcode.CssClass = cd.csstextro;
        //btsave.CssClass = "btn btn-default ro"; // Riwan ask to update data.
        btsave.CssClass = "btn btn-primary";
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        txcashoutcode.CssClass = cd.csstext;
        txcashoutcode.Text = "";
        txcashoutname.Text = "";
        btsave.CssClass = "btn btn-primary";
        hditem.Value = "";

    }
    protected void btcradd_Click(object sender, EventArgs e)
    {
        if (hdcredit.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('COA Credit can not empty!','Please select COA Credit','warning');", true);
            return;
        }
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Cashout is not selected!','Please select item','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@coa_cd", hdcredit.Value.ToString()));
        arr.Add(new cArrayList("@crdb", "CR"));
        bll.vInsertItemCashoutCOA(arr);

        hdcredit.Value = "";
    }
    protected void btadddebet_Click(object sender, EventArgs e)
    {
        if (hditem.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item Cashout is not selected!','Please select item','warning');", true);
            return;
        }

        if (hddebet.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('COA Debet!','Please select COA Debet','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", hditem.Value.ToString()));
        arr.Add(new cArrayList("@coa_cd", hddebet.Value.ToString()));
        arr.Add(new cArrayList("@crdb", "DB"));
        bll.vInsertItemCashoutCOA(arr);
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
    protected void grdcredit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@itemco_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@deleted", 1));
        bll.vUpdateMstItemCashout(arr);
        vBindingGrid();
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                LinkButton lnk_Remarks = (LinkButton)e.Row.FindControl("lnk_Remarks");
                LinkButton lnk_Attribute = (LinkButton)e.Row.FindControl("lnk_Attribute");
                LinkButton lnk_RemarksNew = (LinkButton)e.Row.FindControl("lnk_RemarksNew");
                LinkButton lnk_AttributeNew = (LinkButton)e.Row.FindControl("lnk_AttributeNew");
                var attributeCount = bll.vLookUp("select count(*) from titemcashout_attribute where itemco_cd='" + lbitemcode.Text + "'");
                var remarkCount = bll.vLookUp("select count(*) from titemcashout_remark where itemco_cd='" + lbitemcode.Text + "'");

                string url = string.Empty;
                if (attributeCount != "0")
                {
                    url = "fm_cashoutAttributePopup.aspx?cashoutType=attribute" + "&cashOutCode=" + lbitemcode.Text;
                    //lnk_Attribute.Text = "Attribute(" + attributeCount + ")";
                    lnk_Attribute.Visible = true;
                    lnk_AttributeNew.Visible = false;
                    lnk_Attribute.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");
                }
                else
                {
                    url = "fm_cashoutAttributePopup.aspx?cashoutType=attribute" + "&cashOutCode=" + lbitemcode.Text;
                    lnk_Attribute.Visible = false; lnk_AttributeNew.Visible = true;
                    lnk_AttributeNew.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");
                }
                if (remarkCount != "0")
                {
                    url = "fm_cashoutRemarksPopup.aspx?cashoutType=remark" + "&cashOutCode=" + lbitemcode.Text;
                    lnk_Remarks.Visible = true;
                    lnk_RemarksNew.Visible = false;
                    lnk_Remarks.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");
                    //lnk_Remarks.Text = "Reamrks(" + remarkCount + ")";
                }
                else
                {
                    url = "fm_cashoutRemarksPopup.aspx?cashoutType=remark" + "&cashOutCode=" + lbitemcode.Text;
                    lnk_Remarks.Visible = false; lnk_RemarksNew.Visible = true;
                    lnk_RemarksNew.Attributes.Add("onClick", "JavaScript: window.open('" + url + "','','_blank','width=500,height=245,left=350,top=400')");
                }


            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Sales Target", "Sales Target Head Office", "fm_salestargetho2", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (cbfor.SelectedValue.ToString() == "B")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
            arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_itemcashout_get", arr);
        }
        else if (cbfor.SelectedValue.ToString() == "E")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
            arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_texhibit_mstitemcashout_get", arr);
        }
    }
}