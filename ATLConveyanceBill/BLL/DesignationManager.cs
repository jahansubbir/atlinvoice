using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class DesignationManager
    {
        DesignationGateway designationGateway = new DesignationGateway();

        public string CreateDesignation(Designation designation)
        {
            if (designationGateway.IsDesignationExist(designation.Name)==null)
            {
                if (designationGateway.SetDesignation(designation)>0)
                {
                    return "Created";
                }
            }
            return "Already Exist";
        }

        public List<Designation> GetDesignations()
        {
            return designationGateway.GetDesignations();
        }

        public string EditDesignation(Designation designation)
        {
            if (designationGateway.EditDesignation(designation.Id, designation.Name)>0)
            {
                return "Edited Successfully";
            }
            return "Failed";

       
      }

        public string Delete(Designation designation)
        {
            if (designationGateway.DeleteDesignation(designation.Id)>0)
            {
                return "Deleted";
            }
            else
            {
                return "Failed";
            }
            
            //throw new NotImplementedException();
        }
    }
}