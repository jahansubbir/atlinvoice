using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class WorkLogManager
    {
        WorkLogGateway workLogGateway =new WorkLogGateway();

        public List<WorkSystem> GetWorkSystems()
        {
            return workLogGateway.GetWorkSystems();
        }

        public string EditWorkLog(WorkLog workLog)
        {
            if (workLogGateway.EditWorkLog(workLog)>0)
            {
                return "Edited Successfully";
            }
            else
            {
                return "Failed";
            }
        }

        public List<WorkType> GetWorkTypes()
        {
            return workLogGateway.GetWorkTypes();
        }

        public string LogWork(WorkLog log)
        {
            if (!workLogGateway.IsExist(log.WorkLogNo))
            {
                if (workLogGateway.LogWork(log) > 0)
                {
                    return "Work Log has been stored";
                }
                else
                {
                    return "Failed Attempt";
                }
                
            }
            else
            {
                return "Work Log Exist";
            }
        }

        public List<WorkLog> GetWorkLogs()
        {
            return workLogGateway.GetWorkLogs();
        }

        public List<WorkLogViewModel> GetWorkLogViewModels()
        {
            return workLogGateway.GetLogViewModels();
        } 
    }
}