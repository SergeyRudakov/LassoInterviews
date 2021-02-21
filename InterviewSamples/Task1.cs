using System;
using System.Collections.Generic;
using System.Text;

namespace InterviewSamples
{
    /// <summary>
    /// Generic Employee class 
    /// </summary>
    public class Employee
    {
        public Employee(string name, string title, string departmentName)
        {
            Name = name;
            Title = title;
            DepartmentName = departmentName;
        }

        public string State { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string DepartmentName { get; set; }

        public static IEnumerable<Employee> GetEmployees()
        {
            return new Employee[] {
                new Employee("John Doe", "Manager", "Sales"){ State="TX"},
                new Employee("Michael Scott", "Manager", "Sales"){ State="NY"},
                new Employee("Tom Hawk", "Sr Software Engineer", "Engineering"){ State="TX"},
                new Employee("Alicia Newton", "HR Manager", "Human Resources"){ State="TX"},
                new Employee("Alex Smith", "Software Engineer", "Engineering"){ State="NY"}
            };
        }

        public string[] GetACL()
        {
            return new string[] { "profile:read" };
        }
    }



    /*  
    public class Manager: Employee
    {
      
        public string[] GetACL()
        {
            return new string[] { "profile:list,read,write", "accounts:list,read" };
        }
    }

    }
    */

}
