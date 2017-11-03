USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetProducerInfo]    Script Date: 07/28/2010 11:05:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClaimsDocs_GetProducerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClaimsDocs_GetProducerInfo]
GO

USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetProducerInfo]    Script Date: 07/28/2010 11:05:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create         PROCEDURE [dbo].[ClaimsDocs_GetProducerInfo] --'ACA000501125'--,'2006-02-01'

(
@policyno varchar(15)
)

AS
/*************************************************************************
Create Date:  4/3/07
Author:       Sherry Collins

Uses:  	Can be used by any application that needs Producer's Name and Address
Calls	  	[Put the name of Jorge's process]
Returns: If Address Is Valid - Validated and Formatted Address with 9 digit zipcode
         If Address Not Valid - Same Address 
			If Address Null - NULL
Calls	   Genesis.dbo.fn.GetPostNetZip
Returns: Zipcode Plus One Digit


*/


DECLARE @prodno varchar(10)

SELECT @prodno = prodno from POLICY.genesis.dbo.all_ppols where policyno = @policyno


DECLARE @ProducerInfo TABLE (
ProducerCode varchar(15),
ProducerName varchar(50),
ProducerAddressLine1 varchar(64),
ProducerAddressLine2 varchar(32),
ProducerAddressLine3 varchar(50),
ProducerCity varchar(32),
ProducerState char(2),
ProducerZip varchar(5),
ProducerZip4 varchar(4),
ProducerPhoneNumber varchar(15),
ProducerFaxNumber varchar(15),
ProducerEmailAddress varchar(50),
MailPostNetZip int,
MatchLevel varchar(20)
)


DECLARE 

	@Addr1 varchar(30),
	@Addr2 varchar(30), 
	@City varchar(20),
	@State char(2),
	@Zip varchar(12),
	@Zip4 varchar(5)


select @Addr1 = ISNULL(AG.adr1str,' '), 
       @Addr2 = ' ',
	   @City = ISNULL(AG.adr2ct, ' '),
       @State = ISNULL(AG.adr2stt, ' '),
       @Zip = ISNULL(AG.adr2zip, ' ')
/*
SELECT		 @Addr1 = 	Case When AG.adr2str is null or Rtrim(AG.adr2str) = '' or 
								Upper(Left(adr2str,4)) = 'SAME' Then AG.adr1str else AG.adr2str end , 
				 @Addr2 = 	NULL,
				 @City = 	Case 	When AG.adr2ct is null or Rtrim(AG.adr2ct) = '' or  
								Upper(Left(adr2ct,4)) = 'SAME' Then AG.adr1ct else AG.adr2ct end, 
				 @State = 	Case 	When AG.adr2stt is null or Rtrim(AG.adr2stt) = '' or  
								Upper(Left(adr2stt,4)) = 'SAME'
								Then AG.adr1stt else AG.adr2stt end ,
				 @Zip = 		Case 	When AG.adr2zip is null or Rtrim(AG.adr2zip) = '' 
								Then AG.adr1zip else AG.adr2zip end ,
				@Zip4 = NULL
*/
from POLICY.Genesis.dbo.Agents AG Where AgentNo = @prodno
--(Select top 1 prodno from POLICY.Genesis.dbo.all_ppols Where Policyno = @policyno)

BEGIN TRY
--exec ams.dbo.sp_AMSopen




INSERT INTO @ProducerInfo (ProducerAddressLine1,ProducerAddressLine2,ProducerCity,ProducerState,ProducerZip,ProducerZip4,MatchLevel)

exec ams.dbo.sp_AMSmatchAddress @Addr1,@Addr2,@city,@state,@zip,NULL

--exec ams.dbo.sp_AMSClose
END TRY
BEGIN CATCH
	DECLARE @ErrorNumber INT;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
		@ErrorNumber = ERROR_NUMBER(),
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();

    
    RAISERROR (@ErrorMessage, -- Message text.
               @ErrorSeverity, -- Severity.
               @ErrorState -- State.
               );
		RETURN ;
END CATCH;		 


UPDATE @ProducerINFO

Set
ProducerCode = AG.agentno,
ProducerName = Ltrim(Rtrim(AG.bizname)),
ProducerAddressLine3 = rtrim(Ltrim(ProducerCity)) + ', ' + rtrim(ltrim(ProducerState)) + ' ' + rtrim(ltrim(ProducerZip)) + '-' + ProducerZip4, 
ProducerPhoneNumber = 	AG.tel1,         
ProducerFaxNumber = AG.fax,
ProducerEmailAddress = 	AG.emailaccount,
MailPostNetZip = 	dbo.fn_GetPostNetZip(ProducerZip)
FROM
POLICY.Genesis.dbo.agents AG (NoLock) JOIN POLICY.Genesis.dbo.all_ppols PO on
AG.agentno = PO.prodno
WHERE PO.policyno = @policyno 


Select ProducerState,ProducerCode,ProducerZip,ProducerName,ProducerPhoneNumber,ProducerFaxNumber,
ProducerEmailAddress,ProducerAddressLine1,ProducerAddressLine2,ProducerAddressLine3,MatchLevel,
MailPostNetZip from @ProducerInfo

--select * from @ProducerInfo

SET NOCOUNT OFF

GO


Grant Execute On 
	dbo.[ClaimsDocs_GetProducerInfo] to public
Go	