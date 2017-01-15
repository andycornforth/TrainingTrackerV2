using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TrainingTracker.Service.Models;
using TrainingTracker.Service.Repositories;
using TrainingTracker.Service.Services;

namespace TrainingTracker.Service.Tests.Services
{
    [TestClass]
    public class LogServiceTests
    {
        private ILogService _service;
        private Mock<ILogRepository> _logRepositoryMock;

        [TestInitialize]
        public void BeforeEach()
        {
            _logRepositoryMock = new Mock<ILogRepository>();
            _logRepositoryMock.Setup(x => x.Add(It.IsAny<Log>())).Returns(1);

            _service = new LogService(_logRepositoryMock.Object);
        }

        [TestMethod]
        public void Should_add_log_to_repository()
        {
            var log = new Log
            {
                UserId = 1,
                Title = "Chest",
                DateAdded = DateTime.Now
            };

            var id = _service.Add(log);

            id.Should().Be(1);
            _logRepositoryMock.Verify(x => x.Add(log), Times.Once);
        }

        [TestMethod]
        public void Should_set_title_to_date_given_title_is_not_provided()
        {
            var log = new Log
            {
                UserId = 1,
                DateAdded = DateTime.Now
            };

            _service.Add(log);

            log.Title.Should().Be(log.DateAdded.ToString("g"));
        }

        [TestMethod]
        public void Should_set_date_added_given_date_added_is_not_already_set()
        {
            var log = new Log
            {
                UserId = 1,
                Title = "Chest",
            };

            var id = _service.Add(log);

            id.Should().Be(1);
            _logRepositoryMock.Verify(x => x.Add(log), Times.Once);
            log.DateAdded.Should().BeSameDateAs(DateTime.UtcNow);
        }

        [TestMethod]
        public void Should_return_log_given_valid_id()
        {
            _logRepositoryMock.Setup(x => x.GetById(1)).Returns(new Log());
            var log = _service.GetById(1);

            log.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_return_null_given_invalid_id()
        {
            _logRepositoryMock.Setup(x => x.GetById(1)).Returns(new Log());
            var log = _service.GetById(2);

            log.Should().BeNull();
        }

        [TestMethod]
        public void Should_return_users_logs_from_repository()
        {
            _logRepositoryMock.Setup(x => x.GetLogsByUserId(1)).Returns(new List<Log>());
            var logs = _service.GetLogsByUserId(1);

            logs.Should().NotBeNull();
            _logRepositoryMock.Verify(x => x.GetLogsByUserId(1), Times.Once);
        }
    }
}
