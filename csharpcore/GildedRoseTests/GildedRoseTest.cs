using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void Should_never_have_negative_quality()
        {
            var items = new List<Item>
            {
                new Item { Name = "Never Negative", SellIn = 1, Quality = 1 }
            };
            var app = new GildedRose(items);
            app.UpdateQuality();
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
            app.UpdateQuality();
            Assert.Equal(0, items[0].Quality);
            Assert.Equal(0, items[1].Quality);
        }
    }
}
