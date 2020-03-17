using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementConsole
{
    /// <summary>
    /// Interface Employee
    /// </summary>
    public interface IEmployee
    {
        void AddEmployee(string name, int status);   //add employee
        void DeleteEmployee(int id);   //delete employee
        List<Employee> GetActiveEmployee();     //get active employee
        List<Employee> GetTerminatedEmployee(DateTime dtOfStart, DateTime dtOfEnd);     //get terminated employees
        List<Employee> GetEmployee();   //get Currunt Employee
    }

    public class Employee : IEmployee
    {
        public enum actStatus { Aactive = 1, Terminated, Layoff, Leave, Quit };
        public static int EmployeeID = 0;

        private int id;
        private string empName;
        private int status;
        private DateTime dtOfTerminated;
        private List<Employee> empList;

        public int Id { get => id; set => id = value; }
        public string EmpName { get => empName; set => empName = value; }
        public int Status { get => status; set => status = value; }
        public DateTime DtOfTerminated { get => dtOfTerminated; set => dtOfTerminated = value; }

        /// <summary>
        /// constructor
        /// create a new instance of list of employee 
        /// </summary>
        public Employee()
        {
            empList = new List<Employee>();
        }
        
        /// <summary>
        /// Returen list of all current employee
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetEmployee()
        {
            return empList;
        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="newStatus"></param>
        public virtual void AddEmployee(string newName, int newStatus)
        {
            Employee e1 = new Employee();

            e1.id = ++EmployeeID;
            e1.empName = newName;
            e1.status = newStatus;

            empList.Add(e1);
            Console.WriteLine($"Added ID: {EmployeeID}, Employee Name: {newName}, status: {GetStatus(newStatus)}");
        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="emp"></param>
        public virtual void AddEmployee(Employee emp)
        {
            empList.Add(emp);
            Console.WriteLine($"ID: {emp.id}, Employee Name: {emp.empName}, status: {GetStatus(emp.status)}");
            Console.WriteLine($"Added a new employee\n");
        }

        /// <summary>
        /// Remove employee by id
        /// </summary>
        /// <param name="id"></param>
        public virtual void DeleteEmployee(int id)
        {
            empList.RemoveAll(x => x.id == id);
        }

        /// <summary>
        /// Return list of current active employee
        /// </summary>
        /// <returns></returns>
        public List<Employee> GetActiveEmployee()
        {
            var empActGroug = from emp in empList
                              where emp.status == (int)actStatus.Aactive
                              select emp;

            return empActGroug.ToList();
        }

        /// <summary>
        /// Return list of terminated employee defend on the date
        /// </summary>
        /// <param name="dtOfStart">date of start</param>
        /// <param name="dtOfEnd">date of end</param>
        /// <returns></returns>
        public List<Employee> GetTerminatedEmployee(DateTime dtOfStart, DateTime dtOfEnd)
        {
            var empActGroug = from emp in empList
                              where emp.dtOfTerminated >= dtOfStart &&
                               emp.dtOfTerminated <= dtOfEnd
                              select emp;

            return empActGroug.ToList();
        }
        
        /// <summary>
        /// Set date of terminate for the test
        /// </summary>
        /// <param name="id">employee id</param>
        /// <param name="dt">date of terminated</param>
        public virtual void SetTerminatedDate(int id, string dt)
        {
            var emp = empList.First(i => i.id == id);
            var index = empList.IndexOf(emp);

            if (index != -1) { 
                empList[index].dtOfTerminated = DateTime.Parse(dt);
                empList[index].status = (int)actStatus.Terminated;
            }
        }

        /// <summary>
        /// convert enum variable to string
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string GetStatus(int status)
        {
            string strStatus = "";
            switch (status)
            {
                case (int)actStatus.Aactive:
                    strStatus = "Active";
                    break;
                case (int)actStatus.Terminated:
                    strStatus = "Terminated";
                    break;
                case (int)actStatus.Layoff:
                    strStatus = "Layoff";
                    break;
                case (int)actStatus.Leave:
                    strStatus = "Leave";
                    break;
                case (int)actStatus.Quit:
                    strStatus = "Quit";
                    break;
                default:
                    break;
            }

            return strStatus;
        }
    }
}
