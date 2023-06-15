using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iandroservice" in both code and config file together.
[ServiceContract]
public interface Iandroservice
{
    
	[OperationContract]
	void DoWork();
    [OperationContract]
    string ssBack();
    [OperationContract]
    List<tuser_profiles> lup(string sEmpCode);
    [OperationContract]
    List<tmst_product> lmstproduct();
    [OperationContract]
    List<tmst_item> lmstitem();

    [OperationContract]
    string vInsertCanvasOrder(ttablet_canvasorder lcanvas);

    [OperationContract]
    List<tcustomertype_price> lcustomertypeprice();
    [OperationContract]
    List<tmst_customer> lmstcustomer(string sSalespoint);

    [OperationContract]
    List<tmst_stock> lmststock(string sSalespoint);

    [OperationContract]
    List<tmst_van_stock> lmstvanstock(string sSalespoint);
  }

[DataContract]
public class tmst_van_stock
{ 
    protected string _item_cd;
    protected string _item_nm;
    protected string _item_size;
    protected string _vhc_cd;
    protected string _bin_cd;
    protected double _stock_amt;
    protected double _stock_display;
    protected string _item_arabic;
    protected string _branded_nm;
    public tmst_van_stock(string sItemCode, string sITemName, string sItemSize, string sBranded , string sItemArabic,string sVhcCode, string sBinCode, double dStockAmt , double dStockDisplay)
    {
        item_cd = sItemCode;
        item_nm = sITemName;
        item_size = sItemSize;
        branded_nm = sBranded;
        item_arabic = sItemArabic;
        vhc_cd = sVhcCode;
        bin_cd = sBinCode;
        stock_amt = dStockAmt;
        stock_display = dStockDisplay;
    }

    [DataMember]
    public string item_cd
    { set { _item_cd = value; } get { return (_item_cd); } }

    [DataMember]
    public string item_nm
    { set { _item_nm = value; } get { return (_item_nm); } }

    [DataMember]
    public string item_size
    {
        set { _item_size = value; }
        get { return (_item_size); }
    }

    [DataMember]
    public string item_arabic
    { set { _item_arabic = value; } get { return (_item_arabic); } }
    [DataMember]
    public string branded_nm
    { set { _branded_nm = value; } get { return (_branded_nm); } }

    [DataMember]
    public double stock_amt
    { set { _stock_amt = value; } get { return (_stock_amt); } }

    [DataMember]
    public double stock_display
    { set { _stock_display = value; } get { return (_stock_display); } }

    [DataMember]
    public string vhc_cd
    { set { _vhc_cd = value; } get { return (_vhc_cd); } }

    [DataMember]
    public string bin_cd
    {
        set { _bin_cd = value; }
        get { return (_bin_cd); }
    }
}

[DataContract]
public class tmst_stock
{
    protected string _item_cd;
    protected string _item_nm;
    protected string _item_arabic;
    protected string _item_size;
    protected double _stock_amt;
    protected double _stock_display;
    protected string _whs_cd;
    protected string _bin_cd;
    protected string _branded_nm;
    public tmst_stock(string sItemCode,string sITemName, string sItemArabic, string sItemSize, double dStockAmt, double dStockDisplay, string sWhsCode, string sBinCode, string sBranded)
    {
        item_cd = sItemCode;
        item_nm = sITemName;
        item_arabic = sItemArabic;
        item_size = sItemSize;
        stock_amt = dStockAmt;
        stock_display = dStockDisplay;
        whs_cd = sWhsCode;
        bin_cd = sBinCode;
        branded_nm = sBranded;
    }

    [DataMember]
    public string branded_nm
    { set { _branded_nm = value; } get { return (_branded_nm); } }

    [DataMember]
    public string item_cd
    { set { _item_cd = value; } get { return (_item_cd); } }
    [DataMember]
    public string item_nm
    { set { _item_nm = value; } get { return (_item_nm); } }
    [DataMember]
    public string item_arabic
    { set { _item_arabic = value; } get { return (_item_arabic); } }
    [DataMember]
    public string item_size
    { set { _item_size = value; } get { return (_item_size); } }

    [DataMember]
    public double stock_amt
    { set { _stock_amt = value; } get { return (_stock_amt); } }
    [DataMember]
    public double stock_display
    { set { _stock_display = value; } get { return (_stock_display); } }
    [DataMember]
    public string whs_cd
    { set { _whs_cd = value; } get { return (_whs_cd); } }
    [DataMember]
    public string bin_cd
    { set { _bin_cd = value; } get { return (_bin_cd); } }
}

[DataContract]
public class tmst_customer
{ 
    protected string  _cust_cd;
    protected string _cust_nm;
    protected string _addr;
    protected string _city_cd;
    protected string _cusgrdcd;
    protected string _otlcd;
    protected double _credit_limit;
    protected string _salesman_cd;
    public tmst_customer (string sCustCd, string sCustName, string sAddr, string sCityCode, string sCusGroup, string sOtlCode, double dCreditLimit , string sSalesman)
    {
        cust_cd = sCustCd;
        cust_nm = sCustName;
        city_cd = sCityCode;
        addr = sAddr;
        cusgrcd = sCusGroup;
        otlcd = sOtlCode;
        credit_limit = dCreditLimit;
        salesman_cd = sSalesman;
    }
    [DataMember]
    public string cust_cd
    {
        set { _cust_cd = value; }
        get { return (_cust_cd); }
    }

