USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Adjuster]    Script Date: 07/28/2010 11:13:52 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_Adjuster')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Adjuster]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Adjuster]    Script Date: 07/28/2010 11:13:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create           PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Adjuster] --'API0000103'
(@ClaimNo CHAR(10))

AS






SELECT
	ISNULL(Adjuster.extension, '')				AS AdjusterPhoneNumber,
	ISNULL(dbo.fn_GetAdjusterName(fullname,1), '') 		AS AdjusterFirstName, 
	ISNULL(dbo.fn_GetAdjusterName(fullname,2) , '')		AS AdjusterLastName, 
	'' 				AS AdjusterMiddleInitial, 
	ISNULL(RTRIM(Adjuster.loginid) + 
	'@accessgeneral.com', '')				AS AdjusterEmailAddress,
	ISNULL(Adjuster.extension, '')   			AS AdjusterFaxNumber

FROM
Claims.dbo.clbase Claims 	
LEFT JOIN Claims.dbo.clm_UserLevels Adjuster ON Claims.repcode = Adjuster.repcode 

WHERE
Claims.claimno = @ClaimNo


GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_Adjuster] to public
Go	
