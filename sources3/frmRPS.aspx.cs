using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
public partial class frmRPS : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbSalesPointCD, "sp_tmst_salespoint_get", "salespointcd", "salespoint_desc");
            cbSalesPointCD.SelectedValue = Request.Cookies["sp"].Value;
            rbDayCD.SelectedValue = "1";
            
        }
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmRPS.aspx");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        hdrsave(sender, e);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('The record have been sucessfuly added','','success');", true);
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        bll.vDeleteTblRPS(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Deleted successfully','','success');", true);        
        Response.Redirect("frmRPS.aspx");
    }
    protected void btAdd_Click(object sender, EventArgs e)
    {
        if (txRPSCD.Text== "")
        {
            hdrsave(sender, e);
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@cust_cd", hdcust_cd.Value.ToString()));
        bll.vInsertTblRPSDtl(arr);

        clearAdd();
        txsearchCust.Focus();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblRPSDtl_get", arr);
    }
    private void clearAdd()
    {
        txsearchCust.Text = "";
    }
    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbRPSDetID = (Label)grd.Rows[e.RowIndex].FindControl("lbRPSDetID");
        Label lbSalesPointCD = (Label)grd.Rows[e.RowIndex].FindControl("lbSalesPointCD");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@RPSDetID", lbRPSDetID.Text));
        arr.Add(new cArrayList("@SalesPointCD", lbSalesPointCD.Text));
        bll.vDeleteTblRPSDet(arr);
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblRPSDtl_get", arr);
    }
  
    protected void hdrsave(object sender, EventArgs e)
    {
        if (txRPSCD.Text == null || txRPSCD.Text == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sRPSCD = "0";
            arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@salesman_CD", hdsalesman_CD.Value));
            arr.Add(new cArrayList("@DayCD", rbDayCD.SelectedValue));
            bll.vInserttblRPS(arr, ref sRPSCD);
            txRPSCD.Text= sRPSCD;
            txKey.Text = bll.vLookUp("select RPSID from tblRPS where RPSCD='" + sRPSCD + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
            
            arr.Clear();
            arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
            arr.Add(new cArrayList("@RPSID", txKey.Text));
            bll.vBindingGridToSp(ref grd, "sp_tblRPSDtl_get", arr);

        }
        //else
        //{
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@RPSID", txKey.Text));
        //    arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        //    arr.Add(new cArrayList("@salesman_CD", cbSalesCD.SelectedValue));
        //    arr.Add(new cArrayList("@DayCD", rbDayCD.SelectedValue));
        //    bll.vUpdatetblReturn(arr);
        //    //arr.Clear();
        //    //arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        //    //arr.Add(new cArrayList("@returnID", txKey.Text));
        //    //bll.vBindingGridToSp(ref grd, "sp_tblReturnDtl_get", arr);

        //}
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListSalesman_CD(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sSalesman = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lSalesman = new List<string>();
        arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@emp_desc", prefixText));
        bll.vSearchSalesman_CD(arr, ref rs);
        while (rs.Read())
        {
            sSalesman = AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + " | " + rs["emp_nm"].ToString(), rs["emp_cd"].ToString());
            lSalesman.Add(sSalesman);
        } rs.Close();
        return (lSalesman.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCust(string prefixText, int count, string contextKey)
    {
        HttpCookie cookieSP;
        cookieSP = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        SqlDataReader rs = null;
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lCust = new List<string>();
        arr.Add(new cArrayList("@SalesPointCD", cookieSP.Value.ToString()));
        arr.Add(new cArrayList("@cust_nm", prefixText));
        bll.vSearchMstCust2(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + " | " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        } rs.Close();
        return (lCust.ToArray());
    }


    protected void txsearchsalesman_CD_TextChanged(object sender, EventArgs e)
    {
        display();
    }
    protected void display()
    {
        txKey.Text = null;
        txRPSCD.Text = null;
        txKey.Text = bll.vLookUp("select RPSID from tblRPS where salesman_CD='" + hdsalesman_CD.Value + "' AND DayCD='" + rbDayCD.SelectedValue + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
        txRPSCD.Text = bll.vLookUp("select RPSCD from tblRPS where salesman_CD='" + hdsalesman_CD.Value + "' AND DayCD='" + rbDayCD.SelectedValue + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@SalesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        bll.vGetTblRPS(arr, ref rs);
        while (rs.Read())
        {
            txRPSCD.Text = rs["RPSCD"].ToString();
            cbSalesPointCD.SelectedValue = rs["SalesPointCD"].ToString();
            rbDayCD.SelectedValue = rs["DayCD"].ToString();
            hdsalesman_CD.Value = rs["salesman_CD"].ToString();
            txsearchsalesman_CD.Text = rs["salesman_CD"].ToString() + " | " + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + rs["salesman_CD"].ToString() + "' AND SalesPointCD='" + cbSalesPointCD.SelectedValue + "'");
        } rs.Close();
        arr.Clear();
        arr.Add(new cArrayList("@salesPointCD", cbSalesPointCD.SelectedValue));
        arr.Add(new cArrayList("@RPSID", txKey.Text));
        bll.vBindingGridToSp(ref grd, "sp_tblRPSDtl_get", arr);
    }
    protected void rbDayCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        display();
    }
}