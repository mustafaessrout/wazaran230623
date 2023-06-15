using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_AccCashAdvanceApproval_List : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalDebit = 0;
    decimal totalCredit= 0;
    public static int PreviousIndex;
    Boolean hasApprovalRole = false;
    Boolean isEmptyGrid1 = false;
    Boolean isEmptyGrid2 = false;
    Boolean isEmptyGrid3 = false;
    string userid = null;
    //string userid2 = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = Request.Cookies["usr_id"].Value.ToString();
        //string userid = "2833";

        ////userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
        ////if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
        //if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
        //{
            hasApprovalRole = true;
        //}

        if (!IsPostBack)
        {
            bindinggrd(hasApprovalRole, isEmptyGrid1); 
            bindinggrd2(hasApprovalRole, isEmptyGrid2); 
            bindinggrd3(hasApprovalRole, isEmptyGrid3);
        }

        if (hasApprovalRole)
        {
            //btaddfinreport.Visible = false;
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    private void bindinggrd(Boolean hasApprovalRole, Boolean isEmptyGrid1)
    {
        List<cArrayList> arr = new List<cArrayList>();
            
        string isEmptyGridStr = string.Empty;
        arr.Add(new cArrayList("@isEmptyGridStr", isEmptyGridStr));
        bll.vBindingGridToSp(ref grd, "sp_acc_cash_advance_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        arr.Clear();
        bll.vIsCashAdvanceEmptyGrid(arr, ref isEmptyGridStr);
        if (isEmptyGridStr.Equals("1"))
        {
            isEmptyGrid1 = true;
            grd.Columns[7].Visible = false; 
        }
        else
        {
            isEmptyGrid1 = false;
        }
    }

    private void bindinggrd2(Boolean hasApprovalRole, Boolean isEmptyGrid2)
    {
        List<cArrayList> arr = new List<cArrayList>();

        string isEmptyGridStr = string.Empty;
        arr.Add(new cArrayList("@isEmptyGridStr", isEmptyGridStr));
        bll.vBindingGridToSp(ref grd2, "sp_acc_cash_advance_settlement_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        arr.Clear();
        bll.vIsCashAdvanceSettlementEmptyGrid(arr, ref isEmptyGridStr);
        if (isEmptyGridStr.Equals("1"))
        {
            isEmptyGrid2 = true;
            grd2.Columns[8].Visible = false;
        }
        else
        {
            isEmptyGrid2 = false;
        }
    }

    private void bindinggrd3(Boolean hasApprovalRole, Boolean isEmptyGrid3)
    {
        List<cArrayList> arr = new List<cArrayList>();

        string isEmptyGridStr = string.Empty;
        arr.Add(new cArrayList("@isEmptyGridStr", isEmptyGridStr));
        bll.vBindingGridToSp(ref grd3, "sp_acc_cash_advance_settlement_claim_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);

        arr.Clear();
        bll.vIsCashAdvanceSettlementClaimEmptyGrid(arr, ref isEmptyGridStr);
        if (isEmptyGridStr.Equals("1"))
        {
            isEmptyGrid3 = true;
            grd3.Columns[7].Visible = false;
        }
        else
        {
            isEmptyGrid3 = false;
        }
    }
}

                    