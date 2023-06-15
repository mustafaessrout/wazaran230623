using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_accClosePeriod : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    decimal totalDebit = 0;
    decimal totalCredit= 0;
    public static int PreviousIndex;
    Boolean hasApprovalRole = false;
    Boolean isEmptyGrid = false;
    string userid = null;
    //string userid2 = null;
    string closeOrOpen = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        userid = Request.Cookies["usr_id"].Value.ToString();
        //string userid = "2833";

        ////userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
        ////if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
        //if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
        //{
        //    hasApprovalRole = true;
        //}

        //if (!IsPostBack)
        //{
        //    //bindinggrd(hasApprovalRole, isEmptyGrid); 
        //}

        //if (hasApprovalRole)
        //{
        //    btReopenPeriod.Visible = false;
        //    btClosePeriod.Visible = false;
        //}

        lblPeriod.Text = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period_acc'");

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    protected void btClosePeriod_Click(object sender, EventArgs e)
    {
        closeOrOpen = "C";
        updateClosePeriod(closeOrOpen);
    }
    protected void btReopenPeriod_Click(object sender, EventArgs e)
    {
        closeOrOpen = "O";
        updateClosePeriod(closeOrOpen);
    }
    protected void updateClosePeriod(string closeOrOpen)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Clear();
        arr.Add(new cArrayList("@closeOrOpen", closeOrOpen));
        arr.Add(new cArrayList("@userId", userid));
        bll.vUpdateClosePeriod(arr);


        lblPeriod.Text = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period_acc'");
        if (closeOrOpen == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Success Close Period!','Close Period successfully');", true);

        }
        else if (closeOrOpen == "O")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Success Reopen Period!','Reopen Period successfully');", true);
        }
    }
}

                    