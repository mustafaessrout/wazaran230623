using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class promotor_fm_mstoutsource : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@loc_typ", "CIT"));
            bll.vBindingFieldValueToCombo(ref cbidtype, "id_typ");
            bll.vBindingFieldValueToCombo(ref cbgender, "gender");
            bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
            bll.vBindingComboToSp(ref cbcity, "sp_tmst_location_getbytype", "loc_cd", "loc_nm", arr);
            bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txidno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('ID can not empty!','Pls ID');", true);
            return;
        }

        if (txaddress.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Address can not empty!','Pls Address');", true);
            return;
        }

        if (txmobile.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Mobile No!','Pls mobile no');", true);
            return;
        }

        if (txname.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('Name can not empty!','Pls Fullname');", true);
            return;
        }

        string sCheck = bll.vLookUp("select count(1) from tmst_outsource where idno='"+txidno.Text+"'");
        if ((Convert.ToInt16(sCheck) == 1) && txidno.CssClass == "form-control")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "MsgWarning('This ID already used before!','IQOMA/PASSPORT');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fullname", txname.Text));
        arr.Add(new cArrayList("@idno", txidno.Text));
        arr.Add(new cArrayList("@id_typ", cbidtype.SelectedValue.ToString()));
        arr.Add(new cArrayList("@gender", cbgender.SelectedValue.ToString()));
        arr.Add(new cArrayList("@nationality", cbnationality.SelectedValue.ToString()));
        arr.Add(new cArrayList("@addr", txaddress.Text));
        arr.Add(new cArrayList("@city_cd", cbcity.SelectedValue.ToString()));
        arr.Add(new cArrayList("@deleted", 0));
        arr.Add(new cArrayList("@mobileno", txmobile.Text));
        arr.Add(new cArrayList("@married_sta_id", cbmarried.SelectedValue.ToString()));
        bll.vInsertMstOutsource(arr);
        txname.CssClass = "form-control ro";
        txidno.CssClass = "form-control ro";
        cbidtype.CssClass = "form-control ro";
        cbnationality.CssClass = "form-control ro";
        cbcity.CssClass = "form-control ro";
        cbgender.CssClass = "form-control ro";
        btsave.CssClass = "btn btn-info ro";
        txaddress.CssClass = "form-control ro";
        txmobile.CssClass = "form-control ro";
        cbmarried.CssClass = "form-control ro";
        btsave.CssClass = "btn btn-info ro";

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Outsourcing has been saved!','"+txname.Text+"','success');", true);

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_mstoutsource.aspx");
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupcenter('/promotor/lookupoutsource.aspx')", true);

    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('/fm_report2.aspx?src=outsource');", true);
    }
    protected void btlookup_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs=null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@idno", hdiqomah.Value.ToString()));
        bll.vGetMstOutsource(arr, ref rs);
        while (rs.Read())
        {
            txidno.Text = hdiqomah.Value.ToString();
            txname.Text = rs["fullname"].ToString();
            txaddress.Text = rs["addr"].ToString();
            txmobile.Text = rs["mobileno"].ToString();
            cbgender.SelectedValue = rs["gender"].ToString();
            cbcity.SelectedValue = rs["city_cd"].ToString();
            cbidtype.SelectedValue = rs["id_typ"].ToString();
            cbmarried.SelectedValue = rs["married_sta_id"].ToString();
            cbnationality.SelectedValue = rs["nationality"].ToString();
            txidno.CssClass = "form-control ro";
            txname.CssClass = "form-control ro";
            txaddress.CssClass = "form-control ro";
            txmobile.CssClass = "form-control ro";
            cbgender.CssClass = "form-control ro";
            cbcity.CssClass = "form-control ro";
            cbidtype.CssClass = "form-control ro";
            cbnationality.CssClass = "form-control ro";
            cbmarried.CssClass = "form-control ro";
            btsave.CssClass = "btn btn-info ro";
        }
        rs.Close();
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        txname.CssClass = "form-control";
        txaddress.CssClass = "form-control";
        txmobile.CssClass = "form-control";
        cbcity.CssClass = "form-control";
        cbgender.CssClass = "form-control";
        cbidtype.CssClass = "form-control";
        cbmarried.CssClass = "form-control";
        cbnationality.CssClass = "form-control";
        btsave.CssClass = "btn btn-info";
        btnew.CssClass = "btn btn-primary ro";
        btprint.CssClass = "btn btn-danger ro";
    }
}