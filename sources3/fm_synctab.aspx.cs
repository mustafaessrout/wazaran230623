using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


public partial class fm_synctab : System.Web.UI.Page
{
    cbll bll = new cbll();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            
        }
    }
    protected void btsync_Click(object sender, EventArgs e)
    {
        SqlCommand cmdx = new SqlCommand();
        //string sCanvas = bll.vLookUp("select dbo.fn_getcontrolparameter('tabcanvas')");
        string sSales = bll.vLookUp("select dbo.fn_getcontrolparameter('tabsales')");
        string sPayment = bll.vLookUp("select dbo.fn_getcontrolparameter('tabpayment')");
        string sTransfer = bll.vLookUp("select dbo.fn_getcontrolparameter('tabtransfer')");
        string sRetur = bll.vLookUp("select dbo.fn_getcontrolparameter('tabretur')");
        string sDOSalesInv = bll.vLookUp("select dbo.fn_getcontrolparameter('tabdosales')");


        try
        {
            SqlConnection cnho = new SqlConnection(ConfigurationManager.ConnectionStrings["connstrho"].ConnectionString.ToString());
            cnho.Open();
            cmdx.CommandType = System.Data.CommandType.StoredProcedure;
            cmdx.Connection = cnho;
            cmdx.CommandText = sSales ;
            cmdx.ExecuteNonQuery();
            cmdx.CommandText = sPayment;
            cmdx.ExecuteNonQuery();
            cmdx.CommandText = sTransfer;
            cmdx.ExecuteNonQuery();
            cmdx.CommandText = sRetur;
            cmdx.ExecuteNonQuery();
            cmdx.CommandText = sDOSalesInv;
            cmdx.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Synchronization Done','Please check!','success');", true);
        }
        catch (Exception ex)
        {
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : btsync_Click - fm_synctab");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Connection error between HO and Branch','Try again until completed!','warning');", true);
        }
        finally{
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "hidemsg();", true);
            
        }
    }
}