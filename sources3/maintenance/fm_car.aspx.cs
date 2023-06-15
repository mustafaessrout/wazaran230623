using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class maintenance_fm_car : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbplatetype, "plate_typ");
            bll.vBindingFieldValueToCombo(ref cbcolor, "color",1);
            bll.vBindingFieldValueToCombo(ref cbbranded, "branded_cd");
            bll.vBindingFieldValueToCombo(ref cbmodel, "model_cd");
            bll.vBindingFieldValueToCombo(ref cbvhctype, "vhc_typ");
            bll.vBindingFieldValueToCombo(ref cbbusinessunit, "bussunit_cd");
            bll.vBindingFieldValueToCombo(ref cbownership, "ownership");
            bll.vBindingFieldValueToCombo(ref cbvhcstatus, "vhc_sta_id");
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        }
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@vhc_cd", hdcar.Value.ToString()));
        bll.vGetMtnMstVehicle(arr, ref rs);
        while (rs.Read())
        { 
            lbvhcode.Text = rs["vhc_cd"].ToString();
            txplateno.Text = rs["plate_no"].ToString();
            txengineno.Text = rs["engine_no"].ToString();
            cbbranded.SelectedValue = rs["branded_cd"].ToString();
            cbbusinessunit.SelectedValue = rs["bussunit_cd"].ToString();
            cbmodel.SelectedValue = rs["model_cd"].ToString();
            txyear.Text = rs["builtyear"].ToString();
            txremark.Text = rs["remark"].ToString();
            dtpurchase.Text = Convert.ToDateTime(rs["purchase_dt"]).ToString("d/M/yyyy");
            dtexpired.Text =  Convert.ToDateTime(rs["expired_dt"]).ToString("d/M/yyyy");
            txpurchaseprice.Text = rs["buyprice"].ToString();
            txboxprice.Text = rs["boxprice"].ToString();
            dtbox.Text = Convert.ToDateTime(rs["box_dt"]).ToString("d/M/yyyy");
            cbplatetype.SelectedValue = rs["plate_typ"].ToString();
            txinsuranceno.Text = rs["insurance_no"].ToString();
            cbvhcstatus.SelectedValue = rs["vhc_sta_id"].ToString();
            hdemp.Value = rs["emp_cd"].ToString();
            txemp.Text = bll.vLookUp("select emp_cd+':'+emp_nm from tmst_employee where emp_cd='"+hdemp.Value.ToString()+"'");
          //  cbcolor.SelectedValue = rs["color_cd"].ToString();
            vDisableControl(true);
        }
        rs.Close();
    }

    void vDisableControl(bool bFlag)
    {
        if (bFlag)
        {
            lbvhcode.CssClass = cd.csstextro;
            txplateno.CssClass = cd.csstextro;
            txengineno.CssClass = cd.csstextro;
            cbbranded.CssClass = cd.csstextro;
            cbmodel.CssClass = cd.csstextro;
            cbbusinessunit.CssClass = cd.csstextro;
            txyear.CssClass = cd.csstextro;
            txremark.CssClass = cd.csstextro;
            dtpurchase.CssClass = cd.csstextro;
            dtexpired.CssClass = cd.csstextro;
            txpurchaseprice.CssClass = cd.csstextro;
            txboxprice.CssClass = cd.csstextro;
            cbplatetype.CssClass = cd.csstextro;
            dtbox.CssClass = cd.csstextro;
           // cbcolor.CssClass = cd.csstextro;
            txinsuranceno.CssClass = cd.csstextro;
            cbvhcstatus.CssClass = cd.csstextro;
            txemp.CssClass = cd.csstextro;
        }
        else
        {
            lbvhcode.CssClass = cd.csstext;
            txplateno.CssClass = cd.csstext;
            txengineno.CssClass = cd.csstext;
            cbbranded.CssClass = cd.csstext;
            cbmodel.CssClass = cd.csstext;
            cbbusinessunit.CssClass = cd.csstext;
            txyear.CssClass = cd.csstext;
            txremark.CssClass = cd.csstext;
            dtpurchase.CssClass = cd.csstext;
            dtexpired.CssClass = cd.csstext;
            txpurchaseprice.CssClass = cd.csstext;
            txboxprice.CssClass = cd.csstext;
            cbplatetype.CssClass = cd.csstext;
            dtbox.CssClass = cd.csstext;
          //  cbcolor.CssClass = cd.csstext;
            txinsuranceno.CssClass = cd.csstext;
            cbvhcstatus.CssClass = cd.csstext;
            txemp.CssClass = cd.csstext;
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_car.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sVhc = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@vhc_cd",sVhc));
	    arr.Add(new cArrayList("@plate_no", txplateno.Text));
	    arr.Add(new cArrayList("@plate_typ", cbplatetype.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@engine_no",txengineno.Text));
	    arr.Add(new cArrayList("@body_no", txbodyno.Text));
	    arr.Add(new cArrayList("@branded_cd", cbbranded.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@model_cd" , cbmodel.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@vhc_typ", cbvhctype.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@builtyear", txyear.Text));
	    arr.Add(new cArrayList("@color_cd" , cbcolor.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@purchase_dt" , System.DateTime.ParseExact( dtpurchase.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@expired_dt", System.DateTime.ParseExact(dtexpired.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@bussunit_cd" , cbbusinessunit.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
	    arr.Add(new cArrayList("@buyprice" , txpurchaseprice.Text));
	    arr.Add(new cArrayList("@boxprice" , txboxprice.Text));
	    arr.Add(new cArrayList("@box_dt", System.DateTime.ParseExact(dtbox.Text , "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
	    arr.Add(new cArrayList("@emp_cd" , hdemp.Value.ToString()));
	    arr.Add(new cArrayList("@remark" , txremark.Text));
	    arr.Add(new cArrayList("@vhc_sta_id", "A"));
	    arr.Add(new cArrayList("@insurance_no" , txinsuranceno.Text));
	    arr.Add(new cArrayList("@ownership" , cbownership.SelectedValue.ToString()));
        bll.vInsertMtnMstVehicle(arr, ref sVhc);
        lbvhcode.Text = sVhc;
        vDisableControl(true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('new car has been saved!','Vehicle Code:','success');", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        //arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        vDisableControl(false);
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=car');", true);
    }
    protected void btprintvhc_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=carprivate&carprivs="+hdcar.Value.ToString()+"');", true);
    }
}