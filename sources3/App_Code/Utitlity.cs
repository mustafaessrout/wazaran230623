using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Configuration;

/// <summary>
/// Summary description for Utitlity
/// </summary>
/// 
public class Utitlity
{
    cbll bll = new cbll();
    cdal cdl = new cdal();
    creport rep = new creport();
    public Utitlity()
    {

    }

    public void Logs(string filePath, string domainName, string moduleName, string formName, string methodName, string errorType, string error)
    {
        //string startupPath = System.IO.Directory.GetCurrentDirectory();
        //string startupPathDir = Environment.CurrentDirectory;
        //filePath = HttpContext.Current.Server.MapPath("/App_Data/AppLogs/");
        //string fileName = filePath + DateTime.Today.ToString("dd-MMM-yyyy") + "_" +Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value) + ".txt";

        //bool fileExist = File.Exists(fileName);
        //if (fileExist == false)
        //{
        //    File.Create(fileName);
        //}

        //string text = string.Empty;
        ////File.Create(fileName);
        //long fileSize = GetFileSizeOnDisk(fileName);
        //List<Utitlity.Data> _data = new List<Utitlity.Data>();
        //_data.Add(new Utitlity.Data()
        //{
        //    DomainName = domainName,
        //    Error = error,
        //    ErrorType = errorType,
        //    FormName = formName,
        //    MethodName = methodName,
        //    ModuleName = moduleName,
        //    CreatedBy = Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value),
        //    CreatedDate = DateTime.Now
        //});
        //string json = JsonConvert.SerializeObject(_data.ToArray());


        //using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
        //{
        //    StreamWriter str = new StreamWriter(fs);
        //    str.BaseStream.Seek(0, SeekOrigin.End);
        //    //str.Write("************" + domainName + "************" + moduleName +
        //    //       "************" + formName + "************" + methodName +
        //    //        "************" + errorType + "************"  + Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value) + Environment.NewLine);
        //    //str.WriteLine(DateTime.Now.ToLongTimeString() + " " +
        //    //              DateTime.Now.ToLongDateString());
        //    //str.WriteLine(error + Environment.NewLine);
        //    //str.WriteLine(json.ToString().Remove(json.ToString().Length - 1));
        //    str.WriteLine(json);
        //    str.Flush();
        //    str.Close();
        //    fs.Close();
        //    File.ReadAllText(fileName);
        //}
        Data _data = new Data();
        _data.DomainName = domainName;
        _data.Error = error;
        _data.ErrorType = errorType;
        _data.FormName = formName;
        _data.MethodName = methodName;
        _data.ModuleName = moduleName;
        _data.BrnachCode = Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value);
        _data.CreatedBy = Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value);
        //_data.CreatedDate = DateTime.Now;


        DataTable dataTable = new DataTable("ErrorData");
        //we create column names as per the type in DB 
        dataTable.Columns.Add("DomainName", typeof(string));
        dataTable.Columns.Add("Error", typeof(string));
        dataTable.Columns.Add("ErrorType", typeof(string));
        dataTable.Columns.Add("FormName", typeof(string));
        dataTable.Columns.Add("MethodName", typeof(string));
        dataTable.Columns.Add("ModuleName", typeof(string));
        dataTable.Columns.Add("BrnachCode", typeof(string));
        dataTable.Columns.Add("CreatedBy", typeof(string));
        //and fill in some values 
        dataTable.Rows.Add(domainName, error, errorType, formName, methodName, moduleName, Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value),
            Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value));


        //SqlParameter param = new SqlParameter();
        //param.ParameterName = "@ErrorLogDetails";
        //param.SqlDbType = SqlDbType.Structured;
        //param.Value = _data;
        ////param.Direction = ParameterDirection.Input;
        //cdl.vExecuteStructuredSP(param);

        SqlParameter parameter = new SqlParameter();
        //The parameter for the SP must be of SqlDbType.Structured 
        parameter.ParameterName = "@ErrorLogDetails";
        parameter.SqlDbType = System.Data.SqlDbType.Structured;
        parameter.Value = dataTable;
        cdl.vExecuteStructuredSP(parameter);
    }


    private static void ExecuteProcedure(bool useDataTable, string connectionString, IEnumerable<long> ids)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "dbo.procMergePageView";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter;
                if (useDataTable)
                {
                    parameter = command.Parameters.AddWithValue("@Display", CreateDataTable(ids));
                }
                else
                {
                    parameter = command.Parameters.AddWithValue("@Display", CreateSqlDataRecords(ids));
                }
                parameter.SqlDbType = SqlDbType.Structured;
                parameter.TypeName = "dbo.PageViewTableType";

                command.ExecuteNonQuery();
            }
        }
    }

    private static DataTable CreateDataTable(IEnumerable<long> ids)
    {
        DataTable table = new DataTable();
        table.Columns.Add("ID", typeof(long));
        foreach (long id in ids)
        {
            table.Rows.Add(id);
        }
        return table;
    }

    private static IEnumerable<SqlDataRecord> CreateSqlDataRecords(IEnumerable<long> ids)
    {
        SqlMetaData[] metaData = new SqlMetaData[1];
        metaData[0] = new SqlMetaData("ID", SqlDbType.BigInt);
        SqlDataRecord record = new SqlDataRecord(metaData);
        foreach (long id in ids)
        {
            record.SetInt64(0, id);
            yield return record;
        }
    }

    public static long GetFileSizeOnDisk(string file)
    {
        FileInfo info = new FileInfo(file);
        uint dummy, sectorsPerCluster, bytesPerSector;
        int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
        if (result == 0) throw new Win32Exception();
        uint clusterSize = sectorsPerCluster * bytesPerSector;
        uint hosize;
        uint losize = GetCompressedFileSizeW(file, out hosize);
        long size;
        size = (long)hosize << 32 | losize;
        return ((size + clusterSize - 1) / clusterSize) * clusterSize;
    }

    [DllImport("kernel32.dll")]
    static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
       [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

    [DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
    static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
       out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
       out uint lpTotalNumberOfClusters);

    public void ClearSession()
    {
        HttpContext.Current.Session.Remove("dateLst");
        HttpContext.Current.Session.Remove("ReturndateLst");
        HttpContext.Current.Session.Remove("strStartDate");
        HttpContext.Current.Session.Remove("strEndDate");
    }

    public string GetErrors(DateTime fromDate, DateTime toDate, string branchCode)
    {
        string contents = string.Empty;
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        DataTable dt = new DataTable();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@fromDate", fromDate));
        arr.Add(new cArrayList("@toDate", toDate));
        arr.Add(new cArrayList("@branchCode", branchCode));
        dt = cdl.GetValueFromSP("sp_errorDetails_get", arr);

        if (dt.Rows.Count > 0)
        {
            contents = serializer.Serialize(dt);
        }
        //bool fileExist = File.Exists(HttpContext.Current.Server.MapPath("/App_Data/AppLogs/") + fileName);
        //if (fileExist == true)
        //{
        //    string file = Directory.EnumerateFiles(Convert.ToString(HttpContext.Current.Server.MapPath("/App_Data/AppLogs/")), fileName).FirstOrDefault();
        //    contents = File.ReadAllText(file);
        //}
        return contents;
    }

    public void BranchTargetPriority()
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;


            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            string period = bll.vLookUp("select dbo.fn_getcontrolparametersalespoint('period','"+ HttpContext.Current.Request.Cookies["sp"].Value + "')");//Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProduct_Get");

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

                string dtServerDate = Convert.ToString(bll.sGetControlParameterSalespoint("WAZARAN_DT", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)).Split('/')[0]) + '-' + Convert.ToString(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(bll.sGetControlParameter("WAZARAN_DT").Split('/')[1]))) + "-" + Convert.ToString(bll.sGetControlParameter("WAZARAN_DT").Split('/')[2]);
                if (Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length > 8)
                {
                    branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Substring(Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length - 7);
                }
                else { branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value); }


                if (Convert.ToDateTime(dtServerDate) >= Convert.ToDateTime(dt))
                {
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
                        drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
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
                                DateTime endDate = Convert.ToDateTime(dt);
                                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                                DataTable dtsalespointTargetPriorityItems = new DataTable();
                                productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                    arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrTakeOrder.Add(new cArrayList("@endDate", dt));
                                    arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);

                                    List<cArrayList> arrVanOrder = new List<cArrayList>();
                                    DataTable dtVanOrder = new DataTable();
                                    arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrVanOrder.Add(new cArrayList("@endDate", dt));

                                    arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                    List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                    DataTable dtProdGroupStock = new DataTable();
                                    arrProdGroupStock.Add(new cArrayList("@dtdate", dt));
                                    arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                    arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                    dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);

                                    List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                    DataTable dtProdGroupDepoStock = new DataTable();
                                    arrProdGroupDepoStock.Add(new cArrayList("@dtdate", dt));
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
                                    dtReport.Rows.Add(branchName,
                                        Convert.ToString(startDate),
                                        Convert.ToString(endDate),
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


                            rptDoc.Load(HttpContext.Current.Server.MapPath("PriorityTarget.rpt"));

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
                            
                            string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                            string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                            rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                            rptDoc.Close();
                            List<cArrayList> arrMailPDF = new List<cArrayList>();
                            string subject = "N/A";
                            if (dtTemp.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                                {
                                    subject = " Target priority by Product Group for All Salesman. No records found.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for All Salesman. No records found."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for All Salesman. No records found."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", null));
                                }
                                else
                                {
                                    subject = " Target priority by Product Group for All Salesman.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for All Salesman."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for All Salesman."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));

                                    string salesTargetAchievementID = string.Empty;
                                    List<cArrayList> arr = new List<cArrayList>();
                                    arr.Add(new cArrayList("@Period", period));
                                    arr.Add(new cArrayList("@transType", "TargetPriority"));
                                    arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                    arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                    arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                    arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                    arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                    arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                    bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                    foreach (DataRow dr in dtTemp.Rows)
                                    {
                                        arr.Clear();
                                        arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                        arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                        arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                        arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                        arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                        arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                        arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                        arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                        arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                        arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                        bll.vInsertTargetAchievementDtl(arr);
                                    }

                                }
                            }
                            else if (dtTemp.Rows.Count == 0)
                            {
                                subject = " Target priority by Product Group for All Salesman. No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for All Salesman. No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for All Salesman. No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = " Target priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                                
                                string salesTargetAchievementID = string.Empty;
                                List<cArrayList> arr = new List<cArrayList>();
                                arr.Add(new cArrayList("@Period", period));
                                arr.Add(new cArrayList("@transType", "TargetPriority"));
                                arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    arr.Clear();
                                    arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                    arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                    arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                    arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                    arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                    arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                    arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                    arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                    arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                    arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                    bll.vInsertTargetAchievementDtl(arr);
                                }
                            }
                            arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                            arrMailPDF.Add(new cArrayList("@token", null));
                            arrMailPDF.Add(new cArrayList("@doc_no", null));
                            arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));
                            DataTable dtMail = new DataTable();
                            if (true == true)
                            {
                                dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                                dtMail.Clear();
                            }
                            dtReport.Clear();

                            List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                            DataTable dtDuplicateMailInsert = new DataTable();
                            
                            drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                            drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                            drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                            drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriority"));
                            drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                            drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                            drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                            drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Daily"));
                            drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                            dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                        }

                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetPriority", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetPriorityVan()
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            string period = bll.vLookUp("select dbo.fn_getcontrolparametersalespoint('period','" + HttpContext.Current.Request.Cookies["sp"].Value + "')") ;//Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProduct_Get");

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



                if (Convert.ToDateTime(dtServerDate) >= Convert.ToDateTime(dt))
                {
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
                        drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
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
                                DateTime endDate = Convert.ToDateTime(dt);
                                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                                DataTable dtsalespointTargetPriorityItems = new DataTable();
                                productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                    arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrTakeOrder.Add(new cArrayList("@endDate", dt));
                                    arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                    List<cArrayList> arrVanOrder = new List<cArrayList>();
                                    DataTable dtVanOrder = new DataTable();
                                    arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrVanOrder.Add(new cArrayList("@endDate", dt));


                                    arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                    List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                    DataTable dtProdGroupStock = new DataTable();
                                    arrProdGroupStock.Add(new cArrayList("@dtdate", dt));
                                    arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                    arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                    dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);


                                    List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                    DataTable dtProdGroupDepoStock = new DataTable();
                                    arrProdGroupDepoStock.Add(new cArrayList("@dtdate", dt));
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
                                        Convert.ToString(endDate),
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


                            rptDoc.Load(HttpContext.Current.Server.MapPath("PriorityTargetVan.rpt"));

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
                            string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                            string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                            rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                            rptDoc.Close();
                            List<cArrayList> arrMailPDF = new List<cArrayList>();
                            string subject = "N/A";
                            if (dtTemp.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                                {
                                    subject = " Target priority by Product Group for Canvaser. No records found.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for Canvaser. No records found."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for Canvaser. No records found."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", null));
                                }
                                else
                                {
                                    subject = " Target priority by Product Group for Canvaser.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for Canvaser."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for Canvaser."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));

                                    // Here we 

                                    
                                    string salesTargetAchievementID = string.Empty;
                                    List<cArrayList> arr = new List<cArrayList>();
                                    arr.Add(new cArrayList("@Period", period));
                                    arr.Add(new cArrayList("@transType", "TargetPriorityVan"));
                                    arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                    arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                    arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                    arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                    arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                    arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                    bll.vInsertTargetAchievement(arr,ref salesTargetAchievementID);

                                    foreach (DataRow dr in dtTemp.Rows)
                                    {
                                        arr.Clear();
                                        arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                        arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                        arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                        arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                        arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                        arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                        arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                        arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                        arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                        arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                        bll.vInsertTargetAchievementDtl(arr);
                                    }
                                }
                            }
                            else if (dtTemp.Rows.Count == 0)
                            {
                                subject = " Target priority by Product Group for Canvaser. No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for Canvaser. No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for Canvaser. No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = " Target priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            }
                            arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                            arrMailPDF.Add(new cArrayList("@token", null));
                            arrMailPDF.Add(new cArrayList("@doc_no", null));
                            arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

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
                            drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                            drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                            drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                            drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriorityVan"));
                            drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                            drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                            drDuplicateMailInsert.Add(new cArrayList("@DocType", "Canvaser"));
                            drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Daily"));
                            drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                            dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);

                            
                        }

                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetPriorityVan", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetNonPriority()
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;


            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            string period = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'");///Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProduct_Get");

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


                if (Convert.ToDateTime(dtServerDate) >= Convert.ToDateTime(dt))
                {
                    DataTable dtMailList = new DataTable();
                    dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItems_get");

                    DataView view = new DataView(dtMailList);
                    DataTable distinctValues = view.ToTable(true, "contactPerson");
                    foreach (DataRow drDistinctValues in distinctValues.Rows)
                    {
                        var results = from myRow in dtMailList.AsEnumerable()
                                      where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                      select myRow;
                        List<cArrayList> drDuplicateMail = new List<cArrayList>();
                        DataTable dtDuplicateMail = new DataTable();
                        drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                        dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                        int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);
                        // here we get productlist
                        if (checkDuplicateMail == 0)
                        {
                            #region collect PriorityItems
                            foreach (DataRow dr in results.ToList())
                            {
                                DateTime endDate = Convert.ToDateTime(dt);
                                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                                DataTable dtsalespointTargetPriorityItems = new DataTable();
                                productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                                salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                                salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsNP_get", salespointTargetPriorityItems);
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
                                    arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrTakeOrder.Add(new cArrayList("@endDate", dt));
                                    arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                    List<cArrayList> arrVanOrder = new List<cArrayList>();
                                    DataTable dtVanOrder = new DataTable();
                                    arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrVanOrder.Add(new cArrayList("@endDate", dt));


                                    arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                    List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                    DataTable dtProdGroupStock = new DataTable();
                                    arrProdGroupStock.Add(new cArrayList("@dtdate", dt));
                                    arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                    arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                    dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);


                                    List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                    DataTable dtProdGroupDepoStock = new DataTable();
                                    arrProdGroupDepoStock.Add(new cArrayList("@dtdate", dt));
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
                                    dtReport.Rows.Add(branchName,
                                        Convert.ToString(startDate),
                                        Convert.ToString(endDate),
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


                            rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTarget.rpt"));

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
                            string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                            string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                            rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                            rptDoc.Close();
                            List<cArrayList> arrMailPDF = new List<cArrayList>();
                            string subject = "N/A";
                            if (dtTemp.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                                {
                                    subject = " Target non priority by Product Group for All Salesman. No records found.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman. No records found."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman. No records found."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", null));
                                }
                                else
                                {
                                    subject = " Target non priority by Product Group for All Salesman.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                                }
                            }
                            else if (dtTemp.Rows.Count == 0)
                            {
                                subject = " Target non priority by Product Group for All Salesman. No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman. No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman. No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = " Target non priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            }
                            arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                            arrMailPDF.Add(new cArrayList("@token", null));
                            arrMailPDF.Add(new cArrayList("@doc_no", null));
                            arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));
                            DataTable dtMail = new DataTable();
                            if (true == true)
                            {
                                dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                                dtMail.Clear();
                            }
                            dtReport.Clear();

                            List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                            DataTable dtDuplicateMailInsert = new DataTable();
                            drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                            drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                            drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                            drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriority"));
                            drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                            drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                            drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                            drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Daily"));
                            drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                            dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                        }

                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetNonPriority", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetNonPriorityVan()
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            string period = bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'");//Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            DataTable allWeekDateForPriorityProduct = cdl.GetValueFromSP("sp_allWeekDateForPriorityProduct_Get");

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
                string dt = Convert.ToString(drDate[0]);
                string dtServerDate = Convert.ToString(bll.sGetControlParameter("WAZARAN_DT").Split('/')[0]) + '-' + Convert.ToString(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(Convert.ToInt32(bll.sGetControlParameter("WAZARAN_DT").Split('/')[1]))) + "-" + Convert.ToString(bll.sGetControlParameter("WAZARAN_DT").Split('/')[2]);
                if (Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length > 8)
                {
                    branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Substring(Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value).Length - 7);
                }
                else { branchName = Convert.ToString(HttpContext.Current.Request.Cookies["spn"].Value); }
                if (Convert.ToDateTime(dtServerDate) >= Convert.ToDateTime(dt))
                {
                    DataTable dtMailList = new DataTable();
                    dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItems_get");

                    DataView view = new DataView(dtMailList);
                    DataTable distinctValues = view.ToTable(true, "contactPerson");

                    foreach (DataRow drDistinctValues in distinctValues.Rows)
                    {
                        var results = from myRow in dtMailList.AsEnumerable()
                                      where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                      select myRow;

                        List<cArrayList> drDuplicateMail = new List<cArrayList>();
                        DataTable dtDuplicateMail = new DataTable();
                        drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                        dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                        int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);

                        // here we get productlist
                        if (checkDuplicateMail == 0)
                        {
                            #region collect PriorityItems
                            foreach (DataRow dr in results.ToList())
                            {
                                DateTime endDate = Convert.ToDateTime(dt);
                                DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                                DataTable dtsalespointTargetPriorityItems = new DataTable();
                                productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                                salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                                salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsVanNP_get", salespointTargetPriorityItems);
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
                                    arrTakeOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrTakeOrder.Add(new cArrayList("@endDate", dt));
                                    arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                    List<cArrayList> arrVanOrder = new List<cArrayList>();
                                    DataTable dtVanOrder = new DataTable();
                                    arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)));
                                    arrVanOrder.Add(new cArrayList("@endDate", dt));


                                    arrVanOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                    dtVanOrder = cdl.GetValueFromSP("sp_priorityAchievmentVan_get", arrVanOrder);

                                    List<cArrayList> arrProdGroupStock = new List<cArrayList>();
                                    DataTable dtProdGroupStock = new DataTable();
                                    arrProdGroupStock.Add(new cArrayList("@dtdate", dt));
                                    arrProdGroupStock.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                    arrProdGroupStock.Add(new cArrayList("@salespointcd", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
                                    arrProdGroupStock.Add(new cArrayList("@salesman_cd", Convert.ToString(dtPri["salesman_cd"])));
                                    dtProdGroupStock = cdl.GetValueFromSP("sp_ProdGroupStockForPeriorityItems", arrProdGroupStock);


                                    List<cArrayList> arrProdGroupDepoStock = new List<cArrayList>();
                                    DataTable dtProdGroupDepoStock = new DataTable();
                                    arrProdGroupDepoStock.Add(new cArrayList("@dtdate", dt));
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
                                        Convert.ToString(endDate),
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


                            rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTargetVan.rpt"));

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
                            string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                            string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                            rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                            rptDoc.Close();
                            List<cArrayList> arrMailPDF = new List<cArrayList>();
                            string subject = "N/A";
                            if (dtTemp.Rows.Count > 0)
                            {
                                if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                                {
                                    subject = " Target non priority by Product Group for Canvaser. No records found.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser. No records found."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser. No records found."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", null));
                                }
                                else
                                {
                                    subject = " Target non priority by Product Group for Canvaser.";
                                    arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser."));
                                    arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser."));
                                    arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                                }
                            }
                            else if (dtTemp.Rows.Count == 0)
                            {
                                subject = " Target non priority by Product Group for Canvaser. No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser. No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser. No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = " Target non priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + " Target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            }
                            arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                            arrMailPDF.Add(new cArrayList("@token", null));
                            arrMailPDF.Add(new cArrayList("@doc_no", null));
                            arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                            DataTable dtMail = new DataTable();
                            if (true == true)
                            {
                                dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                                dtMail.Clear();
                            }
                            dtReport.Clear();

                            List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                            DataTable dtDuplicateMailInsert = new DataTable();
                            drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                            drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                            drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                            drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                            drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriority"));
                            drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                            drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                            drDuplicateMailInsert.Add(new cArrayList("@DocType", "Canvaser"));
                            drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Daily"));
                            drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                            dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                        }

                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Logs("", "Daily Closing", "BranchTargetPriority", "fm_closingdaily", "BranchTargetNonPriorityVan", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetPriorityMonthly(string period)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

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
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
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
                            DateTime endDate = Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][1]);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target priority by Product Group for All Salesman.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));

                            }
                            else
                            {
                                subject = "Monthly target priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));

                            }
                        }
                        else
                        {
                            subject = "Monthly target priority by Product Group for All Salesman. No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman. No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));

                        }



                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));
                        
                        DataTable dtMail = new DataTable();
                        dtMail.Clear();
                        
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriorityMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }

        catch (Exception ex)
        {
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetPriorityMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetPriorityVanMonthly(string period)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


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
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
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
                            DateTime endDate = Convert.ToDateTime(dt);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target priority by Product Group for Canvaser.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + HttpContext.Current.Request.Cookies["sp"].Value + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + HttpContext.Current.Request.Cookies["sp"].Value + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = "Monthly target priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            }
                        }
                        else
                        {
                            subject = "Monthly target priority by Product Group for Canvaser.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));
                        }




                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                        
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriorityVanMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "Canvaser"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetPriorityVanMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetNonPriorityMonthly(string period)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";
            List<cArrayList> drMonth = new List<cArrayList>();
            //DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            string dtWazaran = period.Substring(4) + "/" + "01/" + DateTime.Now.Year.ToString();
            drMonth.Add(new cArrayList("@wazrDate", period));
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
                dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItems_get");

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");
                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);
                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);
                    // here we get productlist
                    if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][1]);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsNP_get", salespointTargetPriorityItems);
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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


                        rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTargetMonthly.rpt"));

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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target non priority by Product Group for All Salesman.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));

                            }
                            else
                            {
                                subject = "Monthly target non priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));

                            }
                        }
                        else
                        {
                            subject = "Monthly target non priority by Product Group for All Salesman.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman. No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));

                        }



                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        //if (true == true)
                        //{
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                            //dtMail.Clear();

                        //}
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriorityMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetNonPriorityMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void BranchTargetNonPriorityVanMonthly(string period)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            List<cArrayList> drMonth = new List<cArrayList>();
            //DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            string dtWazaran = period.Substring(4) + "/" + "01/" + DateTime.Now.Year.ToString();
            drMonth.Add(new cArrayList("@wazrDate", period));

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
                dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItems_get");

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");

                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);

                    // here we get productlist
                    if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(dt);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsVanNP_get", salespointTargetPriorityItems);
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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


                        rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTargetMonthlyVan.rpt"));

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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target non priority by Product Group for Canvaser.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = "Monthly target non priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                            }
                        }
                        else
                        {
                            subject = "Monthly target non priority by Product Group for Canvaser.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));
                        }




                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        if (true == true)
                        {
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                            //dtMail.Clear();

                        }
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriorityVanMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetNonPriorityVanMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }
    public void BranchTargetNonPriorityMonthlyBYUser(string period,string emp_cd)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //string period = Convert.ToString(DateTime.Now.Year) + (Convert.ToString(DateTime.Now.Month).Length == 1 ? ("0" + Convert.ToString(DateTime.Now.Month)) : Convert.ToString(DateTime.Now.Month));
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";
            List<cArrayList> drMonth = new List<cArrayList>();
            //DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            string dtWazaran = period.Substring(4) + "/" + "01/" + DateTime.Now.Year.ToString();
            drMonth.Add(new cArrayList("@wazrDate", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'")));
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
                List<cArrayList> drUser = new List<cArrayList>();
                drUser.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));

                dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItemsByUser_get", drUser);

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");
                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);
                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);
                    // here we get productlist
                    if (true == true) //if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][1]);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsNP_get", salespointTargetPriorityItems);
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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


                        rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTargetMonthly.rpt"));

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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target non priority by Product Group for All Salesman.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));

                            }
                            else
                            {
                                subject = "Monthly target non priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));


                                string salesTargetAchievementID = string.Empty;
                                List<cArrayList> arr = new List<cArrayList>();
                                arr.Add(new cArrayList("@Period", period));
                                arr.Add(new cArrayList("@transType", "TargetNonPriorityMonthlyBYUser"));
                                arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    arr.Clear();
                                    arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                    arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                    arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                    arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                    arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                    arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                    arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                    arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                    arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                    arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                    bll.vInsertTargetAchievementDtl(arr);
                                }
                            }
                        }
                        else
                        {
                            subject = "Monthly target non priority by Product Group for All Salesman.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for All Salesman. No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));

                        }



                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        //if (true == true)
                        //{
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                        dtMail.Clear();
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                        //dtMail.Clear();

                        //}
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVanNonPriority")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriorityMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetNonPriorityMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }
    public void BranchTargetNonPriorityVanMonthlyByUser(string period, string emp_cd)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            List<cArrayList> drMonth = new List<cArrayList>();
            //DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            string dtWazaran = period.Substring(4) + "/" + "01/" + DateTime.Now.Year.ToString();
            drMonth.Add(new cArrayList("@wazrDate", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'")));

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
                List<cArrayList> drUser = new List<cArrayList>();
                drUser.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));
                dtMailList = cdl.GetValueFromSP("sp_TargetNonPeriorityItemsByUser_get", drUser);

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");

                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);

                    // here we get productlist
                    if (true == true) //if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(dt);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
                            salespointTargetPriorityItems.Add(new cArrayList("@period", period));
                            salespointTargetPriorityItems.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                            dtsalespointTargetPriorityItems = cdl.GetValueFromSP("sp_SalespointTargetPriorityItemsVanNP_get", salespointTargetPriorityItems);
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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


                        rptDoc.Load(HttpContext.Current.Server.MapPath("NonPriorityTargetMonthlyVan.rpt"));

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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target non priority by Product Group for Canvaser.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = "Monthly target non priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));

                                string salesTargetAchievementID = string.Empty;
                                List<cArrayList> arr = new List<cArrayList>();
                                arr.Add(new cArrayList("@Period", period));
                                arr.Add(new cArrayList("@transType", "TargetNonPriorityVanMonthlyByUser"));
                                arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    arr.Clear();
                                    arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                    arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                    arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                    arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                    arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                    arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                    arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                    arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                    arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                    arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                    bll.vInsertTargetAchievementDtl(arr);
                                }
                            }
                        }
                        else
                        {
                            subject = "Monthly target non priority by Product Group for Canvaser.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target non priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));
                        }




                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        if (true == true)
                        {
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                            dtMail.Clear();
                            //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                            //dtMail.Clear();

                        }
                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("VanNonPriority")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetNonPriorityVanMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "Canvaser"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetNonPriorityVanMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }
    public void BranchTargetPriorityMonthlyByUser(string period, string emp_cd)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

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
            drMonth.Add(new cArrayList("@wazrDate", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'")));
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
                List<cArrayList> drUser = new List<cArrayList>();
                drUser.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));
                dtMailList = cdl.GetValueFromSP("sp_TargetPeriorityItemsByUser_get",drUser);

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");
                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;
                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);
                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);
                    // here we get productlist
                    if (true == true) //if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][1]);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target priority by Product Group for All Salesman.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));

                            }
                            else
                            {
                                subject = "Monthly target priority by Product Group for All Salesman.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));


                                string salesTargetAchievementID = string.Empty;
                                List<cArrayList> arr = new List<cArrayList>();
                                arr.Add(new cArrayList("@Period", period));
                                arr.Add(new cArrayList("@transType", "TargetPriorityMonthlyByUser"));
                                arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    arr.Clear();
                                    arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                    arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                    arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                    arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                    arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                    arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                    arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                    arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                    arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                    arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                    bll.vInsertTargetAchievementDtl(arr);
                                }
                            }
                        }
                        else
                        {
                            subject = "Monthly target priority by Product Group for All Salesman. No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for All Salesman. No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));

                        }



                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                        dtMail.Clear();
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailExcel);
                        //dtMail.Clear();

                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(allWeekDateForPriorityProduct.Rows[0][0])));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("NonVan")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriorityMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "All Salesman"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }

        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetPriorityMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }
    public void BranchTargetPriorityVanMonthlyByUser(string period, string emp_cd)
    {
        try
        {
            string body = string.Empty;
            string branchName = string.Empty;
            string hoTarget = string.Empty;
            string salesmanGroup = string.Empty;
            string productGroup = string.Empty;
            string productGroupCode = string.Empty;

            string dataHTML = string.Empty;


            // Genrate priority mail
            // mail date 01,07,14,21,28 and month close
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            int days = DateTime.DaysInMonth(year, month);
            //var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //string period = "201704";

            List<cArrayList> drMonth = new List<cArrayList>();
            DateTime dtWazaran = Convert.ToDateTime(period.Substring(4) + "/" + "01/" + DateTime.Now.Year);
            drMonth.Add(new cArrayList("@wazrDate", bll.vLookUp("select parm_valu from tcontrol_parameter where parm_nm = 'period'")));

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
                List<cArrayList> drUser = new List<cArrayList>();
                drUser.Add(new cArrayList("@emp_cd", Convert.ToString(emp_cd)));
                dtMailList = cdl.GetValueFromSP("sp_TargetPeriorityItemsByUser_get",drUser);

                DataView view = new DataView(dtMailList);
                DataTable distinctValues = view.ToTable(true, "contactPerson");

                foreach (DataRow drDistinctValues in distinctValues.Rows)
                {
                    var results = from myRow in dtMailList.AsEnumerable()
                                  where myRow.Field<string>("contactPerson") == Convert.ToString(drDistinctValues["contactPerson"])
                                  select myRow;

                    List<cArrayList> drDuplicateMail = new List<cArrayList>();
                    DataTable dtDuplicateMail = new DataTable();
                    drDuplicateMail.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                    drDuplicateMail.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                    drDuplicateMail.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                    dtDuplicateMail = cdl.GetValueFromSP("sp_PriorityProductMails_Get", drDuplicateMail);

                    int checkDuplicateMail = Convert.ToInt32(dtDuplicateMail.Rows[0][0]);

                    // here we get productlist
                    if (true == true) //if (checkDuplicateMail == 0)
                    {
                        #region collect PriorityItems
                        foreach (DataRow dr in results.ToList())
                        {
                            DateTime endDate = Convert.ToDateTime(dt);
                            DateTime startDate = new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1);
                            List<cArrayList> salespointTargetPriorityItems = new List<cArrayList>();
                            DataTable dtsalespointTargetPriorityItems = new DataTable();
                            productGroupCode += Convert.ToString(dr["prod_cds"]) + ",";
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
                                arrTakeOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));
                                arrTakeOrder.Add(new cArrayList("@saleman", Convert.ToString(dtPri["salesman_cd"])));
                                dtTakeOrder = cdl.GetValueFromSP("sp_priorityAchievmentTakeOrder_get", arrTakeOrder);


                                List<cArrayList> arrVanOrder = new List<cArrayList>();
                                DataTable dtVanOrder = new DataTable();
                                arrVanOrder.Add(new cArrayList("@prod_cd", Convert.ToString(dr["prod_cds"])));
                                arrVanOrder.Add(new cArrayList("@startDate", new DateTime(DateTime.Now.Year, Convert.ToInt32(period.Substring(period.Length - 2)), 1)));
                                arrVanOrder.Add(new cArrayList("@endDate", allWeekDateForPriorityProduct.Rows[0][1]));


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
                                    Convert.ToString(endDate),
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
                        string pdfFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf";
                        string xlsFile = sPath + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls";
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pdfFile);
                        rptDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, xlsFile);
                        rptDoc.Close();
                        List<cArrayList> arrMailPDF = new List<cArrayList>();
                        string subject = "N/A";
                        if (dtTemp.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(dtTemp.Compute("Sum(TotalAchievment)", string.Empty)) == 0)
                            {
                                subject = "Monthly target priority by Product Group for Canvaser.No records found.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", null));
                            }
                            else
                            {
                                subject = "Monthly target priority by Product Group for Canvaser.";
                                arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser."));
                                arrMailPDF.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));


                                string salesTargetAchievementID = string.Empty;
                                List<cArrayList> arr = new List<cArrayList>();
                                arr.Add(new cArrayList("@Period", period));
                                arr.Add(new cArrayList("@transType", "TargetPriorityVanMonthlyByUser"));
                                arr.Add(new cArrayList("@salespointcd", HttpContext.Current.Request.Cookies["sp"].Value));
                                arr.Add(new cArrayList("@prod_cd", Convert.ToString(dtTemp.Rows[0]["ProductGroup"])));
                                arr.Add(new cArrayList("@hoTarget", Convert.ToString(dtTemp.Rows[0]["HOTarget"])));
                                arr.Add(new cArrayList("@priodFrom", Convert.ToDateTime(dtTemp.Rows[0]["StartDate"])));
                                arr.Add(new cArrayList("@priodTo", Convert.ToDateTime(dtTemp.Rows[0]["EndDate"])));
                                arr.Add(new cArrayList("@createdby", Convert.ToString(HttpContext.Current.Request.Cookies["usr_id"].Value)));

                                bll.vInsertTargetAchievement(arr, ref salesTargetAchievementID);

                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    arr.Clear();
                                    arr.Add(new cArrayList("@SalesTargetAchievementID", salesTargetAchievementID));
                                    arr.Add(new cArrayList("@emp_cd", Convert.ToString(dtTemp.Rows[0]["Salesman"])));
                                    arr.Add(new cArrayList("@BrnTarget", Convert.ToDecimal(dtTemp.Rows[0]["BrnTarget"])));
                                    arr.Add(new cArrayList("@AddTarget", Convert.ToDecimal(dtTemp.Rows[0]["AddTarget"])));
                                    arr.Add(new cArrayList("@TargetTotal", Convert.ToDecimal(dtTemp.Rows[0]["TargetTotal"])));
                                    arr.Add(new cArrayList("@VanSales", Convert.ToDecimal(dtTemp.Rows[0]["VanSales"])));
                                    arr.Add(new cArrayList("@TakingOrder", Convert.ToDecimal(dtTemp.Rows[0]["TakingOrder"])));
                                    arr.Add(new cArrayList("@TotalAchievment", Convert.ToDecimal(dtTemp.Rows[0]["TotalAchievment"])));
                                    arr.Add(new cArrayList("@BrnTargetAchievment", Convert.ToDecimal(dtTemp.Rows[0]["BrnTargetAchievment"])));
                                    arr.Add(new cArrayList("@TotalTarget", Convert.ToDecimal(dtTemp.Rows[0]["TotalTarget"])));
                                    bll.vInsertTargetAchievementDtl(arr);
                                }
                            }
                        }
                        else
                        {
                            subject = "Monthly target priority by Product Group for Canvaser.No records found.";
                            arrMailPDF.Add(new cArrayList("@emailsubject", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@msg", "[" + branchName + "] " + "Monthly target priority by Product Group for Canvaser.No records found."));
                            arrMailPDF.Add(new cArrayList("@file_attachment", null));
                        }




                        arrMailPDF.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        arrMailPDF.Add(new cArrayList("@token", null));
                        arrMailPDF.Add(new cArrayList("@doc_no", null));
                        arrMailPDF.Add(new cArrayList("@doc_typ", "Salespoint Monthly target to Salesman"));

                        //List<cArrayList> arrMailExcel = new List<cArrayList>();

                        //arrMailExcel.Add(new cArrayList("@emailsubject", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@msg", "Target priority by Product Group. "));
                        //arrMailExcel.Add(new cArrayList("@file_attachment", folderLocation + HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        //arrMailExcel.Add(new cArrayList("@to", Convert.ToString(drDistinctValues["contactPerson"])));
                        //arrMailExcel.Add(new cArrayList("@token", null));
                        //arrMailExcel.Add(new cArrayList("@doc_no", null));
                        //arrMailExcel.Add(new cArrayList("@doc_typ", "Salespoint Target to Salesman"));

                        DataTable dtMail = new DataTable();
                        //dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                        dtMail.Clear();

                        dtReport.Clear();

                        List<cArrayList> drDuplicateMailInsert = new List<cArrayList>();
                        DataTable dtDuplicateMailInsert = new DataTable();
                        drDuplicateMailInsert.Add(new cArrayList("@emaildate", Convert.ToDateTime(dt)));
                        drDuplicateMailInsert.Add(new cArrayList("@email", Convert.ToString(drDistinctValues["contactPerson"])));
                        drDuplicateMailInsert.Add(new cArrayList("@salesmanType", Convert.ToString("Van")));
                        drDuplicateMailInsert.Add(new cArrayList("@MethodName", "BranchTargetPriorityVanMonthly"));
                        drDuplicateMailInsert.Add(new cArrayList("@Period", bll.sGetControlParameter("period")));
                        drDuplicateMailInsert.Add(new cArrayList("@PDFFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".pdf"));
                        drDuplicateMailInsert.Add(new cArrayList("@ExcelFile", HttpContext.Current.Request.Cookies["sp"].Value + fileName + ".xls"));
                        drDuplicateMailInsert.Add(new cArrayList("@PriorityFileName", fileName)); fileName = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@ProductName", productGroupCode)); productGroupCode = string.Empty;
                        drDuplicateMailInsert.Add(new cArrayList("@DocType", "Canvaser"));
                        drDuplicateMailInsert.Add(new cArrayList("@DailyMonth", "Month"));
                        drDuplicateMailInsert.Add(new cArrayList("@SubjectMail", subject));
                        dtDuplicateMailInsert = cdl.GetValueFromSP("sp_PriorityProductMails_inst", drDuplicateMailInsert);
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "al", "sweetAlert('Attention','Error occured during pageload','error');", true);
            Logs("", "Monthly Closing", "BranchTargetPriority", "fm_closingmonthly", "BranchTargetPriorityVanMonthly", "Exception", ex.Message + ex.InnerException);
        }
    }
    public void NewTargetByEDP()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@SalesPointCD", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
            string fileName = "NewSalesTargetMail_" + Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value);
            string sPath = bll.sGetControlParameter("image_path");
            string sPdfName = fileName + ".pdf";
            List<cArrayList> arrCheckData = new List<cArrayList>();
            arrCheckData.Add(new cArrayList("@SalesPointCD", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
            DataTable dt = new DataTable();
            dt = cdl.GetValueFromSP("sp_NewSalesTargetMail", arr);
            if (dt.Rows.Count > 0)
            {
                rep.vShowReportToPDFWithSP("NewSalesTargetMail.rpt", arr, sPath + sPdfName);

                List<cArrayList> arrEmail = new List<cArrayList>();
                DataTable dtEmail = new DataTable();
                var salesPointCD = Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value.ToString());
                arrEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                dtEmail = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrEmail);

                if (dtEmail.Rows.Count > 0)
                {

                    //foreach (DataRow dr in dtEmail.Rows)
                    //{
                    List<cArrayList> arrMailPDF = new List<cArrayList>();
                    arrMailPDF.Add(new cArrayList("@emailsubject", "New target priority product group data to Salesman by branch."));
                    arrMailPDF.Add(new cArrayList("@msg", "New target priority product group data to Salesman by branch."));
                    arrMailPDF.Add(new cArrayList("@file_attachment", sPdfName));
                    arrMailPDF.Add(new cArrayList("@to", Convert.ToString(Convert.ToString(dtEmail.Rows[0]["email"]) + ";hani@sbtcgroup.com;frk@sbtcgroup.com")));
                    arrMailPDF.Add(new cArrayList("@token", null));
                    arrMailPDF.Add(new cArrayList("@doc_no", null));
                    arrMailPDF.Add(new cArrayList("@doc_typ", "Branch Target to Salesman"));

                    DataTable dtMail = new DataTable();
                    dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                    //}

                }
            }
        }
        catch (Exception ex)
        {
            Logs("", "UpdatedTargetByEDP", "UpdatedTargetByEDP", "UpdatedTargetByEDP", "SendEmail", "Exception", ex.Message + ex.InnerException);
        }
    }

    public void UpdatedTargetByEDP()
    {
        try
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@SalesPointCD", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
            string fileName = "UpdateSalesTargetMail_" + Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value);
            string sPath = bll.sGetControlParameter("image_path");
            string sPdfName = fileName + ".pdf";
            List<cArrayList> arrCheckData = new List<cArrayList>();
            arrCheckData.Add(new cArrayList("@SalesPointCD", Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value)));
            DataTable dt = new DataTable();
            dt = cdl.GetValueFromSP("sp_UpdateSalesTargetMail", arr);
            if (dt.Rows.Count > 0)
            {
                rep.vShowReportToPDFWithSP("UpdateSalesTargetMail.rpt", arr, sPath + sPdfName);

                List<cArrayList> arrEmail = new List<cArrayList>();
                DataTable dtEmail = new DataTable();
                var salesPointCD = Convert.ToString(HttpContext.Current.Request.Cookies["sp"].Value.ToString());
                arrEmail.Add(new cArrayList("@salespointcd", salesPointCD));
                dtEmail = cdl.GetValueFromSP("sp_BranchSupervisor_get", arrEmail);

                if (dtEmail.Rows.Count > 0)
                {

                    //foreach (DataRow dr in dtEmail.Rows)
                    //{
                    List<cArrayList> arrMailPDF = new List<cArrayList>();
                    arrMailPDF.Add(new cArrayList("@emailsubject", "Updated target priority product group data to Salesman by branch."));
                    arrMailPDF.Add(new cArrayList("@msg", "Updated target priority product group data to Salesman by branch."));
                    arrMailPDF.Add(new cArrayList("@file_attachment", sPdfName));
                    arrMailPDF.Add(new cArrayList("@to", Convert.ToString(Convert.ToString(dtEmail.Rows[0]["email"]) + ";hani@sbtcgroup.com;frk@sbtcgroup.com")));
                    arrMailPDF.Add(new cArrayList("@token", null));
                    arrMailPDF.Add(new cArrayList("@doc_no", null));
                    arrMailPDF.Add(new cArrayList("@doc_typ", "Branch Target to Salesman"));

                    DataTable dtMail = new DataTable();
                    dtMail = cdl.GetValueFromSP("sp_temail_outbox_ins", arrMailPDF);
                    //}

                }
            }
        }
        catch (Exception ex)
        {
            Logs("", "UpdatedTargetByEDP", "UpdatedTargetByEDP", "UpdatedTargetByEDP", "SendEmail", "Exception", ex.Message + ex.InnerException);
        }

    }


    public class Data
    {
        public string DomainName { get; set; }
        public string ModuleName { get; set; }
        public string FormName { get; set; }
        public string MethodName { get; set; }
        public string ErrorType { get; set; }
        public string Error { get; set; }
        public string BrnachCode { get; set; }
        public string CreatedBy { get; set; }
    }
}