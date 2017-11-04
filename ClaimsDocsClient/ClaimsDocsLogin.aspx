<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsLogin.aspx.cs" Inherits="ClaimsDocsClient.ClaimsDocsLogin" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ClaimsDocs Login Page</title>
    <link rel="stylesheet" type="text/css" href="styles/accessgeneral.css">
</head>
<body>
    <form id="frmClaimsDocsLoginPage" runat="server">
        <div align="center">
                <table class="content" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <div class="siteHeader">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td width="120">
                                            <img alt="Access Small Logo" src="images/AccessNewLogo.jpg" />
                                        </td>
                                        <td align="center">
                                            <div class="siteName" align="center">
                                                <a class="siteName" href="#">Correspondence &amp; Document Composition</a>
                                            </div>
                                        </td>
                                        <td width="120" valign="top" align="right" style="font-size: 8pt">
                                            <div style="padding: 5px 5px 5px 0px">
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="3" align="center">
                                            <table border="0"  class="content" width="100%"  cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" >
                                                        <table border="0" cellpadding="0" cellspacing="0"> 
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                 <td>
                                                                    Username : 
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="valtxtUserName" ControlToValidate="txtUserName" runat="server" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                 <td>
                                                                    Password : 
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                                                </td>
                                                                
                                                                <td>
                                                                    <asp:RequiredFieldValidator ID="valtxtPassword" ControlToValidate="txtPassword" runat="server" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                 <td colspan="3" align=center>
                                                                    <asp:Button ID="cmdLogin" style="width:8em;text-align: center" class="button" runat="server" Text="Login"  onclick="cmdLogin_Click" /> 
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <div class="copyright">
			                                                        &copy; <a href="http://www.accessgeneral.com/" target="_new">Access Insurance Holdings, Inc.</a>
			                                                        2001 - 2006, All rights reserved.<a href="ClaimsDocsLogViewer.aspx" target="_new">View Log</a>
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
    </form>
</body>
</html>
