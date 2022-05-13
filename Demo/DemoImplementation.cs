using System.Text.RegularExpressions;

namespace DemoImplementation
{
    public static class DemoImplementation
    {
        public static IEnumerable<DemoTarget.PersonWithEmail> Flatten(IEnumerable<DemoSource.Person> people)
        {
            //Migrating/Exporting data to other system etc. 
            //After sanitizing data we are loosing some information (polish letters for example), so we cannot recreate orginal data with them
            var sanitizeRegex = new Regex("[^a-zA-Z0-9]");
            return people.SelectMany(p => p.Emails.Select(e => new DemoTarget.PersonWithEmail()
            {
                SanitizedNameWithId = sanitizeRegex.Replace($"{p.Name}{p.Id}", string.Empty),
                FormattedEmail = $"[{e.EmailType}]{e.Email}"
            }));
        }
    }
}
