using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class LoginManager
    {
        LoginGateway loginGateway=new LoginGateway();
        public string AttemptLogin(Login login)
        {
            if (loginGateway.AttemptLogin(login)!=null)
            {
                if (loginGateway.AttemptLogin(login).Status!=0)
                {
                    loginGateway.LoginToProfile(login);
                    return "Logged in Successfully";
                }
                else
                {
                    return "Your Account Has Not Been Activated Yet";
                }
               
            }
            else
            {
                return "Invalid User Name or Password";
            }
        }

        public string IsUser(Login login)
        {
           
            if (loginGateway.AttemptLogin(login).Status==1)
            {
                return "User";
            }
            else if (loginGateway.AttemptLogin(login).Status==3)
            {
                return "Admin";
            }
            return "Invalid User";

        }
    }
}