using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_drivertaken : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            dtreceived.Text = Request.Cookies["waz_dt"].Value.ToString();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        DateTime dt;
        if (dtreceived.Text=="")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alr", "sweetAlert('Date received not properly set','Use format : d/m/yyyy','warning');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        arr.Add(new cArrayList("@driver_sta_id", "Y"));
        bll.vUpdateTsalesorderInfoByDriverStaID(arr);
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        arr.Add(new cArrayList("@do_dt", System.DateTime.ParseExact(dtreceived.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vUpdateMstDoSalesByDate(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "window.close();", true);
    }
}