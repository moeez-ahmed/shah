using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Course_Management_System
{
    /*
        - In this class I`ll be creating the connection between my app and the database. 
        - I`ll be using SQL database, and for that I`ll need sql database connection.
         */
    
    class MY_DB
    {
        private SqlConnection con = new SqlConnection("Data Source=DESKTOP-G9DILRJ;Initial Catalog=Course_Management_System;Integrated Security=True");

        //Creating a function to get the connection.

        public SqlConnection getConnection
        {
            get
            {
                return con;
            }
        }

        // Creating a function to open the connection

        public void openConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        // Creating a function to Close the connection

        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
