using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frmWizRecCheque : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        bll.vBindingComboToSp(ref cbBankID, "sp_tblBank_get", "BankID", "banViewName", arr);
        txinsDueDate.Text = DateTime.Now.ToString("d/M/yyyy");
        txinsTransDate.Text = DateTime.Now.ToString("d/M/yyyy");
    }
    protected void btok_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grd.Rows)
        {
            CheckBox chk = (CheckBox)row.FindControl("chk");
            if (chk.Checked)
            {
                Label lblInstruID = (Label)row.FindControl("lblInstruID");
                Label lblSalesPointCD = (Label)row.FindControl("lblSalesPointCD");
                Label lblinsDocNo = (Label)row.FindControl("lblinsDocNo");
                Label lblinsDueDate = (Label)row.FindControl("lblinsDueDate");

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@bigID", lblInstruID.Text));
                arr.Add(new cArrayList("@SalesPointCD", lblSalesPointCD.Text));
                arr.Add(new cArrayList("@datDate", DateTime.ParseExact(txinsTransDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@varDocNo", lblinsDocNo.Text));
                arr.Add(new cArrayList("@datDueDate", DateTime.ParseExact(lblinsDueDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                
                    switch (rbtobe.SelectedValue)
                    {
                        case "1":
                            bll.vInsertsprWizRecToInstruStep2(arr);
                            break;
                        case "2":
                            bll.vInsertsprWizRecToInstruStep3(arr);
                            break;
                        case "3":
                            bll.vInsertsprWizRecToInstruStep4(arr);
                            break;
                        case "4":
                            bll.vInsertsprWizRecToInstruStep5(arr);
                            break;
                    }
                    
            }
        }
        bindingGrd();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Wizard Receipt Cheque successfully !')", true);
    }
    protected void rbtobe_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindingGrd();
    }
    private void bindingGrd()
    {
         string InstruTypeID="0";
        switch (rbtobe.SelectedValue)
        {
            case "1":
                InstruTypeID = "1";
                break;
            case "2":
                InstruTypeID = "2";
                break;
            case "3":
                InstruTypeID = "2";//InstruTypeID = "1,2"
                break;
            case "4":
                InstruTypeID = "4";
                break;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@InstruTypeID", InstruTypeID));
        arr.Add(new cArrayList("@insDueDate", DateTime.ParseExact(txinsDueDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vBindingGridToSp(ref grd, "sp_tblInstru_get", arr);
    }
}