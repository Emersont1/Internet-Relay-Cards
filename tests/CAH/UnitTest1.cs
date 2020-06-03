using NUnit.Framework;
using LibCAH;
using System;

namespace CAH
{
    public class Tests
    {
        Card.Collection collection;
        [SetUp]
        public void Setup()
        {
            collection = Card.Load.XyxxyFromHttp("https://raw.githubusercontent.com/ajanata/PretendYoureXyzzy/master/cah_cards.sql");
        }

        [Test]
        public void Test1()
        {
            Game.Instance instance = new Game.Instance(collection.WhiteCards, collection.BlackCards);
            Card.White c = instance.DrawCard();
            Console.WriteLine(c.Text);
            Assert.Pass();
        }
    }
}