using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class cbllHO
{
    cdalHO dal = new cdalHO();
    
    public void vInsertWazaranCNDN(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertWazaranCNDNTransaction", arr);
    }
    public void vInsertWazaranCashout(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertWazaranCashOutTransaction", arr);
    }
    public void vInsertWazaranSalesman(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertWazaranSalesman", arr);
    }
    public void vInsertWazaranCustomer(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertWazaranCustomer", arr);
    }
    public void vInsertIntoWazaranCashTransaction(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertIntoWazaranCashTransaction", arr);
    }
    public void vInsertWazaranItemTransaction(List<cArrayList> arr)
    {
        dal.vExecuteSPHO("InsertIntoWazaranItemTransaction", arr);
    }
}