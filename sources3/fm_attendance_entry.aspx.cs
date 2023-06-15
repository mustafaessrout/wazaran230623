using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_attendance_entry : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {
            try
            {
                string sho, waz_dt;
                List<cArrayList> arr = new List<cArrayList>();
                hdno.Value = "NEW";
                waz_dt = Request.Cookies["waz_dt"].Value.ToString();
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dtattendance.Text = Request.Cookies["waz_dt"].Value.ToString();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_getuser", "salespointcd", "salespoint_desc", arr);
                cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value.ToString();
                cbSalesPointCD_SelectedIndexChanged(sender, e);
                sho = Request.Cookies["sp"].Value.ToString();
                if (sho == "0")
                {
                    cbSalesPointCD.Enabled = true;
                    cbSalesPointCD.CssClass = "";
                }
                else
                {
                    cbSalesPointCD.Enabled = false;
                    cbSalesPointCD.CssClass = "makeitreadonly ro form-control";
                }
                btnew.CssClass = "divhid";
                btsave.CssClass = "divhid";
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_attendance_entry");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void btgenerate_Click(object sender, EventArgs e)
    {
        try
        {
            string att_no;
            List<cArrayList> arr = new List<cArrayList>();
            if (hdno.Value == "" || hdno.Value == "NEW")
            {
                att_no = Request.Cookies["usr_id"].Value.ToString();
                arr.Clear();
                //arr.Add(new cArrayList("@att_no", att_no));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                arr.Add(new cArrayList("@attendance_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                bll.vAutoAttendanceEmployee(arr);
                arr.Clear();
                arr.Add(new cArrayList("@attendance_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
                bll.vBindingGridToSp(ref grdemployee, "sp_thrd_attendance_dtl_get", arr);
                btnew.CssClass = "btn btn-add btn-succes";
                btsave.CssClass = "btn btn-save btn-warning";
                btnew.Attributes.Remove("style");
                btsave.Attributes.Remove("style");
                btprint.Attributes.Remove("style");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_attendance_entry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbSalesPointCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtattendance.Text = bll.sGetControlParameterSalespoint("wazaran_dt", cbSalesPointCD.SelectedValue.ToString());
    }

    protected void grdemployee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txattdate = (TextBox)e.Row.FindControl("txattdate");
                //txattdate.Text = Request.Cookies["waz_dt"].Value.ToString();
                txattdate.CssClass = "form-control ro";
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_attendance_entry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_attendance_entry.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["waz_dt"].Value.ToString() != dtattendance.Text)
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Attention','Transaction Date Out Of Periode Date','error');", true);
                return;
            }
            string sAtt_no = "";
            List<cArrayList> arr = new List<cArrayList>();
            
                foreach (GridViewRow row in grdemployee.Rows)
                {
                    Label lbsalespointcd = (Label)row.FindControl("lbsalespointcd");
                    Label lbemp_cd = (Label)row.FindControl("lbemp_cd");
                    TextBox txattdate = (TextBox)row.FindControl("txattdate");
                    TextBox txcheckin = (TextBox)row.FindControl("txcheckin");
                    TextBox txcheckout = (TextBox)row.FindControl("txcheckout");

                    if (txcheckin.Text == "" || txcheckout.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check In and Check out must be entry...','Attendance. " + lbemp_cd.Text + "','warning');", true);
                        return;
                    }

                //var str = "5/12/2020";
                //DateTime dt;

                //var isValidDate = DateTime.TryParse(str, out dt);

                DateTime timeIn, timeOut;
                TimeSpan dTimeIn, dTimeOut;

                if (!TimeSpan.TryParse(txcheckin.Text, out dTimeIn) || !TimeSpan.TryParse(txcheckout.Text, out dTimeOut))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check In / Out , Format time not correct !!','Input Time','warning');", true);
                    return;
                }

                var vartimeIn = DateTime.TryParseExact(txcheckin.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.None,out timeIn);
                    var vartimeOut = DateTime.TryParseExact(txcheckout.Text, "HH:mm", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None,out timeOut);

                if (!(vartimeIn) && !(vartimeOut))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Check In : "+timeIn.ToString("HH:mm")+" and Check out : "+timeOut.ToString("HH:mm")+" must be entry...','Attendance. " + lbemp_cd.Text + "','warning');", true);
                    return;
                }

                    arr.Clear();
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    arr.Add(new cArrayList("@attendance_dt", DateTime.ParseExact(txattdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                    arr.Add(new cArrayList("@emp_cd", lbemp_cd.Text.ToString()));
                    arr.Add(new cArrayList("@checkin", txcheckin.Text.ToString().Replace(" ","")));
                    arr.Add(new cArrayList("@checkout", txcheckout.Text.ToString().Replace(" ","")));
                    bll.vUpdateAttendanceDtlAuto(arr);

                }
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@attendance_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                bll.vGenAttendanceBranch(arr);
                btprint.CssClass = "btn btn-info";
                btsave.CssClass = "divhid";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "swal('Data Save successfully ..','Attendance Date : " + dtattendance.Text + "','info');", true);

            arr.Clear();
            arr.Add(new cArrayList("@attendance_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grdemployee, "sp_thrd_attendance_dtl_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_attendance_entry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@start_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@end_dt", DateTime.ParseExact(dtattendance.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@salespointcd", cbSalesPointCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            Session["lParamAttendance"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=attendance');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString() + " : fm_attendance_entry");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}