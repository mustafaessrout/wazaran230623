using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmcustomercategory_doc : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        { 
        bll.vBindingComboToSp(ref cbcustcate_cd, "sp_tmst_customercategory_get", "custcate_cd", "custcate_nm");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_typ", "CUST"));
        bll.vBindingComboToSp(ref cbdoc_cd, "sp_tmst_document_get", "doc_cd", "doc_nm", arr);
        cbcustcate_cd_SelectedIndexChanged(sender, e);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabFix", "tabFix();", true);
    }

    protected void cbcustcate_cd_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindinggrd();
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_cd",cbdoc_cd.SelectedValue.ToString()));
        arr.Add(new cArrayList("@custcate_cd", cbcustcate_cd.SelectedValue.ToString()));
        bll.vInserttcustomercategory_doc(arr);
        bindinggrd();
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label lbdoc_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbdoc_cd");
        Label lbcustcate_cd = (Label)grd.Rows[e.RowIndex].FindControl("lbcustcate_cd");

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_cd", lbdoc_cd.Text));
        arr.Add(new cArrayList("@custcate_cd", lbcustcate_cd.Text));
        bll.vDeletetcustomercategory_doc(arr); arr.Clear();
        bindinggrd();
    }
    private void bindinggrd()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@custcate_cd", cbcustcate_cd.SelectedValue));
       // arr.Add(new cArrayList("@doc_typ", "CUST"));
        bll.vBindingGridToSp(ref grd, "sp_tcustomercategory_doc_get", arr);
    }
    protected void btadd_Click1(object sender, EventArgs e)
    {

    }
}