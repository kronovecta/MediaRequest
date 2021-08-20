using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Application.Queries.Requests.GetSingleRequest;
using MediaRequest.Application.Tests.Fixtures;
using MediaRequest.Data;
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

        [SetUp]
        public async Task Setup()
        {
            #region Configuration classes

            var fixture = new ConfigurationFixture();
            configurationBuilder = fixture.GenerateConfiguration();

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
        public async Task<bool> GetExistingRequest(string userid)
        {
            // Arrange
            var movieHandler = new GetSingleMovieHandler(_httpHelper, _mediaDbContext, _mediator);
            var movieRequest = new GetSingleMovieRequest { TmdbId = "577922" };
            var movieResponse = await movieHandler.Handle(movieRequest, new System.Threading.CancellationToken());

            // Arrange
            var handler = new RequestExistsHandler(_mediaDbContext);
            var request = new RequestExistsRequest { Movie = movieResponse.Movie, UserId = userid };

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            return response.Exists;
        }
    }
}