using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_activeDriver : System.Web.UI.Page
{
    cbll bll = new cbll();
    Utitlity ut = new Utitlity();
    cdal cdl = new cdal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsalespoint_SelectedIndexChanged(sender, e);
            btnew.Text = "New";
        }
    }

    protected void cbsalespoint_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@job_title_cd", "5"));
        arr.Add(new cArrayList("@level_cd", "1"));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));

        bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_nm", arr);
        arr.Clear();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getBySalespoint", arr);
        cbIsActive.SelectedValue = "Yes";
        cbIsBSV.SelectedValue = "Yes";
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblEmpno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblEmpno");
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", Convert.ToString(lblEmpno.Text)));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        dt = cdl.GetValueFromSP("sp_tmst_activedriver_getByemp", arr);

        if (dt.Rows.Count > 0)
        {
            // Bind control
            arr.Clear();
            arr.Add(new cArrayList("@job_title_cd", "5"));
            arr.Add(new cArrayList("@level_cd", "1"));
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));

            bll.vBindingComboToSp(ref cbemployee, "sp_tmst_employee_getbyjobtitle", "emp_cd", "emp_nm", arr);

            cbemployee.SelectedValue = Convert.ToString(dt.Rows[0]["emp_cd"]);
            cbIsBSV.SelectedValue = Convert.ToString(dt.Rows[0]["isBSVValue"]);
            cbIsActive.SelectedValue = Convert.ToString(dt.Rows[0]["isActiveValue"]);
            btnew.Text = "Update";
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();

        bool isBSV = (cbIsBSV.SelectedValue == "Yes" ? true : false);
        bool isActive = (cbIsActive.SelectedValue == "Yes" ? true : false);

        if (btnew.Text == "Update")
        {
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd",cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@isBSV", isBSV));
            arr.Add(new cArrayList("@isActive", isActive));
            bll.vUpdActivedriver(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Updated successfully ','Updated Success','success');", true);
            btnew.Text = "New";

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getBySalespoint", arr);
        }
        else if (btnew.Text == "New")
        {
            string Empno = string.Empty;
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            arr.Add(new cArrayList("@emp_cd", cbemployee.SelectedValue.ToString()));
            arr.Add(new cArrayList("@isBSV", isBSV));
            arr.Add(new cArrayList("@isActive", isActive));
            bll.vInsActivedriver(arr, ref Empno);

            if (Empno == "-1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee already available ','Employee already available ','warning');", true);
            }
            if (Empno == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Record Inserted successfully ','Insert Success','success');", true);
                btnew.Text = "New";
            }

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_activedriver_getBySalespoint", arr);

        }
    }
}