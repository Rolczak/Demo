using Bogus;
using System.Diagnostics;
using Xunit.Abstractions;

namespace Demo2.Tests
{
    public class DemoImplementationTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public DemoImplementationTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void MatchPersonToAccountShouldReturnValidData()
        {
            Randomizer.Seed = new Random(10);
            var watch = new Stopwatch();
            var context = new MatchPersonToAccountContext();
            watch.Start();
            //Takes some seconds
            var (Groups, Accounts, Emails) = context.GetDataForTests(16000);

            watch.Stop();
            _testOutputHelper.WriteLine($"Test data generated in:{watch.Elapsed}");
            watch.Restart();

            var result = DemoImplementation.ToDo.MatchPersonToAccount(Groups, Accounts, Emails);
            watch.Stop();
            _testOutputHelper.WriteLine($"Matching completed in: {watch.Elapsed}");

            Assert.Equal(Emails.Count(), result.Count());

            foreach (var personWithAccount in result)
            {
                Assert.Contains(personWithAccount.Item1.EmailAdress, personWithAccount.Item2.Emails);
            }
        }
    }

    internal class MatchPersonToAccountContext
    {
        private readonly string[] _buisnessTypes = new[] { "Business", "Private" };

        public (IEnumerable<DemoSource.Group> Groups, IEnumerable<DemoSource.Account> Accounts, IEnumerable<string> Emails)
            GetDataForTests(int numberOfEmailsToSearch)
        {
            var accounts = new List<DemoSource.Account>();
            var testEmails = new Faker<DemoSource.EmailAdress>("pl")
                .RuleFor(e => e.Email, (f, e) => f.Person.Email)
                .RuleFor(e => e.EmailType, (f, e) => f.PickRandom(_buisnessTypes))
                .FinishWith((f, e) =>
                {
                    accounts.Add(new DemoSource.Account()
                    {
                        Id = f.Random.Guid().ToString(),
                        EmailAdress = e
                    });
                });

            var testPerons = new Faker<DemoSource.Person>("pl")
                .RuleFor(p => p.Id, (f, p) => f.Random.Guid().ToString())
                .RuleFor(p => p.Name, (f, p) => f.Name.FullName())
                .RuleFor(p => p.Emails, (f, p) => testEmails.Generate(4));

            var testGroups = new Faker<DemoSource.Group>("pl")
                .RuleFor(g => g.Id, (f, g) => f.Random.Guid().ToString())
                .RuleFor(g => g.Label, (f, g) => f.Commerce.Department())
                .RuleFor(g => g.People, (f, g) => testPerons.Generate(1000));

            var groups = testGroups.Generate(40);

            var serchingEmails = new Faker().Random.ListItems(accounts.Select(x => x.EmailAdress.Email).ToList(), numberOfEmailsToSearch);

            return (groups, accounts, serchingEmails);
        }
    }
}