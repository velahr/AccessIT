<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdmin.aspx.cs" Inherits="ClaimsDocsClient.secure.UserAdmin" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : User Administration</title>
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
                                    <div style="padding: 5px 5px 5px 0px">
                                        <a href="../Administration.aspx">Administration</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="main" align="center">
                    <div class="nav" align="left">
                        <table>
                            <tr>
                                <td>
                                    You are visiting: <a class="nav" href="#" onclick="window.location.replace('AdminNonDocs.aspx')">Administration</a> &rarr; <a class="nav" href="#" onclick="window.location.replace('UserList.aspx')">Users</a> &rarr; <span class="nav">Add User</span>
                                    <td>
                            </tr>
                        </table>
                    </div>
                    <div class="body">
                        <fieldset>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td valign="top" colspan="2" align="center">
                                        <h1>
                                            <asp:Label ID="lblHeader" runat="server"></asp:Label>
                                        </h1>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="400px">
                                            <tr>
                                                <td colspan="1" align="left">
                                                    <asp:Label ID="lblState" runat="server" Visible="false" Text="-1"></asp:Label>
                                                    <asp:Label ID="lblUserID" runat="server" Visible="false" Text="0"></asp:Label>
                                                    User Name : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Department Name : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="cboDepartment" runat="server"  DataValueField="DepartmentID" DataTextField="DepartmentName" Width="200px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Administrator : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="cboAdministrator" runat="server" Width="200px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Approver : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="cboApprover" runat="server" Width="200px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Designer : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="cboDesigner" runat="server" Width="200px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Login Password : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtLoginPassword" runat="server" Width="200px"  ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Confirm Password : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtConfirmPassword" runat="server" Width="200px"  ></asp:TextBox>
                                                </td>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Review Required : 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:DropDownList ID="cboReviewRequired" runat="server" Width="200px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Phone Ext.: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtPhoneExtension" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtPhoneExtension" runat="server" ControlToValidate="txtPhoneExtension" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Email Address: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtEMailAddress" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtEMailAddress" runat="server" ControlToValidate="txtEMailAddress" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Title: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Signature Name: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtSignatureName" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtSignatureName" runat="server" ControlToValidate="txtSignatureName" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Signature File Path: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:TextBox ID="txtSignatureFilePath" runat="server" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtSignatureFilePath" runat="server" ControlToValidate="txtSignatureFilePath" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Groups: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:ListBox 
                                                        ID="lstGroups" 
                                                        runat="server" 
                                                        Width="200px" 
                                                        SelectionMode="Multiple" 
                                                        DataValueField="GroupID" 
                                                        DataTextField="DepartmentName"></asp:ListBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Active: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkActive" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            
                                            
                                        </table>
                                    
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="1">
                                        <asp:Button ID="cmdDo" style="width:8em;text-align: center" class="button" runat="server" Text="Ok" onclick="cmdDo_Click"   /> 
                                    </td>
                                    <td align="center" colspan="1">
                                        <input style="width:8em;text-align: center" class="button" type="button" value="Cancel" onclick="window.location.replace('UserList.aspx')" />
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
