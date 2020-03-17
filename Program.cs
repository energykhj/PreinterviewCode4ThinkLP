using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 1. Add Employee
             *    Employee ID is increse automatically.
             *    Can set current status of employee
            */

            Console.WriteLine($"\nAdd Employees:");
            Employee e1 = new Employee();
            e1.AddEmployee("Terry", (int)Employee.actStatus.Aactive);
            e1.AddEmployee("Cesar", (int)Employee.actStatus.Aactive);
            e1.AddEmployee("Debra", (int)Employee.actStatus.Layoff);
            e1.AddEmployee("Hugo", (int)Employee.actStatus.Aactive);
            e1.AddEmployee("Claire", (int)Employee.actStatus.Aactive);
            e1.AddEmployee("Eugene", (int)Employee.actStatus.Aactive);
            e1.AddEmployee("Steve", (int)Employee.actStatus.Leave);
            e1.AddEmployee("Candy", (int)Employee.actStatus.Aactive);

            Console.WriteLine($"\nCurrunt Employees:");
            foreach (Employee aEmployee in e1.GetEmployee())
            {
                Console.WriteLine($"ID: {aEmployee.Id}, Name: {aEmployee.EmpName}");
            }

            /*
             * 2. Remove employee by Employee ID
            */

            e1.DeleteEmployee(2);
            Console.WriteLine($"\nDelete a employee: ID:{2}");
            foreach (Employee aEmployee in e1.GetEmployee())
            {
                Console.WriteLine($"ID: {aEmployee.Id}, Name: {aEmployee.EmpName}");
            }


            /*
             * 3. Return a list of active employees
            */
            Console.WriteLine($"\nActive Employees:");
            var actEmp = e1.GetActiveEmployee();
            foreach (Employee aEmployee in actEmp)
            {
                Console.WriteLine($"ID: {aEmployee.Id}, Name: {aEmployee.EmpName}");
            }


            /*
             * Set terminate date for test
            */
            Console.WriteLine($"\nSet date of terminated:");
            e1.SetTerminatedDate(3, "12/21/2019");
            e1.SetTerminatedDate(5, "02/21/2020");
            e1.SetTerminatedDate(6, "02/21/2020");
            e1.SetTerminatedDate(7, "03/21/2020");

            var tEmp = from emp in e1.GetEmployee()
                     where emp.Status == (int)Employee.actStatus.Terminated
                     select emp;

            foreach (Employee aEmployee in tEmp)
            {                
                Console.WriteLine($"ID: {aEmployee.Id}, Name: {aEmployee.EmpName}, " +
                    $"set terminated date: {String.Format("{0:MM/dd/yyyy}", aEmployee.DtOfTerminated)}");
            }

            /*
             *  4. Return a list of terminated employees in the last month
            */
            Console.WriteLine($"\nGet employee of terminated in the last month:");
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last = month.AddDays(-1);

            var firedEmp = e1.GetTerminatedEmployee(first, last);
                              
            foreach (Employee aEmployee in firedEmp)
            {
                Console.WriteLine($"ID: {aEmployee.Id}, Name: {aEmployee.EmpName}, " +
                    $"terminated date: {String.Format("{0:MM/dd/yyyy}", aEmployee.DtOfTerminated)}");
            }

            Console.ReadKey();
        }


        /// <summary>
        /// Get information of employee from Front form if applicable
        /// </summary>
        /// <returns></returns>
        private static Employee AddEmployee()
        {
            Employee e1 = new Employee();
            e1.EmpName = "Terry";
            e1.Status = (int)Employee.actStatus.Aactive;

            return e1;
        }
    }
}
