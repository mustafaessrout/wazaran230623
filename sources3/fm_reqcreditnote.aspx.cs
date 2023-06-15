using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_reqcreditnote : System.Web.UI.Page
{
    cbll bll = new cbll();
    decimal totalinv = 0, totalremain = 0, totalarciAmount = 0, totalBalance = 0,totalvat_amt=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtcn.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbcn, "creditdebit");
            bll.vBindingFieldValueToCombo(ref invtyp, "cndninvtype");
            txreqno.Text = "NEW";
            lbdate.Text = Request.Cookies["waz_dt"].Value.ToString();
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='N'");
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        string sCust = string.Empty;
        List<string> lCust = new List<string>();
        arr.Add(new cArrayList("cust_nm", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        //arr.Add(new cArrayList("@inv_typ", contextKey));
        bll.vSearchMstCust2(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }

  

    protected void btsearch_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grdcn, "sp_tblso_get4", arr);
    }

    protected void grdcn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        TextBox txamount = (TextBox)grdcn.Rows[e.NewSelectedIndex].FindControl("txamount");
        CheckBox chkselect = (CheckBox)grdcn.Rows[e.NewSelectedIndex].FindControl("chselect");
        if (chkselect.Checked)
        {
            txamount.Enabled = true;
        }
        else
        {
            txamount.Enabled = false;
        }
       
    }
    protected void grdcn_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtcn.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            grdcn.EditIndex = -1;
            return;
        }
        grdcn.EditIndex = e.NewEditIndex;
        bindinggrdcn();
        if (invtyp.SelectedValue == "BA")
        {
            TextBox vatEdit = (TextBox)grdcn.Rows[e.NewEditIndex].FindControl("vatEdit");
            vatEdit.Enabled = false;
            vatEdit.CssClass = "ro text-white";
        }
    }
  
    protected void grdcn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        object invtype = invtyp.SelectedValue;

        if (invtype.ToString() == "REG")
        {

        }
        else
        {
            e.Row.Cells[4].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbtotinv = (Label)e.Row.FindControl("lbtotinv");
            Label lbremain = (Label)e.Row.FindControl("lbremain");
            Label lbarciAmount = (Label)e.Row.FindControl("lbarciAmounst");
            Label lbbalance = (Label)e.Row.FindControl("lbbalance");
            Label lbvat_amt = (Label)e.Row.FindControl("lbvat_amt");

            decimal totinv, remain, arciAmount, balance, totvat_amt;
            if (lbtotinv.Text != "")
                totinv = decimal.Parse(lbtotinv.Text);
            else
                totinv = 0;

            if (lbremain.Text != "")
                remain = decimal.Parse(lbremain.Text);
            else
                remain = 0;
            if (lbbalance.Text != "")
                balance = decimal.Parse(lbbalance.Text);
            else
                balance = 0;

            if(lbarciAmount == null)
            {
                arciAmount = 0;
            }
            else
            {
                arciAmount = decimal.Parse(lbarciAmount.Text);
            }

             if (lbvat_amt.Text != "")
                 totvat_amt = decimal.Parse(lbvat_amt.Text);
            else
                 totvat_amt = 0;

            totalinv = totalinv + totinv;
            totalremain = totalremain + remain;
            totalBalance = totalBalance + balance;
            totalarciAmount = totalarciAmount + arciAmount;
            totalvat_amt = totalvat_amt + totvat_amt;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbltotinv = (Label)e.Row.FindControl("lbltotinv");
            Label lblremain = (Label)e.Row.FindControl("lblremain");
            Label lblarciAmount = (Label)e.Row.FindControl("lblarciAmount");
            Label lblbalance = (Label)e.Row.FindControl("lblbalance");
            Label lblvat_amt = (Label)e.Row.FindControl("lblvat_amt");
            Label lbltotalwithvat = (Label)e.Row.FindControl("lbltotalwithvat");
            
            lbltotinv.Text = totalinv.ToString("#,##0.00");
            lblremain.Text = totalremain.ToString("#,##0.00");
            lblbalance.Text = totalBalance.ToString("#,##0.00");
            lblarciAmount.Text = totalarciAmount.ToString("#,##0.00");
            lblvat_amt.Text = totalvat_amt.ToString("#,##0.00");
            lbltotalwithvat.Text = (totalarciAmount + totalvat_amt).ToString();
        }

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        uplPnl.CssClass = "";
        dtcnPnl.CssClass = "";



        string scd = bll.vLookUp("select  dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
        //if (scd == "0" && txreqno.Text == "NEW")
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Block entry because deadline to daily closing !','Please daily closing !','warning');", true);
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        //    return;
        //}
        if (Request.Cookies["waz_dt"].Value.ToString() != dtcn.Text)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction Date Out Of Periode Date','error');", true);
            dtcnPnl.CssClass = "error";
            return;
        }
        if ((txreqno.Text != "NEW") && (txreqno.Text != "") && (invtyp.SelectedValue == "REG"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data can not edited!','Pls contact wazaran admin','warning');", true);
            return;
        }
        if(invtyp.SelectedValue == "REG")
        {
            Label lblarciAmount = (Label)grdcn.FooterRow.FindControl("lblarciAmount");
            if (Convert.ToDouble(lblarciAmount.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount can not empty','Amount to be Debit/Credit','warning');", true);
                return;
            }
        }
        if (upl.HasFile)
        {
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;
            byte[] fs = upl.FileBytes;
            if (fs.Length <= 500000)
            {
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    uplPnl.CssClass = "error";
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                uplPnl.CssClass = "error";
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload Attachment image to entry CN/DN','jpg,bmp,gif and png document');", true);
            uplPnl.CssClass = "error err-empty";
            return;
        }

        if (txreqno.Text == "" || txreqno.Text == "NEW")
        {
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@arcn_date", DateTime.ParseExact(dtcn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@cust_cd", hdcust.Value));
            arr.Add(new cArrayList("@arcn_type", cbcn.SelectedValue.ToString()));
            arr.Add(new cArrayList("@arcn_note", txarcn_note.Text));
            arr.Add(new cArrayList("@sta_id", 'N'));
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cndnrefno", txcndnrefno.Text));
            arr.Add(new cArrayList("@file_nm", ext));
            arr.Add(new cArrayList("@salesman_cd", hdsalesman_cd.Value));
            arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
            string sarcn_no = "";
            bll.vInserttblARCN(arr, ref sarcn_no);
            txreqno.Text = sarcn_no;
            hdfile_nm.Value = sarcn_no + ext;
            if ((upl.FileName != "") || (upl.FileName != null))
            {
                upl.SaveAs(bll.sGetControlParameter("image_path") + "/" + sarcn_no + ext);
            }
            // send email
            string sarcn_type, ssp, sspnm, semp_cd, semp_nm;
            semp_cd = Request.Cookies["usr_id"].Value.ToString();
            semp_nm = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + semp_cd + "'");
            sarcn_type = bll.vLookUp("select fld_desc from tfield_value where fld_nm='creditdebit' and  fld_valu='" + cbcn.SelectedValue + "'");
            ssp = Request.Cookies["sp"].Value.ToString();
            sspnm = bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + ssp + "'");
            string sarcnAmount = bll.vLookUp("select arcnAmount from tblARCN where arcn_no='" + txreqno.Text + "' and salespointcd='" + ssp + "'");
            //  List<string> lInfo = bll.lARCNpproval("ARCNApproval");
            List<string> lInfo = bll.lGetApproval("cndn", 1);
            Random rnd = new Random();
            int nRnd = rnd.Next(1000, 9999);
            string sText = "<html><head><body>Dear New " + sarcn_type + " has been created , with no. " + txreqno.Text + "</br>" +
                "<table><tr><td>Date  </td><td>:</td><td>" + dtcn.Text + "</td></tr>" +
                "<tr><td>Salespoint </td><td>:</td><td>" + ssp + "</td></tr>" +
                "<tr><td>Salespointnm </td><td>:</td><td>" + sspnm + "</td></tr>";
            sText += "<tr><td>Customer </td><td>:</td><td>" + txsearch.Text + "</td></tr>" +
                 "<tr><td>Remark </td><td>:</td><td>" + txarcn_note.Text + "</td></tr>" +
                 "<tr><td>Amount </td><td>:</td><td>" + sarcnAmount + "</td></tr>" +
                  "<tr><td>Create by </td><td>:</td><td>" + semp_nm + "</td></tr></table></br>";

            sText += "<table border='1' width='100%'>	<tr><td>Inv No.</td><td>Inv Amt</td><td>CN/DN (SR)</td><td>Description</td></tr>";
            System.Data.SqlClient.SqlDataReader rs = null;
            string sItem = "";
            arr.Clear();
            arr.Add(new cArrayList("@arcn_no", txreqno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
            bll.vGettblARCNDtl(arr, ref rs);
            while (rs.Read())
            {
                sItem += "<tr><td>" + rs["inv_no"].ToString() + " </td><td> " + rs["totinv"] + " </td><td> " + rs["arciAmount"].ToString() + "</td><td>" + rs["arciDescription"].ToString() + "</td></tr>";
            }
            rs.Close();
            sText += sItem + "</table>";
            sText += "</br></br>Note : If you not take action , within 3 days this new " + sarcn_type + " will be disabled.<br/><br/)";
            sText += "Click <a href='landingpage.aspx?appcode=" + nRnd.ToString() + "&sta=A&trnname=cndn'>Approve</a> OR <a href='landingpage.aspx?appcode=" + nRnd.ToString() + "&sta=A&trnname=cndn'>Reject</a> for your action!";
            string sSubject = "#New " + sarcn_type + " Has Been Created";
            string semail = lInfo[1];
            //bll.vSendMail(semail, sSubject, sText);
            bll.vSendMail(semail, sSubject, sText, sarcn_no + ext);
            arr.Clear();
            arr.Add(new cArrayList("@token", nRnd.ToString()));
            arr.Add(new cArrayList("@doc_typ", sarcn_type));
            arr.Add(new cArrayList("@to", semail));
            arr.Add(new cArrayList("@doc_no", txreqno.Text));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sText));
            arr.Add(new cArrayList("@file_attachment", sarcn_no + ext));
            bll.vInsertEmailOutbox(arr); //by yanto 23-1-2017
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "Transaction " + sarcn_type));
            arr.Add(new cArrayList("@token", nRnd.ToString()));
            arr.Add(new cArrayList("@doc_no", txreqno.Text));
            bll.vInsertEmailSent(arr);

            string smobile_no = lInfo[0];
            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nRnd.ToString();
            string sSMS = "# " + sarcn_type + "Branch : " + sspnm + " No : " + txreqno.Text + " SR : " + sarcnAmount + ", need approval , please Approved ,ex. (Y/N)  Y" + stoken.ToString();
            arr.Clear();
            arr.Add(new cArrayList("@token", stoken.ToString()));
            arr.Add(new cArrayList("@doc_typ", "ARCN"));
            arr.Add(new cArrayList("@to", smobile_no));
            arr.Add(new cArrayList("@doc_no", txreqno.Text));
            arr.Add(new cArrayList("@msg", sSMS));
            bll.vInsertSmsOutbox(arr);
            // bll.vInsertSMSSent(arr);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opd", "window.history.forward();", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Tran No. " + txreqno.Text + "','success');", true);
            //Response.Write("<script language='javascript'>window.alert('Data Save successfully .. Tran No : " + txreqno.Text +  "');</script>");
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Only Amount can update, Data saved succesfully','Tran No: " + txreqno.Text + "','danger');", true);
            //Response.Write("<script language='javascript'>window.alert('Only Amount can update, Data saved succesfully...., Tran No : " + txreqno.Text + "');</script>");
        }
        bindinggrdcn();
        hlpic.Text = hdfile_nm.Value;
        hlpic.NavigateUrl = "../images/" + hdfile_nm.Value;
    }
    protected void btinv_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@user_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value.ToString()));
        arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
        bll.vInserttblARCNDtl_ins1st(arr);
        invtyp.Enabled = false;
        string ssp = Request.Cookies["sp"].Value.ToString();
        txsalesman.Text = bll.vLookUp("select emp_nm from tmst_customer inner join tmst_employee on  tmst_customer.salesman_cd=tmst_employee.emp_cd where cust_cd='" + hdcust.Value + "' and tmst_customer.salespointcd='" + ssp + "'");
        hdsalesman_cd.Value = bll.vLookUp("select emp_cd from tmst_customer inner join tmst_employee on  tmst_customer.salesman_cd=tmst_employee.emp_cd where cust_cd='" + hdcust.Value + "' and tmst_customer.salespointcd='" + ssp + "'");
        lbcustname.Text = txsearch.Text;
        txadress.Text = bll.vLookUp("select cast (isnull(addr,'') as nvarchar(max))+' '+cast (isnull((select loc_nm from tmst_location where loc_cd=city_cd),'') as nvarchar(max)) from tmst_customer where cust_cd='" + hdcust.Value + "' and tmst_customer.salespointcd='" + ssp + "'");
        if (invtyp.SelectedValue == "REG")
        {
            grdcn.Columns[6].HeaderText = "CN / DN (SR)";

        }
        else
        {
            grdcn.Columns[6].HeaderText = "Amount";
        }
        bindinggrdcn();
    }
    public string geturl(object id)
    {
        string url = hdfile_nm.Value;
        return url;
    }
    private void bindinggrdcn()
    {
        totalinv = 0;
        totalremain = 0;
        totalarciAmount = 0;

        string arcn_no;
        if (txreqno.Text == "" || txreqno.Text == "NEW") { arcn_no = Request.Cookies["usr_id"].Value.ToString(); } else { arcn_no = txreqno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
      
        grdcn.Visible = true;
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@arcn_no", arcn_no));
        arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
        bll.vBindingGridToSp(ref grdcn, "sp_tblARCNDtl_get", arr);

    }
    protected void invtyp_selected(object sender, EventArgs e)
    {
        invtyp.Enabled = false;
        if (hdcust.Value != "")
        {
            bindinggrdcn();
        }
    }
    private void bindinggrdcn1()
    {
        totalinv = 0;
        totalremain = 0;
        totalarciAmount = 0;
        string arcn_no;
        if (txreqno.Text == "" || txreqno.Text == "NEW") { arcn_no = Request.Cookies["usr_id"].Value.ToString(); } else { arcn_no = txreqno.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", arcn_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
        bll.vBindingGridToSp(ref grdcn, "sp_tblARCNDtl_get", arr);

    }
    protected void grdcn_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdcn.EditIndex = -1;
        bindinggrdcn();
    }

    
    protected void grdcn_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        if (Request.Cookies["waz_dt"].Value.ToString() != dtcn.Text)
        {
            grdcn.EditIndex = -1;
            return;
        }

        Label lbinv_no = (Label)grdcn.Rows[e.RowIndex].FindControl("lbinv_no");
        Label lbremain = (Label)grdcn.Rows[e.RowIndex].FindControl("lbremain");
        Label lbtotinv = (Label)grdcn.Rows[e.RowIndex].FindControl("lbtotinv");
        TextBox txarciAmount = (TextBox)grdcn.Rows[e.RowIndex].FindControl("txarciAmount");
        TextBox txarciDescription = (TextBox)grdcn.Rows[e.RowIndex].FindControl("txarciDescription");
        //TextBox txtotinv = (TextBox)grdcn.Rows[e.RowIndex].FindControl("txtotinv");
        TextBox lbvat_pct = (TextBox)grdcn.Rows[e.RowIndex].FindControl("vatEdit");
        Label lbvat_amt = (Label)grdcn.Rows[e.RowIndex].FindControl("lbvat_amt");
        Label lbbalance = (Label)grdcn.Rows[e.RowIndex].FindControl("lbbalance");
        string arcn_no;
        double amount = 0;
        try
        {
            amount = Convert.ToDouble(txarciAmount.Text);
        }
        catch{ }

        object invtype = invtyp.SelectedValue;
       
        //if (invtype.ToString() == "REG")
        //{
        double dremain = Convert.ToDouble(lbbalance.Text);
            //double dremainaftervat = (Convert.ToDouble(lbbalance.Text) * 100) / (100 + (100 * Convert.ToDouble(lbvat_pct.Text)));
            //double vatamt = Convert.ToDouble(lbvat_pct.Text)* dremainaftervat;
        if (Convert.ToDouble(txarciAmount.Text) > Convert.ToDouble(lbbalance.Text) && cbcn.SelectedValue == "CN") { txarciAmount.Text = lbbalance.Text; }
        //}
        //else
        //{
            if (cbcn.SelectedValue == "CN")
            {
                //double dremain = 0.00;
                if (lbbalance.Text == "")
                {
                     dremain = 0.00;
                }
                else
                {
                     dremain = Convert.ToDouble(lbbalance.Text);
                }
                //double vat = Convert.ToDouble(lbvat_pct.Text);
                //double dremainaftervat = (dremain - (dremain* vat));
                if (amount > dremain)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Your amount more than "+ lbbalance.Text + "(Remain+Vat)','Plase Reduce your amount','warning');", true);
                    grdcn.EditIndex = -1;
                    return;
                }
            }
            else
            {
                double invamt = Convert.ToDouble(lbtotinv.Text);
                if (amount > invamt)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Your amount more than " + invamt + "(Inv Amount)','Plase Reduce your amount','warning');", true);
                    grdcn.EditIndex = -1;
                    return;
                }
            }
        //}


        if (txreqno.Text == "" || txreqno.Text == "NEW") { arcn_no = Request.Cookies["usr_id"].Value.ToString(); } else { arcn_no = txreqno.Text; }
        double darciAmount = (Convert.ToDouble(txarciAmount.Text) * 100) / (100 + (100 * Convert.ToDouble(lbvat_pct.Text)));
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", arcn_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@inv_no", lbinv_no.Text));
        arr.Add(new cArrayList("@arciAmount", darciAmount));
        arr.Add(new cArrayList("@vat_pct", lbvat_pct.Text));
        arr.Add(new cArrayList("@arciDescription", txarciDescription.Text));
        arr.Add(new cArrayList("@inv_typ", invtyp.SelectedValue));
        bll.vUpdatetblARCNDtl(arr);
        grdcn.EditIndex = -1;
        bindinggrdcn();
    }
   
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_reqcreditnote.aspx");
    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        txreqno.Text = Convert.ToString(Session["looarcn_no"]);
        string sarcn_type = bll.vLookUp("select arcn_type from tblARCN where arcn_no='" + txreqno.Text + "'");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", txreqno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@arcn_type", sarcn_type));
        bll.vGettblARCN(arr, ref rs);
        while (rs.Read())
        {
            dtcn.Text = string.Format("{0:d/M/yyyy}", rs["arcn_date"]);
            cbcn.SelectedValue = rs["arcn_type"].ToString();
            hdcust.Value = rs["cust_cd"].ToString();
            txsearch.Text = rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString();
            txarcn_note.Text = rs["arcn_note"].ToString();
            txcndnrefno.Text = rs["cndnrefno"].ToString();
            string sstatus = rs["sta_id"].ToString();
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='sta_id' and fld_valu='" + sstatus + "'");
            string ssp = Request.Cookies["sp"].Value.ToString();
            string ssalesman_cd = rs["salesman_cd"].ToString();
            txsalesman.Text = bll.vLookUp("select emp_nm from tmst_customer inner join tmst_employee on  tmst_customer.salesman_cd=tmst_employee.emp_cd where cust_cd='" + ssalesman_cd + "' and tmst_customer.salespointcd='" + ssp + "'");
            hdsalesman_cd.Value = ssalesman_cd;
            txadress.Text = bll.vLookUp("select cast (isnull(addr,'') as nvarchar(max))+' '+cast (isnull(city_cd,'') as nvarchar(max)) from tmst_customer where cust_cd='" + hdcust.Value + "' and tmst_customer.salespointcd='" + ssp + "'");
            hdfile_nm.Value = rs["file_nm"].ToString();
            invtyp.SelectedValue = rs["inv_typ"].ToString();
        } rs.Close();
        bindinggrdcn1();
        invtyp.Enabled = false;
        hlpic.Text = hdfile_nm.Value;
        hlpic.NavigateUrl = "../images/" + hdfile_nm.Value;
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@arcn_no", txreqno.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@printby", Request.Cookies["fullname"].Value.ToString()));
        Session["lParamarcn_no"] = arr;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=ARCN');", true);
    }
}