namespace GildedRoseKata
{
    public class GildedRoseItem
    {
        public const int MinQualityAllowed = 0;
        public const int MaxQualityAllowed = 50;
        private const int QualityUnit = 1;

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

        public void DegradeQuality(int factor = QualityUnit)
        { 
            var target = _item.Quality - QualityUnit * factor;
            while (_item.Quality != target && _item.Quality > MinQualityAllowed)
                _item.Quality -= QualityUnit;
        }
            
        public void IncreaseQuality(int factor = QualityUnit)
        {
            var target = _item.Quality + QualityUnit * factor;
            while (_item.Quality != target && _item.Quality < MaxQualityAllowed)
                _item.Quality += QualityUnit;
        }

        public void ZeroOutQuality()
        {
            _item.Quality -= _item.Quality;
        }

        public bool IsNearExpirationBy(int totalDays)
        {
            return _item.SellIn < totalDays;
        }

        public bool IsExpired()
        {
            return _item.SellIn < 0;
        }
    }
}