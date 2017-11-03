USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Coverages]    Script Date: 07/28/2010 11:09:25 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE name = 'ClaimsDocs_XMLGeneration_Coverages')
DROP PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Coverages]
GO

USE [Claims]
GO

/****** Object:  StoredProcedure [dbo].[ClaimsDocs_XMLGeneration_Coverages]    Script Date: 07/28/2010 11:09:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE       PROCEDURE [dbo].[ClaimsDocs_XMLGeneration_Coverages] --'ACA000500473',1

(@policyno varchar(20),@carno int)

AS

DECLARE  
	@Whitehill_Covs TABLE (
	[RowInserted] [bit] NULL,
	[Accidental_Death] [varchar] (100)  NULL ,
	[Arbitration_Agreement_Waiver] [varchar] (100)  NULL ,
	[Auto_Club] [varchar] (100)  NULL ,
	[BillingService_Charges] [varchar] (100)  NULL ,
	[Bodily_Injury] [varchar] (100)  NULL ,
	[Brokers_Fee] [varchar] (100)  NULL ,
	[CIGA] [varchar] (100)  NULL ,
	[Collision] [varchar] (100)  NULL ,
	[Collission_Deductible_Waiver] [varchar] (100)  NULL ,
	[Combined_FPB] [varchar] (100)  NULL ,
	[Combined_Single_Limits] [varchar] (100)  NULL ,
	[Comprehensive] [varchar] (100)  NULL ,
	[Expense_Constant] [varchar] (100)  NULL ,
	[Extra_Medical] [varchar] (100)  NULL ,
	[Filing_Fee] [varchar] (100)  NULL ,
	[Funeral] [varchar] (100)  NULL ,
	[Income_Loss] [varchar] (100)  NULL ,
	[Medical] [varchar] (100)  NULL ,
	[Personal_Family_Protection] [varchar] (100)  NULL ,
	[Policy_Fee] [varchar] (100)  NULL ,
	[Property_Damage] [varchar] (100)  NULL ,
	[Rental_Reimbursement] [varchar] (100)  NULL ,
	[Special_Equipment] [varchar] (100)  NULL ,
	[Towing] [varchar] (100)  NULL ,
	[UnderInsured_Motorist_Stacked] [varchar] (100)  NULL ,
	[UnderInsured_Motorist_UnStacked] [varchar] (100)  NULL ,
	[Uninsured_Motorist_BI] [varchar] (100)  NULL ,
	[Uninsured_Motorist_PD] [varchar] (100)  NULL ,
	[UnInsured_Motorist_Stacked] [varchar] (100)  NULL ,
	[UnInsured_Motorist_UnStacked] [varchar] (100)  NULL 
) 

declare @covnew char(3)
declare @insertcov varchar(100)
declare @company char(2)
declare @limits char(4)
declare acursor CURSOR FOR
SELECT a.coverage,b.company,a.lp_1e from policy.genesis.dbo.all_pcovs a join
	    policy.genesis.dbo.all_ppols b on a.policyno = b.policyno
		 where a.policyno = @policyno
open acursor
fetch acursor into @covnew,@company,@limits

while @@Fetch_Status=0

BEGIN
	--Create Starter Row
	Insert into @WhiteHill_Covs (RowInserted) Values(1)

	If @covnew = '010' 
		Begin
			Update 
				@Whitehill_Covs Set Bodily_Injury =(
				
				SELECT Top 
					1 LimitLongDesc 
				From 
					policy.genesis.dbo.all_plmtstab 
				Where 
					cov = @covnew 
				and 
					company = @company 
				and 
					lim_code = @limits)
		End
		
   
	if @covnew = '020' BEGIN
		Update @Whitehill_Covs Set Property_Damage =(SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits)
			 end
	if @covnew = '030' and @company = '41' BEGIN
			Update @Whitehill_Covs Set UnInsured_Motorist_UnStacked = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits)
			end
	if @covnew = '030' and @company <> '41' BEGIN
			Update @Whitehill_Covs Set UnInsured_Motorist_BI = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits)
			end
	if @covnew = '040' BEGIN
		Update @Whitehill_Covs Set UnInsured_Motorist_PD = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '045' BEGIN
		Update @Whitehill_Covs Set UnInsured_Motorist_Stacked = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end

IF @covnew = '046' BEGIN
		Update @Whitehill_Covs Set UnderInsured_Motorist_Stacked = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '047' BEGIN
		Update @Whitehill_Covs Set UnInsured_Motorist_UnStacked = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end

	if @covnew = '050' BEGIN
		Update @Whitehill_Covs Set Medical = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
	IF @covnew = '051' BEGIN
		Update @Whitehill_Covs Set Funeral = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '052' BEGIN
		Update @Whitehill_Covs Set Accidental_Death = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '053' BEGIN
		Update @Whitehill_Covs Set Combined_FPB = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '054' BEGIN
		Update @Whitehill_Covs Set Income_Loss = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '055' BEGIN
		Update @Whitehill_Covs Set Extra_Medical = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '060' BEGIN
		Update @Whitehill_Covs Set Comprehensive = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '070' BEGIN
		Update @Whitehill_Covs Set Collision = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '075' BEGIN
		Update @Whitehill_Covs Set Collission_Deductible_Waiver = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '090' and @company = '42' BEGIN
		Update @Whitehill_Covs Set Combined_Single_Limits = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '090' and @company = '40' BEGIN
		Update @Whitehill_Covs Set Personal_Family_Protection = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '100' BEGIN
		Update @Whitehill_Covs Set Special_Equipment = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '110' BEGIN
		Update @Whitehill_Covs Set Rental_Reimbursement = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '120' BEGIN
		Update @Whitehill_Covs Set Towing = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '170' BEGIN
		Update @Whitehill_Covs Set Arbitration_Agreement_Waiver = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end

IF @covnew = '810' BEGIN
		Update @Whitehill_Covs Set Policy_Fee = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end

IF @covnew = '900' BEGIN
		Update @Whitehill_Covs Set Expense_Constant = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '905' BEGIN
		Update @Whitehill_Covs Set Filing_Fee = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end
IF @covnew = '910' BEGIN
		Update @Whitehill_Covs Set CIGA = (SELECT Top 1 LimitLongDesc from 
			policy.genesis.dbo.all_plmtstab where cov = @covnew and company = @company and lim_code = @limits) 
			end

Fetch acursor into @covnew,@company,@limits
end
close acursor
deallocate acursor

Update @Whitehill_Covs

set Accidental_Death = ISNULL(Accidental_Death,''),
	Arbitration_Agreement_Waiver = ISNULL(Arbitration_Agreement_Waiver,''),
	Auto_Club = ISnull(Auto_Club,''),
	BillingService_Charges = ISNULL(BillingService_Charges, '')  ,
	Bodily_Injury = ISNULL(Bodily_Injury,'')  ,
	Brokers_Fee = ISNULL(Brokers_Fee,'')  ,
	CIGA = ISNULL(CIGA,'')  ,
	Collision = ISNULL(Collision,'')  ,
	Collission_Deductible_Waiver = ISNULL(Collission_Deductible_Waiver,''),
	Combined_FPB = ISNULL(Combined_FPB,'')  ,
	Combined_Single_Limits = ISNULL(Combined_Single_Limits,''),
   Comprehensive = ISNULL(Comprehensive,''),
	Expense_Constant = ISNULL(Expense_Constant,''),
   Extra_Medical = ISNULL(Extra_Medical, ''),
	Filing_Fee = ISNULL(Filing_Fee, ''),
   Funeral = ISNULL(Funeral,''),
   Income_Loss = ISNULL(Income_Loss,''),
   Medical = ISNULL(Medical,''),
   Personal_Family_Protection = ISNULL(Personal_Family_Protection,''),
   Policy_Fee = ISNULL(Policy_Fee,''),
 	Property_Damage = ISNULL(Property_Damage,'')  ,
   Rental_Reimbursement = ISNULL(Rental_Reimbursement,''),
   Special_Equipment = ISNULL(Special_Equipment,''),
   Towing = ISNULL(Towing,''),
   UnderInsured_Motorist_Stacked = isnull(UnderInsured_Motorist_Stacked,''),
   UnderInsured_Motorist_UnStacked = ISNULL(UnderInsured_Motorist_UnStacked,''),
	Uninsured_Motorist_BI = ISNULL(Uninsured_Motorist_BI,''),
	Uninsured_Motorist_PD = ISNULL(Uninsured_Motorist_PD,''), 
	UnInsured_Motorist_Stacked = ISNULL(UnInsured_Motorist_Stacked,''),
   UnInsured_Motorist_UnStacked = ISNULL(UnInsured_Motorist_UnStacked,'')


SELECT Top 1 * from @Whitehill_Covs


GO


Grant Execute On 
	dbo.[ClaimsDocs_XMLGeneration_Coverages] to public
Go	
