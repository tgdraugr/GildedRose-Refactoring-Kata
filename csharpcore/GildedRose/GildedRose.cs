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
            DegradeQualityOnCommonItem(item);

            switch (item.Name)
            {
                case "Aged Brie":
                case "Sulfuras, Hand of Ragnaros":
                    IncreaseQuality(item);
                    break;
                case "Backstage passes to a TAFKAL80ETC concert":
                    IncreaseQualityForBackstagePasses(item);
                    break;
            }

            UpdateSellInOnCommonItem(item);

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
                    default:
                        DegradeQualityOnCommonItem(item);
                        break;
                }
            }
        }

        private static void UpdateSellInOnCommonItem(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros")
            {
                item.SellIn -= 1;
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

        private static void DegradeQualityOnCommonItem(Item item)
        {
            if (item.Name != "Sulfuras, Hand of Ragnaros" && 
                item.Name != "Aged Brie" && 
                item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                DegradeQuality(item);
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