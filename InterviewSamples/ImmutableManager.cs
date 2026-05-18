namespace InterviewSamples
{
    public record ImmutableManager(string Name, string Title, string DepartmentName) :
        ImmutableEmployee(Name, Title, DepartmentName)
    {
        public override string[] GetACL()
        {
            return new string[] { "profile:list,read,write", "accounts:list,read" };
        }
    }
}