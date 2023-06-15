using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_docreject : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            bll.vBindingFieldValueToCombo(ref cbdoctype, "doc_typ");
            arr.Add(new cArrayList("@reasn_cd", "2000"));
            bll.vBindingComboToSp(ref cbreason, "sp_tmst_reason_get", "reasn_cd","reasn_nm", arr);
            dtdoc.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        switch (cbdoctype.SelectedValue.ToString())
        {
            case "RTV":

                break;
            case "":

                break;
        }
        
        string sCheck = bll.vLookUp("select dbo.[fn_checkmanualno]('re','"+txdocno.Text+"')");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_no", txdocno.Text));
        arr.Add(new cArrayList("@doc_typ", cbdoctype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@reasn_cd", cbreason.SelectedValue.ToString()));
        arr.Add(new cArrayList("@reject_dt", System.DateTime.ParseExact( dtdoc.Text , "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
        bll.vInsertDocReject(arr);
        txdocno.CssClass = "form-control ro";
        txdocno.Enabled = false;
        cbdoctype.CssClass = "form-control ro";
        cbdoctype.Enabled = false;
        cbreason.CssClass = "form-control ro";
        cbreason.Enabled = false;
        dtdoc.CssClass = "form-control ro";
        dtdoc.Enabled = false;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('New Document reject has been created !','Doc Reject completed','success');", true);
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_docreject.aspx");
    }
}