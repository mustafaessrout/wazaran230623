using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cashamount : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbtotalpaid.Text = Request.QueryString["am"];
            lbsalesman.Text = Request.QueryString["qsSalesman_cd"];
        }
    }

    public void getit()
    {
        lbtot500.Text = "semprul";
    }
    protected void txamt500_TextChanged(object sender, EventArgs e)
    {
        double d500 = 0;
        if (!double.TryParse(txamt500.Text, out d500))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 500 must be numeric','wrong input','warning');", true);
            txamt500.Text = "";
            lbtot500.Text = "";
            sGetTotalAmount();
            txamt100.Focus();
            return;
        }
        lbtot500.Text = (500 * Convert.ToDouble(txamt500.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt100_TextChanged(object sender, EventArgs e)
    {
        double d100 = 0;
        if (!double.TryParse(txamt100.Text, out d100))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 100 must be numeric','wrong input','warning');", true);
            txamt100.Text = "";
            lbtot100.Text = "";
            sGetTotalAmount();
            txamt50.Focus();
            return;
        }
        lbtot100.Text = (100 * Convert.ToDouble(txamt100.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt50_TextChanged(object sender, EventArgs e)
    {
        double d50 = 0;
        if (!double.TryParse(txamt50.Text, out d50))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 50 must be numeric','wrong input','warning');", true);
            txamt50.Text = "";
            lbtot50.Text = "";
            sGetTotalAmount();
            txamt20.Focus();
            return;
        }
        lbtot50.Text = (50 * Convert.ToDouble(txamt50.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt20_TextChanged(object sender, EventArgs e)
    {
        double d20 = 0;
        if (!double.TryParse(txamt20.Text, out d20))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 20 must be numeric','wrong input','warning');", true);
            txamt20.Text = "";
            lbtot20.Text = "";
            sGetTotalAmount();
            txamt10.Focus();
            return;
        }
        lbtot20.Text = (20 * Convert.ToDouble(txamt20.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt10_TextChanged(object sender, EventArgs e)
    {
        double d10 = 0;
        if (!double.TryParse(txamt10.Text, out d10))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 10 must be numeric','wrong input','warning');", true);
            txamt10.Text = "";
            lbtot10.Text = "";
            sGetTotalAmount();
            txamt5.Focus();
            return;
        }
        lbtot10.Text = (10 * Convert.ToDouble(txamt10.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt5_TextChanged(object sender, EventArgs e)
    {
        double d5 = 0;
        if (!double.TryParse(txamt5.Text, out d5))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 5 must be numeric','wrong input','warning');", true);
            txamt5.Text = "";
            lbtot5.Text = "";
            sGetTotalAmount();
            txamt1.Focus();
            return;
        }
        lbtot5.Text = (5 * Convert.ToDouble(txamt5.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt1_TextChanged(object sender, EventArgs e)
    {
        double d1 = 0;
        if (!double.TryParse(txamt1.Text, out d1))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 1 must be numeric','wrong input','warning');", true);
            txamt1.Text = "";
            lbtot1.Text = "";
            sGetTotalAmount();
            txamthalf.Focus();
            return;
        }
        lbtot1.Text = (1 * Convert.ToDouble(txamt1.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamthalf_TextChanged(object sender, EventArgs e)
    {
        double dhalf = 0;
        if (!double.TryParse(txamthalf.Text, out dhalf))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.5 must be numeric','wrong input','warning');", true);
            txamthalf.Text = "";
            lbtot05.Text = "";
            sGetTotalAmount();
            txamt025.Focus();
            return;
        }
        lbtot05.Text = (0.5 * Convert.ToDouble(txamthalf.Text)).ToString();
        sGetTotalAmount();
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (lbtotal.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('There is no payment','Entry payment','warning');", true);
            return;
        }
        double dTotalPaid = Convert.ToDouble(lbtotalpaid.Text);
        if (Convert.ToDouble(lbtotal.Text) != dTotalPaid)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount paid was not the same with data entry','Check your payment','warning');", true);
            return;
        }

        Guid sCashID = System.Guid.NewGuid();//Request.QueryString["id"];
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cash_id", sCashID));
        arr.Add(new cArrayList("@clear_dt", DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@amt100", ((txamt100.Text == "") ? "0" : txamt100.Text)));
        arr.Add(new cArrayList("@amt500", ((txamt500.Text == "") ? "0" : txamt500.Text)));
        arr.Add(new cArrayList("@amt50", ((txamt50.Text == "") ? "0" : txamt50.Text)));
        arr.Add(new cArrayList("@amt20", ((txamt20.Text == "") ? "0" : txamt20.Text)));
        arr.Add(new cArrayList("@amt10", ((txamt10.Text == "") ? "0" : txamt10.Text)));
        arr.Add(new cArrayList("@amt5", ((txamt5.Text == "") ? "0" : txamt5.Text)));
        arr.Add(new cArrayList("@amt1", ((txamt1.Text == "") ? "0" : lbtot1.Text)));
        arr.Add(new cArrayList("@amt05", ((txamthalf.Text == "") ? "0" : txamthalf.Text)));
        arr.Add(new cArrayList("@amt025", ((txamt025.Text == "") ? "0" : txamt025.Text)));
        arr.Add(new cArrayList("@amt01", ((txamt01.Text == "") ? "0" : txamt01.Text)));
        arr.Add(new cArrayList("@salesman_cd",lbsalesman.Text));
        bll.vInsertCashRegisterStock(arr);
        arr.Clear();
        arr.Add(new cArrayList("@itemco_cd", Request.QueryString["qsitemco_cd"]));
        arr.Add(new cArrayList("@qry_cd", "cash_sta_id"));
        arr.Add(new cArrayList("@salesman_cd", lbsalesman.Text));
        arr.Add(new cArrayList("@cash_sta_id", "C"));
        bll.vUpdateCashRegister2(arr);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
    }

    void sGetTotalAmount()
    { 
        double dSum = 0;
        if (lbtot500.Text != "")
        {
            dSum += Convert.ToDouble(lbtot500.Text);
        }

        if (lbtot100.Text != "")
        {
            dSum += Convert.ToDouble(lbtot100.Text);
        }
        if (lbtot50.Text != "")
        {
            dSum += Convert.ToDouble(lbtot50.Text);
        }
        if (lbtot20.Text != "")
        {
            dSum += Convert.ToDouble(lbtot20.Text);
        }
        if (lbtot10.Text != "")
        {
            dSum += Convert.ToDouble(lbtot10.Text);
        }
        if (lbtot5.Text != "")
        {
            dSum += Convert.ToDouble(lbtot5.Text);
        }
        if (lbtot1.Text != "")
        {
            dSum += Convert.ToDouble(lbtot1.Text);
        }
        if (lbtot05.Text != "")
        {
            dSum += Convert.ToDouble(lbtot05.Text);
        }
        if (lbtot025.Text != "")
        {
            dSum += Convert.ToDouble(lbtot025.Text);
        }
        if (lbtot01.Text != "")
        {
            dSum += Convert.ToDouble(lbtot01.Text);
        }
        lbtotal.Text = dSum.ToString();
    }
    protected void txamt01_TextChanged(object sender, EventArgs e)
    {
        double d01 = 0;
        if (!double.TryParse(txamt01.Text, out d01))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.1 must be numeric','wrong input','warning');", true);
            txamt01.Text = "";
            lbtot01.Text = "";
            sGetTotalAmount();
            btsave.Focus();
            ;return;
        }
        lbtot01.Text = (0.1 * Convert.ToDouble(txamt01.Text)).ToString();
        sGetTotalAmount();
    }
    protected void txamt025_TextChanged(object sender, EventArgs e)
    {
        double d025 = 0;
        if (!double.TryParse(txamt025.Text, out d025))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount 0.25 must be numeric','wrong input','warning');", true);
            txamt025.Text = "";
            lbtot025.Text = "";
            sGetTotalAmount();
            txamt01.Focus();
            ; return;
        }
        lbtot025.Text = (0.25 * Convert.ToDouble(txamt025.Text)).ToString();
        sGetTotalAmount();
    }
    protected void btclose_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cl", "window.opener.RefreshData();window.close();", true);
    }
}