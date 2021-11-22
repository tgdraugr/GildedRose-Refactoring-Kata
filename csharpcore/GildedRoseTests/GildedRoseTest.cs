using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        private const string AgedBrie = "Aged Brie";
        private const string LegendaryItem = "Sulfuras, Hand of Ragnaros";
        private const string RandomItem = "Random";
        private const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";

        [Fact]
        public void Should_never_have_negative_quality()
        {
            var items = new List<Item>
            {
                new Item { Name = RandomItem, SellIn = 1, Quality = 1 }
            };
            var app = new GildedRose(items);
            DaysPassed(10, app);
            Assert.Equal(GildedRoseItem.MinQualityAllowed, items[0].Quality);
        }

        [Fact]
        public void Should_degrade_quality_twice_as_fast_once_sell_date_has_passed()
        {
            var items = new List<Item>
            {
                new Item { Name = RandomItem, SellIn = 0, Quality = 2 },
                new Item { Name = RandomItem, SellIn = -1, Quality = 2 }
            };

            var app = new GildedRose(items);
            DaysPassed(1, app);
            Assert.Equal(GildedRoseItem.MinQualityAllowed, items[0].Quality);
            Assert.Equal(GildedRoseItem.MinQualityAllowed, items[1].Quality);
        }

        [Fact]
        public void Should_increase_quality_as_aged_brie_gets_older()
        {
            const int initialQuality = 1;
            var items = new List<Item>
            {
                new Item { Name = AgedBrie, SellIn = 0, Quality = initialQuality }
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
                new Item { Name = AgedBrie, SellIn = 0, Quality = 40 }
            };

            var app = new GildedRose(items);
            DaysPassed(100, app);
            Assert.Equal(GildedRoseItem.MaxQualityAllowed, items[0].Quality);
        }

        [Fact]
        public void Should_keep_legendary_item_intact()
        {
            const int initialQuality = GildedRoseItem.MaxQualityAllowed + 20;
            
            var items = new List<Item>
            {
                new Item { Name = LegendaryItem, SellIn = 0, Quality = initialQuality }
            };

            var app = new GildedRose(items);
            DaysPassed(50, app);
            Assert.Equal(initialQuality, items[0].Quality);
        }

        [Fact]
        public void Should_increase_quality_as_concert_approaches()
        {
            var items = new List<Item>
            {
                new Item { Name = BackstagePasses, SellIn = 30, Quality = 10 }
            };

            var app = new GildedRose(items);
            DaysPassed(10, app);
            Assert.Equal(20, items[0].Quality);
        }
        
        [Theory]
        [InlineData(10, 2)]
        [InlineData(5, 3)]
        public void Should_increase_quality_by_different_rates_as_concert_approaches(int sellIn, int qualityFactor)
        {
            const int initialQuality = 10;
            const int daysGoneBy = 5;
            var qualityBump = qualityFactor * daysGoneBy;
            
            var items = new List<Item>
            {
                new Item { Name = BackstagePasses, SellIn = sellIn, Quality = initialQuality }
            };
            
            var app = new GildedRose(items);
            DaysPassed(daysGoneBy, app);
            Assert.Equal(initialQuality + qualityBump, items[0].Quality);
        }

        [Fact]
        public void Should_drop_quality_to_zero_after_concert()
        {
            var items = new List<Item>
            {
                new Item { Name = BackstagePasses, SellIn = 0, Quality = 10 }
            };

            var app = new GildedRose(items);
            DaysPassed(1, app);
            Assert.Equal(0, items[0].Quality);
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
