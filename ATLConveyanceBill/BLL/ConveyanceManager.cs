using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class ConveyanceManager
    {
        ConveyanceGateway conveyanceGateway = new ConveyanceGateway();
        WorkLogGateway workLogGateway=new WorkLogGateway();
        public string RequestBill(Conveyance conveyance)
        {
            if (conveyanceGateway.RequestBill(conveyance)>0)
            {
                return "Bill requested";
            }
            return "Failed";
        }

        public List<Conveyance> GetConveyances(int id)
        {
            return conveyanceGateway.GetConveyances(id);
        }

        public string AuthenticateBill(int id, Authenticate authenticate)
        {
            if (conveyanceGateway.AuthenticateBill(id, authenticate)>0)
            {
                if (authenticate.Check)
                {
                    return "Checked";
                }
                else if (authenticate.Approve)
                {
                    return "Approved";
                }
                else if (authenticate.Pay)

                {
                    return "Paid";
                }
                
            }
            return "failed";

        }

        public List<Conveyance> GetAllConveyances()
        {
            return conveyanceGateway.GetAllConveyances();
        }

        public Conveyance GetConveyanceByBillId(int id)
        {
            return conveyanceGateway.GetConveyanceByBillId(id);
        }

        //public List<ConveyanceViewModel> GetAllConveyanceViewModels(ConveyanceViewModel conveyanceView)
        //{
        //    return conveyanceGateway.GetAllConveyanceByParameter(conveyanceView);
        //}

        public List<ConveyanceViewModel> GetAllConveyanceViewModels()
        {
            return conveyanceGateway.GetConveyanceViewModels();
        }

        public List<ConveyanceViewModel> ViewTotalBill()
        {
            return conveyanceGateway.ViewTotalBill();
        }

        public string ApproveBill(Authenticate authenticate)
        {
            if (conveyanceGateway.ApproveBill(authenticate)>0)
            {
                return "Approved";
            }
            else
            {
                return "Few Bills Are Not Checked Yet";
            }
        }
        public string CheckBill(Authenticate authenticate)
        {
            if (conveyanceGateway.CheckBill(authenticate) > 0)
            {
                return "Checked";
            }
            else
            {
                return "Failed";
            }
        }
        public string PayBill(Authenticate authenticate)
        {
            if (conveyanceGateway.PayBill(authenticate) > 0)
            {
                return "Paid";
            }
            else
            {
                return "Few Bills Are Not Approved Yet";
            }
        }

        public Conveyance IsExist(int id)
        {
            return conveyanceGateway.IsExists(id);
        }

        public List<ConveyanceViewModel> GetSpecificBill(DateTime dateFrom, DateTime dateTo, int employeeId)
        {

            return conveyanceGateway.GetSpecificBill(dateFrom,dateTo,employeeId);
        }

        public decimal CalculateTotalAmount(DateTime dateFrom, DateTime dateTo, int employeeId)
        {
            return conveyanceGateway.CalculateTotalAmount(dateFrom, dateTo, employeeId);
        
        }

        public string EditBill(Conveyance conveyance,WorkLog log)
        {
            if (workLogGateway.EditWorkLog(log)>0)
            {
                if (conveyanceGateway.EditBill(conveyance) > 0)
                {
                    return "Edited";
                }
            }
            
            return "Failed";
        }

        public string ApproveSelectedBills(Approval approval)
        {
            if (conveyanceGateway.ApproveSelectedBills(approval)>0)
            {
                return "Approved";
            }
            return "Failed";
        }

        public string CheckSelectedBills(Approval approval)
        {
            if (conveyanceGateway.CheckSelectedBills(approval)>0)
            {
                return "Checked";
            }
            return "Failed";
        }

        public string PaySelectedBills(Approval approval)
        {

            if (conveyanceGateway.PaySelectedBills(approval) > 0)
            {
                return "Paid";
            }
            return "Failed";
        }
    }
}