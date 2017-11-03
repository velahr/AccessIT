USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_VehicleCoverages]    Script Date: 07/28/2010 11:15:28 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_VehicleCoverages')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_VehicleCoverages]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_VehicleCoverages]    Script Date: 07/28/2010 11:15:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[ClaimsDocs_XMLGeneration_VehicleCoverages]
	@intVehicleInfoRequestTypeID int,
	@varPolicyNo VarChar(12),
	@varClaimNo VarChar(10),
	@varVIN VarChar(30),
	@varCompanyNumber VarChar(30),
	@varCoverage VarChar(30),
	@intLimitCode int
As
Begin

	--1 = Get the VIN of vehicles associated with claim
	If @intVehicleInfoRequestTypeID =1
		Begin
			Select 
				*
			From  
				dbo.fn_clm_returnVehiclesList(@varClaimNo)
		
		End
	--2 = Get Company, Coverage and Limit Code associated
	--with the claims vehicle	
	If @intVehicleInfoRequestTypeID =2	
		Begin
			SELECT     
				c.serno, 
				a.policyno, 
				a.carno, 
				a.cov_effd, 
				a.coverage, 
				a.lp_1p, 
				a.lp_1e, 
				a.written, 
				a.original, 
				a.UKey, 
				a.CreatdUser, 
				a.CreatedTime, 
				a.carno AS Expr1, 
				a.original AS Expr2, 
				a.coverage AS Expr3, 
				b.company, 
				a.lp_1e AS Expr4
			FROM         
				POLICY.genesis.dbo.all_pcovs AS a INNER JOIN
				POLICY.genesis.dbo.all_ppols AS b ON a.policyno = b.policyno INNER JOIN
				POLICY.genesis.dbo.all_pcars AS c ON c.policyno = a.policyno AND a.carno = c.carno
			WHERE     
				a.policyno = @varPolicyNo
				And
				c.serno=@varVIN
			Order By
				a.CarNo
		
		End
	--3 = Get detailed vechicle coverage information
	If @intVehicleInfoRequestTypeID =3
		Begin
			Select  
				* 
			From 
				policy.genesis.dbo.all_plmtstab
			Where
				Company=@varCompanyNumber
			And
				Cov =@varCoverage
			And
				lim_code =@intLimitCode
		End
End


GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_VehicleCoverages] to public
Go	
