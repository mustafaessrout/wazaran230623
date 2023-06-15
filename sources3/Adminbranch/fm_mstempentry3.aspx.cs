using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_mstempentry3 : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bll.vBindingFieldValueToCombo(ref cbjobtitle, "job_title_cd");
            bll.vBindingFieldValueToCombo(ref cblevel, "level_cd");
            bll.vBindingFieldValueToCombo(ref cbnationality, "nationality");
            bll.vBindingFieldValueToCombo(ref cbmarried, "married_sta_id");
            bll.vBindingComboToSp(ref cbsp, "sp_tmst_salespoint_get", "salespointcd", "salespoint_nm");
            msg.Visible = false;
          //  MakeReadOnly();
        }
    }

    //protected void ShowMessage(string Message, MessageType type)
    //{
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    //}
    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txpassword.Text != txpassretype.Text)
        {
           // lbmsg.Text = "Password is not same with re type";
           //// ScriptManager.RegisterStartupScript(Page,Page.GetType(), "alrt", "window.alert('Password not same with re type');", true);
           // msg.Visible = true;
           // return;
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Password is not same with re-type','Error');", true);
            return;
        }

        DateTime dt;
        if (dtjoin.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Join date must in date','Error');", true);
            return;
        }
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", txempcode.Text));
        arr.Add(new cArrayList("@emp_nm", txempname.Text));
        arr.Add(new cArrayList("@married_sta_id", cbmarried.SelectedValue.ToString()));
        arr.Add(new cArrayList("@job_title_cd", cbjobtitle.SelectedValue.ToString()));
        arr.Add(new cArrayList("@level_cd", cblevel.SelectedValue.ToString()));
        arr.Add(new cArrayList("@emp_sn", txshortname.Text));
        arr.Add(new cArrayList("@salespointcd", cbsp.SelectedValue.ToString()));
        arr.Add(new cArrayList("@join_dt", DateTime.ParseExact(dtjoin.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        bll.vInsertMstEmployee(arr);
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", txuserid.Text));
        arr.Add(new cArrayList("@email", txemail.Text));
        arr.Add(new cArrayList("@mobile_no", txmobile.Text));
        arr.Add(new cArrayList("@usr_pwd", txpassword.Text));
        MakeReadOnly();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt", "sweetAlert('','"+txempcode.Text + ":"+txempname.Text+"','success');", true);
    }
    protected void btsearch_ServerClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "popupwindow('lookup_emp.aspx');", true);
    }

    void MakeReadOnly()
    {
        txempname.CssClass = "form-control input-sm ro";
        txempname.CssClass = "form-control input-sm ro";
        dtjoin.CssClass = "form-control input-sm ro";
        txshortname.CssClass = "form-control input-sm ro";
        cbjobtitle.CssClass = "form-control-static input-sm ro";
        cbmarried.CssClass = "form-control-static input-sm ro";
        cbnationality.CssClass = "form-control-static input-sm ro";
        cbsp.CssClass = "form-control-static input-sm ro";
        cblevel.CssClass = "form-control-static input-sm ro";
    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
        bll.vGetMstEmployee( ref rs,arr);
        while (rs.Read())
        {
            txempcode.Text = rs["emp_cd"].ToString();
            txempname.Text = rs["emp_nm"].ToString();
            if (!rs["level_cd"].Equals(DBNull.Value))
            {
                cblevel.SelectedValue =  rs["level_cd"].ToString();
            }
            if (!rs["job_title_cd"].Equals(DBNull.Value))
            {
                cbjobtitle.SelectedValue = rs["job_title_cd"].ToString();
            }
            cbsp.SelectedValue = rs["salespointcd"].ToString();
           
        }
        rs.Close();
        MakeReadOnly();
    }
    protected void btedit_Click(object sender, EventArgs e)
    {
        txemail.CssClass = "form-control input-sm";
        txempname.CssClass = "form-control input-sm";
        txmobile.CssClass = "form-control input-sm";
        txshortname.CssClass = "form-control input-sm";
        cbjobtitle.CssClass = "form-control-static input-sm";
        cbmarried.CssClass = "form-control-static input-sm";
        cbnationality.CssClass = "form-control-static input-sm";
        cbsp.CssClass = "form-control-static input-sm";
        cblevel.CssClass = "form-control-static input-sm";
        dtjoin.CssClass = "form-control input-sm";
    }
    protected void CogLinkButton_Click(object sender, EventArgs e)
    {

    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        txemail.CssClass = "form-control input-sm";
        txempname.CssClass = "form-control input-sm";
        txmobile.CssClass = "form-control input-sm";
        txshortname.CssClass = "form-control input-sm";
        cbjobtitle.CssClass = "form-control-static input-sm";
        cbmarried.CssClass = "form-control-static input-sm";
        cbnationality.CssClass = "form-control-static input-sm";
        cbsp.CssClass = "form-control-static input-sm";
        cblevel.CssClass = "form-control-static input-sm";
        dtjoin.CssClass = "form-control input-sm";
        txempcode.Text = ""; txempname.Text = "";
        txempname.Text = ""; txmobile.Text = "";
        txshortname.Text = "";
    }
  
}