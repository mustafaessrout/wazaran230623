using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cmo : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["sp"].Value.ToString() == "0")
            {
                string waz_dt;
                waz_dt = DateTime.Now.ToString("d/M/yyyy");
                DateTime dtwaz_dt = DateTime.ParseExact(waz_dt, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                waz_dt = dtwaz_dt.ToString("yyyyMM");
                bll.vBindingComboToSp(ref cbMonthCD, "sp_tblTRYearMonth_get", "period", "ymtName");
                cbMonthCD.SelectedValue = waz_dt;
                cbMonthCD_SelectedIndexChanged(sender, e);
                cbWeek.Attributes.Add("style", "display:none");
                lbcmono.Text = "NEW";
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('You dont have access this pages!','Please contact Wazaran Admin for open access','error');", true);
                Response.Redirect("default.aspx", false);
            }
        }
    }

    protected void btsearch_Click(object sender, EventArgs e)
    {

    }

    protected void cbMonthCD_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbType_SelectedIndexChanged(sender, e);
    }

    protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        if (cbType.SelectedValue.ToString() == "M")
        {
            cbWeek.Attributes.Add("style", "display:none");
            arr.Clear();
            arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_cmo_product_get", arr);
        }
        else
        {
            cbWeek.Attributes.Remove("style");
            arr.Clear();
            arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbWeek, "sp_tblTRYearMonthWeek_get", "week", "week",arr);
            arr.Clear();
            arr.Add(new cArrayList("@MonthCD", cbMonthCD.SelectedValue.ToString()));
            arr.Add(new cArrayList("@week", cbWeek.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_cmo_product_get", arr);
        }
    }

    protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grd_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void grd_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}