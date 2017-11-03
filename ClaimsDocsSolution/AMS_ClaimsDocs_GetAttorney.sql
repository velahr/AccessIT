USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetAttorney]    Script Date: 07/28/2010 11:03:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ClaimsDocs_GetAttorney]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ClaimsDocs_GetAttorney]
GO

USE [AMS]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_GetAttorney]    Script Date: 07/28/2010 11:03:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


Create         PROCEDURE [dbo].[ClaimsDocs_GetAttorney] --299300
(@claimukey int)

AS

SET NOCOUNT ON



DECLARE @Attorney TABLE (

	AttorneyFirstName 			varchar(50),
	AttorneyMiddleInitial		varchar(30),
	AttorneyLastName			varchar(30), 
	AttorneyAddressLine1 		varchar(64),
	AttorneyAddressLine2		varchar(64),
	AttorneyAddressLine3		varchar(64), 
	AttorneyCity 				varchar(32), 
	AttorneyState 				char(2), 
	AttorneyZip 				varchar(5),
	AttorneyZip4				varchar(4),
	AttorneyPhoneNumber 		varchar(15),
	AttorneyFaxNumber			varchar(15),
	AttorneyEmailAddress		varchar(50),
	MailPostNetZip				int,
	MatchLevel					varchar(20)
)


Declare @fnameblank bit
declare @pocblank bit
declare @companyblank bit
DECLARE @Fname varchar(80)
DECLARE @LName varchar(30)
DECLARE @Mname varchar(30)
DECLARE @Company varchar(64)
DECLARE @Addr1 varchar(50)
DECLARE @Addr2 varchar(50)
DECLARE @City varchar(20)
DECLARE @State char(2)
DECLARE @Zip varchar(12)
DECLARE @Fnametemp varchar(100)
DECLARE @POCtemp varchar(100)
declare @companytemp varchar(100)

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
            case when @fnameblank = 1 and @pocblank = 1 then @companytemp
			end end end,
		@LName = CL.lastname,
		@Addr1 = case when @companyblank = 0 and @fnameblank = 0 and @pocblank = 0 then
              @companytemp else case when @companyblank = 0 and @pocblank = 0 and @fnameblank = 1 then @companytemp
			   else case when @companyblank = 0 and @pocblank = 1 and @fnameblank = 0 then @companytemp
		end end end,
		
		@Addr2 = CL.Street,
		@City = CL.city,
		@state = CL.state,
		@Zip = CL.zipcode
FROM 	CLAIMS.claims.dbo.clm_contacts CL 
WHERE 	CL.ukey = @claimukey 
		--and CL.ukey_contacttype = @contacttype
        and CL.ukey_contacttype in(4,8)



BEGIN TRY

--exec ams.dbo.sp_AMSopen


INSERT INTO @Attorney 
(
AttorneyAddressLine2,
AttorneyAddressLine1,
AttorneyCity,
AttorneyState,
AttorneyZip,
AttorneyZip4,
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

UPDATE @Attorney
Set AttorneyFirstName =		  ISNULL(@FName, ' '),
AttorneyMiddleInitial =		  ISNULL(@MName, ' '),	
AttorneyLastName =			  ISNULL(@LName, ' '),
AttorneyAddressLine3 =		  ISNULL(RTRIM(AttorneyCity), ' ') + ',   '  +
							  ISNULL(RTRIM(AttorneyState), ' ') +  ' ' + 
							  ISNULL(RTRIM(AttorneyZip), ' ') + '-' + AttorneyZip4,
AttorneyPhoneNumber =		  ' ',
AttorneyFaxNumber =			  ' ',
AttorneyEmailAddress =		  ' '

FROM @Attorney






SELECT * FROM @Attorney



set nocount off


GO


Grant Execute On 
	dbo.[ClaimsDocs_GetAttorney] to public
Go	