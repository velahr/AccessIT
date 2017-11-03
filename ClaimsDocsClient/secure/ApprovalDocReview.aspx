<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApprovalDocReview.aspx.cs"
    Inherits="ClaimsDocsClient.secure.ApprovalDocReview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Document Approval Review</title>
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
    <form id="frmApprovalDoc" runat="server" method="post">
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
                     </div>
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
                                                    <asp:Label ID="lblHdrUser" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <font face="Arial" size="2"><b>Department : </b></font>
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblHdrDept" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td align=left>
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
                                        <table class="style1">
                                            <tr>
                                                <td class="Heading" align="center" colspan="5">
                                                    Distribution
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="5">
                                                    <hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">
                                                    ImageRight:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblImageRight" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="Heading" align="left">
                                                    Datamatx:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblDatamatx" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">
                                                    Copy To Producer:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblProducer" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="Heading" align="left">
                                                    Copy To Insured:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblInsured" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">
                                                    Copy To Lien Holder:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblLienHolder" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="Heading" align="left">
                                                    Copy To Finance Company:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblFinanceCo" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">
                                                    Copy To Attorney:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAttorney" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td class="Heading" align="left">
                                                    Proof Of Mailing:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblMailing" runat="server" Text="Label"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="Heading" align="left">
                                                    Attached Document:
                                                </td>
                                                <td align="left">
                                                    <asp:Label ID="lblAttachedDoc" runat="server" Text="Label"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    &nbsp;
                                                </td>
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
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    <input class="button" id="cmdBack" type="button" value="Back" onclick="window.location.replace('ApprovalDocList.aspx')" />
                                                </td>
                                                <td>
                                                    <asp:Button class="button" ID="cmdApprove" runat="server" Text="Approve" onclick="cmdApprove_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button class="button" ID="cmdDecline" runat="server" Text="Decline" 
                                                        onclick="cmdDecline_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button class="button" ID="cmdCancel" runat="server" Text="Cancel" />
                                                </td>
                                            </tr>
                                        </table>
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
    <asp:HiddenField ID="hdnApprovalID" runat="server" />
    <asp:HiddenField ID="hdnDocdefid" runat="server" />
    <asp:HiddenField ID="hdnState" runat="server" />
    <asp:HiddenField ID="hdnDocumentDesc" runat="server" />
    <asp:HiddenField ID="hdnDocumentCode" runat="server" />
    <asp:HiddenField ID="hdnPreviewXML" runat="server" />
    </form>
</body>
</html>
