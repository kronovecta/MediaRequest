using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Commands;
using MediaRequest.Application.Commands.ApproveRequest;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Requests;
using MediaRequest.Application.Queries.Requests.GetSingleRequest;
using MediaRequest.Application.Queries.Requests.GetUserRequests;
using MediaRequest.Application.Tests.Fixtures;
using MediaRequest.Data;
using MediaRequest.Domain;
using MediaRequest.Domain.Configuration;
using MediaRequest.Domain.Radarr;
using MediaRequest.WebUI.Models.IdentityModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MediaRequest.Application.Tests
{
    public class RequestTests
    {
        private IMediator _mediator;
        private IHttpHelper _httpHelper;
        private IConfigurationRoot configurationBuilder;
        private IMediaDbContext _mediaDbContext;

        private ApplicationUser user;
        private RadarrClient _radarrClient;

        [SetUp]
        public async Task Setup()
        {
            #region Configuration classes

            var fixture = new ConfigurationFixture();
            configurationBuilder = fixture.GenerateConfiguration();
            _radarrClient = fixture.radarrClient;

            #endregion

            #region Database Con++text

            _mediaDbContext = fixture.GetDbContext();
            if(!_mediaDbContext.Request.Any())
            {
                _mediaDbContext.Request.Add(new Domain.UserRequest { Id = 1, MovieId = "577922", UserId = "bacbb67d-819e-4e7b-bb29-c81ff99b5d1d" });
                await _mediaDbContext.SaveChangesAsync();
            }

            #endregion

            #region Http Helper
            _httpHelper = new HttpHelper(fixture.ServicePath, fixture.ApiKeys, fixture.radarrClient, fixture.tmdbClient);
            #endregion

            _mediator = A.Fake<IMediator>();
        }

        [TestCase("bacbb67d-819e-4e7b-bb29-c81ff99b5d1d", ExpectedResult = true)]
        [TestCase("b039d8bb-26c5-40d7-aafd-a1a2a93642d6", ExpectedResult = false)]
        public async Task<bool> GetSingleRequest(string userid)
        {
            // Arrange
            var movieHandler = new GetSingleMovieHandler(_radarrClient);
            var movieRequest = new GetSingleMovieRequest { TmdbId = "577922" };
            var movieResponse = await movieHandler.Handle(movieRequest, new System.Threading.CancellationToken());

            var handler = new RequestExistsHandler(_mediaDbContext);
            var request = new RequestExistsRequest { Movie = movieResponse.Movie, UserId = userid };

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            return response.Exists;
        }

        [Test]
        public async Task AddRequestTest()
        {
            // Arrange
            var command = new AddRequestCommand { Request = new Domain.UserRequest { Id = 2, MovieId = "577922", UserId = "bacbb67d-819e-4e7b-bb29-c81ff99b5d1d" } };
            var handler = new AddRequestHandler(_mediaDbContext);

            // Act
            var response = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            var fetchedRequest = _mediaDbContext.Request.First();
            Assert.IsNotNull(fetchedRequest);
        }

        [TestCase("bacbb67d-819e-4e7b-bb29-c81ff99b5d1d", ExpectedResult = 1)]
        [TestCase("6a4726ec-2416-40db-842c-68be9258744f", ExpectedResult = 0)]
        public async Task<int> GetUserRequests(string userId)
        {
            // Arrange
            var request = new GetUserRequestRequest(userId);
            var handler = new GetUserRequestHandler(_mediaDbContext);

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            return response.Requests.Count;
        }

        [Test]
        public async Task GetRequestsTest()
        {
            // Arrange
            var request = new GetRequestsRequest();
            var handler = new GetRequestsHandler(_mediaDbContext);
            var expected = 1;

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.AreEqual(expected, response.Requests.Count);
        }
    }
}