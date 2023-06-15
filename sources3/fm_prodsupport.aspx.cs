using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_prodsupport : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
            lbsp.Text = bll.vLookUp("select salespointcd+'-'+salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            bll.vBindingFieldValueToCombo(ref cbsup_sta_id, "sup_sta_id");
            dtsup_dt.Text = DateTime.Today.ToString("d/M/yyyy");
            dtsup_time.Text = DateTime.Now.ToString("HH:mm:ss");
            bll.vBindingComboToSp(ref cbmenu, "sp_tmst_menu_part_get", "menuprt_cd", "menuprt_nm");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@parent_cd", cbmenu.SelectedValue));
            bll.vBindingComboToSp(ref cbmenu_sub, "sp_tmst_menu_part_get", "menuprt_cd", "menuprt_nm", arr);
            string smenu_cd = bll.vLookUp("select menu_cd from tmst_menu_part where menuprt_cd='" + cbmenu_sub.SelectedValue + "'");
            arr.Clear();
            arr.Add(new cArrayList("@menu_cd", smenu_cd));
            bll.vBindingComboToSp(ref cbmenu_transaction, "sp_tmst_menu_part_get", "menu_cd", "menu_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbsup_tran_type, "sup_tran_type");
            arr.Clear();
            arr.Add(new cArrayList("@doc_typ", "prod_support"));
            bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_prod_sup_get", "emp_cd", "emp_nm_full", arr);
            bll.vBindingFieldValueToCombo(ref cbsup_pic, "sup_pic");
            string screator=Request.Cookies["usr_id"].Value.ToString();
            txcreator.Text = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where emp_cd='" + screator + "'");
            bll.vBindingFieldValueToCombo(ref cberrby, "errby");
            bindinggrddoc();
            fieldsup_pic();
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }

    private void fieldsup_pic()
    {
        string spic = Request.Cookies["usr_id"].Value.ToString();
        if (spic.Length >= 4)
            spic = spic.Substring(0,4);
        string saccessuser = bll.vLookUp("select fld_valu from tfield_value where fld_nm='sup_pic' and fld_valu ='" + spic + "'");
        if (saccessuser == "" ||  saccessuser==  null )
        {
            cbsup_pic.CssClass = "form-control input-sm ro";
            cbsup_pic.Enabled = false;
            txremark.CssClass = "form-control input-sm ro";
            txremark.Enabled = false;
            cbsup_sta_id.CssClass = "form-control input-sm ro"; 
            cbsup_sta_id.Enabled = false;
            cberrby.CssClass = "form-control input-sm ro";
            cberrby.Enabled = false;
            }
        else
        {
            cbsup_pic.CssClass = "makeitreadwrite form-control input-sm";
            cbsup_pic.Enabled = true;
            txremark.CssClass = "makeitreadwrite form-control input-sm";
            txremark.Enabled = true;
            cbsup_sta_id.CssClass = "makeitreadwrite form-control input-sm";
            cbsup_sta_id.Enabled = true;
            cberrby.CssClass = "makeitreadwrite form-control input-sm";
            cberrby.Enabled = true;
        }
        
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_prodsupport.aspx");
    }
    protected void grddoc_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grddoc.EditIndex = e.NewEditIndex;
        bindinggrddoc();
    }
    protected void grddoc_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label lbsup_no = (Label)grddoc.Rows[e.RowIndex].FindControl("lbsup_no");
        Label hdnfilename = (Label)grddoc.Rows[e.RowIndex].FindControl("hdnfilename");
        FileUpload FileUpload1 = (FileUpload)grddoc.Rows[e.RowIndex].FindControl("FileUpload1");
        if (FileUpload1.HasFile)
        {
            String ssup_no;
            if (txsup_no.Text == "" || txsup_no.Text == "NEW") { ssup_no = Request.Cookies["usr_id"].Value.ToString(); } else { ssup_no = txsup_no.Text; }
            string imgpatch = bll.sGetControlParameter("image_path") + "/prodsupport/" + FileUpload1.FileName;
            string imgname = ssup_no+FileUpload1.FileName;
            
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@sup_no", lbsup_no.Text));
            arr.Add(new cArrayList("@salespointCD", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@filename", hdnfilename.Text));
            arr.Add(new cArrayList("@filenamenew", imgname));
            FileUpload1.SaveAs(imgpatch);
            bll.vUpdatetprodsupport_document(arr);

        }
        grddoc.EditIndex = -1;
        bindinggrddoc();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void grddoc_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddoc.EditIndex = -1;
        bindinggrddoc();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        string ssup_sta_id = bll.vLookUp("select sup_sta_id from tprod_support where sup_no ='" + txsup_no.Text + "'");
        if (ssup_sta_id == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data already complete, can not update ..','Tran No. " + txsup_no.Text + "','info');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        if (ssup_sta_id == "L")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data already cancel, can not update ..','Tran No. " + txsup_no.Text + "','info');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
            return;
        }
        string ssup_no = "";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        arr.Add(new cArrayList("@sup_dt", DateTime.ParseExact(dtsup_dt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@sup_time", DateTime.ParseExact(dtsup_time.Text, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@menu", cbmenu.SelectedValue));
        arr.Add(new cArrayList("@menu_sub", cbmenu_sub.SelectedValue));
        arr.Add(new cArrayList("@menu_transaction", cbmenu_transaction.SelectedValue));
        arr.Add(new cArrayList("@sup_tran_type", cbsup_tran_type.SelectedValue));
        arr.Add(new cArrayList("@tran_no", txtran_no.Text));
        arr.Add(new cArrayList("@description", txdescription.Text));
        arr.Add(new cArrayList("@creator", txcreator.Text));
        arr.Add(new cArrayList("@approval", cbapproval.SelectedValue));
        arr.Add(new cArrayList("@sup_pic", cbsup_pic.SelectedValue));
        arr.Add(new cArrayList("@sup_sta_id", cbsup_sta_id.SelectedValue));
        //arr.Add(new cArrayList("@sup_dt_finish", dtsup_dt_finish.Text));
        //arr.Add(new cArrayList("@sup_time_finish", dtsup_time_finish.Text));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@errby", cberrby.SelectedValue));
        if (txsup_no.Text == "NEW" || txsup_no.Text == "")
        {
            bll.vInserttprod_support(arr, ref ssup_no);
            txsup_no.Text = ssup_no;
            int nrnd;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
            List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue);
            string sMsg = "Prod Supp: " + lbsp.Text + " no." + ssup_no + " Creator:"+ txcreator.Text +
                 " " + txdescription.Text + " do you want to approve:(Y/N)" + stoken;
            //string sMsgpic = "Prod Support: " + lbsp.Text + ", no." + ssup_no + ", Creator:" + txcreator.Text +
            //    ", " + txdescription.Text + ", token no:" + nrnd.ToString();
            
            if (sMsg.Length >= 160)
                sMsg = sMsg.Substring(sMsg.Length-160);
                arr.Clear();
                arr.Add(new cArrayList("@token", stoken));
                arr.Add(new cArrayList("@doc_no", ssup_no));
                arr.Add(new cArrayList("@doc_typ", "Prodsupport"));
                arr.Add(new cArrayList("@to", lapproval[0]));
                arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                bll.vInsertSmsOutbox(arr);
                
            //SqlDataReader rs = null;
            //bll.vGettprod_support_sms_pic(ref rs);
            //while (rs.Read())
            //{
            //    if (sMsgpic.Length >= 160)
            //        sMsgpic = sMsgpic.Substring(sMsg.Length - 160);
            //    List<string> lapprovalpic = bll.lGetApproval(rs["emp_cd"].ToString());
            //    arr.Clear();
            //    arr.Add(new cArrayList("@token", nrnd.ToString()));
            //    arr.Add(new cArrayList("@doc_no", ssup_no));
            //    arr.Add(new cArrayList("@doc_typ", "Prodsupport"));
            //    arr.Add(new cArrayList("@to", lapprovalpic[0]));
            //    arr.Add(new cArrayList("@msg", sMsgpic.TrimEnd()));
            //    bll.vInsertSmsOutbox(arr);
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data Save successfully ..','Tran No. " + txsup_no.Text + "','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        }
        else
        {
            arr.Add(new cArrayList("@sup_no", txsup_no.Text));
            bll.vUpdatetprod_support(arr);
            //if (cbsup_sta_id.SelectedValue=="C")
            //{
            //    string ssuppic_nm = bll.vLookUp("SELECT emp_nm from tmst_employee where emp_cd='" + cbsup_pic.SelectedValue + "'");
            //    string sMsgcomplete = "#Prod Support: " + lbsp.Text + ", no." + ssup_no + ", Creator:" + txcreator.Text +
            //        ", " + txdescription.Text + ", finished support by " + ssuppic_nm;
            //    if (sMsgcomplete.Length >= 160)
            //        sMsgcomplete = sMsgcomplete.Substring(sMsgcomplete.Length - 160);
            //    List<string> lapprovalpic = bll.lGetApproval(txcreator.Text);
            //    arr.Clear();
            //    arr.Add(new cArrayList("@token", 0));
            //    arr.Add(new cArrayList("@doc_no", ssup_no));
            //    arr.Add(new cArrayList("@doc_typ", "Prodsupport"));
            //    arr.Add(new cArrayList("@to", lapprovalpic[0]));
            //    arr.Add(new cArrayList("@msg", sMsgcomplete.TrimEnd()));
            //    bll.vInsertSmsOutbox(arr);
            //}
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data updated successfully..','Tran No. " + txsup_no.Text + "','success');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
        }
        
    }
    protected void btDelete_Click(object sender, EventArgs e)
    {

    }
    protected void btprint_Click(object sender, EventArgs e)
    {

    }
    
    protected void cbmenu_sub_SelectedIndexChanged(object sender, EventArgs e)
    {
        string smenu_cd = bll.vLookUp("select menu_cd from tmst_menu_part where menuprt_cd='" + cbmenu_sub.SelectedValue + "'");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@menu_cd", smenu_cd));
        bll.vBindingComboToSp(ref cbmenu_transaction, "sp_tmst_menu_part_get", "menu_cd", "menu_nm", arr);
    }
    protected void cbmenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@parent_cd", cbmenu.SelectedValue));
        bll.vBindingComboToSp(ref cbmenu_sub, "sp_tmst_menu_part_get", "menuprt_cd", "menuprt_nm", arr);
        cbmenu_sub_SelectedIndexChanged(sender, e);
    }


    protected void btedit_Click(object sender, EventArgs e)
    {

    }
    protected void bttabsearch_Click(object sender, EventArgs e)
    {

    }
    protected void bttmp_Click(object sender, EventArgs e)
    {
        SqlDataReader rs = null;
        //string sstatus;
        txsup_no.Text = Convert.ToString(Session["loosup_no"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sup_no", txsup_no.Text));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vGettprod_support(arr, ref rs);
        while (rs.Read())
        {
            lbsp.Text = rs["salespointfull"].ToString();
            cbsup_sta_id.SelectedValue = rs["sup_sta_id"].ToString();
            string sdate = rs["sup_dt"].ToString();
            DateTime dtdate = Convert.ToDateTime(sdate);
            string strDate = dtdate.ToString("d/M/yyyy");
            //string stime = rs["sup_time"].ToString();
            dtsup_dt.Text = strDate;
            dtsup_time.Text = rs["sup_time"].ToString();
            cbmenu.SelectedValue = rs["menu"].ToString();
            cbmenu_SelectedIndexChanged(sender, e);
            cbmenu_sub.SelectedValue = rs["menu_sub"].ToString();
            cbmenu_sub_SelectedIndexChanged(sender, e);
            cbmenu_transaction.SelectedValue = rs["menu_transaction"].ToString();
            txtran_no.Text = rs["tran_no"].ToString();
            txdescription.Text = rs["description"].ToString();
            txcreator.Text = rs["emp_nm"].ToString();
            cbapproval.SelectedValue = rs["approval"].ToString();
            cbsup_pic.SelectedValue = rs["sup_pic"].ToString();
            string ssup_dt_finish = rs["sup_dt_finish"].ToString();
            string ssup_time_finis = rs["sup_time_finish"].ToString();
            if (ssup_dt_finish.Length >= 10)
                ssup_dt_finish = ssup_dt_finish.Substring(0, 10);
            if (ssup_time_finis.Length >= 8)
                ssup_time_finis = ssup_time_finis.Substring(0, 8);
            dtsup_dt_finish.Text = ssup_dt_finish;
            dtsup_time_finish.Text = ssup_time_finis;
            txremark.Text = rs["remark"].ToString();
            fieldsup_pic();
        } rs.Close();
        bindinggrddoc();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sethid", "dvshow.setAttribute('class','divhid');", true);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        if (upl.HasFile)
        {
            FileInfo fi = new FileInfo(upl.FileName);
            string ext = fi.Extension;
            byte[] fs = upl.FileBytes;
            if (fs.Length <= 500000)
            {
                if (ext == ".doc" || ext == ".docx" || ext == ".xls" || ext == ".xlsx" || ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                {
                    if ((upl.FileName != "") || (upl.FileName != null))
                    {
                        String ssup_no;
                        if (txsup_no.Text == "" || txsup_no.Text == "NEW") { ssup_no = Request.Cookies["usr_id"].Value.ToString(); } else { ssup_no = txsup_no.Text; }
                        upl.SaveAs(bll.sGetControlParameter("image_path") + "/prodsupport/" + "/" + ssup_no+upl.FileName);
                        List<cArrayList> arr = new List<cArrayList>();
                        arr.Add(new cArrayList("@sup_no", ssup_no));
                        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                        arr.Add(new cArrayList("@filename", ssup_no+upl.FileName));
                        bll.vInserttprodsupport_document(arr);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
                return;
            }
        }
        bindinggrddoc();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
    }
    private void bindinggrddoc()
    {
        String ssup_no;
        if (txsup_no.Text == "" || txsup_no.Text == "NEW") { ssup_no = Request.Cookies["usr_id"].Value.ToString(); } else { ssup_no = txsup_no.Text; }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sup_no", ssup_no));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vBindingGridToSp(ref grddoc, "sp_tprodsupport_document_get", arr);
    }
    
    protected void grddoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (cbsup_sta_id.SelectedValue == "C")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Transaction already complete can not be delete','error');", true);
            return;
        }
        String ssup_no;
        if (txsup_no.Text == "" || txsup_no.Text == "NEW") { ssup_no = Request.Cookies["usr_id"].Value.ToString(); } else { ssup_no = txsup_no.Text; }
        Label lbfilename = (Label)grddoc.Rows[e.RowIndex].FindControl("lbfilename");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@sup_no", ssup_no));
        arr.Add(new cArrayList("@filename", lbfilename.Text));
        bll.vDeletetprodsupport_document(arr);
        bindinggrddoc();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);
    }
}