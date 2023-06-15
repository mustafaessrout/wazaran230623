using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_cashoutRemarksPopup : System.Web.UI.Page
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
                    BindGrid(lblCashOutCode.Text);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
                ut.Logs("", "Account", "Sales Target Priority", "fm_cashoutRemarksPopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
            }
        }

    }

    void BindGrid(string cashOutCode)
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();

            arr.Add(new cArrayList("@itemco_cd", cashOutCode));
           
            dt = bll.vGetCashOutRemarkByCode(arr);
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
            ut.Logs("", "Account", "CashOut Remark", "fm_cashoutRemarksPopup", "PageLoad", "Exception", ex.Message + ex.InnerException);
        }
    }



    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = new DataTable();

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@itemco_cd", Convert.ToString(Request.QueryString["cashOutCode"])));
            arr.Add(new cArrayList("@remark", Convert.ToString(txtRemark.Text)));

            dt = cdl.GetValueFromSP("sp_titemcashout_remark_ins", arr);
            if (Convert.ToInt32(dt.Rows[0][0]) == -1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data already exists..','info');", true);
            }
            else if (Convert.ToInt32(dt.Rows[0][0]) == 2)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Data save successfully..','info');", true);
               txtRemark.Text = string.Empty;
            }



            //
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during update','error');", true);
            ut.Logs("", "Sales Target to Salesman", "Sales Target to Salesman", "fm_cashoutRemarksPopup", "btsave_Click", "Exception", ex.Message + ex.InnerException);
        }
        BindGrid(Convert.ToString(Request.QueryString["cashOutCode"]));
    }

    protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HiddenField hdsequenceno = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdsequenceno");
            HiddenField hdfitemco_cd = (HiddenField)grd.Rows[e.RowIndex].FindControl("hdfitemco_cd");

            List<cArrayList> arr = new List<cArrayList>();
            DataTable dt = new DataTable();
            arr.Add(new cArrayList("@sequenceno", hdsequenceno.Value));
            arr.Add(new cArrayList("@itemco_cd", hdfitemco_cd.Value));
            bll.vDeleteItemCashOutRemark(arr);
            BindGrid(Convert.ToString(Request.QueryString["cashOutCode"]));
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('data deleted successfully..','info');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during bind data','error');", true);
            ut.Logs("", "Account", "CashOut Remark", "fm_cashoutRemarksPopup", "grd_RowDeleting", "Exception", ex.Message + ex.InnerException);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "updpnl", "closewin()", true);
    }
    protected void grd_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        HiddenField hdsequenceno = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdsequenceno");
        HiddenField hdfitemco_cd = (HiddenField)grd.Rows[e.NewSelectedIndex].FindControl("hdfitemco_cd");


        List<cArrayList> arr = new List<cArrayList>();
        DataTable dt = new DataTable();
        arr.Add(new cArrayList("@sequenceno", hdsequenceno.Value));
        arr.Add(new cArrayList("@itemco_cd", hdfitemco_cd.Value));

        dt = bll.vGetCashOutRemarkByIDS(arr);
        if (dt.Rows.Count > 0)
        {
            txtRemark.Text = Convert.ToString(dt.Rows[0]["remark"]);
        }
    }
}