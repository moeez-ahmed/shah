using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Management_System
{
    public partial class EditCourseForm : Form
    {
        public EditCourseForm()
        {
            InitializeComponent();
        }

        Course course = new Course();

        private void EditCourseForm_Load(object sender, EventArgs e)
        {
            //Populate the combobox with courses. 
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "course_label";
            comboBoxCourse.ValueMember = "id";

            //Set the selected combo item to nothing.
            comboBoxCourse.SelectedItem = null;
        }

        private void buttonEditCourse_Click(object sender, EventArgs e)
        {
            //Updating the selected course.
            try
            {
                string name = textBoxLabel.Text;
                int hrs = (int)numericUpDownHours.Value;
                string desc = textBoxDescription.Text;
                int id = (int)comboBoxCourse.SelectedValue;
                if (name.Trim() != "")
                {
                    if (!course.checkCourseName(name))
                    {
                        if (course.updateCourse(id, name, hrs, desc))
                        {
                            MessageBox.Show("Course Updated.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            fillCombo(comboBoxCourse.SelectedIndex);
                        }
                        else
                        {
                            MessageBox.Show("Course not Updated.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This course already exists.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Enter the course name.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Course Selected.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Creating a function to populate the comboBox
        public void fillCombo(int index)
        {
            //Index is the comboBox item index.
            comboBoxCourse.DataSource = course.getAllCourses();
            comboBoxCourse.DisplayMember = "course_label";
            comboBoxCourse.ValueMember = "id";

            comboBoxCourse.SelectedIndex = index;
        }

        private void comboBoxCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display the selected course data.
            try
            {
                int id = Convert.ToInt32(comboBoxCourse.SelectedValue);
                DataTable table = new DataTable();
                table = course.getCourseById(id);
                textBoxLabel.Text = table.Rows[0][1].ToString();
                numericUpDownHours.Value = Int32.Parse(table.Rows[0][2].ToString());
                textBoxDescription.Text = table.Rows[0][3].ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
