using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml;
public partial class fm_CustomerTransaction : System.Web.UI.Page
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
            arr.Clear();
            arr.Add(new cArrayList("@reasn_typ", Convert.ToString("CustCNDN")));
            bll.vBindingComboToSp(ref ddlReason, "sp_tmst_reason_get", "reasn_cd", "reasn_nm", arr);
            arr.Clear();
            arr.Add(new cArrayList("@fld_nm", Convert.ToString("cust_trxn_type")));
            bll.vBindingComboToSp(ref ddlEmpTrxnType, "sp_tfield_value_get", "fld_valu", "fld_desc", arr);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tabErr", "tabErr();", true);
            txcust.CssClass = "form-control";
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
        if (hdcust.Value == null)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Customer is not Selected ','Please Select Customer!','warning');", true);
            return;
        }
        else
        { arr.Add((new cArrayList("@Emp_CD", hdcust.Value))); }

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

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        cbll bll = new cbll();
        HttpCookie cok;
        cok = HttpContext.Current.Request.Cookies["sp"];
        List<string> lcust = new List<string>();
        List<cArrayList> arr = new List<cArrayList>();
        string scust = string.Empty;
        System.Data.SqlClient.SqlDataReader rs = null;
        arr.Add(new cArrayList("@cust_cd", prefixText));
        //arr.Add(new cArrayList("@salesman_cd", contextKey));
        arr.Add(new cArrayList("@salespointcd", cok.Value.ToString()));
        // bll.vSearchCustomerBySales(arr, ref rs);
        bll.vSearchMstCustomerInRPS(arr, ref rs);
        while (rs.Read())
        {
            scust = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(rs["cust_cd"].ToString() + "-" + rs["cust_nm"].ToString(), rs["cust_cd"].ToString());
            lcust.Add(scust);
        }
        rs.Close();

        return (lcust.ToArray());
    }
    protected void btsearchcust_Click(object sender, EventArgs e)
    {
        
    }



    public static string ReverseGeoLoc(string longitude, string latitude,
        out string Address_ShortName,
        out string Address_country,
        out string Address_administrative_area_level_1,
        out string Address_administrative_area_level_2,
        out string Address_administrative_area_level_3,
        out string Address_colloquial_area,
        out string Address_locality,
        out string Address_sublocality,
        out string Address_neighborhood)
    {

        Address_ShortName = "";
        Address_country = "";
        Address_administrative_area_level_1 = "";
        Address_administrative_area_level_2 = "";
        Address_administrative_area_level_3 = "";
        Address_colloquial_area = "";
        Address_locality = "";
        Address_sublocality = "";
        Address_neighborhood = "";

        XmlDocument doc = new XmlDocument();

        try
        {
            doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false");
            XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
            if (element.InnerText == "ZERO_RESULTS")
            {
                return ("No data available for the specified location");
            }
            else
            {

                element = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");

                string longname = "";
                string shortname = "";
                string typename = "";
                bool fHit = false;


                XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        longname = xn["long_name"].InnerText;
                        shortname = xn["short_name"].InnerText;
                        typename = xn["type"].InnerText;


                        fHit = true;
                        switch (typename)
                        {
                            //Add whatever you are looking for below
                            case "country":
                                {
                                    Address_country = longname;
                                    Address_ShortName = shortname;
                                    break;
                                }

                            case "locality":
                                {
                                    Address_locality = longname;
                                    //Address_locality = shortname; //Om Longname visar sig innehålla konstigheter kan man använda shortname istället
                                    break;
                                }

                            case "sublocality":
                                {
                                    Address_sublocality = longname;
                                    break;
                                }

                            case "neighborhood":
                                {
                                    Address_neighborhood = longname;
                                    break;
                                }

                            case "colloquial_area":
                                {
                                    Address_colloquial_area = longname;
                                    break;
                                }

                            case "administrative_area_level_1":
                                {
                                    Address_administrative_area_level_1 = longname;
                                    break;
                                }

                            case "administrative_area_level_2":
                                {
                                    Address_administrative_area_level_2 = longname;
                                    break;
                                }

                            case "administrative_area_level_3":
                                {
                                    Address_administrative_area_level_3 = longname;
                                    break;
                                }

                            default:
                                fHit = false;
                                break;
                        }


                        if (fHit)
                        {
                            Console.Write(typename);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\tL: " + longname + "\tS:" + shortname + "\r\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }

                    catch (Exception e)
                    {
                        //Node missing either, longname, shortname or typename
                        fHit = false;
                        Console.Write(" Invalid data: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tX: " + xn.InnerXml + "\r\n");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }


                }

                //Console.ReadKey();
                return (element.InnerText);
            }

        }
        catch (Exception ex)
        {
            return ("(Address lookup failed: ) " + ex.Message);
        }
    }
}