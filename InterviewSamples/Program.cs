using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewSamples
{
    /// <summary>
    /// Tree object, contais value and childen nodes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T>
    {

        public Tree() { }

        public Tree(T value, T[] children)
        {
            this.Value = value;
            var childrenNodes = new List<Tree<T>>();
            for (int i = 0; i < children.Length; i++)
                childrenNodes.Add(new Tree<T>() { Value = children[i] });
            this.Children = childrenNodes.AsEnumerable();

        }
        public T Value { get; set; }
        public IEnumerable<Tree<T>> Children { get; set; }


        /// <summary>
        /// Create test sample 
        /// </summary>
        /// <returns></returns>
        public static Tree<int> Init()
        {
            var root = new Tree<int>();
            root.Value = 5;
            root.Children = new List<Tree<int>>();
            root.Children = new List<Tree<int>>() { new Tree<int>(5, new int[] {7,8,9}),
        new Tree<int>(0, new int[] { 3, 5,-9 }),
        new Tree<int>(25, new int[] { 6, 78, 3 })
    };

            return root;
        }
    }

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


    // You need to create new Manager class and  override IsManager method to return true.  
    // Can we hide Manager.GetEmployees() method?

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


    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var tree = Tree<int>.Init();
            
            var employees = Employee.GetEmployees();

            //TASK 1:
            //Select Count of Employees by Every Department in State=TX return object {DeparmentName, Count}

            //TASK 1.1:
            //You need to create new Manager class (above) and  override GetACL method to return updated set of access rights (above).  
            //How can you change implementation of GetEmployees method so it will return Managers array?
            //How to make Employee/Manager objects immutable?



            //TASK 2
            //Given Tree object above return an array of Values on Level 3.

            var result = GetValues<int>(tree, 3);
        }

        public static T[] GetValues<T>(Tree<T> tree, int Level)
        {
            return null;
        }
    }
}
