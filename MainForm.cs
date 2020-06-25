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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addNewStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddStudentForm addStudentForm = new AddStudentForm();
            addStudentForm.Show(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("../../images/course_48px.png");
        }

        private void studentsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentListForm stdListF = new StudentListForm();
            stdListF.Show(this);
        }

        private void staticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StaticsForm stcF = new StaticsForm();
            stcF.Show(this);
        }

        private void editRemoveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDeleteStudentForm upDelStdF = new UpdateDeleteStudentForm();
            upDelStdF.Show(this);
        }

        private void manageStudentsFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStudentForm mstdF = new ManageStudentForm();
            mstdF.Show(this);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintStudentForm prF = new PrintStudentForm();
            prF.Show(this);
        }

        private void courseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCourseForm acF = new AddCourseForm();
            acF.Show(this);
        }

        private void removeCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveCourseForm rcF = new RemoveCourseForm();
            rcF.Show(this);
        }

        private void editCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCourseForm ecf = new EditCourseForm();
            ecf.Show(this);
        }

        private void manageCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCourseForm mcf = new ManageCourseForm();
            mcf.Show(this);
        }

        private void printToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PrintCourseForm pcF = new PrintCourseForm();
            pcF.Show(this);
        }

        private void scoreToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddScoreForm asF = new AddScoreForm();
            asF.Show(this);
        }

        private void manageScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageScoreForm msf = new ManageScoreForm();
            msf.Show(this);
        }

        private void removeCourseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RemoveScoreForm rsf = new RemoveScoreForm();
            rsf.Show(this);
        }

        private void avgScoreByCourseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            avgScoreByCourseForm avgScrByCrsF = new avgScoreByCourseForm();
            avgScrByCrsF.Show(this);
        }

        private void printToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            PrintScoreForm psf = new PrintScoreForm();
            psf.Show(this);
        }
    }
}
