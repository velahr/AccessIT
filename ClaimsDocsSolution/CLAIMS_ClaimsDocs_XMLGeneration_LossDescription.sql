USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_LossDescription]    Script Date: 07/28/2010 11:14:34 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_LossDescription')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_LossDescription]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_LossDescription]    Script Date: 07/28/2010 11:14:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

Create       PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_LossDescription] --'ACI0000015'

(@ClaimNo CHAR(10))

AS

DECLARE @monthloss varchar(20)


SELECT
	ISNULL(Claims.claimno, '')				AS ClaimNumber, 
	ISNULL(CONVERT(VARCHAR(20), Claims.lossd,101), '') 	AS ClaimDateOfLoss, 
	ISNULL(CONVERT(VARCHAR(20), Claims.rptd,101), '') 	AS ClaimReportDate, 
	ISNULL(LossDescription.street, '')			AS LossDescriptionStreetAddress,
	''							AS LossDescriptionLocation, 
	ISNULL(LossDescription.city, '') 			AS LossDescriptionCity,
	ISNULL(LossDescription.State, '')			AS LossDescriptionState,
   Datepart(dd,Claims.lossd) as DayofLoss,
   DATENAME(month, Claims.lossd) AS MonthofLoss,
   DATEPART(yy,Claims.lossd) as YearofLoss
FROM
 Claims.dbo.clbase Claims 
JOIN  Claims.dbo.clm_LossDescription LossDescription ON Claims.ukey = LossDescription.ukey_clbase 

WHERE
Claims.claimno = @ClaimNo



GO



Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_LossDescription] to public
Go	
