using FakeItEasy;
using MediaRequest.Application.Clients;
using MediaRequest.Application.Queries;
using MediaRequest.Application.Queries.Movies;
using MediaRequest.Data;
using MediaRequest.Domain.Configuration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MediaRequest.Application.Tests
{
    public class RequestTests
    {
        private IMediator _mediator;
        private IHttpHelper _httpHelper;
        private IConfigurationRoot configurationBuilder;
        private IMediaDbContext mediaDbContext;

        private RadarrClient radarrClient;
        private TMDBClient tmdbClient;

        [SetUp]
        public async Task Setup()
        {
            #region Configuration classes
            var basePath = Path.GetFullPath("../../../../MediaRequest/");

            configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddYamlFile("settings.yaml", false, true)
                .Build();

            var servicePath = Options.Create(GetServicePathConfiguration());
            var apiKeys = Options.Create(GetApiKeysConfiguration());

            radarrClient = new RadarrClient(new System.Net.Http.HttpClient() { BaseAddress = new System.Uri(servicePath.Value.Radarr) });
            tmdbClient = new TMDBClient(new System.Net.Http.HttpClient() { BaseAddress = new System.Uri(servicePath.Value.TMDB) });
            #endregion

            #region Database Context
            var builder = new DbContextOptionsBuilder<MediaDbContext>();
            builder.UseInMemoryDatabase(databaseName: "MediaDbMemory");

            var dbContextOptions = builder.Options;
            mediaDbContext = new MediaDbContext(dbContextOptions);

            mediaDbContext.Request.Add(new Domain.UserRequest()
            {
                Id = 1,
                MovieId = "577922",
                UserId = new Guid().ToString()
            });

            await mediaDbContext.SaveChangesAsync();
            #endregion

            #region Http Helper
            _httpHelper = new HttpHelper(servicePath, apiKeys, radarrClient, tmdbClient);
            #endregion

            _mediator = A.Fake<IMediator>();
        }

        private ServicePath GetServicePathConfiguration()
        {
            var servicePath = new ServicePath();
            configurationBuilder.GetSection("settings:Path")
                .Bind(servicePath);

            return servicePath;
        }
        public ApiKeys GetApiKeysConfiguration()
        {
            var apiKeys = new ApiKeys();
            configurationBuilder.GetSection("settings:ApiKeys")
                .Bind(apiKeys);

            return apiKeys;
        }

        [Test]
        public async Task GetExistingRequest()
        {
            // Arrange
            var handler = new GetSingleMovieHandler(_httpHelper, mediaDbContext, _mediator);
            var request = new GetSingleMovieRequest { TmdbId = "577922" };

            // Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(response);
        }
    }
}