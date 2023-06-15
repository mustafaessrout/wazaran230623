using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_salestargethoPopup : System.Web.UI.Page
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
                string period = Request.QueryString["period"];
                string salesPoint = Request.QueryString["salesPoint"];
                string prodcd = Request.QueryString["prodcd"];

                DataTable dt = new DataTable();

                List<cArrayList> arr = new List<cArrayList>();
                BindGrid();

                DataTable dtControl = new DataTable();

                arr.Add(new cArrayList("@prodcd", prodcd));
                arr.Add(new cArrayList("@period", period));
                arr.Add(new cArrayList("@salespointcd", salesPoint));

                dtControl = cdl.GetValueFromSP("sp_tsalestargetho_getDetails", arr);
                if (dtControl.Rows.Count > 0)
                {
                    lblBranchName.Text = Convert.ToString(dtControl.Rows[0]["salespoint_nm"]);
                    lblPeriod.Text = Convert.ToString(dtControl.Rows[0]["period"]);
                    lblProduct.Text = Convert.ToString(dtControl.Rows[0]["prod_nm"]);
                }


                arr.Clear();
                arr.Add(new cArrayList("@fld_nm", "GroupSalesman"));
                DataTable dtDDL = new DataTable();
                dtDDL = cdl.GetValueFromSP("sp_tfield_value_get", arr);

                if (dtDDL.Rows.Count > 0)
                {
                    ddlSalesGroup.DataValueField = "fld_valu";
                    ddlSalesGroup.DataTextField = "fld_desc";
                    ddlSalesGroup.DataSource = dtDDL.DefaultView;
                    ddlSalesGroup.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
                ut.Logs("", "Sales Target", "Sales Target Priority", "fm_salestargethoPopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }

    }

    
    void BindGrid()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            string period = Request.QueryString["period"];
            string salesPoint = Request.QueryString["salesPoint"];
            string prodcd = Request.QueryString["prodcd"];

            DataTable dt = new DataTable();

            arr.Add(new cArrayList("@period", period));
            arr.Add(new cArrayList("@SalesPointCD", salesPoint));
            arr.Add(new cArrayList("@prod_cd", prodcd));
            arr.Add(new cArrayList("@GroupSalesman", null));

            dt = cdl.GetValueFromSP("sp_tsalestargetsalespointSalesGroup_get", arr);
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
            ut.Logs("", "Sales Target", "Sales Target Priority", "fm_salestargethoPopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
        }
    }

   protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during close','error');", true);
            ut.Logs("", "Sales Target to Salesman", "Sales Target to Salesman", "fm_salestargethoPopup", "btnClose_Click", "Exception", ex.Message + ex.InnerException);
        }

    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            string period = Request.QueryString["period"];
            string salesPoint = Request.QueryString["salesPoint"];
            string prodcd = Request.QueryString["prodcd"];
            string qty = Request.QueryString["qty"];

            
             
                DataTable dt = new DataTable();

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@period", period));
                arr.Add(new cArrayList("@SalesPointCD", salesPoint));
                arr.Add(new cArrayList("@prod_cd", prodcd));
                arr.Add(new cArrayList("@GroupSalesman", Convert.ToString(ddlSalesGroup.SelectedValue)));
                arr.Add(new cArrayList("@entryby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@qty", qty));

                dt = cdl.GetValueFromSP("sp_tsalestargetsalespointSalesGroup_ins", arr);
                if (Convert.ToInt32(dt.Rows[0][0]) == -1) {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data already exists..','info');", true);
                }
                else if (Convert.ToInt32(dt.Rows[0][0]) == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data save successfully..','info');", true);
                }
                BindGrid();
            
            
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during update','error');", true);
            ut.Logs("", "Sales Target to Salesman", "Sales Target to Salesman", "fm_salestargethoPopup", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }

    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdprodcd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdprodcd");
            Label lbPeriod = (Label)grd.Rows[e.RowIndex].FindControl("lbPeriod");
            HiddenField hdSalesPointCD = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdSalesPointCD");
            HiddenField hdGroupSalesman = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdGroupSalesman");

            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            arr.Add(new cArrayList("@prod_cd", hdprodcd.Value));
            arr.Add(new cArrayList("@period", lbPeriod.Text));
            arr.Add(new cArrayList("@SalesPointCD", Convert.ToString(hdSalesPointCD.Value)));
            arr.Add(new cArrayList("@GroupSalesman", Convert.ToString(hdGroupSalesman.Value)));
            dt = cdl.GetValueFromSP("sp_tsalestargetsalespointSalesGroup_dlt", arr);
            
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('data deleted successfully..','info');", true);
                BindGrid();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Sales Target", "Sales Target Priority", "fm_salestargethoPopup", "grd_RowDeleting", "Exception", ex.Message + ex.InnerException);
        }
    }
}