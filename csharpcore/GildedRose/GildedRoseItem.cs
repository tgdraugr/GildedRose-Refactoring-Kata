namespace GildedRoseKata
{
    public class GildedRoseItem
    {
        public const int MinQualityAllowed = 0;
        public const int MaxQualityAllowed = 50;
            
        private readonly Item _item;

        protected internal GildedRoseItem(Item item)
        {
            _item = item;
        }

        public string Name => _item.Name;

        public void SetCloserToExpiration()
        {
            _item.SellIn -= 1;
        }

        public void DegradeQuality()
        {
            if (_item.Quality > MinQualityAllowed)
            {
                _item.Quality -= 1;
            }
        }
            
        public void IncreaseQuality()
        {
            if (_item.Quality < MaxQualityAllowed)
            {
                _item.Quality += 1;
            }
        }

        public void ZeroOutQuality()
        {
            _item.Quality -= _item.Quality;
        }

        public bool IsNearExpirationBy(int totalDays)
        {
            return _item.SellIn <= totalDays;
        }

        public bool IsExpired()
        {
            return _item.SellIn < 0;
        }
    }
}