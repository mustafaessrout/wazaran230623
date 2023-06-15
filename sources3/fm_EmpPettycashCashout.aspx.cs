using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class fm_EmpPettycashCashout : System.Web.UI.Page
{
    SqlDataReader rs = null;
    cbll bll = new cbll();
    cdal cdl = new cdal();
    string cbType = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
            bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
            bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
            cbcategory.Enabled = false;
            cbapproval.CssClass = cd.csstextro;
            ddlEmp_SelectedIndexChanged(sender, e);
            cbitem_SelectedIndexChanged(sender, e);
            chvat_SelectedIndexChanged(sender, e);
            cbroutine_SelectedIndexChanged(sender, e);
            //ddlpettycashRefno_SelectedIndexChanged(sender, e);
            tbl.Visible = false;
            tbl1.Visible = false;
            txamt.Enabled = false;
            lbbalance.Text = "0.00";
            lbbalanceNYA.Text = "0.00";
            lbbalanceAVL.Text = "0.00";
            //ddlEmp_SelectedIndexChanged(sender, e);
            ddlclaim_SelectedIndexChanged(sender, e);
            txpic.Enabled = false;


            SqlDataReader rdr = null;
            //SqlConnection con = null;
            SqlCommand cmd = null;
            //con = new SqlConnection(ConfigurationManager
            //.ConnectionStrings["connstr"].ConnectionString);

            //con.Open();

            string CommandText = "select * from tmst_itemcashout where deleted=0 and inout='o'; ";
            cmd = new SqlCommand(CommandText);
            //cmd.Connection = con;
            cmd.Connection = cd.getConnection();

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string itemToRmv = rdr["itemco_cd"].ToString();

                cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
            }


            cbroutine.Items.Remove(cbroutine.Items.FindByValue("0"));
            cbroutine.Items.Insert(0, new ListItem("Select", "0"));

            cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
            cbcategory.Items.Insert(0, new ListItem("Select", "0"));

            cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
            cbitem.Items.Insert(0, new ListItem("Select", "0"));

            cbroutine.SelectedValue = "0";
            cbcategory.SelectedValue = "0";
            cbitem.SelectedValue = "0";

            cbitem.Enabled = false;
            cbcategory.Enabled = false;
            cbroutine.Enabled = false;

            txtaxno.CssClass = cd.csstextro;
        }
    }

    protected void ddlclaim_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
        if (ddlclaim.SelectedValue.ToString() == "Handover")
        {
            hdnotclaim.Visible = false;
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@emp_cd", "All"));
            ////arr.Add(new cArrayList("@job_title_cd", null));
            //arr.Add(new cArrayList("@PettycashRefno", null));
            //arr.Add(new cArrayList("@claim", null));
            //bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get2", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get5", "emp_cd", "emp_desc");
            ddlEmp.Items.Remove(ddlEmp.Items.FindByValue("0"));
            ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
            //PettycashRefno.Visible = true;
            //btsave.Visible = false;
            //btStatus.Visible = false;
            New.Visible = false;
            btsave.Visible = true;
            btprint.Visible = false;
            btnShowData.Visible = true;
            btStatus.Visible = false;

            HandoverEmployee.Visible = true;
            HandoverAmount.Visible = true;
            //Remark.Visible = true;

            panelToHide1.Visible = false;
            panelToHide2.Visible = false;
            panelToHide3.Visible = false;
        }
        else if (ddlclaim.SelectedValue.ToString() == "Yes")
        {
            hdnotclaim.Visible = false;
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@emp_cd", "All"));
            ////arr.Add(new cArrayList("@job_title_cd", null));
            //arr.Add(new cArrayList("@PettycashRefno", null));
            //arr.Add(new cArrayList("@claim", null));
            //bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get2", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get5", "emp_cd", "emp_desc");
            ddlEmp.Items.Remove(ddlEmp.Items.FindByValue("0"));
            ddlEmp.Items.Insert(0, new ListItem("Select", "0"));
            //PettycashRefno.Visible = false;
            New.Visible = false;
            btsave.Visible = false;
            btprint.Visible = true;
            btnShowData.Visible = false;
            btStatus.Visible = false;

            HandoverEmployee.Visible = false;
            HandoverAmount.Visible = false;
            //Remark.Visible = false;

            panelToHide1.Visible = false;
            panelToHide2.Visible = false;
            panelToHide3.Visible = false;
            ManualNo.Visible = false;
            TransactionDate.Visible = false;
            panelToHide4.Visible = false;
            //panelToHide5.Visible = false;
        }
        else if (ddlclaim.SelectedValue.ToString() == "No")
        {
            hdnotclaim.Visible = true;

            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Clear();
            //arr.Add(new cArrayList("@emp_cd", "All"));
            ////arr.Add(new cArrayList("@job_title_cd", null));
            //arr.Add(new cArrayList("@PettycashRefno", null));
            //arr.Add(new cArrayList("@claim", null));
            //bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get2", "emp_cd", "emp_desc", arr);
            bll.vBindingComboToSp(ref ddlEmp, "sp_temployee_advancedEmp_get5", "emp_cd", "emp_desc");
            ddlEmp.Items.Remove(ddlEmp.Items.FindByValue("0"));
            ddlEmp.Items.Insert(0, new ListItem("Select", "0"));

            New.Visible = true;
            btsave.Visible = true;
            btprint.Visible = false;
            btnShowData.Visible = true;
            btStatus.Visible = false;

            HandoverEmployee.Visible = false;
            HandoverAmount.Visible = false;
            //Remark.Visible = false;

            panelToHide1.Visible = true;
            panelToHide2.Visible = true;
            panelToHide3.Visible = true;
            ManualNo.Visible = true;
            TransactionDate.Visible = true;
            panelToHide4.Visible = true;
        }
        //SqlConnection con = null;
        //SqlDataReader rdr = null;
        //SqlDataReader rdr2 = null;
        //SqlCommand cmd = null;
        //SqlCommand cmd2 = null;
        //con = new SqlConnection(ConfigurationManager
        //.ConnectionStrings["SBTCDBConnectionString"].ConnectionString);

        //con.Open();

        //string CommandText = "select * from tmst_employee where salespointcd !='" + Request.Cookies["sp"].Value.ToString() + "'";
        //cmd = new SqlCommand(CommandText);
        //cmd.Connection = con;

        //rdr = cmd.ExecuteReader();

        //while (rdr.Read())
        //{
        //    string itemToRmv = rdr["emp_cd"].ToString();

        //    ddlEmp.Items.Remove(ddlEmp.Items.FindByValue(itemToRmv));
        //}

        //string CommandText2 = "select * from tmst_employee where emp_cd not in(select distinct emp_cd from tcashout_request where emp_cd is not null and itemco_cd in('CHA00048') and cashout_sta_id='A') ";
        //cmd2 = new SqlCommand(CommandText2);
        //cmd2.Connection = con;

        //rdr2 = cmd2.ExecuteReader();

        //while (rdr2.Read())
        //{
        //    string itemToRmv = rdr2["emp_cd"].ToString();

        //    ddlEmp.Items.Remove(ddlEmp.Items.FindByValue(itemToRmv));
        //}
        //ddlEmp.Items.Insert(0, new ListItem("Select", "0"));

        cbcategory.Enabled = false;
        cbapproval.CssClass = cd.csstextro;
        ddlEmp_SelectedIndexChanged(sender, e);
        cbitem_SelectedIndexChanged(sender, e);
        chvat_SelectedIndexChanged(sender, e);
        cbroutine_SelectedIndexChanged(sender, e);
        //ddlpettycashRefno_SelectedIndexChanged(sender, e);
        tbl.Visible = false;
        tbl1.Visible = false;
        txamt.Enabled = false;
        cbitem.Enabled = false;
        cbcategory.Enabled = false;
        cbroutine.Enabled = false;
        lbbalance.Text = "0.00";
        lbbalanceNYA.Text = "0.00";
        lbbalanceAVL.Text = "0.00";
        grd.Visible = false;
        grd1.Visible = false;
        txpic.Enabled = false;
        txpic.Text = "";
        txdept.Text = "";

        cbroutine.Items.Remove(cbroutine.Items.FindByValue("0"));
        cbroutine.Items.Insert(0, new ListItem("Select", "0"));

        cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
        cbcategory.Items.Insert(0, new ListItem("Select", "0"));

        cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
        cbitem.Items.Insert(0, new ListItem("Select", "0"));

        cbroutine.SelectedValue = "0";
        cbcategory.SelectedValue = "0";
        cbitem.SelectedValue = "0";

        cbitem.Enabled = false;
        cbcategory.Enabled = false;
        cbroutine.Enabled = false;
    }
    protected void ddlEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlclaim.SelectedValue.ToString() == "No")
        {
            //cbitem.Enabled = false;
            //cbroutine.Enabled = false;
            //cbcategory.Enabled = false;
            cbitem.Enabled = false;
            //cbcategory.Enabled = true;
            cbroutine.Enabled = true;

            if (ddlEmp.SelectedValue.ToString() != "0")
            {
                tbl1.Visible = true;
                grd1.Visible = true;
                txpic.Enabled = true;

                if (ddlEmp.SelectedValue.ToString()!="" && ddlEmp!=null)
                {
                    List<cArrayList> arr = new List<cArrayList>();
                    arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@PettycashRefno", null));
                    //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                    arr.Add(new cArrayList("@claim", null));
                    //bll.vGetPettyCashEmp(ref rs, arr);
                    bll.vGetPettyCashEmp2(ref rs, arr);
                    while (rs.Read())
                    {
                        lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
                    }
                    lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where pc_sta_id='N' and emp_cd='" + ddlEmp.SelectedValue.ToString() + "' ");
                    if (lbbalanceNYA.Text == "")
                    {
                        lbbalanceNYA.Text = "0.00";
                    }
                    lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));


                    if (rs.HasRows == false)
                    {
                        lbbalance.Text = "0";
                        lbbalanceNYA.Text = "0";
                    }
                    lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));

                    rs.Close();
                }

                //ddlpettycashRefno.Enabled = true;
                List<cArrayList> arr2 = new List<cArrayList>();
                //arr2.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                //arr2.Add(new cArrayList("@PettycashRefno", null));
                ////arr2.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                ////bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get", "cashout_cd", "cashout_cd", arr2);
                arr2.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                ////bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get2", "cashout_cd", "cashout_cd", arr2);
                //ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                lbbalance.Text = "0.00";
                lbbalanceNYA.Text = "0.00";
                lbbalanceAVL.Text = "0.00";
                cbitem.Enabled = false;
                cbcategory.Enabled = false;
                cbroutine.Enabled = false;
                txamt.Enabled = false;
                txpic.Enabled = false;
                //ddlpettycashRefno.Enabled = false;
            }

            cbroutine.SelectedValue = "0";
            txamt.Text = "0.00";
            lbvat.Text = "0.00";
            chvat.SelectedValue = "NONVAT";
            grd.Visible = false;
            tbl.Visible = false;
            tbl1.Visible = false;
            grd1.Visible = false;
            cbapproval.CssClass = cd.csstextro;
            cbapproval.Items.Clear();
            txpic.Text = "";
            txdept.Text = "";
            //ddlpettycashRefno.SelectedValue = "0";
            //ddlpettycashRefno.Enabled = true;
            //List<cArrayList> arrX = new List<cArrayList>();
            //arrX.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
            //arrX.Add(new cArrayList("@PettycashRefno", "XXXXXX"));
            ////arr2.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
            ////bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get", "cashout_cd", "cashout_cd", arrX);
            ////ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));

        }
        else
        {
            if (ddlEmp.SelectedValue.ToString() != "0")
            {

                //ddlpettycashRefno.Enabled = true;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@PettycashRefno", null));
                //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                //bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get", "cashout_cd", "cashout_cd", arr);
                //ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                lbbalance.Text = "0.00";
                lbbalanceNYA.Text = "0.00";
                lbbalanceAVL.Text = "0.00";
                //ddlpettycashRefno.Enabled = false;
                //lbbalancerefno.Text = "0.00";
                //ddlpettycashRefno.Items.Clear();
                //ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));
                //ddlpettycashRefno.SelectedValue = "0";
            }
            grd.Visible = false;
            tbl.Visible = false;
            tbl1.Visible = false;
            grd1.Visible = false;
            //ddlpettycashRefno.SelectedValue = "0";
            lbbalance.Text = "0.00";
            lbbalanceNYA.Text = "0.00";
            lbbalanceAVL.Text = "0.00";
            //ddlpettycashRefno.Enabled = true;
            //List<cArrayList> arr2 = new List<cArrayList>();
            //arr2.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
            //arr2.Add(new cArrayList("@PettycashRefno", null));
            ////arr2.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
            ////bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get", "cashout_cd", "cashout_cd", arr2);
            ////ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));

        }
        if (ddlEmp.SelectedValue.ToString() == "0")
        {
            //ListItem item = cbroutine.Items.FindByText("0");
            //if (item == null)
            //{
            //    cbroutine.Items.Insert(0, "Select");
            //}
            //cbcategory.Items.Insert(0, new ListItem("Select", "0"));
            //cbitem.Items.Insert(0, new ListItem("Select", "0"));
            //cbcategory.SelectedValue = "0";
            //cbitem.SelectedValue = "0";

            ////ddlpettycashRefno.Items.Clear();
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
            ////arr.Add(new cArrayList("@PettycashRefno", null));
            //arr.Add(new cArrayList("@PettycashRefno", ""));
            ////arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
            ////bll.vBindingComboToSp(ref ddlpettycashRefno, "sp_temployee_advancedEmps_pettycashRefno_get", "cashout_cd", "cashout_cd", arr);
            ////ddlpettycashRefno.Items.Insert(0, new ListItem("Select", "0"));
        }

        if (ddlclaim.SelectedValue.ToString() == "Handover")
        {
            List<cArrayList> arr2 = new List<cArrayList>();
            arr2.Clear();
            arr2.Add(new cArrayList("@emp_cd", null));
            arr2.Add(new cArrayList("@job_title_cd", null));
            bll.vBindingComboToSp(ref ddlHandovertoemp, "sp_tmst_employee_get", "emp_cd", "emp_desc", arr2);

            ddlHandovertoemp.Items.Remove(ddlHandovertoemp.Items.FindByValue("0"));
            ddlHandovertoemp.Items.Remove(ddlEmp.Items.FindByValue(ddlEmp.SelectedValue.ToString()));
            ddlHandovertoemp.Items.Insert(0, new ListItem("Select", "0"));
        }
        hdempadv.Value = ddlEmp.SelectedValue.ToString();
        string dept_cd = bll.vLookUp("select dept_cd from tmst_employee where emp_cd='" + hdempadv.Value + "'");

        if (dept_cd == "SA")
        {
            dept_cd = "SALES";
        }
        else if (dept_cd == "ACCH")
        {
            dept_cd = "ACC";
        }
        else if (dept_cd == "Merchandiser")
        {
            dept_cd = "MERCH";
        }
        string dept_nm = bll.vLookUp("select dept_nm from tmst_department where dept_cd='" + dept_cd + "'");
        txdept.Text = dept_nm;
    }
    //protected void ddlpettycashRefno_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    cbitem.Enabled = false;
    //    //cbcategory.Enabled = true;
    //    cbroutine.Enabled = true;

    //    //PettycashRefno.Visible = false;
    //    if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
    //    {
    //        //if (ddlpettycashRefno.SelectedValue.ToString() != "0")
    //        //{
    //            List<cArrayList> arr = new List<cArrayList>();
    //            arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
    //            arr.Add(new cArrayList("@PettycashRefno", null));
    //            //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
    //            bll.vGetPettyCashEmp(ref rs, arr);
    //            while (rs.Read())
    //            {
    //                lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
    //            }
    //            rs.Close();

    //            if (ddlclaim.SelectedValue.ToString() == "Handover")
    //            {
    //                btnShowData_Click(sender, e);
    //            }
    //        //}
    //        //else
    //        //{
    //        //    //lbbalancerefno.Text = "0.00";
    //        //    lbbalance.Text = "0.00";
    //        //}
    //        if (ddlclaim.SelectedValue.ToString() == "Handover")
    //        {
    //            grd1.Visible = true;
    //        }
    //        else
    //        {
    //            tbl.Visible = false;
    //            grd1.Visible = false;
    //            tbl1.Visible = false;
    //        }
    //        //tbl.Visible = false;
    //        //grd1.Visible = false;
    //        //tbl1.Visible = false;
    //        //PettycashRefno.Visible = true;
    //    }

    //    SqlDataReader rdr = null;
    //    SqlConnection con = null;
    //    SqlCommand cmd = null;
    //    con = new SqlConnection(ConfigurationManager
    //    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString);

    //    //if (ddlEmp.SelectedValue.ToString() != "" && ddlEmp != null && ddlEmp.SelectedValue.ToString() != "0" && ddlpettycashRefno.SelectedValue.ToString() != "" && ddlEmp != null && ddlpettycashRefno.SelectedValue.ToString() != "0")
    //    if (ddlEmp.SelectedValue.ToString() != "" && ddlEmp != null && ddlEmp.SelectedValue.ToString() != "0" && ddlEmp != null)
    //    {
    //        con.Open();

    //        //string CommandText = "exec sp_temployee_advancedEmp_get2 '" + ddlEmp.SelectedValue.ToString() + "', '" + ddlpettycashRefno.SelectedValue.ToString() + "'; ";
    //        string CommandText = "exec sp_temployee_advancedEmp_get2 '" + ddlEmp.SelectedValue.ToString() + "', null; ";
    //        cmd = new SqlCommand(CommandText);
    //        cmd.Connection = con;

    //        rdr = cmd.ExecuteReader();

    //        while (rdr.Read())
    //        {
    //            //lbbalancerefno.Text = rdr["totalBalance"].ToString();
    //        }
    //        con.Close();
    //    }
    //    else {
    //        //lbbalancerefno.Text = "0";
    //    }
    //}

    //void vInitItemCashout()
    void vInitItemCashout(string cbType)
    {
        //List<cArrayList> arr = new List<cArrayList>();
        //arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        //arr.Add(new cArrayList("@inout", "O"));
        //bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
        //cbitem_SelectedIndexChanged(this, EventArgs.Empty);


        SqlDataReader rdr = null;
        //SqlConnection con = null;
        SqlCommand cmd = null;
        string CommandText = null;
        //con = new SqlConnection(ConfigurationManager
        //.ConnectionStrings["connstr"].ConnectionString);
        //con.Open();

        CommandText = "select * from tmst_itemcashout where deleted=0 and inout='o'; ";
        cmd = new SqlCommand(CommandText);
        //cmd.Connection = con;
        cmd.Connection = cd.getConnection();

        rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            cbitem.Items.Remove(cbitem.Items.FindByValue(rdr["itemco_cd"].ToString()));
        }
        //con.Close();


        if (cbType != "cbTypeCategory")
        {
            bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);


            //con.Open();

            CommandText = "select * from tfield_value where fld_nm='cashout_typ' and (fld_valu not in(select cashout_typ from tmst_itemcashout where routine='" + cbroutine.SelectedValue.ToString() + "' and inout='O') or fld_valu='ADV') ";
            cmd = new SqlCommand(CommandText);
            //cmd.Connection = con;
            cmd.Connection = cd.getConnection();

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string itemToRmv = rdr["fld_valu"].ToString();

                cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
            }
            //con.Close();
        }

        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", "O"));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
        cbitem_SelectedIndexChanged(this, EventArgs.Empty);

        if (ddlEmp.SelectedValue.ToString() != "" && ddlEmp != null && ddlEmp.SelectedValue.ToString() != "0")
        {
            //con.Open();

            //CommandText = "exec sp_temployee_advancedEmp_get2 '" + ddlEmp.SelectedValue.ToString() + "', '" + ddlpettycashRefno.SelectedValue.ToString() + "'; ";
            CommandText = "exec sp_temployee_advancedEmp_get2 '" + ddlEmp.SelectedValue.ToString() + "', null; ";
            cmd = new SqlCommand(CommandText);
            //cmd.Connection = con;
            cmd.Connection = cd.getConnection();

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                //lbbalancerefno.Text = rdr["totalBalance"].ToString();
            }
            //con.Close();
        }
        else {
            //lbbalancerefno.Text = "0";
        }
    }

    protected void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbitem.Enabled = true;

        cbType = "cbTypeCategory";
        vInitItemCashout(cbType);
        if (cbroutine.SelectedValue.ToString() == "0")
        {
            //cbroutine.Items.Insert(0, new ListItem("Select", "0"));
            //cbcategory.Items.Insert(0, new ListItem("Select", "0"));
            cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
            cbitem.Items.Insert(0, new ListItem("Select", "0"));
            //cbroutine.SelectedValue = "0";
            //cbcategory.SelectedValue = "0";
            cbitem.SelectedValue = "0";
        }
    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        cbitem.Enabled = true;
        cbcategory.Enabled = true;

        cbType = "cbTypeRoutine";
        vInitItemCashout(cbType);
        if (cbroutine.SelectedValue.ToString() == "NR")
        {
            cbcategory_SelectedIndexChanged(sender, e);

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@doc_typ", "cashout"));
            arr.Add(new cArrayList("@level_no", "1"));
            bll.vBindingComboToSp(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
            cbapproval.Items.Insert(0, new ListItem("Select", "0"));
            cbapproval.CssClass = cd.csstext;
        }
        else
        {
            cbapproval.Items.Clear();
            cbapproval.CssClass = cd.csstextro;
            cbcategory_SelectedIndexChanged(sender, e);
        }
        if (cbroutine.SelectedValue.ToString() == "0")
        {
            //cbroutine.Items.Insert(0, new ListItem("Select", "0"));            
            cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
            cbcategory.Items.Insert(0, new ListItem("Select", "0"));
            //cbitem.Items.Insert(0, new ListItem("Select", "0"));
            //cbroutine.SelectedValue = "0";
            cbcategory.SelectedValue = "0";
            //cbitem.SelectedValue = "0";
        }
    }
    protected void cbitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbitem.SelectedValue.ToString() != "")
        {
            txamt.Enabled = true;
            txamt.Text = "0.00";
            lbvat.Text = "0.00";
            chvat.SelectedValue = "NONVAT";
        }
        else
        {
            txamt.Enabled = false;
        }
        txamt.Text = "0.00";
        txtTotal.Text = "0.00";
        ckmunicipality.Checked = false;
        ckmunicipality_CheckedChanged(sender, e);
    }
    protected void btnShowData_Click(object sender, EventArgs e)
    {


        if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
        {
            if (ddlEmp.SelectedValue.ToString() == "0")
            {
                grd.Visible = false;
                tbl.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected!','Please Select Employee!!','warning');", true);
                return;
            }
            //if (ddlpettycashRefno.SelectedValue.ToString() == "" || ddlpettycashRefno.SelectedValue.ToString() == "0")
            //{
            //    grd.Visible = false;
            //    tbl.Visible = false;
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pettycash Refno is not Selected!','Please Select Pettycash Refno!!','warning');", true);
            //    return;
            //}
            tbl.Visible = true;
            grd.Visible = true;

            List<cArrayList> arr = new List<cArrayList>();
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@PettycashRefno", null));
            if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
            {
                arr.Add(new cArrayList("@PettycashRefno", null));
            }
            else
            {
                arr.Add(new cArrayList("@PettycashRefno", '0'));
            }
            //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd, "sp_temployee_advancedEmp_get4", arr);
        }
        else
        {
            if (ddlEmp.SelectedValue.ToString() == "0")
            {
                grd1.Visible = false;
                tbl1.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected!','Please Select Employee!!','warning');", true);
                return;
            }


            tbl1.Visible = true;
            grd1.Visible = true;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@PettycashRefno", null));
            arr.Add(new cArrayList("@PettycashRefno", '0'));
            //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
            bll.vBindingGridToSp(ref grd1, "sp_temployee_advancedEmp_get4", arr);
        }


        List<cArrayList> arr2 = new List<cArrayList>();
        arr2.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
        //arr2.Add(new cArrayList("@PettycashRefno", null));
        //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
        bll.vGetPettyCashEmp(ref rs, arr2);
        while (rs.Read())
        {
            lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
        }
        rs.Close();
        lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where pc_sta_id='N' and emp_cd='" + ddlEmp.SelectedValue.ToString() + "' ");
        if (lbbalanceNYA.Text == "")
        {
            lbbalanceNYA.Text = "0.00";
        }
        lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));


        SqlDataReader rdr2 = null;
        //SqlConnection con2 = null;
        SqlCommand cmd2 = null;
        //con2 = new SqlConnection(ConfigurationManager
        //.ConnectionStrings["connstr"].ConnectionString);

        //con2.Open();

        string CommandText2 = "exec sp_temployee_advancedEmp_get2 '" + ddlEmp.SelectedValue.ToString() + "', null; ";
        cmd2 = new SqlCommand(CommandText2);
        //cmd2.Connection = con2;
        cmd2.Connection = cd.getConnection();

        rdr2 = cmd2.ExecuteReader();

        while (rdr2.Read())
        {
            //lbbalancerefno.Text = rdr2["totalBalance"].ToString();
        }
        //con2.Close();

        cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
        cbitem.Items.Insert(0, new ListItem("Select", "0"));

    }




    protected void btsave_Click(object sender, EventArgs e)
    {
        if (txamt.Text == "" || lbvat.Text == "" || txamtReturned.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount Spent or Excess Amount Returned should not be blank!','Please input the amount!!','warning');", true);
        }
        else
        {

            Double amountToBeSaved = 0;
            //Double amtBalance = Convert.ToDouble(lbbalancerefno.Text.ToString());
            Double amtBalance = Convert.ToDouble(lbbalance.Text.ToString());
            Double amtBalanceNYA = Convert.ToDouble(lbbalanceNYA.Text.ToString());
            Double amtBalanceAVL = Convert.ToDouble(lbbalanceAVL.Text.ToString());

            if (ddlclaim.SelectedValue.ToString() == "No")
            {
                Double amount = 0;
                Double tax = 0;
                Double amountReturned = 0;
                amount = Convert.ToDouble(txamt.Text.ToString());
                tax = Convert.ToDouble(lbvat.Text.ToString());
                amountReturned = Convert.ToDouble(txamtReturned.Text.ToString());
                amountToBeSaved = amount + tax + amountReturned;
            }
            else if (ddlclaim.SelectedValue.ToString() == "Handover")
            {
                Double amtToHandover = Convert.ToDouble(txamttohandover.Text.ToString());
                amountToBeSaved = amtToHandover;
            }

            if (amountToBeSaved < 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount to be proccessed is Zero or Less!','Please input the correct amount!!','warning');", true);
                return;
            }

            if (amountToBeSaved > amtBalance - amtBalanceNYA)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount to be proccessed is greater than available Balance!','Please reduce the amount at least as same as the balance!!','warning');", true);
                return;
            }

            if (ddlclaim.SelectedValue.ToString() == "No")
            {
                double dAmt = 0;
                double dVat = 0; double dVatCut = 0; string sFileName = string.Empty;


                if (ddlEmp.SelectedValue.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected!','Please Select Employee!!','warning');", true);
                    return;
                }
                //if (ddlpettycashRefno.SelectedValue.ToString() == "" || ddlpettycashRefno.SelectedValue.ToString() == "0")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pettycash Refno is not Selected!','Please Select Pettycash Refno!!','warning');", true);
                //    return;
                //}
                if (!double.TryParse(txamt.Text, out dAmt))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amount can not be empty!','Amount Cashout','warning');", true);
                    return;
                }

                double dBal = double.Parse(lbbalance.Text);
                double dAmount = double.Parse(txamt.Text);
                if ((dBal < 1) || (dBal < dAmount))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Balance is low!','Can not be used to pay','warning');", true);
                    return;
                }

                if (cbitem.SelectedValue.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item cashout must be select!','Please Select Item Cashout','warning');", true);
                    return;
                }

                if (chvat.SelectedValue == "VAT")
                {
                    if (hdtax.Value.ToString() == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Supplier VAT must be entered!','Select supplier with VAT Number','warning');", true);
                        return;
                    }

                }

                if (txmanualno.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual number can not be empty!','Please Insert Manual no','warning');", true);
                    return;
                }
                if (cbroutine.SelectedValue.ToString() == "NR")
                {

                    if (fucashout.HasFile == false)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File can not be empty!','Please Upload File','warning');", true);
                        return;
                    }
                }

                if (txpic.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC can not be empty!','Please Select PIC','warning');", true);
                    return;
                }
                bool isMunicipality = false;

                DataTable dt = new DataTable();
                List<cArrayList> arrMunicipality = new List<cArrayList>();
                arrMunicipality.Add(new cArrayList("@hiddendata", false));
                arrMunicipality.Add(new cArrayList("@fld_nm", "municipality_itemco_cd"));
                dt = cdl.GetValueFromSP("sp_tfield_value_get", arrMunicipality);

                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr["fld_valu"]) == Convert.ToString(cbitem.SelectedValue))
                    {
                        isMunicipality = true;
                    }
                }

                // we can't  force to apply Municipality tax
                //if (isMunicipality == true && ckmunicipality.Checked == false)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select municipality tax!','Municipality tax','warning');", true);
                //    return;
                //}
                string sDoc_no = string.Empty;

                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@transactionDate", DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));

                if (chvat.SelectedValue == "VAT")
                {
                    dVatCut = Convert.ToDouble(bll.sGetControlParameter("vat"));
                    dVat = Convert.ToDouble(lbvat.Text);
                    dAmt = Convert.ToDouble(txamt.Text);
                    arr.Add(new cArrayList("@credit", dAmt));
                    arr.Add(new cArrayList("@vat_amt", dVat));
                    arr.Add(new cArrayList("@isVat", "Yes"));
                }
                else
                {
                    arr.Add(new cArrayList("@credit", dAmt));
                    arr.Add(new cArrayList("@vat_amt", 0));
                    arr.Add(new cArrayList("@isVat", "No"));
                }
                if (cbroutine.SelectedValue.ToString() == "NR")
                {
                    arr.Add(new cArrayList("@pc_sta_id", "N"));
                }
                else if (cbroutine.SelectedValue.ToString() == "R")
                {
                    arr.Add(new cArrayList("@pc_sta_id", "A"));
                }

                arr.Add(new cArrayList("@debit", 0));

                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
                arr.Add(new cArrayList("@manualno", txmanualno.Text));
                arr.Add(new cArrayList("@doc_typ", "PettycashCashout"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@PettycashRefno", null));
                arr.Add(new cArrayList("@cashout_cd", null));
                arr.Add(new cArrayList("@pic_cd", hdpic.Value));
                arr.Add(new cArrayList("@amt_returned", txamtReturned.Text.ToString()));
                arr.Add(new cArrayList("@claim_cashout", ddlClaimCashout.SelectedValue.ToString()));
                arr.Add(new cArrayList("@tax_no_supp", hdtax.Value.ToString()));

                //arr.Add(new cArrayList("@approval_cd", cbapproval.SelectedValue.ToString()));
                arr.Add(new cArrayList("@remark", txremark2.Text));

                bll.vInsertPettycashCashoutRequest(arr, ref sDoc_no);


                if (cbroutine.SelectedValue.ToString() == "NR")
                {
                    if ((fucashout.FileName != "") || (fucashout.FileName.Equals(null)))
                    {
                        FileInfo fi = new FileInfo(fucashout.FileName);
                        string ext = fi.Extension;
                        byte[] fs = fucashout.FileBytes;
                        if (fs.Length <= 500000)
                        {

                            if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                            {

                                arr.Clear();
                                sFileName = sDoc_no + ext;
                                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@doc_no", sDoc_no));
                                arr.Add(new cArrayList("@uploadedfile", sDoc_no + ext));
                                bll.vUpdatePettycashCashoutRequest(arr);
                                fucashout.SaveAs(bll.sGetControlParameter("image_path") + sFileName);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                            return;
                        }
                    }

                }
                else
                {
                    if ((fucashout.FileName != "") || (fucashout.FileName.Equals(null)))
                    {
                        FileInfo fi = new FileInfo(fucashout.FileName);
                        string ext = fi.Extension;
                        byte[] fs = fucashout.FileBytes;
                        if (fs.Length <= 500000)
                        {

                            if (ext == ".jpg" || ext == ".jpeg" || ext == ".bmp" || ext == ".gif" || ext == ".png" || ext == ".JPEG" || ext == ".JPG" || ext == ".BMP" || ext == ".GIF" || ext == ".PNG")
                            {

                                arr.Clear();
                                sFileName = sDoc_no + ext;
                                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                                arr.Add(new cArrayList("@doc_no", sDoc_no));
                                arr.Add(new cArrayList("@uploadedfile", sDoc_no + ext));
                                bll.vUpdatePettycashCashoutRequest(arr);
                                fucashout.SaveAs(bll.sGetControlParameter("image_path") + sFileName);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('You should upload image','jpg,bmp,gif and png upload document again');", true);
                                return;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al1", "sweetAlert('Image size too big','Image size must be less than 500KB');", true);
                            return;
                        }

                    }
                }


                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Pettycash Cash Out has been created!','" + sDoc_no + "','success');", true);
                btnShowData_Click(sender, e);
                if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
                {
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                    //arr1.Add(new cArrayList("@PettycashRefno", null));
                    //arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                    bll.vGetPettyCashEmp(ref rs, arr1);
                    while (rs.Read())
                    {
                        lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);

                    }
                    rs.Close();
                    lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where emp_cd='"+ ddlEmp.SelectedValue.ToString() + "' ");
                    if (lbbalanceNYA.Text == "")
                    {
                        lbbalanceNYA.Text = "0.00";
                    }
                    lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));
                }
                else
                {
                    List<cArrayList> arr1 = new List<cArrayList>();
                    arr1.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                    //arr1.Add(new cArrayList("@PettycashRefno", null));
                    //arr1.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                    bll.vGetPettyCashEmp(ref rs, arr1);
                    while (rs.Read())
                    {
                        lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
                    }

                    rs.Close();
                    lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where pc_sta_id='N' and emp_cd='" + ddlEmp.SelectedValue.ToString() + "' ");
                    if (lbbalanceNYA.Text == "")
                    {
                        lbbalanceNYA.Text = "0.00";
                    }
                    lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));

                    btsave.Visible = false;

                }

                if (cbroutine.SelectedValue.ToString() == "NR")
                {
                    int nrnd = 0;
                    Random rnd = new Random();
                    nrnd = rnd.Next(1000, 9999);
                    string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
                    List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());
                    string sMsg = "#Pettycash Cashout request from " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + ", no." + sDoc_no + ", amt  " + txamt.Text +
                         ", for " + cbitem.SelectedItem.Text + ", do you want to approved : (Y/N)" + stoken;
                    arr.Clear();
                    arr.Add(new cArrayList("@token", stoken));
                    arr.Add(new cArrayList("@doc_no", sDoc_no));
                    arr.Add(new cArrayList("@doc_typ", "PettycashCashout"));
                    arr.Add(new cArrayList("@to", lapproval[0]));
                    arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                    bll.vInsertSmsOutbox(arr);

                    // Sending Email 
                    string sSubject = ""; string sMessage = "";
                    string sfile_attachment = sFileName;
                    //string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
                    string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                    //string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + hdpic.Value + "')");
                    string cashout_typ = cbcategory.SelectedValue.ToString();
                    string scashout_typ = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu='" + cashout_typ + "'");
                    string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
                    string screator_id = Request.Cookies["usr_id"].Value.ToString();
                    string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + screator_id + "'");
                    string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + hdpic.Value + "'");
                    string spc_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + ddlEmp.SelectedValue.ToString() + "'");
                    sSubject = "#New Pettycash Cash out Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";
                    //sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
                    sMessage = "<table><tr><td colspan=3>#Pettycash Cash Out Request Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + sDoc_no + "</td><td>PIC</td><td>:</td><td>" + spic_nm + "</td></tr><tr><td>Pc Employee</td><td>:</td><td>" + spc_nm + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><tr><td>Trans Type</td><td>:</td><td>" + scashout_typ + "</td><td>Item</td><td>:</td><td>" + cbitem.SelectedItem.Text + "</td></tr><tr><td><font color='#FF0000'>Req Amount / VAT</font></td><td>:</td><td><font color='#FF0000'>" + txamt.Text + "/" + lbvat.Text + "</font></td></tr><tr><td></td></tr><tr><td>Note</td><td>:</td><td colspan=4>1. See Attached file </td></tr><tr><td></td><td></td><td colspan=4>2. This Approval to make sure all detail as attached is match with entry request.</td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
                    "<p> Please Click this  for approved : <a href='" + slink_branch + "/landingpage2.aspx?src=PettycashCashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sDoc_no + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage2.aspx?src=PettycashCashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sDoc_no + "&sta=R'>Reject</a></p>" +
                    " \n\r\n\r\n\r\n Wazaran Admin";
                    //bll.vSendMail(lapproval[1], sSubject,sMessage, "CO20409172345.gif");
                    arr.Clear();
                    arr.Add(new cArrayList("@trxcd", "PettycashCashout"));
                    arr.Add(new cArrayList("@token", nrnd.ToString()));
                    arr.Add(new cArrayList("@doc_no", sDoc_no));
                    bll.vInsertEmailSent(arr);
                    arr.Clear();
                    arr.Add(new cArrayList("@token", nrnd.ToString()));
                    arr.Add(new cArrayList("@doc_typ", "PettycashCashout"));
                    arr.Add(new cArrayList("@to", lapproval[1]));
                    arr.Add(new cArrayList("@doc_no", sDoc_no));
                    arr.Add(new cArrayList("@emailsubject", sSubject));
                    arr.Add(new cArrayList("@msg", sMessage));
                    arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                    bll.vInsertEmailOutbox(arr);
                    //************************ END OF ENTRY
                }
            }


            else if (ddlclaim.SelectedValue.ToString() == "Handover")
            {
                if (ddlEmp.SelectedValue.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected!','Please Select Employee!!','warning');", true);
                    return;
                }
                else if (ddlHandovertoemp.SelectedValue.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee To Handover is not Selected!','Please Select Employee To Handover!!','warning');", true);
                    return;
                }
                else if (txamttohandover.Text.ToString() == "0")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Amount To Handover is empty!','Please fill Amount To Handover !!','warning');", true);
                    return;
                }

                string sDoc_no = string.Empty;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@PettycashRefno", null));
                arr.Add(new cArrayList("@transactionDate", DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@emp_hov_cd", ddlHandovertoemp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@amount_hov", Convert.ToDouble(txamttohandover.Text.ToString())));
                //arr.Add(new cArrayList("@remark", txremark.Text.ToString()));
                arr.Add(new cArrayList("@remark", null));
                arr.Add(new cArrayList("@pchov_sta_id", "N"));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                bll.vInsertPettycashCashoutHandover(arr, ref sDoc_no);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Cash Advance Handover has been created!','" + sDoc_no + "','success');", true);
                btnShowData_Click(sender, e);
            }
            ddlclaim.Enabled = false;
            ddlEmp.Enabled = false;
            cbroutine.Enabled = false;
            cbcategory.Enabled = false;
            cbitem.Enabled = false;
            cbapproval.Enabled = false;
            txpic.Enabled = false;
            txmanualno.Enabled = false;
            dtcashout.Enabled = false;
            txamt.Enabled = false;
            chvat.Enabled = false;
            txamtReturned.Enabled = false;
            ddlClaimCashout.Enabled = false;

        }
    }

    protected void New_Click(object sender, EventArgs e)
    {
        btsave.Enabled = true;
        Response.Redirect("fm_EmpPettycashCashout.aspx");
    }


    protected void chvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        double dAmt = 0;
        txamt.Enabled = false;
        if (chvat.SelectedValue == "VAT")
        {

            if (!double.TryParse(txamt.Text, out dAmt))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry amount!','Must be numeric','warning');", true);
                chvat.SelectedValue = "NONVAT";
                return;
            }
            else
            {
                double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
                double dValue = (1 / (1 + dPctVat)) * dAmt;
                double dVat = dAmt - dValue;
                //double dVat = dPctVat * dAmt;
                txamt.Text = Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString(); // String.Format("{0:0.00}", dValue);
                lbvat.Text = Math.Round(dVat, 2, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);
            }


            if (chvat.SelectedValue == "VAT")
            {
                //   txsuppliername.CssClass = "form-control";
                txtaxno.CssClass = cd.csstext;
                txtaxno.Text = "";
                //  txsuppliername.Text = "";
            }
            else
            {
                //   txsuppliername.CssClass = "form-control ro" ;
                txtaxno.CssClass = cd.csstextro;
                txtaxno.Text = "";
                //   txsuppliername.Text = "";
            }
        }
        else if (chvat.SelectedValue == "NONVAT")
        {

            if (txamt.Text == "")
            {
                txamt.Text = "0.00";
                lbvat.Text = "0.00";
            }
            else
            {
                double dVat = Convert.ToDouble(lbvat.Text);
                dAmt = Convert.ToDouble(txamt.Text);
                lbvat.Text = "0.00";
                txamt.Text = (dAmt + dVat).ToString();
            }
        }
        ckmunicipality_CheckedChanged(sender, e);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lEmployee = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sEmployee = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sEmployee = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_cd"].ToString() + "-" + rs["emp_nm"], rs["emp_cd"].ToString());
            lEmployee.Add(sEmployee);
        }
        return (lEmployee.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList2(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lsupplier = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sTax = string.Empty;
        arr.Add(new cArrayList("@suppliertax_no", prefixText));
        bll.vSearchAccSupplier(ref rs, arr);
        while (rs.Read())
        {
            sTax = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["supplier_desc"].ToString(), rs["supplier_cd"].ToString());
            lsupplier.Add(sTax);
        }
        return (lsupplier.ToArray());
    }


    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lbtotamt = (Label)e.Row.FindControl("lbtotAmount");
                Label lbtotlabel = (Label)e.Row.FindControl("lbtotlabel");
                Label lbtotdebit = (Label)e.Row.FindControl("lbtotdebit");
                Label lbtotcredit = (Label)e.Row.FindControl("lbtotcredit");
                Label lbballabel = (Label)e.Row.FindControl("lbballabel");
                Label lbbalance = (Label)e.Row.FindControl("lbbalance");
                Label lbtotamtreturned = (Label)e.Row.FindControl("lbtotamtreturned");

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@PettycashRefno", null));
                arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                bll.vGetPettyCashEmpTotalBal(ref rs, arr);
                while (rs.Read())
                {
                    //lbtotamt.Text = rs["totalBalance"].ToString();
                    lbtotlabel.Text = "Total";
                    lbtotdebit.Text = rs["totdebitX"].ToString();
                    lbtotcredit.Text = rs["totcreditX"].ToString();
                    lbballabel.Text = "Balance";
                    lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
                }
                lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where pc_sta_id='N' and emp_cd='" + ddlEmp.SelectedValue.ToString() + "' ");
                if (lbbalanceNYA.Text == "")
                {
                    lbbalanceNYA.Text = "0.00";
                }
                lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));

                rs.Close();
            }

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grd.ClientID + "', 220, 1263 , 57 ,false); </script>", false);
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lbtotamt = (Label)e.Row.FindControl("lbtotAmount");
                Label lbtotlabel = (Label)e.Row.FindControl("lbtotlabel");
                Label lbtotdebit = (Label)e.Row.FindControl("lbtotdebit");
                Label lbtotcredit = (Label)e.Row.FindControl("lbtotcredit");
                Label lbballabel = (Label)e.Row.FindControl("lbballabel");
                Label lbbalance = (Label)e.Row.FindControl("lbbalance");

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@emp_cd", ddlEmp.SelectedValue.ToString()));
                arr.Add(new cArrayList("@PettycashRefno", null));
                arr.Add(new cArrayList("@claim", ddlclaim.SelectedValue.ToString()));
                bll.vGetPettyCashEmpTotalBal(ref rs, arr);
                while (rs.Read())
                {
                    //lbtotamt.Text = rs["totalBalance"].ToString();
                    lbtotlabel.Text = "Total";
                    lbtotdebit.Text = rs["totdebitX"].ToString();
                    lbtotcredit.Text = rs["totcreditX"].ToString();
                    lbballabel.Text = "Balance";
                    lbbalance.Text = String.Format("{0:0.00}", rs["totalBalance"]);
                }

                rs.Close();
                lbbalanceNYA.Text = bll.vLookUp("select sum(isnull(credit,0)+isnull(vat_amt,0)+isnull(amt_returned,0))balanceNYA from temployee_advanced_cash where pc_sta_id='N' and emp_cd='" + ddlEmp.SelectedValue.ToString() + "' ");
                if (lbbalanceNYA.Text == "")
                {
                    lbbalanceNYA.Text = "0.00";
                }
                lbbalanceAVL.Text = String.Format("{0:0.00}",Convert.ToDouble(lbbalance.Text.ToString()) - Convert.ToDouble(lbbalanceNYA.Text.ToString()));
            }

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader1('" + grd1.ClientID + "', 220, 1263 , 57 ,false); </script>", false);
        }




    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        if (ddlEmp.SelectedValue.ToString() == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Employee is not Selected!','Please Select Employee!!','warning');", true);
            return;
        }

        if (ddlclaim.SelectedValue.ToString() == "Yes" || ddlclaim.SelectedValue.ToString() == "Handover")
        {
            //if (ddlpettycashRefno.SelectedValue.ToString() == "" || ddlpettycashRefno.SelectedValue.ToString() == "0")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Pettycash Refno is not Selected!','Please Select Pettycash Refno!!','warning');", true);
            //    return;
            //}

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=PettycashCashoutClaimEmp&emp=" + ddlEmp.SelectedValue.ToString() + "&PettycashRefno=" + ddlpettycashRefno.SelectedValue.ToString() + "&claim=" + ddlclaim.SelectedValue.ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=PettycashCashoutClaimEmp&emp=" + ddlEmp.SelectedValue.ToString() + "&PettycashRefno=null&claim=" + ddlclaim.SelectedValue.ToString() + "');", true);
        }


        if (ddlclaim.SelectedValue.ToString() == "No")
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=PettycashCashoutEmp&emp=" + ddlEmp.SelectedValue.ToString() + "&claim=" + ddlclaim.SelectedValue.ToString() + "');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=PettycashCashoutEmp&emp=" + ddlEmp.SelectedValue.ToString() + "');", true);
        }
    }


    protected void btSelectPIC_Click(object sender, EventArgs e)
    {
        string dept_cd = bll.vLookUp("select dept_cd from tmst_employee where emp_cd='" + hdempadv.Value + "'");

        if (dept_cd == "SA")
        {
            dept_cd = "SALES";
        }
        else if (dept_cd == "ACCH")
        {
            dept_cd = "ACC";
        }
        else if (dept_cd == "Merchandiser")
        {
            dept_cd = "MERCH";
        }
        string dept_nm = bll.vLookUp("select dept_nm from tmst_department where dept_cd='" + dept_cd + "'");

        txdept.Text = dept_nm;
        if (txdept.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Dept can not be Empty!','Please Contact Wazaran Admin to update Dept!','warning');", true);
        }
    }
    protected void btStatus_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OpenWindow", "window.open('fm_lookupEmpPettycashCashout.aspx');", true);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader1('" + grd1.ClientID + "', 220, 1263 , 57 ,false); </script>", false);
    }

    protected void ddlHandovertoemp_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void ckmunicipality_CheckedChanged(object sender, EventArgs e)
    {

        lblmunicipality.Text = "0";
        bool isMunicipality = false;

        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@hiddendata", false));
        arr.Add(new cArrayList("@fld_nm", "municipality_itemco_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);

        foreach (DataRow dr in dt.Rows)
        {
            if (Convert.ToString(dr["fld_valu"]) == Convert.ToString(cbitem.SelectedValue))
            {
                isMunicipality = true;
            }
        }
        //// we can't  force to apply Municipality tax
        //if (isMunicipality == true)
        //{
        //    ckmunicipality.Checked = true;
        //}
        if (ckmunicipality.Checked == true && isMunicipality == true)
        {

            decimal municipalityPer = Convert.ToDecimal(bll.sGetControlParameter("municipalityTax"));

            decimal totAmt = Convert.ToDecimal(Convert.ToDecimal(txamt.Text));
            lblmunicipality.Text = Convert.ToString(Convert.ToDecimal(municipalityPer * totAmt) / 100);
        }
        // we can't  force to apply Municipality tax
        //else if (isMunicipality == true && ckmunicipality.Checked == false)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select municipality tax!','Municipality tax','warning');", true);

        //}

        txtTotal.Text = Convert.ToString(Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text));
    }

    protected void txamt_TextChanged(object sender, EventArgs e)
    {
        ckmunicipality_CheckedChanged(sender, e);
    }
}