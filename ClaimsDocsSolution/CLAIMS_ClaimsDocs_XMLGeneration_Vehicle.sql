USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Vehicle]    Script Date: 07/28/2010 11:12:36 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_Vehicle')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Vehicle]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Vehicle]    Script Date: 07/28/2010 11:12:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE              PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Vehicle] --'ACA000001020', 1
(@policyno CHAR(12), @carno int)

AS


DECLARE @Whitehill_XMLGeneration_Vehicle1  TABLE (
	[VehicleSerialNumber] [varchar] (30)  NULL ,
	[VehicleYear] [char] (4)  NULL ,
	[VehicleMake] [varchar] (30)  NULL ,
	[VehicleModel] [varchar] (30)  NULL ,
	[VehiclePlateNumber] [varchar] (15)  NULL ,
	[carno] [int] NULL 
) 

--DELETE FROM Whitehill_XMLGeneration_Vehicle1

INSERT INTO @Whitehill_XMLGeneration_Vehicle1 (carno) Values(@carno)

UPDATE @Whitehill_XMLGeneration_Vehicle1
SET VehicleSerialNumber =    ISNULL(Vehicle.serno, ' '), 
	VehicleYear = ISNULL(RTRIM(Vehicle.year), ' ')				,
	VehicleMake = ISNULL(RTRIM(Vehicle.vmake), ' ')			,
	VehicleModel = ISNULL(RTRIM(Vehicle.vModel), ' ')			,
	VehiclePlateNumber = ' ' 

FROM
policy.genesis.dbo.ppols AOPS 	
LEFT JOIN policy.genesis.dbo.pcars Vehicle ON AOPS.policyno = Vehicle.Policyno 		

WHERE
AOPS.policyNo = @PolicyNo AND Vehicle.carno = @CarNo

Update @Whitehill_XMLGeneration_Vehicle1
SET VehicleSerialNumber =    ISNULL(VehicleSerialNumber, ''), 
	VehicleYear = ISNULL(RTRIM(VehicleYear), '')				,
	VehicleMake = ISNULL(RTRIM(VehicleMake), '')			,
	VehicleModel = ISNULL(RTRIM(VehicleModel), '')			,
	VehiclePlateNumber = ' ',
	carno = ' '

SELECT  VehicleSerialNumber,VehicleYear,VehicleMake,VehicleModel,VehiclePlateNumber
FROM @Whitehill_XMLGeneration_Vehicle1


SET NOCOUNT OFF




GO


Grant Execute On 
	dbo.ClaimsDocs_XMLGeneration_Vehicle to public
Go	
