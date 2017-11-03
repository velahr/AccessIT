<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CorrespondenceDocumentReview.aspx.cs" Inherits="ClaimsDocsClient.correspondence.CorrespondenceDocumentReview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
                                                    <font face="Arial" size="2">
                                                        <b>User :</b> 
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblUser" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Run Mode :</b> 
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblMode" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                       <b>Department :</b> 
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Claim # :</b> 
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblClaimNumber" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Program Code :</b> 
                                                    </font>
                                                </td>
                                                
                                                <td align="left">
                                                    <asp:Label ID="lblProgramCode" runat="server"></asp:Label>
                                                </td>
                                                
                                                <td align="left">
                                                    <font face="Arial" size="2">
                                                        <b>Addressee :</b> 
                                                    </font>
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
                                        <table class="style1">
                                            <tr>
                                                <td class="Heading" align="center" colspan="5">Distribution</td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="5">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">ImageRight:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblImageRight" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="Heading" align="left">Datamatx:</td>
                                                <td align="left"><asp:Label ID="lblDatamatx" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">Copy To Producer:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblProducer" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="Heading" align="left">Copy To Insured:</td>
                                                <td align="left"><asp:Label ID="lblInsured" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">Copy To Lien Holder:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblLienHolder" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="Heading" align="left">Copy To Finance Company:</td>
                                                <td align="left"><asp:Label ID="lblFinanceCo" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">Copy To Attorney:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblAttorney" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="Heading" align="left">Proof Of Mailing:</td>
                                                <td align="left"><asp:Label ID="lblMailing" runat="server" Text="Label"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">Attached Document:</td>
                                                <td align="left">
                                                    <asp:Label ID="lblAttachedDoc" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Table ID="tblFileds" runat="server" Width="100%" CellSpacing="3" CellPadding="1">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="Heading" HorizontalAlign="center" ColumnSpan="2">Fields</asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell HorizontalAlign="center" ColumnSpan="2">
                                                    <hr />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td align="center" colspan="2">
                                        <div id="divPreviewXML" runat=server>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="2">
                                        <div id="divPreviewPDF" runat=server>
                                        </div>
                                    </td>
                                </tr>
                                
                                 <tr>
                                    <td align="center" colspan="2">
                                        <div id="divImageRightPDF" runat=server>
                                        </div>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <div runat="server" id="divButtons" visible=true></div>
                                    </td>
                                    <td align="center">
                                        <asp:Button class="button" ID="cmdSubmit" runat="server" Text="Done" onclick="cmdSubmit_Click" />
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
    <asp:HiddenField ID="reviewFlag" runat="server" />
    <asp:HiddenField ID="DocumentDesc" runat="server" />
    <asp:HiddenField ID="DocumentCode" runat="server" />
    <asp:HiddenField ID="PreviewXML" runat="server" />
   </form>
</body>
</html>
