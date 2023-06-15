using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstbom : System.Web.UI.Page
{
    cbll2 bll2 = new cbll2();
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "repacking_to"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbitemto, "sp_tmst_item_getbyqry", "item_cd", "item_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@qry_cd", "repacking_from"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbitemfrom, "sp_tmst_item_getbyqry", "item_cd", "item_desc",arr);
            bll.vBindingFieldValueToComboWithChoosen(ref cbuomfrom, "uom");
            bll.vBindingFieldValueToComboWithChoosen(ref cbuomto, "uom");
          
            Session["tbom_dtl"] = new List<tbom_dtl>();
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(txbomcode);
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        double _qty = 0;
        if (cbitemto.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please select Item to !','Item to','warning');", true);
            return;
        }
        if (cbitemfrom.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
                "sweetAlert('Please select item from !','Item From','warning');", true);
            return;
        }

        if (!double.TryParse(txqty.Text,out _qty))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Qty must numeric !','Qty repack from','warning');", true);
            return;
        }

        if (cbuomfrom.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please select uom from !','UOM From','warning');", true);
            return;
        }
        if (cbuomto.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
               "sweetAlert('Please select uom to !','UOM From','warning');", true);
            return;
        }
        if (txremark.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
             "sweetAlert('Please fill remark !','Remark','warning');", true);
            return;
        }

        List<tbom_dtl> _tbom_dtl = (List<tbom_dtl>)Session["tbom_dtl"];
        _tbom_dtl.Add(new tbom_dtl
        {
            item_cd = cbitemfrom.SelectedValue,
            item_nm = cbitemfrom.SelectedItem.Text,
            qty = Convert.ToDouble(txqty.Text),
            uom = cbuomfrom.SelectedValue
        });

        grd.DataSource = _tbom_dtl;
        grd.DataBind();
    
        cbitemfrom.SelectedValue = string.Empty;
        cbuomfrom.SelectedValue = string.Empty;
        txqty.Text = string.Empty;
        cd.v_showcontrol(btsave);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(cbitemto);
        cd.v_disablecontrol(cbuomto);
        cd.v_disablecontrol(txqtyout);
        Session["tbom_dtl"] = _tbom_dtl;

    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstbom.aspx");
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string _bomcode = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bom_nm", txremark.Text));
        arr.Add(new cArrayList("@item_cd", cbitemto.SelectedValue));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@bom_sta_id", "A"));
        arr.Add(new cArrayList("@created_by", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@created_date", System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@uom", cbuomto.SelectedValue));
        arr.Add(new cArrayList("@qty", Convert.ToDouble( txqtyout.Text)));
        bll2.vInsertMstBom(arr, ref _bomcode);
        List<tbom_dtl> _tbom_dtl = (List<tbom_dtl>)Session["tbom_dtl"];
        foreach(tbom_dtl _t in _tbom_dtl)
        {
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", _t.item_cd));
            arr.Add(new cArrayList("@qty", _t.qty));
            arr.Add(new cArrayList("@uom", _t.uom));
            arr.Add(new cArrayList("@bom_cd", _bomcode));
            bll2.vInsertBomDtl(arr);
        }
        txbomcode.Text = _bomcode;
        cd.v_disablecontrol(grd);
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(btnadd);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "sweetAlert('New BOM has been created!','"+_bomcode+"','success');", true);

    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=bom&b="+txbomcode.Text+"');", true);
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<tbom_dtl> _tbom_dtl = (List<tbom_dtl>)Session["tbom_dtl"];
        _tbom_dtl.RemoveAt(e.RowIndex);

        grd.DataSource = _tbom_dtl;
        grd.DataBind();
        Session["tbom_dtl"] = _tbom_dtl;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Delete successfully!','Delete Detail','info');", true);
    }
}