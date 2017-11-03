<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocDefDocList.aspx.cs" Inherits="ClaimsDocsClient.secure.DocDefDocList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Document Definition List</title>
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
        <table class="content" border="0" cellspacing="0" cellpadding="0" width="100%">
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
                    
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td align="center" colspan="2">
                                        <input class="flat" type="button" value="Add Document Definition" onclick="window.location.replace('DocDefDocDefAdmin.aspx?state=1&docdefid=0')">
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
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblSortOrder" runat="server" Visible="false" Text="DocumentCode Asc"></asp:Label>
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
                                                                                <asp:LinkButton ID="lnkSortByDocumentCode" runat="server" Text="Doc ID" onclick="lnkSortByDocumentCode_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "DocumentCode")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="300px" FooterStyle-Width="300px" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByDescription" runat="server" Text="Description" onclick="lnkSortByDescription_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByProgramCode" runat="server" Text="Program" onclick="lnkSortByProgramCode_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "ProgramCode")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByLastModified" runat="server" Text="Last Modified" onclick="lnkSortByLastModified_Click"></asp:LinkButton> 
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "LastModified")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField ItemStyle-Wrap="false"    ItemStyle-HorizontalAlign="right"  HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="Medium" HeaderStyle-Width="100px" FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <input class="flat" type="button" value="Edit" onclick="window.location.replace('DocDefDocDefAdmin.aspx?state=2&docdefid=<%# DataBinder.Eval(Container.DataItem, "DocumentID")%>')">
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
                                        <input style="width: 8em; text-align: center" class="button" type="button" value="Back" onclick="window.location.replace('AdminDocs.aspx')" />
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
