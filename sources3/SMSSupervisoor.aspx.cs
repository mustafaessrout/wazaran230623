using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SMSSupervisoor : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblSupervisorName.Text = bll.GetSupervisoor();
        string[] arr = bll.GetSupervisoor().Split('[');
        hdfMobileNumber.Value = Convert.ToString(arr[1].Split(']')[0]);
    }

    protected void btSend_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtsms.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please insert message','Please insert message','info');", true);
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@to", hdfMobileNumber.Value));
                arr.Add(new cArrayList("@msg", txtsms.Text));
                arr.Add(new cArrayList("@token", ""));
                arr.Add(new cArrayList("@doc_no", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@doc_typ", "SMSSupervisoor"));
                bll.vInsertSmsOutbox(arr);
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            ut.Logs("", "Tools", "SMS Supervisoor", "SMSSupervisoor", "Send SMS", "Exception", ex.Message + ex.InnerException);
        }
    }
}