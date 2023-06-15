using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class fm_cndnCustomer : System.Web.UI.Page
{
    //cbll bll = new cbll();
    //cdal cdl = new cdal();

    //Utitlity ut = new Utitlity();
    cbll2 bll2 = new cbll2();
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtpost.Text = Request.Cookies["waz_dt"].Value.ToString();//bll.sGetControlParameter("wazaran_dt");//DateTime.Now.ToString("d/M/yyyy");
            List<cArrayList> arr = new List<cArrayList>();
            dtcndn.Text = Request.Cookies["waz_dt"].Value;
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", "cndnc"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            bll.vBindingComboToSpWithEmptyChoosen(ref cbtax, "sp_tmst_tax_get", "tax_cd", "tax_desc");
            arr.Clear();
            arr.Add(new cArrayList("@doc_typ", "cndncustomer"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            arr.Add(new cArrayList("@level_no", 1));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbapproval, "sp_tapprovalpattern_getbytype", "emp_cd", "emp_nm", arr);
            bll.vBindingFieldValueToComboWithChoosen(ref cbtype, "cndn_typ");
            cd.v_hiddencontrol(btSave);
            cd.v_disablecontrol(dtcndn);
        }
    }

    protected void btlookup_Click(object sender, EventArgs e)
    {
        lbsysno.Text = hdfCNDNID.Value;
        cd.v_showcontrol(btSave);
        cd.v_disablecontrol(txcust);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lCust = new List<string>();
        string sCust = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        // arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lCust.Add(sCust);
        }
        rs.Close();
        return (lCust.ToArray());
    }



    protected void ddlCNDN_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grd.EditIndex = -1;
            BindGrid();
        }
        catch (Exception ex)
        {
            // ut.Logs("", "Account", "DN Customer", "fm_cndnCustomer", "grd_RowCancelingEdit", "Exception", ex.Message + ex.InnerException);
        }
    }


    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            Label lblDN_no = (Label)grd.Rows[e.RowIndex].FindControl("lblDN_no");
            TextBox txtDNAmt = (TextBox)grd.Rows[e.RowIndex].FindControl("txtDNAmt");
            Label lblStatus = (Label)grd.Rows[e.RowIndex].FindControl("lblStatus");

            if (lblStatus.Text != "")
            {
                if (Convert.ToString(lblStatus.Text) == "New")
                {
                    if (lblDN_no.Text != "")
                    {
                        string dnNumber = lblDN_no.Text;
                        decimal dnAmt = Convert.ToDecimal(txtDNAmt.Text);

                        decimal vat = 0;
                        //vat  =Convert.ToDecimal( String.Format("{0:0.00}", (Convert.ToDecimal(txtDNAmt.Text) - ((Convert.ToDecimal(txtDNAmt.Text) * 100) / 105))));
                        //if (ddlCNDNType.SelectedValue == "VAT")
                        //{
                        //    dnAmt = dnAmt - vat;
                        //}

                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@cndn_no", Convert.ToString(dnNumber)));
                        arr.Add(new cArrayList("@status", Convert.ToString("N")));
                        arr.Add(new cArrayList("@status", Convert.ToString("N")));
                        arr.Add(new cArrayList("@amt", dnAmt));
                        arr.Add(new cArrayList("@vat", vat));
                        arr.Add(new cArrayList("@totamt", (dnAmt + vat)));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        bll.vUpdtDNCutomerAmt(arr);
                        grd.EditIndex = -1;

                        #region
                        int nrnd = 0;
                        Random rnd = new Random();
                        nrnd = rnd.Next(1000, 9999);
                        string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString());
                        string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + nrnd.ToString();

                        string cndnAmount = bll.vLookUp("select amt from tmst_cndncustomer  where cndn_no = '" + lblDN_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                        string sMsg = "#DN Customer-" + ssalespoint + "-" + lblDN_no.Text + ", DN Amount '" + cndnAmount + "' , do you want to approved : (Y/N)" + stoken;
                        arr.Clear();

                        DataTable dtEmployee = new DataTable();
                        arr.Add(new cArrayList("@emp_cd", cbapproval.SelectedValue));
                        // dtEmployee = cdl.GetValueFromSP("sp_tuser_profile_getbyempcd", arr);


                        arr.Add(new cArrayList("@token", stoken));
                        arr.Add(new cArrayList("@doc_no", lblDN_no.Text));
                        arr.Add(new cArrayList("@doc_typ", "CNDNCustomer"));
                        arr.Add(new cArrayList("@to", Convert.ToString(dtEmployee.Rows[0]["mobile_no"])));
                        arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                        bll.vInsertSmsOutbox(arr);

                        // Sending Email 
                        string sSubject = ""; string sMessage = "";
                        string sfile_attachment = string.Empty;
                        string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                        //string cashout_typ = ddlCNDN.SelectedValue.ToString();

                        sSubject = "#DN Customer Request Branch " + bll.sGetSalespointname(HttpContext.Current.Request.Cookies["sp"].Value.ToString()) + " has been created ";

                        string filePath = bll.sGetControlParameter("image_path") + @"CNDNCust\";
                        string fileName = lblDN_no.Text + ".pdf";
                        string fileExcelName = lblDN_no.Text + ".xls";
                        arr.Clear();
                        arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@cndn_no", lblDN_no.Text));
                        arr.Add(new cArrayList("p_user", HttpContext.Current.Request.Cookies["sp"].Value.ToString()));
                        creport rep = new creport();
                        rep.vShowReportToPDF("rp_cndnCustomer.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileName);
                        //rep.vShowReportToPDF("rp_acccndnCustApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileName);
                        //rep.vShowReportToEXCEL("rp_acccndnCustApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileExcelName);

                        string userUploadFile = bll.vLookUp("SELECT filedoc FROM tmst_cndncustomer WHERE cndn_no ='" + lblDN_no.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
                        sMessage = "<table><tr><td>#DN Customer Request Approval Amount update by user</td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr>" +
                            "<tr><td>DN Customer Code</td><td>:</td><td>" + lblDN_no.Text + "</td></tr>" +
                            "<tr><td>DN Amount</td><td>:</td><td>" + cndnAmount + "</td></tr>" +
                            "<tr><td>Please Click this  for View Document</td><td>:</td><td> <a href='" + slink_branch
                        + "/images/CNDNCust/" + userUploadFile + "'>View Document</a></td></tr>" +
                            "</table>" +
                        "<p> Please Click this  for approved : <a href='" + slink_branch
                        + "/landingpage2.aspx?src=acccndnCustApp&salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString()
                        + "&ids=" + lblDN_no.Text + "&sta=A&appBy=" + cbapproval.SelectedValue + "'>Approve</a>, or for rejected please click <a href='"
                        + slink_branch + "/landingpage2.aspx?src=acccndnCustApp&salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString() + "&ids="
                        + lblDN_no.Text + "&sta=R&appBy=" + cbapproval.SelectedValue + "'>Reject</a></p>" +
                        " \n\r\n\r\n\r\n Wazaran Admin";

                        bll.vSendMail(Convert.ToString(dtEmployee.Rows[0]["email"]), sSubject, sMessage, @"CNDNCust\" + fileName);
                        arr.Clear();
                        arr.Add(new cArrayList("@trxcd", "acccndnCustApp"));
                        arr.Add(new cArrayList("@token", nrnd.ToString()));
                        arr.Add(new cArrayList("@doc_no", lblDN_no.Text));
                        bll.vInsertEmailSent(arr);
                        arr.Clear();
                        sfile_attachment = @"CNDNCust\" + fileName;
                        arr.Add(new cArrayList("@token", nrnd.ToString()));
                        arr.Add(new cArrayList("@doc_typ", "acccndnCustPDF"));
                        arr.Add(new cArrayList("@to", Convert.ToString(dtEmployee.Rows[0]["email"])));
                        arr.Add(new cArrayList("@doc_no", lblDN_no.Text));
                        arr.Add(new cArrayList("@emailsubject", sSubject));
                        arr.Add(new cArrayList("@msg", sMessage));
                        arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                        bll.vInsertEmailOutbox(arr);


                        #endregion


                        BindGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('It is already use. Select another DN Customer','Wrong DN edit','warning');", true);
                    return;
                }
            }

        }
        catch (Exception ex)
        {
            //ut.Logs("", "Account", "DN Customer", "fm_cndnCustomer", "grd_RowUpdating", "Exception", ex.Message + ex.InnerException);
        }

    }

    void BindGrid()
    {
        grd.DataSource = null;
        grd.DataBind();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getByCust", arr);
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grd.EditIndex = e.NewEditIndex;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getByCust", arr);
        }
        catch (Exception ex)
        {
            //ut.Logs("", "Account", "DN Customer", "fm_cndnCustomer", "grd_RowEditing", "Exception", ex.Message + ex.InnerException);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editTable", "editTable();", true);
    }



    protected void btnNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cndnCustomer.aspx");
    }

    [WebMethod]
    public static bool getData()
    {
        return true;
    }




    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveDataValue(cndncustomer cndn)
    {
        try
        {
            cbll bll = new cbll();
            cdal cdl = new cdal();
            string cndn_no = string.Empty;
            creport rep = new creport();
            List<cArrayList> arr = new List<cArrayList>();

            decimal dnAmount = cndn.amt;
            if (cndn.isVat == true)
            {
                dnAmount = cndn.amt - cndn.vat;
            }

            arr.Add(new cArrayList("@cust_cd", cndn.cust_cd));
            arr.Add(new cArrayList("@cndn_dt", cndn.cndn_dt));
            arr.Add(new cArrayList("@post_dt", cndn.post_dt));
            arr.Add(new cArrayList("@cndncust_sta_id", "N"));
            arr.Add(new cArrayList("@refno", cndn.refno));
            arr.Add(new cArrayList("@remark", cndn.remark));
            arr.Add(new cArrayList("@amt", dnAmount));
            arr.Add(new cArrayList("@isVat", cndn.isVat));
            arr.Add(new cArrayList("@vat", cndn.vat));
            arr.Add(new cArrayList("@crdb", cndn.crdb));
            arr.Add(new cArrayList("@filedoc", cndn.filedoc));
            arr.Add(new cArrayList("@authForApprove", cndn.approval));
            arr.Add(new cArrayList("@fileExtension", cndn.fileExtension));
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@createdBy", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value.ToString())));
            bll.vInsCNDNCustomer(arr, ref cndn_no);

            if (cndn_no == "-2")
            {
                if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNCust/" + cndn.filedoc))
                {
                    File.Delete(bll.sGetControlParameter("image_path") + "/CNDNCust/" + cndn.filedoc);
                }
                return cndn_no;
            }
            System.IO.File.Move(bll.sGetControlParameter("image_path") + @"CNDNCust\" + cndn.filedoc, bll.sGetControlParameter("image_path") + @"CNDNCust\" + cndn_no + "_doc" + cndn.fileExtension);
            #region
            int nrnd = 0;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString());
            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='" + HttpContext.Current.Request.Cookies["sp"].Value.ToString() + "'") + nrnd.ToString();

            string cndnAmount = bll.vLookUp("select amt from tmst_cndncustomer  where cndn_no = '" + cndn_no + "' and salespointcd='" + HttpContext.Current.Request.Cookies["sp"].Value.ToString() + "'");
            string sMsg = "#DN Customer-" + ssalespoint + "-" + cndn_no + ", DN Amount '" + cndnAmount + "' , do you want to approved : (Y/N)" + stoken;
            arr.Clear();

            DataTable dtEmployee = new DataTable();
            arr.Add(new cArrayList("@emp_cd", cndn.approval));
            dtEmployee = cdl.GetValueFromSP("sp_tuser_profile_getbyempcd", arr);

            arr.Clear();
            arr.Add(new cArrayList("@token", stoken));
            arr.Add(new cArrayList("@doc_no", cndn_no));
            arr.Add(new cArrayList("@doc_typ", "CNDNCustomer"));
            arr.Add(new cArrayList("@to", Convert.ToString(dtEmployee.Rows[0]["mobile_no"])));
            arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
            // open when live

            bll.vInsertSmsOutbox(arr);

            // Sending Email 
            string sSubject = ""; string sMessage = "";
            string sfile_attachment = string.Empty;
            string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
            //string cashout_typ = ddlCNDN.SelectedValue.ToString();

            sSubject = "#DN Customer Request Branch " + bll.sGetSalespointname(HttpContext.Current.Request.Cookies["sp"].Value.ToString()) + " has been created ";

            string filePath = bll.sGetControlParameter("image_path") + @"CNDNCust\";
            string fileName = cndn_no + ".pdf";
            string fileExcelName = cndn_no + ".xls";
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@cndn_no", cndn_no));
            arr.Add(new cArrayList("p_user", HttpContext.Current.Request.Cookies["sp"].Value.ToString()));

            rep.vShowReportToPDF("rp_cndnCustomer.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileName);
            //rep.vShowReportToPDF("rp_acccndnCustApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileName);
            //rep.vShowReportToEXCEL("rp_acccndnCustApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNCust\" + fileExcelName);


            sMessage = "<table><tr><td>#DN Customer Request Approval</td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr>" +
                "<tr><td>DN Customer Code</td><td>:</td><td>" + cndn_no + "</td></tr>" +
                "<tr><td>DN Amount</td><td>:</td><td>" + cndnAmount + "</td></tr>" +
                "<tr><td>Please Click this  for View Document</td><td>:</td><td> <a href='" + slink_branch
            + "/images/CNDNCust/" + cndn_no + "_doc" + cndn.fileExtension + "'>View Document</a></td></tr>" +
                "</table>" +
            "<p> Please Click this  for approved : <a href='" + slink_branch
            + "/landingpage2.aspx?src=acccndnCustApp&salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString()
            + "&ids=" + cndn_no + "&sta=A&updatMethod=email&appBy=" + cndn.approval + "'>Approve</a>, or for rejected please click <a href='"
            + slink_branch + "/landingpage2.aspx?src=acccndnCustApp&salespointcd=" + HttpContext.Current.Request.Cookies["sp"].Value.ToString() + "&ids="
            + cndn_no + "&sta=R&updatMethod=email&appBy=" + cndn.approval + "'>Reject</a></p>" +
            " \n\r\n\r\n\r\n Wazaran Admin";
            // open when live
            bll.vSendMail(Convert.ToString(dtEmployee.Rows[0]["email"]), sSubject, sMessage, @"CNDNCust\" + fileName);
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "acccndnCustApp"));
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_no", cndn_no));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            sfile_attachment = @"CNDNCust\" + fileName;
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_typ", "acccndnCustPDF"));
            arr.Add(new cArrayList("@to", Convert.ToString(dtEmployee.Rows[0]["email"])));
            arr.Add(new cArrayList("@doc_no", cndn_no));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sMessage));
            arr.Add(new cArrayList("@file_attachment", sfile_attachment));
            bll.vInsertEmailOutbox(arr);


            #endregion


            return cndn_no;
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();

        }
    }


    public class cndncustomer
    {
        public string cndn_no { get; set; }
        public string cust_cd { set; get; }
        public string cndn_dt { set; get; }
        public string post_dt { set; get; }
        public string cndncust_sta_id { get; set; }
        public string refno { get; set; }
        public string remark { get; set; }
        public decimal amt { get; set; }
        public string crdb { get; set; }
        public string filedoc { get; set; }
        public string approval { get; set; }
        public string fileExtension { get; set; }
        public bool isVat { get; set; }
        public decimal vat { get; set; }
    }


    //[HttpPost]
    //public JsonResult CanApprove(int[] selectedIds)
    //{
    //    bool canApprove = service.CanApprove(selectedIds, out string reason, out int[] approvableIDs);
    //    return Json(new { success = canApprove, ex = reason, approvableIDList = approvableIDs }, JsonRequestBehavior.AllowGet);
    //}

    protected void btSave_Click(object sender, EventArgs e)
    {
        double _amount = 0;
        string fileExtension = string.Empty;
        if (hdcust.Value == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Customer must be selected !!','Customer','warning');", true);
            return;
        }
        if (!double.TryParse(txtAmount.Text, out _amount))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('All amount pay must be numeric !!','amount','warning');", true);
            return;
        }
        string cnt = bll.vLookUp("SELECT count(*) FROM tmst_cndncustomer WHERE  refno ='" + txtManual.Text + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
        if (cnt != "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual number already exist, select another ','Manual number wrong','warning');", true);
            return;
        }
        if (dtpost.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please fill post date correctly ! ','Post date','warning');", true);
            return;
        }
        if (cbtax.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select tax rate ! ','Tax Rate','warning');", true);
            return;
        }
        double _taxrate = Convert.ToDouble( bll.vLookUp("select tax_formula from tmst_tax where tax_cd='"+cbtax.SelectedValue+"'"));
        double _basetax =1/ (1 + _taxrate) * _amount;
        double _vat = _amount - _basetax;

        //double _amt = Convert.ToDouble(txamt.Text);
        //double _vatrate = Convert.ToDouble(bll.vLookUp("select tax_formula from tmst_tax where tax_cd='" + cbvatrate.SelectedValue + "'"));
        //double _pricebased = 1 / (1 + _vatrate) * _amt;
        //txbasedprice.Text = _pricebased.ToString("N2");
        //lbvat.Text = (_amt - _pricebased).ToString("N2");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_dt", System.DateTime.ParseExact(dtcndn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@cust_cd", hdcust.Value));
        arr.Add(new cArrayList("@salesman_cd", ""));
        arr.Add(new cArrayList("@post_dt", System.DateTime.ParseExact(dtpost.Text ,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@cndncust_sta_id", "N"));
        arr.Add(new cArrayList("@refno", txtManual.Text));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@amt", _basetax));
        arr.Add(new cArrayList("@tax_cd", cbtax.SelectedValue));
        arr.Add(new cArrayList("@vat", _vat));
        arr.Add(new cArrayList("@balance", 0));
        arr.Add(new cArrayList("@crdb", cbtype.SelectedValue));
        arr.Add(new cArrayList("@filedoc", string.Empty));
        arr.Add(new cArrayList("@authforapprove", cbapproval.SelectedValue));
        //arr.Add(new cArrayList("@cndn_dt", System.DateTime.ParseExact(dtcndn.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        string _cndnnumber = string.Empty;
        
        bll2.vInsertMstCndnCustomer(arr, ref _cndnnumber);
        lbsysno.Text = _cndnnumber;
        if (upl.HasFile)
        {
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;
            fileExtension = ext;
            string _filename = _cndnnumber + ext;
            byte[] fs = upl.FileBytes;
            if (fs.Length <= 104857600)
            {
                if ((upl.FileName != string.Empty) || (upl.FileName != null))
                {
                    //arr.Clear();
                    //upl.SaveAs(bll.sGetControlParameter("image_path") + "/CNDNCust/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + hdfFileName.Value);
                    //hdfFileName.Value = Convert.ToString(Request.Cookies["sp"].Value) + "_" + hdcust.Value + "_" + hdfFileName.Value;
                    //if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNCust/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File already exist" + upl.FileName + ", please select another file','please select another file','warning');", true);
                    //    return;
                    //}

                    //else
                    //{
                    //if (File.Exists(bll.sGetControlParameter("image_path") + "/CNDNCust/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName))
                    //{
                    //    File.Delete(bll.sGetControlParameter("image_path") + "/CNDNCust/" + Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName);
                    //}

                    upl.SaveAs(bll.sGetControlParameter("image_path") + "/CNDNCust/" + _filename);
                    hdfFileName.Value = Convert.ToString(Request.Cookies["sp"].Value) + "_" + upl.FileName;
                    hdfFileNameExtension.Value = ext;
                    //}
                }
            }
        }
        string _message = "#New Debit Note Customer " + hdcust.Value + " , with number :" + _cndnnumber
            + " Please do approve or reject !";
        cd.v_hiddencontrol(btSave);
        cd.v_showcontrol(btprint);
        cd.v_disablecontrol(dtpost);
        cd.v_disablecontrol(cbtax);
        cd.v_disablecontrol(cbapproval);
        cd.v_disablecontrol(cbtype);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(txtAmount);
        cd.v_disablecontrol(txtManual);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('New DN Customer has been created!','Please print " + _cndnnumber + " for go to approval','success');", true);
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        hdcust.Value = "";
        txtAmount.Text = "";
        txtManual.Text = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@reasn_typ", "cndnc"));
        bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
        hdfFileName.Value = "";
        txcust.Text = "";
        //lblVat.Text = "0";
        lbsysno.Text = hdfCNDNID.Value;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Save Successfully.','" + hdfCNDNID.Value + "','success');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opr", "openreport('fm_report2.aspx?src=cndnCustomer&cndn_no=" + hdfCNDNID.Value + "');", true);
    }

    protected void cbapproval_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfUser.Value = cbapproval.SelectedValue;
    }

    protected void btDNCustomer_Click(object sender, EventArgs e)
    {
        // Enh : 22 June 2019 : Customer Transfer Blocked - CIN
        string sCustomerTransferBlock = bll.vLookUp("select dbo.fn_customertransferpending('" + hdcust.Value.ToString() + "','" + Request.Cookies["sp"].Value.ToString() + "')");
        if (sCustomerTransferBlock != "ok")
        {
            hdcust.Value = ""; txcust.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('This customer block for sales caused there is pending in customer transfer ','" + sCustomerTransferBlock + "','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cust_cd", Convert.ToString(hdcust.Value)));
        arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
        bll.vBindingGridToSp(ref grd, "sp_tmst_cndncustomer_getByCust", arr);
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lblDN_no = (Label)grd.Rows[e.RowIndex].FindControl("lblDN_no");
        HiddenField hdfcndncust_sta_id = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfcndncust_sta_id");
        if (hdfcndncust_sta_id.Value != "")
        {
            if (hdfcndncust_sta_id.Value != "N")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('It is already use. Select another DN Customer','Wrong DN delete','warning');", true);
                return;
            }
            else
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@cndncust_sta_id", Convert.ToString(hdfcndncust_sta_id.Value)));
                arr.Add(new cArrayList("@cndn_no", Convert.ToString(lblDN_no.Text)));
                arr.Add(new cArrayList("@salespointcd", Convert.ToString(Request.Cookies["sp"].Value)));
                bll.vDeleteDNCutomerAmt(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data deleted Successfully.','Data deleted Successfully.','success');", true);
            }
        }
    }

    protected void btnViewCNDNC_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_acccndnCustomerBranchReport.aspx");
    }

    protected void ddlCNDNType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCNDNType.SelectedValue == "VAT")
        //{
        //    lblVat.Text = String.Format("{0:0.00}", (Convert.ToDecimal(txtAmount.Text) - ((Convert.ToDecimal(txtAmount.Text) * 100) / 105)));
        //    showTax.Attributes.Remove("style");
        //}
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListTax(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@tax_cd", prefixText));
        arr.Add(new cArrayList("@tax_type", "E"));
        bll.vGetMstTax(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["tax_cd"].ToString() + "-" + rs["tax_desc"], rs["tax_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }

    protected void bttax_Click(object sender, EventArgs e)
    {
        if (txtAmount.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Amount can not empty.','Please input amount.','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cndn_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@tax_cd", hdtaxall.Value));
        arr.Add(new cArrayList("@amount", txtAmount.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertCndnCustomer_tax(arr);
        arr.Clear();
        arr.Add(new cArrayList("@cndn_no", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //  bll.vBindingGridToSp(ref grdtax, "sp_tcndncustomer_tax_get", arr);
        hdtaxall.Value = "";
        //  txtaxall.Text = "";

        double dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcndncustomer_tax where cndn_no='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
        double dAmt = Double.Parse(txtAmount.Text);
        //   lblVat.Text = Convert.ToString(dVat);
    }

    protected void grdtax_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        double dVat = 0, dValue = 0, dAmt = Double.Parse(txtAmount.Text.ToString());
        //if (grdtax.Rows.Count > 0)
        //{
        //    dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcndncustomer_tax where cndn_no='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
        //    dValue = dAmt - dVat;
        //}

        //  lblVat.Text = Math.Round(dVat, 5, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);

        //   HiddenField tax_cd = (HiddenField)grdtax.Rows[e.RowIndex].FindControl("tax_cd");
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@cndn_no", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //arr.Add(new cArrayList("@tax_cd", tax_cd.Value));
        //bll.vDeleteCndnCustomer_tax(arr);
        //arr.Clear();
        //arr.Add(new cArrayList("@cndn_no", Request.Cookies["usr_id"].Value.ToString()));
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //    bll.vBindingGridToSp(ref grdtax, "sp_tcndncustomer_tax_get", arr);
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        string _sql = "update tmst_cndncustomer set cndncust_sta_id='W' where cndn_no='"+lbsysno.Text+"'";
        bll.vExecuteSQL(_sql);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), 
            "openreport('fm_report2.aspx?src=cndncustomer&n="+lbsysno.Text+"');", true); ;
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page,Page.GetType(),Guid.NewGuid().ToString(),
            "popupwindow('lookupcndncustomer.aspx');",true); 
    }

    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        cd.v_disablecontrol(cbtype);
    }

    protected void cbtax_SelectedIndexChanged(object sender, EventArgs e)
    {
        cd.v_disablecontrol(cbtax);
    }
}