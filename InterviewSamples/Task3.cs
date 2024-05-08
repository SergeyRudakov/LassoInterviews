using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InterviewSamples
{
    
    public class ApiServices
    {
        public async Task<string> GetData(string url)
        {
            HttpClient client = new HttpClient();

            var data = await client.GetStringAsync(url);
            return data;
        }
    }
}
