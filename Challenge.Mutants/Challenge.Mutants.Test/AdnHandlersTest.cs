using Challenge.Mutants.Application.Handlers;
using Challenge.Mutants.Application.Mappers;
using Challenge.Mutants.Application.Models.Request;
using Challenge.Mutants.Application.Services;
using Challenge.Mutants.Domain.Contexts;
using Challenge.Mutants.Domain.Entities;
using Challenge.Mutants.Infrastructure.Bootstrapers;
using Challenge.Mutants.Infrastructure.Data;
using Challenge.Mutants.Infrastructure.Services;
using NUnit.Framework;

namespace Challenge.Mutants.Test
{
    [TestFixture]
    public class AdnHandlersTest
    {
        private SaveADNHandler handlerSaveAdn;
        private SaveADNRequest requestSaveAdn;
        private GetStatsADNHandler handlerGetStats;
        private GetStatsADNRequest requestGetStats;

        private SaveADNModel SaveADNModel;

        [SetUp]
        public void Setup()
        {
            var dbContext = new ChallengeDbContext();

            handlerSaveAdn = new SaveADNHandler(
                new CustomLogger(new string[] { "Debug", "Information", "Warning", "Error" }, "C:\\Log\\"),
                new Repository<ChallengeDbContext, ADNEntity>(dbContext, false),
                new UnitOfWork<ChallengeDbContext>(dbContext, new QueryManager(), new AppUserService()),
                new AdnService());

            SaveADNModel = new Application.Models.Request.SaveADNModel
            {
                Dna = new string[] { "ATGCGA", "AGCACA", "TCTAAT", "CGTAGG", "CGTACA", "ATTCCC" }
            };

            requestSaveAdn = new SaveADNRequest(SaveADNModel);

            handlerGetStats = new GetStatsADNHandler(
                 new CustomLogger(new string[] { "Debug", "Information", "Warning", "Error" }, "C:\\Log\\"),
                  new Repository<ChallengeDbContext, ADNEntity>(dbContext, false),
                  new ADNStatsMapper());
        }

        [Test]
        public void HandleSaveAdn()
        {
            var result = handlerSaveAdn.Handle(requestSaveAdn, new System.Threading.CancellationToken()).Result;
            Assert.Pass();
        }

        [Test]
        public void HandleGetStats()
        {
            var result = handlerGetStats.Handle(requestGetStats, new System.Threading.CancellationToken()).Result;
            Assert.Pass();
        }

        [Test]
        public void IsMutant()
        {
            var result = new AdnService().IsMutant(SaveADNModel);
            Assert.Pass();
        }
    }
}