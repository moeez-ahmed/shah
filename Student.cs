using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Course_Management_System
{
    class Student
    {
        //Creating a function to add a new student to the database.

        MY_DB db = new MY_DB();

        public bool insertStudent(string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            SqlCommand command = new SqlCommand("INSERT INTO student (first_name, last_name, birthdate, gender, phone, address, picture) " +
                "VALUES (@fn, @ln, @bdt, @gdr, @phn, @adrs, @pic)", db.getConnection);

            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", SqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", SqlDbType.VarBinary).Value = picture.ToArray();

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.openConnection();
                return false;
            }
        }

        // Creating a function to return a table of Students data from SQL.
        public DataTable getStudents(SqlCommand command)
        {
            command.Connection = db.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        // Creating a function for updating students information 
        public bool updateStudent(int id, string fname, string lname, DateTime bdate, string phone, string gender, string address, MemoryStream picture)
        {
            //UPDATE student SET first_name = @fn, last_name = @ln, birthdate = @bdt, phone = @phn, gender = @gdr, address = @adrs, picture = @pic WHERE id = @ID

            SqlCommand command = new SqlCommand("UPDATE student SET first_name = @fn, last_name = @ln, birthdate = @bdt, phone = @phn, gender = @gdr, address = @adrs, picture = @pic WHERE id = @ID", db.getConnection);

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@bdt", SqlDbType.Date).Value = bdate;
            command.Parameters.Add("@gdr", SqlDbType.VarChar).Value = gender;
            command.Parameters.Add("@phn", SqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@adrs", SqlDbType.VarChar).Value = address;
            command.Parameters.Add("@pic", SqlDbType.VarBinary).Value = picture.ToArray();

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.openConnection();
                return false;
            }
        }

        // Creating a function for deleting the selected student.
        public bool deleteStudent(int id)
        {
            SqlCommand command = new SqlCommand("DELETE FROM student WHERE id = @id", db.getConnection);

            command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.openConnection();
                return false;
            }
        }

        //Creating a function to execute the count queries for statics. 
        public string execCount(string query)
        {
            SqlCommand command = new SqlCommand(query, db.getConnection);

            db.openConnection();
            string count = command.ExecuteScalar().ToString();
            db.closeConnection();

            return count;
        }

        //getting the total students.
        public string totalStudent()
        {
            return execCount("SELECT COUNT(*) FROM student");
        }

        public string totalMaleStudent()
        {
            return execCount("SELECT Count(*) FROM student WHERE gender = 'Male'");
        }

        public string totalFemaleStudent()
        {
            return execCount("SELECT Count(*) FROM student WHERE gender = 'Female'");
        }
    }
}
