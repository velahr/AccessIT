<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalDocList.aspx.cs"
    Inherits="ClaimsDocsClient.secure.ApprovalDocList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                                    <td align="left" colspan="2">
                                        <table border="0" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>User : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Department : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
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
                                                                <asp:GridView ID="gvwData" GridLines="None" runat="server" AutoGenerateColumns="False"
                                                                    OnSelectedIndexChanged="gvwData_SelectedIndexChanged" PageSize="1000" OnPageIndexChanging="gvList_PageIndexChanging"
                                                                    PagerSettings-Position="TopAndBottom" PagerStyle-ForeColor="Black" 
                                                                    AllowSorting="True" onrowdatabound="gvwData_RowDataBound" 
                                                                    onsorting="gvwData_Sorting">
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="right" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="Medium" HeaderStyle-Width="100px" FooterStyle-Width="100px"
                                                                            ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top">
                                                                            <ItemTemplate>
                                                                                <input class="flat" type="button" value="Select..." onclick="window.location='ApprovalDocReview.aspx?state=2&docdefid=<%# DataBinder.Eval(Container.DataItem, "DocumentID")%>&approvalID=<%# DataBinder.Eval(Container.DataItem, "ApprovalQueueID")%>&InstanceID=<%# DataBinder.Eval(Container.DataItem, "InstanceID")%>&GroupName=<%# DataBinder.Eval(Container.DataItem, "GroupName")%>'">
                                                                            </ItemTemplate>
                                                                            <FooterStyle Width="100px"></FooterStyle>
                                                                            <HeaderStyle Font-Bold="True" Font-Size="Medium" Width="100px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False" Width="100px">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="150px" FooterStyle-Width="150px"
                                                                            ItemStyle-Width="150px" ItemStyle-VerticalAlign="Top" HeaderText="Doc ID" 
                                                                            SortExpression="DocumentCode">

                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "DocumentCode")%>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Width="150px"></FooterStyle>
                                                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" Width="150px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" Width="150px">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true"
                                                                            HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="300px" FooterStyle-Width="300px"
                                                                            ItemStyle-Width="300px" ItemStyle-VerticalAlign="Top" 
                                                                            HeaderText="Description" SortExpression="Description">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Width="300px"></FooterStyle>
                                                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" Width="300px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" Width="300px">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Submitted By" ItemStyle-HorizontalAlign="Center"
                                                                            HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small" HeaderStyle-Width="100px"
                                                                            FooterStyle-Width="100px" ItemStyle-Width="100px" 
                                                                            ItemStyle-VerticalAlign="Top" SortExpression="UserName">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Width="100px"></FooterStyle>
                                                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" Width="100px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" Width="100px">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Date &amp; Time Submitted"
                                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="X-Small"
                                                                            HeaderStyle-Width="100px" FooterStyle-Width="100px" 
                                                                            ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" 
                                                                            SortExpression="DateSubmitted">
                                                                            <ItemTemplate>
                                                                                <%# DataBinder.Eval(Container.DataItem, "DateSubmitted")%>
                                                                            </ItemTemplate>
                                                                            <FooterStyle Width="100px"></FooterStyle>
                                                                            <HeaderStyle Font-Bold="True" Font-Size="X-Small" Width="100px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="False" Width="100px">
                                                                            </ItemStyle>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerSettings Position="TopAndBottom"></PagerSettings>
                                                                    <RowStyle CssClass="repeating1" />
                                                                    <AlternatingRowStyle CssClass="repeating2" />
                                                                    <FooterStyle CssClass="GridListFooter" />
                                                                    <HeaderStyle CssClass="GridHeader" />
                                                                    <PagerStyle CssClass="GridPager" ForeColor="White" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <!--Start Data Grid -->
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
                                        <input class="button" id="Button1" type="button" value="Back" onclick="window.location.replace('AdminDocs.aspx');" />
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
