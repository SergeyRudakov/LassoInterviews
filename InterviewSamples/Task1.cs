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

        public string State { get; private set; }
        public string Name { get; }
        public string Title { get; }
        public string DepartmentName { get; }

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

        public virtual string[] GetACL()
        {
            return new string[] { "profile:read" };
        }
    }



     
    public class Manager: Employee
    {
      
        public override string[] GetACL()
        {
            return new string[] { "profile:list,read,write", "accounts:list,read" };
        }

        public Manager(string name, string title, string departmentName) : base(name, title, departmentName)
        {
        }
    }

    }
