using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO;
public partial class fm_claimsumm : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbProd_cdFr, "sp_tmst_product_get4", "ID", "ProdName");
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "1")
        {
            if (dtstart.Text == "" || dtend.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('DATE Error','Please Insert  Date','warning');", true);
                return;

            }
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("p_user", Request.Cookies["usr_id"].Value.ToString()));
        if (cbreport.SelectedValue.ToString() != "2")
        {
            if (cbProd_cdFr.SelectedIndex.ToString() == "0")
            {
                arr.Add(new cArrayList("@prod_cd", null));
            }
            else
            {
                arr.Add(new cArrayList("@prod_cd", cbProd_cdFr.SelectedValue.ToString()));
            }
        }
        if (cbreport.SelectedValue.ToString() == "0")
        {
            cbProd_cdFr.Enabled = true;
            arr.Add(new cArrayList("@startdate", null));
            arr.Add(new cArrayList("@enddate", null));
            Session["lParamclaimsum"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=claimsum');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            cbProd_cdFr.Enabled = true;
            DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime dtto = Convert.ToDateTime(DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            arr.Add(new cArrayList("@startdate", dtfrom));
            arr.Add(new cArrayList("@enddate", dtto));
            Session["lParamclaimsum"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=claimsum');", true);
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            cbProd_cdFr.Enabled = false;
            DateTime dtfrom = Convert.ToDateTime(DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            DateTime dtto = Convert.ToDateTime(DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            arr.Add(new cArrayList("@startdate", dtfrom));
            arr.Add(new cArrayList("@enddate", dtto));
            Session["lParamclaimsum"] = arr;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=salesvsclaimdetail');", true);
        }

    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreport.SelectedValue.ToString() == "0")
        {
            dtstart.Enabled = false;
            dtend.Enabled = false;
            cbProd_cdFr.Enabled = true;
        }
        else if (cbreport.SelectedValue.ToString() == "1")
        {
            dtstart.Enabled = true;
            dtend.Enabled = true;
            cbProd_cdFr.Enabled = true;
        }
        else if (cbreport.SelectedValue.ToString() == "2")
        {
            dtstart.Enabled = true;
            dtend.Enabled = true;
            cbProd_cdFr.Enabled = false;
        }
    }
}