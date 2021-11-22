using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class GildedRose
    {
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
                UpdateQuality(item);
            }
        }

        private void UpdateQuality(GildedRoseItem item)
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

        private void UpdateQualityOfCommon(GildedRoseItem item)
        {
            item.DegradeQuality();

            item.SetCloserToExpiration();

            if (item.IsExpired()) item.DegradeQuality();
        }

        private void UpdateQualityOfLegendaryItem(GildedRoseItem item)
        {
            item.IncreaseQuality();
        }

        private void UpdateQualityOfAgedBrie(GildedRoseItem item)
        {
            item.IncreaseQuality();

            item.SetCloserToExpiration();

            if (item.IsExpired()) item.IncreaseQuality();
        }

        private void UpdateQualityOfBackstagePasses(GildedRoseItem item)
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

            if (item.IsExpired()) item.ZeroOutQuality();
        }
    }
}