using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppClassTools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using System.Web.Configuration;

public partial class fm_mstempentryBasicUpdate : System.Web.UI.Page
{
    AppClass app = new AppClass();
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
            bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
            bll.vBindingComboToSp(ref cbrole, "sp_tmst_role_get", "role_cd", "roleName");
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbsp.SelectedValue = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'");
            bll.vBindingComboToSp(ref ddlDepartment, "sp_tmst_department_get", "dept_cd", "dept_nm");
            msg.Visible = false;
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetMstSalespoint(arr, ref rs);
            Ping pg = new Ping();
            PingReply reply = pg.Send("172.16.1.18");
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                lblHOStat.Text = "disconnected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#e74c3c 80%,#a7f1c6)");
                hdfHOConnected.Value = "false";
            }
            else
            {
                lblHOStat.Text = "connected";
                dvHOStatusValue.Style.Add("box-shadow", "inset 0 0 5px rgba(200, 236, 214, 0.71)");
                dvHOStatusValue.Style.Add("background", "radial-gradient(#2ecc71 80%,#a7f1c6)");
                hdfHOConnected.Value = "true";
            }
            while (rs.Read())
            {
                lblBranchName.Text = rs["salespoint_nm"].ToString();
            }
            rs.Close();
            //  MakeReadOnly();
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (hdfHOConnected.Value == "false")
        {
            app.BootstrapAlert(lblMsg, "HO not connected", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Join date must in date','Error');", true);
            return;
        }
        if (dtjoin.Text == "")
        {
            app.BootstrapAlert(lblMsg, "Join date must in date", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Join date must in date','Error');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", txempcode.Text));
        arr.Add(new cArrayList("@emp_nm", txempname.Text));
        arr.Add(new cArrayList("@nationality", cbnationality.SelectedValue));
        arr.Add(new cArrayList("@married_sta_id", cbmarried.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_sn", txshortname.Text));
        arr.Add(new cArrayList("@dept_cd", ddlDepartment.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'")));//cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@join_dt", DateTime.ParseExact(dtjoin.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertMstEmployee(arr);


        arr.Clear();
        MakeReadOnly();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('','" + txempcode.Text + ":" + txempname.Text + "','success');", true);
    }
    protected void btsearch_ServerClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('lookup_emp.aspx');", true);
    }

    void MakeReadOnly()
    {
       

        txempcode.CssClass = "form-control input-sm ro";
        txempname.CssClass = "form-control input-sm ro";
        //dtjoin.CssClass = "form-control input-sm ro";
        txshortname.CssClass = "form-control input-sm ro";
        //cbjobtitle.CssClass = "form-control-static input-sm ro";
        //cbmarried.CssClass = "form-control-static input-sm ro";
        //cbnationality.CssClass = "form-control-static input-sm ro";
        cbsp.CssClass = "form-control-static input-sm ro";
        //cblevel.CssClass = "form-control-static input-sm ro";
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        dtjoin.Text = string.Empty;
        txshortname.Text = string.Empty;
        cbsp.SelectedValue = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'");
        bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
        bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
        bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
        bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
        bll.vBindingComboToSp(ref cbrole, "sp_tmst_role_get", "role_cd", "roleName");
        bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        bll.vBindingComboToSp(ref ddlDepartment, "sp_tmst_department_get", "dept_cd", "dept_nm");


        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vGetMstEmployee(ref rs, arr);
        while (rs.Read())
        {
            txempcode.Text = rs["emp_cd"].ToString();
            txempname.Text = rs["emp_nm"].ToString();
            if (!rs["emp_sn"].Equals(DBNull.Value))
            {
                txshortname.Text = rs["emp_sn"].ToString();
            }
            if (!rs["nationality"].Equals(DBNull.Value))
            {
                var nationality = Convert.ToInt32(bll.vLookUp("SELECT count(*) FROM tfield_value WHERE fld_nm='nationality' AND isactive=1 AND fld_valu='" + rs["nationality"].ToString() + "'"));
                if (nationality > 0)
                {
                    cbnationality.SelectedValue = rs["nationality"].ToString();
                }
            }
            if (!rs["married_sta_id"].Equals(DBNull.Value))
            {
                var married = Convert.ToInt32(bll.vLookUp("SELECT count(*) FROM tfield_value WHERE fld_nm='married_sta_id' AND isactive=1 AND fld_valu='" + rs["married_sta_id"].ToString() + "'"));
                if (married > 0)
                {
                   cbmarried.SelectedValue = rs["married_sta_id"].ToString();
                }
            }
            if (!rs["join_dt"].Equals(DBNull.Value))
            {
                dtjoin.Text = String.Format("{0:dd/MM/yyyy}", rs["join_dt"]);
            }
            if (!rs["salespointcd"].Equals(DBNull.Value))
            {
                cbsp.SelectedValue = rs["salespointcd"].ToString();
            }
            if (!rs["level_cd"].Equals(DBNull.Value))
            {
                cblevel.SelectedValue = rs["level_cd"].ToString();
            }
            if (!rs["job_title_cd"].Equals(DBNull.Value))
            {
                var title_cd = Convert.ToInt32(bll.vLookUp("SELECT count(*) FROM tfield_value WHERE fld_nm='job_title_cd' AND isactive=1 AND fld_valu='" + rs["job_title_cd"].ToString() + "'"));
                if (title_cd > 0)
                {
                    cbjobtitle.SelectedValue = rs["job_title_cd"].ToString();
                }
            }
            if (!rs["dept_cd"].Equals(DBNull.Value))
            {
                var dep = Convert.ToInt32(bll.vLookUp("select count(*) from tmst_department where dept_cd='" + rs["dept_cd"].ToString() + "'"));
                if (dep > 0)
                {
                    ddlDepartment.SelectedValue = rs["dept_cd"].ToString();
                }
            }
            cbsp.SelectedValue = rs["salespointcd"].ToString();
        }
        rs.Close();

        DataTable dtUserProfile = new DataTable();
        dtUserProfile = cdl.GetValueFromSP("sp_tuser_profile_getbyempcd", arr);
        if (dtUserProfile.Rows.Count > 0) {
            txtemail.Text = Convert.ToString(dtUserProfile.Rows[0]["email"]);
            txtmobile.Text = Convert.ToString(dtUserProfile.Rows[0]["mobile_no"]);
            cbrole.SelectedValue = Convert.ToString(dtUserProfile.Rows[0]["role_cd"]);
            cbrole.CssClass = "form-control-static input-sm ro";
            txtmobile.CssClass = "form-control input-sm ro";
            txtemail.CssClass = "form-control input-sm ro";
        }

        MakeReadOnly();
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        txempname.CssClass = "form-control input-sm";
        txshortname.CssClass = "form-control input-sm";
        cbjobtitle.CssClass = "form-control-static input-sm";
        cbmarried.CssClass = "form-control-static input-sm";
        cbnationality.CssClass = "form-control-static input-sm";
        cbsp.CssClass = "form-control-static input-sm";
        cblevel.CssClass = "form-control-static input-sm";
        dtjoin.CssClass = "form-control input-sm";
    }
    protected void CogLinkButton_Click(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        txempname.CssClass = "form-control input-sm";
        txshortname.CssClass = "form-control input-sm";
        cbjobtitle.CssClass = "form-control-static input-sm";
        cbmarried.CssClass = "form-control-static input-sm";
        cbnationality.CssClass = "form-control-static input-sm";
        cbsp.CssClass = "form-control-static input-sm";
        cblevel.CssClass = "form-control-static input-sm";
        dtjoin.CssClass = "form-control input-sm";
        txempcode.Text = ""; txempname.Text = "";
        txempname.Text = "";
        txshortname.Text = "";
    }


    protected void btnHO_Click(object sender, EventArgs e)
    {
        //Response.Redirect("../fm_loginhoAutoLogin.aspx?emp_cd=" + txempcode.Text);
    }

    protected void btnRefesh_Click(object sender, EventArgs e)
    {

    }
    protected void btSearchHO_Click(object sender, EventArgs e)
    {
        try
        {
            Ping pg = new Ping();
            PingReply reply = pg.Send("172.16.1.18");
            bool status = reply.Status == IPStatus.Success;
            if (status == false)
            {
                dvHOStatusValue.Style.Add("background-color", "red");
                hdfHOConnected.Value = "false";
            }
            else
            {
                dvHOStatusValue.Style.Add("background-color", "green");
                hdfHOConnected.Value = "true";
            }
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Employee", "New Employee", "fm_mstempentryBasicUpdate", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnWazaranLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtmobile.Text == "")
            {
                app.BootstrapAlert(lblMsg, "Enter mobile no", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                return;
            }
            else if (txtemail.Text == "")
            {
                app.BootstrapAlert(lblMsg, "Enter email id", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                return;
            }
            else if (txempcode.Text == "")
            {
                app.BootstrapAlert(lblMsg, "Select Employee", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                return;
            }
            else if (txempname.Text == "")
            {
                app.BootstrapAlert(lblMsg, "Select Employee", app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", txempname.Text.ToString().Substring(0, 3) + txempcode.Text.ToString()));
            arr.Add(new cArrayList("@fullname", txempname.Text));
            arr.Add(new cArrayList("@email", txtemail.Text));
            arr.Add(new cArrayList("@mobile_no", txtmobile.Text));
            arr.Add(new cArrayList("@emp_cd", txempcode.Text));
            arr.Add(new cArrayList("@passwd", "Sbtc2015"));
            arr.Add(new cArrayList("@salespointcd", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='salespoint'")));//cbsp.SelectedValue.ToString()));
            arr.Add(new cArrayList("@role_cd", cbrole.SelectedValue));
            bll.vInsertTuserProfile(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('','User ID:- " + txempname.Text.ToString().Substring(0, 3) + txempcode.Text.ToString() + ", Password:- " + "Sbtc2015" + "','success');", true);
        }
        catch (Exception ex)
        {
            app.BootstrapAlert(lblMsg, ex.Message + " " + ex.InnerException, app.alertType = AppClassTools.AppClass.BootstrapAlertType.Danger, true);
            ut.Logs("", "Employee", "New Employee", "fm_mstempentryBasicUpdate", "Page_Load", "Exception", ex.Message + ex.InnerException);
        }
    }
}