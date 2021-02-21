using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewSamples
{

  

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started");
            var tree = Tree<int>.Init();
            
            var employees = Employee.GetEmployees();

       

            //TASK 1 (Task1.cs):
            //Select Count of Employees by Every Department in State=TX return object {DeparmentName, Count}

            //TASK 1.1:
            //You need to create new Manager class (above) and  override GetACL method to return updated set of access rights (above).  
            //How can you change implementation of GetEmployees method so it will return Managers array?
            //How to make Employee/Manager objects immutable?



            //TASK 2
            //Given Tree object above return an array of Values on Level 3.

            var result = GetValues<int>(tree, 3);

            //TASK 3
            //We need to retrieve 10 random users from external API. Each request is independent. Sometimes API returns error. 
            //You need to run APIs in parallel, handle error messages and return name from JSON response. JSON sample 
            // {"results": [{ "name":{"first":"FIRST NAME", "last": "LAST NAME" } }] }  should be transformed to sting  $"{first} {last}"

            //var result2 = GetUsers(10);
        }
        public static string[] GetUsers(int count)
        {
            const string API = "https://randomuser.me/api/";
            ApiServices service = new ApiServices();
            IList<string> users = new List<string>();
            for (int i =0;i< count; i++)
            {
                var data = service.GetData(API).Result;
                users.Add(data);

            }
            return users.ToArray();
        }

        public static T[] GetValues<T>(Tree<T> tree, int Level)
        {
            return null;
        }
    }
}
