using System.Collections.Generic;

namespace InterviewSamples
{
    public record ImmutableEmployee(string Name, string Title, string DepartmentName)
    {
        public string State { get; set; }
        public string Name { get; set; } = Name;
        public string Title { get; set; } = Title;
        public string DepartmentName { get; set; } = DepartmentName;

        public static IEnumerable<ImmutableEmployee> GetEmployees()
        {
            return new ImmutableEmployee[] {
                new("John Doe", "Manager", "Sales"){ State="TX"},
                new("Michael Scott", "Manager", "Sales"){ State="NY"},
                new("Tom Hawk", "Sr Software Engineer", "Engineering"){ State="TX"},
                new("Alicia Newton", "HR Manager", "Human Resources"){ State="TX"},
                new("Alex Smith", "Software Engineer", "Engineering"){ State="NY"}
            };
        }

        public virtual string[] GetACL()
        {
            return new string[] { "profile:read" };
        }
    }
}