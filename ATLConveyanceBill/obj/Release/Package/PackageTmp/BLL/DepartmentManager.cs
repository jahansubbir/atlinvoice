using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway=new DepartmentGateway();

        public string CreateDepartment(Department department)
        {
            if (departmentGateway.IsDepartmentExist(department.Name)!=null)
            {
                return "Department Exists";
            }
            else
            {
                if (departmentGateway.SaveNewDepartment(department)>0)
                {
                    return "A new department Named " + department.Name + " has been created";
                }
                return "Failed";
            }
        }

        public string EditDepartment(Department department)
        {
            if (departmentGateway.EditDepartment(department.Id, department.Name)>0)
            {
                return "Edited Successfully";
            }
            return "Failed";
        }

        public string DeleteDepartment(int id)
        {
            if (departmentGateway.DeleteDepartment(id)>0)
            {
                return "Deleted";
            }
            return "Failed";
        }

        public List<Department> GetDepartments()
        {
            return departmentGateway.GetDepartments();
        }

        public Department IsExist(int id)
        {
            return departmentGateway.IsDepartmentExist(id);
        
        }
    }
}