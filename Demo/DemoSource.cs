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
}
