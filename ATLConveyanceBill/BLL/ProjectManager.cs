using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATLConveyanceBill.Gateway;
using ATLConveyanceBill.Models;

namespace ATLConveyanceBill.BLL
{
    public class ProjectManager
    {
        ProjectGateway projectGateway=new ProjectGateway();

        public string CreateProject(Project project)
        {
            if (projectGateway.IsProjectExists(project.Name)==null)
            {
                if (projectGateway.SaveNewProject(project) > 0)
                {
                    return "Saved";
                }
                else
                {
                    return "Failed";
                }
            }
            return "Project Exists";
       
        }

        public List<Project> GetProjects()
        {
            return projectGateway.GetProjects();
        }

        public string Edit(int id, Project project)
        {
            if (projectGateway.Edit(id,project)>0)
            {
                return "Edited";
            }
            else
            {
                return "failed";
            }
        }
    }
}