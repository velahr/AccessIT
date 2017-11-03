USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetAddressee]    Script Date: 07/28/2010 11:02:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClaimsDocs_GetAddressee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClaimsDocs_GetAddressee]
GO

USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetAddressee]    Script Date: 07/28/2010 11:02:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[ClaimsDocs_GetAddressee] --331421

(@claimukey INT)

AS

SET NOCOUNT ON
/*
Uses:  		Can be used by any application that needs Insured's Name and Address
Calls	  	ams.dbo.sp_AMSmatchAddress
Returns:	Formatted Addressess and 9 digit zip code
			Query MatchLevel... If MatchLevel = 'Not_Found'...do something

Calls		Genesis.dbo.fn.GetPostNetZip
Returns:	Zipcode Plus One Digit

Author:  SCollins
Date:	   4/2007
*/


DECLARE @Addressee TABLE (
	AddresseeName				varchar(50),
	AddresseeAddressLine1 		varchar(64),
	AddresseeAddressLine2		varchar(32),
	AddresseeAddressLine3 		varchar(50),
	AddresseePhoneNumber 		varchar(15),
	AddresseeFaxNumber			varchar(15),
	AddresseeEmailAddress		varchar(50), 
	AddresseeCity 				varchar(32), 
	AddresseeState 				char(32), 
	AddresseeZip 				char(5),
    AddresseeZip4				char(4), 
    AddresseePostNetZip 		int,
    MatchLevel					varchar(32)
	
)


DECLARE 
	@FName		varchar(30),
	@LName		varchar(30),
	@MName		char(1),
	@Addr1		varchar(30),
	@Addr2		varchar(30), 
	@City		varchar(20),
	@State		char(20),
	@Zip		varchar(10),
	@Zip4		varchar(10),
	@fnameblank bit,
	@pocblank bit,
	@companyblank bit,
	@fnametemp varchar(100),
	@poctemp varchar(100),
	@companytemp varchar(100)

DECLARE @Message varchar(150)

set @fnameblank = 0
set @pocblank = 0
set @companyblank = 0

SELECT @FNametemp = FirstName From claims.claims.dbo.clm_contacts where ukey = @claimukey
If @Fnametemp is null OR @FNAMEtemp = ''
Begin
Select @fnameblank = 1

END
Select @poctemp = Pointofcontact From claims.claims.dbo.clm_contacts where ukey = @claimukey

If @poctemp = null OR @poctemp = ''
Begin
Select @pocblank = 1
end
Select @companytemp = company From claims.claims.dbo.clm_contacts where ukey = @claimukey

If @companytemp = null or @companytemp = ''
Begin
select @companyblank = 1
end


SELECT			
		@FName = case when @fnameblank= 0 then @fnametemp else
			case when @fnameblank = 1 and @pocblank = 0 then @poctemp else
            case when @fnameblank = 1 and @pocblank = 1 then CL.company
			end end end,
		@LName = CL.lastname,
				 
		@Addr1 = case when @companyblank = 0 and @fnameblank = 0 and @pocblank = 0 then
              @companytemp else case when @companyblank = 0 and @pocblank = 0 and @fnameblank = 1 then @companytemp
			end end,
		@Addr2 = CL.Street,
		@City = CL.city,
		@state = CL.state,
		@Zip = CL.zipcode
FROM 	CLAIMS.claims.dbo.clm_contacts CL 
WHERE 	CL.ukey = @claimukey 
		--and CL.ukey_contacttype = @contacttype
        --and CL.ukey_contacttype in(4,8)

BEGIN TRY

	exec ams.dbo.sp_AMSopen

	INSERT INTO @Addressee 
	(
		AddresseeAddressLine2,
		AddresseeAddressLine1,
		AddresseeCity,
		AddresseeState,
		AddresseeZip,
		AddresseeZip4,
		MatchLevel
	) 

	exec ams.dbo.sp_AMSmatchAddress @Addr2,@Addr1,@city,@state,@zip,NULL

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


IF @zip = 'UNKNO'
BEGIN
SELECT @zip = ''
End

UPDATE @Addressee

Set AddresseeName = ISNULL(rtrim(@Fname),' ') + ' ' + ISNULL(rtrim(@LName), ' ')  ,
AddresseeAddressLine3 = ISNULL(rtrim(AddresseeCity), ' ') + ',  ' + ISNULL(rtrim(AddresseeState), ' ') + '  ' + ISNULL(rtrim(AddresseeZip),' ') + '-' + AddresseeZip4 , 
AddresseePhoneNumber = ' '  ,
AddresseeFaxNumber = ' ',
AddresseeEmailAddress = ' ',
AddresseePostNetZip = AMS.dbo.fn_GetPostNetZip(CAST(SUBSTRING(@zip,1,5) AS int))
FROM @Addressee

SELECT * FROM @Addressee



SET NOCOUNT OFF

GO


Grant Execute On 
	dbo.[ClaimsDocs_GetAddressee] to public
Go	