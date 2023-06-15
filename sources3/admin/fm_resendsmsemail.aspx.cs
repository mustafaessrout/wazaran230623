using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_resendsmsemail : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //bll.vBindingFieldValueToCombo(ref cbcusgrcd, "cusgrcd");
            //bll.vBindingGridToSp(ref grd_sms, "sp_tsms_outbox_get");
        }
    }
    protected void rdsend_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdsend.SelectedValue.ToString() == "S")
        {
            this.grd_email.Visible = false;
            this.grd_sms.Visible = true;
            bll.vBindingGridToSp(ref grd_sms, "sp_tsms_outbox_get");
            
        }
        else if (rdsend.SelectedValue.ToString() == "E")
        {
            this.grd_sms.Visible = false;
            this.grd_email.Visible = true;
            bll.vBindingGridToSp(ref grd_email, "sp_temail_outbox_get");
        }
    }
    protected void grdsms_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label doc_no = (Label)grd_sms.Rows[e.NewSelectedIndex].FindControl("lbsms");
        bll.vLookUp("update tsms_outbox set processed = 0 where doc_no = '" + (doc_no.Text) + "'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('SMS Already Resend','' ,'success');", true);
    }

    protected void grdemail_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label doc_no = (Label)grd_email.Rows[e.NewSelectedIndex].FindControl("lbemail");
        bll.vLookUp("update temail_outbox set processed = 0 where doc_no = '"+(doc_no.Text) +"'");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Email Already Resend','' ,'success');", true);
    }
}