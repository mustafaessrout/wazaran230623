using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_loadingsales : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@qry_cd", "so_sta_id"));
            //bll.vBindingGridToSp (ref grd, "sp_tmst_salesorder_getbyqry", arr);
          //  bll.vBindingFieldValueToCombo(ref cbexpeditiontype, "comp_sta_id");
            cbexpeditiontype_SelectedIndexChanged(sender, e);
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref cbwhs, "sp_tmst_warehouse_get", "whs_cd","whs_nm", arr);
         //   bll.vBindingComboToSp(ref cbsalespoint, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            cbwhs_SelectedIndexChanged(sender, e);
            dtloading.Text = System.DateTime.Today.ToShortDateString();
            bll.sFormat2ddmmyyyy(ref dtloading);
            if (Request.QueryString["do"] != null)
            {
                arr.Clear();
                arr.Add(new cArrayList("@do_no", Request.QueryString["do"]));
             
            }
            
        }
    }
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbsocode = (Label)grd.SelectedRow.FindControl("lbsocode");
        Label lbsatatus = (Label)grd.SelectedRow.FindControl("lbstatus");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", lbsocode.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_tsalesorder_dtl_get", arr);
        lbsono.Text = lbsocode.Text;
        lbstatus.Text = lbsatatus.Text;
        
    }
    protected void grd_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_sta_id", "N"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_get", arr);
    }
    protected void cbexpeditiontype_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cbexpeditiontype.SelectedValue.ToString() == "OWN")
        //{
        //    List<cArrayList> arr = new List<cArrayList>();
        //    txdriver.Visible = false;
        //    cbdriver.Visible= true;
        //    arr.Add(new cArrayList("@qry_cd", "Driver"));
        //    bll.vBindingComboToSp(ref cbdriver, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        //}
        //else
        //{
        //    txdriver.Visible = true;
        //    cbdriver.Visible = false;
        //}
       // List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@comp_sta_id" , cbexpeditiontype.SelectedValue.ToString()));
        //bll.vBindingComboToSp(ref cbdriver , "sp_tmst_company_expedition_get","comp_cd","comp_nm", arr);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Manual No must be fill !!');", true);
            return;
        }
        if (grddtl.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Select Sales Order for loading !!');", true);
            return;
        }
        double dQty =0;
        foreach (GridViewRow row in grddtl.Rows)
        {
            //Label lbsono = (Label)row.SelectedRow.FindControl("lbsono");
            Label lbstock = (Label)row.FindControl("lbstock");
            TextBox txqtyloading = (TextBox)row.FindControl("txqtyloading");
            Label lbqtyorder = (Label)row.FindControl("lbqtyorder");
            Label lbdelivered = (Label)row.FindControl("lbdelivered");
            Label lbunitprice = (Label)row.FindControl("lbunitprice");
            if (!double.TryParse(txqtyloading.Text, out dQty))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ax", "Quantity must be numeric !!');", true);
                return;
            }

            if (Convert.ToDouble(lbstock.Text) < dQty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Stock is not enough !!');", true);
                return;
            }

            if (Convert.ToDouble(lbqtyorder.Text) < dQty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Quantity deliver can not bigger than order !!');", true);
                return;
            }

            if ((Convert.ToDouble(lbdelivered.Text) + dQty) > Convert.ToDouble(lbqtyorder.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Quantity deliver can not bigger than delivered !!');", true);
                return;
            }
        }
        List<cArrayList> arr = new List<cArrayList>();
                    //    @do_no varchar(50) out,
                    //@salespointcd varchar(50),
                    //@do_dt date,
                    //@so_no varchar(50),
                    //@dosales_sta_id varchar(50),
                    //@createdby varchar(50),
                    //@driver_cd varchar(50)=null,
                    //@driver_nm varchar(50)=null,
                    //@comp_cd varchar(50),
                    //@ref_no varchar(50)
        string sDoNo = "";
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@do_dt", DateTime.ParseExact(dtloading.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@so_cd", lbsono.Text));
        arr.Add(new cArrayList("@dosales_sta_id", "W"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
       // arr.Add(new cArrayList("@driver_nm", txdriver.Text));
        //arr.Add(new cArrayList("@driver_cd", cbdriver.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@comp_cd", cbexpeditiontype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@ref_no", txmanualno.Text));
        bll.vInsertMstDoSales(arr, ref sDoNo);
        txloadingno.Text = sDoNo;
        //Detail DO
        foreach (GridViewRow row in grddtl.Rows)
        {
            Label lbstock = (Label)row.FindControl("lbstock");
            Label lbitemcode = (Label)row.FindControl("lbitemcode");
            TextBox txqtyloading = (TextBox)row.FindControl("txqtyloading");
            Label lbqtyorder = (Label)row.FindControl("lbqtyorder");
            Label lbunitprice = (Label)row.FindControl("lbunitprice");
            if (txqtyloading.Text != "0")
            {
                arr.Clear();
                arr.Add(new cArrayList("@do_no", sDoNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
                arr.Add(new cArrayList("@qty", txqtyloading.Text));
                arr.Add(new cArrayList("@unitprice", lbunitprice.Text));
                bll.vInsertDoSalesDtl(arr);
            }
            
        }
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", lbsono.Text));
        bll.vBatchSalesOrderStatus(arr);
        btsave.Enabled = false;
   //     btsave.CssClass = "button2 disablesave";
        btloading.Enabled = true;
        grd.DataBind();
        //lbsono.Text = "";
        //lbstatus.Text = ";
        
     //   txinvoiceno.Text = bll.vLookUp("select inv_no from tmst_dosales where do_no='" + txloadingno.Text + "'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Loading sales has been saved ....');", true);
    }
    protected void cbwhs_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@whs_cd", cbwhs.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbbin, "sp_twarehouse_bin_get","bin_cd","bin_nm", arr);
    }
    protected void cbbin_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbsocode = (Label)grd.SelectedRow.FindControl("lbsocode");
        List<cArrayList> arr = new List<cArrayList>();
       
       
        arr.Add(new cArrayList("@so_cd", lbsocode.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddtl, "sp_tsalesorder_dtl_get", arr);
    }
    protected void grddtl_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        double dStock = 0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbitemcode = (Label)e.Row.FindControl("lbitemcode");
            Label lbstock = (Label)e.Row.FindControl("lbstock");
            if (!double.TryParse(bll.vLookUp("select sum(stock_amt) from tmst_stock where item_cd='" + lbitemcode.Text + "' and whs_cd='" + cbwhs.SelectedValue.ToString() + "' and bin_cd='" + cbbin.SelectedValue.ToString() + "'"), out dStock))
            {
                dStock = 0;
            }
            lbstock.Text = dStock.ToString();
        }
    }
    protected void btloading_Click(object sender, EventArgs e)
    {
        if ((lbsono.Text == "-") || (lbsono.Text == ""))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Sales order must be selected !');", true);
            return;
        }

        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "alert('Manual No for loading must be fill !');", true);
            return;
        }
      
        //  btsave_Click(sender, e);
        ScriptManager.RegisterStartupScript(Page,Page.GetType(),"op","openreport('fm_report2.aspx?src=lo&so=" + lbsono.Text +  "');",true);
    }
    protected void btprintinvoice_Click(object sender, EventArgs e)
    {
//       string sInvNo="";
   //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), "op", "openreport('fm_report2.aspx?src=in&no=" + txinvoiceno.Text + "');", true);
        //{tdosales_invoice.inv_no} = "IV00001"
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@qry_cd", "loading"));
        bll.vBindingGridToSp(ref grd, "sp_tmst_salesorder_getbyqry", arr);
    }
}