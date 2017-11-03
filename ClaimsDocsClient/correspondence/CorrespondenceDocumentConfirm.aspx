<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrespondenceDocumentConfirm.aspx.cs"
    Inherits="ClaimsDocsClient.correspondence.CorrespondenceDocumentConfirm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Document Confirm</title>
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
        .style1
        {
            width: 100%;
        }
        .Heading
        {
            font-weight: bold;
        }
        .FakeLabel
        {
            background-color: #dddddd;
        }
    </style>
</head>
<body>
    <form id="frmCorrespondenceDocumentEntry" runat="server" method="post">
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
                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>User :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Run Mode :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Department :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Claim # :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Program Code :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblProgramCode" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Addressee :</b> </font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAddressee" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="Heading" align="left" colspan="2">
                                        <asp:Label ID="lblDocumentCode" runat="server" Text="Label"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblDocumentDesc" runat="server" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblApproveMsg" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div runat="server" visible=false id="divButtons">
                                        </div>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button class="button" Visible=false ID="cmdSubmit" runat="server" Text="Done" />
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
    <asp:HiddenField ID="docdefid" runat="server" />
    <asp:HiddenField ID="state" runat="server" />
    <asp:HiddenField ID="reviewFlag" runat="server" />
    </form>
</body>
</html>
