USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_LeinHolder]    Script Date: 07/28/2010 11:11:38 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_LeinHolder')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_LeinHolder]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_LeinHolder]    Script Date: 07/28/2010 11:11:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE             PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_LeinHolder] --'ACA000001020'
						(@PolicyNo CHAR(12))

AS


SELECT
	ISNULL(LeinHolder.name, ' ') 				AS LeinHolderName, 
 	ISNULL(RTRIM(LeinHolder.addr), ' ')			AS LeinHolderAddress1,	
	' ' AS LeinHolderAddressLine2,
	ISNULL(RTRIM(LeinHolder.city) + ',   ' + RTRIM(LeinHolder.state) + ' ' + RTRIM(LeinHolder.zip), '')					AS LeinHolderAddressLine3,
	' ' AS LeinHolderPhoneNumber,
	' ' AS LeinHolderFaxNumber,
	' 'AS LeinHolderEmailAddress

FROM
policy.genesis.dbo.ppols AOPS			
LEFT JOIN policy.genesis.dbo.plps LeinHolder ON AOPS.policyno = LeinHolder.policyno 

WHERE
AOPS.policyno = @PolicyNo



GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_LeinHolder] to public
Go	
