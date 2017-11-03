<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmation.aspx.cs" Inherits="ClaimsDocsClient.secure.Confirmation" %>

<%@ Import namespace="ClaimsDocsClient" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Confirmation Page</title>
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
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="main" align="center">
                    <div class="nav" align="center">
                        <table>
                            <tr>
                                <td align="center">
                                    
                                 <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table cellspacing="8">
                                <tr>
                                    <td width="400" align=center>
                                        <h4>
                                            What do you want to do next?
                                        </h3>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td width="400" align="center">
                                        <div id="divHTML" runat="server">
                                        </div>
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
