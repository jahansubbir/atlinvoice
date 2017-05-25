﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ATLConveyanceBill.Gateway
{
    public class Gateway
    {
        private string connectionString = WebConfigurationManager.ConnectionStrings["ATL"].ConnectionString;
        public string Query { get; set; }
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public SqlDataReader Reader { get; set; }

        public Gateway()
        {
            Connection=new SqlConnection(connectionString);
        }
    }
}