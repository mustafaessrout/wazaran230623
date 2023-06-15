using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesplan_ho : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            string waz_dt;
            waz_dt = DateTime.Now.ToString("d/M/yyyy");
            DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            waz_dt = dtwaz_dt.ToString("yyyyMM");
            dtcreated.Text = DateTime.Now.ToString("d/M/yyyy");
            bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
            cbMonthCD.SelectedValue = waz_dt;
            arr.Clear();
            arr.Add(new cArrayList("@sp_no", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
            bll.vDeleteAddItemSalesPlan(arr);
            cbMonthCD_SelectedIndexChanged(sender, e);
            rdInsert.SelectedValue = "M";
            rdInsert_SelectedIndexChanged(sender, e);
            rdInsert.Enabled = false;
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 3));
            bll.vBindingComboToSp(ref cbProduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
            cbProduct_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSp(ref cbbranch, "sp_tmst_salespoint_getall", "salespointcd", "salespoint_desc");
            btedit.Visible = false;

        }
    }

    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdspno.Value = null;
        int totalItem = 0; 
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Clear();
        arr.Add(new cArrayList("@type", "total"));
        arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
        bll.vGetSalesPlan(arr, ref rs);
        while (rs.Read())
        {
            totalItem =  int.Parse(rs["total"].ToString());
            if (totalItem > 0)
            {
                hdspno.Value = rs["sp_cd"].ToString();
            }
        }
        if(hdspno.Value == null || hdspno.Value == "")
        {
            hdspno.Value = Request.Cookies["usr_id"].Value.ToString();
            btsave.Text = "Save";
            
        }
        else
        {
            btsave.Text = "Update";
        }
        arr.Clear();
        arr.Add(new cArrayList("@sp_no", hdspno.Value));
        arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grditem, "sp_tpch_salesplan_item_get", arr);
    }

    protected void rdInsert_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdInsert.SelectedValue.ToString() == "M")
        {
            showManual.Attributes.Remove("style");
            showManualList.Attributes.Remove("style");
            showImport.Attributes.Add("style", "display:none");
        }
        else
        {
            showImport.Attributes.Remove("style");
            showManual.Attributes.Add("style", "display:none");
            showManualList.Attributes.Add("style", "display:none");
        }
    }

    protected void btupload_Click(object sender, EventArgs e)
    {

    }

    protected void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prod_cd", cbProduct.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbItem, "sp_tmst_item_get", "item_cd", "item_desc", arr);
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        double dqty;
        if (!double.TryParse(txqty.Text, out dqty))
        {
            if (dqty < 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Qty must be more than 0.','Please input qty !','warning');", true);
                return;
            }            
        }
        
        List<cArrayList> arr = new List<cArrayList>();
        try
        {
            arr.Add(new cArrayList("@sp_no", hdspno.Value));
            arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@item_cd", cbItem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespoint_cd", cbbranch.SelectedValue.ToString()));
            arr.Add(new cArrayList("@qty", txqty.Text));
            arr.Add(new cArrayList("@uom", "CTN"));
            bll.vInsertAddItemSalesPlan(arr);
            arr.Clear();
            arr.Add(new cArrayList("@sp_no", hdspno.Value));
            arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grditem, "sp_tpch_salesplan_item_get", arr);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Add Item Salesplan");
        }

    }

    protected void grditem_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grditem_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void grditem_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void grditem_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grditem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_salesplan_ho.aspx");
    }

    protected void btedit_Click(object sender, EventArgs e)
    {

    }

    protected void btprint_Click(object sender, EventArgs e)
    {

    }


    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        if (bll.nCheckAccess("salesplan", Request.Cookies["usr_id"].Value.ToString()) == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access','To make product salesplan, contact Administrator !!','warning');", true);
            return;
        }
        try
        {
            string sp_no = "";
            if (btsave.Text == "Save")
            {
                arr.Clear();
                arr.Add(new cArrayList("@sys", "salesplan"));
                arr.Add(new cArrayList("@sysno", ""));
                bll.vGetDiscountNo(arr, ref rs);
                while (rs.Read())
                {
                    sp_no = rs["generated"].ToString();
                }
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@sp_no", sp_no));
                arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
                arr.Add(new cArrayList("@status", "N"));
                bll.vInsertSalesPlan(arr);
                btsave.Text = "Update";
                arr.Clear();
                arr.Add(new cArrayList("@sp_no", sp_no));
                bll.vBindingGridToSp(ref grditem, "sp_tpch_salesplan_item_get", arr);
            }
            else
            {
                arr.Clear();
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@sp_no", hdspno.Value));
                arr.Add(new cArrayList("@period", cbMonthCD.SelectedValue.ToString()));
                bll.vInsertSalesPlan(arr);
                btsave.Text = "Update";
                arr.Clear();
                arr.Add(new cArrayList("@sp_no", hdspno.Value));
                bll.vBindingGridToSp(ref grditem, "sp_tpch_salesplan_item_get", arr);
            }

            
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : Save Salesplan");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Sales Plan Updated.','Periode : " + cbMonthCD.SelectedValue.ToString() + "','success');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }
    
}