using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class site : System.Web.UI.MasterPage
{
    cbll bll = new cbll();
    public List<string> arrmenu;
    public List<string> arrmenucaption;
    public List<string> arraspfile;
    public List<string> arrallmenu;
    public List<string> arrallaspfile;
    public List<string> arrallmenucaption;
    protected void Page_Init(object sender, EventArgs e)
    {
        //if (Request.Cookies["startapp"].Value.ToString()=="0")
        //{
        //    Response.Redirect("alert_denied.aspx?m=2");
        //}
        if (Request.Cookies["usr_id"] == null)
        {
            Response.Redirect("fm_loginol.aspx");
        }

        string sSalespoint, sUsrId;
        SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        List<string> arr1 = new List<string>();
        List<string> arraspx = new List<string>();
        List<string> arrmenucaption = new List<string>();
        arrallmenu = new List<string>();
        arrallmenucaption = new List<string>();
        List<string> arrallaspx = new List<string>();
        sUsrId = Request.Cookies["usr_id"].Value.ToString();
        sSalespoint = Request.QueryString["sp"];
        if (sSalespoint == null || sSalespoint == "")
        {
            //sSalespoint = Response.Cookies["sp"].Value.ToString();
            sSalespoint = Request.Cookies["sp"].Value.ToString();
        }
        //if (sSalespoint != null && sSalespoint != "")
        //{
        //    Response.Cookies["sp"].Value = sSalespoint;
        //    Response.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
        //    Request.Cookies["sp"].Value = sSalespoint;
        //    Request.Cookies["spn"].Value = bll.sGetSalespointname(sSalespoint);
        //    Response.Cookies["waz_dt"].Value = bll.sGetControlParameterSalespoint("wazaran_dt", sSalespoint);
        //    Request.Cookies["waz_dt"].Value = bll.sGetControlParameterSalespoint("wazaran_dt", sSalespoint);
        //}
        //else
        //{
        //    sSalespoint = Response.Cookies["sp"].Value;
        //    sSalespoint = Request.Cookies["sp"].Value;
        //}

        arr.Clear();
        arr.Add(new cArrayList("@usr_id", sUsrId));
        arr.Add(new cArrayList("@salespoint_typ", bll.vLookUp("select salespoint_typ from tmst_salespoint where salespointcd='" + sSalespoint + "'")));
        bll.vCheckMenu(arr, ref rs);
        while (rs.Read())
        {
            arr1.Add(rs["menu_cd"].ToString());
            arraspx.Add(rs["aspfile"].ToString());
            arrmenucaption.Add(rs["menu_nm"].ToString());
        }
        rs.Close();
        bll.vGetMstMenu(ref rs);
        while (rs.Read())
        {
            arrallmenu.Add(rs["menu_cd"].ToString());
            arrallmenucaption.Add(rs["menu_nm"].ToString());
            arrallaspx.Add(rs["aspfile"].ToString());
        }
        rs.Close();
        Session["themenu"] = arr1;
        Session["aspfile"] = arraspx;
        Session["menucaption"] = arrmenucaption;
        Session["allmenu"] = arrallmenu;
        Session["allaspfile"] = arrallaspx;
        Session["allmenucaption"] = arrallmenucaption;


        arrmenu = (List<string>)Session["themenu"];
        arrmenucaption = (List<string>)Session["menucaption"];
        arraspfile = (List<string>)Session["aspfile"];
        arrallmenu = (List<string>)Session["allmenu"];
        arrallaspfile = (List<string>)Session["allaspfile"];
        arrallmenucaption = (List<string>)Session["allmenucaption"];
        
        //if ((arrmenu == null) || (Request.Cookies["sms"].Value.ToString() ==""))

	if (arrmenu == null) 
        {
            //Response.Cookies["sms"].Value = "";
            Response.Redirect("fm_loginol.aspx");
        }

       
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Session["themenu"] = arrmenu;
        Session["menucaption"] = arrmenucaption;
        Session["aspfile"] = arraspfile;
        Session["allmenu"] = arrallmenu;
        Session["allmenucaption"] = arrallmenucaption;
        Session["allaspfile"] = arrallaspfile;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        string currentPage = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
        string x;
        cbll bll = new cbll();
        //if (bll.nAccessRight(Request.Cookies["usr_id"].Value.ToString(),currentPage) == 0)
        //{
        //    Response.Redirect("alert_denied.aspx");
        //}
        //x = bll.vLookUp("SELECT notifications + ', ' AS 'data()' FROM tb_note FOR XML PATH('')");
        x = bll.vLookUp("select dbo.sfnGetPendingTaskbyUser('"+ Request.Cookies["sp"].Value.ToString() + "','"+ Request.Cookies["usr_id"].Value.ToString() + "')");
        if (x != "")
        {
            x = x.Remove(x.Trim().Length - 1);
        }
        if (Request.Cookies["sp"].Value.ToString() != "0")
        {
            string scd = bll.vLookUp("select dbo.sfnGetcountdown('"+ Request.Cookies["sp"].Value.ToString() + "')");
            Response.Cookies["cdclosing"].Value = scd;
        }
        //Take the value from database and store it in this string variable
        //This will come from database. 

        //Now set the marquee string to the lable inside marquee tag.
        //lblM.Text = x;
        //Session handled
        if (!IsPostBack)
        {
            if (Request.Cookies["usr_id"] == null)
            {
                Response.Redirect("fm_loginol.aspx");
            }
          
            string sUserID = Request.Cookies["usr_id"].Value.ToString();
            lbuser.Text =  Request.Cookies["fullname"].Value.ToString();  // sUserID + " : " + bll.vLookUp("select fullname from tuser_profile where emp_cd='" + sUserID + "'");
            lblang.Text = Request.Cookies["lang"].Value.ToString();
            lbsp.Text = Request.Cookies["sp"].Value.ToString()+" - " +Request.Cookies["spn"].Value.ToString() ;
          //  lbwazarandate.Text =Request.Cookies["waz_dt"].Value.ToString();
           
            if (Request.Cookies["lang"].Value.ToString() == "SA")
            {
                bll.vLang(ref lbwel);
            }
            lbnotification.Text = x;
        }
        //Session["arrmenu"] = arrmenu;
        //Session["arrmenucaption"] = arrmenucaption;
        //Session["arraspfile"] = arraspfile;
    
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string GetBanner()
    {
        string sTemp = string.Empty;
        sTemp = "Welcome to wazaran";
        return (sTemp);
    }
}
