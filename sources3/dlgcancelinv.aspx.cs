using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dlgcancelinv : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sInv = Request.QueryString["iv"];
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@inv_no", sInv));
            //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vGetDOSalesInvoice2(arr, ref rs);
            while (rs.Read())
            {
                lbsysno.Text = sInv;
                lbsysno.ForeColor = System.Drawing.Color.DarkRed;
                lbcustcode.Text = rs["cust_cd"].ToString();
                lbcustname.Text = rs["cust_nm"].ToString();
                lbtotamt.Text = rs["balance"].ToString();
                lbsalesman.Text = rs["salesman_nm"].ToString();
            }
            rs.Close();
            arr.Clear();
            arr.Add(new cArrayList("reasn_typ", "CI"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get","reasn_cd","reasn_nm", arr);
        }
    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.close();", true);
    }
    protected void btexecute_Click(object sender, EventArgs e)
    {

        System.Data.SqlClient.SqlDataReader rs = null;
        Random rnd = new Random();
        int nToken = rnd.Next(1000, 9999);
        string sCust = string.Empty; string sAmt = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> lapp = new List<string>();
        lapp= bll.lGetApproval("branchspv", 1);
        //------------------------------------------------------------------------
        arr.Add(new cArrayList("@inv_no", Request.QueryString["iv"]));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
        bll.vUpdateDosalesInvoiceInfoByReason(arr);
        //------------------------------------------------------------------------
        //arr.Clear();
        //arr.Add(new cArrayList("@token", nToken.ToString()));
        //arr.Add(new cArrayList("@doc_typ", "cancelinv"));
        //arr.Add(new cArrayList("@receiver", lapp[0]));
        //arr.Add(new cArrayList("@doc_no", Request.QueryString["iv"]));
        //bll.vInsertSMSSent(arr);
        //-----------------------------------------------------------------------
        arr.Clear();
        arr.Add(new cArrayList("@inv_no", Request.QueryString["iv"]));
        bll.vCancelDosalesInvoice(arr);
        //-----------------------------------------------------------------------
        bll.vGetDOSalesInvoice2(arr, ref rs);
        while (rs.Read())
        { 
            sCust = rs["cust_sn"].ToString();
            sAmt = rs["totamt"].ToString();
        }
        rs.Close();
        string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nToken.ToString();

        string sSMS = "#Cancl Inv:" + Request.QueryString["iv"] + ",cust:" + sCust + ",TotAmt:" + sAmt + " ,Pls reply (Y/N)" + stoken.ToString();
        arr.Clear();
        arr.Add(new cArrayList("@token", stoken.ToString()));
        arr.Add(new cArrayList("@doc_typ", "cancelinv"));
        arr.Add(new cArrayList("@to", lapp[0]));
        arr.Add(new cArrayList("@doc_no", Request.QueryString["iv"]));
        arr.Add(new cArrayList("@msg", sSMS));
        bll.vInsertSmsOutbox(arr);
        //bll.vInsertSMSSent(arr);
        //cd.vSendSms(sSMS, lapp[0]);
        string sSubject = "Cancel Invoice Information";
        string sBody = "Dear Branch SPV " + Request.Cookies["sp"].Value.ToString() + ",\n\r";
        string sReason = cbreason.SelectedItem.Text;
        sBody += "<h3>Invoice Cancellation</h3>\n\r";
        sBody += "Sys No." + Request.QueryString["iv"] + "\n\r";
        sBody += "<table><tr style='background-color:silver'><th>Item</th><th>Name</th><th>Size</th><th>Branded</th><th>Qty</th><th>Unit Price</th><th>Amt</th></tr>";
        arr.Clear();
        arr.Add(new cArrayList("@inv_no", Request.QueryString["iv"]));
        bll.vGetDosalesInvoiceDtl(arr, ref rs);
        while (rs.Read())
        {
            sBody += "<tr><td>"+rs["item_cd"].ToString()+"</td><td>"+rs["item_nm"].ToString()+"</td><td>"+rs["size"].ToString()+"</td><td>"+rs["branded_nm"].ToString()+"</td><td>"+rs["qty"].ToString()+"</td><td>"+rs["unitprice"].ToString()+"</td><td>"+rs["amt"].ToString()+"</td></tr>";
        }
        rs.Close();
        sBody += "</table>"+"\n\r";
        sBody += "<h4>This invoice cancelled , because " + sReason + "</h4>";
        sBody += "<h3>Wazaran Admin</h3>";
        bll.vSendMail(lapp[1], sSubject, sBody);
     //   bll.vCancelDosalesInvoice(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cls", "window.opener.makealert();window.close();", true);
    }
}