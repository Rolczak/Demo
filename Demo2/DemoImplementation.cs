using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace DemoImplementation
{
    public static class ToDo
    {
        public static IEnumerable<(DemoSource.Account, DemoSource.Person)> MatchPersonToAccount(IEnumerable<DemoSource.Group> groups,
                                                                                        IEnumerable<DemoSource.Account> accounts,
                                                                                        IEnumerable<string> emails)
        {
            var accountsAndPersons = new ConcurrentBag<(DemoSource.Account, DemoSource.Person)>();

            Parallel.ForEach(emails, email =>
            {
                var account = accounts.Where(a => a.EmailAdress.Email == email).First();
                var person = groups.SelectMany(x => x.People).Where(x => x.Emails.Any(x => x == account.EmailAdress)).First();
                accountsAndPersons.Add((account, person));
            });

            return accountsAndPersons;
        }
    }
}
