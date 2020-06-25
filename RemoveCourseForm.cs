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
    public partial class RemoveCourseForm : Form
    {
        public RemoveCourseForm()
        {
            InitializeComponent();
        }

        private void buttonRemoveCourse_Click(object sender, EventArgs e)
        {
            try
            {
                int courseId = Convert.ToInt32(textBoxCourseID.Text);

                Course course = new Course();

                if (MessageBox.Show("Are you sure you wish to remove this course?", "Delete Course", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (course.deleteCourse(courseId))
                    {
                        MessageBox.Show("Course deleted.", "Delete Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
