using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Course_Management_System
{
    public partial class StudentListForm : Form
    {
        public StudentListForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        private void StudentListForm_Load(object sender, EventArgs e)
        {
            //Populating the datagridview with students data.
            SqlCommand command = new SqlCommand("SELECT * FROM student");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.DataSource = student.getStudents(command);
            //Column 7 is the image column index.
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            // Display the selected student in a new form for edit/remove. 
            UpdateDeleteStudentForm updateDeleteStdF = new UpdateDeleteStudentForm();

            updateDeleteStdF.textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            updateDeleteStdF.textBoxFname.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            updateDeleteStdF.textBoxLname.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            updateDeleteStdF.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[3].Value;

            if (dataGridView1.CurrentRow.Cells[4].Value.ToString() == "Female")
            {
                updateDeleteStdF.radioButtonFemale.Checked = true;
            }

            updateDeleteStdF.textBoxPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            updateDeleteStdF.textBoxAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            // The image
            byte[] pic;
            pic = (byte[])dataGridView1.CurrentRow.Cells[7].Value;
            MemoryStream picture = new MemoryStream(pic);
            updateDeleteStdF.pictureBoxStudentImage.Image = Image.FromStream(picture);
            updateDeleteStdF.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Refresh the data grid view data. 
            //Populating the datagridview with students data.
            SqlCommand command = new SqlCommand("SELECT * FROM student");
            dataGridView1.ReadOnly = true;
            DataGridViewImageColumn picCol = new DataGridViewImageColumn();
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.DataSource = student.getStudents(command);
            //Column 7 is the image column index.
            picCol = (DataGridViewImageColumn)dataGridView1.Columns[7];
            picCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
