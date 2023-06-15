using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using GsmComm.PduConverter;
using GsmComm.PduConverter.SmartMessaging;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.Server;
using System.Web.UI.WebControls;
public static class cd
{
   
    public const string dttextbox = "1";
    public const string dtcalendar = "2";
    public const string dtdropdownlist = "3";
    public const string dtdropdownlistfieldvalue = "4";
    public const string dttextboxcustomer = "5";
    public const string dttextboxemployee = "6";
    public const string dtlookupsupplier = "7";
	public const string dtlookupitemcashout = "8";
    public static string sDocType = "";
    private static SqlConnection cnn;
    public static GsmCommMain comm;
    private static SqlConnection connho;
    public const string cssbuttonprint = "btn btn-info btn-lg";
    public const string cssbuttonsave = "btn btn-warning btn-lg";
    public const string cssbuttonnew = "btn btn-primary btn-lg";
    public const string cssbuttonedit = "btn btn-success btn-lg";
    public const string csstextro = "form-control ro";
    public const string csstext = "form-control";

    public static void v_readonly (TextBox obj)
    {
        obj.BackColor = System.Drawing.Color.GhostWhite;
        obj.ReadOnly = true;
    }
    public static void v_readwrite (TextBox obj)
    {
        obj.ReadOnly = false; obj.BackColor = System.Drawing.Color.White;
    }
    public static void v_readonly(DropDownList obj)
    {
        obj.BackColor = System.Drawing.Color.GhostWhite;
        obj.Attributes.Add("readonly","true");
        obj.BackColor = System.Drawing.Color.White;
    }
    public static void v_readwrite(DropDownList obj)
    {
        obj.Attributes.Add("readonly","false");
    }

