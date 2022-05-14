using Bogus;

namespace Demo3.Tests
{
    public class DemoImplementationTests
    {
        [Fact]
        public void OnlyBigCollectionsShouldReturnValidCollections()
        {
            var testData = new List<IEnumerable<string>>();
            Randomizer.Seed = new Random(10);
            var faker = new Faker();
            for(int i = 0; i < 100; i++)
            {
                var itemNumber = faker.Random.Int(1, 100);
                testData.Add(GetData(itemNumber));
            }
            //to make sure that in list will be item that is not BigCollection
            testData.Add(GetData(5));

            var result = DemoImplementation.DemoImplementation.OnlyBigCollections(testData);

            Assert.All(result, x => Assert.True(x.Count() > 5));
        }

        private IEnumerable<string> GetData(int number)
        {
            for(int i = 0; i < number; i++)
            {
                yield return i.ToString();
            }
        }
    }
}