USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Claims]    Script Date: 07/28/2010 11:08:20 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_Claims')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Claims]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Claims]    Script Date: 07/28/2010 11:08:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE    PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Claims] (@PolicyNo CHAR(12), @ClaimNo CHAR(10), @ukey_Contact INT)
--'ACA000500473','ACI0000004'
--'ACA000500682','ACI0000002',241805

AS



DECLARE @LastEndorsementDate DATETIME
DECLARE @Company CHAR(2) 
DECLARE @ProgramCode CHAR(3) 
DECLARE @Mode CHAR(4)


SET @Mode = CASE WHEN @@SERVERNAME = 'GENESIS' THEN 'PROD' ELSE 'TEST' END


SELECT @LastEndorsementDate = CONVERT(VARCHAR(20),MAX(entdate), 101)
FROM policy.genesis.dbo.all_pacct 
WHERE policyno = @PolicyNo AND type = '3'

SELECT 
	'CLAIMS' 						AS Department,
 	LOWER(SUBSTRING(system_user, 
	CHARINDEX('\',system_user ,1) + 1,20))			AS [User],
	RTRIM(ISNULL(compinfo.policyprefix,' '))			AS ProgramCode,
   Claims.claimno,
	RTRIM(AOPS.Policyno)					AS PolicyNumber,
	CASE WHEN AOPS.Term = '03' THEN '3 Month' 
 	     WHEN AOPS.Term = '06' THEN '6 Month' 
	     WHEN AOPS.Term = '12' THEN '12 Month' 
	END 							AS PolicyTerm,
	CONVERT(VARCHAR(12), AOPS.fromd,101) 			AS PolicyEffectiveDate,
	CONVERT(VARCHAR(12), AOPS.expdate,101)			AS PolicyLastExpirationDate,
	ISNULL(CONVERT(VARCHAR(12), 
		AOPS.cancdate,101),'01/01/1900')		AS PolicyCancellationDate,
	ISNULL(CONVERT(VARCHAR(20), 
		@LastEndorsementDate,101),'01/01/1900')		AS PolicyLastEndorsementDate,
	RTRIM(StatusLookup.Description)				AS PolicyStatus,
	@Mode							AS Mode

FROM 	policy.genesis.dbo.ppols AOPS 
	JOIN Claims.dbo.clbase Claims 					ON AOPS.policyno = Claims.polno
	JOIN genesis.dbo.compinfo compinfo			ON AOPS.company = Compinfo.companyid
	LEFT JOIN genesis.dbo.lk_StatCode StatusLookup  	ON AOPS.status = StatusLookup.status 

WHERE AOPS.policyno = @PolicyNo AND Claims.claimno = @ClaimNo



GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_Claims] to public
Go	
