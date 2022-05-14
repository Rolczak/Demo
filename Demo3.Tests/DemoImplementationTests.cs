using Bogus;
using DemoImplementation;
using System.Diagnostics;

namespace Demo3.Tests
{
    public class DemoImplementationTests
    {
        [Fact]
        public void OnlyBigCollectionsShouldBeFasterThanIteratingAllItems()
        {
            var context = new OnlyBigCollectionTestContext();
            var watch = new Stopwatch();

            var testData = context.GetTestData();
            var testData1 = testData.ToList();
            var testData2 = testData.ToList();
            watch.Start();
            var result = DemoImplementation.DemoImplementation.OnlyBigCollections(testData1);
            watch.Stop();
            var fastImplementationTime = watch.Elapsed;

            watch.Restart();

            var regularImplResult = OnlyBigCollectionTestContext.RegularOnlyBigCollectionsImplementation(testData2);
            watch.Stop();
            var regularImplementionTime = watch.Elapsed;

            Assert.True(fastImplementationTime < regularImplementionTime);
            Assert.Equal(regularImplResult.Count(), result.Count());
        }
    }

    public class OnlyBigCollectionTestContext
    {
        private readonly Faker _faker;
        public OnlyBigCollectionTestContext()
        {
            Randomizer.Seed = new Random(10);
            _faker = new Faker();

        }

        public List<IEnumerable<string>> GetTestData()
        {
            var list = new List<IEnumerable<string>>();
            for(int i = 0; i < 1; i++) {
                list.Add(GetData());
            }
            return list;
        }

        private IEnumerable<string> GetData()
        {
            var itemNumber = _faker.Random.Int(1, 1000);
            for(int i = 1; i <= itemNumber; i++)
            {
                yield return "testString";
            }
        }

        public static IEnumerable<IEnumerable<string>> RegularOnlyBigCollectionsImplementation(List<IEnumerable<string>> toFilter)
        {
            Func<IEnumerable<string>, bool> predicate =
                list => list.Count() > 5;

            return toFilter.Where(predicate);
        }
    }
}