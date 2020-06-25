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
    public partial class ManageCourseForm : Form
    {
        public ManageCourseForm()
        {
            InitializeComponent();
        }

        Course course = new Course();
        int pos;
        private void ManageCourseForm_Load(object sender, EventArgs e)
        {
            reloadListBox();
        }

        //Creating a function to load the list Box with courses. 
        public void reloadListBox()
        {
            listBoxCourses.DataSource = course.getAllCourses();
            listBoxCourses.ValueMember = "id";
            listBoxCourses.DisplayMember = "course_label";

            //Unselect the item from listBox.
            listBoxCourses.SelectedItem = null;

            //Display the total courses
            labelTotalCourses.Text = "Total Courses: " + course.totalCourses();
        }

        //Creating a function to display course data on the index.
        void showData(int index)
        {
            DataRow dr = course.getAllCourses().Rows[index];
            listBoxCourses.SelectedIndex = index;
            textBoxID.Text = dr.ItemArray[0].ToString();
            textBoxLabel.Text = dr.ItemArray[1].ToString();

            numericUpDownHours.Value = int.Parse(dr.ItemArray[2].ToString());
            
            textBoxDescription.Text = dr.ItemArray[3].ToString();
        }

        private void listBoxCourses_Click(object sender, EventArgs e)
        {
            //Display the selected course data.

            pos = listBoxCourses.SelectedIndex;
            showData(pos);
        }

        private void listBoxCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //button first.
        private void buttonFirst_Click(object sender, EventArgs e)
        {
            pos = 0;
            showData(0);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (pos < (course.getAllCourses().Rows.Count - 1))
            {
                pos = pos + 1;
                showData(pos);
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (pos > 0)
            {
                pos = pos - 1;
                showData(pos);
            }
        }

        private void buttonLast_Click(object sender, EventArgs e)
        {
            pos = course.getAllCourses().Rows.Count - 1;
            showData(pos);
        }

        private void buttonAddCourse_Click(object sender, EventArgs e)
        {
            string courseLabel = textBoxLabel.Text;
            int hours = (int)numericUpDownHours.Value;
            string description = textBoxDescription.Text;

            Course course = new Course();

            if (courseLabel.Trim() == "")
            {
                MessageBox.Show("Add a course in order to insert.", "Add course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (course.checkCourseName(courseLabel))
            {
                if (course.insertCourse(courseLabel, hours, description))
                {
                    MessageBox.Show("New course inserted.", "Add course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reloadListBox();
                }
                else
                {
                    MessageBox.Show("Course not inserted.", "Add course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("This course already exists.", "Add course", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEditCourse_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBoxLabel.Text;
                int hrs = (int)numericUpDownHours.Value;
                string desc = textBoxDescription.Text;
                int id = Convert.ToInt32(textBoxID.Text);
                if (name.Trim() != "")
                {
                    if (!course.checkCourseName(name))
                    {
                        if (course.updateCourse(id, name, hrs, desc))
                        {
                            MessageBox.Show("Course Updated.", "Edit course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            reloadListBox();
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

        private void buttonRemoveCourse_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxID.Text);

                Course course = new Course();

                if (MessageBox.Show("Are you sure you wish to remove this course?", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course deleted.", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reloadListBox();
                    }
                    else
                    {
                        MessageBox.Show("Course not deleted.", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid Course ID.", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
