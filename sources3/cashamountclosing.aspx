<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashamountclosing.aspx.cs" Inherits="cashamountclosing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
     <link href="https://fonts.googleapis.com/css?family=Khula" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/index.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_cashamountclosing.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
        $("#form1").find('input, select, a').filter(':visible:enabled:first').focus();
    </script>
     <style>
        #form1{
            display: flex;
            justify-content: center;
            align-content: center;
            position: absolute;
            top:0;
            left:0;
            width: 100%;
        }
       #mydiv {
            border: 1px solid #ccc;
            background-color: #f3f3f3;
            padding: 5px 20px 20px 20px;
            width:70%;
            min-width:500px;
            
        }
       .danger{
           margin-left: -21px;
           margin-right: -21px;
       }
       .danger > .row{
           padding-bottom: 15px;
           padding-top:15px;
           margin-left:0;
           margin-right:0;
           color: #fff;
           font-weight: 100;
       }
       #mydiv .control-label{
           position: relative;
           line-height: 15px;
           padding-top: 0;
           padding-right: 10px;
           font-size: 12px;
       }
      
       .control-label:after{
           position:absolute;
           content:":";
           top:0;
           right: 0;
       }
       input{
           color:#555;
       }
       .text-bold{
           font-weight:bold !important;
       }
       .margin-bottom-5{
           margin-bottom:5px !important;
       }
       .input-sm {
            height: auto;
            padding: 3px 5px;
            font-size: 12px;
            line-height: normal; 
            border-radius: 3px;
        }
        </style>
</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <div id="mydiv">
        <div class="divheader">Entry Nominal Amount</div>
        
        <div class="container-fluid danger">
            <div class="row ">
                <div class="clearfix">
                    <label class="control-label col-xs-2">Cash Closing No </label>
                    <div class="col-xs-6">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbchclosingno" runat="server" Font-Bold="True"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                                </asp:UpdatePanel>
                                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-offset-1 col-xs-3 text-right">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn-sm btn-block btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                    </div>
                </div>
                <div class="clearfix">
                    <label class="control-label col-xs-2" >Date</label>
                    <div class="col-xs-10">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbchclosing_dt" runat="server" Font-Bold="True" ></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="control-label col-xs-2" >Total Amount</label>
                    <div class="col-xs-10">
                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtotalamount" runat="server" Font-Bold="True"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="control-label col-xs-2" >Note</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txchclosing_description" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="row margin-top">
                <div class="col-xs-offset-3 col-xs-6 text-center">Amount</div>
                <div class="col-xs-3 text-center">Total</div>
            </div>
            <div class="row margin-top">
                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount of 500</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                 <asp:TextBox ID="txamt500" runat="server" AutoPostBack="True" OnTextChanged="txamt500_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                    <asp:Label ID="lbtot500" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt500" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 100</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                 <asp:TextBox ID="txamt100" runat="server" OnTextChanged="txamt100_TextChanged" AutoPostBack="true" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot100" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt100" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 50</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                             <ContentTemplate>
                                 <asp:TextBox ID="txamt50" runat="server" AutoPostBack="True" OnTextChanged="txamt50_TextChanged"  CssClass="form-control input-sm"></asp:TextBox>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot50" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt50" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 20</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt20" runat="server" AutoPostBack="True" OnTextChanged="txamt20_TextChanged"  CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                  <asp:Label ID="lbtot20" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt20" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 10</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt10" runat="server" AutoPostBack="True" OnTextChanged="txamt10_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                 <asp:Label ID="lbtot10" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt10" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix margin-bottom-5"  >
                    <label class="control-label col-xs-2">Amount Of 5</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt5" runat="server" AutoPostBack="True" OnTextChanged="txamt5_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div  class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot5" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt5" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    
                    </div>
            </div>

                <div class="clearfix margin-bottom-5"  >
                    <label  class="col-xs-2 control-label">Amount Of 1</label>
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt1" runat="server" AutoPostBack="True" OnTextChanged="txamt1_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot1" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamt1" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    
                    </div>
                </div>
            
                <div class="clearfix margin-bottom-5">
                    <label  class="col-xs-2 control-label">Amount Of 0.50</label>
                
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamthalf" runat="server" AutoPostBack="True" OnTextChanged="txamthalf_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3  text-center">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot05" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txamthalf" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>     
                    </div>
                </div>
          
                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 0.25</label>
                
                     <div class="col-xs-7">
                         <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                             <ContentTemplate>
                                 <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                 </asp:UpdatePanel>
                                 <asp:TextBox ID="txamt025" runat="server" AutoPostBack="True" OnTextChanged="txamt025_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                     <div class="col-xs-3 text-center">
                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbtot025" runat="server" CssClass="text-bold text-red"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                     </div>
                 </div>
         
                <div class="clearfix margin-bottom-5">
                    <label class="control-label col-xs-2">Amount Of 0.10</label>
                
                    <div class="col-xs-7">
                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt01" runat="server" AutoPostBack="True" OnTextChanged="txamt01_TextChanged" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-xs-3 text-center">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbtot01" runat="server" CssClass="text-bold text-red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

             
                <div class="col-xs-offset-7 col-xs-2">
                    <strong>TOTAL :</strong>
                </div>
                <div class="col-xs-3"  style="border-top: 1px dashed #555;">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" class="text-center">
                        <ContentTemplate>
                            <asp:Label ID="lbtotal" runat="server" CssClass="text-bold text-red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row">
                <asp:Label ID="lbalert" runat="server"></asp:Label>
            </div>
            <div class="row navi">
                
                     <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                         <ContentTemplate>
                             <asp:Button ID="btsave" runat="server" CssClass="btn btn-warning btn-save" OnClick="btsave_Click" Text="Save" />
                             <asp:Button ID="btclose" runat="server" CssClass="btn btn-danger btn-close" OnClick="btclose_Click" Text="Close" />
                         </ContentTemplate>
                     </asp:UpdatePanel>
                     <%--<asp:Button ID="btclose" runat="server" Text="Close" CssClass="button2 btn" OnClick="btclose_Click" />--%>
            
            </div>
        </div>

        
     </div>
        
    </form>
</body>
</html>
