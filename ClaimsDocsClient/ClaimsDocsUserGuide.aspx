<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsDocsUserGuide.aspx.cs" Inherits="ClaimsDocsClient.ClaimsDocsUserGuide" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs User Guide</title>
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
                                                        <table border="0" width="100%" cellpadding="4" cellspacing="4"> 
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <h3>ClaimsDocs Setup / Quick Start User Guide</h1>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <h5>
                                                                        How to Setup C4 to ClaimsDocs Integration
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <font face="Calibri" size="3">
                                                                        <ol>
                                                                            <li>
                                                                                Install the C4 Application onto the desktop where ClaimsDocs access is required.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                If open, close the C4 Application.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Locate and open the C4 initialization file. The name of the file is <b>claims.ini</b>.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Locate the <b>[WhiteHill]</b> section of the initialization file.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Locate the <b>WhitehillBaseURL: </b> setting under the <b>[WhiteHill]</b> section of the initialization file
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Replace the entire <b>WhitehillBaseURL: </b> line with <b>WhitehillBaseURL: http://gavwssq01.alpha.accessgeneral.com/ClaimsDocsClient/correspondence/CorrespondenceUserGroup.aspx?PolicyNo={policyno}&ClaimNo={ClaimNo}&ContactNo={ContactUkey}&ContactType={ContactType}&UserID={userid}</b>
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Save the claims.ini file.
                                                                            </li>
                                                                            
                                                                        </ol>
                                                                    </font>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <h5>
                                                                        How to Access ClaimsDocs from C4
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <font face="Calibri" size="3">
                                                                        <ol>
                                                                            <li>
                                                                                Open the C4 Application
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Click the <b>Search for a Claim</b> option in the upper right-hand corner of the C4 screen.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Enter a valid Claim# in the <b>Claim#</b> input field
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Press the Enter key on the keyboard. 
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Wait for C4 to gather and present a list of claims
                                                                                that meet the criteria you entered in the step above (See image below).
                                                                                <br />
                                                                                <br />
                                                                                <img src="images/C4ClaimsSearch.JPG" title="C4 Claims Search Screen" />
                                                                                <br />
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Select a claim from the list of claims.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Click the <b>Contacts</b> button located in the lower left-hand corner of the Claims Screen.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Wait for the Contacts list window to appear.(See image below).
                                                                                <br />
                                                                                <br />
                                                                                <img src="images/C4ContactSearch.JPG" title="C4 Claims Search Screen" />
                                                                                <br />
                                                                                <br />
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Select a Contact from the Contact list.
                                                                            </li>
                                                                            
                                                                            <li>
                                                                                Click the <b>Submit To WhiteHill</b> button in the center of the Contact list screen.
                                                                            </li>
                                                                            <li>
                                                                                Wait for the C4 application to:
                                                                                <ol>
                                                                                    <li>
                                                                                        Open a browser window
                                                                                    </li>
                                                                                    
                                                                                    <li>
                                                                                        Present a list of User Groups.
                                                                                    </li>
                                                                                    
                                                                                </ol>
                                                                            </li>
                                                                            
                                                                             <li>
                                                                                If you see a list similar to the list below, then you have successfully navigated
                                                                                to the ClaimsDocs Web application from the C4 Desktop application.
                                                                                <br />
                                                                                <br />
                                                                                <img src="images/CDCorrespondenceUserGroup.JPG" title="C4 Claims Search Screen" />
                                                                                <br />
                                                                                <br />
                                                                            </li>
                                                                         </ol>
                                                                   </font>
                                                              </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <h5>
                                                                        ClaimsDocs Functional Process Diagram
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                    <img src="images/ClaimsDocsFunctionalProcess.jpg" title="C4 Claims Search Screen" />
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <h5>
                                                                        ClaimsDocs Server Infrastructure Diagram
                                                                    </h5>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                    <img src="images/ClaimsServerLayout.jpg" title="C4 Claims Search Screen" />
                                                                    <br />
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
