using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_acccndnmailSMS : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        creport rep = new creport();


        arr.Add(new cArrayList("@doc_typ", "CNDNAdj"));
        arr.Add(new cArrayList("@level_no", "1"));
        bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
        arr.Clear();

        DataTable dtCNDN = new DataTable();

      
        dtCNDN = cdl.GetValueFromSP("sp_tacc_cndn", arr);

        #region
        if (dtCNDN.Rows.Count > 0)
        {
            foreach (DataRow dr in dtCNDN.Rows)
            {
                string cndn_cd = dr["cndn_cd"].ToString();
                string refho_no = dr["refho_no"].ToString();
                int nrnd = 0;
                Random rnd = new Random();
                nrnd = rnd.Next(1000, 9999);
                string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
                string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
                List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());
                string cndnAmount = bll.vLookUp("select sum(totamtCN + totamtDN + vatamt) from tacc_cndn  where cndn_cd = '" + cndn_cd + "'");
                string sMsg = "#CNDN Adjustment-" + ssalespoint + "-" + refho_no + ", CNDN Amount '" + cndnAmount + "' , do you want to approved : (Y/N)" + stoken;
                arr.Clear();
                arr.Add(new cArrayList("@token", stoken));
                arr.Add(new cArrayList("@doc_no", cndn_cd));
                arr.Add(new cArrayList("@doc_typ", "CNDNAdjustment"));
                arr.Add(new cArrayList("@to", lapproval[0]));
                arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                bll.vInsertSmsOutbox(arr);

                // Sending Email 
                string sSubject = ""; string sMessage = "";
                string sfile_attachment = string.Empty;
                string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + "" + "')");
                //string cashout_typ = ddlCNDN.SelectedValue.ToString();

                sSubject = "#CNDN Adjustment Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";

                string filePath = bll.sGetControlParameter("image_path") + @"CNDNAdj\";
                string fileName = cndn_cd + ".pdf";
                string fileExcelName = cndn_cd + ".xls";
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@RefNo", cndn_cd));
                arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));

                rep.vShowReportToPDF("rp_acccndnadjRefNo.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileName);
                //rep.vShowReportToPDF("rp_acccndnAdjApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileName);
                //rep.vShowReportToEXCEL("rp_acccndnAdjApp.rpt", arr, bll.sGetControlParameter("image_path") + @"CNDNAdj\" + fileExcelName);

                bll.vLookUp("update tacc_cndn set uploadByUser = '"+ fileName + "' where cndn_cd = '"+ cndn_cd +"'");

                sMessage = "<table><tr><td>#CNDN Adjustment Request Approval</td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr>" +
                    "<tr><td>CNDN Code</td><td>:</td><td>" + cndn_cd + "</td></tr>" +
                    "<tr><td>Ref HO</td><td>:</td><td>" + refho_no + "</td></tr>" +
                    "<tr><td>CNDN Amount</td><td>:</td><td>" + cndnAmount + "</td></tr>" +
                "<tr><td>Please Click this  for View Document</td><td>:</td><td> <a href='" + slink_branch
                    + "/images/CNDNAdj/" + refho_no + "_doc" + ".pdf" + "'>View Document</a></td></tr>" +
                    "</table>" +
                "<p> Please Click this  for approved : <a href='" + slink_branch
                + "/landingpage2.aspx?src=acccndnAdjApp&salespointcd=" + Request.Cookies["sp"].Value.ToString()
                + "&ids=" + cndn_cd + "&sta=A&updatMethod=email&appBy=" + cbapproval.SelectedValue + "'>Approve</a>, or for rejected please click <a href='"
                + slink_branch + "/landingpage2.aspx?src=acccndnAdjApp&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids="
                + cndn_cd + "&sta=R&updatMethod=email&appBy=" + cbapproval.SelectedValue + "'>Reject</a></p>" +
                " \n\r\n\r\n\r\n Wazaran Admin";

                bll.vSendMail(lapproval[1], sSubject, sMessage, @"CNDNAdj\" + fileName);
                arr.Clear();
                arr.Add(new cArrayList("@trxcd", "acccndnAdjApp"));
                arr.Add(new cArrayList("@token", nrnd.ToString()));
                arr.Add(new cArrayList("@doc_no", cndn_cd));
                bll.vInsertEmailSent(arr);
                arr.Clear();
                sfile_attachment = @"CNDNAdj\" + fileName;
                arr.Add(new cArrayList("@token", nrnd.ToString()));
                arr.Add(new cArrayList("@doc_typ", "acccndnAdjAppPDF"));
                arr.Add(new cArrayList("@to", lapproval[1]));
                arr.Add(new cArrayList("@doc_no", cndn_cd));
                arr.Add(new cArrayList("@emailsubject", sSubject));
                arr.Add(new cArrayList("@msg", sMessage));
                arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                bll.vInsertEmailOutbox(arr);
                arr.Clear();

                //

                

            }
        }
#endregion
    }
}