using Bogus;
using System.Text;
using System.Text.RegularExpressions;

namespace Demo1.Tests
{
    public class DemoImplementatiosTests
    {
        

        [Fact]
        public void Flatten()
        {
            var context = new FlattenTestContext();

            var persons = context.GetTestPersons();
            
            var result = DemoImplementation.DemoImplementation.Flatten(persons);

            Assert.Equal(persons.SelectMany(x => x.Emails).Count(), result.Count());
            var validNameWithIdRegex = new Regex("[a-zA-Z0-9]");
            foreach(var personWithEmail in result)
            {
                Assert.Matches(validNameWithIdRegex, personWithEmail.SanitizedNameWithId);
            }
        }
    }

    internal class FlattenTestContext
    {
        public readonly string[] _buisnessTypes = new[] { "Business", "Private" };
        public List<DemoSource.Person> GetTestPersons()
        {
            var testEmails = new Faker<DemoSource.EmailAdress>("pl")
                .RuleFor(e => e.Email, (f, e) => f.Person.Email)
                .RuleFor(e => e.EmailType, (f, e) => f.PickRandom(_buisnessTypes));

            var testPerons = new Faker<DemoSource.Person>("pl")
                .RuleFor(p => p.Id, (f, p) => f.Random.Guid().ToString())
                .RuleFor(p => p.Name, (f, p) => f.Name.FullName())
                .RuleFor(p => p.Emails, (f, p) => testEmails.GenerateBetween(1, 10));
            return testPerons.GenerateBetween(10, 50);
        }
    }
}