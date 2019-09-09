using j_crud_son.Tests;
using Moq;
using NUnit.Framework;

namespace j_crud_son.UnitTest
{
    [TestFixture]
    public class ComuneServiceTests
    {
        private Comune _comuneTest;

        [SetUp]
        public void SetUp()
        {
            _comuneTest = new Comune
            {
                Id = 7,
                Code = "Bolo",
                Description = "ByNight",
                Order = 1,
                NotActive = "false"
            };
            
            var comuneService = new Mock<IComuneService>();
        }
        
        [Test]
        public void NextId_WhenCalled_ReturnTheCorrectJToken()
        {
            Assert.True(true);
        }
    }
}