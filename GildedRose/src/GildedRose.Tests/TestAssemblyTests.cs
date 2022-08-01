using System.Collections.Generic;

using GildedRose.Console;

using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        App instanceOfApp;

        static List<Item> items = new List<Item>
                                        {
                                            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                            new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20},
                                            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                        };

        Item plus5DexterityVest = items[0];
        Item agedBrie = items[1];
        Item elixirOfTheMongoose = items[2];
        Item sulfurasHandsOfRagnaros = items[3];
        Item backstagePassesToATAFKAL80ETCConcert = items[4];
        Item conjuredManaCake = items[5];

        [SetUp]
        public void SetUp()
        {
            instanceOfApp = new App(items);
        }

        [Test]
        public void TestTheTruth()
        {
            SetUp();

            Assert.True(true);
        }

        public void Dispose()
        {
            // Any clean up code goes here
        }

        [TestCase(1)]
        public void Plus5DexterityVestDegradesByOneQualityForFirstTenDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                instanceOfApp.UpdateQuality();
            }

            int expectedQuality = 20 - days;

            Assert.True(plus5DexterityVest.Quality == expectedQuality);
        }

        [Test]
        public void PlusFiveDexterityVestDegradesByTwoQualityEveryDayAfterTenDays()
        {

        }
    }
}