using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Movies.GetSingleExistingMovie;
using MediaRequest.Application.Queries.Movies.GetUpcoming;
using MediaRequest.Application.Queries.Movies.SearchMovieByName;
using MediaRequest.Application.Tests.Fixtures;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MediaRequest.Application.Tests
{
    public class RadarrTests
    {
        private IMediator _mediator;
        private IHttpHelper _httpHelper;
        private IConfigurationRoot configurationBuilder;
        private IMediaDbContext _mediaDbContext;

        private IOptions<ServicePath> _servicePath;
        private IOptions<ApiKeys> _apiKeys;
        private RadarrClient _radarrClient;

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

            _radarrClient = fixture.radarrClient;
        }

        [Test]
        public async Task GetExistingMoviesTest()
        {
            // Arrange
            var request = new GetExistingMoviesRequest();
            var handler = new GetExistingMoviesHandler(_radarrClient);

            GetExistingMoviesResponse response = null;

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


        [TestCase("577922")]
        [TestCase("0")]
        public async Task GetMovieMedia(string tmdbId)
        {
            // Arrange
            var request = new GetMovieMediaRequest(tmdbId);
            var handler = new GetMovieMediaHandler(_httpHelper);

            GetMovieMediaResponse response = null;

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


        [TestCase("1")]
        [TestCase("0")]
        public async Task GetSingleExistingMovieTest(string radarrMovieId)
        {
            // Arrange
            var request = new GetSingleExistingMovieRequest { RadarrMovieId = radarrMovieId };
            var handler = new GetSingleExistingMovieHandler(_httpHelper, _mediaDbContext, _servicePath, _apiKeys);

            GetSingleExistingMovieResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); }, "Parameter threw HttpRequestException error");
            }
        }

        [TestCase("641")]
        [TestCase("0")]
        public async Task GetSingleMovieTest(string tmdbId)
        {
            // Arrange
            var request = new GetSingleMovieRequest { TmdbId = tmdbId };
            var handler = new GetSingleMovieHandler(_httpHelper, _mediaDbContext, _mediator);

            GetSingleMovieResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); }, "Parameter threw HttpRequestException error");
            }
        }

        [TestCase(365)]
        [TestCase(null)]
        public async Task GetUpcomingTest(int days)
        {
            // Arrange
            var request = new GetUpcomingRequest { Days = days };
            var handler = new GetUpcomingHandler(_httpHelper, _servicePath);

            GetUpcomingResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); }, "Parameter threw HttpRequestException error");
            }
        }

        [TestCase("Iron Man")]
        [TestCase(null)]
        public async Task SearchMovieByNameTest(string searchTerm)
        {
            // Arrange
            var request = new SearchMovieByNameRequest { Keys = _apiKeys.Value, SearchTerm = searchTerm };
            var handler = new SearchMovieByNameHandler(_apiKeys, _servicePath);

            SearchMovieByNameResponse response = null;

            // Act
            try
            {
                response = await handler.Handle(request, new System.Threading.CancellationToken());
                Assert.NotNull(response);
            }
            catch (Exception)
            {
                Assert.ThrowsAsync<HttpRequestException>(async () => { await handler.Handle(request, new System.Threading.CancellationToken()); }, "Parameter threw HttpRequestException error");
            }
        }
    }
}