    public static void v_disablecontrol(Label obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "silver");
        obj.TabIndex = -1;
    }
    public static void v_disablecontrol(FileUpload obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "silver");
        obj.TabIndex = -1;
    }

    public static void v_enablecontrol(FileUpload obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
    }
    public static void v_disablecontrol(RadioButtonList obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
    }
    public static void v_disablecontrol(RadioButton obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
    }
    public static void v_disablecontrol(LinkButton obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
        obj.TabIndex = -1;
    }
    public static void v_enablecontrol(LinkButton obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
        obj.OnClientClick = "return true";
    }
    public static void v_enablecontrol(Button obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
    }
    public static void v_disablecontrol(GridView obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
        obj.Enabled = false;
    }
    public static void v_disablecontrol(TextBox obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
        obj.ReadOnly = true;
        obj.TabIndex = -1;
    }
    public static void v_disablecontrol(DropDownList obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
        
        //obj.Attributes.Add("disabled","true");
        //obj.TabIndex = -1;
    }

    public static void v_disablecontrol(CheckBox obj)
    {
        obj.Style.Add("pointer-events", "none");
        obj.Style.Add("background-color", "LightGray");
    }

    public static void v_enablecontrol(GridView obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
        obj.Enabled = true;
    }
    public static void v_enablecontrol(TextBox obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
        obj.ReadOnly = false;
        obj.TabIndex = 1;
    }
    public static void v_enablecontrol(DropDownList obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
        obj.Attributes.Remove("disabled");
        obj.TabIndex = 1;
    }
    public static void v_enablecontrol(CheckBox obj)
    {
        obj.Style.Add("pointer-events", "normal");
        obj.Style.Add("background-color", "normal");
    }
    public static void v_showcontrol(Panel btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(Label btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(GridView btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(Button btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(CheckBox btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(TextBox btn)
    {
        btn.Style.Add("display", "normal");
        btn.ReadOnly = false;
    }
    public static void v_showcontrol(DropDownList btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_showcontrol(LinkButton btn)
    {
        btn.Style.Add("display", "normal");
    }

    public static void v_hiddencontrol(Panel btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_hiddencontrol(GridView btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_hiddencontrol(Button btn)
    {
        btn.Style.Add("display", "normal");
    }
    public static void v_hiddencontrol(Label btn)
    {
        btn.Style.Add("display", "none");
    }
    public static void v_hiddencontrol(CheckBox btn)
    {
        btn.Style.Add("display", "none");
    }
    public static void v_hiddencontrol(TextBox btn)
    {
        btn.Style.Add("display", "none");
    }
    public static void v_hiddencontrol(DropDownList btn)
    {
        btn.Style.Add("display", "none");
    }
    public static void v_hiddencontrol(LinkButton btn)
    {
        btn.Style.Add("display", "none");
    }

    
    static cd()
    {
        //string sPort = ConfigurationManager.ConnectionStrings["commport"].ConnectionString.ToString();// "COM67";
        //if (comm == null)
        //{
        //    comm = new GsmCommMain(sPort, 9600, 150);
        //}
        //if (!comm.IsOpen())
        //{ comm.Open(); }
        //comm.EnableMessageNotifications();
        //comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
      //  keepAliveSMS();
    }


    public static void keepAliveSMS()
    {
        string sPort = ConfigurationManager.ConnectionStrings["commport"].ConnectionString.ToString();// "COM67";
        if (comm == null)
        {
            comm = new GsmCommMain(sPort, 9600, 150);
        }
        if (!comm.IsOpen())
        { comm.Open(); }
        comm.EnableMessageNotifications();
        comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
    }

    static void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        cbll bll = new cbll();
        //string sSMS = "";
        try
        {
            IMessageIndicationObject obj = e.IndicationObject;
            MemoryLocation loc = (MemoryLocation)obj;
            DecodedShortMessage[] messages;
            messages = comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, loc.Storage);

            foreach (DecodedShortMessage message in messages)
            {
                // SmsDeliverPdu data = new SmsDeliverPdu();

                SmsPdu smsrec = message.Data;
                ShowMessage(smsrec);
                comm.DeleteMessages(DeleteScope.ReadAndSent, loc.Storage);

            }
        }
        catch (Exception ex)
        {
            List<cArrayList> arr = new List<cArrayList>();
            arr.Add(new cArrayList("@err_source", "comm_MessageReceived"));
            arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
            bll.vInsertErrorLog(arr);
        }
        finally
        { bll = null; }
    }

    private static void ShowMessage(SmsPdu pdu)
    {// Received message
        cbll bll = new cbll();
        string doctype;
        string actualtext = "";
        // string sSender = "";
        SmsDeliverPdu data = (SmsDeliverPdu)pdu;
        actualtext = data.UserDataText;
        string sFrom = data.OriginatingAddress;
        doctype = sDocType;
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@mobile_text", actualtext));
        arr.Add(new cArrayList("@mobile_no", sFrom));
        arr.Add(new cArrayList("@sms_sta_id", actualtext));
        arr.Add(new cArrayList("@cusdoc_typ", doctype));
        bll.vInsertSMSReceived(arr); bll = null;
       
        return;
    }

    public static void vSendSms(string sMessage, string sPhoneNumber)
    {
        cbll bll = new cbll();
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@msg", sMessage));
        arr.Add(new cArrayList("@to", sPhoneNumber));
        bll.vInsertSmsOutbox(arr); 
       
         
        //cbll bll = new cbll();
        //try
        //{
        //    if (!comm.IsOpen())
        //    { comm.Open(); }
        //    SmsSubmitPdu pdu;
        //    byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
        //    pdu = new SmsSubmitPdu(sMessage, sPhoneNumber, dcs);
        //    comm.SendMessage(pdu);
        //}
        //catch (Exception ex) {
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@err_source", "sendsms"));
        //    arr.Add(new cArrayList("@err_description",ex.Message.ToString()));
        //    bll.vInsertErrorLog(arr);
        //}
       

    }
    public static SqlConnection getConnection()
    {
        cbll bll = new cbll();
        if (cnn == null)
        {
            try
            {
                //cnn = new SqlConnection();
                //cnn.ConnectionString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString.ToString();
                //cnn.Open();by yanto 19-11-2018
                //string susersql = HttpContext.Current.Request.Cookies["usersql"].Value.ToString();
                //HttpContext.Current.Response.Cookies["usersql"].Expires = DateTime.Now.AddDays(-1);
                string ScrtCon = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                //ScrtCon = string.Format(ScrtCon, susersql);
                cnn = new SqlConnection();
                cnn.ConnectionString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString.ToString();
                cnn.Open();
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, "cd");
                HttpContext.Current.Response.Redirect("fm_loginol.aspx");
            }
                
        }
        else if (cnn != null && cnn.State == ConnectionState.Closed)
        {
            try
            {
                string susersql = HttpContext.Current.Request.Cookies["usersql"].Value.ToString();
                string ScrtCon = ConfigurationManager.ConnectionStrings["connstr"].ToString();
                HttpContext.Current.Response.Cookies["usersql"].Expires = DateTime.Now.AddDays(-1);
                ScrtCon = string.Format(ScrtCon, susersql);
                cnn = new SqlConnection();
                cnn.ConnectionString = ScrtCon;
                cnn.Open();
            }
            catch (Exception ex)
            {
                bll.vHandledError(ref ex, "cd");
                HttpContext.Current.Response.Redirect("fm_loginol.aspx");
            }
                
        }
        return (cnn);        
    }

    public static SqlConnection getConnectionHO()
    {
        if (connho == null)
        {
            connho = new SqlConnection();
            connho.ConnectionString = ConfigurationManager.ConnectionStrings["connnav"].ConnectionString.ToString();
            connho.Open();
        }
        return (connho);
    }
}
