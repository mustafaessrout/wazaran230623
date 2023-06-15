using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_subtakeorder : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id",  Request.Cookies["usr_id"].Value.ToString()));
            bll.vDelWrkSubOrder(arr);
            bll.vDelWrkSalesDiscount(arr);
            bll.vDelWrkSalesorderFreeItem(arr);
            vInitPage(Request.QueryString["to"]);
        }
    }

    void vInitPage(string sTO)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        bll.vInsertWrkSubOrderFromCore(arr);
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", sTO));
        bll.vGetMstSalesOrder(arr, ref rs);
        while (rs.Read())
        {
            lbtoparent.Text = Request.QueryString["to"];
            lbcust.Text = rs["cust_cd"].ToString();
            lbhws.Text = rs["whs_cd"].ToString();
            lbmanual.Text = rs["ref_no"].ToString();
            lbsalesman.Text = rs["salesman_cd"].ToString();
            hdcust.Value = rs["cust_cd"].ToString();
            dtorder.Text = Convert.ToDateTime(rs["so_dt"]).ToString("d/M/yyyy");
        }
        rs.Close();
        arr.Clear();
        
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_twrk_suborder_get", arr);
        arr.Clear();
        arr.Add(new cArrayList("@so_cd", sTO));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertWrkSalesDiscountFromCore(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grddiscount, "sp_twrk_salesdiscount_get", arr);
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tsalesorder_dtl_get", arr);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        HttpCookie cook;
        cook = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        List<string> lItem = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItem = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@salespointcd", cook.Value.ToString()));
        arr.Add(new cArrayList("@item_cd", prefixText));
        //arr.Add(new cArrayList("@cust_cd", contextKey));
        bll.vSearchMstItemBySalespoint(arr, ref rs);
        while (rs.Read())
        {
            sItem = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["item_cd"].ToString() + "-" + rs["item_nm"].ToString() + "-" + rs["size"].ToString() + "-" + rs["branded_nm"].ToString(), rs["item_cd"].ToString());
            lItem.Add(sItem);
        }
        rs.Close();
        return (lItem.ToArray());
    }
    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txitem = (TextBox)grd.Rows[e.RowIndex].FindControl("txitem");
        Label lbitemcode = (Label)grd.Rows[e.RowIndex].FindControl("lbitemcode");
        Label lbprice = (Label)grd.Rows[e.RowIndex].FindControl("lbprice");
        int nItemDigit = Convert.ToInt32( bll.sGetControlParameter("itemdigit")) + 1;
        string sItemCode = txitem.Text.Substring(0, nItemDigit);

        string sProd1 = bll.vLookUp("select prod_cd from tmst_item where item_cd='"+lbitemcode.Text+"'");
        string sGroupName = bll.vLookUp("select prod_nm from tmst_product where prod_cd='"+sProd1+"'");
        string sProd2 = bll.vLookUp("select prod_cd from tmst_item where item_cd='" + sItemCode + "'");

        if (sProd1 != sProd2)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Item replacement must same product group','"+sGroupName+"','warning');", true);
            return;
        }
        string sMsg = bll.vLookUp("select dbo.fn_checkitemprice('"+hdcust.Value.ToString()+"','"+lbitemcode.Text+"','"+sItemCode+"')");
        if (sMsg!= "ok")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Price for item replacement must same','" + sMsg + "','warning');", true);
            return;
        }
        string sStockAmt = bll.vLookUp("select dbo.sfnGetStockBooking('" + sItemCode + "','GS','" + lbhws.Text + "','0',dbo.fn_getsystemdate())");
        string nQtyShipment = bll.vLookUp("select qty_shipment from twrk_suborder where usr_id='' and item_cd='"+lbitemcode.Text+"'");
        double dStockAmt = Convert.ToDouble(sStockAmt);
        double dQtyShipment = Convert.ToDouble(nQtyShipment);
        if (dQtyShipment > dStockAmt)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('There is not enough stock','" +sItemCode+ "','warning');", true);
            return;
        }
        foreach (GridViewRow row in grd.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lbitemcode2 = (Label)row.FindControl("lbitemcode");
                if (lbitemcode2.Text == sItemCode)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('The item already booked, you can not book same','" + sItemCode + "','warning');", true);
                    return;
                }

                if (row.RowState == DataControlRowState.Edit)
                {
                    foreach (GridViewRow row1 in grddiscount.Rows)
                    {
                        if (row1.RowType == DataControlRowType.DataRow)
                        {
                            Label lbdisccode = (Label)row1.FindControl("lbdisccode");
                            string sMsg2 = bll.vLookUp("select dbo.fn_checkhitdiscount('" + lbitemcode.Text + "','"+lbdisccode.Text+"')");
                            if (sMsg2 == "ok")
                            {
                                string sMsg3 = bll.vLookUp("select dbo.fn_checkhitdiscount('" + sItemCode + "','" + lbdisccode.Text + "')");
                                if (sMsg3 != "ok")
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Item replaced was not hit by discount!','" + txitem.Text + "','warning');", true);
                                    return;
                                
                                }
                            }
                        }
                    }
                }
            }
        }

      //  string sMsg2 = bll.vLookUp("select dbo.fn_checkhitdiscount('"+sItemCode+"','"++"')");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@item_cd", lbitemcode.Text));
        arr.Add(new cArrayList("@item_cd_new", sItemCode));
        bll.vUpdateWrkSubOrder(arr);
        grd.EditIndex = -1;
        vInitPage(Request.QueryString["to"]);

                
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tsalesorder_dtl_get", arr);
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@so_cd", Request.QueryString["to"]));
        bll.vBatchCopyOrder(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Copy Order has been created','','success');", true);
    }
}