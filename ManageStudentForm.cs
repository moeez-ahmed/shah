using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Management_System
{
    public partial class ManageStudentForm : Form
    {
        public ManageStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void ManageStudentForm_Load(object sender, EventArgs e)
        {
            //Populating the datagridView with students data.
            fillDgv(new SqlCommand("SELECT * FROM student"));
        }

        //Creating a function to populate the datagridView.

        public void fillDgv(SqlCommand command)
        {
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.DataSource = student.getStudents(command);

            //Column 7 is the image column index.
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

            dataGridView1.AllowUserToAddRows = false;

            //Show the total students depending on dgv rows.
            labelTotalStudents.Text = "Total Students: " + dataGridView1.Rows.Count;
        }

        //Display student data on datagridview click.
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                radioButtonFemale.Checked = true;
            }
            else
            {
                radioButtonMale.Checked = true;
            }

            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            // The image
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            pictureBoxStudentImage.Image = Image.FromStream(picture);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            //Clear all fields. 
            textBoxID.Text = "";
            textBoxFname.Text = "";
            textBoxLname.Text = "";
            textBoxPhone.Text = "";
            textBoxAddress.Text = "";
            radioButtonMale.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            pictureBoxStudentImage.Image = null;
        }
        //Search and display students in datagridview. 
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM student WHERE CONCAT(first_name, last_name, gender, phone, address) LIKE '%" + textBoxSearch.Text + "%'";
            SqlCommand command = new SqlCommand(query);
            fillDgv(command);
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            // Browse image from my computer.
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudentImage.Image = Image.FromFile(opf.FileName);
            }
        }
        //Save the image in my computer. :) 
        private void buttonDownload_Click(object sender, EventArgs e)
        {
            SaveFileDialog svf = new SaveFileDialog();
            //Set the file name and save with this name ^.^ 
            svf.FileName = "Student_"+textBoxID.Text;

            //Check if the pictureBox is empty.
            if (pictureBoxStudentImage.Image == null)
            {
                MessageBox.Show("No image in the pictureBox to download.", "Image");
            }
            else if (svf.ShowDialog() == DialogResult.OK)
            {
                pictureBoxStudentImage.Image.Save(svf.FileName + ("." + ImageFormat.Jpeg.ToString()));
            }
        }
        //Add a new student.
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
            }
            else if (verify())
            {
                pictureBoxStudentImage.Image.Save(pic, pictureBoxStudentImage.Image.RawFormat);

                if (student.insertStudent(fname, lname, bdate, phone, gender, address, pic))
                {
                    MessageBox.Show("New student added.", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillDgv(new SqlCommand("SELECT * FROM student"));
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
        //Edit the selected student.
        private void buttonEdit_Click(object sender, EventArgs e)
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
                        fillDgv(new SqlCommand("SELECT * FROM student"));
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
        //Remove the selected student.
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
                        fillDgv(new SqlCommand("SELECT * FROM student"));

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
    }
}
