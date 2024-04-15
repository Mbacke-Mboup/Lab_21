
using Labo13.Controllers;
using Labo13.Data;
using Labo13.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;

namespace TestProject1
{
    public class GolfController_TestsUnitaires 
    {
        
        [Fact]
        public async Task ScoresTrou_VueModifTestAsync()
        {
            Mock<Labo13Context> mock = new Mock<Labo13Context>();
            List<ScoreTrou> scoreTrous = new List<ScoreTrou>()
            {
                new ScoreTrou()
                {
                    ScoreTrouId = 1,
                    Score = -1,
                    Terme = "birdie",
                    DateTrou = new DateTime(2023,12,1),
                    GolfeurId = 1,
                },

                 new ScoreTrou()
                {
                    ScoreTrouId = 2,
                    Score = 1,
                    Terme = "bogey",
                    DateTrou = new DateTime(2023,12,2),
                    GolfeurId = 1,
                },
            };
            mock.Setup(x => x.ScoreTrous).ReturnsDbSet(scoreTrous);

            GolfController golfController = new GolfController(mock.Object);
            IActionResult result = await golfController.ScoresTrou();

            ViewResult view = Assert.IsType<ViewResult>(result);
            List<ScoreTrou> model = Assert.IsAssignableFrom<List<ScoreTrou>>(view.Model);
            Assert.Equal(2, model.Count);

        }


        [Fact]
        public async Task DeleteScoreTrou_IdInvalidAsync()
        {
            Mock<Labo13Context> mock = new Mock<Labo13Context>();
            List<ScoreTrou> scoreTrous = new List<ScoreTrou>()
            {
                new ScoreTrou()
                {
                    ScoreTrouId = 1,
                    Score = -1,
                    Terme = "birdie",
                    DateTrou = new DateTime(2023,12,1),
                    GolfeurId = 1,
                },

                 new ScoreTrou()
                {
                    ScoreTrouId = 2,
                    Score = 1,
                    Terme = "bogey",
                    DateTrou = new DateTime(2023,12,2),
                    GolfeurId = 1,
                },
            };
            mock.Setup(x => x.ScoreTrous).ReturnsDbSet(scoreTrous);

            GolfController golfController = new GolfController(mock.Object);
            IActionResult result = await golfController.DeleteScoreTrou(3);

            RedirectToActionResult redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("ScoresTrou", redirect.ActionName);
            Assert.Equal(2, scoreTrous.Count);


        }
    }
}