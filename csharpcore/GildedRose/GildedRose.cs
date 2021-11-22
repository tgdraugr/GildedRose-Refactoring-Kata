using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private readonly IList<Item> _items;
        private const int MinQualityAllowed = 0;
        private const int MaxQualityAllowed = 50;

        public GildedRose(IList<Item> items)
        {
            _items = items;
        }

        public void UpdateQuality()
        {
            foreach (var item in _items)
            {
                UpdateQuality(item);
            }
        }

        private static void UpdateQuality(Item item)
        {
            switch (item.Name)
            {
                case "Backstage passes to a TAFKAL80ETC concert":
                    UpdateQualityOfBackstagePasses(item);
                    break;
                
                case "Aged Brie":
                    UpdateQualityOfAgedBrie(item);
                    break;
                
                case "Sulfuras, Hand of Ragnaros":
                    UpdateQualityOfLegendaryItem(item);
                    break;
                
                default:
                    UpdateQualityOfCommon(item);
                    break;
            }
        }

        private static void UpdateQualityOfCommon(Item item)
        {
            DegradeQuality(item);
            
            item.SellIn -= 1;
            
            if (IsExpired(item)) 
                DegradeQuality(item);
        }

        private static bool IsExpired(Item item)
        {
            return item.SellIn < 0;
        }

        private static void UpdateQualityOfLegendaryItem(Item item)
        {
            IncreaseQuality(item);
        }

        private static void UpdateQualityOfAgedBrie(Item item)
        {
            IncreaseQuality(item);
            
            item.SellIn -= 1;
            
            if (IsExpired(item)) 
                IncreaseQuality(item);
        }

        private static void UpdateQualityOfBackstagePasses(Item item)
        {
            IncreaseQuality(item);
                
            if (item.SellIn <= 10)
            {
                IncreaseQuality(item);
            }

            if (item.SellIn <= 5)
            {
                IncreaseQuality(item);
            }

            item.SellIn -= 1;
            
            if (IsExpired(item)) 
                item.Quality -= item.Quality;
        }

        private static void DegradeQuality(Item item)
        {
            if (item.Quality > MinQualityAllowed)
            {
                item.Quality -= 1;
            }
        }

        private static void IncreaseQuality(Item item)
        {
            if (item.Quality < MaxQualityAllowed)
            {
                item.Quality += 1;
            }
        }
    }
}