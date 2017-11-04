<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsBizLogicTestPage.aspx.cs" Inherits="ClaimsDocsClient.TestHarness.ClaimsDocsBizLogicTestPage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Test Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/accessgeneral.css">
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
                                            <img alt="Access Small Logo" src="../images/AccessNewLogo.jpg" />
                                        </td>
                                        <td align="center">
                                            <div class="siteName" align="center">
                                                <a class="siteName" href="#">Correspondence &amp; Document Composition</a>
                                            </div>
                                        </td>
                                        <td width="120" valign="top" align="right" style="font-size: 8pt">
                                            <div style="padding: 5px 5px 5px 0px">
                                                <a href="ClaimsDocsTestPage.aspx" >
                                                    Main Test Page&nbsp;&nbsp;
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="3" align="center">
                                            <table border="0"  class="content" width="100%"  cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" >
                                                        <!--Start Content Table-->
                                                        <table border="0" cellpadding="2" cellspacing=0 width="800px">
                                                            <tr>
                                                                <td colspan=2>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            
                                                            <tr>
                                                                <td colspan="1">
                                                                    <font face="arial" size="3" >
                                                                        &nbsp;ClaimsDocs Environment Test
                                                                    </font>
                                                                
                                                                </td>
                                                                
                                                                <td colspan="1" align=right>
                                                                    <asp:Button ID="cmdRunValidation" runat=server Text="Run Environment Test..." onclick="cmdRunValidation_Click" />
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td colspan=2>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td colspan=2>
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="1">
                                                                    <font class="Calibri">
                                                                        Policy Number : 
                                                                    </font>
                                                                </td>
                                                                
                                                                <td colspan="1" align="center">
                                                                    <asp:TextBox ID="txtPolicyNumber" runat=server  Text="ACA000502490"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td colspan="1">
                                                                    <font class="Calibri">
                                                                        Claim Key : 
                                                                    </font>
                                                                </td>
                                                                
                                                                <td colspan="1" align="center">
                                                                    <asp:TextBox ID="txtClaimNumber" runat=server  Text="241810"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="1">
                                                                    <font class="Calibri">
                                                                        Contact Number : 
                                                                    </font>
                                                                </td>
                                                                
                                                                <td colspan="1" align="center">
                                                                    <asp:TextBox ID="txtContactNumber" runat=server  Text="242504"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            
                                                             <tr>
                                                                <td colspan="1">
                                                                    <font class="Calibri">
                                                                        Vehicle Found : 
                                                                    </font>
                                                                </td>
                                                                
                                                                <td colspan="1" align="center">
                                                                    <asp:TextBox ID="txtVehicleFound" runat=server  Text="0"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                          
                                                          
                                                            
                                                             <tr>
                                                                <td colspan=2>
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                             <tr>
                                                                <td colspan=2 align="center">
                                                                    <div id="divResult" runat="server">
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                           
                                                            
                                                            <tr>
                                                                <td colspan=2>
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            
                                                        
                                                            <tr>
                                                                <td colspan=2>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <!--End Content Table-->
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
