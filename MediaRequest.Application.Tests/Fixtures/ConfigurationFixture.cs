using MediaRequest.Application.Clients;
using MediaRequest.Data;
using MediaRequest.Domain.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MediaRequest.Application.Tests.Fixtures
{

    public class ConfigurationFixture
    {
        public RadarrClient radarrClient { get; set; }
        public TMDBClient tmdbClient { get; set; }
        public IOptions<ApiKeys> ApiKeys { get; set; }
        public IOptions<ServicePath> ServicePath { get; set; }

        private IConfigurationRoot configurationBuilder;

        public ConfigurationFixture()
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

            ServicePath = Options.Create(GetServicePathConfiguration());
            ApiKeys = Options.Create(GetApiKeysConfiguration());
            #endregion
        }

        public IConfigurationRoot GenerateConfiguration()
        {
            return configurationBuilder;
        }

        public IMediaDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<MediaDbContext>();
            builder.UseInMemoryDatabase(databaseName: "MediaDbMemory");

            var dbContextOptions = builder.Options;
            return new MediaDbContext(dbContextOptions);
        }

        private ServicePath GetServicePathConfiguration()
        {
            var servicePath = new ServicePath();
            configurationBuilder.GetSection("settings:Path")
                .Bind(servicePath);

            return servicePath;
        }
        private ApiKeys GetApiKeysConfiguration()
        {
            var apiKeys = new ApiKeys();
            configurationBuilder.GetSection("settings:ApiKeys")
                .Bind(apiKeys);

            return apiKeys;
        }
    }
}
