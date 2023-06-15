using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_SalesmanGroupMapping : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@qry_cd", "SalesJob"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                //if (bll.nCheckAccess("IT", Request.Cookies["usr_id"].Value.ToString()) == 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','To approve this Sales Group Mapping !!','warning');", true);
                //    btnCancel.Enabled = false;
                //    btsave.Enabled = false;
                //    txsalesman.Visible = false;
                //    lblSalesma.Visible = false;
                //    return;
                //}
                bll.vBindingGridToSp(ref grd, "sp_tmst_employee_getbyqryForPriority", arr);
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention'," + ex.Message + ex.InnerException + ",'error');", true);
                ut.Logs("", "Sales Target", "Salesman mapping", "fm_SalesmanGroupMapping", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }
    }
    protected void btCancel_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            txsalesman.Text = string.Empty;
            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_getbyqryForPriority", arr);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention'," + ex.Message + ex.InnerException + ",'error');", true);
            ut.Logs("", "Sales Target", "Salesman mapping", "fm_SalesmanGroupMapping", "btCancel_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtbranchHead = new DataTable();
                            var salesPointCD = HttpContext.Current.Request.Cookies["sp"].Value.ToString();
                            List<cArrayList> arrBranchHeadEmail = new List<cArrayList>();
                            arrBranchHeadEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                            dtbranchHead = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrBranchHeadEmail);
            foreach (GridViewRow row in grd.Rows)
            {

                CheckBox chkisVan = (CheckBox)row.FindControl("chkisVan");
                HiddenField hdempCD = (HiddenField)row.FindControl("hdempCD");
                if (chkisVan.Checked)
                {
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Add(new cArrayList("@emp_cd", Convert.ToString(hdempCD.Value)));
                    arr1.Add(new cArrayList("@group", "VAN"));
                    arr1.Add(new cArrayList("@direcspv_cd", Convert.ToString(dtbranchHead.Rows[0]["emp_cd"])));
                    arr1.Add(new cArrayList("@enterBy", Convert.ToString(Request.Cookies["usr_id"].Value.ToString())));
                    bll.vBindingGridToSp(ref grd, "sp_temployee_info_ins", arr1);
                }
                else
                {
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Add(new cArrayList("@emp_cd", Convert.ToString(hdempCD.Value)));
                    bll.vBindingGridToSp(ref grd, "sp_temployee_info_dlt", arr1);
                }
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_getbyqryForPriority", arr);
            txsalesman.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during data update','error');", true);
            ut.Logs("", "Sales Target", "Salesman mapping", "fm_SalesmanGroupMapping", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox isVan = (CheckBox)e.Row.FindControl("chkisVan");
                HiddenField hdprodcd = (HiddenField)e.Row.FindControl("hdempCD");
                HiddenField hdfisVan = (HiddenField)e.Row.FindControl("hdfisVan");
                 List<cArrayList> arr = new List<cArrayList>();

                 if (hdfisVan.Value == "False" || hdfisVan.Value == "")
                 {
                     isVan.Checked = false;
                 }
                 else { isVan.Checked = true; }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Sales Target", "Salesman mapping", "fm_SalesmanGroupMapping", "grd_RowDataBound", "Exception", ex.Message + ex.InnerException);
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList1(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        string sEmp = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmp = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@job_title", "SalesJob"));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        cbll bll = new cbll();
        bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
        while (rs.Read())
        {
            sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lEmp.Add(sEmp);
        } rs.Close();
        return (lEmp.ToArray());
    }

    protected void bttmp_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@salesman_cd", Convert.ToString(hdemp.Value)));

            bll.vBindingGridToSp(ref grd, "sp_tmst_employee_getbyqryForPriority", arr);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Sales Target", "Salesman mapping", "fm_SalesmanGroupMapping", "bttmp_Click", "Exception", ex.Message + ex.InnerException);
        }
    }
}