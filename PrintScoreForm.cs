using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Course_Management_System
{
    public partial class PrintScoreForm : Form
    {
        public PrintScoreForm()
        {
            InitializeComponent();
        }

        Score score = new Score();
        Course course = new Course();
        Student student = new Student();
        private void PrintScoreForm_Load(object sender, EventArgs e)
        {
            dataGridViewScores.DataSource = student.getStudents(new SqlCommand("SELECT id, first_name, last_name FROM student"));

            dataGridViewStudentsScore.DataSource = score.getStudentsScore();

            listBoxCourses.DataSource = course.getAllCourses();
            listBoxCourses.DisplayMember = "course_label";
            listBoxCourses.ValueMember = "id";
        }

        //When we select a course from the listBox then all scores assigned to the course will be displayed. 
        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            dataGridViewStudentsScore.DataSource = score.getCourseScores(int.Parse(listBoxCourses.SelectedValue.ToString()));


        }

        private void dataGridViewScores_Click(object sender, EventArgs e)
        {
            dataGridViewStudentsScore.DataSource = score.getStudentScores(int.Parse(dataGridViewScores.CurrentRow.Cells[0].Value.ToString()));
        }

        private void labelReset_Click(object sender, EventArgs e)
        {
            dataGridViewStudentsScore.DataSource = score.getStudentsScore();
        }


        //Print scores data from datagridview to text file. 

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // Path of our file.The file name would be Score_list.txt..
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Score_list.txt";

            using (var writer = new StreamWriter(path))
            {
                //Checking if the file exists.
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                //Rows
                for (int i = 0; i < dataGridViewStudentsScore.Rows.Count; i++)
                {
                    //Columns
                    for (int j = 0; j < dataGridViewStudentsScore.Columns.Count; j++)
                    {
                        writer.Write("\t" + dataGridViewStudentsScore.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                    }
                    //Making a new line
                    writer.WriteLine("");

                    //Making a Seperation
                    writer.WriteLine("===================================================================================================================================================");
                }


                writer.Close();
                MessageBox.Show("Date exported successfully.");
            }
        }
    }
}
