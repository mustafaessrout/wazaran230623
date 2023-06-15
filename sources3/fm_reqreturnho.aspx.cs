using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_reqreturnho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingComboToSp(ref cbprodspv, "sp_tmst_product_getprdspv", "emp_cd", "emp_desc");
            cbprodspv_SelectedIndexChanged(sender, e);
            arr.Add(new cArrayList("@reasn_typ", "100"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
        }
    }
    protected void cbprodspv_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@supervisor_cd", cbprodspv.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbyprdspv2", arr);
        //if (grd.Rows.Count > 0)
        //{
        //    grd_SelectedIndexChanging(0);
        //}
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@supervisor_cd", cbprodspv.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_product_getbyprdspv2", arr);
        //if (grd.Rows.Count > 0)
        //{
        //    grd_SelectedIndexChanging(0);
        //}
    }
    protected void btsaved_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        double dOut = 0;
        if (!double.TryParse(txamt.Text, out dOut))
        {
            ScriptManager.RegisterStartupScript(Page,Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount of Return must value!','Check Amount Return','warning');", true);
            return;
        }

        if (dOut == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount of Return must bigger than 0!','Check Amount Return','warning');", true);
            return;
        }
        string sExist = bll.vLookUp("select count(1) from treturnho_booking where reqretho_sta_id in ('N','A') and supervisor_cd='"+cbprodspv.SelectedValue.ToString()+"'");
        if (Convert.ToDouble(sExist) > 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Previous request return ho is not yet implemented!','Please use that request','warning');", true);
            return;
        }
        string sBookingNo = string.Empty;
        arr.Add(new cArrayList("@supervisor_cd", cbprodspv.SelectedValue.ToString()));
        arr.Add(new cArrayList("@amt", dOut));
        arr.Add(new cArrayList("@reqretho_sta_id", "N"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@remark", cbreason.SelectedValue.ToString()));
        bll.vInsertReturnHoBooking(arr, ref sBookingNo);
        lbmessage.Text = "Your request has been sent to " + cbprodspv.SelectedItem.Text + ", pls follow up to get approval!";
        btsaved.CssClass = "divhid";
        txamt.CssClass = "form-control ro";
        cbprodspv.CssClass = "form-control ro";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New request Return HO to Prod Spv "+cbprodspv.SelectedItem.Text+"','Please follow up to get approval','success');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqreturnho.aspx");
    }
    protected void btnSupItemMapping_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=SupItemMapping&supID=' "+ Convert.ToString(cbprodspv.SelectedValue) +");", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lblprod_cd = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lblprod_cd");
        if (lblprod_cd.Text != "") {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@supervisor_cd", cbprodspv.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", lblprod_cd.Text.ToString()));
            bll.vBindingGridToSp(ref grdItem, "sp_tmst_product_getbyprdspvItemMapping", arr);
        }
    }

    protected void grd_SelectedIndexChanging(int rowNumber)
    {
        Label lblprod_cd = (Label)grd.Rows[rowNumber].FindControl("lblprod_cd");
        if (lblprod_cd.Text != "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@supervisor_cd", cbprodspv.SelectedValue.ToString()));
            arr.Add(new cArrayList("@prod_cd", lblprod_cd.Text.ToString()));
            bll.vBindingGridToSp(ref grdItem, "sp_tmst_product_getbyprdspvItemMapping", arr);
        }
    }
}