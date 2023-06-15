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

public partial class fm_cashregoutentry2 : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    string cbType = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //cbdept.CssClass = "form-control-static input-sm ro";
                bll.vBindingFieldValueToCombo(ref cbinout, "inout");
                cbinout.SelectedValue = "O";
                bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
                dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
                lbcashier.Text = bll.vLookUp("select dbo.fn_getcashierbalance('" + Request.Cookies["sp"].Value.ToString() + "')").ToString();
                //lbcashier.Text = bll.vLookUp("select sum(amt)amt from(select cash_dt,amt from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='I')union all select cash_dt,amt-1 from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='O'))x where cash_dt>='2018-01-01'").ToString();
                lbbalanceco.Text = "";
                bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);


                //List<cArrayList> arr = new List<cArrayList>();
                //arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
                //arr.Add(new cArrayList("@inout", cbinout.SelectedValue.ToString()));
                //bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);

                //SqlDataReader rdr =  null;
                //SqlConnection con = new SqlConnection();
                //SqlCommand cmd = null;
                //con = new SqlConnection(ConfigurationManager
                //.ConnectionStrings["connstr"].ConnectionString);

                //con.Open();

                //string CommandText = "select * from tfield_value where fld_nm='cashout_typ' and fld_valu not in(select cashout_typ from tmst_itemcashout where routine='" + cbroutine.SelectedValue.ToString() + "' and inout='" + cbinout.SelectedValue.ToString() + "') ";
                //cmd = new SqlCommand(CommandText);
                //cmd.Connection = con;

                //rdr = cmd.ExecuteReader();

                //while (rdr.Read())
                //{
                //    string itemToRmv = rdr["fld_valu"].ToString();

                //    cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
                //}

                cbcategory.Items.Remove(bll.vLookUp("select fld_valu from tfield_value where fld_nm='cashout_typ' and fld_valu not in(select cashout_typ from tmst_itemcashout where routine='" + cbroutine.SelectedValue.ToString() + "' and inout='" + cbinout.SelectedValue.ToString() + "') "));
                bll.vBindingComboToSp(ref cbdept, "sp_tmst_department_get", "dept_cd", "dept_nm");
                // chvat.Checked = false;
                chvat_SelectedIndexChanged(sender, e);
                txamt.CssClass = cd.csstext;
                // Support for Multiple Tax
                //showTax.Attributes.Add("style", "display:none");
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@cashout_cd", Request.Cookies["usr_id"].Value.ToString()));
                bll.vDeleteCashoutRequest_Tax(arr);
                txtaxall.Text = "";
                hdtaxall.Value = "";
                vMunicipality.Attributes.Add("style", "display:none");
                //cbinout.Items.Remove(cbinout.Items.FindByValue("H"));
                //cbinout.Items.Insert(0, new ListItem("HO To Employee", "H"));

                cbroutine.Items.Remove(cbroutine.Items.FindByValue("0"));
                cbroutine.Items.Insert(0, new ListItem("Select", "0"));

                cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
                cbcategory.Items.Insert(0, new ListItem("Select", "0"));

                cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
                cbitem.Items.Insert(0, new ListItem("Select", "0"));

                if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue.ToString() == "O")
                {
                    lbCashoutInfo.InnerText = "Cash Received By";
                    lbPIC.InnerText = "Cash Issued By";
                }
                else if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue.ToString() == "I")
                {
                    lbCashoutInfo.InnerText = "Cash Issued By";
                    lbPIC.InnerText = "Cash Received By";
                }
                else
                {
                    lbCashoutInfo.InnerText = "Cashout Info";
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
                //if (cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088")
                if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_WASH'")) || cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'")))  // by yanto 18-01-2020
                {
                    DivPlate.Visible = true;
                    if (hdemp.Value != "")
                    {
                        lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
                                                   "  from tcashout_attribute" +
                                                   "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
                                                   "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
                                                   "  where tcashout_attribute.ids in ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
                                                   "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
                                                   "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
                                                   "  order by tcashout_request.cashout_dt desc");
                    }
                }
                else
                {
                    DivPlate.Visible = false;
                }

                DivPlate.Visible = false;
                cbremark.Enabled = false;
                //cbapproval.Enabled = false;
                cbinout_SelectedIndexChanged(sender, e);
                cbroutine_SelectedIndexChanged(sender, e);

            }
            catch (Exception ex)
            {
                Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
                bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
                Response.Redirect("fm_ErrorPage.aspx");
            }
        }
        

    }
    protected void cbcategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbType = "cbTypeCategory";
            vInitItemCashout(cbType);
            if (cbcategory.SelectedValue == "ADV")
            {
                ClaimCashout.Visible = true;
                txpic.Enabled = false;
                cbdept.Enabled = false;
                txpic.Text = Request.Cookies["usr_id"].Value.ToString() + '-' + bll.vLookUp("select emp_nm from tmst_employee where emp_cd='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                hdemp.Value = Request.Cookies["usr_id"].Value.ToString();
            }
            else
            {
                ClaimCashout.Visible = false;
                txpic.Enabled = true;
                cbdept.Enabled = true;
                txpic.Text = "";
            }

            //if ((cbcategory != null && cbcategory.SelectedValue != "") && cbcategory.SelectedValue != "EMPLOYEE" && cbcategory.SelectedValue != "DISTRIBUTION")
            if ((cbcategory != null && cbcategory.SelectedValue != "") && cbcategory.SelectedValue != "EMPLOYEE" && cbcategory.SelectedValue != "DISTRIBUTION" && cbroutine.SelectedValue == "NR")
            {
                if (cbcategory.SelectedValue == "ADV")
                {
                    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
                }                
            }

            if (cbcategory.SelectedValue.ToString() == "ADV" && cbinout.SelectedValue == "O")
            {                EmployeeAdvanceOutNote.Visible = true;

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
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    //void vInitItemCashout()
    void vInitItemCashout(string cbType)
    {
        try
        {
            //List<cArrayList> arr = new List<cArrayList>();
            //arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
            //arr.Add(new cArrayList("@inout", "O"));
            //bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
            //cbitem_SelectedIndexChanged(this, EventArgs.Empty);

            if (cbType != "cbTypeCategory")
            {
                bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
                SqlDataReader rdr = null;
                //SqlConnection con = null;
                SqlCommand cmd = null;
                //con = new SqlConnection(ConfigurationManager
                //.ConnectionStrings["connstr"].ConnectionString);

                //con.Open();
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
                //cmd.Connection = con;
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
            bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
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
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
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
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdattr, "sp_twrk_cashoutattribute_get", arr);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //hdemp.Value = "";
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vDelWRKCashoutAttribute(arr);
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertWRKCashoutAttribute(arr);
            vInitGrid();
            arr.Clear();
            arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
            bll.vBindingComboToSp(ref cbremark, "sp_titemcashout_remark_get", "sequenceno", "remark", arr);
            //// for Jeddah Branch
            //if (cbitem.SelectedValue == "CHA00099" && cbcategory.SelectedValue == "CASHDEPTOHO" && Convert.ToString(Request.Cookies["sp"].Value) == "101")
            //{
            //    //select isnull(sum(totamt),0) from tmst_payment where payment_dt
            //    //string alreadyPaid = bll.vLookUp("select isnull(sum(amt),0) from tcashout_request where itemco_cd = 'CHA00099' and cashout_dt='" + Convert.ToString(bll.sGetControlParameter("wazaran_dt")) + "'");
            //    txamt.Text = bll.vLookUp("select isnull(sum(totamt),0) from tmst_payment where datediff(d,payment_dt,'" + DateTime.ParseExact(Request.Cookies["waz_dt"].Value, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString() + "')=0 and payment_sta_id in ('N','C')");
            //    txamt.CssClass = cd.csstextro;
            //}

            if (cbcategory.SelectedValue.ToString() == "ADV")
            {
                yellowLine1.Style.Add("background-color", "#ADFF2F");
                yellowLine2.Style.Add("background-color", "#ADFF2F");
                yellowLine3.Style.Add("background-color", "#ADFF2F");
            }
            else
            {
                yellowLine1.Style.Add("background-color", "yellow");
                yellowLine2.Style.Add("background-color", "yellow");
                yellowLine3.Style.Add("background-color", "yellow");
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "HideProgress();", true);

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
            txamt.Text = "0.00";
            txtTotal.Text = "0.00";
            ckmunicipality.Checked = false;
            //ckmunicipality_CheckedChanged(sender, e);
            //if (cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088")
            if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_WASH'")) || cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'")))  // by yanto 18-01-2020
            {
                DivPlate.Visible = true;
                if (hdemp.Value != "")
                {
                    lblLast.Text = bll.vLookUp("select   top(1) isnull(tcashout_attribute.attributevalue,0) " +
                                               "  from tcashout_attribute" +
                                               "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
                                               "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
                                               "  where tcashout_attribute.ids in ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
                                               "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
                                               "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
                                               "  order by tcashout_request.cashout_dt desc");
                }
            }
            else
            {
                DivPlate.Visible = false;
            }
            DivPlate.Visible = false;
        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void cbroutine_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            cbType = "cbTypeRoutine";
            vInitItemCashout(cbType);
            if (cbroutine.SelectedValue.ToString() == "NR")
            {
                cbcategory_SelectedIndexChanged(sender, e);
                if (cbinout.SelectedValue.ToString() == "O")
                {
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
                }
                if (cbcategory != null && cbinout.SelectedValue.ToString() != "H")
                {
                    if (cbcategory.SelectedValue.ToString() == "ADV")
                    {
                        cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
                    }
                    
                }

                if (cbinout.SelectedValue.ToString() == "H")
                {
                    cbcategory.Items.Insert(0, new ListItem("ADVANCED", "ADV"));
                    cbitem.Items.Insert(0, new ListItem("EMPLOYEE ADVANCE", bll.vLookUp("select qry_data from tmap_query where qry_cd='select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'")));
                    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
                    yellowLine1.Style.Add("background-color", "#ADFF2F");
                    yellowLine2.Style.Add("background-color", "#ADFF2F");
                    yellowLine3.Style.Add("background-color", "#ADFF2F");
                }
            }
            else if (cbroutine.SelectedValue.ToString() == "0")
            {
                //cbinout.SelectedValue = "H";

                cbcategory.Items.Remove(cbcategory.Items.FindByValue("0"));
                cbcategory.Items.Insert(0, new ListItem("Select", "0"));

                cbitem.Items.Remove(cbitem.Items.FindByValue("0"));
                cbitem.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                cbapproval.Items.Clear();
                cbapproval.CssClass = cd.csstextro;
                cbcategory_SelectedIndexChanged(sender, e);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btadd_Click(object sender, EventArgs e)
    {
        try
        {
            double dAmt = 0;
            if (!double.TryParse(txamt.Text, out dAmt))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Entry numeric only for amount!','Check amount to be cashout','warning');", true);
                return;
            }

            if (hdemp.Value.ToString() == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select PIC (Employee)!','Person In Charge','warning');", true);
                return;
            }

            List<cArrayList> arr = new List<cArrayList>();

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdattr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
                //if (cbitem.SelectedValue == "CHA00142" && lbattribute != null)
                //{
                //    if (lbattribute.Text == "Emp Code")
                //    {
                //        lbvalue.Text = bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPOffice'");
                //        hdemp.Value = lbvalue.Text;
                //    }
                //    else if (lbattribute.Text == "Purpose")
                //    {
                //        lbvalue.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd in ( select qry_data from tmap_query where qry_cd='EMPOffice')");
                //    }
                //}
                if (hdcashout.Value.ToString() != "" && hdcashout != null)
                {
                    string @param = hdcashout.Value.ToString();
                    if (lbattribute != null)
                    {
                        hdempadv.Value = bll.vLookUp("select at.attributevalue from tcashout_request x inner join (select a.*,b.attribute_nm from tcashout_attribute a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Emp Code'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
                        hdpurpose.Value = bll.vLookUp("select at.attributevalue from tcashout_request x inner join (select a.*,b.attribute_nm from tcashout_attribute a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Purpose'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
                    }
                    hdemp.Value = bll.vLookUp("select x.emp_cd from tcashout_request x inner join (select a.*,b.attribute_nm from tcashout_attribute a left join titemcashout_attribute b on a.ids=b.ids where a.ids in(select ids from titemcashout_attribute where attribute_nm in('Purpose'))and isnull(cashout_cd,'') like case when ('" + @param + "'  is not null and '" + @param + "' <>'0' and '" + @param + "' <>'') then ''+'" + @param + "'+'' else '%%' end)at on x.cashout_cd=at.cashout_cd");
                }
                if (lbattribute != null)
                {
                    if (lbattribute.Text == "Emp Code")
                    {
                        lbvalue.Text = bll.vLookUp("select emp_cd +' :: '+ emp_nm from tmst_employee where emp_cd = '" + hdempadv.Value + "'");
                    }
                    //if (lbattribute.Text == "Emp Code")
                    //{ 
                    //    if (hdemp.Value != "")
                    //    {
                    //        lbvalue.Text = bll.vLookUp("select emp_nm from tmst_employee where emp_cd = '" + hdemp.Value + "'");
                    //    }
                    //}
                    if (lbattribute.Text == "Purpose")
                    {
                        lbvalue.Text = bll.vLookUp("select itemco_cd +' :: '+ itemco_nm from tmst_itemcashout where itemco_cd ='" + hdpurpose.Value + "'");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsave_Click(object sender, EventArgs e)
    {
        try
        {
            double dAmt = 0;
            double dVat = 0; double dVatCut = 0; string sFileName = string.Empty;
            FileInfo fi = null;

            string sClosingCashier = "";
            double dCashierAmt = 0;

            sClosingCashier = bll.vLookUp("select 1 from tcashregister_closing where acknowledge in ('N','Y') and salespointcd='"+ Request.Cookies["sp"].Value.ToString() + "' and chclosing_dt=dbo.fn_getsystemdate('"+ Request.Cookies["sp"].Value.ToString() + "')");

            if (sClosingCashier.ToString() == "1")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Cashier Already closing or pending in Approval!','Plese Check ur cashier again!','warning');", true);
                return;
            }

            if (cbroutine.SelectedValue == "NR" && cbcategory.SelectedValue == "ADV")
            {
                if (hdempadv.Value.ToString() == "" || hdpurpose.Value.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attributes can not be empty!','Please Input Attributes!','warning');", true);
                    return;
                }
            }
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
            //if (cbinout.SelectedValue != "I" && cbroutine.SelectedValue != "R" && cbcategory.SelectedValue != "HEADOFFICE" && cbitem.SelectedValue != "CHA00142")
            if (cbinout.SelectedValue != "I" && cbroutine.SelectedValue != "R" && cbcategory.SelectedValue != "HEADOFFICE" && cbitem.SelectedValue != bll.vLookUp("select qry_data from tmap_query where qry_cd='CASH_IN_FROM_HO'"))

            {
                if (hdemp.Value.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC must be selected!','PIC','warning');", true);
                    return;
                }
            }
            //if ((cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088"))
            //if (cbitem.SelectedValue == "CHA00007" || cbitem.SelectedValue == "CHA00023" || cbitem.SelectedValue == "CHA00024"
            //    || cbitem.SelectedValue == "CHA00056" || cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088"
            //    || cbitem.SelectedValue == "CHA00109")
            if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='need_PIC_cashout'"))) //by yanto 19-01-2020
            {
                if (hdemp.Value.ToString() == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('PIC must be selected!','PIC','warning');", true);
                    return;
                }
                if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'"))
                {
                    foreach (GridViewRow row in grdattr.Rows)
                    {
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            Label lbattribute = (Label)row.FindControl("lbattribute");
                            Label lbvalue = (Label)row.FindControl("lbvalue");
                            if (lbattribute.Text == "KM")
                            {
                                if (lbvalue.Text == "" || lbvalue.Text == null || Convert.ToDecimal(lbvalue.Text) == 0)
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Additional information for cashout is not completeld (" + lbattribute.Text + ") !','Additional Info (" + lbattribute.Text + ") ','warning');", true);
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            if (!double.TryParse(txamt.Text, out dAmt))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amout can not empty!','Amount Cashout','warning');", true);
                return;
            }
            if (Convert.ToDecimal(txamt.Text) <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Amout can not zero or less than zero!','Amount Cashout','warning');", true);
                return;
            }

            // Add Block If Cashier Balance less than Amount 

            dCashierAmt = double.Parse(bll.vLookUp("select dbo.fn_getcashierbalance('"+ Request.Cookies["sp"].Value.ToString() + "')"));

            if (cbinout.SelectedValue.ToString() == "O")
            {
                if ((dCashierAmt - dAmt) < 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Balance Cashier : " + dCashierAmt.ToString() + " , Not Enough To Create this Cashout!','Please check your cashier balance!','warning');", true);
                    return;
                }
            }
               

            //// 

            if (cbitem.SelectedValue.ToString() == "" || cbitem.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Item cashout must be be selected!','Select Item Cashout','warning');", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Manual number must inserted!','Manual no','warning');", true);
                return;
            }


            bool isMunicipality = false;

            DataTable dt = new DataTable();
            dt = null;
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

            //if ((cbroutine.SelectedValue.ToString() == "NR") && (fucashout.FileName == "") && (cbinout.SelectedValue.ToString() == "O"))
            if ((fucashout.FileName == "") && (cbinout.SelectedValue.ToString() == "O"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Transaction Document must be uploaded!','Non Routine / Routine','warning');", true);
                return;
            }

            if (fucashout.HasFile)
            {
                fi = new FileInfo(fucashout.FileName);
                byte[] by = fucashout.FileBytes;
                if (by.Length > 1000000)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('File uploaded can not more than 1 MB!','Non Routine / Routine Cashout','warning');", true);
                    return;
                }
            }

            if (grdattr.Rows.Count > 0)
            {
                foreach (GridViewRow row in grdattr.Rows)
                {
                    if (row.RowState == DataControlRowState.Edit)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('There is attribute on edit state not yet update!','Update attribute','warning');", true);
                        return;
                    }
                    if (cbcategory.SelectedValue == "ADV")
                    {
                        string employeeNotUpdated = bll.vLookUp("select isnull(wca.attributevalue,'') from titemcashout_attribute ica right join twrk_cashoutattribute wca on ica.IDS=wca.IDS where attribute_nm='Emp Code' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                        if (employeeNotUpdated.ToString() == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attribute Employee on edit state not yet update!','Please Edit, Choose Value and Update the Attribute Value!','warning');", true);
                            return;
                        }
                        string purposeNotUpdated = bll.vLookUp("select isnull(wca.attributevalue,'') from titemcashout_attribute ica right join twrk_cashoutattribute wca on ica.IDS=wca.IDS where attribute_nm='Purpose' and usr_id='" + Request.Cookies["usr_id"].Value.ToString() + "'");
                        if (purposeNotUpdated.ToString() == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Attribute Purpose on edit state not yet update!','Please Edit, Choose Value and Update the Attribute Value!','warning');", true);
                            return;
                        }
                    }
                }
            }
            

            string sCashout = string.Empty;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_dt", DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
            //if ((cbinout.SelectedValue == "I" && cbroutine.SelectedValue == "R" && cbcategory.SelectedValue == "HEADOFFICE" && cbitem.SelectedValue == "CHA00142"))
            if ((cbinout.SelectedValue == "I" && cbroutine.SelectedValue == "R" && cbcategory.SelectedValue == "HEADOFFICE" && cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='CASH_IN_FROM_HO'")))
            {
                arr.Add(new cArrayList("@emp_cd", null));
            }
            else
            {
                arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
            }
            arr.Add(new cArrayList("@dept_cd", cbdept.SelectedValue.ToString()));
            arr.Add(new cArrayList("@remark", txremark.Text));
            arr.Add(new cArrayList("@sequenceno", cbremark.SelectedValue.ToString()));
            if (ClaimCashout.Visible.Equals(true))
            {
                arr.Add(new cArrayList("@ClaimCashout", ddlClaimCashout.SelectedValue.ToString()));
            }
            if (chvat.SelectedValue == "VAT")
            {
                dVatCut = Convert.ToDouble(bll.sGetControlParameter("vat"));
                dVat = Convert.ToDouble(lbvat.Text);
                dAmt = Convert.ToDouble(txamt.Text);
                arr.Add(new cArrayList("@amt", dAmt));
                arr.Add(new cArrayList("@vat_amt", dVat));
                arr.Add(new cArrayList("@tax_no_supp", hdtax.Value.ToString()));
            }
            else
            {
                arr.Add(new cArrayList("@amt", dAmt));
                arr.Add(new cArrayList("@vat_amt", 0));
            }
            arr.Add(new cArrayList("@municipality", Convert.ToDouble(lblmunicipality.Text)));
            if (cbinout.SelectedValue.ToString() == "O" || cbinout.SelectedValue.ToString() == "H")
            {
                if (cbroutine.SelectedValue.ToString() == "NR")
                {
                    arr.Add(new cArrayList("@cashout_sta_id", "N"));
                }
                else if (cbroutine.SelectedValue.ToString() == "R")
                {
                    if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='cashdepositho'")))
                    {
                        arr.Add(new cArrayList("@cashout_sta_id", "N"));
                    }
                    else
                    {
                        arr.Add(new cArrayList("@cashout_sta_id", "A"));
                    }
                }
            }
            else if (cbinout.SelectedValue.ToString() == "I")
            {
                arr.Add(new cArrayList("@cashout_sta_id", "A"));
            }
            arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@approval_cd", cbapproval.SelectedValue.ToString()));
            arr.Add(new cArrayList("@itemco_cd", cbitem.SelectedValue.ToString()));
            arr.Add(new cArrayList("@manualno", txmanualno.Text));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertCashoutRequest(arr, ref sCashout);

            //if (cbitem.SelectedValue == "CHA00007" || cbitem.SelectedValue == "CHA00023" || cbitem.SelectedValue == "CHA00024"
            //    || cbitem.SelectedValue == "CHA00056" || cbitem.SelectedValue == "CHA00025" || cbitem.SelectedValue == "CHA00088"
            //    || cbitem.SelectedValue == "CHA00109")
            if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='need_PIC_cashout'"))) //by yanto 19-01-2020
            {
                arr.Clear();
                var vhc_cd = ddlPlateNo.SelectedValue.ToString();
                string[] vhc = vhc_cd.Split('-');
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@emp_cd", hdemp.Value));
                arr.Add(new cArrayList("@cashout_cd", sCashout));
                arr.Add(new cArrayList("@vhc_cd", vhc[0]));
                if (vhc.Length == 1)
                {
                    arr.Add(new cArrayList("@vhc_cd_typ", "Single"));

                }
                else if (vhc.Length == 2)
                {
                    arr.Add(new cArrayList("@vhc_cd_typ", "Multiple"));
                }
                bll.vInsertCashoutRequest_Car(arr);
            }

            txsysno.Text = sCashout;
            if (cbinout.SelectedValue.ToString() == "H")
            {
                arr.Clear();
                arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
                dAmt = Convert.ToDouble(txamt.Text);
                arr.Add(new cArrayList("@amt", dAmt));
                arr.Add(new cArrayList("@transactionDate", DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)));
                arr.Add(new cArrayList("@sta_id", "N"));
                arr.Add(new cArrayList("@createdby", Request.Cookies["usr_id"].Value.ToString()));
                arr.Add(new cArrayList("@cashout_cd", sCashout));
                bll.vInsertCashoutRequestHTE(arr);
            }
            // Walking on the moon
            arr.Clear();
            if (cbinout.SelectedValue.ToString() == "I")
            {
                arr.Add(new cArrayList("@trn_typ", "CASHIN"));
            }
            else if (cbinout.SelectedValue.ToString() == "O")
            {
                arr.Add(new cArrayList("@trn_typ", "CASHOUT"));
            }
            //arr.Add(new cArrayList("@refno", sCashout));
            //if (cbinout.SelectedValue.ToString() == "I")
            //{
            //    bll.vBatchAccTransactionLog(arr);
            //}
            //else if (cbinout.SelectedValue.ToString() == "O")
            //{
            //    if (cbroutine.SelectedValue.ToString() == "R")
            //    {
            //        bll.vBatchAccTransactionLog(arr);
            //    }
            //}
            //End of the moon _____________________________________________________________________
            lbstatus.Text = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_sta_id' and fld_valu=(select cashout_sta_id from tcashout_request where cashout_cd='" + sCashout + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "')");
            //if ((cbroutine.SelectedValue.ToString() == "NR") && (cbinout.SelectedValue.ToString() == "O"))
            //{
                //string strdate = Convert.ToDateTime(DateTime.ParseExact(dtcashout.Text, "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("yyyyMM");
                // FileInfo fi = new FileInfo(fucashout.FileName);
                
                if ((fucashout.FileName != "") || (fucashout.FileName.Equals(null)))
                {
                    string ext = fi.Extension;
                    sFileName = sCashout + ext;
                    arr.Clear();
                    arr.Add(new cArrayList("@cashoutfile", sCashout + ext));
                    arr.Add(new cArrayList("@cashout_cd", sCashout));
                    arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
                    bll.vUpdateCashoutRequest(arr);
                    fucashout.SaveAs(bll.sGetControlParameter("image_path") + @"\cashout\" + sFileName);
                }
            //}

            //Walking to the moon
            arr.Clear();
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@cashout_cd", sCashout));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertCashoutAttribute(arr);
            //______________________________________________________________________________
            //if ((cbroutine.SelectedValue.ToString() == "NR") && (cbinout.SelectedValue.ToString() == "O"))
            //{
                //int nrnd = 0;
                //Random rnd = new Random();
                //nrnd = rnd.Next(1000, 9999);
                //string stoken = bll.vLookUp("select short_ip from tmst_salespoint where salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'") + nrnd.ToString();
                //List<string> lapproval = bll.lGetApproval(cbapproval.SelectedValue.ToString());
                //string sMsg = "#Cashout request from " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + ", no." + sCashout + ", amt  " + txamt.Text +
                //     ", for " + cbitem.SelectedItem.Text + ", do you want to approved : (Y/N)" + stoken;
                //arr.Clear();
                //arr.Add(new cArrayList("@token", stoken));
                //arr.Add(new cArrayList("@doc_no", sCashout));
                //arr.Add(new cArrayList("@doc_typ", "cashout"));
                //arr.Add(new cArrayList("@to", lapproval[0]));
                //arr.Add(new cArrayList("@msg", sMsg.TrimEnd()));
                //bll.vInsertSmsOutbox(arr);

                // Sending Email 
                //string sSubject = ""; string sMessage = "";
                //string sfile_attachment = sFileName;
                ////string slink_ho = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_ho'");
                //string slink_branch = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm='link_branch'");
                //string stitle = bll.vLookUp("select fld_desc from tfield_value where fld_nm='job_title_cd' and fld_valu=( select job_title_cd from tmst_employee where emp_cd='" + hdemp.Value + "')");
                //string cashout_typ = cbcategory.SelectedValue.ToString();
                //string scashout_typ = bll.vLookUp("select fld_desc from tfield_value where fld_nm='cashout_typ' and fld_valu='" + cashout_typ + "'");
                //string ssalespoint = bll.vLookUp("select salespointcd +'-'+salespoint_nm from tmst_salespoint where salespointcd=" + Request.Cookies["sp"].Value.ToString());
                //string screator_id = Request.Cookies["usr_id"].Value.ToString();
                //string screator_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + screator_id + "'");
                //string spic_nm = bll.vLookUp("select emp_cd+'-'+emp_nm from tmst_employee where  emp_cd='" + hdemp.Value.ToString() + "'");
                //sSubject = "#New Cash Request Branch " + bll.sGetSalespointname(Request.Cookies["sp"].Value.ToString()) + " has been created ";
                ////sMessage = "Request No. " + sCasregNo + " created by " + lbemp_nm.Text + ", with the item " + lbitemcode.Text + 
                //sMessage = "<table><tr><td colspan=3>#Cashout Request Approval</td></tr><tr><td></td></tr><tr><td>Salespoint</td><td>:</td><td>" + ssalespoint + "</td></tr><tr><td>Request No</td><td>:</td><td>" + sCashout + "</td><td>PIC</td><td>:</td><td>" + spic_nm + "</td></tr><tr><td>Created</td><td>:</td><td>" + screator_nm + "</td><td>Position </td><td>:</td><td>" + stitle + "</td></tr><tr><td>Trans Type</td><td>:</td><td>" + scashout_typ + "</td><td>Item</td><td>:</td><td>" + cbitem.SelectedItem.Text + "</td></tr><tr><td><font color='#FF0000'>Req Amount / VAT</font></td><td>:</td><td><font color='#FF0000'>" + txamt.Text + "/" + lbvat.Text + "</font></td></tr><tr><td></td></tr><tr><td>Note</td><td>:</td><td colspan=4>1. See Attached file </td></tr><tr><td></td><td></td><td colspan=4>2. This Approval to make sure all detail as attached is match with entry request.</td></tr><tr><td></td></tr><tr><td></td></tr></table>" +
                //"<p> Please Click this  for approved : <a href='" + slink_branch + "/landingpage2.aspx?src=cashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sCashout + "&sta=A'>Approve</a>, or for rejected please click <a href='" + slink_branch + "/landingpage2.aspx?src=cashout&salespointcd=" + Request.Cookies["sp"].Value.ToString() + "&ids=" + sCashout + "&sta=R'>Reject</a></p>" +
                //" \n\r\n\r\n\r\n Wazaran Admin";
                //bll.vSendMail(lapproval[1], sSubject,sMessage, "CO20409172345.gif");
                //arr.Clear();
                //arr.Add(new cArrayList("@trxcd", "cashout"));
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_no", sCashout));
                //bll.vInsertEmailSent(arr);
                //arr.Clear();
                //arr.Add(new cArrayList("@token", nrnd.ToString()));
                //arr.Add(new cArrayList("@doc_typ", "cashout"));
                //arr.Add(new cArrayList("@to", lapproval[1]));
                //arr.Add(new cArrayList("@doc_no", sCashout));
                //arr.Add(new cArrayList("@emailsubject", sSubject));
                //arr.Add(new cArrayList("@msg", sMessage));
                //arr.Add(new cArrayList("@file_attachment", sfile_attachment));
                //bll.vInsertEmailOutbox(arr);
                //************************ END OF ENTRY
            //}
            cbitem.CssClass = cd.csstextro;
            cbremark.CssClass = cd.csstextro;
            cbroutine.CssClass = cd.csstextro;
            cbinout.CssClass = cd.csstextro;
            cbapproval.CssClass = cd.csstextro;
            txamt.CssClass = cd.csstextro;
            txmanualno.CssClass = cd.csstextro;
            txpic.CssClass = cd.csstextro;
            txremark.CssClass = cd.csstextro;
            cbdept.CssClass = cd.csstextro;
            txtaxno.CssClass = cd.csstextro;
            btsave.CssClass = "btn btn-default ro";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('New Cashout has been created!','" + txsysno.Text + "','success');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void New_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_cashregoutentry2.aspx");
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
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
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
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            HiddenField hdids = (HiddenField)grdattr.Rows[e.RowIndex].FindControl("IDS");
            TextBox txvalue = (TextBox)grdattr.Rows[e.RowIndex].FindControl("txvalue");
            TextBox txvalue2 = (TextBox)grdattr.Rows[e.RowIndex].FindControl("txvalue2");
            DropDownList cbvalue = (DropDownList)grdattr.Rows[e.RowIndex].FindControl("cbvalue");
            arr.Add(new cArrayList("@IDS", hdids.Value.ToString()));
            arr.Add(new cArrayList("@usr_id", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
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
            bll.vUpdateWRKCashoutAttribute(arr);
            grdattr.EditIndex = -1;
            vInitGrid();
            //if (cbitem.SelectedValue.Equals("CHA00048") || cbitem.SelectedValue.Equals("CHA00080"))
            if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'") || cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_IN'")) //BY YANTO 19-01-2020
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "callJSFunction", "SelectPcPIC();", true);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void btPcPIC_Click(object sender, EventArgs e)
    {
        try
        {
            //if (cbitem.SelectedValue.Equals("CHA00048") || cbitem.SelectedValue.Equals("CHA00080"))
            if (cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_OUT'") || cbitem.SelectedValue == bll.vLookUp("select qry_data from tmap_query where qry_cd='EMPLOYEE_ADVANCE_IN'")) //BY YANTO 19-01-2020
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
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdattr_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdattr.EditIndex = -1;
        vInitGrid();
    }
    protected void btprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "openreport('fm_report2.aspx?src=croreq&no=" + txsysno.Text + "');", true);
    }
    protected void cbinout_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (cbinout.SelectedValue != "H")
            {
                cbType = "cbTypeInOut";
                vInitItemCashout(cbType);
                //if ((cbcategory != null && cbcategory.SelectedValue != "") && !(cbroutine.SelectedValue == "R" && cbcategory.SelectedValue == "EMPLOYEE")) { 
                if ((cbcategory != null && cbcategory.SelectedValue != "") && cbcategory.SelectedValue != "EMPLOYEE" && cbcategory.SelectedValue != "DISTRIBUTION" && cbroutine.SelectedValue == "NR")
                {
                    cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
                }
            }
            else
            {
                cbinout.SelectedValue = "H";
                cbroutine.SelectedValue = "NR";
                bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
                cbcategory.SelectedValue = "ADV";

                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@cashout_typ", cbcategory.SelectedValue.ToString()));
                arr.Add(new cArrayList("@routine", cbroutine.SelectedValue.ToString()));
                arr.Add(new cArrayList("@inout", "O"));
                bll.vBindingComboToSp(ref cbitem, "sp_tmst_itemcashout_get", "itemco_cd", "itemco_nm", arr);
                //cbitem_SelectedIndexChanged(this, EventArgs.Empty);

                cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");
                yellowLine1.Style.Add("background-color", "#ADFF2F");
                yellowLine2.Style.Add("background-color", "#ADFF2F");
                yellowLine3.Style.Add("background-color", "#ADFF2F");

                cbroutine.Items.Remove(cbroutine.Items.FindByValue("0"));

                SqlDataReader rdr = null;
                //SqlConnection con = null;
                SqlCommand cmd = null;
                //con = new SqlConnection(ConfigurationManager
                //.ConnectionStrings["connstr"].ConnectionString);

                //con.Open();
                string CommandText;
                CommandText = "select * from tfield_value where fld_nm='cashout_typ' and fld_valu not in('ADV') ";
                cmd = new SqlCommand(CommandText);
                //cmd.Connection = con;
                cmd.Connection = cd.getConnection();

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string itemToRmv = rdr["fld_valu"].ToString();

                    cbcategory.Items.Remove(cbcategory.Items.FindByValue(itemToRmv));
                }
                cbitem_SelectedIndexChanged(this, EventArgs.Empty);
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

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void grdattr_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void btrefresh_Click(object sender, EventArgs e)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader rs = null;
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_cd", hdcashout.Value.ToString()));
            bll.vGetCashoutRequest(arr, ref rs);
            while (rs.Read())
            {
                txsysno.Text = rs["cashout_cd"].ToString();
                txamt.Text = rs["amt"].ToString();
                txmanualno.Text = rs["manualno"].ToString();
                lbstatus.Text = bll.sGetFieldValue("cashout_sta_id", rs["cashout_sta_id"].ToString());
                txpic.Text = rs["emp_cd"].ToString();
                cbroutine.SelectedValue = bll.vLookUp("select routine from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");
                cbinout.SelectedValue = bll.vLookUp("select inout from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");
                cbcategory.SelectedValue = bll.vLookUp("select cashout_typ from tmst_itemcashout where itemco_cd='" + rs["itemco_cd"].ToString() + "'");

                cbType = "none";
                vInitItemCashout(cbType);
                cbitem.SelectedValue = rs["itemco_cd"].ToString();
                txremark.Text = rs["remark"].ToString();
                cbdept.CssClass = cd.csstextro;
                txamt.CssClass = cd.csstextro;
                txmanualno.CssClass = cd.csstextro;
                txpic.CssClass = cd.csstextro;
                cbinout.CssClass = cd.csstextro;
                cbroutine.CssClass = cd.csstextro;
                cbitem.CssClass = cd.csstextro;
                cbcategory.CssClass = cd.csstextro;
                txremark.CssClass = cd.csstextro;
                cbremark.CssClass = cd.csstextro;
                cbapproval.CssClass = cd.csstextro;
                chvat.CssClass = cd.csstextro;
                dtcashout.CssClass = cd.csstextro;
                btsave.CssClass = "btn btn-default ro";
                ddlClaimCashout.CssClass = cd.csstextro;

                //lbbalanceco.Text= bll.vLookUp("select amt from tcashout_request where cashout_cd='" + rs["cashout_cd"].ToString() + "'");

                System.Data.SqlClient.SqlDataReader rs2 = null;
                List<cArrayList> arr2 = new List<cArrayList>();
                arr2.Add(new cArrayList("@emp_cd", null));
                arr2.Add(new cArrayList("@PettycashRefno", hdcashout.Value.ToString()));
                arr2.Add(new cArrayList("@claim", null));
                bll.vGetPettyCashEmp2(ref rs2, arr2);
                while (rs2.Read())
                {
                    lbbalanceco.Text = rs2["totalBalance"].ToString();
                    if (cbcategory.SelectedValue == "ADV")
                    {
                        //ddlClaimCashout.SelectedValue = rs2["ClaimCashout"].ToString();
                        ClaimCashout.Visible = true;
                    }
                    else
                    {
                        ClaimCashout.Visible = false;
                    }
                }
                rs2.Close();
            }
            rs.Close();

            //cbcategory.Items.FindByValue("ADV").Attributes.Add("style", "background-color:#ADFF2F");

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
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void txamt_TextChanged(object sender, EventArgs e)
    {
        chvat_CheckedChanged(sender, e);
    }
    protected void chvat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            double dAmt, dVat, dValue;
            txamt.CssClass = cd.csstextro;
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
                    dVat = 0; dValue = 0;
                    dAmt = Double.Parse(txamt.Text);
                    if (grdtax.Rows.Count > 0)
                    {
                        dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcashout_request_tax where cashout_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
                        dValue = dAmt + dVat;
                    }
                    else
                    {
                        dValue = dAmt;
                    }
                    //double dPctVat = Convert.ToDouble(bll.sGetControlParameter("vat"));
                    //double dValue = (1 / (1 + dPctVat)) * dAmt;
                    //double dVat = dAmt - dValue;
                    //double dVat = dPctVat * dAmt;
                    txamt.Text = Math.Round(dValue, 5, MidpointRounding.AwayFromZero).ToString(); // String.Format("{0:0.00}", dValue);
                    lbvat.Text = Math.Round(dVat, 5, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);
                }
            }
            else if (chvat.SelectedValue == "NONVAT")
            {

                if (txamt.Text == "")
                {
                    txamt.Text = "0";
                    lbvat.Text = "0";
                }
                else
                {
                    dVat = Convert.ToDouble(lbvat.Text);
                    dAmt = Convert.ToDouble(txamt.Text);
                    lbvat.Text = "0";
                    txamt.Text = (dAmt + dVat).ToString();
                }

                // Support for Multiple Tax
                showTax.Attributes.Remove("style");

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

            txtTotal.Text = txamt.Text;
            //ckmunicipality_CheckedChanged(sender, e);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    //protected void btsysno_Click(object sender, EventArgs e)
    //{
    //    bll.vBindingFieldValueToCombo(ref cbinout, "inout");
    //    cbinout.SelectedValue = "O";
    //    bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
    //    dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
    //    dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
    //    lbcashier.Text = bll.vLookUp("select dbo.fn_getcashierbalance()").ToString();
    //    bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);
    //}

    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    [WebMethod]
    //public static void btsysno_RefreshPage()
    public void btsysno_Click(object sender, EventArgs e)
    {
        try
        {
            bll.vBindingFieldValueToCombo(ref cbinout, "inout");
            cbinout.SelectedValue = "O";
            bll.vBindingFieldValueToCombo(ref cbroutine, "routine");
            dtcashout_CalendarExtender.StartDate = System.DateTime.ParseExact(Request.Cookies["waz_dt"].Value.ToString(), "d/M/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            dtcashout.Text = Request.Cookies["waz_dt"].Value.ToString();
            lbcashier.Text = bll.vLookUp("select dbo.fn_getcashierbalance('" + Request.Cookies["sp"].Value.ToString() + "')").ToString();
            //lbcashier.Text = bll.vLookUp("select sum(amt)amt from(select cash_dt,amt from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='I')union all select cash_dt,amt-1 from tcashregister where itemco_cd in (select itemco_cd from tmst_itemcashout where inout='O'))x where cash_dt>='2018-01-01'").ToString();
            bll.vBindingFieldValueToCombo(ref cbcategory, "cashout_typ", false);

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "CallMyFunction", "PopupCenter('lookupcashout.aspx', 'xtf', '900', '500');", true);

            if (hdcashout.Value.ToString() == null)
            {
                cbType = "none";
                vInitItemCashout(cbType);
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    //[WebMethod]
    //public static string GetBalancePerCashOut(string sVar)
    //{
    //    string result;

    //    SqlConnection con = new SqlConnection(ConfigurationManager
    //    .ConnectionStrings["SBTCDBConnectionString"].ConnectionString);
    //    SqlCommand SelectCommand = new SqlCommand("select amt from tcashout_request where cashout_cd='"+ sVar + "'", con);

    //    con.Open();
    //    result = (string)SelectCommand.ExecuteScalar().ToString();
    //    con.Close();

    //    return result;
    //}

    protected void ckmunicipality_CheckedChanged(object sender, EventArgs e)
    {
        try
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
            //if (isMunicipality == true)
            //{
            //    ckmunicipality.Checked = true;
            //}
            if (ckmunicipality.Checked == true && isMunicipality == true)
            {

                double dAmount = 0;
                if (!double.TryParse(txamt.Text, out dAmount))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Qty must be numeric','Check Amount','warning');", true);
                    return;
                }

                decimal municipalityPer = Convert.ToDecimal(bll.sGetControlParameter("municipalityTax"));

                decimal totAmt = Convert.ToDecimal(Convert.ToDecimal(txamt.Text));
                lblmunicipality.Text = Convert.ToString(Convert.ToDecimal(municipalityPer * totAmt) / 100);
            }
            // we can't  force to apply Municipality tax
            //else if(isMunicipality==true && ckmunicipality.Checked == false)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "sweetAlert('Please select municipality tax!','Municipality tax','warning');", true);

            //}

            txtTotal.Text = Convert.ToString(Convert.ToDecimal(txamt.Text) + Convert.ToDecimal(lbvat.Text) + Convert.ToDecimal(lblmunicipality.Text));

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
    protected void btsearchPlate_Click(object sender, EventArgs e)
    {
        try
        {
            // Auto bind department;
            string dep = bll.vLookUp("select dept_cd from tmst_employee where  emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
            string depExist = bll.vLookUp("select dept_nm from tmst_department  where deleted=0 and dept_cd='" + dep + "'");
            string depExist_cd = bll.vLookUp("select dept_cd from tmst_department  where deleted=0 and dept_cd='" + dep + "'");
            if (depExist == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Please update department for employee " + hdemp.Value + " .','Please Contact Wazaran Admin ','warning');", true);
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
            if (cbitem.SelectedValue.ToString().Contains(bll.vLookUp("select qry_data from tmap_query where qry_cd='need_PIC_cashout'"))) //by yanto 19-01-2020
            {
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@emp_cd", hdemp.Value.ToString()));
                arr.Add(new cArrayList("@slaespointcd", Request.Cookies["sp"].Value.ToString()));
                bll.vBindingComboToSp(ref ddlPlateNo, "sp_tmst_tmst_vehicleOther_get", "vhc_cd", "vehicleName", arr);

                string sPlate = bll.vLookUp("select vhc_cd from tmst_vehicle where emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
                if (sPlate == "")
                {
                    sPlate = bll.vLookUp("select vhc_cd from tmst_vehicle_other where emp_cd='" + hdemp.Value.ToString() + "' and deleted=0");
                }
                //txPlateNo.Text = sPlate;
                if (sPlate == "")
                {
                    //txpic.Text = "";
                    //hdemp.Value = "";
                    //txPlateNo.Text = "";
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Plate No. is not Available.','Please Contact Wazaran Admin','warning');", true);
                    //return;
                }
                if (hdemp.Value != "")
                {
                    lblLast.Text = bll.vLookUp("select top(1)  isnull(tcashout_attribute.attributevalue,0) " +
                                               "  from tcashout_attribute" +
                                               "  inner join tcashout_request_car_info    on tcashout_request_car_info.cashout_cd = tcashout_attribute.cashout_cd" +
                                               "  inner join tcashout_request on tcashout_request.cashout_cd = tcashout_attribute.cashout_cd" +
                                               //"  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = 'CHA00088' )" +
                                               "  where tcashout_attribute.ids = ( select ids from titemcashout_attribute where itemco_cd = '" + bll.vLookUp("select qry_data from tmap_query where qry_cd='itemco_cd_CAR_MAINTENANCE'") + "' )" +
                                               "  and tcashout_request.emp_Cd = '" + hdemp.Value + "'" +
                                               "  and tcashout_request_car_info.vhc_cd = '" + ddlPlateNo.SelectedValue.ToString() + "'" +
                                               "  order by tcashout_request.cashout_dt desc");
                }
            }

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionListTax(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        System.Data.SqlClient.SqlDataReader rs = null;
        List<string> lCustomer = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string sCustomer = string.Empty;
        arr.Add(new cArrayList("@tax_cd", prefixText));
        arr.Add(new cArrayList("@tax_type", "E"));
        bll.vGetMstTax(arr, ref rs);
        while (rs.Read())
        {
            sCustomer = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["tax_cd"].ToString() + "-" + rs["tax_desc"], rs["tax_cd"].ToString());
            lCustomer.Add(sCustomer);
        }
        return (lCustomer.ToArray());
    }

    protected void grdtax_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void bttax_Click(object sender, EventArgs e)
    {
        try
        {
            if (txamt.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alrt2", "sweetAlert('Amount can not empty.','Please input amount.','warning');", true);
                return;
            }
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@tax_cd", hdtaxall.Value));
            arr.Add(new cArrayList("@amount", txamt.Text.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vInsertCashoutRequest_Tax(arr);
            arr.Clear();
            arr.Add(new cArrayList("@cashout_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtax, "sp_tcashout_request_tax_get", arr);
            hdtaxall.Value = "";
            txtaxall.Text = "";

            double dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcashout_request_tax where cashout_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
            double dAmt = Double.Parse(txamt.Text);
            txtTotal.Text = Convert.ToString(dAmt + dVat);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }

    protected void grdtax_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            double dVat = 0, dValue = 0, dAmt = Double.Parse(txamt.Text.ToString());
            if (grdtax.Rows.Count > 0)
            {
                dVat = Double.Parse(bll.vLookUp("select sum(amount) from tcashout_request_tax where cashout_cd='" + Request.Cookies["usr_id"].Value.ToString() + "' and salespointcd='" + Request.Cookies["sp"].Value.ToString() + "'"));
                dValue = dAmt - dVat;
            }
            txamt.Text = Math.Round(dValue, 5, MidpointRounding.AwayFromZero).ToString(); // String.Format("{0:0.00}", dValue);
            lbvat.Text = Math.Round(dVat, 5, MidpointRounding.AwayFromZero).ToString();// String.Format("{0:0.00}", dVat);

            HiddenField tax_cd = (HiddenField)grdtax.Rows[e.RowIndex].FindControl("tax_cd");
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@cashout_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            arr.Add(new cArrayList("@tax_cd", tax_cd.Value));
            bll.vDeleteCashoutRequest_Tax(arr);
            arr.Clear();
            arr.Add(new cArrayList("@cashout_cd", Request.Cookies["usr_id"].Value.ToString()));
            arr.Add(new cArrayList("@salespointcd", Request.Cookies["sp"].Value.ToString()));
            bll.vBindingGridToSp(ref grdtax, "sp_tcashout_request_tax_get", arr);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Data Deleted successfully !','error');", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setshow", "dvshow.setAttribute('class','divhid');", true);

        }
        catch (Exception ex)
        {
            Session["error"] = "500" + " - " + bll.sGetFieldValue("error", "500");
            bll.vHandledError(ref ex, Request.Cookies["sp"].Value.ToString()+" : fm_cashregoutentry2");
            Response.Redirect("fm_ErrorPage.aspx");
        }
    }
}