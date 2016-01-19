using NUnit.Framework;
using System;

namespace DogTrainingWindowsService.Tests.Unit
{
    [TestFixture]
    public class DogTrainingWindowsServiceTests
    {
        [Test]
        public void FirstUnitTestExample()
        {
            Assert.AreEqual(2 + 2, 4);
        }

        [Test]
        public void SecondUnitTestExample()
        {
            Assert.Throws<DivideByZeroException>(delegate {
                var x = 0;
                var divideMe = 10 / x;
            });
        }

        [Test]
        public void ThirdUnitTestExample()
        {
            var model = new DogBarkModel();
            Assert.NotNull(model);
        }
    }
}
