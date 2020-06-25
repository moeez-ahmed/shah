using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Course_Management_System
{
    class Course
    {
        MY_DB mydb = new MY_DB();

        //Creating a function to insert course.

        public bool insertCourse(string courseName, int hoursNumber, string description)
        {
            SqlCommand command = new SqlCommand("INSERT INTO course(course_label, hours_number, description) VALUES(@name, @hrs, @desc)", mydb.getConnection);

            command.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", SqlDbType.Int).Value = hoursNumber;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = description;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        //Creating a function to check if the course name already exists in the database. 
        public bool checkCourseName(string courseName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE course_label = @cName", mydb.getConnection);

            command.Parameters.Add("@cName", SqlDbType.VarChar).Value = courseName;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                //This course name already exists.
                mydb.closeConnection();
                return false;
            }
            else
            {
                mydb.closeConnection();
                return true;
            }
        }

        //Function to remove a course. 
        public bool deleteCourse(int courseId)
        {
            SqlCommand command = new SqlCommand("DELETE FROM course WHERE id = @CID", mydb.getConnection);

            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        //Creating a function to get all courses.
        public DataTable getAllCourses()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course", mydb.getConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //Creating a function to get a course by Id.
        public DataTable getCourseById(int courseId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM course WHERE id = @CID", mydb.getConnection);

            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            DataTable table = new DataTable();

            adapter.Fill(table);

            return table;
        }

        //Creating a function to edit the selected course.
        public bool updateCourse(int courseId, string courseName, int hoursNumber, string description)
        {
            SqlCommand command = new SqlCommand("UPDATE course SET course_label = @name ,hours_number = @hrs ,description = @desc WHERE id = @CID", mydb.getConnection);

            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;
            command.Parameters.Add("@name", SqlDbType.VarChar).Value = courseName;
            command.Parameters.Add("@hrs", SqlDbType.Int).Value = hoursNumber;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = description;

            mydb.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        //Creating a function to execute the count queries for ManageCourse Form. 
        public string execCount(string query)
        {
            SqlCommand command = new SqlCommand(query, mydb.getConnection);

            mydb.openConnection();
            string count = command.ExecuteScalar().ToString();
            mydb.closeConnection();

            return count;
        }

        //getting the total Courses.
        public string totalCourses()
        {
            return execCount("SELECT COUNT(*) FROM course");
        }
    }
}
