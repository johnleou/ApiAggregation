using Moq;
using Microsoft.Extensions.Configuration;
using Moq.Protected;
using System.Net.Http.Json;
using System.Net;
using ApiAggregationProject.Api.Models;
using ApiAggregationProject.Api.Clients;

namespace ApiAggregationProject.Tests
{
    public class CommentClientTests
    {
        [Fact]
        public async Task ReturnsListsOfPosts_WhenApiCallIsSuccesfull()
        {
            var expectedComments = new List<Comment>
            {
                new() { PostId = 1, Id = 1, Name = "test name", Email = "test@example.com", Body = "test body" },
                new() { PostId = 1, Id = 2, Name = "test name 2", Email = "test2@example.com", Body = "test body 2" }
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
                    Content = JsonContent.Create(expectedComments)
                })
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object);
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(c => c["CommentApi:BaseUrl"]).Returns("https://jsonplaceholder.typicode.com/comments");

            var CommentClient = new CommentClient(httpClient, configurationMock.Object);

            // Act
            var actualComments = await CommentClient.GetDataAsync();

            // Assert
            Assert.NotNull(actualComments);
            Assert.Equal(expectedComments.Count, actualComments.Count);
            Assert.Equal(expectedComments[0].Name, actualComments[0].Name);
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), // We expect it to be called exactly once
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
