using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockopschedule : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtschedule.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
            cbperiod.SelectedValue = bll.sGetControlParameter("period");
            bll.vBindingFieldValueToCombo(ref cbstocktype, "stockop_typ");
            cbstocktype_SelectedIndexChanged(sender, e);
            cbperiod_SelectedIndexChanged(sender,e);
        }
    }

    private void bindinggrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        arr.Add(new cArrayList("@stocktype", cbstocktype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tstockopname_schedule_get", arr);
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        DateTime ddate = DateTime.ParseExact(dtschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        var dateAsString = ddate.ToString("yyyy-MM-dd");
        string swhs_cd = bll.vLookUp("select whs_cd from tstockopname_schedule where whs_cd='" + cbwhs.SelectedValue + "' and period=dbo.fnFormatDate ('"+ dateAsString+"', 'YYYYMM') ");
        DateTime dtadjjaret_dt = DateTime.ParseExact(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='adjjaret_dt'"), "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture);
        if (ddate > dtadjjaret_dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('schedule jaret date can not greater then '"+dtadjjaret_dt+" ,'error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (swhs_cd == cbwhs.SelectedValue)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Already have schedule jaret !','one month only 1 times schedule jaret','error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (dtschedule.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Schedule date must be fill ','','warning');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@schedule_dt", System.DateTime.ParseExact(dtschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@rdstockop", cbstocktype.SelectedValue.ToString()));
            arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
            bll.vInsertStockOpnameSchedule(arr);
            bindinggrid();
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add Schedule stock opname");
        }
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            HiddenField lbids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
            Label lbschedule = (Label)grd.Rows[e.RowIndex].FindControl("dtschedule");

            string waz_dt = Request.Cookies["waz_dt"].Value.ToString();
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dschedule = DateTime.ParseExact(lbschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (dschedule<=dtwaz_dt)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Can not delete old transaction ','','warning');", true);
                return;
            }
            else
            {
                arr.Add(new cArrayList("@ids", lbids.Value.ToString()));
                bll.vDelStockOpnameSchedule(arr);
            }
            
            bindinggrid();
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Delete Opname Schedule");
        }
    }
    

    protected void dtschedule_TextChanged(object sender, EventArgs e)
    {
        string waz_dt;
        DateTime dtFrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtTo = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        waz_dt = Request.Cookies["waz_dt"].Value.ToString();
        DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dschedule = DateTime.ParseExact(dtschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (!(dschedule>=dtFrom && dschedule<=dtTo))
        {
            dtschedule.Text = txto.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Schedule date not in this period ','','warning');", true);
            return;
        }
        if (dschedule < dtwaz_dt)
        {
            dtschedule.Text = Request.Cookies["waz_dt"].Value.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Schedule date can not less than system date ','','warning');", true);
            return;
            
        }
   
        bindinggrid();
    }
    protected void cbperiod_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strFrom, strTo;
        strFrom = bll.vLookUp("select convert(varchar, ymtStart, 103)  from tblTRYearMonth where period='" + cbperiod.SelectedValue + "'");
        strTo = bll.vLookUp("select  convert(varchar,ymtEnd, 103) from tblTRYearMonth where period='" + cbperiod.SelectedValue + "'");
        txfrom.Text = strFrom;
        txto.Text = strTo;
        bindinggrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=stocksch&period="+cbperiod.SelectedValue.ToString()+"');", true);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        arr.Add(new cArrayList("@stocktype", cbstocktype.SelectedValue));
        Session["lParamstocksch"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stocksch');", true);
    }
    protected void cbstocktype_SelectedIndexChanged(object sender, EventArgs e)
    {
       
       
        if (cbstocktype.SelectedValue.ToString() == "VS")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_vehicle_salesman_get", "vhc_cd", "vhc_desc", arr);
            lbdepo.Text = "SALESMAN";
            bindinggrid();
        }
        else if (cbstocktype.SelectedValue.ToString() == "DEPO")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", 2));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd", "whs_nm", arr);
            lbdepo.Text = "SUB DEPO";  cbwhs.CssClass = "divnormal form-control";
            bindinggrid();
        }
    }


    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Label dtschedule = (Label)grd.Rows[e.NewEditIndex].FindControl("dtschedule");
        string waz_dt = Request.Cookies["waz_dt"].Value.ToString();
        DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dschedule = DateTime.ParseExact(dtschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dschedule < dtwaz_dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Schedule  should be greater then system Date','error');", true);
            grd.EditIndex = -1;
            bindinggrid();
            return;
        }
        DateTime dtadjjaret_dt = Convert.ToDateTime(bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='''adjjaret_dt'''"));
        if (dschedule > dtadjjaret_dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('schedule jaret date can not greater then '" + dtadjjaret_dt + " ,'error');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        grd.EditIndex = e.NewEditIndex;
        bindinggrid();
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        bindinggrid();
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        HiddenField lbids = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdids");
        TextBox txschedule_dt = (TextBox)grd.Rows[e.RowIndex].FindControl("txschedule_dt");
        string waz_dt;
        DateTime dtFrom = DateTime.ParseExact(txfrom.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtTo = DateTime.ParseExact(txto.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        waz_dt = Request.Cookies["waz_dt"].Value.ToString();
        DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dschedule = DateTime.ParseExact(txschedule_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (!(dschedule >= dtFrom && dschedule <= dtTo))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Schedule date not in this period ','','warning');", true);
            return;
        }
        if (dschedule < dtwaz_dt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Schedule date can not less than system date ','','warning');", true);
            return;

        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ids", lbids.Value));
        arr.Add(new cArrayList("@schedule_dt", dschedule));
        bll.vUpdateStockOpnameSchedule(arr);
        grd.EditIndex = -1; arr.Clear();
        bindinggrid();
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

        Label dtschedule = (Label)grd.Rows[e.NewSelectedIndex].FindControl("dtschedule");
        HiddenField hdwhs_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdwhs_cd");
        DateTime dschedule = DateTime.ParseExact(dtschedule.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue));
        arr.Add(new cArrayList("@whs_cd", hdwhs_cd.Value));
        arr.Add(new cArrayList("@sched_dt", dschedule));
        Session["lParamstockschentry"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stockschentry');", true);
    }
}
