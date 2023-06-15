using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class us_News : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static List<Notification> GetNotification(string userID)
    {
        cdal cdl = new cdal();
        List<Notification> lst = new List<Notification>();
        List<cArrayList> drNotification = new List<cArrayList>();
        drNotification.Add(new cArrayList("@userID", userID));
        DataTable dtNotification = new DataTable();
        dtNotification = cdl.GetValueFromSP("sp_notification_get", drNotification);
        foreach (DataRow dr in dtNotification.Rows)
        {
            Notification nt = new Notification();
            nt.isActive = Convert.ToBoolean(dr["isActive"]);
            nt.notificationDate = Convert.ToDateTime(dr["notificationDate"]).ToString("dd-MMM-yyyy-HH:MM:ss");
            nt.notificationDesc = Convert.ToString(dr["notificationDesc"]);
            nt.notificationSortMessage = Convert.ToString(dr["notificationSortMessage"]);
            nt.titleSort = Convert.ToString(dr["titleSort"]);
            nt.descSort = Convert.ToString(dr["descSort"]);
            nt.notificationID = Convert.ToString(dr["notificationID"]);
            nt.usr_id = Convert.ToString(dr["usr_id"]);
            nt.fullname = Convert.ToString(dr["fullname"]);
            nt.email = Convert.ToString(dr["email"]);
            nt.mobile_no = Convert.ToString(dr["mobile_no"]);
            nt.emp_cd = Convert.ToString(dr["emp_cd"]);
            lst.Add(nt);
        }

        return lst;
    }

}


public class Notification
{
    public string notificationID { get; set; }
    public string notificationSortMessage { get; set; }
    public string notificationDesc { get; set; }
    public string titleSort { get; set; }
    public string descSort { get; set; }
    public string notificationDate { get; set; }
    public bool isActive { get; set; }
    public string usr_id { get; set; }
    public string fullname { get; set; }
    public string email { get; set; }
    public string mobile_no { get; set; }
    public string emp_cd { get; set; }
}