using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingTracker.Api.Controllers;
using TrainingTracker.Api.Models;
using TrainingTracker.Service.Services;

namespace TrainingTracker.Api.Tests
{
    [TestClass]
    public class LogControllerTests
    {
        private LogController _controller;
        private Mock<ILogService> _logServiceMock;
        private Mock<IMapper> _mapperMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _logServiceMock = new Mock<ILogService>();
            _mapperMock = new Mock<IMapper>();
            _controller = new LogController(_logServiceMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public void Should_create_log_with_valid_post_request()
        {
            _logServiceMock.Setup(x => x.Add(It.IsAny<Service.Models.Log>())).Returns(1);

            var log = new Log
            {
                UserId = 1,
                Title = "Back"
            };

            var id = _controller.Post(log);

            id.Should().Be(1);
            _logServiceMock.Verify(x => x.Add(It.IsAny<Service.Models.Log>()), Times.Once);
        }

        [TestMethod]
        public void Should_get_log_by_id_with_valid_get_request()
        {
            var serviceLog = ServiceLog;
            var apiLog = new Log();

            _logServiceMock.Setup(x => x.GetById(1)).Returns(serviceLog);
            _mapperMock.Setup(x => x.Map<Service.Models.Log, Log>(It.IsAny<Service.Models.Log>())).Returns(apiLog);

            var log = _controller.Get(1);
            log.Should().Be(apiLog);
            _logServiceMock.Verify(x => x.GetById(1), Times.Once);
        }

        [TestMethod]
        public void Should_return_logs_for_user_id_given_user_exists()
        {
            _logServiceMock.Setup(x => x.GetLogsByUserId(1)).Returns(new List<Service.Models.Log>());
            _mapperMock.Setup(x => x.Map<IList<Service.Models.Log>, IList<Log>>(It.IsAny<IList<Service.Models.Log>>()))
                .Returns(new List<Log>());

            var apiLogs = _controller.GetLogsByUserId(1).ToList();

            _mapperMock.Verify(
                x => x.Map<IList<Service.Models.Log>, IList<Log>>(It.IsAny<IList<Service.Models.Log>>()), Times.Once);
            _logServiceMock.Verify(x => x.GetLogsByUserId(1), Times.Once);
            apiLogs.Should().BeOfType<List<Log>>();
        }

        private static Service.Models.Log ServiceLog => new Service.Models.Log
        {
            Id = 2,
            UserId = 3,
            Title = "Biceps",
            DateAdded = DateTime.MaxValue
        };
    }
}
