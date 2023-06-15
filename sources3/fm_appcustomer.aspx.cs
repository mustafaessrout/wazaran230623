using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_appcustomer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
           // arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cust_sta_id", "D"));
            bll.vBindingGridToSp(ref grdapp, "sp_tmst_customer_getbystatus", arr);
        }
    }
    protected void grdapp_RowEditing(object sender, GridViewEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.EditIndex = e.NewEditIndex;
      //  arr.Add(new cArrayList("@cust_sta_id", "N"));
        arr.Add(new cArrayList("@cust_sta_id", "D"));
        bll.vBindingGridToSp(ref grdapp, "sp_tmst_customer_getbystatus", arr);
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.NewEditIndex].FindControl("cbapp");
        bll.vBindingFieldValueToCombo(ref cbapp, "app_sta_id");
    }
    protected void grdapp_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbcustcode = (Label)grdapp.Rows[e.RowIndex].FindControl("lbcustcode");
        DropDownList cbapp = (DropDownList)grdapp.Rows[e.RowIndex].FindControl("cbapp");
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        //arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@doc_typ", "customer"));
        //arr.Add(new cArrayList("@app_sta_id", cbapp.SelectedValue.ToString()));
        //bll.vUpdateWrkApproval(arr);
        arr.Add(new cArrayList("@cust_cd", lbcustcode.Text));
        arr.Add(new cArrayList("@cust_sta_id", "A"));
        bll.vUpdateMstCustomer(arr);
        arr.Clear();
        grdapp.EditIndex = -1;
        arr.Add(new cArrayList("@cust_sta_id", "D"));
     //   arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdapp, "sp_tmst_customer_getbystatus", arr);
    }
    protected void grdapp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        grdapp.EditIndex = -1;
        arr.Add(new cArrayList("@cust_sta_id", "D"));
        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdapp, "sp_appmstcustomer_get", arr);
    }
}