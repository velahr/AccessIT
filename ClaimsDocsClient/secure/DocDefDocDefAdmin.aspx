<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocDefDocDefAdmin.aspx.cs" Inherits="ClaimsDocsClient.secure.DocDefDocDefAdmin" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ClaimsDocs : Document Definition Administration</title>
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
                                        <a href="../Administration.aspx">Administration</a>
                                    </div>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td class="main" align="center">
                    <div  style="width: 100%">
                        <fieldset>
                            <legend class="title">Document Definition</legend>
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
                                                <td colspan="1" align="right">
                                                    <asp:Label ID="lblState" runat="server" Visible="false" Text="-1"></asp:Label>
                                                    <asp:Label ID="lblDocumentID" runat="server" Visible="false" Text="0"></asp:Label>
                                                    Doc ID : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtDocumentID" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtDocumentID" runat="server" ControlToValidate="txtDocumentID" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Description : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtDescription" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtDescription" runat="server" ControlToValidate="txtDescription" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Department : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboDepartment" runat="server"  DataValueField="DepartmentID" DataTextField="DepartmentName" Width="150px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Program : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboProgram" runat="server"  DataValueField="ProgramID" DataTextField="ProgramCode" Width="150px"></asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                             <tr>
                                                <td colspan="1" align="right" valign="top">
                                                    Groups : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:ListBox ID="lstGroups" runat="server" DataValueField="GroupID" SelectionMode=Multiple DataTextField="GroupName" Width="150px" Height="100px"></asp:ListBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr> 
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td class="main" align="center">
                    <div  style="width: 100%">
                        <fieldset>
                            <legend class="title">Additional Detail Information</legend>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Template Name: 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtTemplateName" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtTemplateName" runat="server" ControlToValidate="txtTemplateName" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Stylesheet Name : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtStyleSheetName" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtStyleSheetName" runat="server" ControlToValidate="txtStyleSheetName" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Attached Document : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtAttachedDocument" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtAttachedDocument" runat="server" ControlToValidate="txtAttachedDocument" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Effective Date : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtEffectiveDate" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="rangevaltxtEffectiveDate" runat="server" Type="Date" MinimumValue="01/01/1990" MaximumValue="01/01/2030" ControlToValidate="txtEffectiveDate" ErrorMessage="required"></asp:RangeValidator>
                                                    <asp:RequiredFieldValidator id="valtxtEffectiveDate" runat="server" ControlToValidate="txtEffectiveDate" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Expiration Date : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtExpirationDate" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RangeValidator ID="rangevaltxtExpirationDate" runat="server" Type="Date" MinimumValue="01/01/1990" MaximumValue="01/01/2030" ControlToValidate="txtExpirationDate" ErrorMessage="required"></asp:RangeValidator>
                                                    <asp:RequiredFieldValidator id="valtxtExpirationDate" runat="server" ControlToValidate="txtExpirationDate" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Proof Of Mailing : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboProofOfMailing" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Import To Datamatx : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboImportToDatamatx" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Approval Required : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboApprovalRequired" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Auto Diary Update : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboAutoDiaryUpdate" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Diary # of Days : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtDiaryNumberOfDays" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtDiaryNumberOfDays" runat="server" ControlToValidate="txtDiaryNumberOfDays" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                           <tr>
                                                <td colspan="1" align="right">
                                                    Import To ImageRight : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboImportToImageRight" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    ImageRight DocumentID : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtImageRightDocumentID" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtImageRightDocumentID" runat="server" ControlToValidate="txtImageRightDocumentID" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    ImageRight Document Section : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtImageRightDocumentSession" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtImageRightDocumentSession" runat="server" ControlToValidate="txtImageRightDocumentSession" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    ImageRight Drawer : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:TextBox ID="txtImageRightDrawer" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator id="valtxtImageRightDrawer" runat="server" ControlToValidate="txtImageRightDrawer" ErrorMessage="required"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Active : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:DropDownList ID="cboActive" runat="server" Width="150px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="right">
                                                    Last Modified : 
                                                </td>
                                                <td colspan="1" align="center">
                                                    <asp:Label ID="lblLastModified" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr> 
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td class="main" align="center">
                    <div  style="width: 100%">
                        <fieldset>
                            <legend class="title">Additional Copies To</legend>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="300px">
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Producer: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkCopyToProducer" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Insured: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkCopyToInsured" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Lien Holder: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkCopyToLienholder" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Finance Company: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkCopyToFinanceCompany" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="1" align="left">
                                                    Attorney: 
                                                </td>
                                                <td colspan="1" align="left">
                                                    <asp:CheckBox ID="chkCopyToAttorney" runat="server" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            
                                        </table>
                                    </td>
                                </tr> 
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td class="main" align="center">
                    <div  style="width: 100%">
                        <fieldset>
                            <legend class="title">Free Form Fields</legend>
                            <table class="body" cellspacing="8">
                                <tr>
                                    <td colspan="2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="500px">
                                            <tr>
                                                <td>
                                                </td>
                                                
                                                <td align="center">
                                                    <font face="arial" size="2">
                                                        <b>
                                                            Name
                                                        </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="center">
                                                    <font face="arial" size="2">
                                                        <b>
                                                            Description
                                                        </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="center">
                                                    <font face="arial" size="2">
                                                        <b>
                                                            Type
                                                        </b>
                                                    </font>
                                                </td>
                                                
                                                <td align="center">
                                                    <font face="arial" size="2">
                                                        <b>
                                                            Req'd
                                                        </b>
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 1 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName01" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription01" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType01" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired01" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 2 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName02" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription02" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType02" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired02" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 3 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName03" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription03" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType03" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired03" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 4 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName04" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription04" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType04" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired04" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 5 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName05" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription05" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType05" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired05" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 6 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName06" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription06" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType06" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired06" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 7 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName07" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription07" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType07" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired07" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 8 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName08" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription08" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType08" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired08" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 9 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName09" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription09" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType09" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired09" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 10 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName10" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription10" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType10" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired10" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 11 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName11" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription11" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType11" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired11" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 12 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName12" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription12" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType12" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired12" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 13 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName13" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription13" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType13" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired13" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 14 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName14" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription14" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType14" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired14" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 15 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName15" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription15" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType15" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired15" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 16 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName16" MaxLength="16" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription16" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType16" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired16" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 17 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName17" MaxLength="17" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription17" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType17" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired17" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 18 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName18" MaxLength="18" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription18" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType18" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired18" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 19 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName19" MaxLength="19" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription19" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType19" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired19" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                    <font face="arial" size="2">
                                                        Field 20 : 
                                                    </font>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtFieldName20" MaxLength="20" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:TextBox ID="txtDescription20" MaxLength="255" runat=server Width="150px"></asp:TextBox>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboType20" runat=server Width="75px">
                                                        <asp:ListItem Selected="True" Value="STRING" Text="String"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="TEXT" Text="Text"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="NUMBER" Text="Number"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="DATE" Text="Date"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:DropDownList ID="cboRequired20" runat=server Width="50px">
                                                        <asp:ListItem Selected="True" Value="N" Text="No"></asp:ListItem>
                                                        <asp:ListItem Selected="False" Value="Y" Text="Yes"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </td>
                                </tr> 
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td align=center>
                    <table border=0 cellpadding=0 cellspacing=0>
                        <tr>
                            <td align="center" colspan="1">
                                <asp:Button ID="cmdDo" style="width:8em;text-align: center" class="button" runat="server" Text="Ok" onclick="cmdDo_Click"   /> 
                            </td>
                            <td align="center" colspan="1">
                                <asp:Button ID="cmdFillForm" style="width:8em;text-align: center" class="button" CausesValidation="false" runat="server" Text="Fill Form" onclick="cmdFillForm_Click"   /> 
                            </td>
                            <td align="center" colspan="1">
                                <input style="width:8em;text-align: center" class="button" type="button" value="Cancel" onclick="window.location.replace('docdefdoclist.aspx')" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan=3>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <div class="copyright">
                                    &copy; <a href="http://www.accessgeneral.com/" target="_new">Access Insurance Holdings, Inc.</a> 2001 - 2006, All rights reserved.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=3>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
