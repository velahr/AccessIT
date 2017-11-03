using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace ClaimsDocsClient.secure
{
    public partial class AdminNonDocs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            //declare variables

            try
            {
                //signout
                FormsAuthentication.SignOut();
                //redirect to login page
                FormsAuthentication.RedirectToLoginPage();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }//end : protected void Page_Load(object sender, EventArgs e)
    }//end : public partial class AdminNonDocs : System.Web.UI.Page
}//end : namespace ClaimsDocsClient.secure
