using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_login : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        string sBA = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='"+Request.Cookies["usr_id"].Value.ToString()+"' and hoaccess_cd='homasterba'");
        string sClaim = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='"+Request.Cookies["usr_id"].Value.ToString()+"' and hoaccess_cd='homasterclaim'");
        string sAcc = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and hoaccess_cd='homasteracc'");
        string sHR = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and hoaccess_cd='homasterHR'");
        string sHRIncentive = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and hoaccess_cd='homasterHRIncentive'");
        string sHRMerchandiser = bll.vLookUp("select count(1) from tappho_userrole where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and hoaccess_cd='homasterHRMerchandiser'");
        Session["claim"] = sClaim;
        Session["ba"] = sBA;
        Session["acc"] = sAcc;
        Session["HR"] = sHR;
        Session["HRIncentive"] = sHRIncentive;
        Session["HRMerchandiser"] = sHRMerchandiser;
        Response.Redirect("default.aspx");

    }
}