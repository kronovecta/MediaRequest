using MediaRequest.Infrastructure.Notifications.Discord;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MediaRequest.Tests
{
    public class DiscordNotificationTests : IClassFixture<DiscordNotificationTestFixtures>
    {

        [Fact]
        public async Task DiscordMessageTest()
        {
            // Arrange
            var client = new DiscordTestClient();
            var provider = new DiscordProvider();

            // Act
            var response = await provider.TestMessage();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
