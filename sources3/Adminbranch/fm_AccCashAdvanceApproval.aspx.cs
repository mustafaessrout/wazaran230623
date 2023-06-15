using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using AjaxControlToolkit;
using System.IO;
public partial class fm_AccCashAdvanceApproval : System.Web.UI.Page
{
    cbll bll = new cbll();
    creport rep = new creport();
    Boolean hasApprovalRole = false;
    string userid = null;
    //string userid2 = null;
    string doc_no = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string userid = Request.Cookies["usr_id"].Value.ToString();
            //string userid = "2833";

            //userid2 = bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'");
            //if (bll.vLookUp("select * from tmst_employee where (level_cd between 5 and 8 or level_cd=10)and dept_cd='fi'and salespointcd='0' and emp_cd='" + userid + "'") != "")
            if (bll.vLookUp("select e.emp_cd from tapprovalpattern a left join tmst_employee e on a.emp_cd=e.emp_cd where a.doc_typ='manualjournal'and a.emp_cd='" + userid + "'") != "")
            {
                hasApprovalRole = true;
            }

            doc_no = Request.QueryString["doc_no"];

            if (doc_no != "" && doc_no != null)
            {
                List<cArrayList> arr = new List<cArrayList>();
                if (hasApprovalRole)
                {
                    //btapprove.Visible = true;
                    //btreturn.Visible = true;

                    ////temporary for testing, shoould be uncomment false
                    //// ||
                    //// VV
                    ////btNewDetailRow.Visible = false;

                    //dttrnstkDate.Enabled = false;
                    //txtrnstkRemark.Enabled = false;


                }
                else
                {
                    btapprove.Visible = true;
                    btreject.Visible = true;
                }
                //lblDocNo.CssClass = "text-align:center";
                lblDocNo.Text = "Document No : " + doc_no;
            }

        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
    }
    protected void btapprove_Click(object sender, EventArgs e)
    {
        string salespoint = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'salespoint'");

        string docno = lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 2);

        if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 2) == "CO")
        {
            Response.Redirect("landingpage3.aspx?src=cashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=A" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        else if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 3) == "CAD")
        {
            Response.Redirect("landingpage3.aspx?src=PettycashCashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=A" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        else if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 3) == "CLP")
        {
            Response.Redirect("landingpage3.aspx?src=ClaimCashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=A" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        //Response.Redirect("fm_AccCashAdvanceApproval_List.aspx");

    }
    protected void btcancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_AccCashAdvanceApproval_List.aspx");
    }
    protected void btreject_Click(object sender, EventArgs e)
    {
        string salespoint = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'salespoint'");

        string docno = lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 2);

        if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 2) == "CO")
        {
            Response.Redirect("landingpage3.aspx?src=cashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=R" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        else if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 3) == "CAD")
        {
            Response.Redirect("landingpage3.aspx?src=PettycashCashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=R" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        else if (lblDocNo.Text.ToString().Replace("Document No : ", "").Substring(0, 3) == "CLP")
        {
            Response.Redirect("landingpage3.aspx?src=ClaimCashout&salespointcd=" + salespoint + "&ids=" + lblDocNo.Text.ToString().Replace("Document No : ", "") + "&sta=R" + "&appBy=" + Request.Cookies["usr_id"].Value.ToString());
        }
        //Response.Redirect("fm_AccCashAdvanceApproval_List.aspx");
    }

}

