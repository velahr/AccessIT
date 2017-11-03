﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrespondenceDocumentList.aspx.cs"
    Inherits="ClaimsDocsClient.correspondence.CorrespondenceDocumentList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Select Document Definition</title>
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
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Size="Smaller"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="main" align="center">
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td align="center" colspan="2">
                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>User : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Mode : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Department : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Claim # : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Program Code : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblProgramCode" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Addressee : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAddressee" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <font face="arial" size="4">
                                                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                                    </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
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
                                                                <asp:GridView ID="gvwData" GridLines="None" runat="server" AllowPaging="false" AllowSorting="false"
                                                                    AutoGenerateColumns="false" OnSelectedIndexChanged="gvwData_SelectedIndexChanged"
                                                                    PageSize="1000" OnPageIndexChanging="gvList_PageIndexChanging" PagerSettings-Position="TopAndBottom"
                                                                    PagerStyle-ForeColor="Black">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="Medium" HeaderStyle-Width="100px" FooterStyle-Width="100px"
                                                                            ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <input class="flat" type="button" value="Select..." onclick="window.location='CorrespondenceDocumentEntry.aspx?state=2&docdefid=<%# DataBinder.Eval(Container.DataItem, "DocumentID")%>'">
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="150px" FooterStyle-Width="150px"
                                                                            ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByDocumentCode" runat="server" Text="Doc ID" OnClick="lnkSortByDocumentCode_Click"></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "DocumentCode")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="300px" FooterStyle-Width="300px"
                                                                            ItemStyle-Width="300px" ItemStyle-VerticalAlign="Top">
                                                                            <HeaderTemplate>
                                                                                <asp:LinkButton ID="lnkSortByDescription" runat="server" Text="Description" OnClick="lnkSortByDescription_Click"></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Addl. Data Req." ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px"
                                                                            FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "AdditionalDataRequired")%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="POM" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px"
                                                                            FooterStyle-Width="100px" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "ProofOfMailing")%>
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
                                    <td align="center" colspan="4">
                                        <div id="divButtons" runat="server">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div class="copyright">
                        &copy; <a href="http://www.accessgeneral.com/" target="_new">Access Insurance Holdings,
                            Inc.</a> 2001 - 2006, All rights reserved.
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
