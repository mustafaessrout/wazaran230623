using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class claim_track_map : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingComboToSp(ref cbperiod, "sp_tmst_period_get", "period_cd", "period_nm");
        }
        
    }
    protected void btclaimmap_Click(object sender, EventArgs e)
    {
        LinkButton button = (LinkButton)sender;
        string buttonId = button.ID;
        string salespoint = "", branchnm = "";
        switch (buttonId)
        {
            case "bisha":
                salespoint = "526"; branchnm = "BISHA";
                break;
            case "dawadmi":
                salespoint = "208"; branchnm = "DAWADMI";
                break;
            case "gasheem":
                salespoint = "204"; branchnm = "GASIEM";
                break;
            case "hafrbatin":
                salespoint = "307"; branchnm = "HAFR BATIN";
                break;
            case "hail":
                salespoint = "209"; branchnm = "HAIL";
                break;
            case "hufuf":
                salespoint = "306"; branchnm = "HUFUF";
                break;
            case "jeddah":
                salespoint = "101"; branchnm = "SBTC JEDDAH";
                break;
            case "jizan":
                salespoint = "507"; branchnm = "JIZAN";
                break;
            case "jubail":
                salespoint = "302"; branchnm = "JUBAIL";
                break;
            case "khamis":
                salespoint = "501"; branchnm = "KHAMIS MUSHAIT";
                break;
            case "kharaj":
                salespoint = "202"; branchnm = "KHARJ";
                break;
            case "khobar":
                salespoint = "301"; branchnm = "KHOBAR";
                break;
            case "makkah":
                salespoint = "105"; branchnm = "MAKKAH";
                break;
            case "medina":
                salespoint = "108"; branchnm = "MADINAH";
                break;
            case "najran":
                salespoint = "511"; branchnm = "NAJRAN";
                break;
            case "qunfuda":
                salespoint = "517"; branchnm = "QONFUDA";
                break;
            case "riyadh":
                salespoint = "201"; branchnm = "RIYADH";
                break;
            case "sakaka":
                salespoint = "401"; branchnm = "SKAKA";
                break;
            case "tabuk":
                salespoint = "407"; branchnm = "TABUK";
                break;
            case "taif":
                salespoint = "112"; branchnm = "TAIF";
                break;
            case "yanbu":
                salespoint = "114"; branchnm = "YANBU";
                break;
        }
        //salespoint = "516"; branchnm = "BAHA";                     
        claimmap(salespoint, branchnm);
        //lblModalTitle.Text = "Claim Track DATA Bisha";
        ////lblModalBody.Text = "This is modal body";
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@salespointcd", "101"));
        //arr.Add(new cArrayList("@period",cbperiod.SelectedValue.ToString()));
        //bll.vBindingGridToSp(ref grd, "sp_trackclaimho", arr);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        //upModal.Update();

        //txtx.Text = "Test";

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dataPopUp", "dataPopUp();", true);
        //upModal1.Update();
    }
    void claimmap(string salespoint, string branchnm)
    {
        lblModalTitle.Text = "CLAIM TRACK DATA " + branchnm + "";
        //lblModalBody.Text = "This is modal body";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salespointcd", salespoint));
        arr.Add(new cArrayList("@period", cbperiod.SelectedValue.ToString()));
        bll.vBindingGridToSp(ref grd, "sp_trackclaimho", arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
        upModal.Update();
    }
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    lblModalTitle.Text = "Validation Errors List for HP7 Citation";
    //    //lblModalBody.Text = "This is modal body";
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@salespointcd", ""));
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
    //    upModal.Update();
    //}
   
}