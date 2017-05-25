using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.Gateway
{
    public class ConveyanceGateway : Gateway
    {
        //insert bill
        public int RequestBill(Conveyance conveyance)
        {
            Query = "INSERT INTO Conveyance (EmployeeId,Date,Source,Destination,VehicleBy, ProjectId,Amount,Remarks,Checked,Approved,Paid,WorkLogNo) VALUES(@EmployeeId, @Date, @Source,@Destination,@VehicleBy, @ProjectId,@Amount,@Remarks,@Checked,@Approved,@Paid,@WorkLogNo)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = conveyance.EmployeeId;
            AddParameter(conveyance);
            Command.Parameters.Add("Checked", SqlDbType.VarChar);
            Command.Parameters["Checked"].Value = conveyance.Checked;

            Command.Parameters.Add("Approved", SqlDbType.VarChar);
            Command.Parameters["Approved"].Value = conveyance.Approved;

            Command.Parameters.Add("Paid", SqlDbType.VarChar);
            Command.Parameters["Paid"].Value = conveyance.Paid;

            

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        void AddParameter(Conveyance conveyance)
        {
            Command.Parameters.Add("Date", SqlDbType.Date);
            Command.Parameters["Date"].Value = conveyance.Date;

            Command.Parameters.Add("Source", SqlDbType.VarChar);
            Command.Parameters["Source"].Value = conveyance.From;

            Command.Parameters.Add("Destination", SqlDbType.VarChar);
            Command.Parameters["Destination"].Value = conveyance.To;

            Command.Parameters.Add("VehicleBy", SqlDbType.VarChar);
            Command.Parameters["VehicleBy"].Value = conveyance.VehicleBy;

            Command.Parameters.Add("ProjectId", SqlDbType.Int);
            Command.Parameters["ProjectId"].Value = conveyance.ProjectId;

            Command.Parameters.Add("Amount", SqlDbType.Decimal);
            Command.Parameters["Amount"].Value = conveyance.Amount;

            Command.Parameters.Add("Remarks", SqlDbType.VarChar);
            Command.Parameters["Remarks"].Value = conveyance.Remarks ?? "";

            Command.Parameters.Add("WorkLogNo", SqlDbType.VarChar);
            Command.Parameters["WorkLogNo"].Value = conveyance.WorkLogNo;
        }

        public int EditBill(Conveyance conveyance)
        {
            Query =
                "UPDATE Conveyance SET Date=@Date, Source=@Source, Destination=@Destination,VehicleBy=@VehicleBy, ProjectId= @ProjectId,Amount=@Amount,Remarks=@Remarks,WorkLogNo=@WorkLogNo WHERE Id='" + conveyance.Id + "' AND Checked=0";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            AddParameter(conveyance);
           
            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }


        public Conveyance IsExists(int id)
        {
            Query = "SELECT * FROM Conveyance WHERE Id='" + id + "'";
            Command = new SqlCommand(Query, Connection);

            Conveyance conveyance = null;

            Connection.Open();

            Reader = Command.ExecuteReader();

            if (Reader.HasRows)
            {
                Reader.Read();
                conveyance = new Conveyance();

                BindModel(conveyance);
            }
            Reader.Close();
            Connection.Close();
            return conveyance;
        }
        public List<Conveyance> GetConveyances(int id)
        {
            List<Conveyance> conveyances = new List<Conveyance>();
            Query = "SELECT * FROM Conveyance WHERE EmployeeId='" + id + "' ORDER BY Date";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                while (Reader.Read())
                {
                    Conveyance conveyance = new Conveyance();
                    BindModel(conveyance);
                    conveyances.Add(conveyance);
                }
            }

            Reader.Close();
            Connection.Close();
            return conveyances;
        }

        //bind from database
        public void BindModel(Conveyance conveyance)
        {
            conveyance.Id = Convert.ToInt32(Reader["Id"]);
            conveyance.EmployeeId = Convert.ToInt32(Reader["EmployeeId"]);
            conveyance.Date = Convert.ToDateTime(Reader["Date"]).ToLocalTime();
            conveyance.From = Reader["Source"].ToString();
            conveyance.To = Reader["Destination"].ToString();
            conveyance.VehicleBy = Reader["VehicleBy"].ToString();
            conveyance.ProjectId = Convert.ToInt32(Reader["ProjectId"]);
            conveyance.Amount = Convert.ToInt32(Reader["Amount"]);
            conveyance.Remarks = Reader["Remarks"].ToString();
            conveyance.Checked = Convert.ToBoolean(Reader["Checked"]);
            conveyance.Approved = Convert.ToBoolean(Reader["Approved"]);
            conveyance.Paid = Convert.ToBoolean(Reader["Paid"]);
            conveyance.WorkLogNo = Reader["WorkLogNo"].ToString();
        }

        //get a single authenticate
        public Conveyance GetConveyanceByBillId(int id)
        {
            Query = "SELECT * FROM Conveyance WHERE Id='" + id + "'";
            Command = new SqlCommand(Query, Connection);
            Conveyance conveyance = null;
            Connection.Open();
            Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
                Reader.Read();
                conveyance = new Conveyance();
                BindModel(conveyance);
            }
            Reader.Close();
            Connection.Close();
            return conveyance;
        }

        //check/Approval/pay bill 
        public int AuthenticateBill(int id, Authenticate authenticate)
        {
            Query = "UPDATE Conveyance SET Amount=@Amount, Checked=@Checked, Approved=@Approved, Paid=@Paid WHERE Id='" + authenticate.Id + "'";

            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Amount", SqlDbType.Decimal);
            Command.Parameters["Amount"].Value = authenticate.Amount;

            Command.Parameters.Add("Checked", SqlDbType.Bit);
            Command.Parameters["Checked"].Value = authenticate.Check;

            Command.Parameters.Add("Approved", SqlDbType.Bit);
            Command.Parameters["Approved"].Value = authenticate.Approve;

            Command.Parameters.Add("Paid", SqlDbType.Bit);
            Command.Parameters["Paid"].Value = authenticate.Pay;


            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;

        }

        //get all conveyances in the database
        public List<Conveyance> GetAllConveyances()
        {
            List<Conveyance> conveyances = new List<Conveyance>();
            Query = "SELECT * FROM Conveyance";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Conveyance conveyance = new Conveyance();
                BindModel(conveyance);
                conveyances.Add(conveyance);
            }
            Reader.Close();
            Connection.Close();
            return conveyances;
        }

        //public List<ConveyanceViewModel> GetAllConveyanceByParameter(ConveyanceViewModel conveyanceView)
        //{
        //    List<ConveyanceViewModel> conveyanceViewModels = new List<ConveyanceViewModel>();
        //    Query = "SELECT * FROM ViewConveyanceBill WHERE Date BETWEEN '" + conveyanceView.DateFrom + "' AND '" + conveyanceView.DateTo + "'";
        //    Command = new SqlCommand(Query, Connection);
        //    Connection.Open();
        //    Reader = Command.ExecuteReader();
        //    if (Reader.HasRows)
        //    {
        //        while (Reader.Read())
        //        {
        //            ConveyanceViewModel conveyanceViewModel = new ConveyanceViewModel();
        //            BindViewModel(conveyanceViewModel);
        //            conveyanceViewModels.Add(conveyanceViewModel);
        //        }
        //    }
        //    Reader.Close();
        //    Connection.Close();
        //    return conveyanceViewModels;
        //}

        public List<ConveyanceViewModel> GetConveyanceViewModels()
        {
            List<ConveyanceViewModel> conveyanceViewModels = new List<ConveyanceViewModel>();
            Query = "SELECT * FROM ViewConveyanceBill  Where Date is not null ORDER BY Date";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                ConveyanceViewModel conveyanceViewModel = new ConveyanceViewModel();
                BindViewModel(conveyanceViewModel);
                conveyanceViewModels.Add(conveyanceViewModel);
            }

            Reader.Close();
            Connection.Close();
            return conveyanceViewModels;

        }
        // change of EmployeeId was from Id in the method below
        public void BindViewModel(ConveyanceViewModel conveyanceViewModel)
        {
            // ConveyanceViewModel conveyanceViewModel=new ConveyanceViewModel();
            conveyanceViewModel.EmployeeId = Reader["EmployeeId"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["EmployeeId"]);
            conveyanceViewModel.Id = Reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["Id"]);
            conveyanceViewModel.Name = Reader["Name"].ToString();
            conveyanceViewModel.EmpId = Reader["EmpId"].ToString().ToUpper();
            if (Reader["Date"] == DBNull.Value)
            {
                conveyanceViewModel.Date = "".AsDateTime();
            }
            else
            {
                conveyanceViewModel.Date = Convert.ToDateTime(Reader["Date"]);
            }

            conveyanceViewModel.From = Reader["Source"].ToString();
            conveyanceViewModel.To = Reader["Destination"].ToString();
            conveyanceViewModel.Project = Reader["Project"].ToString();
            conveyanceViewModel.VehicleBy = Reader["VehicleBy"].ToString();
            if (Reader["Amount"] == DBNull.Value)
            {
                conveyanceViewModel.Amount = 00;
            }
            else
            {
                conveyanceViewModel.Amount = Convert.ToDecimal(Reader["Amount"]);
            }
            conveyanceViewModel.Remarks = Reader["Remarks"].ToString();
            if (Reader["Checked"] != DBNull.Value && Convert.ToBoolean(Reader["Checked"]))
            {
                conveyanceViewModel.Checked = "YES";
            }
            else
            {
                conveyanceViewModel.Checked = "NO";
            }
            if (Reader["Approved"] != DBNull.Value && Convert.ToBoolean(Reader["Approved"]))
            {
                conveyanceViewModel.Approved = "YES";
            }
            else
            {
                conveyanceViewModel.Approved = "NO";
            }
            if ( Reader["Paid"] != DBNull.Value && Convert.ToBoolean(Reader["Paid"]))
            {
                conveyanceViewModel.Paid ="YES";
            }
            else
            {
                conveyanceViewModel.Paid ="NO";
            }
            

            //conveyanceViewModels.Add(conveyanceViewModel);
        }

        public List<ConveyanceViewModel> ViewTotalBill()
        {
            List<ConveyanceViewModel> totalBillList = new List<ConveyanceViewModel>();
            Query = "Select Name, EmployeeId, SUM(Amount) AS TotalAmount FROM ViewConveyanceBill GROUP BY Name,EmployeeId";
            // Query = "SELECT * FROM ViewConveyanceBill ORDER BY Date";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                ConveyanceViewModel conveyanceViewModel = new ConveyanceViewModel();
                conveyanceViewModel.Name = Reader["Name"].ToString();
                conveyanceViewModel.EmployeeId = Reader["EmployeeId"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["EmployeeId"]);
                conveyanceViewModel.Id = Reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["Id"]);
               
                conveyanceViewModel.Amount = Reader["TotalAmount"]==DBNull.Value ? 0 : Convert.ToDecimal(Reader["TotalAmount"]);
                totalBillList.Add(conveyanceViewModel);
            }

            Reader.Close();
            Connection.Close();
            return totalBillList;

        }

        public int ApproveBill(Authenticate approve)
        {
            if (approve.EmployeeId!=0)
            {
                Query = "UPDATE Conveyance SET Approved=@Approved WHERE EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Checked=1 AND Approved=0";
            
            }
            else
            {
                Query = "UPDATE Conveyance SET Approved=@Approved WHERE Date Between @Date1 and @Date2 AND Checked=1 AND Approved=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Approved", SqlDbType.Bit);
            Command.Parameters["Approved"].Value = approve.Approve;
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = approve.EmployeeId;


            Command.Parameters.Add("Date1", SqlDbType.Date);
            Command.Parameters["Date1"].Value = approve.From;
            Command.Parameters.Add("Date2", SqlDbType.Date);
            Command.Parameters["Date2"].Value = approve.To;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public int CheckBill(Authenticate approve)
        {
            if (approve.EmployeeId != 0)
            {
                Query = "UPDATE Conveyance SET Checked=@Checked WHERE EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Checked=0";

            }
            else
            {
                Query = "UPDATE Conveyance SET Checked=@Checked WHERE Date Between @Date1 and @Date2 AND Checked=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Checked", SqlDbType.Bit);
            Command.Parameters["Checked"].Value = approve.Check;
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = approve.EmployeeId;


            Command.Parameters.Add("Date1", SqlDbType.Date);
            Command.Parameters["Date1"].Value = approve.From;
            Command.Parameters.Add("Date2", SqlDbType.Date);
            Command.Parameters["Date2"].Value = approve.To;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
        public int PayBill(Authenticate approve)
        {
            if (approve.EmployeeId != 0)
            {
                Query = "UPDATE Conveyance SET Paid=@Paid WHERE EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Approved=1 AND Paid=0";

            }
            else
            {
                Query = "UPDATE Conveyance SET Paid=@Paid WHERE Date Between @Date1 and @Date2 AND Approved=1 AND Paid=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Paid", SqlDbType.Bit);
            Command.Parameters["Paid"].Value = approve.Pay;
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = approve.EmployeeId;


            Command.Parameters.Add("Date1", SqlDbType.Date);
            Command.Parameters["Date1"].Value = approve.From;
            Command.Parameters.Add("Date2", SqlDbType.Date);
            Command.Parameters["Date2"].Value = approve.To;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }

        public List<ConveyanceViewModel> GetSpecificBill(DateTime dateFrom, DateTime dateTo,int employeeId)
        {
            List<ConveyanceViewModel> totalBillList = new List<ConveyanceViewModel>();
            Query = "SELECT * FROM ViewConveyanceBill WHERE Date BETWEEN '"+dateFrom+"' AND '"+dateTo+"' AND EmployeeId='"+employeeId+"'";
            Command=new SqlCommand(Query,Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                ConveyanceViewModel conveyanceViewModel = new ConveyanceViewModel();
                conveyanceViewModel.Date = Reader["Date"] == DBNull.Value ?"".AsDateTime()  : Convert.ToDateTime(Reader["Date"]);
                conveyanceViewModel.Name = Reader["Name"].ToString();
                conveyanceViewModel.EmpId = Reader["EmpId"].ToString();
                conveyanceViewModel.EmployeeId = Reader["EmployeeId"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["EmployeeId"]);
                conveyanceViewModel.Id = Reader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(Reader["Id"]);
                conveyanceViewModel.From = Reader["Source"].ToString();
                conveyanceViewModel.To = Reader["Destination"].ToString();
                conveyanceViewModel.Project = Reader["Project"].ToString();
                conveyanceViewModel.VehicleBy = Reader["VehicleBy"].ToString();
                conveyanceViewModel.Amount = Reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(Reader["Amount"]);
                conveyanceViewModel.Remarks = Reader["Remarks"].ToString();
                if (Reader["Checked"] != DBNull.Value && Convert.ToBoolean(Reader["Checked"]) == true)
                {
                    conveyanceViewModel.Checked = "YES";
                }
                else
                {
                    conveyanceViewModel.Checked = "NO";
                }
                if ( Reader["Approved"] != DBNull.Value && Convert.ToBoolean(Reader["Approved"])==true)
                {
                    conveyanceViewModel.Approved = "YES";
                }
                else
                {
                    conveyanceViewModel.Approved = "NO";
                }
                if (Reader["Paid"] != DBNull.Value && Convert.ToBoolean(Reader["Paid"]) == true)
                {
                    conveyanceViewModel.Paid = "YES";
                }
                else
                {
                    conveyanceViewModel.Paid = "NO";
                }
                //conveyanceViewModel.Paid = Reader["Paid"] != DBNull.Value && Convert.ToBoolean(Reader["Paid"]);
                //conveyanceViewModel.Amount = Reader["TotalAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(Reader["TotalAmount"]);
                totalBillList.Add(conveyanceViewModel);
            }

            Reader.Close();
            Connection.Close();
            return totalBillList;

        }

        public decimal CalculateTotalAmount(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            decimal total=0;
            Query = "SELECT SUM(Amount) AS Total FROM ViewConveyanceBill WHERE Date BETWEEN @FromDate AND @ToDate AND EmployeeId= @EmpId";
            Command=new SqlCommand(Query,Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("FromDate", SqlDbType.DateTime);
            Command.Parameters["FromDate"].Value = dateFrom;
            Command.Parameters.Add("ToDate", SqlDbType.DateTime);
            Command.Parameters["ToDate"].Value = dateTo;
            Command.Parameters.Add("EmpId", SqlDbType.Int);
            Command.Parameters["EmpId"].Value = employeeId;
            Connection.Open();
            Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                total = Convert.ToDecimal(Reader["Total"]);
            }
            else
            {
                total = 0;
            }
            
            Reader.Close();
            Connection.Close();
            return total;

        }

        public int ApproveSelectedBills(Approval approval)
        {
            if (approval.EmployeeId != 0)
            {
                if (approval.Id==null)
                {
                    Query = "UPDATE Conveyance SET Approved=@Approved WHERE  EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Checked=1 AND Approved=0";
 
                }
                else
                {
                    string inSqlCmd = "IN(";
                    for (int i = 0; i < approval.Id.Count; i++)
                    {
                        if (i < (approval.Id.Count - 1))
                        {
                            inSqlCmd += approval.Id[i] + ",";
                        }
                        else
                        {
                            inSqlCmd += approval.Id[i] + ")";
                        }


                    }
                    Query = "UPDATE Conveyance SET Approved=@Approved WHERE Id "+inSqlCmd+" AND Approved=0";
 
                }
            }
            
            else
            {
                Query = "UPDATE Conveyance SET Approved=@Approved WHERE Date Between @Date1 and @Date2 AND Checked=1 AND Approved=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Approved", SqlDbType.Bit);
            Command.Parameters["Approved"].Value = approval.Approve;
            return Authenticate(approval);
        }

        public int CheckSelectedBills(Approval approval)
        {
            if (approval.EmployeeId != 0)
            {
                if (approval.Id == null)
                {
                    Query = "UPDATE Conveyance SET Checked=@Checked WHERE  EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Checked=0";

                }
                else
                {
                    string inSqlCmd = "IN(";
                    for (int i = 0; i < approval.Id.Count; i++)
                    {
                        if (i < (approval.Id.Count - 1))
                        {
                            inSqlCmd += approval.Id[i] + ",";
                        }
                        else
                        {
                            inSqlCmd += approval.Id[i] + ")";
                        }


                    }
                    Query = "UPDATE Conveyance SET Checked=@Checked WHERE Id " + inSqlCmd + " AND Checked=0";

                }
            }

            else
            {
                Query = "UPDATE Conveyance SET Checked=@Checked WHERE Date Between @Date1 and @Date2 AND Checked=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Checked", SqlDbType.Bit);
            Command.Parameters["Checked"].Value = approval.Approve;
            return Authenticate(approval);

        }

        public int PaySelectedBills(Approval approval)
        {
            if (approval.EmployeeId != 0)
            {
                if (approval.Id == null)
                {
                    Query = "UPDATE Conveyance SET Paid=@Paid WHERE  EmployeeId=@EmployeeId AND Date Between @Date1 and @Date2 AND Approved=1 AND Paid=0";

                }
                else
                {
                    string inSqlCmd = "IN(";
                    for (int i = 0; i < approval.Id.Count; i++)
                    {
                        if (i < (approval.Id.Count - 1))
                        {
                            inSqlCmd += approval.Id[i] + ",";
                        }
                        else
                        {
                            inSqlCmd += approval.Id[i] + ")";
                        }


                    }
                    Query = "UPDATE Conveyance SET Paid=@Paid WHERE Id " + inSqlCmd + " AND Approved=1 AND Paid=0";

                }
            }

            else
            {
                Query = "UPDATE Conveyance SET Paid=@Paid WHERE Date Between @Date1 and @Date2 AND Approved=1 AND Paid=0";
            }
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();

            Command.Parameters.Add("Paid", SqlDbType.Bit);
            Command.Parameters["Paid"].Value = approval.Approve;
            return Authenticate(approval);
       
        }


        public int Authenticate(Approval approval)
        {
            Command.Parameters.Add("EmployeeId", SqlDbType.Int);
            Command.Parameters["EmployeeId"].Value = approval.EmployeeId;


            Command.Parameters.Add("Date1", SqlDbType.Date);
            Command.Parameters["Date1"].Value = approval.From;
            Command.Parameters.Add("Date2", SqlDbType.Date);
            Command.Parameters["Date2"].Value = approval.To;

            Connection.Open();
            int rowAffected = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffected;
        }
    }
}