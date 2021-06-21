using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaTench
{
    public partial class frmDataSearch : Form
    {
        List<Employee> listOfEmployees = DataAccess.getAllEmployees();
        public frmDataSearch()
        {
            InitializeComponent();
        }
        private void SetUp()
        {   //Loop through list of cars and add to datagridview
            for (int i = 0; i < listOfEmployees.Count; i++)
            {
                dgvEmployee.Rows.Add(listOfEmployees[i].EmployeeID,
                    listOfEmployees[i].FirstName,
                    listOfEmployees[i].LastName,
                    listOfEmployees[i].Department,
                    listOfEmployees[i].Salary);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cboDepartment.Text.Length > 0)
            {
                listOfEmployees = DataAccess.searchOperator(cboDepartment.Text);
                //Clear all data before loading new search result
                dgvEmployee.Rows.Clear();
                SetUp();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmEmployees employees = new frmEmployees();
            this.Hide();
            employees.ShowDialog();
            this.Close();
        }
    }
}
