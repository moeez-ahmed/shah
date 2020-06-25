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
    public partial class AddScoreForm : Form
    {
        public AddScoreForm()
        {
            InitializeComponent();
        }

        Score score = new Score();
        Course course = new Course();
        Student student = new Student();
        private void AddScoreForm_Load(object sender, EventArgs e)
        {
            //Populate the comboBox with courses Name
                comboBoxCourse.DataSource = course.getAllCourses();
                comboBoxCourse.DisplayMember = "course_label";
                comboBoxCourse.ValueMember = "id";

            //Populate datagrid View with students data (id,first_name, last_name)
            SqlCommand command = new SqlCommand("SELECT id, first_name, last_name FROM student");
            dataGridView1.DataSource = student.getStudents(command);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //Get id of the selected student.
            textBoxStudentID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
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
    }
}
