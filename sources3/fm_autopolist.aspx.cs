using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_autopolist : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkPoDtl(arr);
            arr.Clear();
            arr.Add(new cArrayList("@auto_sta_id", "N"));
            bll.vBindingGridToSp(ref grdauto, "sp_tautopo_get", arr);
        }
    }
    protected void btpo_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        foreach (GridViewRow row in grdauto.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chk");
            if (chk.Checked)
            {
                HiddenField hdids = (HiddenField)row.FindControl("hdids");
                arr.Clear();
                arr.Add(new cArrayList("@ids", hdids.Value));
                bll.vUpdateAutoPO(arr);
                Label lbitemcode = (Label)row.FindControl("lbitemcode");
                Label lbqty = (Label)row.FindControl("lbqty");
                arr.Clear();
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", lbqty.Text));
                arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsertWrkPoDtl(arr);
            }
        }
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        double dTot = 100; // bll.dblSumTmpPoDtl(arr);
        arr.Clear();
        string sPoNo = "";
        arr.Add(new cArrayList("@salespoint_cd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@po_dt", System.DateTime.Today.ToShortDateString()));
        arr.Add(new cArrayList("@po_delivery_dt", System.DateTime.Today.ToShortDateString()));
        arr.Add(new cArrayList("@po_typ", "A"));
        arr.Add(new cArrayList("@vendor_cd", "0"));
        arr.Add(new cArrayList("@remark", "Auto PO from Branch"));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@to_nm",bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'")));
        arr.Add(new cArrayList("@to_addr", "Sheeata "));
        arr.Add(new cArrayList("@delivery_typ", "O"));
        arr.Add(new cArrayList("@tot_amt", dTot));
        bll.vInsertMstPO(arr, ref sPoNo);
        
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@po_no", sPoNo));
        bll.vInsertPoDtl2(arr);
        Response.Redirect("fm_po.aspx?po=" + sPoNo);
    }
}