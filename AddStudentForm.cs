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

namespace Course_Management_System
{
    public partial class AddStudentForm : Form
    {
        public AddStudentForm()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

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

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            //Add new student.
            Student student = new Student();
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
            } else if (verify())
            {
                pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);

                if (student.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New student added.", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Empty Fields.", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
