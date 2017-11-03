<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminDocs.aspx.cs" Inherits="ClaimsDocsClient.secure.AdminDocs" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs Administration Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/accessgeneral.css">
</head>
<body>
    <form id="frmStandardPage" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
            <div align="center">
                <table class="content" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <div class="siteHeader">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td width="120">
                                            <img alt="Access Small Logo" src="../images/AccessNewLogo.JPG" />
                                        </td>
                                        <td align="center">
                                            <div class="siteName" align="center">
                                                <a class="siteName" href="#">Correspondence &amp; Document Composition</a>
                                            </div>
                                        </td>
                                        <td width="120" valign="top" align="right" style="font-size: 8pt">
                                            <asp:LinkButton ID="lnkLogOut" runat="server" Text="Logout" onclick="lnkLogOut_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center" >
                                            <table border="0" width="100%" style="background-color: silver" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center">
                                                        <table border="0" class="content"  cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <div style="width: 75%">
                                                                        <fieldset>
                                                                            <legend class="title">Approval Services</legend>
                                                                            <div style="padding: 1em">
                                                                                <div align="left">
                                                                                    <p>
                                                                                        The Approval Services are used to approve or reject previously created correspondence.
                                                                                    </p>
                                                                                </div>
                                                                                <div align="right">
                                                                                    <input style="width:8em;text-align: center" class="button" type="button" value="Approvals" onclick="window.location.replace('ApprovalDocList.aspx')" />
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <div style="width: 75%">
                                                                        <fieldset>
                                                                            <legend class="title">Document Definition Service</legend>
                                                                            <div style="padding: 1em">
                                                                                <div align="left">
                                                                                    <p>
                                                                                        The Document Definition Service is used to create or modify Document Definitions used in creating correspondence.
                                                                                    </p>
                                                                                </div>
                                                                                <div align="right">
                                                                                    <input style="width:8em;text-align: center" class="button" type="button" value="Documents" onclick="window.location.replace('DocDefDocList.aspx')" />
                                                                                </div>
                                                                            </div>
                                                                        </fieldset>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <div class="copyright">
                                                                        &copy; <a href="http://www.accessgeneral.com/" target="_new">Access Insurance Holdings, Inc.</a> 2001 - 2006, All rights reserved.
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
