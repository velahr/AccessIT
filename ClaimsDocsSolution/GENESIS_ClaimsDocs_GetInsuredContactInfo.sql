USE [AMS]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Alter      PROCEDURE [dbo].[ClaimsDocs_GetInsuredContactInfo] --'ACA000501125'
(@policyno varchar(12))

AS
SET NOCOUNT ON

/*
Uses:  	Can be used by any application that needs Insured's Name and Address
Calls	  	[Put the name of Jorge's process]
Returns: If Address Is Valid - Validated and Formatted Address with 9 digit zipcode
         If Address Not Valid - Same Address 
			If Address Null - NULL
Calls	   Genesis.dbo.fn.GetPostNetZip
Returns: Zipcode Plus One Digit

Author:  SCollins
Date:	   4/2007
*/


DECLARE @InsuredContact TABLE (



	NamedInsuredFirstName 			varchar(15), 
	NamedInsuredMiddleInitial 		char(1), 
	NamedInsuredLastName 			varchar(20), 
	NamedInsuredAddressLine1 		varchar(30),
	NamedInsuredAddressLine2		varchar(30), 
	NamedInsuredCity 				varchar(20), 
	NamedInsuredState 				char(20), 
	NamedInsuredZip 				char(10),
    NamedInsuredZip4				char(4), 
	NamedInsuredAddressLine3 		varchar(50),
	NamedInsuredPhoneNumber 		varchar(15),
	NamedInsuredFaxNumber			varchar(15),
	NamedInsuredEmailAddress		varchar(50),
    NamedInsuredGaragingAddr 		varchar(30),
	NamedInsuredGaragingCity 		varchar(20),
    NamedInsuredGaragingState 		char(2),
    NamedInsuredGaragingZip 		char(10),
    NamedInsuredPostNetZip 			int,
	NamedInsuredFullName			varchar(50), 
    MatchLevel varchar(20)
	


)



DECLARE 
	@FName varchar(30),
	@MName varchar(30),
	@LName varchar(30),
	@Addr1 varchar(30),
	@Addr2 varchar(30), 
	@City varchar(20),
	@State char(20),
	@Zip varchar(10),
	@Zip4 varchar(10)



SELECT		 
@Fname = PO.fname,
--@MName = PO.mi,
@LName = PO.lname,
@Addr1 = PO.addr,
			 @Addr2 = NULL,
			 @City = PO.city,
			 @State = PO.state,
			 @Zip = PO.zip,
			 @Zip4 = NULL
FROM 		 POLICY.Genesis.dbo.all_ppols PO
WHERE 		 PO.policyno = @policyno


BEGIN TRY

exec ams.dbo.sp_AMSopen

INSERT INTO @InsuredContact 
(
	NamedInsuredAddressLine1,
	NamedInsuredAddressLine2,
	NamedInsuredCity,
	NamedInsuredState,
	NamedInsuredZip,
	NamedInsuredZip4,
	MatchLevel
) 

exec ams.dbo.sp_AMSmatchAddress @Addr1,@Addr2,@city,@state,@zip,NULL
--exec ams.dbo.sp_AMSclose
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

UPDATE @InsuredContact

Set NamedInsuredFullName =Ltrim(Rtrim(@Fname)) + ' ' + 	Ltrim(Rtrim(@LName)) ,
NamedInsuredFirstName = Ltrim(Rtrim(@FName))     ,  
--NamedInsuredMiddleInitial = Ltrim(Rtrim(@MName)),        
NamedInsuredLastName = Ltrim(Rtrim(@LName))     ,
NamedInsuredAddressLine3 = Ltrim(Rtrim(NamedInsuredCity)) + ',  ' + 	Ltrim(Rtrim(NamedInsuredState)) + '  ' + Ltrim(Rtrim(NamedInsuredZip)) + '-' + LTRIM(RTRIM(NamedInsuredZip4)) ,
NamedInsuredPhoneNumber = Ltrim(Rtrim(PO.ins_phone)) ,
NamedInsuredFaxNumber = ' ',
NamedInsuredEmailAddress = ' ',
NamedInsuredGaragingAddr = Case when PO.addr = PO.gar_addr then NamedInsuredAddressLine1 else PO.gar_addr end ,
NamedInsuredGaragingCity = Case When PO.city = PO.gar_city then NamedInsuredCity else PO.gar_city end ,
NamedInsuredGaragingState = Case when PO.state = PO.gar_state then NamedInsuredState else PO.gar_state end ,
NamedInsuredGaragingZip = Case when PO.zip = PO.gar_zip then NamedInsuredZip else PO.gar_zip end, 
NamedInsuredPostNetZip = AMS.dbo.fn_GetPostNetZip(CAST(SUBSTRING(@zip,1,5) AS int))

	


FROM POLICY.genesis.dbo.all_ppols PO Where policyno = @policyno

select * from @InsuredContact

SET NOCOUNT OFF


Go

Grant Execute On 
	dbo.[ClaimsDocs_GetInsuredContactInfo] to public
Go	
