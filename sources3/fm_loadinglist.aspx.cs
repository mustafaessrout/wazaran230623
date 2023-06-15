using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_loadinglist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "driver"));
            bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
          //  bll.vBindingGridToSp(ref grd, "sp_tmst_dosales_get", arr);
            cbdriver_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkMstExpedition(arr);
            dttrip.Text = System.DateTime.Today.ToShortDateString();
            bll.sFormat2ddmmyyyy(ref dttrip);
            txtripno.Text = "NEW";
            btprintinvoice.Enabled = false;
            btprint.Enabled = false;
        //    bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
        }
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdono =  (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbdono");
        Label lbinvoiceno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbinvoiceno");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
        bll.vBindingGridToSp(ref grddtl, "sp_tdosalesinvoice_dtl_get", arr);
   //     Response.Redirect("fm_loadingsales.aspx?do=" + lbdono.Text ) ;
    }
    protected void cbdriver_SelectedIndexChanged(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        lbvehiclecode.Text = "";
        lbplate.Text = "";
        lbtype.Text = "";
        lbcapacity.Text ="";
        lbuom.Text = "";
        arr.Add(new cArrayList("@emp_cd", cbdriver.SelectedValue.ToString()));
        bll.vGetMstVehicleByEmpcd(arr, ref rs);
        while (rs.Read())
        { 
            lbvehiclecode.Text = rs["vhc_cd"].ToString();
            lbplate.Text = rs["vhc_no"].ToString();
            lbtype.Text = rs["vhc_typ_nm"].ToString();
            lbcapacity.Text = rs["capacity"].ToString();
            lbuom.Text = rs["uom_nm"].ToString();
        } rs.Close();
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (txmanualinvoice.Text.Equals(""))
        {
            ScriptManager.RegisterStartupScript(Page,Page.GetType(), "al", "alert('Manual Invoice must be fill !');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
	    arr.Add(new cArrayList("@do_no" , txloadingno.Text));
	    arr.Add(new cArrayList("@inv_no", lbinvoiceno.Text));
	    arr.Add(new cArrayList("@inv_manual_no", txmanualinvoice.Text));
	    arr.Add(new cArrayList("@pallette", lbpallette.Text));
        bll.vInsertWrkMstExpedition(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_mstexpedition_get", arr);
        txloadingno.Text = "";
        lbpallette.Text = "0";
        lbinvoiceno.Text = "";
        lbloadingmanual.Text = "";
        txmanualinvoice.Text = "";

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        string sDONo = string.Empty;
        List<string> lDO = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "dosales_sta_w"));
        arr.Add(new cArrayList("@do_no", prefixText));
        bll.vGetMstDoSalesByQry(arr, ref rs);
        while (rs.Read())
        { 
            sDONo = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["do_no"].ToString() + " - " + rs["dosales_sta_nm"].ToString() + "-" + rs["inv_no"].ToString(),rs["do_no"].ToString());
            lDO.Add(sDONo);
        } rs.Close();
        return lDO.ToArray();
    }
    protected void btload_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@do_no", hddo.Value.ToString()));
        bll.vGetMstDoSales(arr, ref rs);
        while (rs.Read())
        { 
            lbinvoiceno.Text = rs["inv_no"].ToString();
            lbpallette.Text = rs["pallete"].ToString();
            lbloadingmanual.Text = rs["ref_no"].ToString();
        } rs.Close();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string sTripNo="";

        List<cArrayList> arr = new List<cArrayList>();
        // @trip_no varchar(50) out,
        arr.Add(new cArrayList("@trip_dt", DateTime.ParseExact(dttrip.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@driver_nm", ""));
        arr.Add(new cArrayList("@vhc_cd", lbvehiclecode.Text));
        bll.vInsertMstExpedition(arr, ref sTripNo);
        txtripno.Text = sTripNo;
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@trip_no", sTripNo));
        bll.vInsertExpeditionDtl(arr);
        btprint.Enabled = true;
        btprintinvoice.Enabled = true;
        btsave.Enabled = false;
        btadd.Enabled = false;
        
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Loading saved successfully ...');", true);
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //{tmst_expedition.trip_no} = "EX00002"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "openreport('fm_report2.aspx?src=lv&no=" + txtripno.Text  +  "');", true);
    }
}