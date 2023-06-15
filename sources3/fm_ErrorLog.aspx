<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_ErrorLog.aspx.cs" Inherits="fm_ErrorLog" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>--%>
    <script src="css/jquery-1.9.1.js"></script>
        <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-ui.js"></script>
    
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>



    <script>
        

    </script>
    <style type="text/css">
       
    </style>
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid">
        <div class="row">     
    <h2><span class="label label-primary col-md-3">Error Log </span>
        <span class="label label-default col-md-2"">

            
            <asp:Label ID="lbsp" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
        </span>
    </h2>
            
   </div>
      
          <asp:Panel ID="Panel1" runat="server"  
      style="width:100%"  GroupingText="Error Condition">
              <div class="div-table">

                  <div class="div-table-row">
                      <div class="div-table-col1" align="center"></div>
                      <div class="div-table-col2"></div>
                      <div class="div-table-col3"></div>
                      <div class="div-table-col4"></div>
                  </div>
                  <div class="div-table-row">
                      <div class="div-table-col1">Branch Name</div>
                      <div class="div-table-col2">
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                              <ContentTemplate>
                                 
                                  <asp:DropDownList ID="ddlSalesPoint" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="ddlSalesPoint_SelectedIndexChanged"></asp:DropDownList>
                              </ContentTemplate>
                          </asp:UpdatePanel>
                      </div>
                      <div class="div-table-col3">Received Date</div>
                      <div class="div-table-col4">  
                          <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                              <ContentTemplate>
                                   <asp:TextBox ID="txstart_dt" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txstart_dt_CalendarExtender" runat="server" TargetControlID="txstart_dt" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                                  </ContentTemplate>
                          </asp:UpdatePanel></div>
                      <div><asp:Button ID="btnShow" runat="server" Text="Show Error" CssClass="button2 Show" OnClick="btnShow_Click" /></div>
                  </div>
                 
              </div>

          </asp:Panel>

         
        <asp:Panel ID="Panel2" runat="server"
            Style="width: 100%" GroupingText="Error List">
            <div class="div-table">

                

                <div class="div-table-row">
                    <div class="div-table-colFull">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdErrorLog" runat="server"  Width="1150" AllowPaging="true" PageSize="10"
                                    AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged" OnPageIndexChanging="grd_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField HeaderText="Module Name" DataField="ModuleName" />
                                        <asp:BoundField HeaderText="Form Name" DataField="FormName" />
                                        <asp:BoundField HeaderText="Method Name" DataField="MethodName" />
                                        <asp:BoundField HeaderText="Error Type" DataField="ErrorType" />
                                        <asp:BoundField HeaderText="Error" DataField="Error" />
                                        <asp:BoundField HeaderText="Created Date" DataField="CreatedDate" />
                                        <asp:BoundField HeaderText="Created By" DataField="CreatedBy" />
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="First" LastPageText="Last" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </asp:Panel>

        
     </div>
</asp:Content>

