using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_fm_destpostpone : System.Web.UI.Page
{
    cbll bll = new cbll();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingGridToSp(ref grd, "sp_get_tblTrnDestroy");
        }
    }

    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        Label trnstkno = (Label)grd.Rows[e.NewSelectedIndex].FindControl("trnstkno");
        Label postpone_day = (Label)grd.Rows[e.NewSelectedIndex].FindControl("postpone_day");
        if (Int32.Parse(postpone_day.Text) < 5)
        {
            bll.vLookUp("update tblTrnStock set trn_trnstkdate=DATEADD(DAY, 1, trn_trnstkdate) where trnstkno='" + trnstkno.Text + "'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Postpone success','" + bll.vLookUp("SELECT CONVERT(date, (select trn_trnstkdate from tblTrnStock where trnstkno = '"+ trnstkno.Text + "'))") + "' ,'success');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Maximal Postpone 5 days','' ,'warning');", true);
            return;
        }
        bll.vBindingGridToSp(ref grd, "sp_get_tblTrnDestroy");
    }
}