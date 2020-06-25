using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Course_Management_System
{
    class Score
    {
        MY_DB db = new MY_DB();

        //Create a function to insert a new score. 
        
        public bool insertScore(int studentId, int courseId, int score, string description)
        {
            //Query = INSERT INTO score(studentId, courseId, score, description) VALUES(@SID, @CID, @scr, @desc)
            SqlCommand command = new SqlCommand("INSERT INTO score(studentId, courseId, score, description) VALUES(@SID, @CID, @scr, @desc)", db.getConnection);

            command.Parameters.Add("@SID", SqlDbType.Int).Value = studentId;
            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;
            command.Parameters.Add("@scr", SqlDbType.Int).Value = score;
            command.Parameters.Add("@desc", SqlDbType.VarChar).Value = description;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

        //Creating a function if a score is already assigned to this student in a particular course. 
        public bool studentScoreExists(int studentId, int courseId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM score WHERE studentId = @SID AND courseId = @CID", db.getConnection);

            command.Parameters.Add("@SID", SqlDbType.Int).Value = studentId;
            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            if (table.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Creating a function to get students score. 
        public DataTable getStudentsScore()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.studentId, student.first_name, student.last_name, score.courseId, course.course_label, score.score " +
                                   "FROM student INNER JOIN score ON student.id = score.studentId INNER JOIN course ON score.courseId = course.id");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        public bool deleteScore(int studentId, int courseId)
        {
            SqlCommand command = new SqlCommand("DELETE FROM score WHERE studentId = @SID AND courseId = @CID", db.getConnection);

            command.Parameters.Add("@SID", SqlDbType.Int).Value = studentId;
            command.Parameters.Add("@CID", SqlDbType.Int).Value = courseId;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                db.closeConnection();
                return true;
            }
            else
            {
                db.closeConnection();
                return false;
            }
        }

        //Creating a function to get the average score by course. 
        public DataTable avgScoreByCourse()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT course.course_label, AVG(score.score) as 'Average_Score' FROM course,score WHERE course.id = score.courseId GROUP BY course.course_label");

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //Getting course Scores. 
        public DataTable getCourseScores(int courseId)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.studentId, student.first_name, student.last_name, score.courseId, course.course_label, score.score" +
" FROM student INNER JOIN score ON student.id = score.studentId INNER JOIN course ON score.courseId = course.id" +
" WHERE score.courseId ="+courseId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }

        //Getting students Scores. 
        public DataTable getStudentScores(int studentId)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = db.getConnection;
            command.CommandText = ("SELECT score.studentId, student.first_name, student.last_name, score.courseId, course.course_label, score.score" +
" FROM student INNER JOIN score ON student.id = score.studentId INNER JOIN course ON score.courseId = course.id" +
" WHERE score.studentId =" + studentId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
