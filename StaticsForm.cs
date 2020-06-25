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
    public partial class StaticsForm : Form
    {
        public StaticsForm()
        {
            InitializeComponent();
        }

        //Color variables
        Color panTotalColor;
        Color panMaleColor;
        Color panFemaleColor;

        private void StaticsForm_Load(object sender, EventArgs e)
        {
            //Getting the panels color.
            panTotalColor = panelTotal.BackColor;
            panMaleColor = panelMale.BackColor;
            panFemaleColor = panelFemale.BackColor;

            //Displaying the values.
            Student student = new Student();
            double totalStudents = Convert.ToDouble(student.totalStudent());
            double totalMaleStudent = Convert.ToDouble(student.totalMaleStudent());
            double totalFemaleStudent = Convert.ToDouble(student.totalFemaleStudent());

            // Counting the percentage (%)
            double malePercentage = totalMaleStudent * 100 / totalStudents;
            double femalePercentage = totalFemaleStudent * 100 / totalStudents;

            labelTotal.Text = "Total Students: " + totalStudents.ToString();
            labelMale.Text = "Male: " + malePercentage.ToString("0.00") + "%";
            labelFemale.Text = "Female: " + femalePercentage.ToString("0.00") + "%";
        }

        private void labelTotal_MouseEnter(object sender, EventArgs e)
        {
            panelTotal.BackColor = Color.White;
            labelTotal.ForeColor = panTotalColor;
        }

        private void labelTotal_MouseLeave(object sender, EventArgs e)
        {
            panelTotal.BackColor = panTotalColor;
            labelTotal.ForeColor = Color.White;
        }

        private void labelMale_MouseEnter(object sender, EventArgs e)
        {
            panelMale.BackColor = Color.White;
            labelMale.ForeColor = panMaleColor;
        }

        private void labelMale_MouseLeave(object sender, EventArgs e)
        {
            panelMale.BackColor = panMaleColor;
            labelMale.ForeColor = Color.White;
        }

        private void labelFemale_MouseEnter(object sender, EventArgs e)
        {
            panelFemale.BackColor = Color.White;
            labelFemale.ForeColor = panFemaleColor;
        }

        private void labelFemale_MouseLeave(object sender, EventArgs e)
        {
            panelFemale.BackColor = panFemaleColor;
            labelFemale.ForeColor = Color.White;
        }
    }
}
