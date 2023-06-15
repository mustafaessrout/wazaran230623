using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_bankhoconfirmandpostpone : System.Web.UI.Page
{
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

        
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            dt = cdl.GetValueFromSP("sp_tpaymentreceipt_postpone_get", arr);
            if (dt.Rows.Count > 0)
            {
                grd.DataSource = dt;
                grd.DataBind();
            }
            else
            {
                grd.DataSource = null;
                grd.DataBind();
            }
        }
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        string sRefno = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        string commandName = e.CommandName.ToString().Trim();
        GridViewRow row = grd.Rows[Convert.ToInt32(e.CommandArgument)];
        Int32 nRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
        string deposit_id = Convert.ToString(grd.DataKeys[nRowIndex].Value);
        //grd.Rows[nRowIndex].FindControl("lblinv_no");
        switch (commandName)
        {
            case "btnConfirmation":

                sRefno = bll.vLookUp("select ref_no from tbank_deposit where deposit_id='" + deposit_id + "'");
                arr.Clear();
                arr.Add(new cArrayList("@IDS", deposit_id));
                arr.Add(new cArrayList("@dep_sta_id", "H"));
                arr.Add(new cArrayList("@createdRecordBy", Request.Cookies["usr_id"].Value));
                bll.vUpdateBankDepositByStatusByHO(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment Cheque/Bank Transfer No." + sRefno + " has been CONFIRMED!','CONFIRMED','success');", true);
                break;
            case "btnrejected":
                sRefno = bll.vLookUp("select ref_no from tbank_deposit where deposit_id='" + deposit_id + "'");
                arr.Add(new cArrayList("@IDS", deposit_id));
                arr.Add(new cArrayList("@dep_sta_id", "R"));
                arr.Add(new cArrayList("@createdRecordBy", Request.Cookies["usr_id"].Value));
                bll.vUpdateBankDepositByStatusByHO(arr);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Payment Cheque/Bank Transfer No." + sRefno + " has been DECLINE!','DECLINE.','success');", true);
                break;
            case "btnPostpone": 
                // For Postpone we holding record in this table.
                break;
            default: break;
        }

        arr.Clear();
        DataTable dt = new DataTable();
        dt = cdl.GetValueFromSP("sp_tpaymentreceipt_postpone_get", arr);
        if (dt.Rows.Count > 0)
        {
            grd.DataSource = dt;
            grd.DataBind();
        }
        else
        {
            grd.DataSource = null;
            grd.DataBind();
        }


        //string s1 = "Command Name: " + e.CommandName;
        //string s2 = "Row Index: " + nRowIndex;
        //string s3 = "Product ID: " + nProductID;
    }
}