using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries.Television;
using MediaRequest.Application.Tests.Fixtures;
using MediaRequest.Domain.API_Responses.Sonarr;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace MediaRequest.Application.Tests
{
    public class SonarrTests
    {
        private IOptions<ServicePath> _servicePath;
        private IOptions<ApiKeys> _apiKeys;

        private SonarrClient _sonarrClient;

        [SetUp]
        public void Setup()
        {
            var fixture = new ConfigurationFixture();

            _servicePath = fixture.ServicePath;
            _apiKeys = fixture.ApiKeys;
            _sonarrClient = fixture.sonarrClient;
        }

        [Test]
        public async Task GetAllSeriesTest()
        {
            // Assert
            var request = new GetAllSeriesRequest();
            var handler = new GetAllSeriesHandler(_apiKeys, _servicePath, _sonarrClient);

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.Greater(response.Series.Count(), 0);
        }

        [Test]
        public async Task GetSingleSeriesTest()
        {
            // Arrange
            var request = new GetSingleSeriesRequest(1);
            var handler = new GetSingleSeriesHandler(_apiKeys, _servicePath, _sonarrClient);
            var response = new GetSingleSeriesResponse();

            // Act
            response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response.TvShow);
        }

        [Test]
        public async Task GetSingleEpisodeTest()
        {
            // Arrange
            var request = new GetSingleEpisodeRequest();
            var handler = new GetSingleEpisodeHandler(_apiKeys, _servicePath, _sonarrClient);

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());
            
            // Assert
            Assert.NotNull(response.Episode);
        }
    }
}
