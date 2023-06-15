using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class fm_customer : System.Web.UI.Page
{
    cbll bll = new cbll();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["custCD"] != null)
            {
                string scustCD = Request.QueryString["custCD"].ToString();
                SqlDataReader rs = null;
                List<cArrayList> arr = new List<cArrayList>();
                arr.Add(new cArrayList("@custcd", scustCD));
                bll.vGetMstcustomer2(arr, ref rs);
                while (rs.Read())
                {
                    img.ImageUrl = @"/image/" + rs["identitySignaturePath"].ToString();
                } rs.Close();
            }
            bll.vBindingFieldValueToCombo(ref cbCusGrCD, "CusGrCD");
            bll.vBindingFieldValueToCombo(ref cbOtlCD, "OtlCD");
            bll.vBindingFieldValueToCombo(ref cbPrLevCD, "PrLevCD");
            bll.vBindingFieldValueToCombo(ref cbCusClCD, "CusClCD");
            bll.vBindingFieldValueToCombo(ref cbTermCD, "payment_term");
                            
        }
    }

    protected void btsave_Click(object sender, EventArgs e)
    {
        string sImagePath = bll.sGetControlParameter("identitySignaturePath");
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@CustCD", txtCustCD.Text));
        arr.Add(new cArrayList("@CustNM", txtCustNM.Text));
        arr.Add(new cArrayList("@CustSN", txtCustSN.Text));
        arr.Add(new cArrayList("@BillNM", txtBillNM.Text));
        arr.Add(new cArrayList("@Cperson", txtCperson.Text));
        arr.Add(new cArrayList("@Birthday", txtBirthday.Text));
        arr.Add(new cArrayList("@Addr1", txtAddr1.Text));
        arr.Add(new cArrayList("@Addr2", txtAddr2.Text));
        arr.Add(new cArrayList("@Addr3", txtAddr3.Text));
        arr.Add(new cArrayList("@Phone", txtPhone.Text));
        arr.Add(new cArrayList("@Fax", txtFax.Text));
        arr.Add(new cArrayList("@City", txtCity.Text));
        arr.Add(new cArrayList("@distCD", txtdistCD.Text));
        arr.Add(new cArrayList("@Country", txtCountry.Text));
        arr.Add(new cArrayList("@MarketCD", txtMarketCD.Text));
        arr.Add(new cArrayList("@BlokCD", txtBlokCD.Text));
        arr.Add(new cArrayList("@DlvrCD", txtDlvrCD.Text));
        arr.Add(new cArrayList("@PostCD", txtPostCD.Text));
        arr.Add(new cArrayList("@Latitude", txtLatitude.Text));
        arr.Add(new cArrayList("@Longitude", txtLongitude.Text));
        arr.Add(new cArrayList("@SalesPointCD", txtSalesPointCD.Text));
        arr.Add(new cArrayList("@CusGrCD", cbCusGrCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@CusClCD", cbCusClCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@CustMapCd", txtCustMapCd.Text));
        arr.Add(new cArrayList("@BillCD", txtBillCD.Text));
        arr.Add(new cArrayList("@ShipCD", txtShipCD.Text));
        arr.Add(new cArrayList("@PrLevCD", cbPrLevCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@OtlCD", cbOtlCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@TermCD", cbTermCD.SelectedValue.ToString()));
        arr.Add(new cArrayList("@TaxCD", txtTaxCD.Text));
        arr.Add(new cArrayList("@ActCD", txtActCD.Text));
        arr.Add(new cArrayList("@CreateDT", txtCreateDT.Text));
        arr.Add(new cArrayList("@distance", txtdistance.Text));
        arr.Add(new cArrayList("@identityID", txtidentityID.Text));
        arr.Add(new cArrayList("@identityExpireDate", txtidentityExpireDate.Text));
        //arr.Add(new cArrayList("@identitySignaturePath", txtCustCD.Text + ".jpg")); yg bener
        //arr.Add(new cArrayList("@identitySignature", txtidentitySignature.Text));
        //bll.vinsertMstCustomer(arr,);
        //if (uplsignature.FileName != null)
        //{ uplsignature.SaveAs(sImagePath + txtCustCD.Text + ".jpg"); }
        //Response.Redirect("fm_customerlist.aspx");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "window.alert('Data saved successfully !')", true);
       
    }
    protected void btnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("fm_customer.aspx");
        //txtCustCD = null;
        //txtCustNM=null;
        //txtCustSN=null;
        //txtBillNM=null;
        //txtCperson=null;
        ////txtBirthday=null;
        //txtAddr1=null;
        //txtAddr2=null;
        //txtAddr3=null;
        //txtPhone=null;
        //txtFax=null;
        //txtCity=null;
        //txtdistCD=null;
        //txtCountry=null;
        //txtMarketCD=null;
        //txtBlokCD=null;
        //txtDlvrCD=null;
        //txtPostCD=null;
        //txtLatitude=null;
        //txtLongitude=null;
        //txtSalesPointCD=null;
        //cbCusGrCD=null;
        //cbCusClCD=null;
        //txtCustMapCd=null;
        //txtBillCD=null;
        //txtShipCD=null;
        //cbPrLevCD=null;
        //cbOtlCD=null;
        //cbTermCD = null;
        //txtTaxCD=null;
        //txtActCD=null;
        ////txtCreateDT=null;
        //txtdistance=null;
        //txtidentityID=null;
        ////txtidentityExpireDate=null;
    }
    protected void btedit_Click(object sender, EventArgs e)
    {

    }
    protected void bttmp_Click(object sender, EventArgs e)
    {

        SqlDataReader rs = null;
        txtCustCD.Text = Convert.ToString(Session["looCustomerCustCD"]);
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@salesPointCD", Request.Cookies["sp"].Value));
        arr.Add(new cArrayList("@CustCD", txtCustCD.Text));
        bll.vGetMstCustomer2(arr, ref rs);
        while (rs.Read())
        {
            txtCustNM.Text = rs["CustNM"].ToString();
            txtCustSN.Text = rs["CustSN"].ToString();
            txtBillNM.Text = rs["BillNM"].ToString();
            txtCperson.Text = rs["Cperson"].ToString();
            txtBirthday.Text = rs["Birthday"].ToString();
            txtAddr1.Text = rs["Addr1"].ToString();
            txtAddr2.Text = rs["Addr2"].ToString();
            txtAddr3.Text = rs["Addr3"].ToString();
            txtPhone.Text = rs["Phone"].ToString();
            txtFax.Text = rs["Fax"].ToString();
            txtCity.Text = rs["City"].ToString();
            txtdistCD.Text = rs["distCD"].ToString();
            txtCountry.Text = rs["Country"].ToString();
            txtMarketCD.Text = rs["MarketCD"].ToString();
            txtBlokCD.Text = rs["BlokCD"].ToString();
            txtDlvrCD.Text = rs["DlvrCD"].ToString();
            txtPostCD.Text = rs["PostCD"].ToString();
            txtLatitude.Text = rs["Latitude"].ToString();
            txtLongitude.Text = rs["Longitude"].ToString();
            txtSalesPointCD.Text = rs["SalesPointCD"].ToString();
            txtPostCD.Text = rs["PostCD"].ToString();
            cbCusGrCD.Text = rs["CusGrCD"].ToString();
            cbCusClCD.Text = rs["CusClCD"].ToString();
            txtCustMapCd.Text = rs["CustMapCd"].ToString();
            txtBillCD.Text = rs["BillCD"].ToString();
            txtShipCD.Text = rs["ShipCD"].ToString();
            cbPrLevCD.Text = rs["PrLevCD"].ToString();
            cbOtlCD.Text = rs["OtlCD"].ToString();
            cbTermCD.Text = rs["TermCD"].ToString();
            txtTaxCD.Text = rs["TaxCD"].ToString();
            txtActCD.Text = rs["ActCD"].ToString();
            txtCreateDT.Text = rs["CreateDT"].ToString();
            txtdistance.Text = rs["distance"].ToString();
            txtidentityID.Text = rs["identityID"].ToString();
            txtidentityExpireDate.Text = rs["identityExpireDate"].ToString();
            //uplsignature.Text = rs["identitySignaturePath"].ToString();
            
        } rs.Close();
    }

}