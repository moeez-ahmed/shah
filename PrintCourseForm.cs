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
    public partial class PrintCourseForm : Form
    {
        public PrintCourseForm()
        {
            InitializeComponent();
        }

        private void PrintCourseForm_Load(object sender, EventArgs e)
        {
            //Populate DGV with courses.
            Course course = new Course();
            dataGridView1.DataSource = course.getAllCourses();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // Path of our file.The file name would be Courses_list.txt..
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Courses_list.txt";

            using (var writer = new StreamWriter(path))
            {
                //Checking if the file exists.
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                //Rows
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    //Columns
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
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
    }
}
