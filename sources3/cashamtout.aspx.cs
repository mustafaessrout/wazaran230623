using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cashamtout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            string sID = Request.QueryString["am"];
            lbamoutout.Text = sID;
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@cash_id", sID));
            //bll.vGetCashRegisterByCashID(arr, ref rs);
            //while (rs.Read())
            //{ 
            //    lbamoutout.Text = rs["amt"].ToString();
            //    lbrefno.Text = rs["ref_no"].ToString();
            //    lbitemconame.Text = rs["itemco_nm"].ToString();
            //    lbpic.Text = rs["pic"].ToString();
            //} rs.Close();
        }
    }
    protected void btno_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }
    protected void btyes_Click(object sender, EventArgs e)
    {
        string sCashID= Request.QueryString["id"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cash_id", sCashID));
        arr.Add(new cArrayList("@cash_sta_id", "C"));
        bll.vUpdateCashRegister(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl11", "window.opener.RefreshData();window.close();", true);
    }
}