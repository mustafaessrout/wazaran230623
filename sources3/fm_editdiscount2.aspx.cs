using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_editdiscount2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                initgrid(Request.QueryString["dc"]);

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelwrkFormulaDiscount(arr);
                bll.vDelWrkFreeItem(arr);
                bll.vDelWrkFreeProduct(arr);
                bll.vBindingFieldValueToCombo(ref cbdisctyp, "disc_typ");
                bll.vBindingFieldValueToCombo(ref cbuom, "uom");
                bll.vBindingFieldValueToCombo(ref cbuomfree, "uom");
                bll.vBindingFieldValueToCombo(ref cbmethod, "disc_method");

                rdmethod.Enabled = false;
                rdfree.Enabled = false;

                rdmethod_SelectedIndexChanged(sender, e);
                rdfree_SelectedIndexChanged(sender, e);

                arr.Clear();
                arr.Add(new cArrayList("@level_no", 1));
                bll.vBindingComboToSp(ref cbbrandedfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
                cbbrandedfree_SelectedIndexChanged(sender, e);
                arr.Clear();

                txordermin.Attributes.Add("readonly", "readonly");
                rdused.Enabled = false;
                cbdisctyp.Enabled = false;
                txregularcost.Attributes.Add("readonly", "readonly");
                txnetcost.Attributes.Add("readonly", "readonly");
                //txminqty.Attributes.Add("readonly", "readonly");
                //txmaxorder.Attributes.Add("readonly", "readonly");
                btaddformula.Visible = false;
                btaddfree.Visible = true;
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }
        
    void initgrid(string sDisc)
    {
        try
        {

            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@disc_cd", sDisc));
            bll.vGetMstDiscount(arr, ref rs);
            while (rs.Read())
            {
                hdprop.Value = rs["proposal_no"].ToString();
                hddisc.Value = rs["disc_cd"].ToString();
                lbdisc.Text = sDisc.ToString();
                lbprop.Text = rs["proposal_no"].ToString();
                lbremark.Text = rs["remark"].ToString();
                txremark.Text = rs["remark"].ToString();
                dtstart.Text = Convert.ToDateTime(rs["start_dt"]).ToString("dd/MM/yyyy");
                lbdelidate.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("dd/MM/yyyy");
                dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("dd/MM/yyyy");
                dteditend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("dd/MM/yyyy");
                dteditdelivery.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("dd/MM/yyyy");
                cbdisctyp.SelectedValue = rs["disc_typ"].ToString();
                rdused.SelectedValue = rs["discount_use"].ToString();
                txregularcost.Text = rs["regularcost"].ToString();
                txnetcost.Text = rs["netcost"].ToString();
                txminqty.Text = rs["qty_min"].ToString();
                txmaxorder.Text = rs["qty_max"].ToString();
                txordermin.Text = rs["qty_min"].ToString();
                rdmethod.SelectedValue = rs["discount_mec"].ToString();
                rdfree.SelectedValue = rs["rditem"].ToString();

            }
            rs.Close();
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@dtend", DateTime.ParseExact(dteditend.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@dtdelivery", DateTime.ParseExact(dteditdelivery.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@disc_cd", hddisc.Value.ToString()));
            arr.Add(new cArrayList("@disc_typ", cbdisctyp.SelectedValue.ToString()));
            arr.Add(new cArrayList("@discount_use", rdused.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty_min", txminqty.Text));
            arr.Add(new cArrayList("@qty_max", txmaxorder.Text));
            arr.Add(new cArrayList("@regularcost", txregularcost.Text));
            arr.Add(new cArrayList("@netcost", txnetcost.Text));
            arr.Add(new cArrayList("@remark", txremark.Text));
            bll.vUpdateMstDiscount4(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@disc_no", hddisc.Value.ToString()));
            bll.vInsertDiscountFormula(arr);
            if (rdfree.SelectedValue.ToString() == "P")
            {
                bll.vInsertDiscountFreeitem(arr);
            }
            else if (rdfree.SelectedValue.ToString() == "G")
            {
                bll.vInsertDiscountFreeProduct(arr);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Discount has edit successfully','" + hddisc.Value.ToString() + "','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void rdmethod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdmethod.SelectedValue.ToString() == "FG")
        {
            txcash.Attributes.Add("readonly", "readonly"); txcash.Text = "0";
            txpercent.Attributes.Add("readonly", "readonly"); txpercent.Text = "0";
            txqty.Attributes.Remove("readonly"); txqty.Text = "0";
        }
        else if (rdmethod.SelectedValue.ToString() == "CH")
        {
            txqty.Attributes.Add("readonly", "readonly"); txqty.Text = "0";
            txpercent.Attributes.Add("readonly", "readonly"); txpercent.Text = "0";
            txcash.Attributes.Remove("readonly"); txcash.Text = "0";
        }
        else if (rdmethod.SelectedValue.ToString() == "PC")
        {
            txqty.Attributes.Add("readonly", "readonly"); txqty.Text = "0";
            txcash.Attributes.Add("readonly", "readonly"); txcash.Text = "0";
            txpercent.Attributes.Remove("readonly"); txpercent.Text = "0";
        }
        else if (rdmethod.SelectedValue.ToString() == "CG")
        {
            txqty.Attributes.Add("readonly", "readonly"); txqty.Text = "0";
            txpercent.Attributes.Add("readonly", "readonly"); txpercent.Text = "0";
            txcash.Attributes.Remove("readonly"); txcash.Text = "0";
        }
    }

    protected void btaddformula_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (rdmethod.SelectedValue.ToString() == "FG")
            {
                txpercent.Text = "0";
                txcash.Text = "0";
            }
            else if (rdmethod.SelectedValue.ToString() == "CH")
            {
                txqty.Text = "0";
                txpercent.Text = "0";
            }
            else if (rdmethod.SelectedValue.ToString() == "PC")
            {
                txcash.Text = "0";
                txqty.Text = "0";
            }
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@min_qty", txordermin.Text));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@amt", txcash.Text));
            arr.Add(new cArrayList("@percentage", txpercent.Text));
            arr.Add(new cArrayList("@uom", cbuom.SelectedValue.ToString()));
            arr.Add(new cArrayList("@uom_free", cbuomfree.SelectedValue.ToString()));
            arr.Add(new cArrayList("@disc_typ", cbmethod.SelectedValue.ToString()));
            bll.vInsertWrkFomulaDiscount(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdformula_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            Label lbminqty = (Label)grdformula.Rows[e.RowIndex].FindControl("lbminqty");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@min_qty", lbminqty.Text));
            bll.vDelwrkFormulaDiscount(arr); arr.Clear(); arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdformula, "sp_twrk_formuladiscount_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void rdfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkFreeItem(arr);
            bll.vDelWrkFreeProduct(arr);
            arr.Clear();
            if (rdfree.SelectedValue.ToString() == "P")
            {
                showItemFree.Attributes.Remove("style");
                grdfreeproduct.Attributes.Add("style", "display:none");
                grdfreeitem.Attributes.Remove("style");
            }
            else if (rdfree.SelectedValue.ToString() == "G")
            {
                showItemFree.Attributes.Add("style", "display:none");
                grdfreeitem.Attributes.Add("style", "display:none");
                grdfreeproduct.Attributes.Remove("style");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void cbbrandedfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", 2));
            arr.Add(new cArrayList("@prod_cd_parent", cbbrandedfree.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbprodgroupfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbprodgroupfree_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbprodgroupfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@level_no", 3));
            arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbitemfree, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbitemfree_SelectedIndexChanged(sender, e);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbitemfree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("prod_cd", cbitemfree.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@prod_cd_parent", cbprodgroupfree.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbitemfrees, "sp_tmst_item_get", "item_cd", "item_desc", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btaddfree_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (rdfree.SelectedValue.ToString() == "P")
            {
                arr.Add(new cArrayList("@item_cd", cbitemfrees.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@uom", cbuomfree.SelectedValue.ToString()));
                bll.vInsertWrkFreeItem(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
                arr.Clear();
                grdfreeproduct.Attributes.Add("style", "display:none");
                grdfreeitem.Attributes.Remove("style");
            }
            else
            {
                arr.Add(new cArrayList("@prod_cd", cbitemfree.SelectedValue.ToString()));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsertWrkFreeProduct(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);
                arr.Clear();
                grdfreeitem.Attributes.Add("style", "display:none");
                grdfreeproduct.Attributes.Remove("style");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdfreeitem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            grdfreeitem.PageIndex = e.NewPageIndex;
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdfreeitem_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();
            if (rdfree.SelectedValue.ToString() == "P")
            {
                Label lbitemcode = (Label)grdfreeitem.Rows[e.RowIndex].FindControl("lbitemcode");
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelWrkFreeItem(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdfreeitem, "sp_twrk_freeitem_get", arr);
            }
            else
            {
                Label lbprodcode = (Label)grdfreeproduct.Rows[e.RowIndex].FindControl("lbprodcode");
                arr.Add(new cArrayList("@prod_cd", lbprodcode.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDelWrkFreeProduct(arr);
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vBindingGridToSp(ref grdfreeproduct, "sp_twrk_freeproduct_get", arr);
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_editdiscount2");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

    protected void txminqty_TextChanged(object sender, EventArgs e)
    {
        txordermin.Text = txminqty.Text;
        //txordermin.Attributes.Add("readonly", "readonly");
    }

    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstdiscount2.aspx");
    }
}