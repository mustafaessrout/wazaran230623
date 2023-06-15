using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_returho_print : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            //cbrepby_SelectedIndexChanged(sender, e);            

            cbtypofrep_SelectedIndexChanged(sender, e);
            dtstart.Text = Request.Cookies["waz_dt"].Value.ToString();
            dtend.Text = Request.Cookies["waz_dt"].Value.ToString();
        }
    }
    protected void btprint_Click(object sender, EventArgs e)
    {

        try
        {
            DateTime dtst = DateTime.ParseExact(dtstart.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dten = DateTime.ParseExact(dtend.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            List<cArrayList> arr = new List<cArrayList>();
            if (cbtypofrep.SelectedValue.ToString() == "0")
            {
                arr.Add(new cArrayList("@returho_no", hdretho.Value.ToString()));
                Session["lParamretHO1"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=retHO1');", true);
            }
            else if (cbtypofrep.SelectedValue.ToString() == "1")
            {
                arr.Add(new cArrayList("@start_dt", dtst.Year + "-" + dtst.Month + "-" + dtst.Day));
                arr.Add(new cArrayList("@end_dt", dten.Year + "-" + dten.Month + "-" + dten.Day));
                Session["lParamretHO1"] = arr;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=retHO2');", true);
            }

            //arr.Add(new cArrayList("@startdate", dtstart.Text));
            //arr.Add(new cArrayList("@enddate", dtend.Text));


            //if (cbtypofrep.SelectedValue.ToString() == "SOA1")
            //{
            //    //arr.Add(new cArrayList("@type", "Detail"));
            //    if (cbrepby.SelectedValue.ToString() == "0")
            //    {
            //        Session["lParamsoabr"] = arr;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bybranch');", true);
            //    }
            //    else if (cbrepby.SelectedValue.ToString() == "1")
            //    {
            //        if (grdsl.Rows.Count.ToString() == "0")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please insert at least one salesman !!','warning');", true);
            //            return;
            //        }
            //        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //        Session["lParamsoabr"] = arr;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1byslsman&p=1');", true);
            //    }
            //    else if (cbrepby.SelectedValue.ToString() == "2")
            //    {
            //        if (grdcust.Rows.Count.ToString() == "0")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Error','Please insert at least one customer !!','warning');", true);
            //            return;
            //        }
            //        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //        Session["lParamsoabr"] = arr;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bycust&p=2');", true);
            //    }
            //    else if (cbrepby.SelectedValue.ToString() == "3")
            //    {
            //        arr.Add(new cArrayList("@cusgrcd", cbcusgr.SelectedValue.ToString()));
            //        Session["lParamsoabr"] = arr;
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa1bycusgr&p=3');", true);
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            //else
            //{
            //    //arr.Add(new cArrayList("@type", "Summary"));
            //    if (cbtypofrep.SelectedValue.ToString() == "SOA2")
            //    {
            //        if (cbrepby.SelectedValue.ToString() == "0")
            //        {
            //            Session["lParamsoabr"] = arr;
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bybranch');", true);
            //        }
            //        else if (cbrepby.SelectedValue.ToString() == "1")
            //        {
            //            if (grdsl.Rows.Count.ToString() == "0")
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Salesman Error','Please insert at least one salesman !!','warning');", true);
            //                return;
            //            }
            //            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //            Session["lParamsoabr"] = arr;
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2byslsman&p=1');", true);
            //        }
            //        else if (cbrepby.SelectedValue.ToString() == "2")
            //        {
            //            if (grdcust.Rows.Count.ToString() == "0")
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer Error','Please insert at least one customer !!','warning');", true);
            //                return;
            //            }
            //            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            //            Session["lParamsoabr"] = arr;
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bycust&p=2');", true);
            //        }
            //        else if (cbrepby.SelectedValue.ToString() == "3")
            //        {
            //            arr.Add(new cArrayList("@cusgrcd", cbcusgr.SelectedValue.ToString()));
            //            Session["lParamsoabr"] = arr;
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "go", "openreport('fm_report2.aspx?src=soa2bycusgr&p=3');", true);
            //        }
            //        else
            //        {
            //            return;
            //        }

            //    }
            //}
        }
        catch (Exception ex)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@err_source", "Print RETURHO"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        //arr.Add(new cArrayList("@cust_cd", prefixText));
        arr.Add(new cArrayList("@returho_no", prefixText));
        bll.vGettmst_returho(arr, ref rs);
        //bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["returho_no"].ToString(), rs["returho_no"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }

    protected void cbtypofrep_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbtypofrep.SelectedValue.ToString() == "0")
        {
            dtstart.CssClass = "form-control ro";
            dtend.CssClass = "form-control ro";
            txreturho.CssClass = "form-control";

        }
        else if (cbtypofrep.SelectedValue.ToString() == "1")
        {
            dtstart.CssClass = "form-control";
            dtend.CssClass = "form-control";
            txreturho.CssClass = "form-control ro";
        }

    }
}