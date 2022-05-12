namespace DemoTarget
{
    public class PersonWithEmail
    {
        // contains only characters from english alphabet
        // (a-z, both uppercase and lowercase) and numbers 0-9
        public string SanitizedNameWithId { get; set; } = null!;

        // to be formatted based on email and email type
        public string FormattedEmail { get; set; } = null!;
    }
}
