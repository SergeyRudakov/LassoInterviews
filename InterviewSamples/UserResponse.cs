using System.Collections.Generic;

namespace InterviewSamples
{
    public class UserResponse
    {
        public List<Result> Results { get; set; }
    }
    
    public class Name
    {
        public string First { get; set; }
        
        public string Last { get; set; }
    }

    public class Result
    {
        public Name Name { get; set; }
    }
}