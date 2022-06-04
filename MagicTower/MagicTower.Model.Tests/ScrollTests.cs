using System;
using MagicTower.Model.EnemiesModels;
using MagicTower.Model.Items;
using NUnit.Framework;

namespace MagicTower.Model.Tests
{
    [TestFixture]
    public class ScrollTests
    {
        [Test]
        public void ScrollCantGetNotMagicType()
        {
            Assert.Throws<ArgumentException>(() => new Scroll(1, 1, typeof(Demon)));
        }
    }
}