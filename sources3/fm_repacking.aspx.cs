using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_repacking : System.Web.UI.Page
{
    cbll bll = new cbll();
    cbll2 bll2 = new cbll2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSpWithEmptyChoosen(ref cbbom, "sp_tmst_bom_get", "bom_cd", "bom_nm");
            dtrepack.Text = Request.Cookies["waz_dt"].Value;
            cd.v_disablecontrol(txrepackno);
            cd.v_disablecontrol(dtrepack);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
        }
    }

    protected void cbbom_SelectedIndexChanged(object sender, EventArgs e)
    {
        double _qty = 0;
        if (!double.TryParse(txqtyout.Text, out _qty))
        {
            cbbom.SelectedValue = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
                "sweetAlert('Please enter qty pack out correctly!','Qty Produced Out','warning');", true);
            return;
        }

        vBindingGrid();
        cd.v_disablecontrol(txqtyout);
        cd.v_showcontrol(btsave);
    }

    void vBindingGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@bom_cd", cbbom.SelectedValue));
        bll.vBindingGridToSp(ref grd, "sp_tbom_dtl_get", arr);
        Label lbtotpcs = (Label)grd.FooterRow.FindControl("lbtotpcs");
        Label lbtotctn = (Label)grd.FooterRow.FindControl("lbtotctn");
        double _qtypcs = 0;double _qtyctn = 0;  
        foreach(GridViewRow _row in grd.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                Label lbqtypcs = (Label)_row.FindControl("lbqty");
                Label lbqtyctn = (Label)_row.FindControl("lbqtyctn");
                _qtyctn += Convert.ToDouble(lbqtyctn.Text);
                _qtypcs += Convert.ToDouble(lbqtypcs.Text);

            }
        }
        lbtotctn.Text = _qtyctn.ToString("N2");
        lbtotpcs.Text = _qtypcs.ToString("N2");
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdqty = (HiddenField)e.Row.FindControl("hdqty");
            Label lbqty = (Label)e.Row.FindControl("lbqty");
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbtotctn = (Label)e.Row.FindControl("lbqtyctn");
            double _qty = Convert.ToDouble(hdqty.Value);
            double _ctn = Convert.ToDouble(txqtyout.Text);
            double _totalctn = 0;
            double _total = 0;
            Label lbuom = (Label)e.Row.FindControl("lbuom");
            if (lbuom.Text == "PCS")
            {
                _total = _qty * _ctn;
                _totalctn = Convert.ToDouble(bll.vLookUp("select dbo.fn_itemconversion('PCS','CTN','" + lbitemcode.Text + "'," + _total.ToString("N1") + ")"));
            }
            else
            {
                _total = 0;
            }

            lbqty.Text = _total.ToString("N2");
            lbtotctn.Text = _totalctn.ToString("N2");

        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string _repackcode = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@repack_dt", System.DateTime.ParseExact(dtrepack.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@bom_cd", cbbom.SelectedValue));
        arr.Add(new cArrayList("@qty", txqtyout.Text));
        arr.Add(new cArrayList("@uom", "CTN"));
        bll2.vInsertMstRepacking(arr, ref _repackcode);
        txrepackno.Text = _repackcode;
        foreach (GridViewRow _row in grd.Rows)
        {
            if (_row.RowType == DataControlRowType.DataRow)
            {
                Label lbitemcode = (Label)_row.FindControl("lbitemcode");
                Label lbuom = (Label)_row.FindControl("lbuom");
                Label lbqty = (Label)_row.FindControl("lbqty");
                arr.Clear();
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@uom", lbuom.Text));
                arr.Add(new cArrayList("@qty", lbqty.Text));
                arr.Add(new cArrayList("@repack_cd", _repackcode));
                bll2.vInsertRepackingdtl(arr);
            }
        }

        string _sql = "update tmst_repacking set repack_sta_id='C' where repack_cd='" + _repackcode + "'";
        bll.vExecuteSQL(_sql);

        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(cbbom);
        cd.v_disablecontrol(cbbom);
        cd.v_disablecontrol(txrepackno);
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New repacking has been created !','" + _repackcode + "','success');", true);
    }

    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_repacking.aspx");
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=repack&n=" + txrepackno.Text + "');", true);
    }
}