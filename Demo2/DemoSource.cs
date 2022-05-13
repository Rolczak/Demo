namespace DemoSource
{
    public class Person
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public IEnumerable<EmailAdress> Emails { get; set; } = Enumerable.Empty<EmailAdress>();
    }

    public class EmailAdress
    {
        public string Email { get; set; } = null!;
        public string EmailType { get; set; } = null!;
    }

    public class Account
    {
        public string Id { get; set; } = null!;
        public EmailAdress EmailAdress { get; set; } = null!;
    }

    public class Group
    {
        public string Id { get; set; } = null!;
        public string Label { get; set; } = null!;
        public IEnumerable<Person> People { get; set; } = Enumerable.Empty<Person>();
    }
}
