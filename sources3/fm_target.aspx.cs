using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fm_target : System.Web.UI.Page
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    Utitlity ut = new Utitlity();
    protected void Page_Load(object sender, EventArgs e)
    {
        //BranchTargetPriority();
        //BranchTargetPriorityVan();
        //ut.BranchTargetPriorityWithOutMail();
        //ut.NewTargetByEDP();
        //ut.UpdatedTargetByEDP();
        //ut.BranchTargetByEDP1();
        BranchTargetPriorityMonthly("201706");
        BranchTargetPriorityVanMonthly("201706");
        ut.BranchTargetPriority();
        ut.BranchTargetPriorityVan();
        //BranchTargetPriorityMonthly("201706");
        //ut.BranchTargetPriorityVanMonthly("201706");
    }

    public void BranchTargetPriorityMonthly(string period)
    {
        try
        {
            int endDateNumer = Convert.ToInt32(Request.QueryString["endate"].ToString());
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            DateTime globaleEndDate = Convert.ToDateTime(Convert.ToString(endDateNumer) + "-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(7) + "-" + "2017");
            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";
            List<cArrayList> drMonth = new List<cArrayList>();
            DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            drMonth.Add(new cArrayList("@wazrDate", dtWazaran));
            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProductMonth_Get", drMonth);

            DataTable dtReport = new DataTable();
            dtReport.TableName = "DataTable1";
            dtReport.Columns.Add("BranchName", typeof(System.String));
            dtReport.Columns.Add("StartDate", typeof(System.DateTime));
            dtReport.Columns.Add("EndDate", typeof(System.DateTime));
            dtReport.Columns.Add("ProdGroupStock", typeof(System.Decimal));
            dtReport.Columns.Add("ProdGroupDepoStock", typeof(System.Decimal));
            dtReport.Columns.Add("ProductGroup", typeof(System.String));
            dtReport.Columns.Add("SalesmanGroup", typeof(System.String));
            dtReport.Columns.Add("HOTarget", typeof(System.Decimal));
            dtReport.Columns.Add("Salesman", typeof(System.String));
            dtReport.Columns.Add("BrnTarget", typeof(System.Decimal));
            dtReport.Columns.Add("AddTarget", typeof(System.Decimal));
            dtReport.Columns.Add("TargetTotal", typeof(System.Decimal));
            dtReport.Columns.Add("VanSales", typeof(System.Decimal));
            dtReport.Columns.Add("TakingOrder", typeof(System.Decimal));
            dtReport.Columns.Add("TotalAchievment", typeof(System.Decimal));
            dtReport.Columns.Add("BrnTargetAchievment", typeof(System.String));
            dtReport.Columns.Add("TotalTarget", typeof(System.String));


            foreach (DataRow drDate in allWeekDateForPriorityProduct.Rows)
            {
                //DateTime dt = Convert.ToDateTime( drDate[0]).Date;
                if (Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length > 8)
                {
                    branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Substring(Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length - 7);
                }
                else { branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value); }

                DataTable dtMailList = new DataTable();
                dtMailList = cdl.GetValueFromSP("sp_TargetPeriorityItems_get");

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");
                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;
                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(globaleEndDate)));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);
                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);
                    // here we get productlist
                    if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = globaleEndDate;
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();

                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItems_get", salespointTargetPriorityItems);
                            bool isSendMail = true;

                            foreach (DataRow dtPri in dtsalespointTargetPriorityItems.Rows)
                            {
                                decimal vanOrder = 0;
                                decimal takeOrder = 0;
                                decimal prodGroupStock = 0;
                                decimal prodGroupDepoStock = 0;


                                List<cArrayList> arrTakeOrder = new List<cArrayList>();
                                DataTable dtTakeOrder = new DataTable();
                                arrTakeOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrTakeOrder.Add(new cArrayList("@endDate", globaleEndDate));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", globaleEndDate));


                                arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                DataTable dtProdGroupStock = new DataTable();
                                arrProdGroupStock.Add(new cArrayList("@dtdate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);


                                List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                DataTable dtProdGroupDepoStock = new DataTable();
                                arrProdGroupDepoStock.Add(new cArrayList("@dtdate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrProdGroupDepoStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrProdGroupDepoStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                dtProdGroupDepoStock = cdl.GetValueFromSP("sp_DepoForPeriorityItems", arrProdGroupDepoStock);

                                if (dtProdGroupDepoStock.Rows.Count > 0) { prodGroupDepoStock = Convert.ToDecimal(dtProdGroupDepoStock.Rows[0][0]); }
                                if (dtProdGroupStock.Rows.Count > 0) { prodGroupStock = Convert.ToDecimal(dtProdGroupStock.Rows[0][0]); }
                                if (dtVanOrder.Rows.Count > 0) { vanOrder = Convert.ToDecimal(dtVanOrder.Rows[0]["qty"]); }
                                if (dtTakeOrder.Rows.Count > 0) { takeOrder = Convert.ToDecimal(dtTakeOrder.Rows[0]["qty"]); }

                                string percentageBranch = "0";
                                string percentageTotalTarget = "0";
                                if ((vanOrder + takeOrder) != 0)
                                {
                                    decimal totalOrder = vanOrder + takeOrder;
                                    percentageBranch = Convert.ToString((totalOrder / Convert.ToDecimal(dtPri["qty"])) * 100);
                                    decimal totalTarget = Convert.ToDecimal(dtPri["qty"]) + Convert.ToDecimal(dtPri["tabtarget"]);
                                    percentageTotalTarget = Convert.ToString((totalOrder / totalTarget) * 100);

                                }
                                productGroup = "(" + Convert.ToString(dtPri["prod_cd"]) + ") " + Convert.ToString(dtPri["prod_nm"]);// + Convert.ToString(dt.AddDays(-5)) + "- " + Convert.ToString(dt);
                                salesmanGroup = Convert.ToString(dtPri["groupName"]);
                                // Request.Cookies["spn"].Value.ToString()
                                //HttpContext.Current.Request.Cookies["spn"].Value.ToString()
                                dtReport.Rows.Add(branchName,
                                    Convert.ToString(startDate),
                                    Convert.ToString(globaleEndDate.AddDays(-1)),
                                    Convert.ToString(String.Format("{0:0.00}", prodGroupStock)),
                                    Convert.ToString(String.Format("{0:0.00}", prodGroupDepoStock)),
                                    productGroup,
                                    Convert.ToString(dtPri["groupName"]),
                                    Convert.ToString(String.Format("{0:0.00}", dtsalespointTargetPriorityItems.Rows[0]["target_ho"])),
                                    Convert.ToString(dtPri["fullName"]),
                                    Convert.ToString(String.Format("{0:0.00}", dtPri["qty"])),
                                    Convert.ToString(String.Format("{0:0.00}", dtPri["tabtarget"])),
                                    Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(dtPri["tabtarget"]) + Convert.ToDecimal(dtPri["qty"])))),
                                    Convert.ToString(String.Format("{0:0.00}", vanOrder)),
                                    Convert.ToString(String.Format("{0:0.00}", takeOrder)),
                                    Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(vanOrder + takeOrder))),
                                    Convert.ToString(String.Format("{0:0.00}", Decimal.Parse(percentageBranch, CultureInfo.InvariantCulture))),
                                    Convert.ToString(String.Format("{0:0.00}", Decimal.Parse(percentageTotalTarget, CultureInfo.InvariantCulture))));


                                isSendMail = true;
                            }
                            DataTable dtTempForTotal = new DataTable();
                            dtTempForTotal = dtReport.Copy();


                        }
                        CrystalReportViewer crv = new CrystalReportViewer();

                        ReportDocument rptDoc = new ReportDocument();
                        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                        ConnectionInfo crConnectionInfo = new ConnectionInfo();
                        ParameterFields pfs = new ParameterFields();
                        ParameterField pf = null;
                        ParameterDiscreteValue pfd = new ParameterDiscreteValue();


                        rptDoc.Load(HttpContext.Current.Server.MapPath("PriorityTargetMonthly.rpt"));

                        List<cArrayList> lParameter = new List<cArrayList>();

                        DataSet ds = new DataSet();
                        DataTable dtTemp = new DataTable();
                        dtTemp = dtReport.Copy();
                        ds.Tables.Add(dtTemp);
                        rptDoc.SetDataSource(ds);



                        crv.ReportSource = rptDoc;
                        string sPath = bll.sGetControlParameter("image_path") + "\\PriorityTarget\\"; //+ @"\Priority\";
                        string folderLocation = "PriorityTarget\\";
                        string fileName = "_A_" + Convert.ToString(Convert.ToString(DateTime.Now.Ticks).Substring(Convert.ToString(DateTime.Now.Ticks).Length - 6));



                        //string pdfFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_"+Convert.ToString(Guid.NewGuid()) + ".pdf";
                        //string pdfFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_" + Convert.ToString(DateTime.Now.Ticks) + ".pdf";
                        //string xlsFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_" +Convert.ToString(DateTime.Now.Ticks) + ".xls";
                        //WebConfigurationManager.AppSettings["SalesPoint"]
                        string pdfFile = sPath + branchName + fileName + ".pdf";
                        string xlsFile = sPath + branchName + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        //rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                        arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                        arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + branchName + fileName + ".pdf"));
                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + branchName + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        if (true == true)
                        {
                            dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                            //dtMail.Clear();

                        }
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(globaleEndDate)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                        #endregion
                }
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            ut.Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetPriority", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetPriorityVanMonthly(string period)
    {
        try
        {
            int endDateNumer = Convert.ToInt32(Request.QueryString["endate"].ToString());
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            

            string dataHTML = string.Empty;

            DateTime globaleEndDate = Convert.ToDateTime(Convert.ToString(endDateNumer) + "-" + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(7) + "-" + "2017");
            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            List<cArrayList> drMonth = new List<cArrayList>();
            DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            drMonth.Add(new cArrayList("@wazrDate", dtWazaran));

            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProductMonth_Get", drMonth);

            DataTable dtReport = new DataTable();
            dtReport.TableName = "DataTable1";
            dtReport.Columns.Add("BranchName", typeof(System.String));
            dtReport.Columns.Add("StartDate", typeof(System.DateTime));
            dtReport.Columns.Add("EndDate", typeof(System.DateTime));
            dtReport.Columns.Add("ProdGroupStock", typeof(System.Decimal));
            dtReport.Columns.Add("ProdGroupDepoStock", typeof(System.Decimal));
            dtReport.Columns.Add("ProductGroup", typeof(System.String));
            dtReport.Columns.Add("SalesmanGroup", typeof(System.String));
            dtReport.Columns.Add("HOTarget", typeof(System.Decimal));
            dtReport.Columns.Add("Salesman", typeof(System.String));
            dtReport.Columns.Add("BrnTarget", typeof(System.Decimal));
            dtReport.Columns.Add("AddTarget", typeof(System.Decimal));
            dtReport.Columns.Add("TargetTotal", typeof(System.Decimal));
            dtReport.Columns.Add("VanSales", typeof(System.Decimal));
            dtReport.Columns.Add("TakingOrder", typeof(System.Decimal));
            dtReport.Columns.Add("TotalAchievment", typeof(System.Decimal));
            dtReport.Columns.Add("BrnTargetAchievment", typeof(System.String));
            dtReport.Columns.Add("TotalTarget", typeof(System.String));


            foreach (DataRow drDate in allWeekDateForPriorityProduct.Rows)
            {
                //DateTime dt = Convert.ToDateTime( drDate[0]).Date;




                string dt = Convert.ToString(drDate[0]);
                string dtServerDate = Convert.ToString(bll.sGetControlParameter("WAZARAN_DT").Split('/')[0]) + '-' + Convert.ToString(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(bll.sGetControlParameter("WAZARAN_DT").Split('/')[1]))) + "-" + Convert.ToString(bll.sGetControlParameter("WAZARAN_DT").Split('/')[2]);
                if (Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length > 8)
                {
                    branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Substring(Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length - 7);
                }
                else { branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value); }




                DataTable dtMailList = new DataTable();
                dtMailList = cdl.GetValueFromSP("sp_TargetPeriorityItems_get");

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");

                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(globaleEndDate)));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);

                    // here we get productlist
                    if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = globaleEndDate;
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();

                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsVan_get", salespointTargetPriorityItems);
                            bool isSendMail = true;

                            foreach (DataRow dtPri in dtsalespointTargetPriorityItems.Rows)
                            {
                                decimal vanOrder = 0;
                                decimal takeOrder = 0;
                                decimal prodGroupStock = 0;
                                decimal prodGroupDepoStock = 0;


                                List<cArrayList> arrTakeOrder = new List<cArrayList>();
                                DataTable dtTakeOrder = new DataTable();
                                arrTakeOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrTakeOrder.Add(new cArrayList("@endDate", globaleEndDate));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", globaleEndDate));


                                arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                DataTable dtProdGroupStock = new DataTable();
                                arrProdGroupStock.Add(new cArrayList("@dtdate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);


                                List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                DataTable dtProdGroupDepoStock = new DataTable();
                                arrProdGroupDepoStock.Add(new cArrayList("@dtdate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrProdGroupDepoStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrProdGroupDepoStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                dtProdGroupDepoStock = cdl.GetValueFromSP("sp_DepoForPeriorityItems", arrProdGroupDepoStock);

                                if (dtProdGroupDepoStock.Rows.Count > 0) { prodGroupDepoStock = Convert.ToDecimal(dtProdGroupDepoStock.Rows[0][0]); }
                                if (dtProdGroupStock.Rows.Count > 0) { prodGroupStock = Convert.ToDecimal(dtProdGroupStock.Rows[0][0]); }
                                if (dtVanOrder.Rows.Count > 0) { vanOrder = Convert.ToDecimal(dtVanOrder.Rows[0]["qty"]); }
                                if (dtTakeOrder.Rows.Count > 0) { takeOrder = Convert.ToDecimal(dtTakeOrder.Rows[0]["qty"]); }

                                string percentageBranch = "0";
                                string percentageTotalTarget = "0";
                                if ((vanOrder + takeOrder) != 0)
                                {
                                    decimal totalOrder = vanOrder + takeOrder;
                                    percentageBranch = Convert.ToString((totalOrder / Convert.ToDecimal(dtPri["qty"])) * 100);
                                    decimal totalTarget = Convert.ToDecimal(dtPri["qty"]) + Convert.ToDecimal(dtPri["tabtarget"]);
                                    percentageTotalTarget = Convert.ToString((totalOrder / totalTarget) * 100);

                                }
                                productGroup = "(" + Convert.ToString(dtPri["prod_cd"]) + ") " + Convert.ToString(dtPri["prod_nm"]);// + Convert.ToString(dt.AddDays(-5)) + "- " + Convert.ToString(dt);
                                salesmanGroup = Convert.ToString(dtPri["groupName"]);
                                // Request.Cookies["spn"].Value.ToString()
                                //HttpContext.Current.Request.Cookies["spn"].Value.ToString()
                                dtReport.Rows.Add(branchName,
                                    Convert.ToString(startDate),
                                    Convert.ToString(globaleEndDate.AddDays(-1)),
                                    Convert.ToString(String.Format("{0:0.00}", prodGroupStock)),
                                    Convert.ToString(String.Format("{0:0.00}", prodGroupDepoStock)),
                                    productGroup,
                                    Convert.ToString(dtPri["groupName"]),
                                    Convert.ToString(String.Format("{0:0.00}", dtsalespointTargetPriorityItems.Rows[0]["target_ho"])),
                                    Convert.ToString(dtPri["fullName"]),
                                    Convert.ToString(String.Format("{0:0.00}", dtPri["qty"])),
                                    Convert.ToString(String.Format("{0:0.00}", dtPri["tabtarget"])),
                                    Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(Convert.ToDecimal(dtPri["tabtarget"]) + Convert.ToDecimal(dtPri["qty"])))),
                                    Convert.ToString(String.Format("{0:0.00}", vanOrder)),
                                    Convert.ToString(String.Format("{0:0.00}", takeOrder)),
                                    Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(vanOrder + takeOrder))),
                                    Convert.ToString(String.Format("{0:0.00}", Decimal.Parse(percentageBranch, CultureInfo.InvariantCulture))),
                                    Convert.ToString(String.Format("{0:0.00}", Decimal.Parse(percentageTotalTarget, CultureInfo.InvariantCulture))));


                                isSendMail = true;
                            }
                            DataTable dtTempForTotal = new DataTable();
                            dtTempForTotal = dtReport.Copy();


                        }
                        CrystalReportViewer crv = new CrystalReportViewer();

                        ReportDocument rptDoc = new ReportDocument();
                        TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                        TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                        ConnectionInfo crConnectionInfo = new ConnectionInfo();
                        ParameterFields pfs = new ParameterFields();
                        ParameterField pf = null;
                        ParameterDiscreteValue pfd = new ParameterDiscreteValue();


                        rptDoc.Load(HttpContext.Current.Server.MapPath("PriorityTargetMonthlyVan.rpt"));

                        List<cArrayList> lParameter = new List<cArrayList>();

                        DataSet ds = new DataSet();
                        DataTable dtTemp = new DataTable();
                        dtTemp = dtReport.Copy();
                        ds.Tables.Add(dtTemp);
                        rptDoc.SetDataSource(ds);



                        crv.ReportSource = rptDoc;
                        string sPath = bll.sGetControlParameter("image_path") + "\\PriorityTarget\\"; //+ @"\Priority\";
                        string folderLocation = "PriorityTarget\\";
                        string fileName = "_C_" + Convert.ToString(Convert.ToString(DateTime.Now.Ticks).Substring(Convert.ToString(DateTime.Now.Ticks).Length - 6));



                        //string pdfFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_"+Convert.ToString(Guid.NewGuid()) + ".pdf";
                        //string pdfFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_" + Convert.ToString(DateTime.Now.Ticks) + ".pdf";
                        //string xlsFile = sPath + "PriorityTarget_" + Convert.ToString(dr["fld_valu"]) + "_" +Convert.ToString(DateTime.Now.Ticks) + ".xls";
                        //WebConfigurationManager.AppSettings["SalesPoint"]
                        string pdfFile = sPath + branchName + fileName + ".pdf";
                        string xlsFile = sPath + branchName + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        //rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                        arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                        arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + branchName + fileName + ".pdf"));
                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + branchName + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        if (true == true)
                        {
                            dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                            //dtMail.Clear();

                        }
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(globaleEndDate)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                        #endregion
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            ut.Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetPriority", "Exception", ex.Message + ex.InnerException);
        }
    }
}