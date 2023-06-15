using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_stockconfirm : System.Web.UI.Page
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

            btpostpone.Text = "You have chance postpone until " + bll.sGetControlParameter("postpone") + " Days";
            if (bll.vLookUp("select dbo.fn_checkconfirmstock('" + System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture) + "','"+ Request.Cookies["sp"].Value.ToString() + "')") != "ok")
            {
                btpostpone.CssClass = "button2 cancel";
                btconfirm.CssClass = "button2 add"; lbcap.Text = "Hereby state that all stock in this day is CORRECT";
            }
            else { btconfirm.CssClass = "divhid"; btpostpone.CssClass = "divhid"; lbcap.Text = "You already confirmed !"; }
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
            string sconfirm = bll.vLookUp("select dbo.fn_checkstockconfirm('"+ Request.Cookies["sp"].Value.ToString() + "')");
            if (sconfirm != "ok")
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@confirm_tm", System.DateTime.Today.ToLongTimeString()));
                arr.Add(new cArrayList("@confirmby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@confirm_sta_id", "N"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vInsertStockConfirm(arr);
            }
            foreach (GridViewRow row in grd.Rows)
            {
                double dQty = 0;
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                HiddenField hdwhs_cd = (HiddenField)row.FindControl("hdwhs_cd");
                HiddenField hdbin_cd = (HiddenField)row.FindControl("hdbin_cd");
                Label lbclosing = (Label)row.FindControl("lbclosing");
                TextBox txqty = (TextBox)row.FindControl("txqty");
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@whs_cd", hdwhs_cd.Value));
                arr.Add(new cArrayList("@bin_cd", hdbin_cd.Value));
                arr.Add(new cArrayList("@qty", txqty.Text));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                if (string.IsNullOrEmpty(txqty.Text))
                {
                    txqty.Text = "0";
                }

                if (!double.TryParse(txqty.Text, out dQty))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric','Check Qty','warning');", true);
                    return;
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Thank you , you are confirmed with this data','Confirmed Yes','success');", true);
                if (Convert.ToDouble(lbclosing.Text) != Convert.ToDouble(txqty.Text))
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is items not yet confirmed!','" + lbitemcode.Text + "','err');", true);
                    Response.Write("<script language='javascript'>window.alert('There is items : " + lbitemcode.Text + " not yet confirmed! ');</script>");
                    return;
                }
                if (Convert.ToDouble(txqty.Text) > 0)
                {
                    bll.vInsertStockConfirmDtl(arr);
                }
            }

            List<cArrayList> arr2 = new List<cArrayList>();
            arr2.Add(new cArrayList("@stock_dt", System.DateTime.ParseExact(dtstock.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr2.Add(new cArrayList("@confirm_tm", System.DateTime.Today.ToLongTimeString()));
            arr2.Add(new cArrayList("@confirmby", Request.Cookies["usr_id"].Value.ToString()));
            arr2.Add(new cArrayList("@confirm_sta_id", "C"));
            arr2.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertStockConfirm(arr2);
            CheckConfirm();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Thank you , you are confirmed with this data','Confirmed Yes','success');", true);

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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Thank you , you are Postpone this stock','Max next 5 days must be confirm','success');", true);
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
                Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
                HiddenField hdwhs_cd = (HiddenField)e.Row.FindControl("hdwhs_cd");
                HiddenField hdbin_cd = (HiddenField)e.Row.FindControl("hdbin_cd");
                Label lbclosing = (Label)e.Row.FindControl("lbclosing");
                TextBox txqty = (TextBox)e.Row.FindControl("txqty");
                HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
                txqty.Text = hdqty.Value;

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
            Session["lParamstockconfirm"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stockconfirm');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_stockconfirm");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}