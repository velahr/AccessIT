USE [ClaimsDocs]
GO

/****** Object:  StoredProcedure [dbo].[spGetDocDeclineInformationByInstanceID]    Script Date: 07/28/2010 11:16:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetDocDeclineInformationByInstanceID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[spGetDocDeclineInformationByInstanceID]
GO

USE [ClaimsDocs]
GO

/****** Object:  StoredProcedure [dbo].[spGetDocDeclineInformationByInstanceID]    Script Date: 07/28/2010 11:16:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure	[dbo].[spGetDocDeclineInformationByInstanceID]
@varInstanceID  VarChar(36)
As
Begin
SELECT     
	dbo.tblDocumentApprovalQueue.ApprovalQueueID, 
	dbo.tblDocumentApprovalQueue.InstanceID, 
	dbo.tblDocumentApprovalQueue.DocumentID, 
    dbo.tblDocumentLog.PolicyNo, 
    dbo.tblDocumentLog.ClaimNo, 
    dbo.tblDocument.ContactNo, 
    dbo.tblDocument.ContactType, 
    dbo.tblUser.UserName AS SubmitterName, 
    dbo.tblUser.EMailAddress AS SubmitterEMailAddress, 
    tblUser_1.EMailAddress AS DeclinerEMailAddress, 
    tblUser_1.UserName AS DeclinerName, 
    dbo.tblDocument.DocumentCode, 
    dbo.tblDocument.Description, 
    dbo.tblDocumentLog.ReasonDeclined, 
    dbo.tblDocumentLog.DateDeclined
FROM         
	dbo.tblDocumentApprovalQueue INNER JOIN
	dbo.tblUser ON dbo.tblDocumentApprovalQueue.SubmitterID = dbo.tblUser.UserID INNER JOIN
	dbo.tblDepartment ON dbo.tblUser.DepartmentID = dbo.tblDepartment.DepartmentID INNER JOIN
	dbo.tblDocumentLog ON dbo.tblDocumentApprovalQueue.InstanceID = dbo.tblDocumentLog.InstanceID INNER JOIN
	dbo.tblDocument ON dbo.tblDocumentApprovalQueue.DocumentID = dbo.tblDocument.DocumentID INNER JOIN
	dbo.tblProgram ON dbo.tblDocument.ProgramID = dbo.tblProgram.ProgramID INNER JOIN
	dbo.tblUser AS tblUser_1 ON dbo.tblDocumentLog.DeclinerID = tblUser_1.UserID		
WHERE     
	(dbo.tblDocumentApprovalQueue.InstanceID = @varInstanceID)
End


GO


Grant Execute On 
	dbo.[spGetDocDeclineInformationByInstanceID] to public
Go	