using System.Diagnostics;

namespace DemoImplementation
{
    public static class DemoImplementation
    {
        public static IEnumerable<IEnumerable<string>> OnlyBigCollections(List<IEnumerable<string>> toFilter)
        {
            Func<IEnumerable<string>, bool> predicate =
                list => list.Skip(5).Any();

            return toFilter.Where(predicate);
        }
    }
}
