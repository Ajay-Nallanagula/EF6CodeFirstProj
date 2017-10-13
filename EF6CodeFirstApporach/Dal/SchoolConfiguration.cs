using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace EF6CodeFirstApporach.Dal
{
    public class SchoolConfiguration : DbConfiguration
    {
        
        public SchoolConfiguration()
        {
           IsWindowsAzureDb = ConfigurationManager.AppSettings["IsWindowsAzureDb"];
           SetExcecutionRules();
        }

        public string IsWindowsAzureDb { get; }

        private void SetExcecutionRules()
        {
            //InCase of using Windows Azure Sql DB uncomment below 
            if (IsWindowsAzureDb.Equals("true"))
            {
                SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            }
            else
            {
                SetExecutionStrategy("System.Data.SqlClient", () => new SchoolExecutionStrategy());
            }
        }
    }
}