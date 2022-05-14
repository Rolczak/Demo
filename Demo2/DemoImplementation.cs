using System.Collections.Concurrent;

namespace DemoImplementation
{
    public static class ToDo
    {
        public static IEnumerable<(DemoSource.Account, DemoSource.Person)> MatchPersonToAccount(IEnumerable<DemoSource.Group> groups,
                                                                                        IEnumerable<DemoSource.Account> accounts,
                                                                                        IEnumerable<string> emails)
        {
            var accountsAndPersons = new ConcurrentBag<(DemoSource.Account, DemoSource.Person)>();

            //Flattening data helps searching performance with little bit cost of memory
            //I assumend that we will be matching many emails (I don't know why)
            //For matching small ammounts of emails previous version is little faster (Previous commit)
            //but when matchin more emails ex. 16k flattening structure will benefit a lot
            //Example with 16k emails and very unfortunate emails placement (tests with exactly same data)
            //without flattening: Matching completed in: 00:00:45.2234191 Memory: 119MB
            //with flattening: Matching completed in: 00:00:23.0623495 Memory: 130MB

            var peopleWithEmail = groups.AsParallel().SelectMany(x => x.People).SelectMany(p => p.Emails.Select(e => new { Person = p, Email = e })).ToList();

            Parallel.ForEach(emails, email =>
            {
                var account = accounts.Where(a => a.EmailAdress.Email == email).First();
                //Assuming that EmailAdress has same reference in Person and Account
                var person = peopleWithEmail.Where(pe => pe.Email == account.EmailAdress).Select(pe => pe.Person).First();
                accountsAndPersons.Add((account, person));
            });

            return accountsAndPersons;
        }
    }
}
