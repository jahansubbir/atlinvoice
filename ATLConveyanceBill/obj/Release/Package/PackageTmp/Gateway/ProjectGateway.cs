using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Models;


namespace ATLConveyanceBill.Gateway
{
    public class ProjectGateway:Gateway
    {
        public int SaveNewProject(Project project)
        {
            Query = "INSERT INTO Project VALUES(@Name, @Location)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = project.Name;
            Command.Parameters.Add("Location", SqlDbType.VarChar);
            Command.Parameters["Location"].Value = project.Location;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public Project IsProjectExists(string name)
        {
            Query = "SELECT * FROM Project WHERE Name='" + name + "'";
            Command = new SqlCommand(Query, Connection);

            Project project = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                project = new Project();

                project.Id = Convert.ToInt32(Reader["Id"]);
                project.Name = Reader["Name"].ToString();
                project.Location = Reader["Location"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return project;
        }

        public List<Project> GetProjects()
        {
            List<Project> projects = new List<Project>();
            Query = "SELECT * FROM Project ORDER BY Name";
            Command = new SqlCommand(Query, Connection);

            Project project = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                project = new Project();

                project.Id = Convert.ToInt32(Reader["Id"]);
                project.Name = Reader["Name"].ToString();
                project.Location = Reader["Location"].ToString();
                projects.Add(project);
            }
            Reader.Close();
            Connection.Close();
            return projects;
        }

        public int Edit(int id, Project project)
        {
            Query = "UPDATE Project SET Name=@Name, Location=@Location WHERE Id=" + id;
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Name", SqlDbType.VarChar);
            Command.Parameters["Name"].Value = project.Name;
            Command.Parameters.Add("Location", SqlDbType.VarChar);
            Command.Parameters["Location"].Value = project.Location;
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}