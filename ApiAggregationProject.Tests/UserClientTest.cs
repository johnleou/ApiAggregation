using Moq;
using Microsoft.Extensions.Configuration;
using Moq.Protected;
using System.Net.Http.Json;
using System.Net;
using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Clients;

namespace ApiAggregationProject.Tests
{
    public class UserClientTests
    {
        [Fact]
        public async Task ReturnsListsOfUsers_WhenApiCallIsSuccesfull()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new() { Id = 1, Name = "test name", Email = "test@example.com", Username = "test username" },
                new() { Id = 2, Name = "test name 2", Email = "test2@example.com", Username = "test username" }
            };

            // Setup HttpClient mock
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = JsonContent.Create(expectedUsers)
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(c => c["UserApi:BaseUrl"]).Returns("https://jsonplaceholder.typicode.com/users");

            var UserClient = new UserClient(httpClient, configurationMock.Object);

            // Act
            var actualUsers = await UserClient.GetDataAsync();

            // Assert
            Assert.NotNull(actualUsers);
            Assert.Equal(expectedUsers.Count, actualUsers.Count);
            Assert.Equal(expectedUsers[0].Name, actualUsers[0].Name);
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // We expect it to be called exactly once
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
