<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_basicpage.aspx.cs" Inherits="ClaimsDocsClient.template.template_basicpage" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="template_standardpage.aspx.cs" Inherits="ClaimsDocsClient.template.template_standardpage" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs Administration Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/accessgeneral.css">
</head>
<body>
    <form id="frmStandardPage" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upPanel" runat="server">
        <ContentTemplate>
        
        
        <div align="center">
                <table class="content" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td>
                            <div class="siteHeader">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td width="120">
                                            <img alt="Access Small Logo" src="../images/logo_small.jpg" />
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
                                            <table border="0" width="100%" style="background-color:silver" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" >
                                                        <table border="0" a cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="3">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                 <td colspan="3">
                                                                    <div style="width:100%">
	                                                                    <fieldset>
		                                                                    <legend class="title">Administration Services</legend>
		                                                                    <div style="padding:1em">		
			                                                                    <div align="left">
				                                                                    <p>The Administration Services are used to create, modify and update Users, Groups, Program Codes and Departments.</p>	
			                                                                    </div>
			                                                                    <div align="right">
				                                                                    <input style="width:8em;text-align: center" class="button" type="button" value="Administration" onclick="document.location='administration/'" />	
			                                                                    </div>
		                                                                    </div>
	                                                                    </fieldset>
                                                                	
	                                                                </div>

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
			                                                        2001 - 2006, All rights reserved.
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
