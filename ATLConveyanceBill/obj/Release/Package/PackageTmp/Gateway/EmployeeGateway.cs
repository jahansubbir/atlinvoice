using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class EmployeeGateway : Gateway
    {
        DepartmentGateway departmentGateway=new DepartmentGateway();
        public int CreateAccount(Employee employee)
        {
            Query = "INSERT INTO Employee VALUES(@Name,@Address,@ContactNo,@Email,@EmpId,@DesignationId,@DepartmentId,@PasswordHash,@Status)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            AddParameter(employee);

            Command.Parameters.Add("Status", SqlDbType.Int);
            Command.Parameters["Status"].Value = employee.Status;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public Employee IsEmployeeExist(string empId)
        {
            Query = "SELECT * FROM Employee WHERE EmpId='" + empId + "'";
            Command = new SqlCommand(Query, Connection);
            //bool exist = false;
            Employee employee = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                employee=new Employee();
                RetrieveFromDatabase(employee);
                //exist = true;

            }
            Reader.Close();
            Connection.Close();
            return employee;
        }

        public List<Employee> GetEmployees()
        {
            
            List<Employee> Employees=new List<Employee>();
                
            Query = "SELECT * FROM Employee ORDER BY EmpId";
            Command = new SqlCommand(Query, Connection);
            Employee employee = null;
             
            Connection.Open();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                employee=new Employee();
                RetrieveFromDatabase(employee);
                Employees.Add(employee);
            }
            Reader.Close();
            Connection.Close();
            return Employees;

        }

        public List<Employee> GetInactiveEmployees()
        {

            List<Employee> Employees = new List<Employee>();

            Query = "SELECT * FROM Employee WHERE Status=0";
            Command = new SqlCommand(Query, Connection);
            Employee employee = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                employee = new Employee();
                RetrieveFromDatabase(employee);
                Employees.Add(employee);
            }
            Reader.Close();
            Connection.Close();
            return Employees;

        }

        public Employee GetEmployeeById(int id)
        {
            Query = "SELECT * FROM Employee WHERE Id=" + id;
            return GetAnEmployee();
        }
        public Employee GetEmployeeByUserId(string empId)
        {
            Query= "SELECT * FROM Employee WHERE EmpId='"+empId+"'";
            return GetAnEmployee();
        }

        private Employee GetAnEmployee()
        {
            Command = new SqlCommand(Query, Connection);
            Employee employee = null;
            Connection.Open();

            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                Reader.Read();
                employee=new Employee();
                RetrieveFromDatabase(employee);
            }
            Reader.Close();
            Connection.Close();
            return employee;
        }
       public int EditEmployee(int id, Employee employee)
       {
           Query ="UPDATE Employee SET Name=@Name, Address=@Address,ContactNo=@ContactNo, Email=@Email, EmpId=@EmpId,DesignationId=@DesignationId, DepartmentId=@DepartmentId WHERE Id='"+id+"'";
           Command=new SqlCommand(Query,Connection);
           Command.Parameters.Clear();
           AddParameter(employee);
           Connection.Open();
           int rowAffected = Command.ExecuteNonQuery();
           Connection.Close();
           return rowAffected;
       }

        public Employee RetrieveFromDatabase(Employee employee)
        {

            employee.Id = Convert.ToInt32(Reader["Id"]);
            employee.EmpId = Reader["EmpId"].ToString();
            employee.Name = Reader["Name"].ToString();
            employee.Address = Reader["Address"].ToString();
            employee.ContactNo = Reader["ContactNo"].ToString();
            employee.Email = Reader["Email"].ToString();
            employee.DesignationId = Convert.ToInt32(Reader["DesignationId"]);
            employee.DepartmentId = Convert.ToInt32(Reader["DepartmentId"]);
           // employee.UserName = Reader["UserName"].ToString();
            return employee;
            //employee.Password = DecryptPassword(employee.EmpId);
        }

        private string encryptPassword(string password)
        {
            string hash = null;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);
               
            }
            return hash;
        }
        //GetMd5Hash
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        //
        //public string DecryptPassword(string EmpId)
        //{
        //    Query = "SELECT PasswordHash from Employee WHERE EmpId='"+EmpId+"'";
        //    Command = new SqlCommand(Query, Connection);
        //    Connection.Open();
        //    string password=null;
        //    Reader = Command.ExecuteReader();
        //    if (Reader.HasRows)
        //    {
        //        Reader.Read();
        //        password = Reader["PasswordHash"].ToString();
        //    }
        //    Reader.Close();
        //    Connection.Close();
        //    return password;
        //}
        private void AddParameter(Employee employee)
        {
            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = employee.Name;

            Command.Parameters.Add("Address", SqlDbType.VarChar);
            Command.Parameters["Address"].Value = employee.Address;

            Command.Parameters.Add("ContactNo", SqlDbType.VarChar);
            Command.Parameters["ContactNo"].Value = employee.ContactNo;

            Command.Parameters.Add("Email", SqlDbType.VarChar);
            if (employee.Email == null)
            {
                Command.Parameters["Email"].Value = " ";
            }
            else
            {
                Command.Parameters["Email"].Value = employee.Email;
            }

            Command.Parameters.Add("EmpId", SqlDbType.VarChar);
            Command.Parameters["EmpId"].Value = employee.EmpId;

            Command.Parameters.Add("DesignationId", SqlDbType.Int);
            Command.Parameters["DesignationId"].Value = employee.DesignationId;

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = employee.DepartmentId;


            Command.Parameters.Add("PasswordHash", SqlDbType.NVarChar);
            Command.Parameters["PasswordHash"].Value = encryptPassword(employee.Password);
        }

        public int ShowNewUser()
        {
            int count = 0;
            Query = "SELECT COUNT(Name) AS UnActivated FROM Employee WHERE Status=0";
            Command=new SqlCommand(Query,Connection);
            
            Connection.Open();
            count = (int) Command.ExecuteScalar();
            Connection.Close();
            return count;
        }

        public int ActivateUser(Activate activate)
        {
            string inSqlCmd = "IN(";
            for (int i = 0; i < activate.Id.Count; i++)
            {
                if (i<(activate.Id.Count-1))
                {
                    inSqlCmd += activate.Id[i] + ",";
                }
                else
                {
                    inSqlCmd += activate.Id[i] + ")";
                }
            }
            Query = "UPDATE Employee SET Status='"+activate.Status+"' WHERE Id "+inSqlCmd;
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

      
}
}