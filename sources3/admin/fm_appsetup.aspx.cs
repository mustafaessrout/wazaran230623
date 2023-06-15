using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_appsetup : System.Web.UI.Page
{
    cbll bll = new cbll();

    private string _emp_cd;
    public string emp_cd
    {
    set{_emp_cd=value;} get{return(_emp_cd);}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbtype, "doc_typ");
          
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }
    protected void cbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_typ", cbtype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdapp, "sp_tapprovalpattern_get2",arr);
        txemp.Text = "";
        txmin.Text = "";
        txmax.Text = "";
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
       
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@doc_typ", cbtype.SelectedValue.ToString()));
        if (grdapp.SelectedRow == null)
        { arr.Add(new cArrayList("@emp_cd", hdemp2.Value.ToString())); }
        else
        {
            HiddenField hdemp = (HiddenField)grdapp.SelectedRow.FindControl("hdemp");
            arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        }
        arr.Add(new cArrayList("@min_amt", txmin.Text));
        arr.Add(new cArrayList("@max_amt", txmax.Text));
        arr.Add(new cArrayList("@level_no", 1));
        bll.vInsertApprovalPattern(arr); arr.Clear();
        arr.Add(new cArrayList("@doc_typ", cbtype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdapp, "sp_tapprovalpattern_get2", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Approval has been successfully saved','"+cbtype.SelectedItem.Text+"','success');", true);
        txemp.Text = "";
        txmin.Text = "";
        txmax.Text = "";
        hdemp2.Value = "";
       
    }
    protected void grdapp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        Label lbdoctype = (Label)grdapp.Rows[e.RowIndex].FindControl("lbdoctype");
        HiddenField hdemp = (HiddenField)grdapp.Rows[e.RowIndex].FindControl("hdemp");
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        arr.Add(new cArrayList("@doc_typ", lbdoctype.Text));
        bll.vDelApprovalPattern(arr); arr.Clear();
        arr.Add(new cArrayList("@doc_typ", cbtype.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grdapp, "sp_tapprovalpattern_get2", arr);
        //arr.Add(new cArrayList("@emp_cd", ))
    }
    protected void grdapp_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label lbdoctype =(Label) grdapp.Rows[e.NewSelectedIndex].FindControl("lbdoctype");
        HiddenField hdemp = (HiddenField)grdapp.Rows[e.NewSelectedIndex].FindControl("hdemp");
        cbtype.SelectedValue = lbdoctype.Text;
        txemp.Text = bll.vLookUp("select emp_cd +' | ' + emp_nm from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
        txmin.Text = bll.vLookUp("select min_amt from tapprovalpattern where emp_cd='" + hdemp.Value.ToString() + "' and doc_typ='" +lbdoctype.Text +"'");
        txmax.Text = bll.vLookUp("select max_amt from tapprovalpattern where emp_cd='" + hdemp.Value.ToString() + "' and doc_typ='" + lbdoctype.Text + "'");
     
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_appsetup.aspx");
    }
}