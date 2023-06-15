using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_addlose_van : System.Web.UI.Page
{
    cbll2 bll2 = new cbll2();
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            dtaddlose.Text = Request.Cookies["waz_dt"].Value;
            bll.vBindingFieldValueToComboWithChoosen(ref cbtranstype, "trans_van_typ");
            //arr.Add(new cArrayList("@qry_cd", "SalesJob"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);

            bll.vBindingComboToSpWithEmptyChoosen(ref cbsalesman, "sp_tmst_vehicle_get", "vhc_cd", "vhc_desc", arr);
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvatrate, "sp_tmst_tax_get", "tax_cd", "tax_desc");
            List<taddlose_van_dtl> _taddlose_van_dtl = new List<taddlose_van_dtl>();
            Session["taddlose_van_dtl"] = _taddlose_van_dtl;
            cd.v_disablecontrol(txunitpricecarton);
            cd.v_disablecontrol(txunitpricepcs);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(txaddlosenumber);
        }
    }



    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_addlose_van.aspx");
    }

    protected void btadd_Click(object sender, EventArgs e)
    {
        double _pcs = 0; double _ctn = 0; double _qty = 0; string _uom = string.Empty;
        if (Session["taddlose_van_dtl"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Session expired !','Please click New button to re-start !','warning');", true);
        }
        if (txpcs.Text == string.Empty)
        {
            txpcs.Text = "0";

        }
        if (!double.TryParse(txpcs.Text, out _pcs))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                            "sweetAlert('Please entry correct qty pcs!','Qty PCS','warning');", true);
        }
        if (!double.TryParse(txctn.Text, out _ctn))
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                            "sweetAlert('Please entry correct ctn !','CTN','warning');", true);
        }
        if (txctn.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please qty ctn ! ','Qty Ctn','warning');", true);
        }
        if (_pcs > 0)
        {
            _qty = Convert.ToDouble(bll.vLookUp("select dbo.fn_getqtybypcs('" + hditem.Value + "'," + _ctn.ToString("N2") + "," + _pcs.ToString("N2") + ")"));
            _uom = "PCS";
        }
        else
        {
            _uom = "CTN";
            _qty = _ctn;
        }
        List<taddlose_van_dtl> _taddlose_van_dtl = (List<taddlose_van_dtl>)Session["taddlose_van_dtl"];
        _taddlose_van_dtl.Add(new taddlose_van_dtl
        {
            item_cd = hditem.Value,
            qty_ctn = _ctn,
            qty_pcs = _pcs,
            unitprice_ctn = Convert.ToDouble(bll.vLookUp("select dbo.fn_getunitpricechannel('" + hditem.Value + "','" + bll.sGetControlParameter("addlosevanprice") + "','CTN','" + Request.Cookies["sp"].Value + "')")),
            unitprice_pcs = Convert.ToDouble(bll.vLookUp("select dbo.fn_getunitpricechannel('" + hditem.Value + "','" + bll.sGetControlParameter("addlosevanprice") + "','PCS','" + Request.Cookies["sp"].Value + "')")),
            //qty =  Convert.ToDouble(bll.vLookUp("select dbo.fn_getqtybypcs('" + hditem.Value + "'," + _ctn.ToString("N2") + "," + _pcs.ToString("N2") + ")")),
            qty = _qty,
            uom = _uom,
            vat_rat = Convert.ToDouble(bll.vLookUp("select tax_formula from tmst_tax where tax_cd='" + cbvatrate.SelectedValue + "'")),
            vat = 0,
            item_nm = bll.vLookUp("select item_nm from tmst_item where item_cd='" + hditem.Value + "'")
            //vat = Convert.ToDouble(cbvatrate.SelectedValue) * (Convert.ToDouble(txunitprice.Text) * Convert.ToDouble(bll.vLookUp("select dbo.fn_getqtybypcs('" + hditem.Value + "'," + txctn.Text + "," + txpcs.Text + ")")))
        });
        grd.DataSource = _taddlose_van_dtl;
        grd.DataBind();
        Session["taddlose_van_dtl"] = _taddlose_van_dtl;
        cd.v_showcontrol(btsave);
        txitem.Text = string.Empty;
        hditem.Value = string.Empty;
        txctn.Text = string.Empty;
        txpcs.Text = string.Empty;
        txunitpricecarton.Text = string.Empty;
        txunitpricepcs.Text = string.Empty;
        cbvatrate.SelectedValue = string.Empty;

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetItemList(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        HttpCookie otlcd;
        otlcd = HttpContext.Current.Request.Cookies["otlcd"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        arr.Add(new cArrayList("@otlcd", contextKey));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "|" + rs["item_nm"].ToString() + "|" + rs["size"].ToString() + "|" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);

        }
        rs.Close();
        return (lItem.ToArray());
    }

    protected void btnprice_Click(object sender, EventArgs e)
    {
        if (Session["taddlose_van_dtl"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Session expired!','Please re-start entry','warning');", true);
        }
        List<taddlose_van_dtl> _taddlose_van_dtl = (List<taddlose_van_dtl>)Session["taddlose_van_dtl"];
        txunitpricecarton.Text = bll.vLookUp("select dbo.fn_getunitpricechannel('" + hditem.Value + "','" + bll.sGetControlParameter("addlosevanprice") + "','CTN','" + Request.Cookies["sp"].Value + "')");
        txunitpricepcs.Text = bll.vLookUp("select dbo.fn_getunitpricechannel('" + hditem.Value + "','" + bll.sGetControlParameter("addlosevanprice") + "','PCS','" + Request.Cookies["sp"].Value + "')");
        Session["taddlose_van_dtl"] = _taddlose_van_dtl;
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        if (cbsalesman.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Plase select salesman !','Salesman for add/lose stock','warning');", true);
            return;
        }
        string _addlosenumber = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesman_cd", cbsalesman.SelectedValue));
        arr.Add(new cArrayList("@addlose_dt", System.DateTime.ParseExact(dtaddlose.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@trans_van_typ", cbtranstype.SelectedValue));
        arr.Add(new cArrayList("@addlose_sta_id", "N"));
        bll2.vInsertMstAddLoseVan(arr, ref _addlosenumber);
        txaddlosenumber.Text = _addlosenumber;

        List<taddlose_van_dtl> _taddlose_van_dtl = (List<taddlose_van_dtl>)Session["taddlose_van_dtl"];
        foreach (taddlose_van_dtl _t in _taddlose_van_dtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", _t.item_cd));
            arr.Add(new cArrayList("@qty", _t.qty));
            arr.Add(new cArrayList("@uom", _t.uom));
            arr.Add(new cArrayList("@qty_ctn", _t.qty_ctn));
            arr.Add(new cArrayList("@qty_pcs", _t.qty_pcs));
            arr.Add(new cArrayList("@unitprice_ctn", _t.unitprice_ctn));
            arr.Add(new cArrayList("@unitprice_pcs", _t.unitprice_pcs));
            arr.Add(new cArrayList("@vat_rat", _t.vat_rat));
            arr.Add(new cArrayList("@vat", _t.vat));
            arr.Add(new cArrayList("@unitprice", _t.unitprice));
            arr.Add(new cArrayList("@addlose_cd", _addlosenumber));

            bll2.vInsertAddLoseVanDtl(arr);

        }
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Add lose van has been successfully !','Add Lose Van NO." + _addlosenumber + "','success');", true);

    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        List<taddlose_van_dtl> _taddlose_van_dtl = (List<taddlose_van_dtl>)Session["taddlose_van_dtl"];
        _taddlose_van_dtl.RemoveAt(e.RowIndex);
        grd.DataSource = _taddlose_van_dtl;
        grd.DataBind();
        Session["taddlose_van_dtl"] = _taddlose_van_dtl;
    }
}