using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class fm_lookup_ARRecAddDtl : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.QueryString["salesPointCD"]));
        arr.Add(new cArrayList("@CustCD", Request.QueryString["CustCD"]));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@ARRecID", Request.QueryString["ARRecID"]));
        bll.vInserttblARRecAddDtl(arr);
        binding();
        }

        //if (Request.QueryString["ARCType"]=="1")
        //{
        //    grd.Columns[4].Visible = false;
        //    grd.Columns[5].Visible = false;
        //    grd.Columns[6].Visible = false;
        //    grd.Columns[7].Visible = false;
        //}
        //else if (Request.QueryString["ARCType"]=="2")
        //{
        //    grd.Columns[4].Visible = true;
        //    grd.Columns[5].Visible = true;
        //    grd.Columns[6].Visible = true;
        //    grd.Columns[7].Visible = false;
        //}
        //else if (Request.QueryString["ARCType"] == "3")
        //{
        //    grd.Columns[4].Visible = true;
        //    grd.Columns[5].Visible = true;
        //    grd.Columns[6].Visible = true;
        //    grd.Columns[7].Visible = true;
        //}
        //if (Request.QueryString["ARCType"] == "4")
        //{
        //    grd.Columns[4].Visible = false;
        //    grd.Columns[5].Visible = false;
        //    grd.Columns[6].Visible = false;
        //    grd.Columns[7].Visible = false;
        //}
        
        
    }
    void binding()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@salespointcd", Request.QueryString["salesPointCD"]));
        arr.Add(new cArrayList("@CustCD", Request.QueryString["CustCD"]));
        bll.vBindingGridToSp(ref grd, "sp_tblARRecAddDtl_get", arr);
    }
   
    protected void btsearch_Click(object sender, EventArgs e)
    {
        

    }
    protected void btok_Click(object sender, EventArgs e)
    {
        string sARRecID = "0";
        if (Request.QueryString["ARRecID"] == "")
        {
            List<cArrayList> arr = new List<cArrayList>();
            string sARRecCD = "0";
            arr.Add(new cArrayList("@recDate", DateTime.ParseExact(Request.QueryString["recDate"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@SalesCD", Request.QueryString["SalesCD"]));
            arr.Add(new cArrayList("@SalesPointCD", Request.QueryString["salesPointCD"]));
            arr.Add(new cArrayList("@ARCType", Request.QueryString["ARCType"]));
            arr.Add(new cArrayList("@CustCD", Request.QueryString["CustCD"]));
            bll.vInsertTblARRec(arr, ref sARRecCD);
            sARRecID = bll.vLookUp("select ARRecID from tblARRec where ARRecCD='" + sARRecCD + "' AND SalesPointCD='" + Request.QueryString["salesPointCD"] + "'");
        }
        else 
        {
            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@ARRecID", Request.QueryString["ARRecID"]));
            arr.Add(new cArrayList("@recDate", DateTime.ParseExact(Request.QueryString["recDate"], "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@SalesCD", Request.QueryString["SalesCD"]));
            arr.Add(new cArrayList("@SalesPointCD", Request.QueryString["SalesPointCD"]));
            arr.Add(new cArrayList("@ARCType", Request.QueryString["ARCType"]));
            arr.Add(new cArrayList("@CustCD", Request.QueryString["CustCD"]));
            bll.vUpdateTblARRec(arr);
            sARRecID = Request.QueryString["ARRecID"];
        }
       
        List<cArrayList> arr2 = new List<cArrayList>();
        arr2.Add(new cArrayList("@ARRecID", sARRecID));
        arr2.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
        arr2.Add(new cArrayList("@SalesPointCD", Request.QueryString["SalesPointCD"]));
        bll.vInsertTblARRecDtlAddDtl(arr2);

        Session["looARRecID"] = sARRecID;
        Session["looARRecSalespointCD"] = Request.QueryString["SalesPointCD"];
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl2", "closewin()", true);
        
    }
   
    protected void grd_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label lbARRecID = (Label)grd.SelectedRow.FindControl("lbARRecID");
        Label lbsalespointCD = (Label)grd.SelectedRow.FindControl("lbsalespointCD");

        Session["looARRecID"] = lbARRecID.Text;
        Session["looARRecSalespointCD"] = lbsalespointCD.Text;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        GridViewRow row = (GridViewRow)grd.Rows[e.RowIndex];
        Label lbID=(Label)row.FindControl("lbID");
        Label lbBalAmt = (Label)row.FindControl("lbBalAmt");
        TextBox txARCAmt = (TextBox)row.FindControl("txARCAmt");
        //TextBox txARCDocNo = (TextBox)row.FindControl("txARCDocNo");
        //DropDownList cbBankID = (DropDownList)row.FindControl("cbBankID");
        //TextBox txARCDate = (TextBox)row.FindControl("txARCDate");
        //TextBox txARCDueDate = (TextBox)row.FindControl("txARCDueDate");

        
        decimal dcmARCAmt = Convert.ToDecimal(txARCAmt.Text);
        decimal dcmBalAmt = Convert.ToDecimal(lbBalAmt.Text);

        if (dcmARCAmt > dcmBalAmt)
        {

            txARCAmt.Text = lbBalAmt.Text;
        }
        
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@ID",lbID.Text));
        arr.Add(new cArrayList("@ARCAmt",Convert.ToDecimal( txARCAmt.Text)));
        //arr.Add(new cArrayList("@ARCDocNo", txARCDocNo.Text));
        //arr.Add(new cArrayList("@BankID", cbBankID.SelectedValue));
        //arr.Add(new cArrayList("@ARCDate", txARCDate.Text));
        //arr.Add(new cArrayList("@ARCDueDate", txARCDueDate.Text));
        bll.vUpdatetblARRecAddDtl(arr);

        grd.EditIndex = -1;
        binding();
        lbTotal.Text = bll.vLookUp("select sum(ARCAmt) from twrk_tblARRecAddDtl where usr_id='" + Request.Cookies["usr_id"].Value + "' and SalesPointCD='" + Request.QueryString["SalesPointCD"] + "' AND CustCD='"+Request.QueryString["CustCD"] +"'"); 
        
    }
    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grd.EditIndex = -1;
        binding();
    }
    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grd.EditIndex = e.NewEditIndex;
        binding();
        GridViewRow row = (GridViewRow)grd.Rows[e.NewEditIndex];
        Label lbBalAmt = (Label)row.FindControl("lbBalAmt");
        TextBox txARCAmt = (TextBox)row.FindControl("txARCAmt");
        //DropDownList cbBankID = (DropDownList)row.FindControl("cbBankID");
        txARCAmt.Text = lbBalAmt.Text;
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        //bll.vBindingComboToSp(ref cbBankID, "sp_tblBank_get", "BankID", "banViewName", arr);
   

    }
   
}