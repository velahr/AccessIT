<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsLogViewer.aspx.cs" Inherits="ClaimsDocsClient.ClaimsDocsLogViewer" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Log Viewer</title>
    <link rel="stylesheet" type="text/css" href="styles/accessgeneral.css">
    <style type="text/css">
        input.homeButton
        {
            padding-left: 2px;
            padding-right: 2px;
            border-style: solid;
            border-width: 1px;
            border-color: #989898;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="frmAdminNonDocs" runat="server">
    <div align="center" >
        <table class="content" border="1" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td>
                    <div class="siteHeader">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td width="120">
                                    <img src="images/AccessNewLogo.jpg" />
                                </td>
                                <td align="center">
                                    <div class="siteName" align="center">
                                        <a class="siteName" href="/Correspondence/">Correspondence &amp; Document Composition</a>
                                    </div>
                                </td>
                                <td width="120" valign="top" align="right" style="font-size: 8pt">
                                    <div style="padding: 5px 5px 5px 0px">
                                        <a href="ClaimsDocsLogin.aspx">Login</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="main" align="center">
                    <div class="nav" align="left">
                        <table>
                            <tr>
                                <td>
                                    You are visiting: <a class="nav" href="#" onclick="window.location.replace('ClaimsDocsLogin.aspx')">Login</a> &rarr; <span class="nav">Log Viewer</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                
                                
                                
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border=1 cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td colspan=2>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView 
                                                        ID="gvwData" 
                                                        runat="server" 
                                                        AllowPaging="true" 
                                                        AllowSorting="false" 
                                                        AutoGenerateColumns="false" 
                                                        onselectedindexchanged="gvwData_SelectedIndexChanged"
                                                        PageSize="20" 
                                                        OnPageIndexChanging="gvList_PageIndexChanging"
                                                        PagerSettings-Position="TopAndBottom"
                                                        PagerStyle-ForeColor="Black"
                                                        
                                                        >
                                                        <Columns>
                                                            <asp:TemplateField 
                                                                ItemStyle-Wrap="false" 
                                                                headertext="Log Details" 
                                                                HeaderStyle-Font-Bold="true" 
                                                                HeaderStyle-Font-Size="Medium" 
                                                                HeaderStyle-Width="100px" 
                                                                FooterStyle-Width="100px" 
                                                                ItemStyle-Width="100px" 
                                                                ItemStyle-VerticalAlign="Top"   
                                                                >
                                                                <ItemTemplate>
		                                                            <%# 
		                                                            
		                                                                "<br>Log Date/Time&nbsp;&nbsp;: " + DataBinder.Eval(Container.DataItem, "IUDateTime") + "<br>" +
                                                                        "Source&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;" + DataBinder.Eval(Container.DataItem, "LogSourceName") + "<br>" +
                                                                        "Type&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: " + DataBinder.Eval(Container.DataItem, "LogTypeName") + "<br><br>" +
                                                                        "Message<br>" +
                                                                        "-----------------------------------------------<br>" +
                                                                        DataBinder.Eval(Container.DataItem, "MessageIs") + "<br><br>" +
                                                                        "Exception<br>" +
                                                                        "-----------------------------------------------<br>" +
                                                                        DataBinder.Eval(Container.DataItem, "ExceptionIs") + "<br><br>" +
                                                                        "Stack Trace<br>" +
                                                                        "-----------------------------------------------<br>" + 
                                                                        DataBinder.Eval(Container.DataItem, "StackTraceIs") + "<br><br>"
		                                                                                                                                    
		                                                           %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                        </Columns>
                                                        <RowStyle  CssClass="repeating1" />
                                                        <AlternatingRowStyle CssClass="repeating2" />
                                                         <FooterStyle CssClass="GridListFooter" />
                                                         <HeaderStyle CssClass="GridHeader" />
                                                         <PagerStyle CssClass="GridPager" ForeColor="White" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="2">
                                    </td>
                                </tr>
                                
                            </table>
                        </fieldset>
                    </div>
                    <div class="copyright">
                        &copy; <a href="http://www.accessgeneral.com/" target="_new">Access Insurance Holdings, Inc.</a> 2001 - 2006, All rights reserved.
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
