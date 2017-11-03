Use [ClaimsDocs]
--This script will remove non-production created records from the 
--ClaimsDocs approval queue. (i.e. Removed records where copied from
--the ClaimsDocs UAT & ITQA envrionments.

--Delete associated approval records from tblDocumentApprovalQueue	
Delete 
	tblDocumentApprovalQueue	
From tblDocumentApprovalQueue Where
	tblDocumentApprovalQueue.IUDateTime < '08/09/2010'	


--Delete approval records from 	tblDocumentLog
Delete 
	tblDocumentLog  
From 
	tblDocumentLog 
Where
	tblDocumentLog.IUDateTime < '08/09/2010'