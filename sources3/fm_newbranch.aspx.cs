using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_newbranch : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                List<cArrayList> arr = new List<cArrayList>();
                bll.vBindingGridToSp(ref grdsalespoint, "sp_newsalespoint_get", arr);
                jjsalespoint.Attributes.Add("style", "display:none");
            }
            catch (Exception ex) 
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_newbranch");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {

    }

    protected void btnext_Click(object sender, EventArgs e)
    {

    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            double dAmountCash = 0;
            if (!double.TryParse(txcash.Text, out dAmountCash))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Balance Cashier must be numeric','Check Amount Cashier','warning');", true);
                return;
            }

            foreach (GridViewRow row in grditem.Rows)
            {
                TextBox txtqty = (TextBox)row.FindControl("txtqty");
                Label lbitem_cd = (Label)row.FindControl("lbitem_cd");
                double dqty = Convert.ToDouble(txtqty.Text);
                double dQtyCheck = 0;
                if (!double.TryParse(txtqty.Text, out dQtyCheck))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Qty must be numeric : "+lbitem_cd.Text+"','Check Qty','warning');", true);
                    return;
                }
                if (dqty < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Cant add Qty Minus','Item. " + lbitem_cd.Text + "','warning');", true);
                    return;
                }
            }

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@wazaran_dt", DateTime.ParseExact(wazarandt.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            arr.Add(new cArrayList("@amount_cash", txcash.Text));
            bll.vSetupNewBranchIns(arr);
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_newbranch");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdsalespoint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdsalespoint_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            Label lbsalespointcd = (Label)grdsalespoint.Rows[e.NewSelectedIndex].FindControl("lbsalespointcd");
            Label lbsalespoint_nm = (Label)grdsalespoint.Rows[e.NewSelectedIndex].FindControl("lbsalespoint_nm");
            lbsalespoint.Text = lbsalespoint_nm.Text;
            jjsalespoint.Attributes.Remove("style");
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_newbranch");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdsalespointparameter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grdsalespointparameter_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void grdsalespointparameter_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdsalespointparameter_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void grdsalespointparameter_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void grdsalespointparameter_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
}