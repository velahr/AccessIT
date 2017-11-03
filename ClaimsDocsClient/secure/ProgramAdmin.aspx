<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgramAdmin.aspx.cs" Inherits="ClaimsDocsClient.secure.ProgramAdmin" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Program Administration</title>
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
                                        <a href="../Administration.aspx">Administration</a>
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
                                    You are visiting: <a class="nav" href="#" onclick="window.location.replace('AdminNonDocs.aspx')">Administration</a> &rarr; <a class="nav" href="#" onclick="window.location.replace('ProgramList.aspx')">Programs</a> &rarr; <span class="nav">Add Program</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td valign="top" colspan="2" align="center">
                                        <h1>
                                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                        </h1>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="400px">
                                            <tr>
                                                <td colspan="1" align="right">
                                                    <asp:Label ID="lblState" runat="server" Visible="false" Text="-1"></asp:Label>
                                                    <asp:Label ID="lblProgramID" runat="server" Visible="false" Text="0"></asp:Label>
                                                    Program Code : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtProgramCode" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtProgramCode" runat="server" ControlToValidate="txtProgramCode" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="1">
                                        <asp:Button ID="cmdDo" style="width:8em;text-align: center" class="button" runat="server" Text="Ok" onclick="cmdDo_Click"   /> 
                                    </td>
                                    <td align="center" colspan="1">
                                        <input style="width:8em;text-align: center" class="button" type="button" value="Cancel" onclick="window.location.replace('ProgramList.aspx')" />
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
