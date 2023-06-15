using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Web.Services.Protocols;

public partial class fm_cashregout_pettycash : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    cbll2 bll2 = new cbll2();
    //string cbType = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@tax_type", "E"));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvatrate, "sp_tmst_tax_get", "tax_cd", "tax_desc", arr);
            //arr.Clear();
            //arr.Add(new cArrayList("@tax_type", "M"));
            //bll.vBindingComboToSpWithEmptyChoosen(ref cbmunipacility, "sp_tmst_tax_get", "tax_cd", "tax_desc", arr);

            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcashregister, "sp_tcashregister_id_get", "cashier_id", "name", arr);

            bll.vBindingFieldValueToComboWithChoosen(ref cbinout, "inout");
            cbinout.SelectedValue = "";
            cbinout_SelectedIndexChanged(sender, e);
            bll.vBindingFieldValueToComboWithChoosen(ref cbroutine, "routine");
            //cbroutine.SelectedValue = "NR";
            //bll.vBindingFieldValueToComboWithChoosen(ref cbmunipacility, "municipalityTaxByGrade");
            dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
            lbcashier.Text = string.Format("{0:#,0.####}", Convert.ToDouble(bll.vLookUp("select dbo.fn_getcashierbalance_advance('" + cbcashregister.SelectedValue + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
            lbbalanceco.Text = string.Empty;
            bll.vBindingFieldValueToComboWithChoosen(ref cbcategory, "cashout_typ");
            if (cbinout.SelectedValue == "I")
            {
                //cbcategory.SelectedValue = "ADV";
            }
            cd.v_disablecontrol(cbitem);
            cbcategory_SelectedIndexChanged(sender, e);
            bll.vBindingComboToSpWithEmptyChoosen(ref cbdept, "sp_tmst_department_get", "dept_cd", "dept_nm");
            bll.vBindingComboToSpWithEmptyChoosen(ref cbvatrate, "sp_tmst_tax_get", "tax_cd", "tax_desc");
            //chvat_SelectedIndexChanged(sender, e);
            //cd.v_disablecontrol(cbmunipacility);
            cd.v_disablecontrol(cbdept);
            cd.v_enablecontrol(txamt);
            cd.v_disablecontrol(cbcategory);
            cd.v_disablecontrol(cbapproval);
            cd.v_disablecontrol(cbdept);

            cd.v_disablecontrol(cbvatrate);
            cd.v_disablecontrol(cbroutine);
            cd.v_disablecontrol(dtcashout);
            cd.v_disablecontrol(txmanualno);
            cd.v_disablecontrol(txremark);
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbdept);
            //cd.v_disablecontrol(cbacctdept);
            //cd.v_disablecontrol(cbremark);
            cd.v_disablecontrol(txamt);
            //cd.v_disablecontrol(chvat);
            cd.v_disablecontrol(txsysno);
            cd.v_disablecontrol(txtaxno);
            //cd.v_disablecontrol(chismcp);
            //cd.v_disablecontrol(lblmunicipality);
            cd.v_disablecontrol(lbvat);
            cd.v_hiddencontrol(btsave);
            cd.v_hiddencontrol(btprint);
            cd.v_disablecontrol(cbinout);
            cd.v_disablecontrol(cbitem);
            //grdattr.DataSource = null;
            //grdattr.DataBind();
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWRKCashoutAttribute_advance(arr);
        }

        if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue.ToString() == "O")
        {
            lbCashoutInfo.InnerText = "Petty Cash Received By";
            lbPIC.InnerText = "Petty Cash Issued By";
        }
        else if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue.ToString() == "I")
        {
            lbCashoutInfo.InnerText = "Petty Cash Issued By";
            lbPIC.InnerText = "Petty Cash Received By";
        }
        else
        {
            lbCashoutInfo.InnerText = "Petty Cashout Info";
            lbPIC.InnerText = "PIC";
        }

        if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
        {
            EmployeeAdvanceOutNote.Visible = true;
        }
        else
        {
            EmployeeAdvanceOutNote.Visible = false;
        }
        if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
        {
            EmployeeAdvanceInNote.Visible = true;
        }
        else
        {
            EmployeeAdvanceInNote.Visible = false;
        }
        //if (cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_WASH'")) || cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'")))  // by yanto 18-01-2020
        //{
        //    DivPlate.Visible = true;
        //    if (hdemp.Value != "")
        //    {
        //        if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'"))
        //        {
        //            lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
        //                                       "  from tcashout_attribute" +
        //                                       "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                       "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                       //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
        //                                       "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'") + "' )" +
        //                                       "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
        //                                       "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
        //                                       "  order by tcashout_request.cashout_dt desc");
        //        }
        //        else
        //        {
        //            lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
        //                                    "  from tcashout_attribute" +
        //                                    "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                    "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                    //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
        //                                    "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
        //                                    "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
        //                                    "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
        //                                    "  order by tcashout_request.cashout_dt desc");
        //        }
        //        cbremark_SelectedIndexChanged(sender, e);
        //    }
        //}
        //else
        //{
        //    DivPlate.Visible = false;
        //}
    }
    protected void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //cbType = "cbTypeCategory";
        //vInitItemCashout(cbType);
        //if (cbcategory.SelectedValue == "ADV")
        //{
        //    ClaimCashout.Visible = true;
        //    txpic.Enabled = false;
        //    cbdept.Enabled = false;
        //    txpic.Text = Request.Cookies["usr_id"].Value.ToString() + '-' + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
        //    hdemp.Value = Request.Cookies["usr_id"].Value.ToString();
        //}
        //else
        //{
        //    ClaimCashout.Visible = false;
        //    txpic.Enabled = true;
        //    cbdept.Enabled = true;
        //    txpic.Text = "";
        //}

        //if ((cbcategory != null && cbcategory.SelectedValue != string.Empty) && cbcategory.SelectedValue != "EMPLOYEE" && cbcategory.SelectedValue != "DISTRIBUTION" && cbroutine.SelectedValue == "NR")
        //{
        //    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
        //}

        //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
        //{
        //    EmployeeAdvanceOutNote.Visible = true;
        //}
        //else
        //{
        //    EmployeeAdvanceOutNote.Visible = false;
        //}
        //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
        //{
        //    EmployeeAdvanceInNote.Visible = true;
        //}
        //else
        //{
        //    EmployeeAdvanceInNote.Visible = false;
        //}
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue));
        if (cbcategory.SelectedValue != "" && cbroutine.SelectedValue != "" && cbinout.SelectedValue != "")
            bll.vBindingComboToSpWithEmptyChoosen(ref cbitem, "sp_tmst_itemcashout_get1", "itemco_cd", "itemco_nm", arr);

        if (cbinout.SelectedValue == "I" && cbcategory.SelectedValue == "ADV")
        {
            cbitem.Items.Remove(cbitem.Items.FindByValue("CHA00080"));
            //cd.v_disablecontrol(cbremark);
            cd.v_disablecontrol(cbvatrate);
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbdept);
            cd.v_disablecontrol(txtaxno);
        }
        cd.v_enablecontrol(cbitem);
        cd.v_disablecontrol(cbcategory);
    }

    //void vInitItemCashout()
    void vInitItemCashout(string cbType)
    {
        if (cbType != "cbTypeCategory")
        {
            bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);

            if (cbinout.SelectedValue == "I")
            {
                //cbcategory.SelectedValue = "ADV";
                //cd.v_disablecontrol(cbremark);
                cd.v_disablecontrol(cbvatrate);
                cd.v_disablecontrol(txpic);
                cd.v_disablecontrol(cbdept);
                cd.v_disablecontrol(txtaxno);
            }

            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            string CommandText;
            if (cbroutine == null)
            {
                CommandText = "select * from tfield_value where fld_nm='cashout_typ' and fld_valu not in(select cashout_typ from tmst_itemcashout where inout='" + cbinout.SelectedValue.ToString() + "') ";
            }
            else
            {
                CommandText = "select * from tfield_value where fld_nm='cashout_typ' and fld_valu not in(select cashout_typ from tmst_itemcashout where routine='" + cbroutine.SelectedValue.ToString() + "' and inout='" + cbinout.SelectedValue.ToString() + "') ";
            }
            cmd = new SqlCommand(CommandText);
            cmd.Connection = cd.getConnection();
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                string itemToRmv = rdr["fld_valu"].ToString();
                cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
            }

            if (cbcategory.SelectedValue.ToString() == "ADV" || cbinout.SelectedValue.ToString() == "H")
            {
                lbCashoutInfo.InnerText = "Cash Received By";
                lbPIC.InnerText = "Cash Issued By";
            }
            else
            {
                lbCashoutInfo.InnerText = "Cashout Info";
                lbPIC.InnerText = "PIC";
            }
        }


        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
        bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get1", "itemco_cd", "itemco_nm", arr);
        cbitem_SelectedIndexChanged(this, EventArgs.Empty);

        if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
        {
            EmployeeAdvanceOutNote.Visible = true;
        }
        else
        {
            EmployeeAdvanceOutNote.Visible = false;
        }
        if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
        {
            EmployeeAdvanceInNote.Visible = true;
        }
        else
        {
            EmployeeAdvanceInNote.Visible = false;
        }
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
    public static string[] GetCompletionList3(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        arr.Add(new cArrayList("@cust_cd", prefixText));
        bll.vSearchMstCustomer(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"], rs["cust_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListSup(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@suppliertax_no", prefixText));
        bll.vSearchAccMstSupplier2(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["suppliertax_no"].ToString() + "-" + rs["supplier_nm"], rs["supplier_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListItemcashoutpurpose(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lItemcashoutpurpose = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sItemcashoutpurpose = string.Empty;
        arr.Add(new cArrayList("@inout", "O"));
        arr.Add(new cArrayList("@itemcashout_lookup", prefixText));
        bll.vSearchItemCashoutPurpose(arr, ref rs);
        while (rs.Read())
        {
            sItemcashoutpurpose = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["itemco_desc"].ToString(), rs["itemco_cd"].ToString());
            lItemcashoutpurpose.Add(sItemcashoutpurpose);
        }
        return (lItemcashoutpurpose.ToArray());
    }

    void vInitGrid()
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        bll.vBindingGridToSp(ref grdattr, "sp_twrk_cashoutattribute_advance_get", arr);
    }
    protected void cbitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        List<cArrayList> arrMunicipality = new List<cArrayList>();
        arrMunicipality.Add(new cArrayList("@hiddendata", false));
        arrMunicipality.Add(new cArrayList("@fld_nm", "municipality_itemco_cd"));
        dt = cdl.GetValueFromSP("sp_tfield_value_get", arrMunicipality);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vDelWRKCashoutAttribute_advance(arr);
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        bll.vInsertWRKCashoutAttribute_advance(arr);
        vInitGrid();
        //arr.Clear();
        //arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue));
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbsubitem, "sp_titemcashout_remark_get", "remark", "remark", arr);
        arr.Clear();
        arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingComboToSp(ref cbremark, "sp_titemcashout_remark_get", "sequenceno", "remark", arr);
        //cd.v_enablecontrol(cbremark);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

        //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
        //{
        //    EmployeeAdvanceOutNote.Visible = true;
        //}
        //else
        //{
        //    EmployeeAdvanceOutNote.Visible = false;
        //}
        //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
        //{
        //    EmployeeAdvanceInNote.Visible = true;
        //}
        //else
        //{
        //    EmployeeAdvanceInNote.Visible = false;
        //}
        txamt.Text = "0.00";
        txbasedprice.Text = "0.00";
        //ddlMunicipality_SelectedIndexChanged(sender, e);

        //if (cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_WASH'")) || cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'")))  // by yanto 18-01-2020
        //{
        //    DivPlate.Visible = true;
        //    if (hdemp.Value != "")
        //    {
        //        if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'"))
        //        {
        //            lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
        //                                       "  from tcashout_attribute" +
        //                                       "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                       "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                       //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
        //                                       "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'") + "' )" +
        //                                       "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
        //                                       "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
        //                                       "  order by tcashout_request.cashout_dt desc");
        //        }
        //        else
        //        {
        //            lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
        //                                    "  from tcashout_attribute" +
        //                                    "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                    "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
        //                                    //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
        //                                    "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
        //                                    "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
        //                                    "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
        //                                    "  order by tcashout_request.cashout_dt desc");
        //        }
        //        cbremark_SelectedIndexChanged(sender, e);
        //    }
        //}
        //else
        //{
        //    DivPlate.Visible = false;
        //}
        //arr.Clear();
        //arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        //bll.vBindingComboToSpWithEmptyChoosen(ref cbacctdept, "sp_tacc_mst_department_get", "dept_cd", "dept_nm", arr);
        //int cnt = Convert.ToInt32(bll.vLookUp("select count(*) from titemcashout_department where itemco_cd='" + cbitem.SelectedValue + "'"));
        //if (cnt > 0)
        //{
        //    cd.v_disablecontrol(cbacctdept);
        //    cbacctdept.SelectedValue = bll.vLookUp("select  top(1) dept_cd from titemcashout_department where itemco_cd='" + cbitem.SelectedValue + "'");
        //}
        //else
        //{
        //    cd.v_enablecontrol(cbacctdept);
        //}
        cd.v_enablecontrol(txremark);
        cd.v_enablecontrol(txmanualno);
        cd.v_enablecontrol(txamt);
        cd.v_disablecontrol(cbvatrate);
        cd.v_enablecontrol(txpic);
        cd.v_enablecontrol(cbdept);
        cd.v_enablecontrol(cbvatrate);
        //cd.v_enablecontrol(cbremark);
        cd.v_enablecontrol(txmanualno);
        cd.v_disablecontrol(cbitem);
        //cd.v_enablecontrol(cbacctdept);
        cd.v_enablecontrol(cbcategory);
        if (cbinout.SelectedValue == "I")
        {
            //cd.v_disablecontrol(cbremark);
            
            cd.v_disablecontrol(cbvatrate);
            cbvatrate.SelectedValue = bll.sGetControlParameter("novat");
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbdept);
            cd.v_disablecontrol(txtaxno);
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@inout", cbinout.SelectedValue));
        arr.Add(new cArrayList("@routine", cbroutine.SelectedValue));
        bll.vBindingComboToSpWithEmptyChoosen(ref cbcategory, "sp_tfield_value_getbycashout", "fld_valu", "fld_desc", arr);
        cbcategory_SelectedIndexChanged(sender, e);
        if (cbinout.SelectedValue == "I")
        {
            //cbcategory.SelectedValue = "ADV";
            //cd.v_disablecontrol(cbremark);
            cd.v_disablecontrol(cbvatrate);
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbdept);
            cd.v_disablecontrol(txtaxno);
        }
        //cbcategory_SelectedIndexChanged(sender, e);
        if (cbroutine.SelectedValue.ToString() == "NR")
        {
            if (cbinout.SelectedValue.ToString() == "O")
            {
                arr.Clear();
                arr.Add(new cArrayList("@doc_typ", "cashout"));
                arr.Add(new cArrayList("@level_no", "1"));
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSpWithEmptyChoosen(ref cbapproval, "sp_tapprovalpattern_get", "emp_cd", "emp_nm", arr);
                cd.v_enablecontrol(cbapproval);
            }
            else
            {
                cbapproval.Items.Clear();
                cd.v_disablecontrol(cbapproval);
            }

            //if (cbcategory != null && cbinout.SelectedValue.ToString() != "H")
            //{
            //    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
            //}

            //if (cbinout.SelectedValue.ToString() == "H")
            //{
            //    cbcategory.Items.Insert(0, new ListItem("ADVANCED", "ADV"));
            //    cbitem.Items.Insert(0, new ListItem("EMPLOYEE ADVANCE", bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'")));
            //    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
            //    //yellowLine1.Style.Add("background-color", "#ADFF2F");
            //    //yellowLine2.Style.Add("background-color", "#ADFF2F");
            //    //yellowLine3.Style.Add("background-color", "#ADFF2F");
            //}
        }
        else
        {
            cbapproval.Items.Clear();
            cd.v_disablecontrol(cbapproval);
        }

        //else if (cbroutine.SelectedValue.ToString() == "0")
        //{
        //    //cbinout.SelectedValue = "H";

        //    cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
        //    //cbcategory.Items.Insert(0, new ListItem("Select", "0"));

        //    cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
        //    cbitem.Items.Insert(0, new ListItem("Select", "0"));
        //}
        //else
        //{
        //    cbapproval.Items.Clear();
        //    cbapproval.CssClass = cd.csstextro;
        //    cbcategory_SelectedIndexChanged(sender, e);
        //}
        cd.v_enablecontrol(cbcategory);
        //cd.v_disablecontrol(cbroutine);
        if (cbinout.SelectedValue == "I")
        {
            cd.v_disablecontrol(cbroutine);
            cd.v_enablecontrol(cbcategory);
        }
        else if (cbinout.SelectedValue == "O")
        {
            cd.v_enablecontrol(cbroutine);
            cd.v_enablecontrol(cbcategory);
        }
        cd.v_disablecontrol(cbroutine);
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        double dAmt = 0;
        if (!double.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Entry numeric only for amount!','Check amount to be cashout','warning');", true);
            return;
        }

        if (hdemp.Value.ToString() == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select PIC (Employee)!','Person In Charge','warning');", true);
            return;
        }

        List<cArrayList> arr = new List<cArrayList>();
    }
    protected void grdattr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            DropDownList cbvalue = (DropDownList)e.Row.FindControl("cbvalue");
            HiddenField hdids = (HiddenField)e.Row.FindControl("IDS");
            TextBox txvalue = (TextBox)e.Row.FindControl("txvalue");
            TextBox txvalue2 = (TextBox)e.Row.FindControl("txvalue2");
            TextBox txvalue3 = (TextBox)e.Row.FindControl("txvalue3");
            TextBox txvalueemp = (TextBox)e.Row.FindControl("txvalueemp");
            TextBox txlookupitemcashoutpurpose = (TextBox)e.Row.FindControl("txlookupitemcashoutpurpose");
            TextBox txlookupsupplier = (TextBox)e.Row.FindControl("txlookupsupplier");
            CalendarExtender clvalue = (CalendarExtender)e.Row.FindControl("clvalue");
            string sDataType = bll.vLookUp("select datatype from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
            if (sDataType == cd.dttextbox)
            {
                cbvalue.Visible = false;
                txvalue.Visible = false;
                txvalue2.Visible = true;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }
            else if (sDataType == cd.dtcalendar)
            {
                cbvalue.Visible = false;
                txvalue.Visible = true;
                txvalue2.Visible = false;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }
            else if (sDataType == cd.dtdropdownlist)
            {
                cbvalue.Visible = true;
                txvalue.Visible = false;
                txvalue2.Visible = false;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                string sSpName = bll.vLookUp("select spname from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
                string sValue = bll.vLookUp("select idval from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
                string sName = bll.vLookUp("select idname from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
                bll.vBindingComboToSp(ref cbvalue, sSpName, sValue, sName);
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }
            else if (sDataType == cd.dtdropdownlistfieldvalue)
            {
                string sValue = bll.vLookUp("select idval from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
                bll.vBindingFieldValueToCombo(ref cbvalue, sValue);
                txvalue2.Visible = false;
                txvalue.Visible = false;
                cbvalue.Visible = true;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }
            else if (sDataType == cd.dttextboxcustomer)
            {
                txvalue2.Visible = false;
                txvalue.Visible = false;
                cbvalue.Visible = false;
                txvalue3.Visible = true;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }

            else if (sDataType == cd.dttextboxemployee)
            {
                txvalue2.Visible = false;
                txvalue.Visible = false;
                cbvalue.Visible = false;
                txvalue3.Visible = false;
                txvalueemp.Visible = true;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = false;
            }
            else if (sDataType == cd.dtlookupsupplier)
            {
                txvalue2.Visible = false;
                txvalue.Visible = false;
                cbvalue.Visible = false;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = true;
                txlookupitemcashoutpurpose.Visible = false;

            }
            else if (sDataType == cd.dtlookupitemcashout)
            {
                txvalue2.Visible = false;
                txvalue.Visible = false;
                cbvalue.Visible = false;
                txvalue3.Visible = false;
                txvalueemp.Visible = false;
                txlookupsupplier.Visible = false;
                txlookupitemcashoutpurpose.Visible = true;

            }
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbvalue = (Label)e.Row.FindControl("lbvalue");
            Label lbattribute = (Label)e.Row.FindControl("lbattribute");

            if (hdcashout.Value.ToString() != "" && hdcashout != null)
            {
                string @param = hdcashout.Value.ToString();
                if (lbattribute != null)
                {
                    hdempadv.Value = bll.vLookUp("select at.attributevalue from tcashout_advance x inner join (select a.*,b.attribute_nm from tcashout_attribute_advance a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Emp Code'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
                    hdpurpose.Value = bll.vLookUp("select at.attributevalue from tcashout_advance x inner join (select a.*,b.attribute_nm from tcashout_attribute_advance a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Purpose'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
                }
                hdemp.Value = bll.vLookUp("select x.emp_cd from tcashout_advance x inner join (select a.*,b.attribute_nm from tcashout_attribute_advance a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Purpose'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
            }
            if (lbattribute != null)
            {
                if (lbattribute.Text == "Emp Code")
                {
                    lbvalue.Text = bll.vLookUp("select emp_cd +' :: '+ emp_nm from tmst_employee where emp_cd = '" + hdempadv.Value + "'");
                }
                if (lbattribute.Text == "Purpose")
                {
                    lbvalue.Text = bll.vLookUp("select itemco_cd +' :: '+ itemco_nm from tmst_itemcashout where itemco_cd ='" + hdpurpose.Value + "'");
                }
            }
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        decimal dAmt = 0;
        decimal dVat = 0; decimal dVatCut = 0; string sFileName = string.Empty;
        FileInfo fi = null;

        string sClosingPetty = bll.vLookUp("select 1 from tcashregister_advance_closing where acknowledge='N' and cashier_id='" + cbcashregister.SelectedValue + "'");
        if (sClosingPetty.ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing PettyCashier is pending Approval','Ask For Approval','warning');", true);
            return;
        }
        if (cbcashregister.SelectedValue == string.Empty)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cash Register!','Please Select Cash Register!','warning');", true);
            return;
        }
        //if (cbacctdept.SelectedValue == string.Empty)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Account Department!','Please Select Account Department!','warning');", true);
        //    return;
        //}

        if (cbroutine.SelectedValue == "NR" && cbapproval.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Approval can not be empty!','Please Select Approval!','warning');", true);
            return;
        }
        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual No can not be empty!','Manual No','warning');", true);
            return;
        }
        if (txremark.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Remark can not be empty!','Remark','warning');", true);
            return;
        }
        if ((hdemp.Value == string.Empty) && (cbinout.SelectedValue == "O"))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC must be selected!','PIC','warning');", true);
            return;
        }
        //if (cbinout.SelectedValue != "I" && cbroutine.SelectedValue != "R" && cbcategory.SelectedValue != "HEADOFFICE" && cbitem.SelectedValue != "CHA00142")
        //if (cbinout.SelectedValue != "I" && cbroutine.SelectedValue != "R" && cbcategory.SelectedValue != "HEADOFFICE" && cbitem.SelectedValue != bll.vLookUp("select qry_data from tmap_query where qry_cd='CASH_IN_FROM_HO'"))

        //{
        //    if (hdemp.Value.ToString() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC must be selected!','PIC','warning');", true);
        //        return;
        //    }
        //}
        if (cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='need_PIC_cashout'"))) //by yanto 19-01-2020
        {
            if (hdemp.Value.ToString() == string.Empty)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC must be selected!','PIC','warning');", true);
                return;
            }
            if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'") || cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'"))
            {
                foreach (GridViewRow row in grdattr.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label lbattribute = (Label)row.FindControl("lbattribute");
                        Label lbvalue = (Label)row.FindControl("lbvalue");
                        if (lbattribute.Text == "KM")
                        {
                            if (lbvalue.Text == string.Empty || lbvalue.Text == null || Convert.ToDecimal(lbvalue.Text) == 0)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Additional information for petty cash is not completeld (" + lbattribute.Text + ") !','Additional Info (" + lbattribute.Text + ") ','warning');", true);
                                return;
                            }
                        }

                    }
                }
            }
        }
        if (!decimal.TryParse(txamt.Text, out dAmt))
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amout can not empty!','Amount Cashout','warning');", true);
            return;
        }
        if (Convert.ToDecimal(txamt.Text) <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amout can not zero or less than zero!','Amount Cashout','warning');", true);
            return;
        }
        if (cbitem.SelectedValue.ToString() == "" || cbitem.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item patty cash must be be selected!','Select Item Cashout','warning');", true);
            return;
        }

        //if (chvat.SelectedValue == "VAT")
        //{
        //    if (hdtax.Value.ToString() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Supplier VAT must be entered!','Select supplier with VAT Number','warning');", true);
        //        return;
        //    }

        //}
        else if (Convert.ToDecimal(lbvat.Text) > 0 && fucashout.HasFile == false)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Document must be uploaded for vat purpose, if vat amount greater than 0','Upload document','warning');", true);
            return;
        }
        if ((cbroutine.SelectedValue.ToString() == "NR") && (fucashout.FileName == ""))/*&& (cbinout.SelectedValue.ToString() == "O")*/
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Document must be uploaded for vat purpose!','Non Routine','warning');", true);
            return;
        }

        if (txmanualno.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual number must inserted!','Manual no','warning');", true);
            return;
        }

        if (fucashout.HasFile)
        {
            fi = new FileInfo(fucashout.FileName);
            byte[] by = fucashout.FileBytes;
            if (by.Length > 1000000)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File uploaded can not more than 1 MB!','Non Routine','warning');", true);
                return;
            }
        }

        foreach (GridViewRow row in grdattr.Rows)
        {
            if (row.RowState == DataControlRowState.Edit)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is attribute on edit state not yet update!','Update attribute','warning');", true);
                return;
            }
            if (cbcategory.SelectedValue == "ADV")
            {
                //no need in petty cash
                //string employeeNotUpdated = bll.vLookUp("select isnull(wca.attributevalue,'') from titemcashout_attribute ica right join twrk_cashoutattribute_advance wca on ica.IDS=wca.IDS where attribute_nm='Emp Code' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                //if (employeeNotUpdated.ToString() == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attribute Employee on edit state not yet update!','Please Edit, Choose Value and Update the Attribute Value!','warning');", true);
                //    return;
                //}
                //string purposeNotUpdated = bll.vLookUp("select isnull(wca.attributevalue,'') from titemcashout_attribute ica right join twrk_cashoutattribute_advance wca on ica.IDS=wca.IDS where attribute_nm='Purpose' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                //if (purposeNotUpdated.ToString() == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attribute Purpose on edit state not yet update!','Please Edit, Choose Value and Update the Attribute Value!','warning');", true);
                //    return;
                //}
            }
        }
        //if (Convert.ToDecimal(txtTotal.Text) > Convert.ToDecimal(lbcashier.Text) && cbinout.SelectedValue == "O")// && cbinout.SelectedValue == "O"
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cashier Balanace less then cashout!','Check LAST BALANCE CASHIER!','warning');", true);
        //    return;
        //    //Cashier Balanace less then cashout
        //}
        Decimal cashierbalance = Convert.ToDecimal(bll.vLookUp("select [dbo].fn_getcashierbalance_advance_temp('" + Request.Cookies["sp"].Value.ToString() + "','" + cbcashregister.SelectedValue + "')"));
        if ((Convert.ToDecimal(txbasedprice.Text) > cashierbalance) && cbinout.SelectedValue == "O")// && cbinout.SelectedValue == "O"
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cashier Balanace less then cashout!','Check LAST BALANCE CASHIER!','warning');", true);
            return;
            //Cashier Balanace less then cashout
        }
        string sCashout = string.Empty;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_dt", DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
        arr.Add(new cArrayList("@emp_cd", hdemp.Value));
        arr.Add(new cArrayList("@dept_cd", cbdept.SelectedValue));
        arr.Add(new cArrayList("@remark", "NA"));
        arr.Add(new cArrayList("@itemco_cd",cbitem.SelectedValue));
        arr.Add(new cArrayList("@amt", Convert.ToDouble( txbasedprice.Text))); 
        arr.Add(new cArrayList("@vat_amt", Convert.ToDouble( lbvat.Text)));
        arr.Add(new cArrayList("@cashout_sta_id", "N"));
        arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value));
        arr.Add(new cArrayList("@manualno", txmanualno.Text));
        //arr.Add(new cArrayList("@tacc_department_cd" , cbacctdept.SelectedValue));
        arr.Add(new cArrayList("@tacc_department_cd","NA"));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@cashier_id", cbcashregister.SelectedValue));
        arr.Add(new cArrayList("@municipality", 0));

        bll.vInsertCashoutRequest_advance(arr, ref sCashout);
        txsysno.Text = sCashout;hdcashout.Value = sCashout;
        
        lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_sta_id' and fld_valu=(select cashout_sta_id from tcashout_advance where cashout_cd='" + sCashout + "')");
        if ((cbroutine.SelectedValue.ToString() == "NR")) /*&& (cbinout.SelectedValue.ToString() == "O")*/
        {
            string strdate = Convert.ToDateTime(DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("yyyyMM");
            // FileInfo fi = new FileInfo(fucashout.FileName);
            string ext = fi.Extension;
            if (fucashout.HasFile)
            {
                sFileName = sCashout + ext;
                arr.Clear();
                arr.Add(new cArrayList("@cashoutfile", sFileName));
                arr.Add(new cArrayList("@cashout_cd", sCashout));
                bll.vUpdateCashoutRequest_advance(arr);
                fucashout.SaveAs(bll.sGetControlParameter("image_path") + @"pettycash\" + sFileName);
            }
        }

        //Walking to the moon
        arr.Clear();
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        arr.Add(new cArrayList("@cashout_cd", sCashout));
        arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
        bll.vInsertCashoutAttribute_advance(arr);
        //______________________________________________________________________________
        if ((cbroutine.SelectedValue.ToString() == "NR") && (cbinout.SelectedValue.ToString() == "O"))
        {
            //----Approval Dtl 
            arr.Clear();
            arr.Add(new cArrayList("@emp_cd", cbapproval.SelectedValue));
            arr.Add(new cArrayList("@doc_typ", "PETTYCASH"));
            arr.Add(new cArrayList("@doc_no", sCashout));
            arr.Add(new cArrayList("@approval_cd", "W"));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertApprovalDtl(arr);

            //---------------------
            int nrnd = 0;
            Random rnd = new Random();
            nrnd = rnd.Next(1000, 9999);
            string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd=(select parm_valu from tcontrol_parameter where parm_nm='salespoint')") + nrnd.ToString();
            List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());
            string sMsg = "#PETTYCASH request from " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + ", no." + sCashout + ", amt  " + txamt.Text +
                 ", for " + cbitem.SelectedItem.Text + ", do you want to approved : (Y/N)" + stoken;
            arr.Clear();
            arr.Add(new cArrayList("@token", stoken));
            arr.Add(new cArrayList("@doc_no", sCashout));
            arr.Add(new cArrayList("@doc_typ", "pettycash"));
            arr.Add(new cArrayList("@to", lapproval[0]));
            arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
            //bll.vInsertSmsOutbox(arr);

            // Sending Email 
            string sSubject = ""; string sMessage = "";
            string sfile_attachment = sFileName;
            //string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
            string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
            string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + hdemp.Value + "')");
            string cashout_typ = cbcategory.SelectedValue.ToString();
            string scashout_typ = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu='" + cashout_typ + "'");
            string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
            string screator_id = Request.Cookies["usr_id"].Value.ToString();
            string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + screator_id + "'");
            string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + hdemp.Value.ToString() + "'");
            sSubject = "#New Petty Cash Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";
            //sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
            sMessage = "<table><tr><td colspan=3>#Petty Cash Request Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + sCashout + "</td><td>PIC</td><td>:</td><td>" + spic_nm + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><td>Position </td><td>:</td><td>" + stitle + "</td></tr><tr><td>Trans Type</td><td>:</td><td>" + scashout_typ + "</td><td>Item</td><td>:</td><td>" + cbitem.SelectedItem.Text + "</td></tr><tr><td><font color='#FF0000'>Req Amount / VAT</font></td><td>:</td><td><font color='#FF0000'>" + txamt.Text + "/" + lbvat.Text + "</font></td></tr><tr><td></td></tr><tr><td>Note</td><td>:</td><td colspan=4>1. See Attached file </td></tr><tr><td></td><td></td><td colspan=4>2. This Approval to make sure all detail as attached is match with entry request.</td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
            "<p> Please Click this  for approved : <a href='" + slink_branch + "/landingpage2.aspx?src=cashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sCashout + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage2.aspx?src=cashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sCashout + "&sta=R'>Reject</a></p>" +
            " \n\r\n\r\n\r\n Wazaran Admin";
            //bll.vSendMail(lapproval[1], sSubject,sMessage, "CO20409172345.gif");
            arr.Clear();
            arr.Add(new cArrayList("@trxcd", "pettycash"));
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_no", sCashout));
            bll.vInsertEmailSent(arr);
            arr.Clear();
            arr.Add(new cArrayList("@token", nrnd.ToString()));
            arr.Add(new cArrayList("@doc_typ", "pettycash"));
            arr.Add(new cArrayList("@to", lapproval[1]));
            arr.Add(new cArrayList("@doc_no", sCashout));
            arr.Add(new cArrayList("@emailsubject", sSubject));
            arr.Add(new cArrayList("@msg", sMessage));
            arr.Add(new cArrayList("@file_attachment", @"pettycash\" + sfile_attachment));
            bll.vInsertEmailOutbox(arr);
            //************************ END OF ENTRY
            //Send WA ________________________________________________________________
            //Random rdtoken = new Random();
            //string sRandom = Request.Cookies["sp"].Value + rdtoken.Next(1000, 9999).ToString();

            //string sWa = "%23New pettycash No. " + sCashout + ", from " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value + "'") + @"%0D%0A"
            //    + cbitem.SelectedValue + "-" + bll.vLookUp("select itemco_nm from tmst_itemcashout where itemco_cd='" + cbitem.SelectedValue + "'") + "-" + (Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text)).ToString("#.#0")
            //    + @",%0D%0A " + txremark.Text + @"%0D%0APlease approve or reject with reply : Y" + sRandom + " for Approve, OR N" + sRandom + " for reject";

            //string sMobileno = bll.vLookUp("select top 1 whatsapp_no from tuser_profile where emp_cd='" + cbapproval.SelectedValue + "'");
            //arr.Clear();
            //arr.Add(new cArrayList("@wa_typ", "PETTYCASH"));
            //arr.Add(new cArrayList("@token_sent", sRandom));
            //arr.Add(new cArrayList("@mobileno", sMobileno));
            //arr.Add(new cArrayList("@emp_cd", cbapproval.SelectedValue));
            //arr.Add(new cArrayList("@refno", sCashout));
            ////bll.vInsertWhatsappOutbox(arr);


            //FileInfo fif = new FileInfo(bll.sGetControlParameter("image_path") + @"pettycash\" + sFileName);
            //string sExt = fif.Extension;
            ////data:image/jpeg;base64,
            //string sFormat = string.Empty;
            //switch (sExt)
            //{
            //    case ".png":
            //        sFormat = @"data:image/png;base64,";
            //        break;
            //    case ".pdf":
            //        sFormat = @"data:application/pdf;base64,";
            //        break;
            //    case ".xls":
            //        sFormat = @"data:application/xls;base64,";
            //        break;
            //    case ".jpg":
            //        sFormat = @"data:image/jpg;base64,";
            //        break;
            //    case ".jpeg":
            //        sFormat = @"data:image/jpeg;base64,";
            //        break;
            //}

            //if (sFormat == string.Empty)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Format file is not acceptable','Pls : xls, jpg, jpeg ,png or pdf','warning');", true);
            //    return;
            //}
            //byte[] bytes = File.ReadAllBytes(fif.FullName);
            //string sWaFile = Convert.ToBase64String(bytes);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "v_sendmessage('" + sMobileno + "','" + sWa + "');", true);
            //string URI = bll.sGetControlParameter("wazaran_api") + "SendMessage";

            //string myParameters = "phone=" + sMobileno + "&text=" + sWa;

            //using (WebClient wc = new WebClient())
            //{
            //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //    string HtmlResult = wc.UploadString(URI, myParameters);
            //}
            //System.Threading.Thread.Sleep(2000);
            //}
            //whatsapp stop
            //string sResult = bll.vSendWhatsapp(sMobileno, sWa);
            string sFileCheck = bll.sGetControlParameter("image_path") + @"pettycash\" + sFileName;
            while (!System.IO.File.Exists(sFileCheck))
            {
                System.Threading.Thread.Sleep(1000);
            }
            // stop whatsapp
            //string sResultFile = bll.vSendFile(sMobileno, sFileCheck);
            //string URIF = bll.sGetControlParameter("wazaran_api") + "SendFile";

            //string myParametersF = "phone=" + sMobileno + "&filename=" + sFileCheck;

            //using (WebClient wc = new WebClient())
            //{
            //    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            //    string HtmlResult = wc.UploadString(URIF, myParametersF);
            //}
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "v_sendfile('" + sMobileno + "','" + sFormat + sWaFile + "','" + sFileName + "');", true);
            //if (chvat.SelectedValue != string.Empty)
            //{
            //foreach (tapprovalpattern apptax in lapptax)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "v_sendfile('" + apptax.whatsapp_no + "','" + sFormat + sWaFile + "','" + sFileName + "');", true);
            //    System.Threading.Thread.Sleep(2000);
            //}
            //}
        }
        else
        {
            string _sql = "update tcashout_advance set cashout_sta_id='A' where cashout_cd='" + sCashout + "'";
            bll.vExecuteSQL(_sql ); 
        }
        // Mod : IAG , 20 Jun 2020 , accomodate VAT ___________________________________________________________________
        //if (chvat.SelectedValue == "VAT")
        //{
        //    if ((lapptax.Count > 0) && (cbvatrate.SelectedValue == (bll.vLookUp("select top 1 qry_data from tmap_query where qry_cd='vat_approval'"))))
        //    {
        //        Random rdtoken = new Random();
        //        string sTokenTax = Request.Cookies["sp"].Value + rdtoken.Next(1000, 9999).ToString();
        //        string sWaTax = "#New cashout with VAT 5%, No. " + sCashout + ", from " + bll.vLookUp("select salespoint_nm from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value + "'") + @"\r\n"
        //            + cbitem.SelectedValue + "-" + bll.vLookUp("select itemco_nm from tmst_itemcashout where itemco_cd='" + cbitem.SelectedValue + "'") + "-" + Convert.ToDouble(txamt.Text).ToString("#.#0")
        //            + @",\r\n " + txremark.Text + @"\r\nPlease approve or reject with reply : Y" + sTokenTax + " for Approve, OR N" + sTokenTax + " for reject";
        //        foreach (tapprovalpattern apptax in lapptax)
        //        {
        //            arr.Clear();
        //            arr.Add(new cArrayList("@wa_typ", "CASHOUTVAT"));
        //            arr.Add(new cArrayList("@token_sent", sTokenTax));
        //            arr.Add(new cArrayList("@mobileno", apptax.whatsapp_no));
        //            arr.Add(new cArrayList("@emp_cd", apptax.emp_cd));
        //            arr.Add(new cArrayList("@refno", sCashout));
        //            bll.vInsertWhatsappOutbox(arr);
        //            arr.Clear();
        //            arr.Add(new cArrayList("@doc_typ", "CASHOUT"));
        //            arr.Add(new cArrayList("@doc_no", sCashout));
        //            arr.Add(new cArrayList("@emp_cd", apptax.emp_cd));
        //            arr.Add(new cArrayList("@approval_cd", "W"));
        //            arr.Add(new cArrayList("@approval_method", "WA"));
        //            bll.vInsertApprovalDtl(arr);
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "v_sendmessage('" + apptax.whatsapp_no + "','" + sWaTax + "');", true);
        //            System.Threading.Thread.Sleep(2000);
        //        }
        //    }
        //}
        //_______________________________________________________________________
        cd.v_disablecontrol(cbitem);
        //cd.v_disablecontrol(cbremark);
        cd.v_disablecontrol(cbroutine);
        cd.v_disablecontrol(cbinout);
        cd.v_disablecontrol(cbapproval);
        cd.v_disablecontrol(txamt);
        cd.v_disablecontrol(txmanualno);
        cd.v_disablecontrol(txpic);
        cd.v_disablecontrol(txremark);
        cd.v_disablecontrol(cbdept);
        cd.v_disablecontrol(txtaxno);
        //cd.v_disablecontrol(cbacctdept);
        cd.v_hiddencontrol(btsave);
        cd.v_showcontrol(btprint);
        cd.v_showcontrol(New);
        //cd.v_disablecontrol(lblmunicipality);
        cd.v_disablecontrol(lbvat);
        cd.v_disablecontrol(fucashout);
        //cbacctdept.SelectedValue = string.Empty;

        //string sAlert = string.Empty;
        //if (cbvatrate.SelectedValue == (bll.vLookUp("select qry_data from tmap_query where qry_cd='vat_approval'")))
        //{ sAlert = "New Pettycash has been created, and need approval VAT on Head Office, because use 5 %"; }
        //else { sAlert = "New Cashout has been created!"; }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Cashout has been created !','"+sCashout+"','success');", true);
    }

    protected void New_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cashregout_pettycash.aspx");
    }
    protected void chvat_CheckedChanged(object sender, EventArgs e)
    {
        //if (chvat.Checked)
        //{
        //    double dAmt = 0;
        //    if (!double.TryParse(txamt.Text, out dAmt))
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry amount!','Must be numeric','warning');", true);
        //        chvat.Checked = !chvat.Checked;
        //        return;
        //    }
        //    else
        //    {
        //        double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
        //        double dVat = dPctVat * dAmt;
        //        txamt.Text = (dAmt - dVat).ToString();
        //        lbvat.Text = dVat.ToString();
        //    }
        //}
        //else
        //{

        //    if (txamt.Text == "")
        //    {
        //        txamt.Text = "0";
        //        lbvat.Text = "0";
        //    }
        //    else
        //    {
        //        double dVat = Convert.ToDouble(lbvat.Text);
        //        double dAmt = Convert.ToDouble(txamt.Text);
        //        lbvat.Text = "0";
        //        txamt.Text = (dAmt + dVat).ToString();
        //    }


        //}
        //if (chvat.Checked)
        //{
        //    //   txsuppliername.CssClass = "form-control";
        //    txtaxno.CssClass = "form-control";
        //    txtaxno.Text = "";
        //    //  txsuppliername.Text = "";
        //}
        //else
        //{
        //    //   txsuppliername.CssClass = "form-control ro" ;
        //    txtaxno.CssClass = "form-control ro";
        //    txtaxno.Text = "";
        //    //   txsuppliername.Text = "";
        //}

        //ckmunicipality_CheckedChanged(sender, e);
        //ddlMunicipality_SelectedIndexChanged(sender, e);
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
        arr.Add(new cArrayList("@supplier_cd", prefixText));
        bll.vSearchAccSupplier(ref rs, arr);
        while (rs.Read())
        {
            sTax = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["supplier_desc"].ToString(), rs["supplier_cd"].ToString());
            lsupplier.Add(sTax);
        }
        return (lsupplier.ToArray());
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList4(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lsupplier = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sTax = string.Empty;
        arr.Add(new cArrayList("@emp_nm", prefixText));
        bll.vSearchMstEmployee(arr, ref rs);
        while (rs.Read())
        {
            sTax = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["emp_desc"].ToString(), rs["emp_cd"].ToString());
            lsupplier.Add(sTax);
        }
        return (lsupplier.ToArray());
    }
    protected void bt_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        //txsuppliername.Text = bll.vLookUp("select supp_nm from tacc_supplier where tax_no='"+hdtax.Value.ToString()+"'");
        //txtaxno.Text = hdtax.Value.ToString();
    }
    protected void grdattr_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdattr.EditIndex = e.NewEditIndex;
        vInitGrid();
    }
    protected void grdattr_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        HiddenField hdids = (HiddenField)grdattr.Rows[e.RowIndex].FindControl("IDS");
        TextBox txvalue = (TextBox)grdattr.Rows[e.RowIndex].FindControl("txvalue");
        TextBox txvalue2 = (TextBox)grdattr.Rows[e.RowIndex].FindControl("txvalue2");
        DropDownList cbvalue = (DropDownList)grdattr.Rows[e.RowIndex].FindControl("cbvalue");
        arr.Add(new cArrayList("@IDS", hdids.Value.ToString()));
        arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
        string sDatatype = bll.vLookUp("select datatype from titemcashout_attribute where IDS='" + hdids.Value.ToString() + "'");
        if (sDatatype == "1")
        {
            arr.Add(new cArrayList("@attributevalue", txvalue2.Text));
        }
        else if (sDatatype == "2")
        {
            arr.Add(new cArrayList("@attributevalue", txvalue.Text));
        }
        else if (sDatatype == "3")
        {
            arr.Add(new cArrayList("@attributevalue", cbvalue.SelectedValue.ToString()));
        }
        else if (sDatatype == "4")
        {
            arr.Add(new cArrayList("@attributevalue", cbvalue.SelectedValue.ToString()));
        }
        else if (sDatatype == cd.dttextboxcustomer) // Customer
        {
            arr.Add(new cArrayList("@attributevalue", hdcust.Value.ToString()));
        }
        //else if (sDatatype == cd.dttextboxemployee)  // Employee
        //{
        //    arr.Add(new cArrayList("@attributevalue", hdemp.Value.ToString()));
        //}
        else if (sDatatype == cd.dttextboxemployee)  // Employee Advance
        {
            arr.Add(new cArrayList("@attributevalue", hdempadv.Value.ToString()));
        }
        else if (sDatatype == cd.dtlookupsupplier)
        {
            arr.Add(new cArrayList("@attributevalue", hdlookuptax.Value.ToString()));
        }
        else if (sDatatype == cd.dtlookupitemcashout)
        {
            arr.Add(new cArrayList("@attributevalue", hdpurpose.Value.ToString()));
        }
        bll.vUpdateWRKCashoutAttribute_advance(arr);
        grdattr.EditIndex = -1;
        vInitGrid();
        //if (cbitem.SelectedValue.Equals("CHA00048") || cbitem.SelectedValue.Equals("CHA00080"))
        if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'") || cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'")) //BY YANTO 19-01-2020
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectPcPIC();", true);
        }
    }

    protected void btPcPIC_Click(object sender, EventArgs e)
    {
        //if (cbitem.SelectedValue.Equals("CHA00048") || cbitem.SelectedValue.Equals("CHA00080"))
        if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'") || cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'")) //BY YANTO 19-01-2020
        {
            //txpic.Text = bll.vLookUp("select (emp_cd + '-'+ emp_nm) emp_desc from tmst_employee where emp_cd = '" + hdemp.Value.ToString() + "'");
            string dept_cd = "";
            string dept_nm = "";
            if (hdempadv.ToString() != "" && hdempadv.ToString() != null)
            {
                dept_cd = bll.vLookUp("select dept_cd from tmst_employee where emp_cd='" + hdempadv.Value.ToString() + "'");
                dept_nm = bll.vLookUp("select dept_nm from tmst_employee a left join tmst_department b on a.dept_cd = b.dept_cd where emp_cd='" + hdempadv.Value.ToString() + "'");
            }
            else
            {
                dept_cd = bll.vLookUp("select dept_cd from tmst_employee where emp_cd='" + hdemp.Value.ToString() + "'");
                dept_nm = bll.vLookUp("select dept_nm from tmst_employee a left join tmst_department b on a.dept_cd = b.dept_cd where emp_cd='" + hdemp.Value.ToString() + "'");
            }

            if (cbdept.Items.FindByValue(dept_cd) != null)
            {
                cbdept.SelectedValue = dept_cd;
            }
            else if (dept_cd == "SA")
            {
                cbdept.SelectedValue = "SALES";
            }
            else if (dept_cd == "ACCH")
            {
                cbdept.SelectedValue = "ACC";
            }
            else if (dept_cd == "Merchandiser")
            {
                cbdept.SelectedValue = "MERCH";
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please Select this PIC Dept Or Call Wazaran Admin:','" + dept_cd + '-' + dept_nm + "','warning');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please don't forget to input Employee!','" + dept_cd + '-' + dept_nm + "','warning');", true);
            }

        }
    }
    protected void grdattr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdattr.EditIndex = -1;
        vInitGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=croreq&no=" + txsysno.Text + "');", true);
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=croreq2&no=" + txsysno.Text + "');", true);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=croreq2&no=" + txsysno.Text + "');", true);
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (cbinout.SelectedValue == "I")
        {
            bll.vBindingFieldValueToComboWithChoosen(ref cbcategory, "cashout_typ");

            //cbroutine.SelectedValue = "NR";
            //cbcategory.SelectedValue = "ADV";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue));
            arr.Add(new cArrayList("@routine", cbroutine.SelectedValue));
            arr.Add(new cArrayList("@inout", cbinout.SelectedValue));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbitem, "sp_tmst_itemcashout_get1", "itemco_cd", "itemco_nm", arr);


            cbitem.Items.Remove(cbitem.Items.FindByValue("CHA00080"));

            cd.v_disablecontrol(cbinout);
            cd.v_enablecontrol(cbroutine);
            cd.v_enablecontrol(cbcategory);

            //cd.v_disablecontrol(cbremark);
            cd.v_disablecontrol(cbvatrate);
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbdept);
            cd.v_disablecontrol(txtaxno);

        }
        else if (cbinout.SelectedValue == "O")
        {
            cd.v_enablecontrol(cbroutine);
            cd.v_enablecontrol(cbcategory);

        }
        //if (cbinout.SelectedValue != "H")
        //{
        //    cbType = "cbTypeInOut";
        //    vInitItemCashout(cbType);
        //    //if ((cbcategory != null && cbcategory.SelectedValue != "") && !(cbroutine.SelectedValue == "R" && cbcategory.SelectedValue == "EMPLOYEE")) { 
        //    if ((cbcategory != null && cbcategory.SelectedValue != "") && cbcategory.SelectedValue != "EMPLOYEE" && cbcategory.SelectedValue != "DISTRIBUTION" && cbroutine.SelectedValue == "NR")
        //    {
        //        cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
        //    }
        //}
        //else
        //{
        //    cbinout.SelectedValue = "H";
        //    cbroutine.SelectedValue = "NR";
        //    bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
        //    cbcategory.SelectedValue = "ADV";

        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
        //    arr.Add(new cArrayList("@inout", "O"));
        //    bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get1", "itemco_cd", "itemco_nm", arr);
        //    //cbitem_SelectedIndexChanged(this, EventArgs.Empty);

        //    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
        //    //yellowLine1.Style.Add("background-color", "#ADFF2F");
        //    //yellowLine2.Style.Add("background-color", "#ADFF2F");
        //    //yellowLine3.Style.Add("background-color", "#ADFF2F");

        //    cbroutine.Items.Remove(cbroutine.Items.FindByValue("0"));

        //    SqlDataReader rdr = null;
        //    //SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    //con = new SqlConnection(ConfigurationManager
        //    //.ConnectionStrings["connstr"].ConnectionString);

        //    //con.Open();
        //    string CommandText;
        //    CommandText = "select * from tfield_value where fld_nm='cashout_typ' and fld_valu not in('ADV') ";
        //    cmd = new SqlCommand(CommandText);
        //    //cmd.Connection = con;
        //    cmd.Connection = cd.getConnection();

        //    rdr = cmd.ExecuteReader();

        //    while (rdr.Read())
        //    {
        //        string itemToRmv = rdr["fld_valu"].ToString();

        //        cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
        //    }
        //    cbitem_SelectedIndexChanged(this, EventArgs.Empty);
        cd.v_disablecontrol(cbinout);
    }

    //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
    //{
    //    EmployeeAdvanceOutNote.Visible = true;
    //}
    //else
    //{
    //    EmployeeAdvanceOutNote.Visible = false;
    //}
    //if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
    //{
    //    EmployeeAdvanceInNote.Visible = true;
    //}
    //else
    //{
    //    EmployeeAdvanceInNote.Visible = false;
    //}

    //cd.v_enablecontrol(cbroutine);

    //protected void grdattr_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{

    //}
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        System.Data.SqlClient.SqlDataReader rs = null;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@cashout_cd", hdcashout.Value.ToString()));
        bll.vGetCashoutRequest_advance(arr, ref rs);
        while (rs.Read())
        {

            txsysno.Text = rs["cashout_cd"].ToString();
            txamt.Text = rs["amt"].ToString();
            txmanualno.Text = rs["manualno"].ToString();
            lbstatus.Text = bll.sGetFieldValue("cashout_sta_id", rs["cashout_sta_id"].ToString());
            txpic.Text = rs["emp_cd"].ToString();
            cbroutine.SelectedValue = bll.vLookUp("select routine from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");
            cbinout.SelectedValue = bll.vLookUp("select inout from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");
            // Here we open all for view 
            arr.Clear();
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingComboToSpWithEmptyChoosen(ref cbcashregister, "sp_tcashregister_id_getAll", "cashier_id", "name", arr);

            cbcashregister.SelectedValue = rs["cashier_id"].ToString();
            cbcategory.SelectedValue = bll.vLookUp("select cashout_typ from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");
            cbcategory_SelectedIndexChanged(sender, e);
            // cbType = "none";
            // vInitItemCashout(cbType);
            cbitem.SelectedValue = rs["itemco_cd"].ToString();
            txremark.Text = rs["remark"].ToString();
            //cbdept.CssClass = cd.csstextro;
            //txamt.CssClass = cd.csstextro;
            //txmanualno.CssClass = cd.csstextro;
            //txpic.CssClass = cd.csstextro;
            //cbinout.CssClass = cd.csstextro;
            //cbroutine.CssClass = cd.csstextro;
            //cbitem.CssClass = cd.csstextro;
            //cbcategory.CssClass = cd.csstextro;
            //txremark.CssClass = cd.csstextro;
            //cbremark.CssClass = cd.csstextro;
            //cbapproval.CssClass = cd.csstextro;
            //chvat.CssClass = cd.csstextro;
            //dtcashout.CssClass = cd.csstextro;
            //btsave.CssClass = "btn btn-default ro";
            //ddlClaimCashout.CssClass = cd.csstextro;
            cd.v_disablecontrol(cbdept);
            cd.v_disablecontrol(txamt);
            cd.v_disablecontrol(txmanualno);
            cd.v_disablecontrol(txpic);
            cd.v_disablecontrol(cbinout);
            cd.v_disablecontrol(cbroutine);
            cd.v_disablecontrol(cbitem);
            cd.v_disablecontrol(cbcategory);
            cd.v_disablecontrol(txremark);
            //cd.v_disablecontrol(cbremark);
            cd.v_disablecontrol(cbapproval);
            //cd.v_disablecontrol(chvat);
            cd.v_disablecontrol(dtcashout);
            cd.v_hiddencontrol(btsave);
            cd.v_showcontrol(btprint);
            //cd.v_disablecontrol(ddlClaimCashout);
            //lbbalanceco.Text= bll.vLookUp("select amt from tcashout_advance where cashout_cd='" + rs["cashout_cd"].ToString() + "'");

            //    System.Data.SqlClient.SqlDataReader rs2 = null;
            //    List<cArrayList> arr2 = new List<cArrayList>();
            //    arr2.Add(new cArrayList("@emp_cd", null));
            //    arr2.Add(new cArrayList("@PettycashRefno", hdcashout.Value.ToString()));
            //    arr2.Add(new cArrayList("@claim", null));
            //    bll.vGetPettyCashEmp2(ref rs2, arr2);
            //    while (rs2.Read())
            //    {
            //        lbbalanceco.Text = rs2["totalBalance"].ToString();
            //        if (cbcategory.SelectedValue == "ADV")
            //        {
            //            //ddlClaimCashout.SelectedValue = rs2["ClaimCashout"].ToString();
            //            ClaimCashout.Visible = true;
            //        }
            //        else
            //        {
            //            ClaimCashout.Visible = false;
            //        }
            //    }
            //    rs2.Close();
            //}
            //rs.Close();

            //cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");

            if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
            {
                EmployeeAdvanceOutNote.Visible = true;
                EmployeeAdvanceOutNote.Text = string.Empty;
            }
            else
            {
                EmployeeAdvanceOutNote.Visible = false;
                EmployeeAdvanceOutNote.Text = string.Empty;
            }
            if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "I")
            {
                EmployeeAdvanceInNote.Visible = true;
                EmployeeAdvanceInNote.Text = string.Empty;
            }
            else
            {
                EmployeeAdvanceInNote.Visible = false;
                EmployeeAdvanceInNote.Text = string.Empty;
            }
        }
    }
    protected void txamt_TextChanged(object sender, EventArgs e)
    {
        double _amt = Convert.ToDouble(txamt.Text);
        double _vatrate = Convert.ToDouble(bll.vLookUp("select tax_formula from tmst_tax where tax_cd='"+cbvatrate.SelectedValue+"'"));
        double _pricebased = 1 / (1 + _vatrate) * _amt;
        txbasedprice.Text = _pricebased.ToString("N2");
        lbvat.Text = (_amt - _pricebased).ToString("N2");
         
        try { 
            
            cd.v_disablecontrol(txamt);
            cd.v_disablecontrol(cbvatrate);
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt1", "sweetAlert('Error Munipacility Vat Calculation','Vat Calculation','warning');", true);
            bll.vHandledError(ref ex, "VAT");
        }
        finally
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        }
    }

    //private decimal gettotalamt()
    //{
    //    double dAmount = 0;
    //    if (!double.TryParse(txamt.Text, out dAmount))
    //    {
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please input amount based!','Please input correctly','warning');", true);
    //        return 0;
    //    }

    //    return (Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text));
    //}
    protected void chvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        //double dAmt = 0;
        //txamt.CssClass = cd.csstextro;
        //cd.v_disablecontrol(txamt);
        //if (chvat.SelectedValue == "VAT")
        //{

        //    //if (!double.TryParse(txamt.Text, out dAmt))
        //    //{
        //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please entry amount!','Must be numeric','warning');", true);
        //    //    chvat.SelectedValue = "NONVAT";
        //    //    return;
        //    //}
        //    //else
        //    //{
        //    //    double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
        //    //    double dValue = (1 / (1 + dPctVat)) * dAmt;
        //    //    double dVat = dAmt - dValue;
        //    //    //double dVat = dPctVat * dAmt;
        //    //    txamt.Text = Math.Round(dValue, 2, MidpointRounding.AwayFromZero).ToString(); // String.Format("{0:0.00}", dValue);
        //    //    lbvat.Text = Math.Round(dVat, 2, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);
        //    //}
        //    cd.v_enablecontrol(cbvatrate);
        //    cd.v_enablecontrol(txamt);
        //}
        //else if (chvat.SelectedValue == "NONVAT")
        //{

        //    if (txamt.Text == string.Empty)
        //    {
        //        txamt.Text = "0";
        //        lbvat.Text = "0";
        //    }
        //    else
        //    {
        //        double dVat = Convert.ToDouble(lbvat.Text);
        //        dAmt = Convert.ToDouble(txamt.Text);
        //        lbvat.Text = "0";
        //        txamt.Text = (dAmt + dVat).ToString();
        //    }


        //}
        //if (chvat.SelectedValue == "VAT")
        //{
        //    //   txsuppliername.CssClass = "form-control";
        //    //txtaxno.CssClass = cd.csstext;
        //    cd.v_enablecontrol(txtaxno);
        //    cd.v_enablecontrol(cbvatrate);
        //    cd.v_enablecontrol(txamt);
        //    txtaxno.Text = string.Empty;
        //    //  txsuppliername.Text = "";
        //}
        //else if (chvat.SelectedValue == "NON VAT")
        //{
        //    //   txsuppliername.CssClass = "form-control ro" ;
        //    //txtaxno.CssClass = cd.csstextro;
        //    cd.v_disablecontrol(txtaxno);
        //    cd.v_disablecontrol(cbvatrate);
        //    txtaxno.Text = string.Empty;
        //    cbvatrate.SelectedValue = string.Empty;
        //    //   txsuppliername.Text = "";
        //}

        //ckmunicipality_CheckedChanged(sender, e);
        //cd.v_disablecontrol(chvat);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
        //ddlMunicipality_SelectedIndexChanged(sender, e);
    }

    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public static void btsysno_RefreshPage()
    public void btsysno_Click(object sender, EventArgs e)
    {
        bll.vBindingFieldValueToComboWithChoosen(ref cbinout, "inout");
        cbinout.SelectedValue = "";
        bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
        dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
        //lbcashier.Text = bll.vLookUp("select dbo.fn_getcashierbalance_advance("+cbcashregister.SelectedValue+")").ToString();
        lbcashier.Text = string.Format("{0:#,0.####}", Convert.ToDouble(bll.vLookUp("select dbo.fn_getcashierbalance_advance('" + cbcashregister.SelectedValue + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
        //lbcashier.Text = bll.vLookUp("select sum(amt)amt from(select cash_dt,amt from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='I')union all select cash_dt,amt-1 from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='O'))x where cash_dt>='2018-01-01'").ToString();
        bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
        if (cbinout.SelectedValue == "I")
        {
            //cbcategory.SelectedValue = "ADV";
        }
        cbcategory_SelectedIndexChanged(sender, e);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "CallMyFunction", "PopupCenter('lookupcashoutPettyCash.aspx', 'xtf', '900', '500');", true);

        if (hdcashout.Value.ToString() == null)
        {
            //cbType = "none";
            //vInitItemCashout(cbType);
        }
    }

    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public static string GetBalancePerCashOut(string sVar)
    //{
    //    string result;

    //    SqlConnection con = new SqlConnection(ConfigurationManager
    //    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString);
    //    SqlCommand SelectCommand = new SqlCommand("select amt from tcashout_advance where cashout_cd='"+ sVar + "'", con);

    //    con.Open();
    //    result = (string)SelectCommand.ExecuteScalar().ToString();
    //    con.Close();

    //    return result;
    //}

    //protected void ckmunicipality_CheckedChanged(object sender, EventArgs e)
    //{

    //    lblmunicipality.Text = "0";
    //    bool isMunicipality = false;

    //    DataTable dt = new DataTable();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@hiddendata", false));
    //    arr.Add(new cArrayList("@fld_nm", "municipality_itemco_cd"));
    //    dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        if (Convert.ToString(dr["fld_valu"]) == Convert.ToString(cbitem.SelectedValue))
    //        {
    //            isMunicipality = true;
    //        }
    //    }
    //    //if (isMunicipality == true)
    //    //{
    //    //    ckmunicipality.Checked = true;
    //    //}
    //    //if (isMunicipality == true)
    //    //{

    //    //    decimal municipalityPer = Convert.ToDecimal(cbmunipacility.SelectedValue);//bll.sGetControlParameter("municipalityTax")

    //    //    decimal totAmt = Convert.ToDecimal(Convert.ToDecimal(txamt.Text));
    //    //    lblmunicipality.Text = Convert.ToString(Convert.ToDecimal(municipalityPer * totAmt) / 100);
    //    //}
    //    // we can't  force to apply Municipality tax
    //    //else if(isMunicipality==true && ckmunicipality.Checked == false)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select municipality tax!','Municipality tax','warning');", true);

    //    //}

    //    txtTotal.Text = Convert.ToString(Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text));
    //}
    protected void btsearchPlate_Click(object sender, EventArgs e)
    {
        // Auto bind department;
        string dep = bll.vLookUp("select dept_cd from tmst_employee where  emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
        string depExist = bll.vLookUp("select dept_nm from tmst_department  where deleted=0 and dept_cd='" + dep + "'");
        string depExist_cd = bll.vLookUp("select dept_cd from tmst_department  where deleted=0 and dept_cd='" + dep + "'");
        if (depExist == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Please update department for employee " + hdemp.Value + " .','Please Contact Wazaran Admin','warning');", true);
            txpic.Text = "";
            hdemp.Value = "";
            return;
        }
        else
        {
            cbdept.SelectedValue = depExist_cd;
        }

        //if (cbitem.SelectedValue == "CHA00007" || cbitem.SelectedValue == "CHA00023" || cbitem.SelectedValue == "CHA00024" 
        //    || cbitem.SelectedValue == "CHA00056" || cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088" 
        //    || cbitem.SelectedValue == "CHA00109")
        DataTable dtTemp = new DataTable();
        List<cArrayList> arrDt = new List<cArrayList>();
        arrDt.Add(new cArrayList("@qry_cd", "need_PIC_cashout"));
        dtTemp = cdl.GetValueFromSP("sp_tmap_query_get", arrDt);
        var cashOutIds = string.Empty;
        bool isValidCash = false;
        foreach (DataRow dr in dtTemp.Rows)
        {
            if (Convert.ToString(cbitem.SelectedValue) == Convert.ToString(dr["qry_data"]))
            {
                isValidCash = true;
            }
        }

        //if (cbitem.SelectedValue.Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='need_PIC_cashout'"))) //by yanto 19-01-2020
        //if (isValidCash)
        {
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
            //arr.Add(new cArrayList("@slaespointcd", Request.Cookies["sp"].Value.ToString()));
            //bll.vBindingComboToSp(ref ddlPlateNo, "sp_tmst_tmst_vehicleOther_get", "vhc_cd", "vehicleName", arr);

            //string sPlate = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
            //if (sPlate == "")
            //{
            //    sPlate = bll.vLookUp("select vhc_cd from tmst_vehicle_other where emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
            //}
            ////txPlateNo.Text = sPlate;
            //if (sPlate == "")
            //{
            //    txpic.Text = "";
            //    hdemp.Value = "";
            //    //txPlateNo.Text = "";
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Plate No. is not Available.','Please Contact Wazaran Admin','warning');", true);
            //    return;
            //}
            //if (hdemp.Value != "")
            //{
            //    if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'"))
            //    {
            //        lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
            //                                   "  from tcashout_attribute" +
            //                                   "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
            //                                   "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
            //                                   //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
            //                                   "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT1'") + "' )" +
            //                                   "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
            //                                   "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
            //                                   "  order by tcashout_request.cashout_dt desc");
            //    }
            //    else
            //    {
            //        lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
            //                                "  from tcashout_attribute" +
            //                                "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
            //                                "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
            //                                //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
            //                                "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
            //                                "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
            //                                "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
            //                                "  order by tcashout_request.cashout_dt desc");
            //    }
            //cbremark_SelectedIndexChanged(sender, e);
            //}
        }
    }

    protected void cbremark_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (cbremark.SelectedValue != "")
        //{
        //    lblReasonValue.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute_advance.attributevalue,0) " +
        //                                      "  from tcashout_attribute_advance" +
        //                                      "  inner join tcashout_request_car_info_advance    on tcashout_request_car_info_advance.cashout_cd = tcashout_attribute_advance.cashout_cd" +
        //                                      "  inner join tcashout_advance on tcashout_advance.cashout_cd = tcashout_attribute_advance.cashout_cd" +
        //                                      "  where tcashout_attribute_advance.ids in ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
        //                                      "  and tcashout_advance.emp_Cd = '" + hdemp.Value + "'" +
        //                                      "  and tcashout_request_car_info_advance.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
        //                                      "  and tcashout_advance.sequenceno =  " + cbremark.SelectedValue.ToString() + " " +
        //                                      "  and tcashout_attribute_advance.salespointcd =  '" + Request.Cookies["sp"].Value.ToString() + "' " +
        //                                      "  order by tcashout_advance.cashout_dt desc");
        //    if (lblReasonValue.Text == "")
        //    { lblReasonValue.Text = "0"; }
        //    lblReason.Text = "Last Reading(KM) - " + cbremark.SelectedItem.Text;

        //}
        //else
        //{
        //    lblReasonValue.Text = string.Empty;
        //    lblReason.Text = string.Empty;
        //}
    }

    //protected void ddlMunicipality_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    lblmunicipality.Text = "0";
    //    bool isMunicipality = false;

    //    DataTable dt = new DataTable();
    //    List<cArrayList> arr = new List<cArrayList>();
    //    arr.Add(new cArrayList("@hiddendata", false));
    //    arr.Add(new cArrayList("@fld_nm", "municipality_itemco_cd"));
    //    dt = cdl.GetValueFromSP("sp_tfield_value_get", arr);

    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        if (Convert.ToString(dr["fld_valu"]) == Convert.ToString(cbitem.SelectedValue))
    //        {
    //            isMunicipality = true;
    //        }
    //    }
    //    //if (isMunicipality == true)
    //    //{
    //    //    ckmunicipality.Checked = true;
    //    //}
    //    if (isMunicipality == true)
    //    {

    //        //    decimal municipalityPer = Convert.ToDecimal(cbmunipacility.SelectedValue);//bll.sGetControlParameter("municipalityTax")

    //        //decimal totAmt = Convert.ToDecimal(Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text));
    //        //lblmunicipality.Text = Convert.ToString(Convert.ToDecimal(municipalityPer * totAmt) / 100);
    //    }
    //    // we can't  force to apply Municipality tax
    //    //else if(isMunicipality==true && ckmunicipality.Checked == false)
    //    //{
    //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select municipality tax!','Municipality tax','warning');", true);

    //    //}
    //    cd.v_disablecontrol(cbmunipacility);
    //    //txtTotal.Text = Convert.ToString(Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text));
    //}

    protected void cbvatrate_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sNoVat = bll.sGetControlParameter("novat_expense_code");
        if (sNoVat == cbvatrate.SelectedValue)
        {
            cd.v_disablecontrol(txtaxno);
        }
        else { cd.v_enablecontrol(txtaxno); }
        cd.v_disablecontrol(cbvatrate);
      
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void txtTotal_TextChanged(object sender, EventArgs e)
    {

    }

    protected void lbvat_TextChanged(object sender, EventArgs e)
    {
        //txtTotal.Text = string.Format("{0:F2}", gettotalamt());
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);
    }

    protected void cbcashregister_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sClosingPetty = bll.vLookUp("select 1 from tcashregister_advance_closing where acknowledge='N' and cashier_id='" + cbcashregister.SelectedValue.ToString() + "'");
        if (sClosingPetty.ToString() == "1")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Closing PettyCashier is pending Approval','Ask For Approval','warning');", true);
            return;
        }
        cd.v_disablecontrol(cbcashregister);
        //cd.v_readonly(cbacctdept);
        cd.v_showcontrol(btsave);
        //cd.v_enablecontrol(cbacctdept);
        cd.v_enablecontrol(cbinout);
        lbcashier.Text = string.Format("{0:#,0.####}", Convert.ToDouble(bll.vLookUp("select dbo.fn_getcashierbalance_advance('" + cbcashregister.SelectedValue + "','" + Request.Cookies["sp"].Value.ToString() + "')")));
    }

    protected void btupdatesupplier_Click(object sender, EventArgs e)
    {
        List<cArrayList> arr = new List<cArrayList>();
        List<tacc_mst_supplier> _tacc_mst_supplier = new List<tacc_mst_supplier>();
        string _url = "http://192.168.120.123:6048/INDOMOROC_API/OData/Wazaran_Vendor?$format=json";
        using (WebClient ws = new WebClient())
        {
            ws.Credentials = new NetworkCredential("NAVAPI", "CCXLFGq+5xIRijJAK/ghkhMJwuScb0/O2f8xHSJ9Wrk=");
            ServicePointManager.ServerCertificateValidationCallback += (senderX, certificate, chain, sslPolicyErrors) => true;
            var json = ws.DownloadString(_url);
            Root _json = JsonConvert.DeserializeObject<Root>(json);
            List<Value> _Value = _json.value;
            foreach (Value _t in _Value)
            {
                arr.Clear();
                arr.Add(new cArrayList("@supplier_cd", _t.No));
                arr.Add(new cArrayList("@supplier_nm", _t.Name));
                arr.Add(new cArrayList("@supplier_address", "NA"));
                bll2.vInsertAccMstSupplier(arr);
            }
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Sync supplier with nav has been successfully!','Supplier Data','info');", true);
    }
}