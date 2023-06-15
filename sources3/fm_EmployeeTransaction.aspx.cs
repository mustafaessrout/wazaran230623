using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_EmployeeTransaction : System.Web.UI.Page
{
    cbll bll = new cbll();
    List<cArrayList> arr = new List<cArrayList>();
    decimal Vat_Amt;
    decimal dVat;
    string sVat = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {         
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSp(ref ddlEmp, "sp_tmst_employee_getbyprofile1", "emp_cd", "emp_desc", arr);
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", Convert.ToString("EmpCNDN")));
            bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", Convert.ToString("emp_trxn_type")));
            bll.vBindingComboToSp(ref ddlEmpTrxnType, "sp_tfield_value_get", "fld_valu", "fld_desc", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
           

            
        }
    }

    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlEmpTrxnType.SelectedIndex = 0;   
        ddlReason.SelectedIndex = 0;
        ddlVat.SelectedIndex = 0;
        txAmt.Text = "";
        txRefNo.Text = "";
        txRemark.Text = "";
        lbvat.Visible = false;
        dtTrxnDate.Text = "";
       
    }
      protected void ddlVat_SelectedIndexChanged(object sender, EventArgs e)
      {
     
          if (ddlVat.SelectedValue.ToString() == "Yes")
          {
              sVat = bll.sGetControlParameter("VAT");
              dVat = Convert.ToDecimal(sVat);

              if (txAmt.Text == "")
              {
                  lbvat.ForeColor = System.Drawing.Color.Red;
                  lbvat.Text = "Enter a Valid Amount to Show Vat!";
                  lbvat.Visible = true;
              }
              else
              { Vat_Amt = Convert.ToDecimal(txAmt.Text) * dVat;
              hdvat.Value = Convert.ToString(Math.Round(Vat_Amt, 2));
              lbvat.ForeColor = System.Drawing.Color.Blue;
              lbvat.Text = "Vat = " + Convert.ToString(Math.Round(Vat_Amt, 2));
              lbvat.Visible = true;
              }
          }
          else
          { lbvat.Visible = false;
            Vat_Amt = Convert.ToDecimal(txAmt.Text) * 0 ;
          }
      }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      
        arr.Clear();
        if (ddlEmp.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected ','Please Select Employee!','warning');", true);
            return;
        }
        else
        {   arr.Add((new cArrayList("@Emp_CD", ddlEmp.SelectedValue.ToString())));}

        if (dtTrxnDate.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Transaction Date is not Selected ','Please Select Transaction Date!','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Trxn_dt", DateTime.ParseExact(dtTrxnDate.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)))); }
        

        if (ddlEmpTrxnType.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Transaction Type is not Selected ','Please Select Transaction Type !','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Trxn_typ", ddlEmpTrxnType.SelectedValue.ToString()))); }

        if (txRefNo.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Reference No is Empty','Please Enter Reference No!','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Ref_no", txRefNo.Text))); }

        if (txAmt.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount is Empty ','Please Enter The Amount !','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Amt", txAmt.Text))); }

        if (ddlVat.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Vat Included is not Selected ','Please Select Vat Included !','warning');", true);
            return;
        }
        else if (ddlVat.SelectedValue.ToString() == "Yes")
        {
            arr.Add((new cArrayList("@Vat_Amt", Convert.ToDouble(hdvat.Value))));         
        }
        else 
        {
            arr.Add((new cArrayList("@Vat_Amt", 0.00)));
        }

        if (ddlReason.SelectedValue.ToString() == "Select")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Reason is not Selected ','Please Select Reason!','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@reasn_cd", ddlReason.SelectedValue.ToString()))); }
        if (txRemark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Remark is Empty ','Please Write a Remark !','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Remark", txRemark.Text))); }
        arr.Add((new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString())));
        SqlDataReader rs = null;
        bll.vTacc_Employeelog_Ins(arr, ref rs);
        while (rs.Read())
        {
            hdids.Value = rs["ids"].ToString();

        } rs.Close();

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Data Submitted Successfully!','','success');", true);
        btnSubmit.Enabled = false;
        btnPrint.Visible = true;
        btnNew.Visible = true;

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "opf", "openreport('fm_report2.aspx?src=EmpTrxn&ids=" + hdids.Value.ToString() + "');", true);
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_EmployeeTransactionReport.aspx");
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
         Response.Redirect("fm_EmployeeTransaction.aspx");
    }
}