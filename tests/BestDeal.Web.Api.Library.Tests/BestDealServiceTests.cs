using AutoFixture;
using AutoMapper;
using BestDeal.FakeResponse.Models;
using BestDeal.Web.Api.Library.Implementation;
using BestDeal.Web.Api.Library.Interface;
using BestDeal.Web.Api.Library.Models;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace BestDeal.Web.Api.Library.Tests
{
    public class BestDealServiceTests
    {
        private const string response1 = @"{""total"": 2}";
        private const string response2 = @"{""amount"": 10}";
        private const string response3 = @"<quote>15</quote>}";

        private readonly IFixture _fixture;

        public BestDealServiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void GivenApi1ResponseIsInvalid_WhenGetBestDeails_ShouldThrowException()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();
            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(string.Empty);

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => bestDealService.GetBestDeal());
        }

        [Fact]
        public void GivenApi2ResponseIsInvalid_WhenGetBestDeails_ShouldThrowException()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();
            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(_fixture.Create<string>());
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(string.Empty);

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);

            // Assert
            Assert.Throws<ArgumentNullException>(() => bestDealService.GetBestDeal());
        }

        [Fact]
        public void GivenApi3ResponseIsInvalid_WhenGetBestDeails_ShouldThrowException()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();
            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(_fixture.Create<string>());
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(_fixture.Create<string>());
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(string.Empty);

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            
            // Assert
            Assert.Throws<ArgumentNullException>(() => bestDealService.GetBestDeal());
        }

        [Fact]
        public void GivenApi1ResponseHasMinimumValue_WhenGetBestDeails_ShouldGetAPI1InResponseModel()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();

            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(response1);
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(response2);
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(response3);

            var api1Response = _fixture.Build<API1Response>()
                                       .With(response => response.Total, 2)
                                       .Create();
            var api2Response = _fixture.Build<API2Response>()
                                       .With(response => response.Amount, 10)
                                       .Create();
            var api3Response = _fixture.Build<API3Response>()
                                       .With(response => response.Quote, 15)
                                       .Create();

            deserializeServiceMock.Setup(s => s.FromJsonString<API1Response>(It.IsAny<string>())).Returns(api1Response);
            deserializeServiceMock.Setup(s => s.FromJsonString<API2Response>(It.IsAny<string>())).Returns(api2Response);
            deserializeServiceMock.Setup(s => s.FromXmlString<API3Response>(It.IsAny<string>())).Returns(api3Response);

            mapperMock.Setup(m => m.Map<BestDealResponse>(api1Response)).Returns(new BestDealResponse { Amount = api1Response.Total });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api2Response)).Returns(new BestDealResponse { Amount = api2Response.Amount });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api3Response)).Returns(new BestDealResponse { Amount = api3Response.Quote });

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            var bestDeal = bestDealService.GetBestDeal();

            // Assert
            bestDeal.Should().NotBeNull();
            bestDeal.Amount.Should().Be(api1Response.Total);
            bestDeal.APIName.Should().Be("API1");
        }

        [Fact]
        public void GivenApi2ResponseHasMinimumValue_WhenGetBestDeails_ShouldGetAPI2InResponseModel()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();

            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(response1);
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(response2);
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(response3);

            var api1Response = _fixture.Build<API1Response>()
                                       .With(response => response.Total, 10)
                                       .Create();
            var api2Response = _fixture.Build<API2Response>()
                                       .With(response => response.Amount, 2)
                                       .Create();
            var api3Response = _fixture.Build<API3Response>()
                                       .With(response => response.Quote, 15)
                                       .Create();

            deserializeServiceMock.Setup(s => s.FromJsonString<API1Response>(It.IsAny<string>())).Returns(api1Response);
            deserializeServiceMock.Setup(s => s.FromJsonString<API2Response>(It.IsAny<string>())).Returns(api2Response);
            deserializeServiceMock.Setup(s => s.FromXmlString<API3Response>(It.IsAny<string>())).Returns(api3Response);

            mapperMock.Setup(m => m.Map<BestDealResponse>(api1Response)).Returns(new BestDealResponse { Amount = api1Response.Total });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api2Response)).Returns(new BestDealResponse { Amount = api2Response.Amount });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api3Response)).Returns(new BestDealResponse { Amount = api3Response.Quote });

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            var bestDeal = bestDealService.GetBestDeal();

            // Assert
            bestDeal.Should().NotBeNull();
            bestDeal.Amount.Should().Be(api2Response.Amount);
            bestDeal.APIName.Should().Be("API2");
        }

        [Fact]
        public void GivenApi3ResponseHasMinimumValue_WhenGetBestDeails_ShouldGetAPI3InResponseModel()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();

            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(response1);
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(response2);
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(response3);

            var api1Response = _fixture.Build<API1Response>()
                                       .With(response => response.Total, 10)
                                       .Create();
            var api2Response = _fixture.Build<API2Response>()
                                       .With(response => response.Amount, 15)
                                       .Create();
            var api3Response = _fixture.Build<API3Response>()
                                       .With(response => response.Quote, 2)
                                       .Create();

            deserializeServiceMock.Setup(s => s.FromJsonString<API1Response>(It.IsAny<string>())).Returns(api1Response);
            deserializeServiceMock.Setup(s => s.FromJsonString<API2Response>(It.IsAny<string>())).Returns(api2Response);
            deserializeServiceMock.Setup(s => s.FromXmlString<API3Response>(It.IsAny<string>())).Returns(api3Response);

            mapperMock.Setup(m => m.Map<BestDealResponse>(api1Response)).Returns(new BestDealResponse { Amount = api1Response.Total });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api2Response)).Returns(new BestDealResponse { Amount = api2Response.Amount });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api3Response)).Returns(new BestDealResponse { Amount = api3Response.Quote });

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            var bestDeal = bestDealService.GetBestDeal();

            // Assert
            bestDeal.Should().NotBeNull();
            bestDeal.Amount.Should().Be(api3Response.Quote);
            bestDeal.APIName.Should().Be("API3");
        }

        [Fact]
        public void GivenTwoResponsesHasSameValue_WhenGetBestDeails_ShouldGetAlphabetically()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();

            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(response1);
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(response2);
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(response3);

            var api1Response = _fixture.Build<API1Response>()
                                       .With(response => response.Total, 15)
                                       .Create();
            var api2Response = _fixture.Build<API2Response>()
                                       .With(response => response.Amount, 10)
                                       .Create();
            var api3Response = _fixture.Build<API3Response>()
                                       .With(response => response.Quote, 10)
                                       .Create();

            deserializeServiceMock.Setup(s => s.FromJsonString<API1Response>(It.IsAny<string>())).Returns(api1Response);
            deserializeServiceMock.Setup(s => s.FromJsonString<API2Response>(It.IsAny<string>())).Returns(api2Response);
            deserializeServiceMock.Setup(s => s.FromXmlString<API3Response>(It.IsAny<string>())).Returns(api3Response);

            mapperMock.Setup(m => m.Map<BestDealResponse>(api1Response)).Returns(new BestDealResponse { Amount = api1Response.Total });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api2Response)).Returns(new BestDealResponse { Amount = api2Response.Amount });
            mapperMock.Setup(m => m.Map<BestDealResponse>(api3Response)).Returns(new BestDealResponse { Amount = api3Response.Quote });

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            var bestDeal = bestDealService.GetBestDeal();

            // Assert
            bestDeal.Should().NotBeNull();
            bestDeal.Amount.Should().Be(api2Response.Amount);
            bestDeal.APIName.Should().Be("API2");
        }

        [Fact]
        public void GivenBestDealResponsesHasNoItem_WhenGetBestDeails_ShouldGetEmptyObject()
        {
            // Arrange
            var mapperMock = new Mock<IMapper>();
            var deserializeServiceMock = new Mock<IDeserializeService>();
            var apiResponseServiceMock = new Mock<IApiResponseService>();

            apiResponseServiceMock.Setup(s => s.GetApi1Response()).Returns(_fixture.Create<string>());
            apiResponseServiceMock.Setup(s => s.GetApi2Response()).Returns(_fixture.Create<string>());
            apiResponseServiceMock.Setup(s => s.GetApi3Response()).Returns(_fixture.Create<string>());

            // Act
            var bestDealService = new BestDealService(mapperMock.Object, deserializeServiceMock.Object, apiResponseServiceMock.Object);
            var bestDeal = bestDealService.GetBestDeal();

            // Assert
            bestDeal.Should().BeNull();
        }
    }
}
