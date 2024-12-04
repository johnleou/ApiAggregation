using ApiAggregationProject.Api.Clients;
using ApiAggregationProject.Api.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace ApiAggregationProject.Tests
{
    public class PostClientTests
    {
        [Fact]
        public async Task ReturnsListOfPosts_WhenApiCallIsSuccessful()
        {
            // Arrange
            var expectedPosts = new List<Post>
            {
                new() { UserId = 1, Id = 1, Title = "test title 1", Body = "test body 1" },
                new() { UserId = 1, Id = 2, Title = "test title 2", Body = "test body 2" }
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
                    Content = JsonContent.Create(expectedPosts),
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(c => c["PostApi:BaseUrl"]).Returns("https://jsonplaceholder.typicode.com/posts");

            var PostClient = new PostClient(httpClient, configurationMock.Object);

            // Act
            var actualPosts = await PostClient.GetDataAsync();

            // Assert
            Assert.NotNull(actualPosts);
            Assert.Equal(expectedPosts.Count, actualPosts.Count);
            Assert.Equal(expectedPosts[0].Title, actualPosts[0].Title);
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // We expect it to be called exactly once
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
