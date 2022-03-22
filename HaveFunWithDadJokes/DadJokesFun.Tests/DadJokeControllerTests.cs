using HaveFunWithDadJoke.Helper;
using HaveFunWithDadJokes.Controllers;
using HaveFunWithDadJokes.Helper;
using HaveFunWithDadJokes.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FakeHttpMessageHandler;
using Moq.Protected;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Routing;

namespace DadJokesFun.Tests
{
    public class DadJokeControllerTests
    {
        private  Mock<IClient> _mockClient;
        private  DadJokesController _controller;
        private readonly Mock<ILogger<DadJokesController>> _mockLogger;
        private readonly Mock<IAPIHelper> _mockHelper;

        public DadJokeControllerTests()
        {
            //// Setup Global Mock object for use by some of the test methods to speed up testing

            _mockClient = new Mock<IClient>();
            _mockClient.Setup(c => c.CreateClinet()).Returns(new HttpClient() { DefaultRequestHeaders = { } });
            _mockHelper = new Mock<IAPIHelper>();
            _mockHelper.Setup(h => h.CreateUrl("", "", 1, 1)).Returns("http://localhost:61478/api/dadjokes");
            _mockLogger = new Mock<ILogger<DadJokesController>>();

            _controller = new DadJokesController(_mockHelper.Object, _mockClient.Object, _mockLogger.Object);
            //_controller.Set
        }


       

        [Fact]
        public void GetRandomJoke_IsInvoked_And_Returns_Success()
        {
            var client = new HttpClient();
            
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:61478/api/dadjokes"),
                Method = HttpMethod.Get
            };           

            using (var response = client.SendAsync(request).Result)
            {
                Assert.True(HttpStatusCode.OK == response.StatusCode);
            }
        }

        [Fact]
        public void GetFilteredJokes_IsInvoked_And_Returns_SuccessUrl()
        {
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:61478/api/dadjokes?Term=forest"),
                Method = HttpMethod.Get
            };          

            using (var response = client.SendAsync(request).Result)
            {
                Assert.True(HttpStatusCode.OK == response.StatusCode);
            }
        }

        [Fact]
        public async Task DadJokeController_Returns_OK_Response()
        {
            var dadJokeResponse = new DadJokeResponse()
            {
                Text = "Joke",
                UserName = "123"
            };           

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(JsonConvert.SerializeObject(dadJokeResponse), Encoding.UTF8, "application/json"),
               })
               .Verifiable();

            var fakeHttpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:61478/api/dadjokes")

            };
            fakeHttpClient.DefaultRequestHeaders.Accept.Clear();
            fakeHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _mockClient = new Mock<IClient>();
            _mockClient.Setup(c => c.CreateClinet()).Returns(fakeHttpClient);

            _controller = new DadJokesController(_mockHelper.Object, _mockClient.Object, _mockLogger.Object);
            _controller.Configuration = new HttpConfiguration();
            _controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            _controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "jokes" } });

            _controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:61478/api/dadjokes")
            };

            var result = await _controller.GetRandomJoke();

            Assert.True(result.StatusCode == HttpStatusCode.OK );
          
        }

        [Fact]
        public async Task DadJokeController_Returns_Internal_Server_Error_Without_Formatter()
        {
            // Arrange
            DadJokesController controller = new DadJokesController(_mockHelper.Object, _mockClient.Object, _mockLogger.Object);

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost:61478/api/dadjokes")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "jokesAPI" } });

            // Act            
            var response = await controller.GetRandomJoke();

            // Assert
            Assert.True(response.StatusCode == HttpStatusCode.InternalServerError);
        }
    }
}
