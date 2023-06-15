using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_msttax : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                listTax.Attributes.Remove("style");
                newTax.Attributes.Add("style", "display:none");
                btsave.Visible = false;
                lbtaxcode.Text = "NEW";
                bll.vBindingGridToSp(ref grd, "sp_tmst_tax_get");
                bll.vBindingFieldValueToCombo(ref cbtaxtype, "tax_type");
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_msttax");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        bll.vBindingGridToSp(ref grd, "sp_tmst_tax_get");
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbcode = (Label)grd.Rows[e.NewSelectedIndex].FindControl("lbtaxcode");
            SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@tax_cd", lbcode.Text));
            bll.vGetMstTax(arr, ref rs);
            while (rs.Read())
            {
                lbtaxcode.Text = rs["tax_cd"].ToString();
                txtaxnm.Text = rs["tax_nm"].ToString();
                txtaxformula.Text = rs["tax_formula"].ToString();
                txtaxdesc.Text = rs["tax_desc"].ToString();
                cbtaxtype.SelectedValue = rs["tax_type"].ToString();
            }
            rs.Close();
            newTax.Attributes.Remove("style");
            btsave.Visible = true;
            btsave.Text = "Update";

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_msttax");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        newTax.Attributes.Remove("style");
        btsave.Visible = true;
        btsave.Text = "Save";
        lbtaxcode.Text = "NEW";
        txtaxnm.Text = "";
        txtaxformula.Text = "0";
        txtaxdesc.Text = "";
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (bll.nCheckAccess("msttaxsave", Request.Cookies["usr_id"].Value.ToString()) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You dont have access','Create/Modify Tax','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            if (btsave.Text == "Save")
            {
                arr.Clear();
                //arr.Add(new cArrayList("@salespointcd", lbsalespoint_cd.Text));
                arr.Add(new cArrayList("@tax_nm", txtaxnm.Text));
                arr.Add(new cArrayList("@tax_desc", txtaxdesc.Text));
                arr.Add(new cArrayList("@tax_formula", txtaxformula.Text));
                arr.Add(new cArrayList("@tax_type", cbtaxtype.SelectedValue.ToString()));
                bll.vInsertMstTax(arr);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@tax_cd", lbtaxcode.Text));
                arr.Add(new cArrayList("@tax_nm", txtaxnm.Text));
                arr.Add(new cArrayList("@tax_desc", txtaxdesc.Text));
                arr.Add(new cArrayList("@tax_formula", txtaxformula.Text));
                arr.Add(new cArrayList("@tax_type", cbtaxtype.SelectedValue.ToString()));
                bll.vUpdateMstTax(arr);
            }
            lbtaxcode.Text = "NEW";
            txtaxnm.Text = "";
            txtaxdesc.Text = "";
            txtaxformula.Text = "0";
            btsave.Visible = false;
            newTax.Attributes.Add("style", "display:none");
            bll.vBindingGridToSp(ref grd, "sp_tmst_tax_get");

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_msttax");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}