USE [Genesis]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetCompanyInfo]    Script Date: 07/28/2010 17:10:35 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_GetCompanyInfo')
DROP PROCEDURE [dbo].[ClaimsDocs_GetCompanyInfo]
GO

USE [Genesis]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetCompanyInfo]    Script Date: 07/28/2010 17:10:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create Procedure [dbo].[ClaimsDocs_GetCompanyInfo]
	(
		@CompanyNumber Char(2)
	)
/***************************************
Author: Tony Chu
Date Created: 01/20/2009
Update :	05/24/2010 K. Phifer Renamed from GetCompanyInfo to ClaimsDocs_GetCompanyInfo
			to reflect new application
Summary: Gets company info from DB based on company number
****************************************/
AS

	SELECT TOP 1 AccessName, cdirectory, ClaimsAddress, ClaimsCSZ, ClaimsLive, ClaimsName, ClaimsPhone, CompanyID,
		CompanyLongName, CompanyShortName, cprtfilesd, cprtfilesm, CSRHours, CWALive, DirBillAddress,
		DirBillCSZ, DirBillName, Fax, GeneralAddress, GeneralCSZ, GeneralName, iCompanyStateId, imagerightfaxcovervalue_CLAIMS, 
		imagerightfaxcovervalue_UW, InsoftLive, Is_AccessCompany, IVRPhone, lgdsdiscnt, ncomm1, ncomm2, ndncndays, 
		ndownpct04, ndownpct10, ndwnpctp02, ndwnpctp05, ndwnpctp11, nendfee, nfilefee, ngddsr22, nholdayadd, 
		ninstalfee, nnsffee, npayplan2, npayplan3, npepdown, npepmo, npolfee, nreinsfee, nrewrfee, PolicyPrefix, 
		ProcessCommissions, RenDaylag, StateOfOperation, UKey, UnderwritingPhone, Website, WebsiteLive
        FROM TBL_CompInfo
		WHERE CompanyID = @CompanyNumber


GO


Grant Execute On 
	dbo.[ClaimsDocs_GetCompanyInfo] to public
Go	