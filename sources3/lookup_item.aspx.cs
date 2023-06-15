using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lookup_item : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        if (!IsPostBack)
        {
            string sItemCode = Request.QueryString["item"];
            arr.Add(new cArrayList("@item_cd", sItemCode));
            bll.vGetMstItem2(arr, ref rs);
            while (rs.Read())
            {
                lbitemcode.Text = sItemCode;
                lbitemname.Text= rs["item_nm"].ToString();
                lbsize.Text = rs["size"].ToString();
                lbbranded.Text = rs["branded_nm"].ToString();
                lbarabic.Text =rs["item_arabic"].ToString();
                lbuom.Text = rs["uom_base"].ToString();
                lbvendorprice.Text = rs["price_buy"].ToString();
                lbvendorname.Text = rs["vendor_nm"].ToString();
                lbpacking.Text = rs["packing"].ToString();
            }
            rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("@item_cd", sItemCode));
            bll.vBindingGridToSp(ref grdsp, "sp_titem_salespoint_get", arr);
            bll.vBindingGridToSp(ref grdprice, "sp_tcustomertype_price_get", arr);
        }
    }
    protected void grdsp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdsp.PageIndex = e.NewPageIndex;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", Request.QueryString["item"]));
        bll.vBindingGridToSp(ref grdsp, "sp_titem_salespoint_get", arr);
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.close();", true);
    }
    protected void btapp_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@item_cd", Request.QueryString["item"]));
        bll.vApproveItem(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "clos" ,"window.opener.refreshdata();window.close();", true);
    }
}