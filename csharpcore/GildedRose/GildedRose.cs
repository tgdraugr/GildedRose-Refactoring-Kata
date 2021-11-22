using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class GildedRose
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes";
        public const string Legendary = "Sulfuras, Hand of Ragnaros";
        public const string Conjured = "Conjured";

        private readonly IList<Item> _items;
        private readonly IEnumerable<GildedRoseItem> _gildedRoseItems;

        public GildedRose(IList<Item> items)
            : this(items.Select(item => new GildedRoseItem(item)))
        {
            _items = items;
        }

        private GildedRose(IEnumerable<GildedRoseItem> gildedRoseItems)
        {
            _gildedRoseItems = gildedRoseItems;
        }

        public void UpdateQuality()
        {
            foreach (var item in _gildedRoseItems)
            {
                SellInUpdaterFor(item.Name).Invoke(item);
                QualityUpdaterFor(item.Name).Invoke(item);
            }
        }

        private static Action<GildedRoseItem> SellInUpdaterFor(string itemName)
        {
            return itemName switch
            {
                Legendary => item => { },
                _ => item => item.SetCloserToExpiration()
            };
        }

        private static Action<GildedRoseItem> QualityUpdaterFor(string itemName)
        {
            return itemName switch
            {
                AgedBrie => item => 
                {
                    item.IncreaseQuality();
                    
                    if (item.IsExpired())
                    {
                        item.IncreaseQuality();
                    }
                },
                var name when name.Contains(BackstagePasses) => item =>
                {
                    if (item.IsNearExpirationBy(5))
                    {
                        item.IncreaseQuality(3);
                    }
                    else if (item.IsNearExpirationBy(10))
                    {
                        item.IncreaseQuality(2);
                    }
                    else
                    {
                        item.IncreaseQuality();
                    }
                    
                    if (item.IsExpired())
                    {
                        item.ZeroOutQuality();
                    }
                },
                Legendary => item => item.IncreaseQuality(),
                var name when name.Contains(Conjured) => item =>
                {
                    item.DegradeQuality(2);
                    
                    if (item.IsExpired())
                    {
                        item.DegradeQuality(2);
                    }
                },
                _ => item =>
                {
                    item.DegradeQuality();
                    
                    if (item.IsExpired())
                    {
                        item.DegradeQuality();
                    }
                },
            };
        }
    }
}