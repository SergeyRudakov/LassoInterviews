using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewSamples
{
    
    public class ApiServices
    {
        static HttpClient client = new HttpClient();


        public async Task<string> GetData(string url)
        {
            var data = await client.GetStringAsync(url);
            return data;
        }
    }
}
