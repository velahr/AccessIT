<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ClaimsDocsClient.secure.UserList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : User List</title>
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
    <div align="center">
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
                                    You are visiting: <a class="nav" href="#" onclick="window.location.replace('AdminNonDocs.aspx')">Administration</a> &rarr; <span class="nav">User</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td valign="top">
                                        Select one user below to Edit or:&nbsp;
                                    </td>
                                    <td align="left">
                                        <input class="flat" type="button" value="Add a new User" onclick="window.location.replace('UserAdmin.aspx?state=1&userid=0')">
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
                                        <table border="1" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblSortOrder" runat="server" Visible="true" Text="UserName Asc"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <table border="0" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td colspan="8">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <!--Start Data Grid -->
                                                                <asp:GridView 
                                                                    ID="gvwData" 
                                                                    GridLines="None"
                                                                    runat="server" 
                                                                    AllowPaging="false" 
                                                                    AllowSorting="false" 
                                                                    AutoGenerateColumns="false" 
                                                                    OnSelectedIndexChanged="gvwData_SelectedIndexChanged" 
                                                                    PageSize="1000" 
                                                                    OnPageIndexChanging="gvList_PageIndexChanging" 
                                                                    PagerSettings-Position="TopAndBottom" 
                                                                    PagerStyle-ForeColor="Black">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="150px" FooterStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByUserName" runat="server" Text="Name" onclick="lnkSortByUserName_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByDepartment" runat="server" Text="Department" onclick="lnkSortByDepartment_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "DepartmentName")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByApprover" runat="server" Text="Department" onclick="lnklnkSortByApprover_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Approver")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByDesigner" runat="server" Text="Department" onclick="lnkSortByDesigner_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Designer")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                         <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByReviewer" runat="server" Text="Department" onclick="lnkSortByReviewer_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Reviewer")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderText="Group(s)" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByActive" runat="server" Text="Active" onclick="lnkSortByActive_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Active")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"    ItemStyle-HorizontalAlign="right"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <input class="flat" type="button" value="Edit" onclick="window.location.replace('UserAdmin.aspx?state=2&userid=<%# DataBinder.Eval(Container.DataItem, "UserID")%>')">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="repeating1" />
                                                                    <AlternatingRowStyle CssClass="repeating2" />
                                                                    <FooterStyle CssClass="GridListFooter" />
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                    <PagerStyle CssClass="GridPager" ForeColor="White" />
                                                                </asp:GridView>
                                                                <!--End Data Grid -->
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <input style="width: 8em; text-align: center" class="button" type="button" value="Back" onclick="window.location.replace('AdminNonDocs.aspx')" />
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
