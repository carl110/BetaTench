using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaTench
{
    public partial class frmEmployees : Form
    {
        List<Employee> listOfEmployees = DataAccess.getAllEmployees();
        int recordNumber = 0;
        public frmEmployees()
        {
            InitializeComponent();
        }

        private void ShowRecord()
        { //Method to show data according to list number called 
            if (listOfEmployees.Count > 0)
            {
                txtEmployeeNo.Text = listOfEmployees[recordNumber].EmployeeID;
                txtFirstName.Text = listOfEmployees[recordNumber].FirstName;
                txtLastName.Text = listOfEmployees[recordNumber].LastName;
                txtNINo.Text = listOfEmployees[recordNumber].NiNumber;
                txtDoB.Text = listOfEmployees[recordNumber].Dob.ToString("d MMMM yyyy");
                txtStartDate.Text = listOfEmployees[recordNumber].StartDate.ToString("d MMMM yyyy");
                txtJobTitle.Text = listOfEmployees[recordNumber].JobTitle;
                txtSalary.Text = listOfEmployees[recordNumber].Salary.ToString();
                txtDepartment.Text = listOfEmployees[recordNumber].Department;
                txtRecordCount.Text = (recordNumber + 1) + " of " + listOfEmployees.Count;
            }
            else
            {   //If not cars exist show empty fields
                txtRecordCount.Text = "No Records";
            }
        }

        public static bool IsNumeric(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }

        private bool isDate(string inputDate)
        {
            DateTime value;
            if (DateTime.TryParse(inputDate, out value) == false)
            {
                return true;
            }
            else return false;
        }

        private void frmEmployees_Load(object sender, EventArgs e) => this.Text = $"Task A Carl Wainwright {DateTime.Now.ToShortDateString()}";
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            ShowRecord();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsNumeric(txtSalary.Text))
            {
                txtSalary.Clear();
                txtSalary.Select();
            }
            else if (isDate(txtDoB.Text))
            {
                txtDoB.Clear();
                txtDoB.Select();
            }
            else if (isDate(txtStartDate.Text))
            {
                txtStartDate.Clear();
                txtStartDate.Select();
            }
            else
            {
                DataAccess.UpdateEmployee(txtEmployeeNo.Text,
                                         txtFirstName.Text,
                                         txtLastName.Text,
                                         txtNINo.Text,
                                         Convert.ToDateTime(txtDoB.Text),
                                         Convert.ToDateTime(txtStartDate.Text),
                                         txtJobTitle.Text,
                                         Convert.ToInt32(txtSalary.Text),
                                         txtDepartment.Text);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            recordNumber = 0;
            ShowRecord();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (recordNumber == 0)
            {
                recordNumber = listOfEmployees.Count - 1;
            }
            else
            {
                recordNumber -= 1;
            }
            ShowRecord();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (recordNumber == listOfEmployees.Count - 1)
            {
                recordNumber = 0;
            }
            else
            {
                recordNumber += 1;
            }
            ShowRecord();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            recordNumber = listOfEmployees.Count - 1;
            ShowRecord();
        }

        private void btnCancel_Click(object sender, EventArgs e) => ShowRecord();

        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        private void frmEmployees_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F10)
            {
                ShowRecord();
            }

            if (e.KeyCode == Keys.Escape)
            {
                ShowRecord();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmDataSearch search = new frmDataSearch();
            this.Hide();
            search.ShowDialog();
            this.Close();
        }
    }
}
