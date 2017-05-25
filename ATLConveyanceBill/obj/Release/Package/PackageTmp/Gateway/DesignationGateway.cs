using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class DesignationGateway:Gateway
    {
        DepartmentGateway departmentGateway=new DepartmentGateway();
        public int SetDesignation(Designation designation)
        {
            Query = "INSERT INTO Designation VALUES(@Name)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = designation.Name;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        
        }
        public int EditDesignation(int id, string name)
        {
            Query = "UPDATE Designation Set Name=@Name WHERE Id='" + id + "'";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("Name",SqlDbType.VarChar);
            Command.Parameters["Name"].Value = name;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }
        public int DeleteDesignation(int id)
        {

            Query = "DELETE Designation WHERE Id='" + id + "'";
            Command=new SqlCommand(Query,Connection);
            //int rowAffected;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }

        public Designation IsDesignationExist(string name)
        {
            Query = "SELECT * FROM Designation WHERE Name='" + name + "'";
            Command = new SqlCommand(Query, Connection);

            Designation designation = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                designation = new Designation();

                designation.Id = Convert.ToInt32(Reader["Id"]);
                designation.Name = Reader["Name"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return designation;
        
        }
        public List<Designation> GetDesignations()
        {

            Query = "SELECT * FROM Designation";
            Command = new SqlCommand(Query, Connection);
            List<Designation> designations = new List<Designation>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Designation designation = new Designation();
                designation.Id = Convert.ToInt32(Reader["Id"]);
                designation.Name = Reader["Name"].ToString();
                designations.Add(designation);

            }
            Reader.Close();
            Connection.Close();
            return designations;
        }
       
       
    }
}