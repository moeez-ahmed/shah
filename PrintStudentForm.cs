using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Management_System
{
    public partial class PrintStudentForm : Form
    {
        public PrintStudentForm()
        {
            InitializeComponent();
        }

        Student student = new Student();

        

        private void PrintStudentForm_Load(object sender, EventArgs e)
        {
            fillDgv(new SqlCommand("SELECT * FROM student"));

            if (radioButtonNo.Checked)
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

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
        }

        private void radioButtonNo_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
        }

        private void radioButtonYes_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            //Display data on data grid view depending on what user have selected.

            SqlCommand command;
            string query;

            //Check if radiobutton yes is checked (meaning user wants to use a date range :) )

            if (radioButtonYes.Checked)
            {
                //get the date values first. 
                string date1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string date2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");

                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM student WHERE birthdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM student WHERE birthdate BETWEEN '" + date1 + "' AND '" + date2 + "' AND gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM student WHERE birthdate BETWEEN '" + date1 + "' AND '" + date2 + "'";
                }

                command = new SqlCommand(query);
                fillDgv(command);
            }
            else //Display data without birthdate.
            {
                if (radioButtonMale.Checked)
                {
                    query = "SELECT * FROM student WHERE gender = 'Male'";
                }
                else if (radioButtonFemale.Checked)
                {
                    query = "SELECT * FROM student WHERE gender = 'Female'";
                }
                else
                {
                    query = "SELECT * FROM student";
                }
                command = new SqlCommand(query);
                fillDgv(command);
            }
        }


        //Printing data from datagrid view to a newly created textfile in my system.
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            //Path of our file. The file name would be students_list.txt ..
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\students_list.txt";

            using(var writer = new StreamWriter(path))
            {
                //Checking if the file exists.
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                DateTime bdate;

                //Rows
                for(int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //Columns
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        //The birthdate Column. 
                        if (j == 3)
                        {
                            bdate = Convert.ToDateTime(dataGridView1.Rows[i].Cells[j].Value.ToString());
                            writer.Write("\t" + bdate.ToString("yyyy-NM-dd") + "\t" + "|");
                        }
                        //The last column that is Address.
                        else if (j == dataGridView1.Columns.Count - 2)
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                        }
                        else
                        {
                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                        }
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
