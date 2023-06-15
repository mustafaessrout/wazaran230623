using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_devicesalesman_closing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_tablet_profile_get");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }

    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbtabdate = (Label)e.Row.FindControl("lbtabdate");
            DateTime _dt = DateTime.ParseExact(lbtabdate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime _waz = DateTime.Today; // DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            LinkButton btaction = (LinkButton)e.Row.FindControl("btaction");
            if (_dt!=_waz)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                cd.v_showcontrol(btaction);
            }
            else
            {
                e.Row.BackColor = System.Drawing.Color.White;
                cd.v_hiddencontrol(btaction);
            }

        }
    }

    protected void btaction_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        Label lbemployeecode = (Label)row.FindControl("lbemployeecode");
        string _whatsappno = bll.vLookUp("select top 1 whatsapp_no from tuser_profile where emp_cd='"+lbemployeecode.Text+"'");
        string _message = "#Votre appareil ne ferme pas encore, veuillez fermer dès que possible pour traiter votre transaction ! , meilleures salutations WAZARAN";
        bll.vSendWhatsapp("966503743024", _message);
        bll.vSendWhatsapp("2126680727708", _message);
        bll.vSendWhatsapp(_whatsappno, _message);   
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(),
            "sweetAlert('Alert has been sent successfully !','Success','success');", true);
    }
}