using System;
using NUnit.Framework;
using System.Collections.Generic;
using Shouldly;

namespace GildedRose
{
	[TestFixture]
	public class GildedRoseTest
	{
        private Item simple;
        private Item agedBrie;
        private Item sulfuras;
        private Item tickets;
        private Item conjured;
        private GildedRose rose;

        [SetUp]
        public void SetUp()
        {
            simple = new Item { Name = "Simple", SellIn = 10, Quality = 5 };
            agedBrie = new Item { Name = "Aged Brie", SellIn = 6, Quality = 5 };
            sulfuras = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 50, Quality = 80 };
            tickets = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 };
            conjured = new Item { Name = "Conjured", SellIn = 11, Quality = 10 };

            rose = new GildedRose(new List<Item> { simple, agedBrie, sulfuras, tickets });
        }

		[Test]
		public void When_UpdateQuality_Is_Called_For_A_Simple_Item_SellIn_And_Quality_Is_Decreased() {
			rose.UpdateQuality();

            simple.Name.ShouldBe("Simple");
            simple.SellIn.ShouldBe(9);
            simple.Quality.ShouldBe(4);
		}

        [Test]
        public void When_UpdateQuality_Is_Called_And_SellIn_Is_0_The_Quality_Degrades_Twice_As_Fast()
        {
            simple.SellIn = 0;
            rose.UpdateQuality();

            simple.SellIn.ShouldBeLessThan(0);
            simple.Quality.ShouldBe(3);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_And_The_Quality_Is_0_It_Does_Not_Degrade()
        {
            simple.Quality = 0;
            rose.UpdateQuality();

            simple.SellIn.ShouldBe(9);
            simple.Quality.ShouldBe(0);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_And_The_Item_Is_Aged_Brie_The_Quality_Increases_And_The_SellIn_Date_Decreases()
        {
            rose.UpdateQuality();

            agedBrie.SellIn.ShouldBe(5);
            agedBrie.Quality.ShouldBe(6);
        }

        [Test]
        public void When_Update_Quality_Is_Called_It_Does_Not_Increase_The_Quality_Above_50()
        {
            agedBrie.Quality = 49;

            rose.UpdateQuality();
            agedBrie.SellIn.ShouldBe(5);
            agedBrie.Quality.ShouldBe(50);

            rose.UpdateQuality();
            agedBrie.SellIn.ShouldBe(4);
            agedBrie.Quality.ShouldBe(50);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_Sulfuras_SellIn_And_Quality_Is_Never_Decreased()
        {
            rose.UpdateQuality();
            sulfuras.SellIn.ShouldBe(50);
            sulfuras.Quality.ShouldBe(80);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_BackStageTicket_Quality_Increases_By_1_If_SellIn_Larger_Than_10()
        {
            rose.UpdateQuality();
            tickets.SellIn.ShouldBe(10);
            tickets.Quality.ShouldBe(11);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_BackStageTicket_Quality_Increases_By_2_If_SellIn_Between_10_and_6()
        {
            tickets.SellIn = 10;

            rose.UpdateQuality();
            tickets.SellIn.ShouldBe(9);
            tickets.Quality.ShouldBe(12);

        }

        [Test]
        public void When_UpdateQuality_Is_Called_BackStageTicket_Quality_Increases_By_3_If_SellIn_Between_5_and_0()
        {
            tickets.SellIn = 1;

            rose.UpdateQuality();
            tickets.SellIn.ShouldBe(0);
            tickets.Quality.ShouldBe(13);
        }

        [Test]
        public void When_UpdateQuality_Is_Called_BackStageTicket_Quality_Drop_To_Zero_If_SellIn_Reached()
        {
            tickets.SellIn = 0;

            rose.UpdateQuality();
            tickets.SellIn.ShouldBeLessThan(0);
            tickets.Quality.ShouldBe(0);
        }

        [Test, Explicit]
        public void When_UpdateQuality_Is_Called_Conjured_Items_Quality_Degrade_Twice_As_Fast()
        {
            rose.UpdateQuality();
            conjured.SellIn.ShouldBe(10);
            conjured.Quality.ShouldBe(8);
        }
    }
}

