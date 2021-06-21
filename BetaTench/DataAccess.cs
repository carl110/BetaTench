using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaTench
{
    class DataAccess
    {

        private static OleDbConnection createConnection(string database = "Microsoft.ACE.OLEDB.12.0", string dataSource = "BetaTench.accdb")
        {
            OleDbConnectionStringBuilder connBuilder = new OleDbConnectionStringBuilder();
            connBuilder.Add("Provider", database);
            connBuilder.Add("Data Source", dataSource);
            OleDbConnection conn = new OleDbConnection(connBuilder.ConnectionString);
            return conn;
        }
        public static List<Employee> getAllEmployees()
        {
            List<Employee> empList = new List<Employee>();
            try
            {
                OleDbConnection conn = createConnection();
                OleDbCommand cmd = conn.CreateCommand();
                conn.Open();
                //OleDb command
                OleDbCommand command = new OleDbCommand("select * from Employees", conn);
                //Used to store the result from an OleDb statement
                OleDbDataReader dataReader = null;
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Employee tempEmp = new Employee();
                    tempEmp.EmployeeID = dataReader[0].ToString();
                    tempEmp.FirstName = dataReader[1].ToString();
                    tempEmp.LastName = dataReader[2].ToString();
                    tempEmp.NiNumber = dataReader[3].ToString();
                    tempEmp.Dob = Convert.ToDateTime(dataReader[4].ToString());
                    tempEmp.StartDate = Convert.ToDateTime(dataReader[5].ToString());
                    tempEmp.Department = dataReader[6].ToString();
                    tempEmp.JobTitle = dataReader[7].ToString();
                    tempEmp.Salary = Convert.ToDouble(dataReader[8]);
                    empList.Add(tempEmp);
                }
                dataReader.Close();
                command.Dispose();
                conn.Close();
            }
            catch (Exception e)
            {
                throw (new Exception("Error***" + e.Message));
            }
            return empList;
        }
        public static void UpdateEmployee(string employeeID, string firstName, string lastName, string niNo, DateTime dob, DateTime startDate, string jobTitle, double salary, string department)
        {
            try
            {
                OleDbConnection conn = createConnection();
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("Update Employees Set FirstName=@2, LastName=@3, NINumber=@4, DOB=@5, StartDate=@6, Department=@7, JobTitle=@8, Salary=@9" +
                    " Where EmployeeID=@1", conn);
                cmd.Parameters.Add(new OleDbParameter("@1", OleDbType.VarChar) { Value = employeeID });
                cmd.Parameters.Add(new OleDbParameter("@2", OleDbType.VarChar) { Value = firstName });
                cmd.Parameters.Add(new OleDbParameter("@3", OleDbType.VarChar) { Value = lastName });
                cmd.Parameters.Add(new OleDbParameter("@4", OleDbType.VarChar) { Value = niNo });
                cmd.Parameters.Add(new OleDbParameter("@5", OleDbType.Date) { Value = dob });
                cmd.Parameters.Add(new OleDbParameter("@6", OleDbType.Date) { Value = startDate });
                cmd.Parameters.Add(new OleDbParameter("@7", OleDbType.VarChar) { Value = department });
                cmd.Parameters.Add(new OleDbParameter("@8", OleDbType.VarChar) { Value = jobTitle });
                cmd.Parameters.Add(new OleDbParameter("@9", OleDbType.Currency) { Value = salary });

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR**** " + e.Message);
            }
        }
        public static List<Employee> searchOperator(string dep)
        {
            List<Employee> employeeList = new List<Employee>();
            try
            {
                OleDbConnection conn = createConnection();
                OleDbCommand cmd = conn.CreateCommand();

                cmd.CommandText = "SELECT * FROM Employees WHERE Department = '" + dep + "'";

                cmd.CommandType = CommandType.Text;
                conn.Open();
                OleDbDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    Employee tempEmp = new Employee();
                    tempEmp.EmployeeID = dataReader[0].ToString();
                    tempEmp.FirstName = dataReader[1].ToString();
                    tempEmp.LastName = dataReader[2].ToString();
                    tempEmp.Department = dataReader[6].ToString();
                    tempEmp.Salary = Convert.ToDouble(dataReader[8]);
                    employeeList.Add(tempEmp);
                }
                dataReader.Close();
                cmd.Dispose();
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employeeList;
        }

    }
}
