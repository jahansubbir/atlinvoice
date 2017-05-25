using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class LoginGateway:Gateway
    {
        public int LoginToProfile(Login login)
        {
            Query = "INSERT INTO Login VALUES(@EmpId,@Date)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("EmpId", SqlDbType.VarChar);
            Command.Parameters["EmpId"].Value = login.UserId;

            Command.Parameters.Add("Date", SqlDbType.DateTime);
            Command.Parameters["Date"].Value = login.Date;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        

        public Employee AttemptLogin(Login login)
        {
            Query = "SELECT * FROM Employee WHERE EmpId =@EmpId AND PasswordHash=@PasswordHash /*AND Status!=0*/";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("EmpId", SqlDbType.VarChar);
            Command.Parameters["EmpId"].Value = login.UserId;

            Command.Parameters.Add("PasswordHash", SqlDbType.NVarChar);
            Command.Parameters["PasswordHash"].Value = encryptPassword(login.Password);


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
            employee.Status = Convert.ToInt32(Reader["Status"]);
            // employee.UserName = Reader["UserName"].ToString();
            return employee;
            //employee.Password = DecryptPassword(employee.EmpId);
        }

        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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
        private string encryptPassword(string password)
        {
            string hash = null;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = GetMd5Hash(md5Hash, password);

            }
            return hash;
        }
    }
}