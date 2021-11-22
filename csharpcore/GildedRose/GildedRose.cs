using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class GildedRose
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string Legendary = "Sulfuras, Hand of Ragnaros";

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
                QualityUpdaterFor(item.Name).Invoke(item);
            }
        }

        private static Action<GildedRoseItem> QualityUpdaterFor(string itemName)
        {
            return itemName switch
            {
                AgedBrie => item => 
                {
                    item.IncreaseQuality();
                    item.SetCloserToExpiration();
                    if (item.IsExpired())
                    {
                        item.IncreaseQuality();
                    }
                },
                BackstagePasses => item =>
                {
                    item.IncreaseQuality();

                    if (item.IsNearExpirationBy(10))
                    {
                        item.IncreaseQuality();
                    }

                    if (item.IsNearExpirationBy(5))
                    {
                        item.IncreaseQuality();
                    }
                    
                    item.SetCloserToExpiration();
                    
                    if (item.IsExpired())
                    {
                        item.ZeroOutQuality();
                    }
                },
                Legendary => item => item.IncreaseQuality(),
                _ => item =>
                {
                    item.DegradeQuality();
                    item.SetCloserToExpiration();
                    if (item.IsExpired())
                    {
                        item.DegradeQuality();
                    }
                }
            };
        }
    }
}