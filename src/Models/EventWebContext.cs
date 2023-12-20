using MySqlConnector;
using System;
using System.Collections.Generic;

namespace Shubak_Website.Models
{
    public class EventWebContext
    {
        public string ConnectionString { get; set; }

        public EventWebContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}