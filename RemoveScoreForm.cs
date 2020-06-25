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
    public partial class RemoveScoreForm : Form
    {
        public RemoveScoreForm()
        {
            InitializeComponent();
        }
        Score score = new Score();
        private void RemoveScoreForm_Load(object sender, EventArgs e)
        {
            //Populate data grid view with student score. 
            dataGridView1.DataSource = score.getStudentsScore();
        }

        private void buttonRemoveScore_Click(object sender, EventArgs e)
        {
            //Remove the selected Score. 
            try
            {
                int studentId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                int courseId = int.Parse(dataGridView1.CurrentRow.Cells[3].Value.ToString());

                if (MessageBox.Show("Do you wish to delete this score?", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(studentId, courseId))
                    {
                        MessageBox.Show("Score deleted.", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = score.getStudentsScore();
                    }
                    else
                    {
                        MessageBox.Show("Score not deleted.", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
