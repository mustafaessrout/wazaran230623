using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class fm_loginho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Response.Redirect("fm_loginho2.aspx");
        }
    }
    protected void btlogin_Click(object sender, EventArgs e)
    {
        
        int n= bll.nCheckUserPassword(txusername.Text, txpassword.Text);
        if (n == 0)
        { ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Wrong password or username','Try Again','warning');", true); return; }
        else
        {
            Response.Cookies["usr_id"].Value = bll.vLookUp("select emp_cd from tuser_profile where usr_id='"+ txusername.Text +"'");
            Response.Cookies["fullname"].Value = txusername.Text;
            Response.Cookies["sp"].Value = "0";
            Response.Cookies["spn"].Value = "Head Office";
            Response.Cookies["waz_dt"].Value = System.DateTime.Today.ToString("d/M/yyyy");
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            List<string> arr1 = new List<string>();
            List<string> arraspx = new List<string>();
            List<string> arrmenucaption = new List<string>();
            List<string> arrallmenu = new List<string>();
            List<string> arrallmenucaption = new List<string>();
            List<string> arrallaspx = new List<string>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vCheckMenu(arr, ref rs);
            while (rs.Read())
            {
                arr1.Add(rs["menu_cd"].ToString());
                arraspx.Add(rs["aspfile"].ToString());
                arrmenucaption.Add(rs["menu_nm"].ToString());
            }
            rs.Close();
            bll.vGetMstMenu(ref rs);
            while (rs.Read())
            {
                arrallmenu.Add(rs["menu_cd"].ToString());
                arrallmenucaption.Add(rs["menu_nm"].ToString());
                arrallaspx.Add(rs["aspfile"].ToString());
            } rs.Close();
            Session["themenu"] = arr1;
            Session["aspfile"] = arraspx;
            Session["menucaption"] = arrmenucaption;
            Session["allmenu"] = arrallmenu;
            Session["allaspfile"] = arrallaspx;
            Session["allmenucaption"] = arrallmenucaption;
            //Start Remark if production  -----------------------------------
          //  Response.Cookies.Clear();
          //  Response.Cookies["usr_id"].Value = "2540";
         //   Response.Cookies["fullname"].Value = "Test Purposed";
         //   Response.Cookies["sp"].Value = "101";
           // Response.Cookies["spn"].Value = "xxxxx";
            Response.Cookies["lang"].Value = "EN";
           // Response.Cookies["waz_dt"].Value = bll.sGetControlParameter("wazaran_dt");
            Response.Redirect("default.aspx");
        
        }
    }
}