<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsTestPage.aspx.cs" Inherits="ClaimsDocsClient.TestHarness.ClaimsDocsTestPage" %>

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
                                                            <font face="arial" size="3">&nbsp;WhiteHill Links </font>
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <a href="http://testweb05a/ClaimsDocsClient/correspondence/CorrespondenceUserGroup.aspx?PolicyNo=ACA000502490&ClaimNo=ACI0000060&ContactNo=242504&ContactType=7&UserID=kphifer">Click to Test UAT Deployment</a>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Whitehill Administrator : </font>
                                                        </td>
                                                        <td align="center">
                                                            <a href="http://aihitqawh01.alpha.accessgeneral.com/Correspondence/">Administrator...</a>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;2.)&nbsp;&nbsp;&nbsp;Whitehill Adjuster : </font>
                                                        </td>
                                                        <td align="center">
                                                            <a href="http://aihitqaautosrv.alpha/whitehill/Default.aspx?PolicyNo=AAL000001144&ClaimNo=AAI0000002&ContactNo=278450&ContactType=6&UserID=kphifer&GroupID=2&GroupName=BI">Adjuster...</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;Environment Validation </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;ClaimsDocs Environment Tests : </font>
                                                        </td>
                                                        <td align="center">
                                                            <a href="ClaimsDocsBizLogicTestPage.aspx">Environment Tests...</a>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;E-Mail Validation </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Enter From E-Mail Address : </font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFromEMailAddress" runat=server Width="250px" Text="ClaimsDocs@AccessGeneral.com"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;2.)&nbsp;&nbsp;&nbsp;Enter To E-Mail Address : </font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtToEMailAddress" runat=server Width="250px" Text="kphifer@AccessGeneral.com"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;3.)&nbsp;&nbsp;&nbsp;Enter Subject : </font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSubject" runat=server Width="250px" Text="ClaimsDocs to Adjuster E-Mail"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;4.)&nbsp;&nbsp;&nbsp;Enter Message : </font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtMessage" runat=server Width="250px" Text="The is a message to the adjuster..."></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;5.)&nbsp;&nbsp;&nbsp;Send E-Mail Message : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button id="cmdSendEMail" runat=server Text="Send E-Mail..." onclick="cmdSendEMail_Click"  Width="150px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;6.)&nbsp;&nbsp;&nbsp;Review Results : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label id="lblEMailResults" runat=server Font-Names="Calibri" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <div id="divMessage" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="3">&nbsp;ClaimsDocs XML File Deserialization Test </font>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFileToDeserialize"  runat="server" Text="\\GAVWSSQ01\c$\inetpub\wwwroot\ClaimsDocsBizLogic\XMLOutputLocation\09758985-9d10-46d0-9a8f-4768e0c5bacf.xml" Width="300px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="3">&nbsp;</font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button ID="cmdDeserializeXML" runat="server" Text="Deserialize XML" Width="150px" onclick="cmdDeserializeXML_Click" />
                                                        </td>
                                                    </tr>
                                                    
                                                     <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;Deserialization Results : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Label id="lblXMLFileDeserializationResults" runat=server Font-Names="Calibri" ForeColor="Red"></asp:Label>
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
                                                        <td>
                                                            <font face="arial" size="3">&nbsp;ImageRight Test </font>
                                                        </td>
                                                        <td align="center">
                                                            <a href="ClaimsDocsImageRight.aspx">ImageRight Test...</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;Administrator to Login </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Administrator Login Page :
                                                                <br />
                                                                <br />
                                                                &nbsp;&nbsp;&nbsp;<b>Note:</b>
                                                                <br />
                                                                <ul>
                                                                    <li>Login with user/password (ClaimsDocs/claimsdocs) for approvals and document definition administration. </li>
                                                                    <li>Login with user/password (admin/admin) for Group, User, Department and Programs administration. </li>
                                                                </ul>
                                                            </font>
                                                        </td>
                                                        <td align="center" valign="top">
                                                            <a href="../ClaimsDocsLogin.aspx">Administrator Login Page</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;Simulate C4 to ClaimsDocs Request </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Click this link to test Valid C4 Address: </font>
                                                        </td>
                                                        <td align="center">
                                                            <ul>
                                                                <li>
                                                                    <a href="../correspondence/CorrespondenceUserGroup.aspx?PolicyNo=ACA000500318&ClaimNo=ACI0000003&ContactNo=241810&ContactType=2&UserID=kphifer">ACA : C4 to ClaimsDocs Request [Valid]</a>
                                                                </li>
                                                                
                                                                <li>
                                                                    <a href="../correspondence/CorrespondenceUserGroup.aspx?PolicyNo=AAL000001144&ClaimNo=AAI0000002&ContactNo=278450&ContactType=6&UserID=kphifer">AAL : C4 to ClaimsDocs Request [Insured]</a>
                                                                </li>
                                                                
                                                                <li>
                                                                    <a href="../correspondence/CorrespondenceUserGroup.aspx?PolicyNo=AAL000001144&ClaimNo=AAI0000002&ContactNo=278453&ContactType=1&UserID=kphifer">AAL : C4 to ClaimsDocs Request [Non-Insured]</a>
                                                                </li>
                                                            </ul>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Click this link to test invalid C4 Address: </font>
                                                        </td>
                                                        <td align="center">
                                                            <a href="../correspondence/CorrespondenceUserGroup.aspx?PolicyNo=ACA000500682&ClaimNo=ACI0000002&ContactNo=241806&ContactType=2&UserID=ClaimsDocs">C4 to ClaimsDocs Request [Invalid Address]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <font face="arial" size="3">&nbsp;Test Claims Document XML Build </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;1.)&nbsp;&nbsp;&nbsp;Enter Submssion Parameters : </font>
                                                        </td>
                                                        <td align="center">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Instance ID : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtInstanceID" runat="server" Width="200px" Text="ABC-123-ZYX-KEN-REFIHP-001"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Policy Number : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtPolicyNumber" runat="server" Width="200px" Text="ACA000746212"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Claim Number : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtClaimNumber" runat="server" Width="200px" Text="ACI0091024"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Contact Number : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtContactNumber" runat="server" Width="200px" Text="380615"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Contact Type : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtContactType" runat="server" Width="200px" Text="6 "></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User Name : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtUserName" runat="server" Width="200px" Text="kphifer"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;2.)&nbsp;&nbsp;&nbsp;Select a User Group : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="cboUserGroup" runat="server" DataValueField="GroupName" DataTextField="GroupName" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;2.)&nbsp;&nbsp;&nbsp;Select a document : </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:DropDownList ID="cboDocumentList" runat="server" DataValueField="DocumentID" DataTextField="DocumentCode" Width="200px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <font face="arial" size="2">&nbsp;&nbsp;&nbsp;3.)&nbsp;&nbsp;&nbsp;Generate XML </font>
                                                        </td>
                                                        <td align="center">
                                                            <asp:Button ID="cmdTestClaimsDocumentXMLBuild" runat="server" Width="200px" Text="Generate XML" OnClick="cmdTestClaimsDocumentXMLBuild_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <div id="divPreviewXML" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <div id="divPreviewPDF" runat="server">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td align="center" colspan="2">
                                                            <div id="divImageRightPDF" runat="server">
                                                            </div>
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