    [DataMember]
    public string cust_nm
    { set { _cust_nm = value; } get { return (_cust_nm); } }
    [DataMember]
    public string addr
    {
        set { _addr = value; }
        get { return (_addr); }
    }

    [DataMember]
    public string city_cd
    { set { _city_cd = value; } get { return (_city_cd); } }
    [DataMember]
    public string cusgrcd
    { set { _cusgrdcd = value; } get { return (_cusgrdcd); } }
    [DataMember]
    public string otlcd
    { set { _otlcd = value; } get { return (_otlcd); } }
    [DataMember]
    public string salesman_cd
    { set { _salesman_cd = value; } get { return (_salesman_cd); } }
    [DataMember]
    public double credit_limit
    { set { _credit_limit = value; } get { return (_credit_limit); } }
}

[DataContract]
public class tcustomertype_price
{
    private string _cust_typ;
    private string _item_cd;
    private double _unitprice;

    public tcustomertype_price(string sCustTyp, string sItemCd, double dUnitPrice)
    {
        cust_typ = sCustTyp;
        item_cd = sItemCd;
        unitprice = dUnitPrice;
    }

    [DataMember]
    public string cust_typ
    {
        set { _cust_typ = value; }
        get { return (_cust_typ); }
    }

    [DataMember]
    public string item_cd
    {
        set { _item_cd = value; }
        get { return (_item_cd); }
    }

    [DataMember]
    public double unitprice
    { set { _unitprice = value; } get { return (_unitprice); } }
}

[DataContract]
public class tmst_item
{
    private string mvaritemcd ;
    private string mvaritemname;
    private string mvaritemarabic;
    private string mvarsize;
    private string mvarbrandedname;

    public tmst_item(string sitemcode, string sitemname, string sitemarabic, string ssize, string sbrandedname)
    {
        item_cd = sitemcode;
        item_nm = sitemname;
        item_arabic = sitemarabic;
        size = ssize;
        branded_nm = sbrandedname;
    }

    [DataMember]
    public string item_cd
    {
    set{mvaritemcd=value;}get{return(mvaritemcd);}
    }

    [DataMember]
    public string item_nm
    {
        set { mvaritemname = value; }
        get { return (mvaritemname); }
    }
    [DataMember]
    public string item_arabic
    {
        set { mvaritemarabic = value; }
        get { return (mvaritemarabic); }
    }

    [DataMember]
    public string size
    {
        set { mvarsize = value; }
        get { return (mvarsize); }
    }

    [DataMember]
    public string branded_nm
    {
        set { mvarbrandedname = value; }
        get { return (mvarbrandedname); }
    }
}

[DataContract]
public class tmst_product
{
    private string mvarprodcd;
    private string mvarprodname;

    public tmst_product(string sProdCd, string sProdName)
    {
        prod_cd = sProdCd;
        prod_nm = sProdName;
    }

    [DataMember]
    public string prod_cd
    {
        set { mvarprodcd = value; }
        get { return (mvarprodcd); }
    }

    [DataMember]
    public string prod_nm
    {
        set { mvarprodname = value; }
        get { return (mvarprodname); }
    }
}

[DataContract]
public class ttablet_canvasorder
{
    private string mvarsocd;
    private DateTime mvarsodate;
    private string mvarsalesmancode;
    private double mvarqty;
    private string mvaruom;
    private string mvaritemcd;
    public ttablet_canvasorder(string sSOCD, DateTime dtso, string sSalesman, double dQty, string sUom, string sItemCode)
    {

        so_cd = sSOCD;
        so_dt = dtso;
        salesman_cd = sSalesman;
        uom = sUom;
        qty = dQty;
        item_cd = sItemCode;
    }

    [DataMember]
    public string so_cd
    { set { mvarsocd = value; } get { return (mvarsocd); } }
    [DataMember]
    public DateTime so_dt
    { set { mvarsodate = value; ; } get { return (mvarsodate); } }

    [DataMember]
    public string item_cd
    {
        set { mvaritemcd = value; }
        get { return (mvaritemcd); }
    }

    [DataMember]
    public string salesman_cd
    {
        set { mvarsalesmancode = value; }
        get { return (mvarsalesmancode); }
    }
    [DataMember]
    public double qty
    {
        set { mvarqty = value; }
        get { return (mvarqty); }
    }

    [DataMember]
    public string uom
    {
        set { mvaruom = value; }
        get { return (mvaruom); }
    }
}

[DataContract]
public class tuser_profiles
{
    private string m_usrid;
    private string m_pwd;
    private string m_fullname;
    
    public tuser_profiles(string sUserID, string sPassword, string sFullname)
    {
        fullname = sFullname;
        password = sPassword;
        usr_id = sUserID;
    }
    [DataMember]
    public string usr_id
    {
        set { m_usrid = value; }
        get { return (m_usrid); }
    }
  [DataMember]
    public string fullname
    {
        set { m_fullname = value; }
        get { return (m_fullname); }
    }
  [DataMember]
    public string password
    {
        set { m_pwd = value; }
        get { return (m_pwd); }
    }

}
    
