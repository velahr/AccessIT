<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrespondenceDocumentEntry.aspx.cs" Inherits="ClaimsDocsClient.correspondence.CorrespondenceDocumentEntry" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ClaimsDocs : Enter Document Custom Fields</title>
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
    </style>
</head>
 <body>
 
    <form id="frmCorrespondenceDocumentEntry" runat="server" method="post" >
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
                <td align=center>
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
                                                    <font face="Arial" size="2">
                                                        <b>User : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Run Mode : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Department : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Claim # : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Program Code : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblProgramCode" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Addressee : </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblAddressee" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan=4>
                                                    <hr />
                                                </td>
                                            </tr>
                                            
                                        </table>
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
                                        <asp:Table class="style1" align="left" cellpadding="1" cellspacing="2" ID="Table1" runat="server">
                                        </asp:Table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="1">
                                        <div id="divButtons" runat="server"></div>
                                    </td>
                                
                                    <td align="center" colspan="1">
                                        <asp:Button class="button" ID="Button1" runat="server" Text="Next" onclick="Button1_Click" />
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
    <asp:HiddenField ID="docdefid" runat="server" />
    <asp:HiddenField ID="state" runat="server" />
   </form>
</body>
</html>
