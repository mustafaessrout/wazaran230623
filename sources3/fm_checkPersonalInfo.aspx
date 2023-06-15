<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_checkPersonalInfo.aspx.cs" Inherits="fm_checkPersonalInfo" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link href="css/loginstyle.css" rel="stylesheet" />--%>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/custom/style.css" rel="stylesheet" />

  
   

    <!--custom js-->
    <script src="js/index.js"></script>
       <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

      
    </script>


    <style>
        .date .ajax__calendar_header .ajax__calendar_title,
        .ajax__calendar_dayname,
        .ajax__calendar_month,.ajax__calendar_year,
        .ajax__calendar_year, .date .ajax__calendar_months .ajax__calendar_hover .ajax__calendar_month{
            color:#555;
        }

        
        input[type="text"]{
            border-radius:0 !Important;
        }
    </style>

</head>
<body class="login4 center middle" style="height:100%;">
     
    <div class="content">
         <div class="logo">
            <img src="image/logo1.jpg" alt="logo sbtc" />
        </div>
       <form id="frm" runat="server" class="login-form">
           <h3 class="title text-center">Check Personal Information</h3>
           
            <div class="login-inputs">
                  <label id="Label1" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your ID (Iqama) Number:</label>
                    </div>
                <div class="border-white border-tops">
                    <div class="border-white border-bottoms">
                    
                        <asp:TextBox ID="UserID" runat="server" CssClass="form-control" style="background:none; color:#fff;" AutoCompleteType="Disabled"  maxlength="10" ></asp:TextBox><br />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserID"
                        ValidationExpression="^[12]\d{9}$"
                        ErrorMessage="The Id Number is not Valid, Please Try Again! " ForeColor="Red" />
                        <ajaxToolkit:FilteredTextBoxExtender ID="UserID_FilteredTextBoxExtender" runat="server"
                            Enabled="True" TargetControlID="UserID" FilterType="Numbers">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </div>
                    </div>
          
                   <%--       <asp:ScriptManager EnablePartialRendering="true"
                    ID="ScriptManager1" runat="server">
                </asp:ScriptManager>--%>

            <ajaxToolkit:ToolkitScriptManager  EnablePartialRendering="true"
               runat="server" >
            </ajaxToolkit:ToolkitScriptManager>
        <div class="login-inputs">
                <label class="control-label col-sm-12" style="padding-top: 17px;">Enter Your Birth Date:</label>   
        </div> 
                      <div class="border-white border-tops control-label col-sm-12 no-padding">
                    <div class="border-white border-bottoms drop-down-date" style="margin-bottom:20px" >
                    
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>      
                               
                                <asp:TextBox ID="dtBirthDate" runat="server" CssClass="form-control" style="background:none; color:#fff;" autocompletetype="Disabled" onkeydown="return false;" ></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date"  ID="dtBirthDate_CalendarExtender" runat="server" TargetControlID="dtBirthDate"  DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy"  DefaultView="Years">
                                </ajaxToolkit:CalendarExtender>

                  
                           </ContentTemplate>
                        </asp:UpdatePanel>
                                       
                  </div>
            </div>
                   
          <div class="login-inputs">
                  <label id="Label3" runat="server" class="control-label col-sm-12" style="padding-top: 17px;">Enter Your Mobile Number:</label>
                    </div>  
                <div class="border-white tops">
                    <div class="border-white bottoms" style="margin-bottom:17px" >
                        <div class="col-sm-2">
                            <label id="Label4" runat="server" class="control-label " style="padding-top: 10px; font-size:16px">(+966)</label>

                        </div>
                        <div class="col-sm-10 no-padding-right ">
                        <asp:TextBox ID="MobileNo" runat="server" CssClass="form-control" placeholder="512345678" style="background:none; color:#fff;" AutoCompleteType="Disabled" maxlength="9" ></asp:TextBox></div><br />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="MobileNo"
                        ValidationExpression="^[5]\d{8}$"
                        ErrorMessage="The Mobile Number is not Valid, Please Try Again! " ForeColor="Red" />
                         <ajaxToolkit:FilteredTextBoxExtender ID="MobileNo_FilteredTextBoxExtender1" runat="server"
                            Enabled="True" TargetControlID="MobileNo" FilterType="Numbers">
                        </ajaxToolkit:FilteredTextBoxExtender>
            </div>
                    </div>
            
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                    <div style="margin-bottom:25px">
                <asp:Label ID="lblMessage" runat="server" />
                </div>
                
                <div style="margin-bottom:15px">
                    <asp:Button ID="btSubmit" runat="server" Text="Submit" OnClick="btSubmit_Click" CssClass="btn btn-white full"  />
                </div>
                <%--<div>
                    <asp:Button ID="btClose" runat="server" Text="Close" OnClick="btClose_Click" CssClass="btn btn-white full"  />
                </div>--%>
                </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btSubmit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                   </div>  
            
        
        </form>
    </div> 

</body>
</html>
