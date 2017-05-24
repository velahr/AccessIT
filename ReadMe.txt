
APPS.Monitor: This is a dll (written in C# .NET) monitors the payment transaction times (in seconds)
	      against set maximum thresholds
====================================================================================================

APPS.Monitor Deployment Procedure
===================================

Follow the instructions in the following order for APPS.Monitor Deployment

=====================================================================================
1. Get the Deployment files from VSS
=====================================================================================
	a) Get the project files from VSS folder, 
           \AccessGeneralNET\WindowsServices\APPS\APPS.Monitor\Deployment\<server environment>.zip
           and place it in C:\Program Files\access\Apps.Monitor folder only.  

	Note: If PaymentWrapper folder doesn't exisit in C Drive, 
              please make a directory C:\Program Files\access\Apps.Monitor and get the <server environment>.zip 

	b) Unzip the <server environment>.zip folder and it should contain the following files.

	   •    Access.WindowServices.APPS.Monitor.exe
	   •	Access.WindowServices.APPS.Monitor.exe.config
	   •	Access.WindowServices.APPS.Monitor.pdb
	   •	Access.WindowServices.APPS.Monitor.vshost.exe
	   •	Access.WindowServices.APPS.Monitor.vshost.exe.config
	   •	post-build.bat
	   •	pre-build.bat

====================================================================================
2) run the pre-build.bat
====================================================================================  
   Note: The pre-build.bat batch program will stop any APPS.Monitor service 
	 running.  


====================================================================================
3) run the post-build.bat
=====================================================================================  
   Note:   The post-build batch program 
           •	will uninstall any current APPS.Monitor service
	   •	Re-install the APPS.Monitor service
	   •	Start the APPS.Monitor service






