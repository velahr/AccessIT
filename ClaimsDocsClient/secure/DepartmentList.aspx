<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Inherits="ClaimsDocsClient.secure.DepartmentList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Department List</title>
    <link rel="stylesheet" type="text/css" href="../styles/accessgeneral.css">
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
                                    <img src="../images/AccessNewLogo.jpg" />
                                </td>
                                <td align="center">
                                    <div class="siteName" align="center">
                                        <a class="siteName" href="/Correspondence/">Correspondence &amp; Document Composition</a>
                                    </div>
                                </td>
                                <td width="120" valign="top" align="right" style="font-size: 8pt">
                                    <div style="padding: 5px 5px 5px 0px">
                                        <a href="Administration.aspx">Administration</a>
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
                                    You are visiting: <a class="nav" href="#" onclick="window.location.replace('AdminNonDocs.aspx')">Administration</a> &rarr; <span class="nav">Departments</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td valign="top">
                                        Select one department below to Edit or:&nbsp;
                                    </td>
                                  <td>
                                     <input class="flat" type="button" value="Add a new Department" onclick="window.location.replace('DepartmentAdmin.aspx?state=1&departmentid=0')">
                                  </td>

                                </tr>
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
                                                    <asp:LinkButton ID="lnkSortByDepartmentName" runat="server" Text="Department Name" onclick="lnkSortByDepartmentName_Click"></asp:LinkButton> <asp:Label ID="lblSortOrder" Text="ASC" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan=2 align="center">
                                                    <asp:DataGrid
                                                        ID="grdData"
                                                        runat="server"
                                                        AutoGenerateColumns="False" 
                                                        onpageindexchanged="Grid_Change" 
                                                        pagesize="20" 
                                                        Width="250px"
                                                        Font-Size="8pt" 
                                                        ShowHeader="false"
                                                        BorderWidth="0px"
                                                    >
                                                    <Columns>
                                                             <asp:templatecolumn headertext="Department Name" >
                                                                <ItemTemplate>
		                                                            <%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>
                                                                </ItemTemplate>
                                                                 <HeaderStyle Width="100px" />
                                                                 <ItemStyle HorizontalAlign="Left" />
                                                            </asp:templatecolumn>
                                                            
                                                            <asp:templatecolumn headertext="" >
                                                                <ItemTemplate>
		                                                            <input class="flat" type="button" value="Edit" onclick="window.location.replace('DepartmentAdmin.aspx?state=2&departmentid=<%# DataBinder.Eval(Container.DataItem, "DepartmentID")%>')">
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="50px" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:templatecolumn>
                                                        </Columns>						
                                                         <AlternatingItemStyle  CssClass="repeating2" />
                                                         <FooterStyle CssClass="GridListFooter" />
                                                         <HeaderStyle CssClass="GridHeader" />
                                                         <ItemStyle CssClass="repeating1" />
                                                         <PagerStyle CssClass="GridPager" ForeColor="White" />
                                                    
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="2">
                                        <input style="width:8em;text-align: center" class="button" type="button" value="Back" onclick="window.location.replace('AdminNonDocs.aspx')" />
                                    
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
