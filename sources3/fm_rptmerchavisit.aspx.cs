using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_rptmerchavisit : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "merchandcd"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSp(ref cbmerchandiser, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value));
            bll.vBindingComboToSp(ref cbreporttype, "sp_getreporttype", "fld_valu", "fld_desc", arr);
            cbreporttype_SelectedIndexChanged(sender, e);
            if (cbreporttype.Items.Count == 0)
            {
                btprint.CssClass = "btn btn-default ro";
            }
        }
    }

    protected void btprint_Click(object sender, EventArgs e)
    {
        if (dtvisit.Text == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select visit date','Visit Date','warning');", true);
            return;
        }
        switch (cbreporttype.SelectedValue)
        {
            case "VM":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=merchanvisit&dt=" + dtvisit.Text + "&end_dt=" + dtend.Text + "&emp=" + cbmerchandiser.SelectedValue + "');", true);
                break;
            case "SC":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=stockcust&dt=" + dtvisit.Text + "');", true);
                break;
            case "VA":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=merchanvisitall&dt=" + dtvisit.Text + "&end_dt=" + dtend.Text + "');", true);
                break;
            case "MR":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=merchanreport&dt=" + dtvisit.Text + "&emp=" + cbmerchandiser.SelectedValue + "&prod=" + cbproduct.SelectedValue + "');", true);
                break;
            case "OS":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=customerstock&dt=" + dtvisit.Text + "&emp=" + Request.Cookies["usr_id"].Value + "&prod=" + cbproduct.SelectedValue + "');", true);
                break;
            case "SN":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=stockcustomerne&start_dt=" + dtvisit.Text + "&end_dt=" + dtend.Text + "');", true);
                break;
            case "MJ":
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=MERCHANDISERJARED&dt=" + dtvisit.Text + "');", true);
                break;
            case "MD":
                if (dtend.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('End date must be choosen!','Select end date','warning');", true);
                    break;
                }


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=merchduration&start_dt=" + dtvisit.Text + "&end_dt="+dtend.Text+"&emp=" + cbmerchandiser.SelectedValue + "&prod=" + cbproduct.SelectedValue + "');", true);
                break;
        }
    }

    protected void cbreporttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbreporttype.SelectedValue == "VM")
        {
            cbmerchandiser.CssClass = "form-control";
            cbproduct.Items.Clear();
            cbproduct.CssClass = "form-control ro";
            //dtend.CssClass = "form-control ro";
            //dtend.Text = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@qry_cd", "merchandcd"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
            bll.vBindingComboToSp(ref cbmerchandiser, "sp_tmst_employee_getbyqry", "emp_cd", "emp_desc", arr);
        }
        else if (cbreporttype.SelectedValue == "SC")
        {
            cbmerchandiser.Items.Clear();
            cbproduct.Items.Clear();
            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            dtend.CssClass = "form-control ro";
            dtend.Text = string.Empty;
        }
        else if (cbreporttype.SelectedValue == "VA")
        {
            cbmerchandiser.Items.Clear();
            cbproduct.Items.Clear();
            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            //dtend.CssClass = "form-control ro";
            //dtend.Text = string.Empty;
        }
        else if (cbreporttype.SelectedValue == "MR")
        {

            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control";
            dtend.CssClass = "form-control ro";
            dtend.Text = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
        else if (cbreporttype.SelectedValue == "OS")
        {

            cbmerchandiser.CssClass = "form-control  ro";
            cbproduct.CssClass = "form-control";
            dtend.CssClass = "form-control ro";
            dtend.Text = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@level_no", 2));
            bll.vBindingComboToSp(ref cbproduct, "sp_tmst_product_get", "prod_cd", "prod_nm", arr);
        }
        else if (cbreporttype.SelectedValue == "MD")
        {
            cbproduct.Items.Clear();
            cbmerchandiser.Items.Clear();
            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            dtvisit.CssClass = "form-control";
            dtend.CssClass = "form-control";
            dtend.Text = string.Empty;
        }
        else if (cbreporttype.SelectedValue == "SN")
        {
            cbproduct.Items.Clear();
            cbmerchandiser.Items.Clear();
            cbproduct.CssClass = "form-control ro";
            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            dtvisit.CssClass = "form-control";
            dtend.CssClass = "form-control";
            dtend.Text = string.Empty;
        }

        else if (cbreporttype.SelectedValue == "MJ")
        {
            cbmerchandiser.Items.Clear();
            cbproduct.Items.Clear();
            cbmerchandiser.CssClass = "form-control ro";
            cbproduct.CssClass = "form-control ro";
            dtend.CssClass = "form-control ro";
            //dtend.Text = string.Empty;
        }
    }
}