using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_closing : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            lbexhibitdate.Text = Request.Cookies["exh_dt"].Value.ToString();
        }
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@exhibit_cd", Request.Cookies["exh_cd"].Value.ToString()));
        bll.vBatchExhibitInitBalance(arr);
        bll.vBatchExhAdvancedDate(arr);
        string sDateExh = Convert.ToDateTime( bll.vLookUp("select dbo.fn_getexhdate('"+Request.Cookies["exh_cd"].Value.ToString()+"')")).ToString("d/M/yyyy");
        Response.Cookies["exh_dt"].Value = sDateExh; lbexhibitdate.Text = sDateExh;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing successfully','Advanced Date : "+sDateExh+"','success');", true);
    }
}