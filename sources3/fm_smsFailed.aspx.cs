using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_smsFailed : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        // If SMS not coming then we are giving altenative option.
        if (!IsPostBack)
        {
            lblName.Text = Response.Cookies["fullname"].Value;
            lblPhone.Text = "";
            bll.vBindingComboToSp(ref ddlFirstQuestion, "sp_tmst_HintQuestion_get", "HintQuestionID", "HintQuestionEnglish");
            bll.vBindingComboToSp(ref ddlSecondQuestion, "sp_tmst_HintQuestion_get", "HintQuestionID", "HintQuestionEnglish");
            bll.vBindingComboToSp(ref ddlThirdQuestion, "sp_tmst_HintQuestion_get", "HintQuestionID", "HintQuestionEnglish");
        }
    }
   
    protected void ddlFirstQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFirstQuestion.SelectedValue == ddlSecondQuestion.SelectedValue || ddlFirstQuestion.SelectedValue == ddlThirdQuestion.SelectedValue)
        {
            ddlFirstQuestion.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your question already selected!','Same question','warning');", true);
            return;
        }
    }
    protected void ddlSecondQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSecondQuestion.SelectedValue == ddlFirstQuestion.SelectedValue || ddlSecondQuestion.SelectedValue == ddlThirdQuestion.SelectedValue)
        {
            ddlSecondQuestion.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your question already selected!','Same question','warning');", true);
            return;
        }
    }
    protected void ddlThirdQuestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlThirdQuestion.SelectedValue == ddlSecondQuestion.SelectedValue || ddlThirdQuestion.SelectedValue == ddlFirstQuestion.SelectedValue)
        {
            ddlThirdQuestion.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your question already selected!','Same question','warning');", true);
            return;
        }
    }
    protected void txtFirstAnswer_TextChanged(object sender, EventArgs e)
    {
        if (txtFirstAnswer.Text == txtSecondAnswer.Text || txtFirstAnswer.Text == txtThirdAnswer.Text)
        {
            txtFirstAnswer.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your answer already selected!','Same answer','warning');", true);
            return;
        }
    }
    protected void txtSecondAnswer_TextChanged(object sender, EventArgs e)
    {
        if (txtSecondAnswer.Text == txtFirstAnswer.Text || txtSecondAnswer.Text == txtThirdAnswer.Text)
        {
            txtSecondAnswer.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your answer already selected!','Same answer','warning');", true);
            return;
        }
    }
    protected void txtThirdAnswer_TextChanged(object sender, EventArgs e)
    {
        if (txtThirdAnswer.Text == txtSecondAnswer.Text || txtThirdAnswer.Text == txtFirstAnswer.Text)
        {
            txtThirdAnswer.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Your answer already selected!','Same answer','warning');", true);
            return;
        }
    }

}