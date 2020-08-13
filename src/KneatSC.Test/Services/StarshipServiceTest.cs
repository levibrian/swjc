using System.Threading.Tasks;
using KneatSC.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KneatSC.Test.Services
{
    [TestClass]
    public class StarshipServiceTest
    {
        private readonly IStarshipService starshipService;

        public StarshipServiceTest()
        {
            starshipService = new StarshipService();
        }

        [TestMethod]
        public async Task GetByPageTest()
        {
            var results = await starshipService.Get(1, 15000);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GetAllTest()
        {
            var results = await starshipService.GetAll(15000);

            Assert.IsNotNull(results);
        }

        [TestMethod]
        public async Task GetStarshipsWithJumpCalculationsTest()
        {
            var distanceInMGLTS = 150;
            var starshipCollection = await starshipService.GetAll(distanceInMGLTS);

            Assert.IsNotNull(starshipCollection);

            foreach (var starship in starshipCollection)
            {
                Assert.AreNotEqual(starship.JumpCount, 0);
            }
        }
    }
}
