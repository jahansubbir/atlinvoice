using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class EmployeeManager
    {
        EmployeeGateway employeeGateway=new EmployeeGateway();

        public string CreateAccount(Employee employee)
        {
            if (employeeGateway.IsEmployeeExist(employee.EmpId)!=null)
            {
                return "Already Exists";
            }
            else
            {
                if (employeeGateway.CreateAccount(employee)>0)
                {
                    return "Account has been created successfully. Please wait for activation";
                }
                return "failed";
            }
        }

        public Employee GetEmployeeByUserId(string empId)
        {
            return employeeGateway.GetEmployeeByUserId(empId);
        }

        public Employee GetEmployeeById(int id)
        {
            return employeeGateway.GetEmployeeById(id);
        }

        public string EditProfile(Employee employee)
        {
            if (employeeGateway.EditEmployee(employee.Id,employee)>0)
            {
                return "Edited";
            }
            return "Failed";
        }

        public int ActivateUser(Activate activate)
        {
            return employeeGateway.ActivateUser(activate);
        
        }

        public List<Employee> GetEmployees()
        {
            return employeeGateway.GetEmployees();
        }
        public List<Employee> GetInactiveEmployees()
        {
            return employeeGateway.GetInactiveEmployees();
        }

        public Employee IsEmployee(string empId)
        {
            return employeeGateway.IsEmployeeExist(empId);
        }

        public int ShowNewUser()
        {
            return employeeGateway.ShowNewUser();
        }
       
    }
    
}