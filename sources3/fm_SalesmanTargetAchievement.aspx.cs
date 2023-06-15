using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_SalesmanTargetAchievement : System.Web.UI.Page
{
    Utitlity ut = new Utitlity();
    cbll bll = new cbll();
    cdal cdl = new cdal();
    creport rep = new creport();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (bll.nCheckAccess("SalesmanTrgtAchiv", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                Response.Write("<script language='javascript'>window.alert('Only Product Supervisor Can Access, Please Contact Hani Sungkar !');window.location='default.aspx';</script>");
            }

            ut.BranchTargetPriority();
            ut.BranchTargetPriorityVan();

            List<cArrayList> arr = new List<cArrayList>();
            System.Data.SqlClient.SqlDataReader rs = null;
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vSearchtuserprofile(arr, ref rs);
            while (rs.Read())
            {
                lblEmpNo.Text = rs["emp_cd"].ToString();
                lblUserEmail.Text = rs["email"].ToString();
                lblUserName.Text = rs["fullname"].ToString();
                lblPeriod.Text = Convert.ToString(bll.sGetControlParameter("period"));
                //BindUserRelatedProduct();
                arr.Clear();
                arr.Add(new cArrayList("@emp_cd", lblEmpNo.Text));
                arr.Add(new cArrayList("@period", lblPeriod.Text));
                //bll.vBindingGridToSp(ref GridView1, "sp_TargetAllItemsByUser_get", arr);
            }
            rs.Close();

            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", lblEmpNo.Text));
            arr.Add(new cArrayList("@period", lblPeriod.Text));
            bll.vBindingGridToSp(ref grd, "sp_tmst_PriorityProductMailsByUser_get", arr);
            //bll.vBindingGridToSp(ref grd, "sp_tmst_PriorityProductMailsByUser_get", arr);
        }
    }

    private void BindUserRelatedProduct() {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", lblEmpNo.Text));
        arr.Add(new cArrayList("@period", lblPeriod.Text));
        //bll.vBindingGridToSp(ref GridView1, "sp_TargetAllItemsByUser_get", arr);
        //bll.vBindingGridToSp(ref grd, "sp_TargetAllItemsByUser_get", arr);
    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("SalesmanTrgtAchiv", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                Response.Write("<script language='javascript'>window.alert('Only Product Supervisor Can Access, Please Contact Hani Sungkar !');window.location='default.aspx';</script>");
            }

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "ShowProgress()", true);

            ut.BranchTargetNonPriorityMonthlyBYUser(bll.sGetControlParameter("period"), lblEmpNo.Text);
            ut.BranchTargetNonPriorityVanMonthlyByUser(bll.sGetControlParameter("period"), lblEmpNo.Text);
            ut.BranchTargetPriorityMonthlyByUser(bll.sGetControlParameter("period"), lblEmpNo.Text);
            ut.BranchTargetPriorityVanMonthlyByUser(bll.sGetControlParameter("period"), lblEmpNo.Text);

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", lblEmpNo.Text));
            arr.Add(new cArrayList("@period", lblPeriod.Text));
            bll.vBindingGridToSp(ref grd, "sp_tmst_PriorityProductMailsByUser_get", arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "HideProgress()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "HideProgress()", true);
            throw;
            
        }
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "HideProgress()", true);
    }
}