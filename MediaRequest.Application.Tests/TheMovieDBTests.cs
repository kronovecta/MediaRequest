using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetTMDBContent;
using MediaRequest.Application.Queries.People.GetCombinedMedia;
using MediaRequest.Application.Queries.People.GetPopularMovies;
using MediaRequest.Application.Tests.Fixtures;
using MediaRequest.Data;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaRequest.Application.Tests
{
    public class TheMovieDBTests
    {
        private IMediator _mediator;
        private IHttpHelper _httpHelper;
        private IConfigurationRoot configurationBuilder;
        private IMediaDbContext _mediaDbContext;

        private IOptions<ServicePath> _servicePath;
        private IOptions<ApiKeys> _apiKeys;

        [SetUp]
        public void Setup()
        {
            #region Configuration classes

            var fixture = new ConfigurationFixture();
            configurationBuilder = fixture.GenerateConfiguration();
            #endregion

            _httpHelper = new HttpHelper(fixture.ServicePath, fixture.ApiKeys, fixture.radarrClient, fixture.tmdbClient);
            _mediator = A.Fake<IMediator>();
            _mediaDbContext = fixture.GetDbContext();

            _servicePath = fixture.ServicePath;
            _apiKeys = fixture.ApiKeys;
        }

        [TestCase("577922")]
        public async Task GetMovieTest(string tmdbId)
        {
            // Arrange
            var handler = new GetSingleMovieHandler(_httpHelper, _mediaDbContext, _mediator);
            var request = new GetSingleMovieRequest { TmdbId = tmdbId };

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response.Movie);
        }

        [TestCase(577922)]
        [TestCase(0)]
        public async Task GetMediaTest(int tmdbId)
        {
            // Arrange
            var request = new GetTMDBMediaRequest(tmdbId);
            var handler = new GetTMDBMediaHandler(_httpHelper);

            GetTMDBMediaResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); });
            }
        }

        [TestCase("577922")]
        [TestCase("0")]
        public async Task GetRecommendedTest(string tmdbId)
        {
            // Arrange
            var request = new GetRecommendedRequest { TMDBId = tmdbId };
            var handler = new GetRecommendedHandler(_httpHelper, _servicePath);

            GetRecommendedResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); });
            }
        }

        [TestCase("577922")]
        [TestCase("0")]
        public async Task GetCreditsTest(string tmdbId)
        {
            // Arrange
            var request = new GetCreditsRequest { TMDBId = tmdbId };
            var handler = new GetCreditsHandler(_httpHelper);

            GetCreditsResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<GetCreditsException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); });
            }
        }

        [Test]
        public async Task GetPopularMovies()
        {
            // Arrange
            var request = new GetPopularMoviesRequest();
            var handler = new GetPopularMoviesHandler(_httpHelper);

            GetPopularMoviesResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); });
            }
        }

        [TestCase("49")]
        [TestCase(null)]
        public async Task GetCombinedMediaTest(string actorId)
        {
            // Arrange
            var request = new GetCombinedMediaRequest { ActorID = actorId };
            var handler = new GetCombinedMediaHandler(_httpHelper, _servicePath, _apiKeys);

            GetCombinedMediaResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<GetCombinedMediaException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); });
            }
        }
    }
}
