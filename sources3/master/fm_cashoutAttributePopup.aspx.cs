using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashoutAttributePopup : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                string cashOutCode = Convert.ToString(Request.QueryString["cashOutCode"]);
                lblCashOutCode.Text = cashOutCode;
                List<cArrayList> arr = new List<cArrayList>();
                DataTable dtCashOut = new DataTable();
                arr.Add(new cArrayList("@cashOut_cd", cashOutCode));
                dtCashOut = bll.vGetCashOutByCode(arr);

                if (dtCashOut.Rows.Count > 0)
                {
                    lblInOut.Text = Convert.ToString(dtCashOut.Rows[0]["inOutValue"]);
                    lblRoutineNonRoutine.Text = Convert.ToString(dtCashOut.Rows[0]["routineValue"]);
                    lblCategory.Text = Convert.ToString(dtCashOut.Rows[0]["cashoutType"]);
                    bll.vBindingFieldValueToCombo(ref ddlDataType, "datatype");
                    ddlDataType_SelectedIndexChanged(sender, e);
                    BindGrid(lblCashOutCode.Text);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
                ut.Logs("", "Account", "CashOut Attribute", "fm_cashoutAttributePopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }

    }

    void BindGrid(string cashOutCode)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();

            arr.Add(new cArrayList("@cashOut_cd", cashOutCode));
            arr.Add(new cArrayList("@dataType", Convert.ToString(ddlDataType.SelectedValue)));

            dt = bll.vGetCashOutAttributeByCode(arr);
            if (dt.Rows.Count > 0)
            {
                grd.DataSource = dt;
                grd.DataBind();
            }
            else
            {
                grd.DataSource = null;
                grd.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Account", "CashOut Attribute", "fm_cashoutAttributePopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
        }
    }



    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@itemco_cd", Convert.ToString(Request.QueryString["cashOutCode"])));
            arr.Add(new cArrayList("@attribute_nm", Convert.ToString(txtAttributeName.Text)));
            arr.Add(new cArrayList("@dataType", Convert.ToString(ddlDataType.SelectedValue)));

            dt = cdl.GetValueFromSP("sp_titemcashout_attribute_ins", arr);
            if (Convert.ToInt32(dt.Rows[0][0]) == -1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data already exists..','info');", true);
            }
            else if (Convert.ToInt32(dt.Rows[0][0]) == 2)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data save successfully..','info');", true);
                txtAttributeName.Text = string.Empty;
            }
           


            //
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during update','error');", true);
            ut.Logs("", "Sales Target to Salesman", "Sales Target to Salesman", "fm_cashoutAttributePopup", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }
        BindGrid(Convert.ToString(Request.QueryString["cashOutCode"]));
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdIDS = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdIDS");

            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            arr.Add(new cArrayList("@IDS", hdIDS.Value));
            bll.vDeleteItemCashOutAttribute(arr);
            BindGrid(Convert.ToString(Request.QueryString["cashOutCode"]));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('data deleted successfully..','info');", true);
            
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Account", "CashOut Attribute", "fm_cashoutAttributePopup", "grd_RowDeleting", "Exception", ex.Message + ex.InnerException);
        }
    }
    protected void ddlDataType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(Convert.ToString(Request.QueryString["cashOutCode"]));
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdIDS = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdIDS");
        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        bll.vBindingFieldValueToCombo(ref ddlDataType, "datatype");
        arr.Add(new cArrayList("@IDS", hdIDS.Value));

        dt = bll.vGetCashOutAttributeByIDS(arr);
        if (dt.Rows.Count > 0)
        {
            txtAttributeName.Text = Convert.ToString(dt.Rows[0]["attribute_nm"]);
           ddlDataType.SelectedValue = Convert.ToString(dt.Rows[0]["datatype"]);
        }
    }
}