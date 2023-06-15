using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using GsmComm.PduConverter;
using GsmComm.PduConverter.SmartMessaging;
using GsmComm.GsmCommunication;
using GsmComm.Interfaces;
using GsmComm.Server;
using System.Globalization;
/// <summary>
/// Summary description for csendsms
/// </summary>
public class csendsms
{
   
    cbll bll = new cbll();

    string _doctype;

    public string doctype
    { set { _doctype = value; } get { return (_doctype); } }
    public csendsms()
	{
      //string sPort = bll.sGetControlParameter("sms_port");
      //if (cd.comm == null)
      //{
      //    cd.comm = new GsmCommMain(sPort, 9600, 150);
      //}
      //if (!cd.comm.IsOpen())
      //{ cd.comm.Open(); }
      //cd.comm.EnableMessageNotifications();
      //cd.comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);      
 	}

    void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
    {
        //string sSMS = "";
        //try
        //{
        //    IMessageIndicationObject obj = e.IndicationObject;
        //    MemoryLocation loc = (MemoryLocation)obj;
        //    DecodedShortMessage[] messages;
        //    messages = cd.comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, loc.Storage);

        //    foreach (DecodedShortMessage message in messages)
        //    {
        //        // SmsDeliverPdu data = new SmsDeliverPdu();

        //        SmsPdu smsrec = message.Data;
        //        ShowMessage(smsrec);
        //        cd.comm.DeleteMessages(DeleteScope.ReadAndSent, loc.Storage);

        //    }
        //}
        //catch (Exception ex)
        //{
        //    List<cArrayList> arr = new List<cArrayList>();
        //    arr.Add(new cArrayList("@err_source", "comm_MessageReceived"));
        //    arr.Add(new cArrayList("@err_description", ex.Message.ToString()));
        //    bll.vInsertErrorLog(arr);
        //}
    }

    private void ShowMessage(SmsPdu pdu)
    {// Received message
        string actualtext = "";
       // string sSender = "";
        SmsDeliverPdu data = (SmsDeliverPdu)pdu;
        actualtext = data.UserDataText;
        string sFrom = data.OriginatingAddress;
        doctype = "customer";
        List<cArrayList> arr = new List<cArrayList>();
        arr.Add(new cArrayList("@mobile_text", actualtext));
        arr.Add(new cArrayList("@mobile_no", sFrom));
        arr.Add(new cArrayList("@sms_sta_id", actualtext));
        arr.Add(new cArrayList("@cusdoc_typ", doctype));
        bll.vInsertSMSReceived(arr);
        return;
    }
        
    public void vSendSms(string sMessage, string sPhoneNumber)
    {
      //  try
      //  {
      //      if (!cd.comm.IsOpen())
      //      { cd.comm.Open(); }
      //      SmsSubmitPdu pdu;
      //      byte dcs = (byte)DataCodingScheme.GeneralCoding.Alpha7BitDefault;
      //      pdu = new SmsSubmitPdu(sMessage, sPhoneNumber, dcs);
      //      cd.comm.SendMessage(pdu);
      //  }
      //  finally { 
      ////  comm.Close();
      //  }
         
    }
}