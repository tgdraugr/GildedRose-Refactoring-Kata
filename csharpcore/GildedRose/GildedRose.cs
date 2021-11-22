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
                    IncreaseQualityForBackstagePasses(item);
                    item.SellIn -= 1;
                    break;
                
                case "Aged Brie":
                    IncreaseQuality(item);
                    item.SellIn -= 1;
                    break;
                
                case "Sulfuras, Hand of Ragnaros":
                    IncreaseQuality(item);
                    break;
                
                default:
                    DegradeQuality(item);
                    item.SellIn -= 1;
                    break;
            }
            
            if (item.SellIn < 0)
            {
                switch (item.Name)
                {
                    case "Backstage passes to a TAFKAL80ETC concert":
                        item.Quality -= item.Quality;
                        break;
                    
                    case "Aged Brie":
                        IncreaseQuality(item);
                        break;
                    
                    case "Sulfuras, Hand of Ragnaros":
                        break;

                    default:
                        DegradeQuality(item);
                        break;
                }
            }
        }

        private static void IncreaseQualityForBackstagePasses(Item item)
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