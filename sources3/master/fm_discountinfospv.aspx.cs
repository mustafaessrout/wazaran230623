using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_discountinfospv : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (!IsPostBack)
        {
            //arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbyspv", arr);
            //lbspv.Text = Request.Cookies["usr_id"].Value.ToString() + "-" + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
            vBindingGrid();
        }
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        vBindingGrid();
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbyspv", arr);
        lbspv.Text = Request.Cookies["usr_id"].Value.ToString() + "-" + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
    }
}