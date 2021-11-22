using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var items = Items();
            var app = new GildedRose(items);

            Console.WriteLine("OMGHAI!");
            
            foreach (var day in Enumerable.Range(1, 30))
            {
                PrintSnapshotForDay(day, items);
                app.UpdateQuality();   
            }
        }

        private static void PrintSnapshotForDay(int day, IEnumerable<Item> items)
        {
            Console.WriteLine("-------- day " + day + " --------");
            Console.WriteLine("name, sellIn, quality");
            
            foreach (var item in items)
            {
                Console.WriteLine(item.Name + ", " + item.SellIn + ", " + item.Quality);
            }

            Console.WriteLine("");
        }

        private static List<Item> Items()
        {
            return new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80 },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 5,
                    Quality = 49
                },
                // this conjured item does not work properly yet
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };
        }
    }
}