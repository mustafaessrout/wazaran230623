using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_promotor2 : System.Web.UI.MasterPage
{
    
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        if (!IsPostBack)
        {
            //if ((Request.Cookies["exh_dt"] == null) || (Request.Cookies["exh_cd"]==null))
            //{
            //    Response.Redirect("fm_login.aspx");
            //}
        }
    }
    protected void btLogout_Click(object sender, EventArgs e)
    {
        Response.Cookies.Clear();
        Response.Redirect("fm_login.aspx");
    }

    [WebMethod]
    public static List<Notification> GetNotification()
    {
        cdal cdl = new cdal();
        List<Notification> lst = new List<Notification>();
        List<cArrayList> drNotification = new List<cArrayList>();
        DataTable dtNotification = new DataTable();
        dtNotification = cdl.GetValueFromSP("sp_notification_get", drNotification);
        return lst;
    }

    
   
}

public class Notification
{
    public string notificationSortMessage { get; set; }
    public string notificationDesc { get; set; }
    public DateTime notificationDate { get; set; }
    public bool isActive { get; set; }
}