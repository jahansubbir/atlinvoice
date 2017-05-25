using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class DepartmentGateway:Gateway
    {
        public int SaveNewDepartment(Department department)
        {
            Query = "INSERT INTO Department VALUES(@Name)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = department.Name;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int EditDepartment(int deptId, string deptName)
        {
            Query = "UPDATE Department Set Name=@Name"+" WHERE Id='"+deptId+"'";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("Name",SqlDbType.VarChar);
            Command.Parameters["Name"].Value = deptName;
            return ExecuteQuery();

        }
        public int DeleteDepartment(int id)
        {

            Query = "DELETE Department WHERE Id='" + id + "'";
            //int rowAffected;
            Command = new SqlCommand(Query, Connection);
            return ExecuteQuery();

        }


        public Department IsDepartmentExist(string name)
        {
            Query = "SELECT * FROM Department WHERE Name='" + name + "'";
            Command = new SqlCommand(Query, Connection);

            Department department = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                department = new Department();

                department.Id = Convert.ToInt32(Reader["Id"]);
                department.Name = Reader["Name"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return department;
        }
        public Department IsDepartmentExist(int id)
        {
            Query = "SELECT * FROM Department WHERE Id='" + id + "'";
            Command = new SqlCommand(Query, Connection);

            Department department = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                department = new Department();

                department.Id = Convert.ToInt32(Reader["Id"]);
                department.Name = Reader["Name"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return department;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            Query = "SELECT * FROM Department";
            Command = new SqlCommand(Query, Connection);

            Department department = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                department = new Department();

                department.Id = Convert.ToInt32(Reader["Id"]);

                department.Name = Reader["Name"].ToString();
                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();
            return departments;
        }


        public int ExecuteQuery()
        {
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}