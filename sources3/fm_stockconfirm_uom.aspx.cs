using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockconfirm_uom : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                dtstock.Text = Request.Cookies["waz_dt"].Value.ToString();
                arr.Add(new cArrayList("@datDate", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingGridToSp(ref grd, "sp_tstock_confirm_get", arr);
                CheckConfirm();
            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    void CheckConfirm()
    {
        try
        {

            string sConfirm = "";
            sConfirm = bll.vLookUp("select confirm_sta_id from tstock_confirm where salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "' and stock_dt='"+ System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "'");

            btpostpone.Text = "You have chance postpone until " + bll.sGetControlParameter("postpone") + " Days";
            if (sConfirm == "N" || sConfirm == "")
            {
                //btpostpone.CssClass = "button2 cancel";
                btconfirm.CssClass = "button2 add"; lbcap.Text = "Hereby state that all stock in this day is CORRECT";
                btpostpone.CssClass = "divhid";
                //vUpload.Visible = true;
                //vUpload.Attributes.Remove("style");
            }
            else if (sConfirm == "W")
            {
                btconfirm.CssClass = "divhid"; btpostpone.CssClass = "divhid"; lbcap.Text = "Waiting Confirmation from HO!";
                //vUpload.Visible = false;
                //vUpload.Attributes.Add("style", "display:none");
            }
            else
            {
                btconfirm.CssClass = "divhid"; btpostpone.CssClass = "divhid"; lbcap.Text = "You already confirmed !";
                //vUpload.Visible = false;
                //vUpload.Attributes.Add("style", "display:none");
            }
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }

   
    protected void btconfirm_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean isCorrect = false;
            FileInfo fi = null;
            List<cArrayList> arr = new List<cArrayList>();


            //if (!fup.HasFile)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Please attach document stock jared !','Stok Confirmation','warning');", true);
            //    return;
            //}
            //if (fup.HasFile)
            //{
            //    fi = new FileInfo(fup.FileName);
            //    byte[] by = fup.FileBytes;
            //    if (by.Length > 1000000)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('File uploaded can not more than 1 MB!','Stok Confirmation','warning');", true);
            //    }
            //}

            if (isCorrect == false)
            {
                arr.Clear();
                arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@confirm_tm", System.DateTime.Today.ToLongTimeString()));
                arr.Add(new cArrayList("@confirmby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@confirm_sta_id", "N"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertStockConfirm(arr);
            }

            foreach (GridViewRow row in grd.Rows)
            {
                double dQty = 0, dQty2 = 0, dQtyTotal = 0;
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbitemnm = (Label)row.FindControl("lbitemnm");
                HiddenField hdwhs_cd = (HiddenField)row.FindControl("hdwhs_cd");
                HiddenField hdbin_cd = (HiddenField)row.FindControl("hdbin_cd");
                //Label lbclosing = (Label)row.FindControl("lbclosing");
                HiddenField lbclosing = (HiddenField)row.FindControl("lbclosing");
                HiddenField hdclosing_ctn = (HiddenField)row.FindControl("hdclosing_ctn");
                HiddenField hdclosing_pcs = (HiddenField)row.FindControl("hdclosing_pcs");
                //TextBox txqty = (TextBox)row.FindControl("txqty");
                TextBox txqty_ctn = (TextBox)row.FindControl("txqty_ctn");
                TextBox txqty_pcs = (TextBox)row.FindControl("txqty_pcs");
                DropDownList cbuom_ctn = (DropDownList)row.FindControl("cbuom_ctn");
                DropDownList cbuom_pcs = (DropDownList)row.FindControl("cbuom_pcs");


                dQty = double.Parse(txqty_ctn.Text);
                dQty2 = double.Parse(txqty_pcs.Text);
                dQtyTotal = dQty + (double.Parse(bll.vLookUp("select round(dbo.sfnUomQtyConv('" + lbitemcode.Text + "','" + cbuom_pcs.SelectedValue.ToString() + "','CTN','" + txqty_pcs.Text + "'),5)")));


                arr.Clear();
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@whs_cd", hdwhs_cd.Value));
                arr.Add(new cArrayList("@bin_cd", hdbin_cd.Value));
                arr.Add(new cArrayList("@qty", txqty_ctn.Text));
                arr.Add(new cArrayList("@qty2", txqty_pcs.Text));
                arr.Add(new cArrayList("@uom", cbuom_ctn.SelectedValue.ToString()));
                arr.Add(new cArrayList("@uom2", cbuom_pcs.SelectedValue.ToString()));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));

                if ((Convert.ToDouble(hdclosing_ctn.Value) != dQty) || (Convert.ToDouble(hdclosing_pcs.Value) != dQty2))
                {
                    isCorrect = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is items : "+lbitemcode.Text+" - "+ lbitemnm.Text + " different QTY !','Stock Confirm','warning');", true);
                    return;
                }
                else { isCorrect = true; }

                if (dQtyTotal > 0)
                {
                    bll.vInsertStockConfirmDtl(arr);
                }
            }

            //string ext = fi.Extension;
           // string pfilename = "SC_" + Request.Cookies["sp"].Value.ToString() + "_" + Request.Cookies["usr_id"].Value.ToString() + "_" + Convert.ToString(DateTime.Today.Day) + Convert.ToString(DateTime.Today.Month) + Convert.ToString(DateTime.Today.Year) + fi.Extension.ToString();
           // fup.SaveAs(bll.vLookUp("select dbo.fn_getcontrolparameter('image_path')") + @"\stock\stock_confirm\" + pfilename);

            if (isCorrect == true)
            {
                arr.Clear();
                arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@confirm_tm", System.DateTime.Today.ToLongTimeString()));
                arr.Add(new cArrayList("@confirmby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@confirm_sta_id", "W"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@file", "NA"));
                bll.vInsertStockConfirm(arr);
            }

            CheckConfirm();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Thank you , you are confirmed with this data','Confirmed Yes','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    
    protected void btpostpone_Click(object sender, EventArgs e)
    {
        try
        {

            List<cArrayList> arr = new List<cArrayList>();

            arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@confirm_tm", System.DateTime.Today.ToLongTimeString()));
            arr.Add(new cArrayList("@confirmby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@confirm_sta_id", "P"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertStockConfirm(arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Thank you , you are Postpone this stock','Max next days must be confirm','success');", true);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }

    }



    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList cbuom_ctn = (DropDownList)e.Row.FindControl("cbuom_ctn");
                DropDownList cbuom_pcs = (DropDownList)e.Row.FindControl("cbuom_pcs");
                DropDownList cbbin = (DropDownList)e.Row.FindControl("cbbin");
                TextBox txqty_ctn = (TextBox)e.Row.FindControl("txqty_ctn");
                TextBox txqty_pcs = (TextBox)e.Row.FindControl("txqty_pcs");

                bll.vBindingFieldValueToCombo(ref cbuom_ctn, "uom_tf", "uom");
                bll.vBindingFieldValueToCombo(ref cbuom_pcs, "uom_tf", "uom");
                cbuom_ctn.SelectedValue = "CTN";
                cbuom_pcs.SelectedValue = "PCS";
                cbuom_ctn.Enabled = false;
                cbuom_pcs.Enabled = false;
                txqty_ctn.Enabled = true;
                txqty_pcs.Enabled = true;

                //Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                //HiddenField hdwhs_cd = (HiddenField)e.Row.FindControl("hdwhs_cd");
                //HiddenField hdbin_cd = (HiddenField)e.Row.FindControl("hdbin_cd");
                //Label lbclosing = (Label)e.Row.FindControl("lbclosing");
                //TextBox txqty = (TextBox)e.Row.FindControl("txqty");
                //HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                //txqty.Text = hdqty.Value;

            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@datDate", DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            Session["lParamstockconfirm_uom"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stockconfirm_uom');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}