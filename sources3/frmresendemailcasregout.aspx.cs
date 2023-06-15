using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmresendemailcasregout : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btemail_Click(object sender, EventArgs e)
    {
        //send email by yanto 2015-11-14
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashregout_cd", txcashregout_cd.Text));
        bll.vGettcashregout_dtl(ref rs, arr);
        while (rs.Read())
        {
            string semp_cd = rs["emp_cd"].ToString(), semp_cdapp = rs["app_cd"].ToString(), sitemco_nm = rs["itemco_nm"].ToString(), ssalespointcd = bll.vLookUp("select salespointcd from tcashregout where casregout_cd='" + txcashregout_cd.Text + "'"), semp_nm;
            string stoken = bll.vLookUp("select * from temail_sent where doc_no='" + txcashregout_cd.Text + "'");
            string samt = rs["amt"].ToString();
                string sSubject = ""; string sMessage = "";
                semp_nm = bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + semp_cd + "'");
                string sfile_attachment = rs["appfile"].ToString(), sto = bll.vLookUp("select email from tuser_profile where emp_cd='" + semp_cdapp + "'");
                string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
                sSubject = "New Cash Request Branch has been created ";
                sMessage = "Request No. " + txcashregout_cd.Text + " created by " + semp_nm + ", with the item " + sitemco_nm + ", Amount :  " + samt + " SAR" +
                "<p> Please Click this  for approved : <a href='" + slink_ho + "/landingpage.aspx?trnname=casregout&salespointcd=" + ssalespointcd + "&appcode=" + stoken + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_ho + "/landingpage.aspx?trnname=casregout&salespointcd=" + ssalespointcd + "&appcode=" + stoken + "&sta=R'>Reject</a></p>" +
                " \n\r\n\r\n\r\n Wazaran Admin";
                bll.vSendMail(sto, sSubject, sMessage, sfile_attachment);
        }
        rs.Close();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Send Email succesfully ..','No. " + txcashregout_cd.Text + "','info');", true);
        
    }
}