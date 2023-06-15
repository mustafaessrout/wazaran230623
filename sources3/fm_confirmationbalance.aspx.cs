using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_confirmationbalance : System.Web.UI.Page
{
    cbll bll = new cbll();
    double dSubTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            vInit();
            dtPrintDate.Text =Request.Cookies["waz_dt"].Value.ToString();            
        }
                 
    }
    void vInit()
    {

        txremark.Enabled = true;
        txremark.CssClass = "makeitreadwrite";
        txConfirmno.Text = "NEW";
        txConfirmno.CssClass = "makeitreadonly";
        dtPrintDate.Enabled = false;
        btConfirm.Visible = false;
        lbConfirmDate.Visible = false;
        lbConfirmValue.Visible = false;
        dtConfirmDate.Visible = false;
        txConfirmValue.Visible = false;
        lbDoc.Visible = false;
        upl.Visible = false;
        btPrint.Visible = false;

    }
    void vnewInit()
    {
        btConfirm.Visible = true;
        lbConfirmDate.Visible = true;
        lbConfirmValue.Visible = true;
        dtConfirmDate.Visible = true;
        txConfirmValue.Visible = true;
        lbDoc.Visible = true;
        upl.Visible = true;

    }
    protected void btPrint_Click(object sender, EventArgs e)
    {       
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=stmtconf&no=" + txConfirmno.Text + "&cust_cd=" + hdcust.Value.ToString() + "');", true);        
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListCust(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll(); SqlDataReader rs = null;
        HttpCookie cok = HttpContext.Current.Request.Cookies["sp"];
        string sSP = cok.Value.ToString();
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", sSP));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"] + " - " + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());        
    }
    protected void btsearch4_Click(object sender, EventArgs e)
    {
        DateTime dtprintt = DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);        
        txBalance.Text = bll.vLookUp("select isnull(sum(balance),0.00)balance from tdosales_invoice where cust_cd="+hdcust.Value.ToString()+" and inv_sta_id in ('P','R') and inv_dt<='" + dtprintt.Year.ToString() + "-" + dtprintt.Month.ToString("00") + "-" + dtprintt.Day.ToString("00") +"'");
        txBalance.Enabled = false;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stmt_no", null));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@stm_dt", DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_tmst_balanceconfirmation_getdtl", arr);
        txcust.Enabled = false;
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbbalance = (Label)e.Row.FindControl("lbbalance");
                dSubTotal += Convert.ToDouble(lbbalance.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotbalance = (Label)e.Row.FindControl("lbtotbalance");
                lbtotbalance.Text = dSubTotal.ToString();
        }
        }
    protected void btsearch3_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "popupwindow('fm_lookup_confirmbalance.aspx');", true);
        //upl.Visible = true;
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        string sStaID = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@stmt_no", hdcb.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGetCustomerBalanceConfermation(arr, ref rs);
        while (rs.Read())
        {
            hdcust.Value = rs["cust_cd"].ToString();
            txcust.Text = rs["cust_nm"].ToString();
            txBalance.Text = rs["balance"].ToString();
            txremark.Text = rs["remark"].ToString();
            dtPrintDate.Text = Convert.ToDateTime(rs["stmt_dt"]).ToShortDateString();
            bll.sFormat2ddmmyyyy(ref dtPrintDate);
            lbstatus.Text = rs["stmt_sta_nm"].ToString();
            sStaID = rs["stmt_sta_id"].ToString();
            if(sStaID == "C")
            {
                dtConfirmDate.Text = Convert.ToDateTime(rs["confirm_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref dtConfirmDate);
                txConfirmValue.Text = rs["amt_confirm"].ToString();
                hpfile_nm.NavigateUrl = "/images/customerbal/" + rs["file_nm"].ToString();
                hpfile_nm.Visible = true;
                
            }
            else if (sStaID == "CP")
            {
                dtConfirmDate.Text = Convert.ToDateTime(rs["confirm_dt"]).ToShortDateString();
                bll.sFormat2ddmmyyyy(ref dtConfirmDate);
            }
        } rs.Close();
        if (sStaID == "N")
        {
            btConfirm.Visible = true;
            lbConfirmDate.Visible = true;
            lbConfirmValue.Visible = true;
            dtConfirmDate.Visible = true;
            dtConfirmDate.CssClass = "makeitreadonly";
            txConfirmValue.Visible = true;
            txConfirmValue.Enabled = true;
            txConfirmValue.Text = "";
            dtConfirmDate.Enabled = true;
            dtConfirmDate.Text = "";
            lbDoc.Visible = true;
            upl.Visible = true;
            txcust.Enabled = false;
            dtPrintDate.Enabled = false;
            txremark.Enabled = false;
            txBalance.Enabled = false;
            txConfirmno.Enabled = false;
            btPrint.Visible = true;
            //dtConfirmDate.Text = Response.Cookies["waz_dt"].Value;
        }
        else if (sStaID == "C")
        {
            btConfirm.Visible = false;
            lbConfirmDate.Visible = true;
            lbConfirmValue.Visible = true;
            dtConfirmDate.Visible = true;
            dtConfirmDate.CssClass = "makeitreadonly";
            txConfirmValue.Visible = true;
            lbDoc.Visible = false;
            upl.Visible = false;
            txcust.Enabled = false;
            dtPrintDate.Enabled = false;
            txremark.Enabled = false;
            txBalance.Enabled = false;
            txConfirmno.Enabled = false;
            dtConfirmDate.Enabled = false;
            txConfirmValue.Enabled = false;
            btPrint.Visible = true;
            btsave.Visible = false;
            lbdocv.Visible = true;
            //lbfileloc.Visible = true;
            
        }
        else if (sStaID == "CP")
        {
            btConfirm.Visible = false;
            lbConfirmDate.Visible = true;
            lbConfirmValue.Visible = false;
            dtConfirmDate.Visible = true;
            dtConfirmDate.CssClass = "makeitreadonly";            
            lbDoc.Visible = false;
            upl.Visible = false;
            txcust.Enabled = false;
            dtPrintDate.Enabled = false;
            txremark.Enabled = false;
            txBalance.Enabled = false;
            txConfirmno.Enabled = false;
            txBalance.Visible = false;
            dtConfirmDate.Enabled = false;
            btPrint.Visible = true;
            btsave.Visible = false;
            txConfirmValue.Visible = false;
        }
        arr.Clear();
        arr.Add(new cArrayList("@stmt_no", null));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@stm_dt", DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_tmst_balanceconfirmation_getdtl", arr);
        txcust.Enabled = false;
    }
    protected void btConfirm_Click(object sender, EventArgs e)
    {
      
        if (txConfirmValue.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Balance is Empty!','Please check the data','warning');", true);
            return;
        }
        if (dtConfirmDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date is empty','Please insert it','warning');", true);
            return;
        } 
        DateTime dt1=DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        DateTime dt2=DateTime.ParseExact(dtConfirmDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        if (dt2 < dt1)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Confirm Date should be after','the statment date','warning');", true);
            return;
        }
        if (upl.FileName == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('No Image uploaded','Please scan the document and upload','warning');", true);
            return;
        }        
        string datest = "";
        btConfirm.Visible = false;
        List<cArrayList> arr = new List<cArrayList>();
       // arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));   
        arr.Add(new cArrayList("@confirm_dt", DateTime.ParseExact(dtConfirmDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@amt_confirm", txConfirmValue.Text));
        arr.Add(new cArrayList("@stmt_no", txConfirmno.Text));
        arr.Add(new cArrayList("@stmt_dt", DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));        
        arr.Add(new cArrayList("@amt", txBalance.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        
        FileInfo fi = new FileInfo(upl.FileName);
        string ext = fi.Extension;
        byte[] fs = upl.FileBytes;
        if (fs.Length <= 500000)
        {
            if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
            {               
                if ((upl.FileName != "") || (upl.FileName != null))
                {

                    arr.Add(new cArrayList("@file_nm", hdcust.Value + txConfirmno.Text + ext));
                    upl.SaveAs(bll.sGetControlParameter("image_path") + "/customerbal/" + hdcust.Value + txConfirmno.Text + ext);

                }               
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
            return;
        }                           


        bll.vUpdateTmst_balanceconfirmation(arr, ref datest);
        if(datest=="15")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Balance has been Confirmed','','warning');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('More than 15 days between','Print date and confirm date Amount is #" +txBalance.Text+ "#','warning');", true);
        }
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='stmt_sta_id' and fld_valu='select stmt_sta_id from tmst_balanceconfirmation where stmt_no="+txConfirmno.Text+"'");
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (hdcust.Value.ToString() == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer must be selected!','Customer has not selected','warning');", true);
            return;
        }
        if (txBalance.Text == "0.00")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Balance is Zero!','No Need to make report','warning');", true);
            return;
        }
        //DateTime dtch = DateTime.ParseExact(dtPrintDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //string x = bll.vLookUp("select max(nextstmt_dt) from tmst_balanceconfirmation where cust_cd=" + hdcust.Value.ToString() + " and stmt_dt='" + dtch.Year.ToString() + "-" + dtch.Month.ToString("00") + "-" + dtch.Day.ToString("00") + "'");
        string x = bll.vLookUp("select max(convert(date,nextstmt_dt)) from tmst_balanceconfirmation where cust_cd=" + hdcust.Value.ToString() + "");
        if (x != "")
        {
            TextBox dtCodtcheck = new TextBox();
            dtCodtcheck.Text = Convert.ToDateTime(x).ToShortDateString();
            bll.sFormat2ddmmyyyy(ref dtCodtcheck);
            DateTime dtch = DateTime.ParseExact(dtCodtcheck.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            if (x != "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('You already print statement ','The next statment will be in " + dtch.Day.ToString() + "-" + dtch.Month.ToString("00") + "-" + dtch.Year.ToString("00") + "','warning');", true);
                return;
            }
        }
        if (dtPrintDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Date is empty','Please insert it','warning');", true);
            return;
        }
        string stmtNo = "";
        btsave.Visible = false;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@stmt_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@printedby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@amt", txBalance.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@stmt_sta_id", "N"));
        bll.vInsertTmst_balanceconfirmation(arr, ref stmtNo);
        arr.Clear();
        arr.Add(new cArrayList("@stmt_no", stmtNo));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@stm_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertTmst_balanceconfirmation_dtl(arr);
        txConfirmno.Text = stmtNo;
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='stmt_sta_id' and fld_valu='N'");
        vnewInit();
        btPrint.Visible = true;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Statment has been Created','Statment No #" +stmtNo+ "#','warning');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_confirmationbalance.aspx");
    }
}
