using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        private const int MaxQualityAllowed = 50;

        [Fact]
        public void Should_never_have_negative_quality()
        {
            var items = new List<Item>
            {
                new Item { Name = "Never Negative", SellIn = 1, Quality = 1 }
            };
            var app = new GildedRose(items);
            DaysPassed(1, app);
            Assert.Equal(0, items[0].Quality);
        }

        [Fact]
        public void Should_degrade_quality_twice_as_fast_once_sell_date_has_passed()
        {
            var items = new List<Item>
            {
                new Item { Name = "an item ", SellIn = 0, Quality = 2 },
                new Item { Name = "another item", SellIn = -1, Quality = 2 }
            };

            var app = new GildedRose(items);
            DaysPassed(1, app);
            Assert.Equal(0, items[0].Quality);
            Assert.Equal(0, items[1].Quality);
        }

        [Fact]
        public void Should_increase_quality_as_aged_brie_gets_older()
        {
            const int initialQuality = 1;
            var items = new List<Item>
            {
                new Item {Name = "Aged Brie", SellIn = 0, Quality = initialQuality }
            };

            var app = new GildedRose(items);
            DaysPassed(2, app);
            Assert.True(items[0].Quality > initialQuality);
        }

        [Fact]
        public void Should_have_max_quality_of_fifty()
        {
            var items = new List<Item>
            {
                new Item { Name = "Aged Brie", SellIn = 0, Quality = 40 }
            };

            var app = new GildedRose(items);
            DaysPassed(100, app);
            Assert.Equal(MaxQualityAllowed, items[0].Quality);
        }

        private static void DaysPassed(int totalDays, GildedRose app)
        {
            for (var days = 0; days < totalDays; days++)
            {
                app.UpdateQuality();
            }
        }
    }
}
