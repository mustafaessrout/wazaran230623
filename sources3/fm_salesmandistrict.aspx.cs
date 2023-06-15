using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

public partial class fm_salesmandistrict : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbsalesman.Text = Request.QueryString["sal"];
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", Request.QueryString["sal"]));
            bll.vBindingGridToSp(ref grd, "sp_tmst_location_salesman_get", arr);
            arr.Clear();
            arr.Add(new cArrayList("@loc_typ", "CIT"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcity, "sp_tmst_location_getbytype", "loc_cd", "loc_nm", arr);

        }
    }

    protected void cbcity_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", cbcity.SelectedValue));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbdistrict, "sp_tmst_location_getbylocation", "loc_cd", "loc_nm", arr);
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@loc_cd", cbdistrict.SelectedValue));
        arr.Add(new cArrayList("@emp_cd", Request.QueryString["sal"]));
        bll2.vInsertMstLocationSalesman(arr);
        arr.Clear();
        arr.Add(new cArrayList("@emp_cd", Request.QueryString["sal"]));
        bll.vBindingGridToSp(ref grd, "sp_tmst_location_salesman_get", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('New salesman district has been added successfully !','New District salesman','success');", true);
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hddistrict = (HiddenField)grd.Rows[e.RowIndex].FindControl("hddistrict");
        string _sql = "delete tmst_location_salesman where emp_cd='" + Request.QueryString["sal"] + "' and loc_cd='" + hddistrict.Value + "'";
        bll.vExecuteSQL(_sql);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Request.QueryString["sal"]));
        bll.vBindingGridToSp(ref grd, "sp_tmst_location_salesman_get", arr);
    }
}