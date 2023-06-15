using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_MerchandiserAttendance : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string emp_cd = Request.QueryString["emp_cd"].ToString();
            string period = Request.QueryString["period"].ToString();

            txtEmployee.Text = emp_cd;
            txtPeriod.Text = period;

            arr.Add(new cArrayList("@salesman_cd", emp_cd));
            arr.Add(new cArrayList("@period_cd", period));
            bll.vBindingGridToSp(ref grd, "sp_merchandiser_atten", arr);
            txtTotAttendance.Text = Convert.ToString(grd.Rows.Count);
        }
    }
   
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        string emp_cd = Request.QueryString["emp_cd"].ToString();
        string period = Request.QueryString["period"].ToString();
        arr.Add(new cArrayList("@salesman_cd", emp_cd));
        arr.Add(new cArrayList("@period_cd", period));
        bll.vBindingGridToSp(ref grd, "sp_merchandiser_atten", arr);
    }
}