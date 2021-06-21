using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BetaTench
{
    public partial class frmDataSearch : Form
    {
        List<Employee> listOfEmployees;
        int recordNumber = 0;
        private PrintDocument myPrintDocument = new PrintDocument();
        private Font printFont;
        private int intPageCount;
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
        private void myPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string employeeRecord = null;
            int currentPage = intPageCount + 1;

            Font printFont = new Font("Courier New", 14, FontStyle.Bold);
            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height / printFont.GetHeight(e.Graphics);
            StringFormat centeredText = new StringFormat();
            centeredText.Alignment = StringAlignment.Center;
            // Title & Page Number
            string title = "EMPLOYEE SEARCH";
            e.Graphics.DrawString(title, printFont, Brushes.Black,
            e.PageSettings.PaperSize.Width / 2, yPos, centeredText);
            e.Graphics.DrawString($"Page {currentPage.ToString().PadLeft(2, '1')}", printFont, Brushes.Black, e.MarginBounds.Right, yPos);
            yPos += Convert.ToInt32(printFont.GetHeight());
            linesPerPage -= 1;
            // Today's Date
            printFont = new Font("Courier New", 10, FontStyle.Bold);
            string date = $"Date {DateTime.Today:d}";
            e.Graphics.DrawString(date, printFont, Brushes.Black, e.PageSettings.PaperSize.Width / 2, yPos, centeredText);
            yPos += Convert.ToInt32(printFont.GetHeight()) * 2;
            linesPerPage -= 1;
            // Column Headings
            string headings = string.Format(
                "{0} {1} {2} {3}",
                "EmployeeID".PadRight(15),
                "Name".PadRight(15),
                "Department".PadRight(10),
                "Salary".PadRight(15)

            );
            e.Graphics.DrawString(headings, printFont, Brushes.Black, 10, yPos);
            yPos += Convert.ToInt32(printFont.GetHeight());
            linesPerPage -= 1;

            //for each record in list
            for (int i = 0; recordNumber < listOfEmployees.Count && i < linesPerPage; recordNumber++, i++)
            {
                yPos = topMargin + (recordNumber * printFont.GetHeight());
                //combine list deatils into 1 line
                employeeRecord = string.Format(
                       "{0} {1} {2} {3}",
                       listOfEmployees[recordNumber].EmployeeID.ToString().PadRight(15),
                       listOfEmployees[recordNumber].LastName.ToString() + ", " + listOfEmployees[recordNumber].FirstName[0].ToString().PadRight(15),
                       listOfEmployees[recordNumber].Department.ToString().PadRight(10),
                       listOfEmployees[recordNumber].Salary.ToString().PadRight(15)
                       );
                e.Graphics.DrawString(employeeRecord, printFont, Brushes.Black, 10, yPos, new StringFormat());
            }

            //If more lines exist, print another page.
            if (listOfEmployees.Count > recordNumber)
            {
                e.HasMorePages = true;
                currentPage++;
            }
            else
                e.HasMorePages = false;
        }
        private void myPrintDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            recordNumber = 0;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                myPrintDocument.Print();
                intPageCount++;
            }
        }
    }
}
