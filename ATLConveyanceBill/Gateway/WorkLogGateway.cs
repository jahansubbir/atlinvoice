using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class WorkLogGateway:Gateway
    {
        public void AddParameter(WorkLog log)
        {
            Command.Parameters.Add("Date", SqlDbType.Date);
            Command.Parameters["Date"].Value = log.WorkDate;

            Command.Parameters.Add("ProjectId", SqlDbType.Int);
            Command.Parameters["ProjectId"].Value = log.ProjectId;

            Command.Parameters.Add("SystemId", SqlDbType.Int);
            Command.Parameters["SystemId"].Value = log.WorkSystemId;

            Command.Parameters.Add("WorkTypeId", SqlDbType.Int);
            Command.Parameters["WorkTypeId"].Value = log.WorkTypeId;

            Command.Parameters.Add("Description", SqlDbType.VarChar);
            Command.Parameters["Description"].Value = log.Description;

            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = log.EmployeeId;

            Command.Parameters.Add("WorkLogNo", SqlDbType.VarChar);
            Command.Parameters["WorkLogNo"].Value = log.WorkLogNo;
        }
        public int LogWork(WorkLog log)
        {
            Query = "INSERT INTO WorkLog (Date,ProjectId,SystemId,WorkTypeId,Description,EmployeeId,WorkLogNo) VALUES(@Date,@ProjectId,@SystemId,@WorkTypeId,@Description,@EmployeeId,@WorkLogNo)";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            AddParameter(log);

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int  EditWorkLog(WorkLog workLog)
        {
            Query = "UPDATE WorkLog SET Date=@Date,ProjectId=@ProjectId,SystemId=@SystemId,WorkTypeId=@WorkTypeId,Description=@Description,EmployeeId=@EmployeeId,WorkLogNo=@WorkLogNo WHERE Id=" + workLog.Id;
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            AddParameter(workLog);
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public List<WorkSystem> GetWorkSystems()
        {
            Query = "SELECT * FROM System  ORDER BY Name";
            Command = new SqlCommand(Query, Connection);
            List<WorkSystem> workSystems = new List<WorkSystem>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                WorkSystem workSystem = new WorkSystem()
                {
                    Id = Convert.ToInt32(Reader["Id"]),
                    Name = Reader["Name"].ToString()

                };
                workSystems.Add(workSystem);
            }
            Reader.Close();
            Connection.Close();
            return workSystems;
        }

        public List<WorkType> GetWorkTypes()
        {
            Query = "SELECT * FROM WorkType ORDER BY Type";
            Command = new SqlCommand(Query, Connection);
            List<WorkType> workTypes = new List<WorkType>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                WorkType type = new WorkType()
                {
                    Id = Convert.ToInt32(Reader["Id"]),
                    Type = Reader["Type"].ToString()
                };
                workTypes.Add(type);
            }
            Reader.Close();
            Connection.Close();
            return workTypes;
        }

        public List<WorkLog> GetWorkLogs()
        {
            Query = "SELECT * FROM WorkLog ORDER BY Date";
            Command = new SqlCommand(Query, Connection);
            List<WorkLog> workLogs = new List<WorkLog>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                WorkLog workLog = new WorkLog()
                {
                    Id = Convert.ToInt32(Reader["Id"]),
                    WorkDate = Convert.ToDateTime(Reader["Date"]),
                    ProjectId = Convert.ToInt32(Reader["ProjectId"]),
                    WorkSystemId = Convert.ToInt32(Reader["SystemId"]),
                    WorkTypeId = Convert.ToInt32(Reader["WorkTypeId"]),
                    Description = Reader["Description"].ToString(),
                    EmployeeId=Convert.ToInt32(Reader["EmployeeId"]),
                    WorkLogNo = Reader["WorkLogNo"].ToString()
                };
                workLogs.Add(workLog);
            }
            Reader.Close();
            Connection.Close();
            return workLogs;
        }

        public bool IsExist(string workLogNo)
        {
            Query = "SELECT * FROM WorkLog WHERE WorkLogNo=@WorkLogNo";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("WorkLogNo", SqlDbType.VarChar);
            Command.Parameters["WorkLogNo"].Value = workLogNo;
            bool exist = false;
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                exist = true;
            }Reader.Close();
            Connection.Close();
            return exist;
        }

        public List<WorkLogViewModel> GetLogViewModels()
        {
            Query = "SELECT * FROM ViewWorkLog ORDER BY EmpId,Date";
            Command=new SqlCommand(Query,Connection);
            List<WorkLogViewModel> workLogs=new List<WorkLogViewModel>();
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                WorkLogViewModel workLog = new WorkLogViewModel()
                {
                    EmployeeId = Convert.ToInt32(Reader["Id"]),
                    Name = Reader["Name"].ToString(),
                    EmpId = Reader["EmpId"].ToString(),
                    Date= Convert.ToDateTime(Reader["Date"]),
                    Project = Reader["Project"].ToString(),
                    WorkSystem = Reader["WorkSystem"].ToString(),
                    WorkType = Reader["Type"].ToString(),
                    Description = Reader["Description"].ToString()
                };
                workLogs.Add(workLog);
            }
            Reader.Close();
            Connection.Close();
            return workLogs;

        } 
    }
}