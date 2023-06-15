using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_editdiscount : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            initgrid(Request.QueryString["dc"]);
            dtend.CssClass = "ro";
        }
    }

    void initgrid(string sDisc)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", sDisc));
        bll.vGetMstDiscount(arr, ref rs);
        while (rs.Read())
        {
            lbdisc.Text = sDisc;
            lbprop.Text = rs["proposal_no"].ToString();  
            lbremark.Text = rs["remark"].ToString();
            dtstart.Text = Convert.ToDateTime( rs["start_dt"]).ToString("d/M/yyyy");
            lbdelidate.Text = Convert.ToDateTime(rs["delivery_dt"]).ToString("d/M/yyyy");
            dtend.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            dtenddisc.Text = Convert.ToDateTime(rs["end_dt"]).ToString("d/M/yyyy");
            txqtymax.Text = rs["qty_max"].ToString();
            lbdiscmec.Text = rs["discount_mec"].ToString();

        }
        rs.Close();

    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        DateTime dt = System.DateTime.ParseExact( Request.Cookies["waz_dt"].Value, "d/M/yyyy",System.Globalization.CultureInfo.InvariantCulture);
        if (dt > System.DateTime.ParseExact(dtenddisc.Text,"d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture))
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Date changed can not less than system date','Check your edit date','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@disc_cd", Request.QueryString["dc"]));
        arr.Add(new cArrayList("@end_dt", System.DateTime.ParseExact(dtenddisc.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vUpdateTmstDiscountByEndDate(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('Discount has edit successfully','End Date','success');", true);
    }
}