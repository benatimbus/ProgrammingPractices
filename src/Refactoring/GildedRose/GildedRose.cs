using System.Collections.Generic;

namespace GildedRose
{
    public interface IUpdateQuality
    {
        void UpdateQuality(Item item);
    }

    class DefaultUpdater : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn -= 1;

            if (item.Quality > 0)
            {
                item.Quality -= item.SellIn > 0 ? 1 : 2;
            }
        }
    }

    class AgedBrieUpdater : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn -= 1;
            if (item.Quality < 50)
            {
                item.Quality += 1;
            }
        }
    }

    class SulfurasUpdater : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
        }
    }

    class TicketsUpdater : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn -= 1;
            if (item.SellIn > 10)
            {
                item.Quality += 1;
            }
            if (item.SellIn <= 10 && item.SellIn > 5)
            {
                item.Quality += 2;
            }
            if (item.SellIn <= 5 && item.SellIn >= 0)
            {
                item.Quality += 3;
            }
            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }
    }

    class ConjuredUpdater : IUpdateQuality
    {
        public void UpdateQuality(Item item)
        {
            item.SellIn -= 1;

            if (item.Quality > 0)
            {
                item.Quality -= 2;
            }
        }
    }

    class GildedRose
    {
        private Dictionary<string, IUpdateQuality> _updater = new Dictionary<string, IUpdateQuality>();

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;

            _updater.Add("Default", new DefaultUpdater());
            _updater.Add("Aged Brie", new AgedBrieUpdater());
            _updater.Add("Sulfuras, Hand of Ragnaros", new SulfurasUpdater());
            _updater.Add("Backstage passes to a TAFKAL80ETC concert", new TicketsUpdater());
            _updater.Add("Conjured", new ConjuredUpdater());
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (_updater.ContainsKey(item.Name))
                {
                    _updater[item.Name].UpdateQuality(item);
                }
                else
                {
                    _updater["Default"].UpdateQuality(item);
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}

