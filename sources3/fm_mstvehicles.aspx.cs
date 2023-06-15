using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstvehicles : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbtype, "vhc_typ");
            bll.vBindingFieldValueToCombo(ref cbstatus, "vhc_status");
            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_get2", "emp_cd", "emp_nm");
            bll.vBindingFieldValueToCombo(ref cbuom, "uom");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
      
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vhc_cd",txvhccode.Text));
	    arr.Add(new cArrayList("@vhc_typ",cbtype.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@vhc_status",cbstatus.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@vhc_no", txvhcno.Text));
	    arr.Add(new cArrayList("@emp_cd",cbemployee.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@salespointcd",cbsalespoint.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@deleted",0));
	    arr.Add(new cArrayList("@capacity", txcapacity.Text));
        arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
       
        bll.vInsertMstVehicle(arr);
        Response.Redirect("fm_mstvehiclelist.aspx");
       
    }
}