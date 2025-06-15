using WatchLibrary;
using LAB12;
namespace UnitTest14
{
    [TestClass]
    public sealed class Test1
    {
        private List<Dictionary<string, Watch>> _watchShopNetwork;

        [TestInitialize]
        public void Setup()
        {
            _watchShopNetwork = new List<Dictionary<string, Watch>>
            {
                new Dictionary<string, Watch>
                {
                    { "Model-001", new ElectronicWatch("Casio", 2020, "LCD") },
                    { "Model-002", new AnalogWatch("Rolex", 2019, "Люкс") },
                    { "Model-003", new SmartWatch("Apple", 2023, "OLED", "WatchOS", true) }
                },
                new Dictionary<string, Watch>
                {
                    { "Model-004", new ElectronicWatch("Garmin", 2022, "AMOLED") },
                    { "Model-005", new AnalogWatch("Omega", 2021, "Дайверский") }
                }
            };
        }

        [TestMethod]
        public void Test_Where_ElectronicWatchesWithOledDisplay()
        {
            // LINQ-синтаксис
            var oledWatches1 =
                from shop in _watchShopNetwork
                from pair in shop
                where pair.Value is ElectronicWatch ew && ew.TypeOfDisplay == "OLED"
                select pair.Value;

            // Методы расширения
            var oledWatches2 = _watchShopNetwork
                .SelectMany(shop => shop)
                .Where(pair => pair.Value is ElectronicWatch ew && ew.TypeOfDisplay == "OLED")
                .Select(pair => pair.Value);

            Assert.AreEqual(1, oledWatches1.Count());
            Assert.AreEqual(1, oledWatches2.Count());
        }

        [TestMethod]
        public void Test_Union_UniqueWatchesFromTwoShops()
        {
            var unionWatches1 = _watchShopNetwork[0].Values.Union(_watchShopNetwork[1].Values);
            Assert.AreEqual(5, unionWatches1.Count());
        }

        [TestMethod]
        public void Test_AverageYearOfSmartWatches()
        {
            var avgYear = _watchShopNetwork
                .SelectMany(shop => shop.Values)
                .OfType<SmartWatch>()
                .Average(sw => sw.YearOfManufacture);

            Assert.AreEqual(2023, avgYear);
        }

        [TestMethod]
        public void Test_GroupBy_Brand()
        {
            var groupByBrand = _watchShopNetwork
                .SelectMany(shop => shop.Values)
                .GroupBy(watch => watch.Brand);

            Assert.AreEqual(5, groupByBrand.Count());
        }
    }
    [TestClass]
    public class MyCollectionTests
    {
        private MyCollection<Watch> _watchCollection;

        [TestInitialize]
        public void Setup()
        {
            _watchCollection = new MyCollection<Watch>(10); // 10 случайных часов
        }

        [TestMethod]
        public void Test_Where_ElectronicWatches()
        {
            var electronicWatches = _watchCollection.Where(w => w is ElectronicWatch);
            Assert.IsTrue(electronicWatches.Any());
        }

        [TestMethod]
        public void Test_Count_AnalogWatches()
        {
            var analogCount = _watchCollection.Count(w => w is AnalogWatch);
            Assert.IsTrue(analogCount > 0);
        }

        [TestMethod]
        public void Test_AverageYearOfManufacture()
        {
            var avgYear = _watchCollection.Average(w => w.YearOfManufacture);
            Assert.IsTrue(avgYear >= 1806 && avgYear <= DateTime.Now.Year);
        }

        [TestMethod]
        public void Test_GroupBy_Type()
        {
            var groupByType = _watchCollection.GroupBy(w => w.GetType().Name);
            Assert.IsTrue(groupByType.Count() >= 1); // Минимум 1 тип часов
        }
    }
}
