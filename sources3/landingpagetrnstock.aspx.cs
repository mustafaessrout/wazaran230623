using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class landingpagetrnstock : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        if (!IsPostBack)
        {
            //by yanto 6-1-2019
            HttpCookie scookusersql = new HttpCookie("usersql");
            scookusersql = Request.Cookies["usersql"];
            if (scookusersql == null)
            {
                Response.Cookies["usersql"].Value = "sa";
            }
            //-------------------------
            string sAppCode = Request.QueryString["appcode"];
            string sStatus = Request.QueryString["sta"];
            string sToken = string.Empty;
            string sDocNo = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@token", sAppCode));
            bll.vGetEmailSent(arr, ref rs);
            while (rs.Read())
            {
                sToken = rs["token"].ToString();
                sDocNo = rs["doc_no"].ToString();
            } rs.Close();

            string strnstockStatus = bll.vLookUp("select sta_id from tbltrnstock where trnstkno='" + sDocNo + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'");
            if (strnstockStatus.Equals("C") || (strnstockStatus.Equals("R")))
            {
                lbstatus.Text = "Ooops there are some errors in approval, please contact Wazaran Admin !";
                return;
            
            }

            if (!sToken.Equals(string.Empty))
            {

                arr.Clear();
                arr.Add(new cArrayList("@trnstkNo", sDocNo));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@sta_id", sStatus));
                bll.vUpdatetbltrnstock_approve(arr);
                if (sStatus == "C")
                {
                    lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN APPROVED ! \r\n Now ";
                }
                else { lbstatus.Text = "Transaction No. " + sDocNo + " HAS BEEN REJECTED ! \r\n Now"; }
            }
            else
            { lbstatus.Text = "Ooops there are some errors in approval, please contact Wazaran Admin !"; }
        }
    }
}