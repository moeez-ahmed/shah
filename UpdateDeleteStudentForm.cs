using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Course_Management_System
{
    public partial class UpdateDeleteStudentForm : Form
    {
        public UpdateDeleteStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            // Browse image from my computer.
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }

            
        }
        //Create a function to verify Data.
        bool verify()
        {
            if (textBoxFname.Text.Trim() == "" ||
                textBoxLname.Text.Trim() == "" ||
                textBoxPhone.Text.Trim() == "" ||
                textBoxAddress.Text.Trim() == "" ||
                pictureBoxStudentImage.Image == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void buttonEditStudent_Click(object sender, EventArgs e)
        {
            //Update the selected student in database.
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                string fname = textBoxFname.Text;
                string lname = textBoxLname.Text;
                DateTime bdate = dateTimePicker1.Value;
                string phone = textBoxPhone.Text;
                string address = textBoxAddress.Text;
                string gender = "Male";
                if (radioButtonFemale.Checked)
                {
                    gender = "Female";
                }
                MemoryStream pic = new MemoryStream();

                //We need to check the age of the student, the student age must be between 10 and 100
                int born_year = dateTimePicker1.Value.Year;
                int this_year = DateTime.Now.Year;

                if (((this_year - born_year) < 10) || ((this_year - born_year) > 100))
                {
                    MessageBox.Show("The student age must be between 10 - 100.", "Invalid birth year", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (verify())
                {
                    pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);

                    if (student.updateStudent(id, fname, lname, bdate, phone, gender, address, pic))
                    {
                        MessageBox.Show("Student information updated.", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Empty Fields.", "Edit Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid things to update.", "Invalid Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            //Delete the selected id(student) from database. 
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);

                //Show a confirmation message for deletion.
                if (MessageBox.Show("Are you sure you want to delete this student?", "Delete Student", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (student.deleteStudent(id))
                    {
                        MessageBox.Show("Student Deleted", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Clear fields.
                        textBoxID.Text = "";
                        textBoxFname.Text = "";
                        textBoxLname.Text = "";
                        textBoxPhone.Text = "";
                        textBoxAddress.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        pictureBoxStudentImage.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Delete not successful.", "Delete Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Enter a valid student Id", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonFind_Click(object sender, EventArgs e)
        {
            //Search student by ID
            try
            {
                int id = Convert.ToInt32(textBoxID.Text);
                SqlCommand command = new SqlCommand("SELECT * FROM student WHERE id =" + id);

                DataTable table = student.getStudents(command);

                if (table.Rows.Count > 0)
                {
                    textBoxFname.Text = table.Rows[0]["first_name"].ToString();
                    textBoxLname.Text = table.Rows[0]["last_name"].ToString();
                    textBoxPhone.Text = table.Rows[0]["phone"].ToString();
                    textBoxAddress.Text = table.Rows[0]["address"].ToString();

                    dateTimePicker1.Value = (DateTime)table.Rows[0]["birthdate"];

                    //gender

                    if (table.Rows[0]["gender"].ToString() == "Female")
                    {
                        radioButtonFemale.Checked = true;
                    }
                    else
                    {
                        radioButtonMale.Checked = true;
                    }

                    //Image
                    byte[] pic = (byte[])table.Rows[0]["picture"];
                    MemoryStream picture = new MemoryStream(pic);
                    pictureBoxStudentImage.Image = Image.FromStream(picture);
                }
                else
                {
                    MessageBox.Show("Cannot find a student with that ID.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Enter a valid student Id", "Invalid Id", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Allow only numbers for ID.
        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
