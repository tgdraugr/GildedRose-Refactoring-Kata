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
    }
}
