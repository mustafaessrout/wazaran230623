using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class master_fm_copyproposal : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txproposal.CssClass = "form-control";
            hdprop.Value = "";
            dtstart.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtend.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtdelivery.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(-7));
            dtclaim_CalendarExtender.StartDate = DateTime.Now.AddDays(10);
            dtclaim.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now.AddDays(10));
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lprop = new List<string>();
        string sProp = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@prop_no", prefixText));
        bll.vSearchProposalByRemark(arr, ref rs);
        while (rs.Read())
        {
            sProp = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["prop_no"].ToString() + "-" + rs["bgremark"].ToString(), rs["prop_no"].ToString());
            lprop.Add(sProp);
        }
        rs.Close();
        return (lprop.ToArray());
    }
  
    protected void btexecute_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        System.Data.SqlClient.SqlDataReader rs = null;
        DateTime dt;
        string sPropNo = string.Empty;
        string propCode = string.Empty;
        int propDigit = 0;
        int propYear = 0;

        if (dtstart.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select start date!','Start Date','warning');", true);
            return;
        }

        if (dtdelivery.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select delivery date!','Delivery Date','warning');", true);
            return;
        }
        if (dtend.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select end date!','End Date','warning');", true);
            return;
        }
        if (dtclaim.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select claim date!','Claim Date','warning');", true);
            return;
        }
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please write remark for new proposal!','Remark','warning');", true);
            return;
        }

        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@year", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).Year));
        bll.vGetProposalNewNo(arr, ref rs);
        while (rs.Read())
        {
            propCode = rs["group_code"].ToString();
            sPropNo = rs["proposal_code"].ToString();
            propDigit = Convert.ToInt16(rs["prop_digit"].ToString());
            propYear = Convert.ToInt16(rs["year"].ToString());
        }
        arr.Clear();
        arr.Add(new cArrayList("@prop_no", hdprop.Value.ToString()));
        arr.Add(new cArrayList("@prop_no_new", sPropNo.ToString()));
        arr.Add(new cArrayList("@prop_dt", System.DateTime.Today));
        arr.Add(new cArrayList("@start_dt", System.DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@delivery_dt", System.DateTime.ParseExact(dtdelivery.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@claim_dt", System.DateTime.ParseExact(dtclaim.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@remark", txremark.Text));
        arr.Add(new cArrayList("@prop_code", propCode));
        arr.Add(new cArrayList("@prop_digit", propDigit));
        arr.Add(new cArrayList("@prop_year", propYear));

        bll.vBatchCopyProposal(arr);
        lbnewproposal.Text = sPropNo;

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Proposal created!','" + sPropNo + "','success');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_copyproposal.aspx");
    }
}