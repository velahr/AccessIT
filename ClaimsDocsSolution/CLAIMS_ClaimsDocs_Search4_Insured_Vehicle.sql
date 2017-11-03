USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_Search4_Insured_Vehicle]    Script Date: 07/28/2010 11:07:39 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_Search4_Insured_Vehicle')
DROP PROCEDURE [dbo].[ClaimsDocs_Search4_Insured_Vehicle]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_Search4_Insured_Vehicle]    Script Date: 07/28/2010 11:07:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC [dbo].[ClaimsDocs_Search4_Insured_Vehicle] --'ACA000500540',241987,1
(@policyno varchar(12),@contactukey int,@vehiclefound int OUTPUT)

AS
set nocount on
DECLARE @carno int
DECLARE @rcount int
DECLARE @returncode int
DECLARE @vehicleukey int
DECLARE @vinnumber varchar(25)
DECLARE @vmake varchar(30)
DECLARE @vmodel varchar(30)
DECLARE @vyear char(4)

SET @carno = 0
SET @returncode = 1
SET @rcount = 0
SET @vehicleukey = 0
SET @vinnumber = ' '
SET @vmake = ' '
SET @vmodel = ' '
SET @vyear = ' '
SELECT @vehiclefound = 0

DECLARE @VehicleTable TABLE 
	(
		vehicleukey varchar(10),
		carno char(1)
	)




SELECT  	@vinnumber = vin 
FROM 		clm_vehicles 
WHERE 	ukey_contact = @contactukey

INSERT INTO @VehicleTable

SELECT 	ukey,carno 
FROM 		policy.genesis.dbo.all_pcars 
WHERE 	policyno= @policyno and serno = @vinnumber




IF (SELECT count(*) FROM @VehicleTable) = 0 
BEGIN


   SELECT top 1 	@vyear = [year], @vmake = make, @vmodel = model
	FROM 			 	clm_vehicles 
	WHERE 		 	ukey_contact = @contactukey 
END

IF @@ROWCOUNT > 0 
BEGIN
  
	SELECT TOP 1 	@vehicleukey = pcars_ukey, @carno = carno 
	FROM 				policy.genesis.dbo.close_pcars 
	WHERE 			policyno = @policyno and  [year] like @vyear+'%' and 
						vmake like @vmake+'%' and vmodel like @vmodel +'%'
	
	IF @@ROWCOUNT > 0 
		BEGIN
	   
			INSERT INTO		@VehicleTable 
			VALUES			(@vehicleUkey,@carno)
			SELECT @vehiclefound = 1
		END
	ELSE 
		BEGIN
			SELECT @vehiclefound = 0
			--GOTO ONERROR

		END

END

SELECT TOP 1 * FROM @VehicleTable


GO


Grant Execute On 
	dbo.[ClaimsDocs_Search4_Insured_Vehicle] to public
Go