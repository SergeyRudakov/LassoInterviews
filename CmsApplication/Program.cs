using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CmsApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            PostManager postManager = new MarketingPostManager();
            var posts = await postManager.SelectPosts();
            string json = JsonSerializer.Serialize(posts);
            Console.WriteLine(json);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }
    }
}
