using Labo13.Controllers;
using Labo13.Data;
using Labo13.Models;
using Labo13.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class GolfController_TestsIntegration : IClassFixture<BDTestFixture>
    {
        private BDTestFixture Fixture { get; }

        public GolfController_TestsIntegration(BDTestFixture fixture)
        {
            Fixture = fixture;
        }

        [Fact]
        public async Task Index_Test()
        {
            using Labo13Context context = Fixture.CreateContext();
            GolfController controller = new GolfController(context);

            IActionResult result = await controller.Index();
            ViewResult view = Assert.IsType<ViewResult>(result);
            List<VwDetailsScoreGolfeur> model = Assert.IsAssignableFrom<List<VwDetailsScoreGolfeur>>(view.Model);
        }

        [Fact]
        public async Task DeleteScoreTrou_Test()
        {
            using Labo13Context context = Fixture.CreateContext();
            int before = await context.ScoreTrous.CountAsync();
            context.Database.BeginTransaction();
            GolfController controller = new GolfController(context);

            IActionResult result = await controller.DeleteScoreTrou(2);
            context.ChangeTracker.Clear();
            Assert.Equal(before - 1, await context.ScoreTrous.CountAsync());


        }
        
        [Fact]
        public async Task CreateScoreTrou_Test()
        {
            using Labo13Context context = Fixture.CreateContext();
            int before = await context.ScoreTrous.CountAsync();
            context.Database.BeginTransaction();
            GolfController controller = new GolfController(context);
            CreerScoreTrouVM vM = new CreerScoreTrouVM();
            vM.NomGolfeur = "Tiger Woods";
            vM.Score = -2;
            IActionResult result = await controller.CreateScoreTrou(vM);
            context.ChangeTracker.Clear();
            Golfeur testing = await context.Golfeurs.FirstOrDefaultAsync(f => f.Nom == "Tiger Woods");
            Assert.Equal(before + 1, await context.ScoreTrous.CountAsync());
            Assert.Equal(-2, testing.ScoreTotal);
            Assert.Equal(6, testing.NbTrous);


        }
    }
}
