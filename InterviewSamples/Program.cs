using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InterviewSamples
{

  

    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Started");

            
            var employees = Employee.GetEmployees(); //employee has {Name, Title, DepartmentName} fields



            //TASK 1 (Task1.cs):
            //Select Count of Employees by Every Department in State=TX. Return array of objects {DeparmentName, Count}
            var employCount = employees
                .Where(x => x.State == "TX")
                .GroupBy(x => x.DepartmentName)
                .Select(x => new
                {
                    Department = x.Key,
                    State = x.Count()
                });
                

            //TASK 1.1:
            //You need to create new Manager class (inherited from Employee) and  override GetACL method to return updated set of access rights (above).  
            //How can you change implementation of GetEmployees method so it will return Managers array?
            //How to make Employee/Manager objects immutable?
            var manager = new Manager("managerName", "managerTitle", "managerDepartment");
            foreach (var acl in manager.GetACL())
            {
                Console.WriteLine(acl);
            }
            
            //ImmutableEmpoyee
            var immutableEmployee = new ImmutableEmployee("immutableName", "immutableString", "immutableDepartment");
            //ImmutableManager
            var immutableManager = new ImmutableManager("immutableName", "immutableString", "immutableDepartment");

            //TASK 2
            //Given Tree object above return an array of Values on Level 3.

            var tree = Tree<int>.Init();
            var resultTree = GetValues<int>(tree, 3);
            foreach (var haveToPrint in resultTree)
            {
                Console.WriteLine(haveToPrint);
            }

            //TASK 3
            //We need to retrieve 10 random users from external API. Each request is independent. Sometimes API returns error. 
            //You need to run APIs in parallel, handle error messages and return name from JSON response. JSON sample 
            // {"results": [{ "name":{"first":"FIRST NAME", "last": "LAST NAME" } }] }  should be transformed to sting  $"{first} {last}"

            var result2 = GetUsers(10);
            foreach (var result in result2)
            {
                Console.WriteLine(result);
            }
            var resultAsync = await GetUsersAsync(10);
            foreach (var result in resultAsync)
            {
                Console.WriteLine(result);
            }

            //TASK 4
            //Let's discuss how Task 4 code can be improved including naming style, possible bugs, pattern usage, 
            //reducing the amount of code, improving long-term support and other best practices. 
            //You can just name things, not have to change actual code.
            
            // 1. All classes should follow CamelCase strategy (Author class)
            // 2. In Post class better to make a constructor and, for example, set PublishTime
            // 3. Method in PostRepository/AuthorRepository (Update) have to by async (I/O with DB) also GetAll methods have to return Task<List<DbEntity>>
            // 4. PostManager(Naming is strange, common managers - it's not a good practice because that class could become a super-class. (hard to support, hard to refactor, e.t.c):
                // (SelectPosts method)
                // 4.1 variable 'list' meaningless, better call it 'posts' or 'allPosts'
                // 4.2 Inherits from PostRepository doesn't make any sense. Better use DI for use Repository functionality
                // 4.3 Better to create a separate method in PostingRepository already with your filters (right now code use client filtering)
                // 4.4 We don't need a separate result list here, can transform data from DB with LINQ(still better to make a separate DB method for filtering)
                // (PublishPost)
                // 4.5 Move PublichPost in Post entity and just call it when you want
            // 5. MarketingPostManager
                // 5.1 Inherits is not correct, PostManager it's not a BaseManager or something like that
                // 5.2 Again, better to inject repository in DI
                // 5.3 authorIds is not used anywhere
                // 5.4 marketingCategory - magic number, we need extend that method for filtering (from request for example) , it shouldn't be hardcoded
                // 5.5 Still filtering should be in DB (it's faster, commonly)
                // 5.6 new AuthorRepository breaks SOLID prinicipals(actually, a lot of things here breaks that rules..), better to use Dependency Inversion ( Dependency Injection like a tool)
                // 5.7 again, we wanna join result in Client (our backend) , better to write a separate query for DB and returned already what you want (or if you use EF just describe IQueriable in Repository)
                // 5.8 PublishPost use new will hide behavior, it's not good , and that method changed Category and Title what will change the method meaning at all. (only two fields have to changed when Publish)
            // In common - managers, it's pretty strange strategy.
            // If we use EF , better to use UnitOfWork for example , and RepositoryFactory + makes DbEntities rich
            // If we wanna use Dapper/ADO.net - same for UnitOfWork, but in my opinion , here repository is better.
            // Looks like what we can use pretty standard architecture here (Controller -> Service -> Repository)
            // Or CQRS with Handlers + Decorators + UoW + e.t.c what ever you want
            PostManager postManager = new MarketingPostManager();

            Console.WriteLine("Finished");
        }
        
        //If we can't change it on AsyncAwait
        public static string[] GetUsers(int count)
        {
            const string API = "http://randomuser.me/api/";
            ApiServices service = new ApiServices();
            IList<string> users = new List<string>();
            List<Task<string>> tasks = new List<Task<string>>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(service.GetData(API));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException aggregateException)
            {
                Console.WriteLine("exception while wait all");
            }
            
            foreach (var task in tasks)
            {
                if (task.IsCompletedSuccessfully && task.Exception == null)
                {
                    var result = JsonSerializer.Deserialize<UserResponse>(
                        task.Result, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                    var user = result.Results.FirstOrDefault();
                    users.Add($"{user?.Name.First} {user?.Name.Last}");
                }

                if (task.Exception != null)
                {
                    Console.WriteLine($"Concrete in task: {task.Exception.Message}");
                }
            }
            

            return users.ToArray();
        }
        
        //Async variant, better because I don't block main thread.
        public static async Task<string[]> GetUsersAsync(int count)
        {
            const string API = "http://randomuser.me/api/";
            ApiServices service = new ApiServices();
            IList<string> users = new List<string>();
            List<Task<string>> tasks = new List<Task<string>>();

            for (int i = 0; i < count; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        //Commonly better choice it's use try/catch in GetData method. But i don't know I can change it or not.
                        return await service.GetData(API);
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine("Catched exception");
                        //Do something when catched
                        return null;
                    }
                }));
            };

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception exception)
            {
                Console.WriteLine("exception while when all");
            }
            
            foreach (var task in tasks)
            {
                if (task.Result == null)
                {
                    continue;
                }
                var result = JsonSerializer.Deserialize<UserResponse>(
                    task.Result, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                var user = result.Results.FirstOrDefault();
                users.Add($"{user?.Name.First} {user?.Name.Last}");
            }
            

            return users.ToArray();
        }

        public static T[] GetValues<T>(Tree<T> tree, int level)
        {
            var queue = new Queue<(Tree<T>, int)>();
            var result = new List<T>();

            //Root is a first?
            level -= 1;
            
            if (level == 0)
            {
                return null;
            }
            //Add root
            queue.Enqueue((tree, 0));
            while (queue.Any())
            {
                var currentNodeWithLevel = queue.Dequeue();
                if (currentNodeWithLevel.Item2 > level)
                {
                    break;
                }

                if (currentNodeWithLevel.Item1.Children != null)
                {
                    foreach (var child in currentNodeWithLevel.Item1.Children)
                    {
                        if (child != null)
                        {
                            var childrenLevel = currentNodeWithLevel.Item2 + 1;
                            queue.Enqueue((child, childrenLevel));
                            if (childrenLevel == level)
                            {
                                result.Add(child.Value);
                            }
                        }
                    }
                }
            }

            return result.ToArray();
        }
    }
}
