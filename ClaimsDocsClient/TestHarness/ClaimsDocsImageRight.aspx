<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsImageRight.aspx.cs" Inherits="ClaimsDocsClient.TestHarness.ClaimsDocsImageRight" %>

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
                                    <table border="0" class="content" width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="center">
                                                <!--Start Content Table-->
                                                <table border="0" cellpadding="2" cellspacing="0" width="800px">
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;ClaimsDocs : ImageRight Test </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <!-- Start ImageRight Table-->
                                                            <table border="1" cellpadding="0" cellspacing="0" width="800px">
                                                                <tr>
                                                                    <td align="left" colspan="3" style="background-color: black">
                                                                        <font face="Calibri" color="white" size="4">ImageRight : ClaimsDocs Sample </font>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: whitesmoke">
                                                                    <td colspan="3" align="left">
                                                                        <font face="Calibri" size="2">&nbsp;Information</font>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Font-Names="Calibri" Text="----"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <div id="divDocmentLink" runat="server">
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: whitesmoke">
                                                                    <td colspan="3" align="left">
                                                                        <font face="Calibri" size="2">&nbsp;Service Setup</font>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;ImageRight WS URL : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtImageRightWSUrl" runat="server" Width="500px" Text="http://aihirwsdev01.development/irwebservice/irwebservice.asmx"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtImageRightWSUrl" runat="server" ControlToValidate="txtImageRightWSUrl" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;ImageRight Login : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtImageRightLogin" runat="server" Width="500px" Text="ADMIN"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtImageRightLogin" runat="server" ControlToValidate="txtImageRightLogin" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;ImageRight Password : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtImageRightPassword" runat="server" Width="500px" Text="IRADMIN"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtImageRightPassword" runat="server" ControlToValidate="txtImageRightPassword" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: whitesmoke">
                                                                    <td colspan="3" align="left">
                                                                        <font face="Calibri" size="2">&nbsp;Document Storage Parameters</font>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Source Document Path:</font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtSourceDocumentPath" runat="server" Width="500px" TextMode="MultiLine" Rows="5" Text="\\DEV016DNET.accessgeneral.com\c$\Documents and Settings\kphifer\My Documents\LocalData\ProjectInfo\Claims\DataSamples\ClaimsDocsOuput\UWPP_ACA1036908_15300_DEC_20100429_1.pdf"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtSourceDocumentPath" runat="server" ControlToValidate="txtSourceDocumentPath" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Policy / File Number : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtFileNumber" runat="server" Width="500px" Text="ACA1223495"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtFileNumber" runat="server" ControlToValidate="txtFileNumber" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Claims Number : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtClaimNumber" runat="server" Width="500px" Text="ACI0000002"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtClaimNumber" runat="server" ControlToValidate="txtClaimNumber" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;ClaimsDocs Document ID : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtCDDocumentID" runat="server" Width="500px" Text="ACAAFTB01"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtCDDocumentID" runat="server" ControlToValidate="txtCDDocumentID" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px" class="style1">
                                                                        <font face="Calibri" size="2">&nbsp;Pack Type : </font>
                                                                    </td>
                                                                    <td align="left" class="style1">
                                                                        <asp:TextBox ID="txtPackType" runat="server" Width="500px" Text="10300"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px" class="style1">
                                                                        <asp:RequiredFieldValidator ID="valtxtPackType" runat="server" ControlToValidate="txtPackType" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Doc Type : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtDocType" runat="server" Width="500px" Text="CORB"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtDocType" runat="server" ControlToValidate="txtDocType" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;ImageRight Claims Drawer : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtImageRightClaimsDrawer" runat="server" Width="500px" Text="CLMS"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtImageRightClaimsDrawer" runat="server" ControlToValidate="txtImageRightClaimsDrawer" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Capture Date : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtCaptureDate" runat="server" Width="500px" Text="06/09/2009"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtCaptureDate" runat="server" ControlToValidate="txtCaptureDate" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Folder Name : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtFolderName" runat="server" Width="500px" Text="Smith, John"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtFolderName" runat="server" ControlToValidate="txtFolderName" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Date of Loss : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtDateOfLoss" runat="server" Width="500px" Text="06/09/2010"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtDateOfLoss" runat="server" ControlToValidate="txtDateOfLoss" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Reason : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtReason" runat="server" Width="500px" Text="ClaimsDocs Generated Document"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="val" runat="server" ControlToValidate="txtReason" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr style="background-color: whitesmoke">
                                                                    <td colspan="3" align="left">
                                                                        <font face="Calibri" size="2">&nbsp;Task Parameters</font>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <font face="Calibri" size="2">&nbsp;Create Task? : </font>
                                                                    </td>
                                                                    <td colspan="2" align="left">
                                                                        &nbsp;<asp:CheckBox ID="chkCreateTask" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Flow ID : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtFlowID" runat="server" Width="500px" Text="25"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtFlowID" runat="server" ControlToValidate="txtFlowID" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Step ID : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtStepID" runat="server" Width="500px" Text="1"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtStepID" runat="server" ControlToValidate="txtStepID" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Description : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtDescription" runat="server" Width="500px" Text="ClaimsDocs Generated Document"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" width="200px">
                                                                        <font face="Calibri" size="2">&nbsp;Priority : </font>
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtPriority" runat="server" Width="500px" Text="5"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100px">
                                                                        <asp:RequiredFieldValidator ID="valtxtPriority" runat="server" ControlToValidate="txtPriority" ErrorMessage="*" Display="Static">
                                                                        </asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" align="center">
                                                                        <asp:Button ID="cmdSaveDocument" runat="server" Text="Save Document..." OnClick="cmdSaveDocument_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="background-color: Black" colspan="3">
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <!-- End ImageRight Table-->
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
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
