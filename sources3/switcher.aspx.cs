using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class switcher : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<string> arr1 = new List<string>();
    List<string> arraspx = new List<string>();
    List<string> arrmenucaption = new List<string>();
    List<string> arrallmenu = new List<string>();
    List<string> arrallmenucaption = new List<string>();
    List<string> arrallaspx = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            List<cArrayList> arr= new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingComboToSp(ref cbapps, "sp_tapp_access_get", "urls","app_nm" ,arr);
            if (cbapps.Items.Count == 0)
            {
                btview.CssClass = "btn btn-default ro";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access to Head Office Apps!','Please contact Hendry Purnomo for open access','error');", true);
                return;
            }
            arr1= (List<string>) Session["themenu"] ;
            arraspx = (List<string>)Session["aspfile"];
            arrmenucaption = (List<string>)Session["menucaption"];
            arrallmenu = (List<string>)Session["allmenu"];
            arrallaspx = (List<string>)Session["allaspfile"];
            arrallmenucaption = (List<string>)Session["allmenucaption"];
        }
    }
    protected void btview_Click(object sender, EventArgs e)
    {
        if (cbapps.SelectedValue == "def")
        {
            Session["themenu"] = arr1;
            Session["aspfile"] = arraspx;
            Session["menucaption"] = arrmenucaption;
            Session["allmenu"] = arrallmenu;
            Session["allaspfile"] = arrallaspx;
            Session["allmenucaption"] = arrallmenucaption;
        }
        Response.Redirect(cbapps.SelectedValue.ToString());
    }
    protected void btBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_loginho2.aspx");
    }
}