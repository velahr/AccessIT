<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminNonDocs.aspx.cs" Inherits="ClaimsDocsClient.secure.AdminNonDocs" %>
<%@ Import namespace="ClaimsDocsClient" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs Administration Page</title>
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
                                    <asp:LinkButton ID="lnkLogOut" runat="server" Text="Logout" onclick="lnkLogOut_Click"></asp:LinkButton>
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
                                    You are visiting: <span class="nav">Administration</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body2" cellspacing="8">
                                <tr>
                                    <td width="200">
                                        <input class="homeButton" type="button" value="Groups" onclick="window.location.replace('GroupList.aspx')">
                                    </td>
                                    <td width="200">
                                        <input class="homeButton" type="button" value="Users" onclick="window.location.replace('UserList.aspx')">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input class="homeButton" type="button" value="Departments" onclick="window.location.replace('DepartmentList.aspx')">
                                    </td>
                                    <td>
                                        <input class="homeButton" type="button" value="Programs" onclick="window.location.replace('ProgramList.aspx')">
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
