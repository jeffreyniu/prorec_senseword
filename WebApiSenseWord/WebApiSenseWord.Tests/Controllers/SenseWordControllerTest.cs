using Application.Adapter.SenseWord.Criteria;
using Application.Adapter.SenseWord.Interfaces;
using CustomCore;
using Infrastructure.Entity.Entities;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using ThirdParties.Authentication.Interfaces;
using ThirdParties.OperationContracts;
using WebApiSenseWord.Controllers;
using WebApiSenseWord.Controllers.Contracts.SenseWord;
using WebApiSenseWord.Mappers.Interfaces;
using WebApiSenseWord.Models;

namespace WebApiSenseWord.Tests.Controllers
{
    [TestClass]
    public class SenseWordControllerTest
    {
        private Mock<IOptions<AppSettings>> _appSettingsMock;
        private Mock<ISenseWordAdapter> _senseWordAdapterMock;
        private Mock<ISenseWordMapper> _senseWordMapperMock;
        private Mock<ITokenAuth> _tokenAuthMock;

        private GetRequest _getRequest;

        private SenseWordEntity _senseWordEntity;
        private SenseWordModel _senseWordModel;
        private RequestHeaderContract _requestHeaderContract;

        private SenseWordController _senseWordController;

        [TestInitialize]
        public void Initialize()
        {
            _appSettingsMock = new Mock<IOptions<AppSettings>>();
            _senseWordAdapterMock = new Mock<ISenseWordAdapter>();
            _senseWordMapperMock = new Mock<ISenseWordMapper>();
            _tokenAuthMock = new Mock<ITokenAuth>();

            _senseWordEntity = new SenseWordEntity
            {
                ID = 1,
                Word = "word"
            };

            _senseWordModel = new SenseWordModel
            {
                ID = 1,
                Word = "word"
            };

            _requestHeaderContract = new RequestHeaderContract
            {
                UserId = 1,
                ApiKey = "apiKey",
                ApiSecret = "apiSecret"
            };

            _getRequest = new GetRequest
            {
                UserId = 1,
                TableName = "tableName",
                WordId = 1
            };

            _appSettingsMock.Setup(app => app.Value).Returns(new AppSettings { AuthenticationUrl = "/api/getauth", BaseAddressOfGo = "http://go.iniutoolbox.cn" });
            _senseWordAdapterMock.Setup(s => s.Get(It.IsAny<GetCriterion>())).Returns(_senseWordEntity);
            _senseWordMapperMock.Setup(s => s.Map(It.IsAny<SenseWordEntity>())).Returns(_senseWordModel);
            _tokenAuthMock.Setup(t => t.IsAuthenticated(It.IsAny<RequestHeaderContract>())).Returns(true);

            _senseWordController = new SenseWordController(_appSettingsMock.Object, _senseWordAdapterMock.Object, _senseWordMapperMock.Object, _tokenAuthMock.Object);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordController_Get_given_valid_id_when_executed_then_return_model_with_http_status_OK()
        {
            // Arrange

            // Act
            var actionResponseModel = _senseWordController.Get(_getRequest);
            var expectedDataJson = JsonSerializer.Serialize(_senseWordModel);

            // Assert
            Assert.IsNotNull(actionResponseModel);
            Assert.AreEqual(HttpStatusCode.OK, actionResponseModel.Status);
            Assert.AreEqual(expectedDataJson, actionResponseModel.Data);
            Assert.IsTrue(string.IsNullOrEmpty(actionResponseModel.Error));
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordController_Get_given_valid_id_but_not_authorized_when_executed_then_return_error_with_http_status_unauthorized()
        {
            // Arrange
            _tokenAuthMock.Setup(t => t.IsAuthenticated(It.IsAny<RequestHeaderContract>())).Throws(new UnauthorizedAccessException());

            // Act
            var actionResponseModel = _senseWordController.Get(_getRequest);

            // Assert
            Assert.IsNotNull(actionResponseModel);
            Assert.AreEqual(HttpStatusCode.Unauthorized, actionResponseModel.Status);
            Assert.IsTrue(string.IsNullOrEmpty(actionResponseModel.Data));
            Assert.IsNotNull(actionResponseModel.Error);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordController_Get_given_invalid_id_when_executed_then_return_error_with_http_status_bad_request()
        {
            // Arrange
            _tokenAuthMock.Setup(t => t.IsAuthenticated(It.IsAny<RequestHeaderContract>())).Throws(new UnauthorizedAccessException());
            _getRequest.WordId = 0;

            // Act
            var actionResponseModel = _senseWordController.Get(_getRequest);

            // Assert
            Assert.IsNotNull(actionResponseModel);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResponseModel.Status);
            Assert.IsTrue(string.IsNullOrEmpty(actionResponseModel.Data));
            Assert.IsNotNull(actionResponseModel.Error);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordController_Get_given_invalid_id_not_found_when_executed_then_return_error_with_http_status_not_found()
        {
            // Arrange
            _senseWordAdapterMock.Setup(s => s.Get(It.IsAny<GetCriterion>())).Returns((SenseWordEntity)null);
            _getRequest.WordId = 0;

            // Act
            var actionResponseModel = _senseWordController.Get(_getRequest);

            // Assert
            Assert.IsNotNull(actionResponseModel);
            Assert.AreEqual(HttpStatusCode.BadRequest, actionResponseModel.Status);
            Assert.IsTrue(string.IsNullOrEmpty(actionResponseModel.Data));
            Assert.IsNotNull(actionResponseModel.Error);
        }

        [TestMethod, TestCategory(TestCategories.UnitTest)]
        public void SenseWordController_Get_given_valid_id_but_throw_internal_error_when_executed_then_return_error_with_http_status_InternalServerError()
        {
            // Arrange
            _senseWordAdapterMock.Setup(s => s.Get(It.IsAny<GetCriterion>())).Throws(new Exception());

            // Act
            var actionResponseModel = _senseWordController.Get(_getRequest);

            // Assert
            Assert.IsNotNull(actionResponseModel);
            Assert.AreEqual(HttpStatusCode.InternalServerError, actionResponseModel.Status);
            Assert.IsTrue(string.IsNullOrEmpty(actionResponseModel.Data));
            Assert.IsNotNull(actionResponseModel.Error);
        }

    }
}
