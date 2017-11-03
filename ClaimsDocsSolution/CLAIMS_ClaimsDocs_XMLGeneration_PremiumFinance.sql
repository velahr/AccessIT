USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_PremiumFinance]    Script Date: 07/28/2010 11:10:53 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_PremiumFinance')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_PremiumFinance]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_PremiumFinance]    Script Date: 07/28/2010 11:10:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE              PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_PremiumFinance] 
(@PolicyNo CHAR(12))

AS




SELECT
	-- Premium Finance 
	ISNULL(PremiumFinanceCo.EmailAddress, '')		AS PremiumFinanceCoEmailAddress,
	ISNULL(PremiumFinanceCo.BizName, '')			AS PremiumFinanceCoName,
	ISNULL(RTRIM(PremiumFinanceCo.Adr1Str), '') 		AS PremiumFinanceCoAddress1,
	' ' AS PremiumFinanceCoAddressLine2,
	ISNULL(RTRIM(PremiumFinanceCo.Adr1Ct) + '  ' + 	RTRIM(PremiumFinanceCo.Adr1Stt) + ' ' + 
				RTRIM(PremiumFinanceCo.Adr1Zip), '') 	AS PremiumFinanceCoAddressLine3,
	ISNULL(RTRIM(PremiumFinanceCo.BankPhone), '')		AS PremiumFinanceCoPhoneNumber,
	' ' AS PremiumFinanceCoFaxNumber

FROM
policy.genesis.dbo.ppols AOPS
LEFT JOIN policy.genesis.dbo.clFinCos PremiumFinanceCo ON AOPS.finco = PremiumFinanceCo.fincono

WHERE
AOPS.policyno = @PolicyNo




GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_PremiumFinance] to public
Go	
