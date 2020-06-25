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

namespace Course_Management_System
{
    public partial class ManageScoreForm : Form
    {
        public ManageScoreForm()
        {
            InitializeComponent();
        }

        Score score = new Score();
        Student student = new Student();
        Course course = new Course();
        string data = "score";

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            //Populate the comboBox with courses
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "course_label";
            comboBoxCourse.ValueMember = "id";

            //Populate the dgv with student score. 

            dataGridView1.DataSource = score.getStudentsScore();

        }

        private void buttonShowStudents_Click(object sender, EventArgs e)
        {
            data = "student";
            SqlCommand command = new SqlCommand("SELECT id, first_name, last_name, birthdate, picture FROM student");
            dataGridView1.DataSource = student.getStudents(command);
        }

        private void buttonShowScores_Click(object sender, EventArgs e)
        {
            data = "score";
            dataGridView1.DataSource = score.getStudentsScore();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            getDataFromDatagridView();
        }

        public void getDataFromDatagridView()
        {
            if (data == "student")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            }
            else if (data == "score")
            {
                textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBoxCourse.SelectedValue = dataGridView1.CurrentRow.Cells[3].Value;
            }
        }

        private void buttonAddScore_Click(object sender, EventArgs e)
        {
            try
            {
                //Add a new score.
                int studentId = Convert.ToInt32(textBoxStudentID.Text);
                int courseId = Convert.ToInt32(comboBoxCourse.SelectedValue);
                int scoreValue = Convert.ToInt32(textBoxScore.Text);
                string description = textBoxDescription.Text;

                if (!score.studentScoreExists(studentId, courseId))
                {
                    if (score.insertScore(studentId, courseId, scoreValue, description))
                    {
                        MessageBox.Show("Student score inserted successfully.", "Add score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student score not inserted successfully.", "Add score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This student is already enrolled in this course and have score for this particular course.", "Add score", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveScore_Click(object sender, EventArgs e)
        {
            //Remove the selected course. 
            try
            {
                int studentId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                int courseId = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

                if (MessageBox.Show("Do you wish to delete this score?", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(studentId, courseId))
                    {
                        MessageBox.Show("Score deleted.", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = score.getStudentsScore();
                    }
                    else
                    {
                        MessageBox.Show("Score not deleted.", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAvgScore_Click(object sender, EventArgs e)
        {
            avgScoreByCourseForm avgScrByCrsF = new avgScoreByCourseForm();
            avgScrByCrsF.Show(this);
        }
    }
}
