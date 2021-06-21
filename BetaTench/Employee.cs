using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetaTench
{
    class Employee
    {
        string employeeID;
        string firstName;
        string lastName;
        string niNumber;
        DateTime dob;
        DateTime startDate;
        string jobTitle;
        double salary;
        string department;

        public string EmployeeID { get => employeeID; set => employeeID = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string NiNumber { get => niNumber; set => niNumber = value; }
        public DateTime Dob { get => dob; set => dob = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public double Salary { get => salary; set => salary = value; }
        public string Department { get => department; set => department = value; }
    }
}
