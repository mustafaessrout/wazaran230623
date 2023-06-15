using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salesofsalesman : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txdt1.Text = Request.Cookies["waz_dt"].Value.ToString();
            txdt2.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbsalespoint.SelectedValue = Request.Cookies["sp"].Value.ToString();
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                cbsalespoint.Enabled = true;
            }
            else
            {
                cbsalespoint.Enabled = false;
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref cbitem_cdFr, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbitem_cdTo, "sp_tmst_item_get4", "ID", "ItemName");
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cbProd_cdTo, "sp_tmst_product_get4", "ID", "ProdName");
            bll.vBindingComboToSp(ref cboutlettype, "sp_tfield_value_getotlet", "sort", "fld_desc");
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", "salesrepbyqty"));
            bll.vBindingComboToSp(ref cbreporttype, "sp_tfield_value_get", "fld_valu", "fld_desc", arr);
            cbreporttype_SelectedIndexChanged(sender, e);
        }
    }

    protected void btreport_Click(object sender, EventArgs e)
    {

        DateTime dtpayp1 = DateTime.ParseExact(txdt1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dtpayp2 = DateTime.ParseExact(txdt2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        string dt1 = dtpayp1.Year.ToString() + "-" + dtpayp1.Month.ToString("00") + "-" + dtpayp1.Day.ToString("00");
        string dt2 = dtpayp2.Year.ToString() + "-" + dtpayp2.Month.ToString("00") + "-" + dtpayp2.Day.ToString("00");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@startdate", dt1));
        arr.Add(new cArrayList("@enddate", dt2));
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

        if (rditemprod.SelectedValue.ToString() == "0")
        {
            arr.Add(new cArrayList("@cbprod_cdFr", cbProd_cdFr.SelectedValue));
            arr.Add(new cArrayList("@cbprod_cdTo", cbProd_cdTo.SelectedValue));
            arr.Add(new cArrayList("@cbprod_cdFrtx", cbProd_cdFr.SelectedItem.Text));
            arr.Add(new cArrayList("@cbprod_cdTotx", cbProd_cdTo.SelectedItem.Text));
            if (cbreporttype.SelectedValue.ToString() == "0")
            {
                if (!chsumm.Checked)
                {
                    arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                    Session["lParamsls"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofsalesmanproduct');", true);
                }
                else
                {
                    arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                    Session["lParamsls"] = arr;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofsalesmanproductbysls');", true);
                }
               
            }
            else if (cbreporttype.SelectedValue.ToString() == "1")
            {
                arr.Add(new cArrayList("@salesmacd", null));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesbybranchbyproduct');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "2")
            {
                if (txcust.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Fill customer data...','error');", true);
                    return;
                }
                arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofcustbyslsman_byprod');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "3")
            {
                if (cboutlettype.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please choose Outlet type...','error');", true);
                    return;
                }
                //arr.Add(new cArrayList("@salesmacd", null));
                if (cboutlettype.SelectedValue == "null")
                {
                    arr.Add(new cArrayList("@outletcd", null));
                }
                else { arr.Add(new cArrayList("@outletcd", cboutlettype.SelectedValue.ToString())); }
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofbranchbyotlcdprod');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "6")
            {
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofcustomer');", true);
            }

        }
        else if (rditemprod.SelectedValue.ToString() == "1")
        {
            arr.Add(new cArrayList("@cbitem_cdFr", cbitem_cdFr.SelectedValue));
            arr.Add(new cArrayList("@cbitem_cdTo", cbitem_cdTo.SelectedValue));
            arr.Add(new cArrayList("@cbitem_cdFrtx", cbitem_cdFr.SelectedItem.Text));
            arr.Add(new cArrayList("@cbitem_cdTotx", cbitem_cdTo.SelectedItem.Text));
            if (cbreporttype.SelectedValue.ToString() == "0")
            {
                arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofsalesman');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "1")
            {
                arr.Add(new cArrayList("@salesmacd", null));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesbybranchbyitem');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "2")
            {
                if (txcust.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Fill customer data...','error');", true);
                    return;
                }
                arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofcustbyslsman');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "3")
            {
                if (cboutlettype.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please choose Outlet type...','error');", true);
                    return;
                }
                //arr.Add(new cArrayList("@salesmacd", null));
                if (cboutlettype.SelectedValue == "null")
                {
                    arr.Add(new cArrayList("@outletcd", null));
                }
                else { arr.Add(new cArrayList("@outletcd", cboutlettype.SelectedValue.ToString())); }

                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofbranchbyotlcd');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "4")
            {
                arr.Add(new cArrayList("@salesmacd", cbsalesman.SelectedValue.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=monthlysales');", true);
            }
            else if (cbreporttype.SelectedValue.ToString() == "5")
            {
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesreturnofbranch');", true);
            }
	    else if (cbreporttype.SelectedValue.ToString() == "6")
            {
                arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
                Session["lParamsls"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=salesofcustomeritem');", true);
            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Please Select Report By Item Or Group','error');", true);
            return;
        }
    }
    //[System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    //public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    //{
    //    HttpCookie cookieSP;
    //    cookieSP = HttpContext.Current.Request.Cookies["sp"];
    //    string sEmp = string.Empty;
    //    System.Data.SqlClient.SqlDataReader rs = null;
    //    List<string> lEmp = new List<string>();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@salespointcd", cookieSP.Value.ToString()));
    //    arr.Add(new cArrayList("@job_title", "SALESCD"));
    //    //arr.Add(new cArrayList("@emp_nm", prefixText));
    //    cbll bll = new cbll();
    //    bll.vSearchMstEmployee2bysalespoint(arr, ref rs);
    //    while (rs.Read())
    //    {
    //        sEmp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
    //        lEmp.Add(sEmp);
    //    } rs.Close();
    //    return (lEmp.ToArray());
    //}
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lcust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(sCust);
        } rs.Close();
        return (lcust.ToArray());

    }
    protected void cbreporttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreporttype.SelectedValue.ToString() == "0")
        {
            //0
            //txsales.Text = "";            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            cbsalesman.Enabled = true;
            txcust.Enabled = false;
            cboutlettype.Enabled = false;
            rditemprod.Enabled = true;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible=true;
        }
        else if (cbreporttype.SelectedValue.ToString() == "1")
        {
            //txsales.Text = "";
            cbsalesman.Enabled = false;
            txcust.Enabled = false;
            cboutlettype.Enabled = false;
            rditemprod.Enabled = true;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible = false;
        }
        else if (cbreporttype.SelectedValue.ToString() == "2")
        {
            cboutlettype.Enabled = false;
            //0
            //txsales.Text = "";            
            cbsalesman.Enabled = true;
            txcust.Enabled = true;
            rditemprod.Enabled = true;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible = false;
        }
        else if (cbreporttype.SelectedValue.ToString() == "3")
        {
            //txsales.Text = "";
            cbsalesman.Enabled = false;
            txcust.Enabled = false;
            cboutlettype.Enabled = true;
            rditemprod.Enabled = true;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible = false;
        }
        else if (cbreporttype.SelectedValue.ToString() == "4")
        {
            cboutlettype.Enabled = false;
            //0
            //txsales.Text = "";            
            cbsalesman.Enabled = true;
            txcust.Enabled = false;
            rditemprod.Enabled = true;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible = false;
        }
        else if (cbreporttype.SelectedValue.ToString() == "5")
        {
            //txsales.Text = "";
            cbsalesman.Enabled = false;
            txcust.Enabled = false;
            cboutlettype.Enabled = false;
            txdt2.Enabled = true;
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            chsumm.Visible = false;
        }
        else if (cbreporttype.SelectedValue.ToString() == "6")
        {
            //txsales.Text = "";
            cbsalesman.Enabled = false;
            txcust.Enabled = true;
            cboutlettype.Enabled = false;
            rditemprod.SelectedValue = "0";
            //rditemprod.Enabled = false;
            cbProd_cdTo.Enabled = false;
            txdt2.Enabled = false;
            cbProd_cdFr.Enabled = true;
            cbProd_cdTo.Enabled = true;
            chsumm.Visible = false;
        }
    }
    protected void btsl_Click(object sender, EventArgs e)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@startdate", DateTime.ParseExact(txdt1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@enddate", DateTime.ParseExact(txdt2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        //arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@cust_cd", txcust.Text));
        //bll.vBindingComboToSp(ref cbsalesman, "", "salesman_cd", "emp_desc", arr);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", cbsalespoint.SelectedValue.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@startdate", DateTime.ParseExact(txdt1.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@enddate", DateTime.ParseExact(txdt2.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingComboToSp(ref cbsalesman, "sp_summ_salesofsalesmanproduct_slsmantrf", "salesman_cd", "emp_nm", arr);
    }
    protected void rditemprod_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rditemprod.SelectedValue.ToString() == "0")
        {
            cbProd_cdFr.Enabled = true;
            cbProd_cdTo.Enabled = true;
            cbitem_cdFr.Enabled = false;
            cbitem_cdTo.Enabled = false;
        }
        else if (rditemprod.SelectedValue.ToString() == "1")
        {
            cbProd_cdFr.Enabled = false;
            cbProd_cdTo.Enabled = false;
            cbitem_cdFr.Enabled = true;
            cbitem_cdTo.Enabled = true;
        }
    }
}